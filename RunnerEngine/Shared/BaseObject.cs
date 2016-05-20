using RunnerEngine.Enums;

namespace RunnerEngine
{
	public abstract class BaseObject
	{
		/// <summary>
		/// position x relative to parent sector
		/// </summary>
		public virtual float X { get { return _x; } }
		public virtual byte Lane { get { return 0; } }
		public virtual ErgoType Type { get { return ErgoType.None; } }
		public virtual int Variation { get { return 0; } }
		public virtual bool LargeTree { get { return false; } }
		public virtual int CoinCount { get { return 0; } }
		public virtual float InnerSpace { get { return 0; } }
		public virtual float Vx { get { return 0f; } }
		public virtual bool IsWalking() { return false; }
		public virtual bool IsFacingLeft() { return true; }
		public virtual void Calibrate(float x, float v) { }

		protected float _x;

		public override string ToString()
		{
			return string.Format("{0},\t x,y={1},{2}, Variation={3}", Type, X, Lane, Variation);
		}
	}
}
