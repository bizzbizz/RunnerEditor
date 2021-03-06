﻿using System;
using System.Collections.Generic;
using RunnerEngine.Enums;

namespace RunnerEngine.Objects
{
	/// <summary>
	/// Endless Runner Static Game Object
	/// </summary>
	public class Tree : BaseObject
	{
		public Tree(float x, bool large, int variation)
		{
			_x = x;
			_variation = variation;
			_largeTree = large;
			//HasNest = EndlessLevelGenerator.random.NextDouble() > .5;
			//if(HasNest)
			//{
			//	nest = new Collectible[1];
			//	nest[0] = new Collectible(.2f, large);
			//}
		}
		//Collectible[] nest;
		//public override IEnumerable<BaseObject> Children
		//{
		//	get
		//	{
		//		return nest;
		//	}
		//}
		//public bool HasNest { get; internal set; }
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
