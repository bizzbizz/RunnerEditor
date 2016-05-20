using System;
using RunnerEngine.Enums;

namespace RunnerEngine.Objects
{
	/// <summary>
	/// Endless Runner Static Game Object
	/// </summary>
	public class FixedPerson : BaseObject
	{
		public FixedPerson(float x)
		{
			_x = x;
		}

		/// <summary>
		/// Lane number (0: ground; 1,2,3: air; etc.)
		/// </summary>
		public override byte Lane { get { return 0; } }

		/// <summary>
		/// Type of game object
		/// </summary>
		public override ErgoType Type { get { return ErgoType.PersonStand; } }

	}
}
