using RunnerEngine.Enums;

namespace RunnerEngine
{
	public static class Extensions
	{
		/// <summary>
		/// returns whether the enum source value contains given value
		/// </summary>
		/// <param name="src">enum source value</param>
		/// <param name="cmp">given value</param>
		/// <returns></returns>
		public static bool Has(this ErgoType src, ErgoType cmp)
		{
			return (src & cmp) == cmp;
		}
		/// <summary>
		/// returns whether the enum source value contains given value
		/// </summary>
		/// <param name="src">enum source value</param>
		/// <param name="cmp">given value</param>
		/// <returns></returns>
		public static bool Has(this CatLanes src, CatLanes cmp)
		{
			return (src & cmp) == cmp;
		}
		/// <summary>
		/// returns whether the enum source value contains given value
		/// </summary>
		/// <param name="src">enum source value</param>
		/// <param name="cmp">given value</param>
		/// <returns></returns>
		internal static bool Has(this GameplayDraft src, GameplayDraft cmp)
		{
			return (src & cmp) == cmp;
		}




		/// <summary>
		/// returns whether the number ends with 1
		/// </summary>
		/// <param name="number"></param>
		/// <returns></returns>
		public static bool HasOne(this int number)
		{
			return (number & 1) == 1;
		}
		/// <summary>
		/// converts the three least significant bits of number
		/// </summary>
		/// <param name="number"></param>
		/// <returns></returns>
		public static byte LSB3(this int number)
		{
			return (byte)(number & 7);
		}
		/// <summary>
		/// returns whether the number ends has given bit
		/// </summary>
		/// <param name="number"></param>
		/// <param name="bit">given bit index (i.e. 0,1,2,3)</param>
		/// <returns></returns>
		public static bool HasBit(this byte number, byte bit)
		{
			byte mask = (byte)(1 << bit);
			return (byte)(number & mask) == mask;
		}
		/// <summary>
		/// returns the number of ones in a binary number
		/// <para>5.Ones(x) = 2;</para>
		/// <para>x = 3;</para>
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		public static byte Ones(this byte src)
		{
			byte count = 0;
			for (byte i = 0; i < 8; i++)
			{
				if ((src & 1) == 1)
					count++;
				src >>= 1;
			}
			return count;
		}
		/// <summary>
		/// returns the number of ones in a binary number
		/// <para>5.Ones(x) = 2;</para>
		/// <para>x = 3;</para>
		/// </summary>
		/// <param name="src"></param>
		/// <param name="digits">returns the number of digits until all zero</param>
		/// <returns></returns>
		public static int Ones(this int src, out int digits)
		{
			int count = 0;
			digits = 1 + (int)System.Math.Log(src, 2);
			for (int i = 0; i < digits; i++)
			{
				if ((src & 1) == 1)
					count++;
				src >>= 1;
			}
			return count;
		}


		/// <summary>
		/// return Calibrated X according to bird's speed and location
		/// </summary>
		/// <param name="x">bird distance to start of this sector</param>
		/// <param name="v">bird current speed</param>
		public static float Calibrate(this float thisX, float thisV, float x, float v)
		{
			return thisX + (thisV / v) * (x + thisX);
		}
	}
}
