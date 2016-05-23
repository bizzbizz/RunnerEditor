namespace RunnerEngine.Enums
{
	[System.Flags]
	public enum GameplayDraft
	{
		Empty = 0,
		Tree = 0x10,
		Cat = 0x20,
		Eagle = 0x40,

		House = 0x80,

		SafeLane1 = 0x100,
		SafeLane2 = 0x200,
		SafeLane3 = 0x400,

		DangerLane1 = 0x1000,
		DangerLane2 = 0x2000,
		DangerLane3 = 0x4000,
	}
	internal static class DraftBuilder
	{
		static GameplayDraft[] safelanes = new[] { GameplayDraft.SafeLane1, GameplayDraft.SafeLane2, GameplayDraft.SafeLane3 };
		static GameplayDraft[] dangerlanes = new[] { GameplayDraft.DangerLane1, GameplayDraft.DangerLane2, GameplayDraft.DangerLane3 };
		internal static GameplayDraft RandomSafeLane()
		{
			return safelanes[EndlessLevelGenerator.random.Next(0, 3)];
		}
		internal static GameplayDraft ContinueSafeLane(GameplayDraft before)
		{
			if (before.Has(GameplayDraft.SafeLane1))
				return safelanes[EndlessLevelGenerator.random.Next(0, 2)];
			else if (before.Has(GameplayDraft.SafeLane3))
				return safelanes[EndlessLevelGenerator.random.Next(1, 3)];
			else
				return safelanes[EndlessLevelGenerator.random.Next(0, 3)];
		}
		internal static GameplayDraft BlockSafeLane(GameplayDraft current)
		{
			if (current.Has(GameplayDraft.Tree))
			{
				if (current.Has(GameplayDraft.SafeLane2))
					return GameplayDraft.DangerLane1;
				else
					return dangerlanes[EndlessLevelGenerator.random.Next(0, 2)];
			}
			else if (current.Has(GameplayDraft.Eagle) || current.Has(GameplayDraft.House))
			{
				if (current.Has(GameplayDraft.SafeLane3))
					return dangerlanes[EndlessLevelGenerator.random.Next(0, 2)];
				else if (current.Has(GameplayDraft.SafeLane2))
					return dangerlanes[EndlessLevelGenerator.random.Next(0, 2) == 0 ? 0 : 2];
				else
					return dangerlanes[EndlessLevelGenerator.random.Next(1, 3)];
			}
			else
				return GameplayDraft.Empty;
		}
	}
}
