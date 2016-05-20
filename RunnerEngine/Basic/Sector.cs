using RunnerEngine.Enums;
using System.Collections.Generic;

namespace RunnerEngine
{
	public class Sector
	{
		public District Scenery { get; set; }
		public List<BaseObject> Objects { get; private set; }
		public float Width { get; private set; }

		internal Seed seed;
		internal Chunk FirstChunk;

		internal Sector()
		{
			//first sector (empty)
			seed = Seed.FirstSeed();
			Objects = new List<BaseObject>();
			Width = 5;

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
			Objects = new List<BaseObject>();

			Scenery = scenery;

			BuildIt();
			ChunkIt();
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
						//advance to tree
						current = GameplayDraft.Tree;
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
					build[currentIndex] |= DraftBuilder.Random();
				}
				else
				{
					//continue
					var lane = DraftBuilder.Continue(build[currentIndex - 1]);
					if (build[currentIndex].Has(GameplayDraft.Tree))
					{
						//on a tree
						if (lane.Has(GameplayDraft.SafeLane1))
							lane = GameplayDraft.Empty;
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
				build[currentIndex] |= DraftBuilder.Block(build[currentIndex]);
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
						Objects.AddRange(currentChunk.MakePeople(EndlessLevelGenerator.random.Next(1, 3)));
					}
					else
					{
						hasCoin = true;
						Objects.AddRange(currentChunk.MakeCoins((float)EndlessLevelGenerator.random.NextDouble() * 1.75f + .5f));
					}

				}
				else
				{
					//keep current flow
					if (hasCoin)
					{
						Objects.AddRange(currentChunk.MakeCoins((float)EndlessLevelGenerator.random.NextDouble() * 1.75f + .5f));
					}
					else
					{
						Objects.AddRange(currentChunk.MakePeople(EndlessLevelGenerator.random.Next(1, 3)));
					}
				}
				//add house and cat
				if (currentChunk.House != null)
				{
					Objects.AddRange(currentChunk.MakeHouseAndCats());
				}
				currentChunk = currentChunk.Next;
			}
		}



		#region Private Methods
		//BuildSpots();
		//BuildCells();
		//BuildCoins();
		//SetCoinsAndObjects();
		//void BuildSpots()
		//{
		//	//randomize variations
		//	int treeGroupVariation = 2 * EndlessLevelGenerator.random.Next(0, 2);

		//	//choose spot objects
		//	activeSpots = new StaticObject[Scenery.Spots.Length];
		//	for (int i = 0; i < activeSpots.Length; i++)
		//	{
		//		activeSpots[i] = Scenery.Spots[i];
		//		if (activeSpots[i].Type == Enums.ErgoType.Tree)
		//		{
		//			activeSpots[i].Variation = EndlessLevelGenerator.random.Next(0, 2) + treeGroupVariation;
		//		}
		//		else if(activeSpots[i].Type == Enums.ErgoType.LargeTree)
		//		{
		//			activeSpots[i].Variation = EndlessLevelGenerator.random.Next(0, 3);
		//		}
		//	}

		//	//make chunks
		//	FirstChunk = new Chunk(DynamicObject.Empty);
		//	Chunk currentChunk = FirstChunk;
		//	//chunk active spots
		//	for (int i = 0; i < activeSpots.Length; i++)
		//	{
		//		//set width of current chunk
		//		currentChunk.Width = activeSpots[i].X - currentChunk.StartX;

		//		//add new chunk
		//		currentChunk.Next = new Chunk(activeSpots[i]);

		//		//goto next
		//		currentChunk = currentChunk.Next;
		//	}
		//	currentChunk.Width = Scenery.Width - currentChunk.StartX;

		//}
		//void BuildCells()
		//{
		//	//init other cells
		//	int ncc = seed.ncc;
		//	int nccDigits;
		//	int nccCount = ncc.Ones(out nccDigits);
		//	int mobiles = seed.mobiles;
		//	int mobilesDigits;
		//	int mobileCount = mobiles.Ones(out mobilesDigits);
		//	int neutralCount = 10;

		//	int counter = 0;

		//	otherCells = new DynamicObject[nccCount + mobileCount];
		//	neutralCells = new DynamicObject[neutralCount];

		//	//generate and add non-coin collectible objects
		//	for (int i = 0; i < nccDigits; i++)
		//	{
		//		if (ncc.HasOne())
		//		{
		//			otherCells[counter] = DynamicObject.NccObjects[i % DynamicObject.NccObjects.Length];//[i]
		//			counter++;
		//		}
		//		ncc >>= 1;
		//	}

		//	//generate and add mobile objects
		//	for (int i = 0; i < mobilesDigits; i++)
		//	{
		//		if (mobiles.HasOne())
		//		{
		//			otherCells[counter] = DynamicObject.MobileObstacles[i % DynamicObject.MobileObstacles.Length];//[i]
		//			counter++;
		//		}
		//		mobiles >>= 1;
		//	}

		//	//insert ncc and mobile chunks between activespot
		//	Chunk currentChunk = FirstChunk;
		//	float minDist = 3;
		//	for (int i = 0; i < otherCells.Length; i++)
		//	{
		//		while (currentChunk != null && currentChunk.Width < minDist)
		//		{
		//			currentChunk = currentChunk.Next;
		//		}
		//		//add cell after current chunk
		//		if (currentChunk != null)
		//		{
		//			currentChunk = currentChunk.AddERGO(otherCells[i], minDist);
		//		}
		//	}
		//}
		//void BuildCoins()
		//{
		//	Chunk currentChunk = FirstChunk;
		//	float minDist = 3;
		//	int coinsRemaining = seed.coins;
		//	int chunkIdx = 0;
		//	int random = seed.random;
		//	while (currentChunk != null && coinsRemaining > 0)
		//	{
		//		while (currentChunk != null && currentChunk.Width < minDist)
		//		{
		//			//put coins in chunk
		//			var lanes = random.LSB3();
		//			currentChunk.MainLanes &= lanes;
		//			coinsRemaining -= (int)(currentChunk.MainLanes.Ones() * currentChunk.Width / Chunk.InnerSpace);

		//			//next chunk
		//			chunkIdx++;
		//			random >>= 1;
		//			if (chunkIdx > 30)
		//			{
		//				chunkIdx = 0;
		//				random = seed.random;
		//			}
		//			currentChunk = currentChunk.Next;
		//		}
		//		//add chunk after current chunk
		//		if (currentChunk != null)
		//		{
		//			//don't advance here
		//			currentChunk.AddERGO(DynamicObject.Empty, minDist);
		//		}
		//	}
		//}
		//void SetCoinsAndObjects()
		//{
		//	var ergolist = new List<DynamicObject>();
		//	Chunk current = FirstChunk;
		//	while (current != null)
		//	{
		//		//add objects
		//		for (int i = 0; i < current.Cells.Length; i++)
		//		{
		//			if (current.Cells[i] != null)
		//				ergolist.Add(current.Cells[i].Value);
		//		}
		//		//add coins
		//		if (current.MainLanes > 0)
		//			ergolist.AddRange(current.MakeCoins());
		//		current = current.Next;
		//	}

		//	Objects = ergolist;
		//}
		#endregion

	}
}
