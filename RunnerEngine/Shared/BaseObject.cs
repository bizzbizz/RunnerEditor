using RunnerEngine.Enums;
using System.Collections.Generic;

namespace RunnerEngine
{
	public abstract class BaseObject
	{
		protected float _x;
		/// <summary>
		/// position x relative to parent sector
		/// </summary>
		public virtual float X { get { return _x; } }
		public virtual byte Lane { get { return 0; } }
		public abstract PoolObjectType Type { get; }
		public virtual IEnumerable<BaseObject> Children { get { return null; } }

		public virtual int Variation { get { return 0; } }

		public override string ToString()
		{
			string str = string.Format("{0},\t x,y={1},{2}, Variation={3}", Type, X, Lane, Variation);
			if (Children != null)
			{
				str += " [";
				foreach (var item in Children)
				{
					if (item != null)
						str += item.ToString();
				}
				str += "] ";
			}
			return str;
		}
	}
}
