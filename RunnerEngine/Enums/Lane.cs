namespace RunnerEngine.Enums
{
	[System.Flags]
	public enum GameplayDraft
	{
		Empty = 0,
		Tree = 0x100,
		Eagle = 0x200,
		Cat = 0x400,
		House = 0x800,

		SafeLane1 = 0x1,
		SafeLane2 = 0x2,
		SafeLane3 = 0x4,
		SafeLanes = 0x7,

		DangerLane1 = 0x10,
		DangerLane2 = 0x20,
		DangerLane3 = 0x40,
		DangerLanes = 0x70,
	}
	[System.Flags]
	public enum Lanes
	{
		None = 0,
		Lane1 = 0x1,
		Lane2 = 0x2,
		Lane3 = 0x4,
		All = 0x7,
	}

	internal static class LaneConverter
	{
		internal static byte[][] lanesArray = new byte[][]
		{
			new[] { (byte)0 },
			new[] { (byte)1 },
			new[] { (byte)2 },
			new[] { (byte)1, (byte)2 },
			new[] { (byte)3 },
			new[] { (byte)1, (byte)3 },
			new[] { (byte)2, (byte)3 },
			new[] { (byte)1, (byte)2, (byte)3 }
		};
		static byte last = 0;
		internal static byte GetRandomLane(this Lanes lanes)
		{
			byte l = (byte)lanes;
			l = lanesArray[l][EndlessLevelGenerator.random.Next(0, lanesArray[l].Length)];
			if (l == last)
				l = lanesArray[l][EndlessLevelGenerator.random.Next(0, lanesArray[l].Length)];
			if (l == last)
				return 0;
			last = l;
			return l;
		}
		internal static int SafeToIndex(this GameplayDraft draft)
		{
			return (int)(draft & GameplayDraft.SafeLanes);
		}
		internal static int DangerToIndex(this GameplayDraft draft)
		{
			return (int)(draft & GameplayDraft.DangerLanes) >> 4;
		}
		//internal static Lanes ConvertToCatLanes(this GameplayDraft draft)
		//{
		//	return (Lanes)((int)draft & (int)Lanes.All);
		//}
		//internal static bool Match(this Lanes cats, GameplayDraft draft)
		//{
		//	Lanes catDraft = draft.ConvertToCatLanes();
		//	return
		//		((cats & catDraft) == catDraft);
		//}
	}
}
