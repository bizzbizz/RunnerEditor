namespace RunnerEngine.Enums
{
	/// <summary>
	/// Endless Runner Game Object Movement
	/// </summary>
	[System.Flags]
	public enum ErgoMovement
	{
		StayStill = 0,
		FaceLeft = 1,
		FaceRight = 2,
		Walk = 4,
		WalkLeft = 5,
		WalkRight = 6,
	}
}
