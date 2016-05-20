using RunnerEngine.Enums;

namespace RunnerEngine.Objects
{
	/// <summary>
	/// Endless Runner Static Game Object
	/// </summary>
	public class Coin : BaseObject
	{
		public Coin(float x, byte lane, int count, float innerSpace)
		{
			_x = x;
			_lane = lane;
			_coinCount = count;
			_innerSpace = innerSpace;
		}

		/// <summary>
		/// 1,2,4 bitwise lane number e.g. 7=1&2&3</para>
		/// </summary>
		public override byte Lane { get { return _lane; } }
		byte _lane;
		/// <summary>
		/// Type of game object
		/// </summary>
		public override ErgoType Type { get { return ErgoType.Coin; } }

		/// <summary>
		/// Only applicable to coins
		/// </summary>
		public override int CoinCount { get { return _coinCount; } }
		int _coinCount;
		public override float InnerSpace { get { return _innerSpace; } }
		float _innerSpace;
	}
}
