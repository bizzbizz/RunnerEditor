using RunnerEngine.Enums;
using RunnerEngine;
using System.Collections.Generic;

namespace RunnerEngine.Objects
{
	public class House : BaseObject
	{
		public House(float x)
		{
			_x = x;
			_variation = 0;
		}
		public House(float x, int variation)
		{
			_x = x;
			_variation = variation;
		}
		public House CloneAt(float x)
		{
			var house = new House(x, Variation);
			house.Width = Width;
			//house._cats = new Cat[3];
			return house;
		}
		public House(int variationId, float width, Lanes dangerLanes/*, params float[] catX*/)
		{
			_variation = variationId;
			Width = width;
			//CatSignature = dangerLanes;

			//_cats = new Cat[3];
			//if (dangerLanes.Has(Lanes.Lane1))
			//	_cats[0] = new Cat(catX[0], 1);
			//if (dangerLanes.Has(Lanes.Lane2))
			//	_cats[1] = new Cat(catX[1], 2);
			//if (dangerLanes.Has(Lanes.Lane3))
			//	_cats[2] = new Cat(catX[2], 3);
		}

		//internal Lanes CatSignature;
		/// <summary>
		/// Width of chunk the house is in
		/// </summary>
		public float Width;

		/// <summary>
		/// Lane number (0: ground; 1,2,3: air; etc.)
		/// </summary>
		public override byte Lane { get { return 0; } }
		/// <summary>
		/// Type of game object
		/// </summary>
		public override PoolObjectType Type { get { return PoolObjectType.House; } }

		public override int Variation { get { return _variation; } }
		int _variation;

		//public override IEnumerable<BaseObject> Children { get { return _cats; } }
		//internal Cat[] _cats;

	}
}
