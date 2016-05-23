using System;
using RunnerEngine.Enums;

namespace RunnerEngine.Objects
{
	/// <summary>
	/// Endless Runner Static Game Object
	/// </summary>
	public class Tree : BaseObject
	{
		public Tree(float x)
		{
			_x = x;
			_variation = 0;
			_largeTree = false;
		}

		/// <summary>
		/// Lane number (0: ground; 1,2,3: air; etc.)
		/// </summary>
		public override byte Lane { get { return 0; } }
		/// <summary>
		/// Type of game object
		/// </summary>
		public override PoolObjectType Type { get { return PoolObjectType.Tree; } }

		public override int Variation { get { return _variation; } }
		int _variation;

		public bool LargeTree { get { return _largeTree; } }
		bool _largeTree;
	}
}
