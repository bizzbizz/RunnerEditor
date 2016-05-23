using RunnerEngine.Enums;
using System.Collections.Generic;
using System;

namespace RunnerEngine
{
	public class Sector : BaseObject
	{
		public District Scenery { get; set; }
		public float Width { get; private set; }
		public override IEnumerable<BaseObject> Children { get { return _objects; } }

		public override PoolObjectType Type { get { return PoolObjectType.Block; } }

		List<BaseObject> _objects;

		internal Seed seed;
		internal Chunk FirstChunk;
		private static float universalX;

		internal Sector()
		{
			//first sector (empty)
			seed = Seed.FirstSeed();
			_objects = new List<BaseObject>();
			Width = 5;

			_x = 0;
			universalX = Width;

			Scenery = new District
			{
				Background = 0,
				Houses = new RunnerEngine.Objects.House[0],
			};
		}

		internal Sector(District scenery, int blockNumber)
		{
			//all other sectors
			seed = Seed.NextSeed();
			_objects = new List<BaseObject>();
			Scenery = scenery;

			BuildIt();
			ChunkIt();

			if (Width < 12) Width = 12;
			_x = universalX;
			universalX += Width;
		}


		public List<GameplayDraft> build;
		void BuildIt()
		{
			build = new List<GameplayDraft>();

			GameplayDraft current = GameplayDraft.Empty;
			for (int i = 0; i < 8; i++)
			{
				if (EndlessLevelGenerator.random.Next(0, 3) == 0)
				{
					//lower possibility
					switch (current)
					{
						case GameplayDraft.Empty:
							//keep empty
							break;
						case GameplayDraft.Tree:
							//switch to house
							current = GameplayDraft.House;
							break;
						case GameplayDraft.House:
							//switch to tree
							current = GameplayDraft.Tree;
							break;
						default:
							break;
					}
				}
				else
				{
					//higher possibility
					if (current == GameplayDraft.Empty)
					{
						//advance to tree
						if (EndlessLevelGenerator.random.Next(0, 2) == 0)
							current = GameplayDraft.Tree;
						//advance to house
						else
							current = GameplayDraft.House;
					}
					//else keep current GameplayDraft for duplication
				}
				build.Add(current);
			}
			build.Add(GameplayDraft.Empty);

			//insert eagle when two neighbor trees found
			int currentIndex = 1;
			while (currentIndex < build.Count)
			{
				if (build[currentIndex - 1] == GameplayDraft.Tree
					&& build[currentIndex] == GameplayDraft.Tree)
				{
					build.Insert(currentIndex, GameplayDraft.Eagle);
					//jump forward to avoid many eagles
					currentIndex += 2;
				}
				else currentIndex++;
			}

			//add lanes to build
			currentIndex = 1;
			while (currentIndex < build.Count)
			{
				if (build[currentIndex - 1] == GameplayDraft.Empty)
				{
					//after an empty
					build[currentIndex] |= DraftBuilder.RandomSafeLane();
				}
				else
				{
					//continue
					var lane = DraftBuilder.ContinueSafeLane(build[currentIndex - 1]);
					if (build[currentIndex].Has(GameplayDraft.Tree))
					{
						//on a tree
						//if (lane.Has(GameplayDraft.SafeLane1))
						//	lane = GameplayDraft.Empty;
					}
					else
					{
						build[currentIndex] |= lane;
					}
				}
				currentIndex++;
			}

			//block some lanes
			currentIndex = 0;
			while (currentIndex < build.Count)
			{
				build[currentIndex] |= DraftBuilder.BlockSafeLane(build[currentIndex]);
				currentIndex++;
			}
		}
		void ChunkIt()
		{
			//convert to chunks
			int currentIndex = 1;
			FirstChunk = new Chunk(null, build[0], Scenery);
			Chunk currentChunk = FirstChunk;
			while (currentIndex < build.Count)
			{
				currentChunk.Next = new Chunk(currentChunk, build[currentIndex], Scenery);
				currentChunk = currentChunk.Next;
				currentIndex++;
			}

			//finalize
			currentChunk = FirstChunk;
			bool hasCoin = false;//current flow
			while (currentChunk != null)
			{
				//width
				Width += currentChunk.Width;
				//add coins and people
				if (EndlessLevelGenerator.random.Next(0, 3) == 0)
				{
					//chance current flow
					if (hasCoin)
					{
						hasCoin = false;
						_objects.AddRange(currentChunk.MakePeople(EndlessLevelGenerator.random.Next(1, 3)));
					}
					else
					{
						hasCoin = true;
						_objects.AddRange(currentChunk.MakeCoins((float)EndlessLevelGenerator.random.NextDouble() * 1.75f + .5f));
					}

				}
				else
				{
					//keep current flow
					if (hasCoin)
					{
						_objects.AddRange(currentChunk.MakeCoins((float)EndlessLevelGenerator.random.NextDouble() * 1.75f + .5f));
					}
					else
					{
						_objects.AddRange(currentChunk.MakePeople(EndlessLevelGenerator.random.Next(1, 3)));
					}
				}
				//add tree, eagle, house and cat
				_objects.AddRange(currentChunk.MakeOthers());

				//goto next
				currentChunk = currentChunk.Next;
			}
		}
	}
}
