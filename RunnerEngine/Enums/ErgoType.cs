namespace RunnerEngine.Enums
{
	/// <summary>
	/// Endless Runner Game Object Type
	/// </summary>
	public enum ErgoType : ushort
	{
		None = 0,

		Good = 0x10,
		Bad = 0x20,
		Neutral = 0x40,

		Spot =			0x100,
		Mobile =		0x200,
		Collectible =	0x400,
		Static =		0x800,
		//instances

		PersonStand =	0x141,
		House =			0x942,
		Cat =			0x122,
		Tree =			0x923,
		LargeTree =		0x924,
		Indoor =		0x915,
		Fan =			0x916,

		PersonWalk =	0x241,
		Eagle =			0x222,

		Coin =		0xC11,
		Gem =		0xC12,
		Magnet =	0xC13,
		Shield =	0xC14,
	}

}
