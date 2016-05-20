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
			cats = Cats;
			CatX = catX;
		}

		/// <summary>
		/// Lane number (0: ground; 1,2,3: air; etc.)
		/// </summary>
		public override byte Lane { get { return 0; } }
		/// <summary>
		/// Type of game object
		/// </summary>
		public override ErgoType Type { get { return ErgoType.House; } }

		public override int Variation { get { return _variation; } }
		int _variation;

		/// <summary>
		/// Lanes of possible cats
		/// </summary>
		public CatLanes Cats;
		/// <summary>
		/// X of each cat in a possible lane (array size always 3)
		/// </summary>
		public float[] CatX;
		/// <summary>
		/// Width of chunk the house is in
		/// </summary>
		public float Width;
		internal IEnumerable<BaseObject> GetCats()
		{
			List<BaseObject> list = new List<BaseObject>();
			if (Cats.Has(CatLanes.DangerLane1))
				list.Add(new Cat(CatX[0], 0));
			if (Cats.Has(CatLanes.DangerLane2))
				list.Add(new Cat(CatX[1], 1));
			if (Cats.Has(CatLanes.DangerLane3))
				list.Add(new Cat(CatX[2], 2));
			return list;
		}
	}
}
