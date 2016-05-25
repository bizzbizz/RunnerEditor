namespace RunnerEngine.Enums
{

	internal static class DraftBuilder
	{
		static GameplayDraft[,] transition = new[,]
		{
			//from empty
			{
				GameplayDraft.SafeLane1 | GameplayDraft.SafeLane2 | GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane2 | GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane1 | GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane1 | GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane1,
				GameplayDraft.Empty,
			},
			//from safe lane 1
			{
				GameplayDraft.SafeLane1 | GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane1,
				GameplayDraft.Empty,
				GameplayDraft.SafeLane1 | GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane1,
				GameplayDraft.Empty,
			},
			//from safe lane 2
			{
				GameplayDraft.SafeLane1 | GameplayDraft.SafeLane2 | GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane1 | GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane1 | GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane1,
				GameplayDraft.Empty,
			},
			//from safe lane 1,2
			{
				GameplayDraft.SafeLane1 | GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane1,
				GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane1 | GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane1,
				GameplayDraft.Empty,
			},
			//from safe lane 3
			{
				GameplayDraft.SafeLane2 | GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane2 | GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane2,
				GameplayDraft.Empty,
				GameplayDraft.Empty,
			},
			//from safe lane 1,3
			{
				GameplayDraft.SafeLane1 | GameplayDraft.SafeLane2 | GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane1 | GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane1 | GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane1,
				GameplayDraft.Empty,
			},
			//from safe lane 2,3
			{
				GameplayDraft.SafeLane2 | GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane2 | GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane1,
				GameplayDraft.Empty,
			},
			//from safe lane 1,2,3
			{
				GameplayDraft.SafeLane1 | GameplayDraft.SafeLane2 | GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane2 | GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane1 | GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane3,
				GameplayDraft.SafeLane1 | GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane2,
				GameplayDraft.SafeLane1,
				GameplayDraft.Empty,
			},
		};
		static GameplayDraft[] safelanes = new[] { GameplayDraft.SafeLane1, GameplayDraft.SafeLane2, GameplayDraft.SafeLane3 };
		static GameplayDraft[] dangerlanes = new[] { GameplayDraft.DangerLane1, GameplayDraft.DangerLane2, GameplayDraft.DangerLane3 };
		static GameplayDraft[] dangerlanescombo = new[]
		{
			GameplayDraft.Empty,
			GameplayDraft.DangerLane1,
			GameplayDraft.DangerLane2,
			GameplayDraft.DangerLane3,
			GameplayDraft.Empty,
			GameplayDraft.SafeLane1 | GameplayDraft.SafeLane2,
			GameplayDraft.SafeLane1 | GameplayDraft.SafeLane3,
			GameplayDraft.SafeLane2 | GameplayDraft.SafeLane3,
			GameplayDraft.Empty,
			GameplayDraft.DangerLane1,
			GameplayDraft.DangerLane2,
			GameplayDraft.DangerLane3,
			GameplayDraft.Empty,
		};
		static GameplayDraft[] dangerlanescombotree = new[]
		{
			GameplayDraft.DangerLane1,
			GameplayDraft.SafeLane1 | GameplayDraft.SafeLane2,
			GameplayDraft.SafeLane1 | GameplayDraft.SafeLane3,
		};


		internal static GameplayDraft RandomDanger(bool tree)
		{
			if (tree)
				return dangerlanescombotree[EndlessLevelGenerator.random.Next(0, dangerlanescombotree.Length)];
			else
				return dangerlanescombo[EndlessLevelGenerator.random.Next(0, dangerlanescombo.Length)];
		}
		internal static GameplayDraft StartSafeLane()
		{
			return GameplayDraft.SafeLanes;
		}
		internal static GameplayDraft ContinueSafeLane(GameplayDraft before, GameplayDraft current)
		{
			return transition[before.SafeToIndex(), current.DangerToIndex()];
		}
		internal static GameplayDraft ParallelDangerLane(GameplayDraft current)
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
