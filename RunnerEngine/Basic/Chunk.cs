using RunnerEngine.Enums;
using RunnerEngine.Objects;
using System.Collections.Generic;

namespace RunnerEngine
{
	public class Chunk
	{
		public float StartX;
		public float Width;
		public byte MainLanes;//bits of lanes (no zero) (lanes 1,2,3 represented as 1,2,4)
		public Chunk Next;
		public House House;
		public BaseObject[] Cells;//vertical cells (0:ground,1,2,3)

		Chunk() { }
		internal Chunk(Chunk prev, GameplayDraft draft, District scenery)
		{
			if(prev == null)
			{
				StartX = 0;
			}
			else
			{
				StartX = prev.StartX + prev.Width;
			}
			MainLanes = 0;
			if (draft.Has(GameplayDraft.SafeLane1))
				MainLanes += 1;
			if (draft.Has(GameplayDraft.SafeLane2))
				MainLanes += 2;
			if (draft.Has(GameplayDraft.SafeLane3))
				MainLanes += 4;
			Cells = new BaseObject[4];
			if(draft.Has(GameplayDraft.House))
			{
				House = scenery.FindSuitableHouse(draft);
			}
			if(draft.Has(GameplayDraft.Tree))
			{
				Cells[0] = new Tree(StartX);
			}
			if (draft.Has(GameplayDraft.Eagle))
			{
				if(draft.Has(GameplayDraft.DangerLane1))
					Cells[1] = new Eagle(StartX, 1);
				if (draft.Has(GameplayDraft.DangerLane2))
					Cells[2] = new Eagle(StartX, 2);
				if (draft.Has(GameplayDraft.DangerLane3))
					Cells[3] = new Eagle(StartX, 3);
			}
		}
		//public Chunk(StaticObject cell)
		//{
		//	StartX = cell.X;
		//	MainLanes = 7;
		//	Cells = new StaticObject?[4];
		//	if (cell.Type != ErgoType.None)
		//	{
		//		Cells[cell.Lane] = cell;
		//	}
		//	if (cell.Type.Has(ErgoType.Good))
		//	{
		//		//on main lane
		//		MainLanes |= cell.Lane;
		//	}
		//	else if (cell.Type.Has(ErgoType.Bad))
		//	{
		//		//off main lane
		//		switch (cell.Lane)
		//		{
		//			case 1:
		//				MainLanes &= 6;//b110 lane 2,3
		//				break;
		//			case 2:
		//				MainLanes &= 5;//b101 lane 1,3
		//				break;
		//			case 3:
		//				MainLanes &= 3;//b011 lane 1,2
		//				break;
		//			default:
		//				break;
		//		}
		//	}
		//	else
		//	{
		//		//not related to main lane
		//	}
		//}
		//public Chunk AddERGO(StaticObject cell, float minDist)
		//{
		//	//find x
		//	float dist = System.Math.Min(minDist, Width / 2);
		//	cell.X = StartX + dist;

		//	//create chunk
		//	Chunk chunk = new Chunk(cell);

		//	//update widths
		//	chunk.Width = Width - dist;
		//	Width = dist;

		//	//update chain
		//	chunk.Next = Next;
		//	Next = chunk;

		//	//return new chunk
		//	return chunk;
		//}

		public IEnumerable<BaseObject> MakeHouseAndCats()
		{
			if (House == null) return null;
			List<BaseObject> list = new List<BaseObject>();
			list.Add(new House(StartX, House.Variation));
			list.AddRange(House.GetCats());
			return list;
		}
		public IEnumerable<BaseObject> MakePeople(int count)
		{
			var people = new BaseObject[count];
			for (int i = 0; i < count; i++)
			{
				people[i] = new MobilePerson(StartX);
			}
			return people;
		}
		public IEnumerable<BaseObject> MakeCoins(float innerSpace)
		{
			List<BaseObject> list = new List<BaseObject>();
			int count = (int)(Width / innerSpace);
			if (MainLanes == 7)
			{
				list.Add(new Coin(StartX, MainLanes, 3, innerSpace));
				StartX += Width / 2;
				count /= 2;
				MainLanes = 2;
			}
			else if (MainLanes == 5 || MainLanes == 6)
			{
				list.Add(new Coin(StartX, MainLanes, 3, innerSpace));
				StartX += Width / 2;
				count /= 2;
				MainLanes = 4;
			}
			else if (MainLanes == 3)
			{
				list.Add(new Coin(StartX, MainLanes, 3, innerSpace));
				StartX += Width / 2;
				count /= 2;
				MainLanes = 1;
			}
			list.Add(new Coin(StartX, MainLanes, count, innerSpace));
			return list;
		}
	}
}
