using RunnerEngine.Enums;
using RunnerEngine.Objects;
using System.Collections.Generic;

namespace RunnerEngine
{
	public class Chunk
	{
		public Chunk Next
		{
			get; set;
		}
		public float Width
		{
			get; private set;
		}

		float _startX;//relative to sector
		Lanes _mainLanes;//safe lanes
		GameplayDraft _draft;//supposed objects
		District _district;//district scenery
		BaseObject[] _cells;//vertical cells (0:ground,1,2,3)

		Chunk() { }
		internal Chunk(Chunk prev, GameplayDraft draft, District district)
		{
			//set start x
			if (prev == null)
			{
				_startX = 0;
			}
			else
			{
				_startX = prev._startX + prev.Width;
			}

			//set other members
			_draft = draft;
			_mainLanes = (Lanes)(draft & GameplayDraft.SafeLanes);
			_district = district;

			//set cells
			_cells = new BaseObject[4];
			if (draft.Has(GameplayDraft.Tree))
			{
				//select variation
				bool large = draft.Has(GameplayDraft.DangerLane2);
				int variation = EndlessLevelGenerator.GetRandomTree(large);

				//add a tree
				_cells[0] = new Tree(_startX, large, variation);
				//move pointer forward
				Width += 2f;

				//add extra eagle
				if (draft.Has(GameplayDraft.DangerLane3))
				{
					_cells[3] = new Eagle(_startX + Width, 3);
					//move pointer forward
					Width += 2f;
				}
			}
			if (draft.Has(GameplayDraft.House))
			{
				//select variation
				int hIndex = EndlessLevelGenerator.random.Next(0, _district.Houses.Length);
				var hPrototype = _district.Houses[hIndex % _district.Houses.Length];
				var house = hPrototype.CloneAt(_startX + Width);

				//add a house
				_cells[0] = house;
				//move pointer forward
				Width += house.Width;


				//add cats or eagles
				if (draft.Has(GameplayDraft.DangerLane1) && hPrototype.CatSignature.Has(Lanes.Lane1))
				{
					//add cat on first floor
					house.CatSignature |= Lanes.Lane1;
					house._cats[0] = hPrototype._cats[0].Clone();
				}
				else if (draft.Has(GameplayDraft.DangerLane2) && hPrototype.CatSignature.Has(Lanes.Lane2))
				{
					//add cat on second floor
					house.CatSignature |= Lanes.Lane2;
					house._cats[1] = hPrototype._cats[1].Clone();
				}
				else if (draft.Has(GameplayDraft.DangerLane3) && hPrototype.CatSignature.Has(Lanes.Lane3))
				{
					//add cat on third floor
					house.CatSignature |= Lanes.Lane3;
					house._cats[2] = hPrototype._cats[2].Clone();
				}
				else
				{
					//add eagle instead of cat
					if(EndlessLevelGenerator.random.Next(0, 2) == 0)
						_cells[2] = new Eagle(_startX, 2);
					else
						_cells[3] = new Eagle(_startX, 3);
				}
			}
		}

		public IEnumerable<BaseObject> MakeOthers()
		{
			var list = new List<BaseObject>();
			foreach (var cell in _cells)
			{
				if (cell != null)
				{
					list.Add(cell);
				}
			}
			return list;
		}
		public IEnumerable<BaseObject> MakePeople(int count)
		{
			var people = new BaseObject[count];
			float x = 0;
			for (int i = 0; i < count; i++)
			{
				people[i] = new MobilePerson(x + _startX);
				x += (1 + (float)(EndlessLevelGenerator.random.NextDouble() * 3));
			}
			Width = System.Math.Max(x, Width);
			return people;
		}
		public IEnumerable<BaseObject> MakeCoins(float innerSpace)
		{
			List<BaseObject> list = new List<BaseObject>();
			Width = System.Math.Max(innerSpace * 4, Width);
			int count = (int)(Width / innerSpace);
			list.AddRange(Coin.CreateCollection(_startX, _mainLanes, count, innerSpace));
			return list;
		}
	}
}
