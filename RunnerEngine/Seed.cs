namespace RunnerEngine
{
	/// <summary>
	/// Singleton class
	/// </summary>
	public struct Seed
	{
		public byte difficulty;//level number
		public byte coins;//number of coins
		public byte ncc;//availability of non-coin collectibles (array of bools)
		public byte mobiles;//availability of mobile objects (array of bools)
		public int random;

		static Seed currentSeed;

		public static Seed FirstSeed()
		{
			currentSeed = new Seed
			{
				difficulty = 1,
				coins = 10,
				ncc = 0,
				mobiles = 0,
				random = EndlessLevelGenerator.random.Next(),
			};
			return currentSeed;
		}
		public static Seed NextSeed()
		{
			currentSeed = new Seed
			{
				difficulty = 1,
				coins = (byte)EndlessLevelGenerator.random.Next(5, 15),
				ncc = (byte)EndlessLevelGenerator.random.Next(0,4),
				mobiles = (byte)((currentSeed.mobiles + 1)%255),
				random = EndlessLevelGenerator.random.Next(),
			};
			return currentSeed;
		}

	}
}
