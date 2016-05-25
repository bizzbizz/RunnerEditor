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
		public static IEnumerable<BaseObject> CreateCollection(float startX, Lanes lanes, int count, float innerSpace)
		{
			List<BaseObject> coins = new List<BaseObject>();
			if (count < 3) return coins;
			float x = startX;

			//byte l = lanes.GetRandomLane();
			//for (int i = 0; i < count; i++)
			//{
			//	coins[i] = new Coin(x, l);
			//	x += innerSpace;
			//}
			//return coins;

			int sprint = count;
			if (count >= 6)
			{
				if (count <= 9)
					sprint = EndlessLevelGenerator.random.Next(3, count);
				else
					sprint = EndlessLevelGenerator.random.Next(3, count - 3);
			}
			byte lane = lanes.GetRandomLane();
			for (int i = 0; i < sprint; i++)
			{
				if (lane > 0)
					coins.Add(new Coin(x, lane));
				x += innerSpace;
			}
			lane = lanes.GetRandomLane();
			for (int i = sprint; i < count; i++)
			{
				if (lane > 0)
					coins.Add(new Coin(x, lane));
				x += innerSpace;
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
