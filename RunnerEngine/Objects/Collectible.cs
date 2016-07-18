using RunnerEngine.Enums;

namespace RunnerEngine.Objects
{
	/// <summary>
	/// Endless Runner Static Game Object
	/// </summary>
	public class Collectible : BaseObject
	{
		Collectible()
		{

		}
		public static Collectible CreateNext(float x)
		{
			nextVariationIndex++;
			nextVariationIndex %= variations.Length;
			return new Collectible
			{
				_x = x,
				_lane = (byte)EndlessLevelGenerator.random.Next(1, 4),
				_variation = variations[nextVariationIndex],
			};
		}
		/*
		0,1,2	foods
		4		drink
		5		turbo
		6		shield
		7		magnet
		8		x2
		9		oil
		*/
		static int[] variations = new int[]
		{
			4, 0, 6, 9, 7, 5, 1, 4, 0, 2, 9, 0, 5, 0, 4, 1, 8, 7, 4, 5, 0
		};
		static int nextVariationIndex = 0;

		public static Collectible CreateFood(float x, byte lane)
		{
			return new Collectible
			{
				_x = x,
				_lane = lane,
				_variation = EndlessLevelGenerator.random.Next(0, 3),
			};
		}
		public static Collectible CreateEnergy(float x, byte lane)
		{
			return new Collectible
			{
				_x = x,
				_lane = lane,
				_variation = 4,
			};
		}
		public static Collectible CreateTimedCollectible(float x, byte lane)
		{
			return new Collectible
			{
				_x = x,
				_lane = lane,
				_variation = EndlessLevelGenerator.random.Next(5, 10),
			};
		}
		public Collectible(float x, bool largeTree)
		{
			_x = x;
			_lane = (byte)(largeTree ? 2 : 1);
			_variation = 3;
		}

		/// <summary>
		/// Lane number (0: ground; 1,2,3: air; etc.)
		/// </summary>
		public override byte Lane { get { return _lane; } }
		byte _lane;
		/// <summary>
		/// Type of game object
		/// </summary>
		public override PoolObjectType Type { get { return PoolObjectType.Collectible; } }

		public override int Variation { get { return _variation; } }
		int _variation;
	}
}
