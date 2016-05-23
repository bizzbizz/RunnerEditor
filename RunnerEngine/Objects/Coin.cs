using RunnerEngine.Enums;
using System.Collections.Generic;

namespace RunnerEngine.Objects
{
	/// <summary>
	/// Endless Runner Static Game Object
	/// </summary>
	public class Coin : BaseObject
	{
		public Coin(float x, byte lane)
		{
			_x = x;
			_lane = lane;
		}
		public static IEnumerable<BaseObject> CreateCollection(float startX, byte lane, int count, float innerSpace)
		{
			Coin[] coins = new Coin[count * lane.Ones()];
			int j = 0;
			for (byte i = 0; i < 3; i++)
			{
				if (lane.HasBit(i))
				{
					float x = startX;
					for (; j < count; j++)
					{
						coins[j] = new Coin(x, (byte)(i + 1));
						x += innerSpace;
					}
				}
			}
			return coins;
		}
		/// <summary>
		/// 1,2,4 bitwise lane number e.g. 7=1&2&3</para>
		/// </summary>
		public override byte Lane { get { return _lane; } }
		byte _lane;
		/// <summary>
		/// Type of game object
		/// </summary>
		public override PoolObjectType Type { get { return PoolObjectType.Coin; } }
	}
}
