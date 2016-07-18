using System;
using RunnerEngine.Enums;

namespace RunnerEngine.Objects
{
	/// <summary>
	/// Endless Runner Static Game Object
	/// </summary>
	public class FixedPerson : BaseObject
	{
		public FixedPerson(float x, int variation)
		{
			_x = x;
			_variation = variation;
		}

		int _variation = 0;

		/// <summary>
		/// Lane number (0: ground; 1,2,3: air; etc.)
		/// </summary>
		public override byte Lane { get { return 0; } }

		/// <summary>
		/// not moded (0..infinity)
		/// </summary>
		public override int Variation { get { return _variation; } }

		/// <summary>
		/// Type of game object
		/// </summary>
		public override PoolObjectType Type { get { return PoolObjectType.Person; } }

	}
}
