using RunnerEngine.Enums;
using RunnerEngine.Objects;

namespace RunnerEngine
{
	/// <summary>
	/// draft of a street block and its specific hotSpots such as stationary people, barriers, etc.
	/// </summary>
	public class District
	{
		public int Background;
		public House[] Houses;
		internal House FindSuitableHouse(GameplayDraft draft)
		{
			int startRandom = EndlessLevelGenerator.random.Next(0, Houses.Length);
			for (int i = 0; i < Houses.Length; i++)
			{
				var house = Houses[(i + startRandom) % Houses.Length];
				if (house.Cats.Match(draft))
					return house;
			}
			return null;
		}
	}
}
