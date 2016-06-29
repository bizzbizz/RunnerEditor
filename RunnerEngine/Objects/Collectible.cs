using RunnerEngine.Enums;

namespace RunnerEngine.Objects
{
	/// <summary>
	/// Endless Runner Static Game Object
	/// </summary>
	public class Collectible : BaseObject
	{
		public Collectible(float x, byte lane)
		{
			_x = x;
			_lane = lane;
			_variation = EndlessLevelGenerator.random.Next(0,3);
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
