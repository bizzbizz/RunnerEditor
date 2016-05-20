using System;
using RunnerEngine.Enums;

namespace RunnerEngine.Objects
{
	/// <summary>
	/// Endless Runner Static Game Object
	/// </summary>
	public class Eagle : BaseMobileObject
	{
		public Eagle(float x, byte lane)
		{
			_x = x;
			_lane = lane;
			_variation = 0;
		}

		/// <summary>
		/// Lane number (0: ground; 1,2,3: air; etc.)
		/// </summary>
		public override byte Lane { get { return _lane; } }
		byte _lane;
		/// <summary>
		/// Type of game object
		/// </summary>
		public override ErgoType Type { get { return ErgoType.Eagle; } }

		public override int Variation { get { return _variation; } }
		int _variation;

		/// <summary>
		/// Create an identical Person facing right
		/// </summary>
		public Eagle Right
		{
			get
			{
				_movement = _movement | ErgoMovement.FaceRight;
				return this;
			}
		}
	}
}
