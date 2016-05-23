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
				if (House != null)
					Width += House.Width;
			}
			if(draft.Has(GameplayDraft.Tree))
			{
				Cells[0] = new Tree(StartX);
				Width += 2;
			}
			if (draft.Has(GameplayDraft.Eagle))
			{
				if(draft.Has(GameplayDraft.DangerLane1))
					Cells[1] = new Eagle(StartX, 1);
				if (draft.Has(GameplayDraft.DangerLane2))
					Cells[2] = new Eagle(StartX, 2);
				if (draft.Has(GameplayDraft.DangerLane3))
					Cells[3] = new Eagle(StartX, 3);
				Width += 2;
			}
		}

		public IEnumerable<BaseObject> MakeOthers()
		{
			var list = new List<BaseObject>();
			if (House != null)
			{
				list.Add(new House(StartX, House.Variation));
			}
			foreach (var cell in Cells)
			{
				if(cell!=null)
				{
					list.Add(cell);
				}
			}
			return list;
		}
		public IEnumerable<BaseObject> MakePeople(int count)
		{
			var people = new BaseObject[count];
			float x = StartX;
			for (int i = 0; i < count; i++)
			{
				people[i] = new MobilePerson(x);
				x += 2;
			}
			if (x > Width) Width = x;
			return people;
		}
		public IEnumerable<BaseObject> MakeCoins(float innerSpace)
		{
			List<BaseObject> list = new List<BaseObject>();
			int count = (int)(Width / innerSpace);
			float x = StartX;
			if (MainLanes == 7)
			{
				list.AddRange(Coin.CreateCollection(x, MainLanes, 3, innerSpace));
				x += innerSpace * 3;
				count /= 2;
				MainLanes = 2;
			}
			else if (MainLanes == 5 || MainLanes == 6)
			{
				list.AddRange(Coin.CreateCollection(x, MainLanes, 3, innerSpace));
				x += innerSpace * 3;
				count /= 2;
				MainLanes = 4;
			}
			else if (MainLanes == 3)
			{
				list.AddRange(Coin.CreateCollection(x, MainLanes, 3, innerSpace));
				x += innerSpace * 3;
				count /= 2;
				MainLanes = 1;
			}
			list.AddRange(Coin.CreateCollection(x, MainLanes, count, innerSpace));
			x += innerSpace * count;
			if (x > Width) Width = x;
			return list;
		}
	}
}
