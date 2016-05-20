using System;
using RunnerEngine.Enums;

namespace RunnerEngine.Objects
{
	/// <summary>
	/// Endless Runner Static Game Object
	/// </summary>
	public class Cat : BaseObject
	{
		public Cat(float x, byte lane)
		{
			_x = x;
			_lane = lane;
		}

		/// <summary>
		/// Lane number (0: ground; 1,2,3: air; etc.)
		/// </summary>
		public override byte Lane { get { return _lane; } }
		byte _lane;

		/// <summary>
		/// Type of game object
		/// </summary>
		public override ErgoType Type { get { return ErgoType.Cat; } }
	}
}
