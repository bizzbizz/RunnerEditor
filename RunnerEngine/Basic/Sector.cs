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
		public List<GameplayDraft> build;

		internal Seed seed;
		internal Chunk FirstChunk;
		private static float universalX;

		internal Sector()
		{
			//first sector (empty)
			seed = Seed.FirstSeed();
			_objects = new List<BaseObject>();
			Width = 20;

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

			BuildIt(blockNumber);
			ChunkIt();

			//if (Width < 12) Width = 12;
			_x = universalX;
			universalX += Width;
		}


		void BuildIt(int blockNumber)
		{
			build = new List<GameplayDraft>();

			GameplayDraft current = GameplayDraft.Empty;
			build.Add(current);
			for (int i = 1; i < 10 + blockNumber * 2; i++)
			{
				int phase = EndlessLevelGenerator.random.Next(0, 10);
				if (phase < 2)
					current = GameplayDraft.Empty | DraftBuilder.RandomDanger(false);
				else if (phase < 5)
					current = GameplayDraft.Tree | DraftBuilder.RandomDanger(true);
				else
					current = GameplayDraft.House | DraftBuilder.RandomDanger(false);
				build.Add(current);
			}

			//add lanes to build
			int currentIndex = 1;
			build[0] |= DraftBuilder.StartSafeLane();
			while (currentIndex < build.Count)
			{
				//continue a lane to a neighbor lane
				build[currentIndex] |= DraftBuilder.ContinueSafeLane(build[currentIndex - 1], build[currentIndex]);
				//build[currentIndex] |= DraftBuilder.ParallelDangerLane(build[currentIndex]);
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
				//new chunk
				var newChunk = new Chunk(currentChunk, build[currentIndex], Scenery);
				_objects.AddRange(newChunk.MakeOthers());
				if (currentIndex % 5 != 0 && currentIndex % 7 != 0 && currentIndex % 8 != 0 && currentIndex % 13 != 0)
					_objects.AddRange(newChunk.MakeCoins((float)EndlessLevelGenerator.random.NextDouble() * .25f + .75f));
				else
					_objects.AddRange(newChunk.MakePeople(EndlessLevelGenerator.random.Next(1, 3)));

				//sector width
				Width += newChunk.Width;

				if (Width - _lastFoodX > 4 && EndlessLevelGenerator.random.Next(0, 2) > 0)
				{
					Width += 2;
					_objects.Add(new Objects.Collectible(Width, (byte)EndlessLevelGenerator.random.Next(1, 4)));
					_lastFoodX = Width;
				}

				//next chunk
				currentChunk.Next = newChunk;
				currentChunk = currentChunk.Next;
				currentIndex++;
			}
			Width += 2;
		}
		float _lastFoodX = 0;
	}
}
