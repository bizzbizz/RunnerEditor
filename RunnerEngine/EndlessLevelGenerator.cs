﻿using System;
namespace RunnerEngine
{
	public static class EndlessLevelGenerator
	{
		//private members
		static District[] sceneries;
		static Sector currentSector = null;
		static int levelCounter = 0;
		static int blockCounter = 0;

		internal static Random random = new Random(DateTime.Now.Millisecond);
		public static int GetRandom() { return random.Next(); }

		public static int NextLevelStartDistance { get { return levelCounter * 250; } }


		public static void RestartGame()
		{
			levelCounter = 0;
			blockCounter = 0;
			currentSector = null;
		}

		public static void ConfigureDistricts(District[] districts)
		{
			sceneries = districts;
		}

		/// <summary>
		/// Advance to next sector and return whole sector hierarchy
		/// <para>advance level as well if needed</para>
		/// </summary>
		/// <returns></returns>
		public static Sector GetNextSector(float meters)
		{
			//bool levelEnded = (meters >= NextLevelStartDistance);
			//if (levelEnded)
			//{
			//	//go to next level
			//	levelCounter++;
			//}
			blockCounter++;

			//build next (|first) block
			if (currentSector == null)
			{
				//create first sector (Empty)
				currentSector = new Sector();
			}
			else
			{
				//go to next sector
				int index = blockCounter % sceneries.Length;
				//load and build sector
				currentSector = new Sector(sceneries[index], blockCounter);
				//???
				//??? next
			}

			return currentSector;
		}
	}
}
