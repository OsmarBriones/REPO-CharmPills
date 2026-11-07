using System;

namespace CharmPills.Domain
{
	/// <summary>Game-free rules of Numbness: how a hit is softened. Kept free of Unity and game types so it is unit tested.</summary>
	internal static class DamageReduction
	{
		/// <summary>
		/// The damage that still gets through. <paramref name="multiplier"/> is the share that passes (0.5 = half), clamped to 0..1.
		/// The result is rounded to the nearest whole point (halves go up), and is at least 1 when the hit was at least 1,
		/// because 0 damage would nullify the hit. Hits of 0 or less are returned unchanged.
		/// </summary>
		internal static int Reduce(int damage, float multiplier)
		{
			if (damage <= 0)
			{
				return damage;
			}

			float share = multiplier < 0f ? 0f : multiplier > 1f ? 1f : multiplier;
			int reduced = (int)Math.Round(damage * share, MidpointRounding.AwayFromZero);
			if (reduced < 1)
			{
				reduced = 1;
			}

			return reduced > damage ? damage : reduced;
		}

		/// <summary>True when a hit of <paramref name="damage"/> is big enough for the charm to act on (and wear).</summary>
		internal static bool IsWorthReducing(int damage, float minimumHit)
		{
			return damage >= 1 && damage >= minimumHit;
		}
	}
}
