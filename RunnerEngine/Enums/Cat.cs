namespace RunnerEngine.Enums
{
	[System.Flags]
	public enum CatLanes
	{
		None = 0x0000,
		DangerLane1 = 0x1000,
		DangerLane2 = 0x2000,
		DangerLane3 = 0x4000,
		All = 0x7000,
	}
	internal static class LaneConverter
	{
		internal static CatLanes ConvertToCatLanes(GameplayDraft draft)
		{
			return (CatLanes)((int)draft & (int)CatLanes.All);
		}
		internal static bool Match(this CatLanes cats, GameplayDraft draft)
		{
			CatLanes catDraft = ConvertToCatLanes(draft);
			return
				((cats & catDraft) == catDraft);
		}
	}
}
