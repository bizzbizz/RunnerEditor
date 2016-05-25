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

		float _startX;
		Lanes _mainLanes;
		GameplayDraft _draft;
		District _scenery;
		BaseObject[] _cells;//vertical cells (0:ground,1,2,3)

		Chunk() { }
		internal Chunk(Chunk prev, GameplayDraft draft, District scenery)
		{
			if (prev == null)
			{
				_startX = 0;
			}
			else
			{
				_startX = prev._startX + prev.Width;
			}
			_draft = draft;
			_mainLanes = (Lanes)(draft & GameplayDraft.SafeLanes);
			_cells = new BaseObject[4];
			_scenery = scenery;
			if (draft.Has(GameplayDraft.Tree))
			{
				bool large = draft.Has(GameplayDraft.DangerLane2);
				int variation = EndlessLevelGenerator.GetRandomTree(large);
				_cells[0] = new Tree(_startX, large, variation);

				if (draft.Has(GameplayDraft.DangerLane3))
					_cells[3] = new Eagle(_startX, 3);
			}
			if (draft.Has(GameplayDraft.House))
			{
				int hIndex = EndlessLevelGenerator.random.Next(0, _scenery.Houses.Length);
				var hPrototype = _scenery.Houses[hIndex % _scenery.Houses.Length];
				var house = hPrototype.CloneAt(_startX);
				_cells[0] = house;
				Width += house.Width;

				bool rand = EndlessLevelGenerator.random.Next(0, 2) == 0;

				//add cats or eagles
				if (draft.Has(GameplayDraft.DangerLane1) && hPrototype.CatSignature.Has(Lanes.Lane1))
				{
					house.CatSignature |= Lanes.Lane1;
					house._cats[0] = hPrototype._cats[0].Clone();
				}
				else
				{
					//_cells[1] = new Eagle(_startX, 1);
				}
				if (draft.Has(GameplayDraft.DangerLane2) && hPrototype.CatSignature.Has(Lanes.Lane2))
				{
					house.CatSignature |= Lanes.Lane2;
					house._cats[1] = hPrototype._cats[1].Clone();
				}
				else
				{
					if(rand)
					_cells[2] = new Eagle(_startX, 2);
				}
				if (draft.Has(GameplayDraft.DangerLane3) && hPrototype.CatSignature.Has(Lanes.Lane3))
				{
					house.CatSignature |= Lanes.Lane3;
					house._cats[2] = hPrototype._cats[2].Clone();
				}
				else
				{
					if(!rand)
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
