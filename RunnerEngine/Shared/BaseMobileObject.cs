using RunnerEngine.Enums;

namespace RunnerEngine
{
	public abstract class BaseMobileObject : BaseObject
	{
		#region Moving ERGO Functionality
		protected float _speedMagnitude;
		protected ErgoMovement _movement;
		public virtual float Vx { get { return IsWalking() ? (IsFacingLeft() ? -_speedMagnitude : _speedMagnitude) : 0; } }
		public virtual bool IsWalking() { return (_movement & ErgoMovement.Walk) == ErgoMovement.Walk; }
		public virtual bool IsFacingLeft() { return (_movement & ErgoMovement.FaceLeft) == ErgoMovement.FaceLeft; }

		/// <summary>
		/// Calibrate StartX according to bird's speed and location
		/// </summary>
		/// <param name="x">bird distance to start of this sector</param>
		/// <param name="v">bird current speed</param>
		public virtual void Calibrate(float x, float v)
		{
			_x -= (Vx / v) * (x + X);
		}
		#endregion
	}
}
