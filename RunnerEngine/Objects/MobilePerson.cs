using System;
using RunnerEngine.Enums;

namespace RunnerEngine.Objects
{
	/// <summary>
	/// Endless Runner Static Game Object
	/// </summary>
	public class MobilePerson : BaseMobileObject
	{
		public MobilePerson(float x)
		{
			_x = x;
			_variation = EndlessLevelGenerator.random.Next(0, 5);//???
			_movement = ErgoMovement.WalkLeft;
			_speedMagnitude = 1;
		}

		/// <summary>
		/// Lane number (0: ground; 1,2,3: air; etc.)
		/// </summary>
		public override byte Lane { get { return 0; } }
		/// <summary>
		/// Type of game object
		/// </summary>
		public override PoolObjectType Type { get { return PoolObjectType.Person; } }

		public override int Variation { get { return _variation; } }
		int _variation;

		/// <summary>
		/// Create an identical Person facing right
		/// </summary>
		public MobilePerson Right
		{
			get
			{
				_movement = _movement | ErgoMovement.FaceRight;
				return this;
			}
		}
	}
}
