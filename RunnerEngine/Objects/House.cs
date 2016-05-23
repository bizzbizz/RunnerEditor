using RunnerEngine.Enums;
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
		public House(int variationId, float width, CatLanes cats, params float[] catX)
		{
			_variation = variationId;
			Width = width;
			CatSignature = cats;

			_cats = new List<BaseObject>();
			if (cats.Has(CatLanes.DangerLane1))
				_cats.Add(new Cat(catX[0], 0));
			if (cats.Has(CatLanes.DangerLane2))
				_cats.Add(new Cat(catX[1], 1));
			if (cats.Has(CatLanes.DangerLane3))
				_cats.Add(new Cat(catX[2], 2));
		}

		internal CatLanes CatSignature;
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

		public override IEnumerable<BaseObject> Children { get { return _cats; } }
		List<BaseObject> _cats;
	}
}
