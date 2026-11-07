using CharmCore.Api;

namespace CharmPills
{
	/// <summary>Numbness: incoming damage is cut in half.</summary>
	internal sealed class PillsCharm : Charm
	{
		internal const string PrefabName = "Valuable Arctic Pills";

		internal PillsCharm()
			: base("Pills", "Pills - Numbness", defaultWearPercent: 12f, defaultCooldownSeconds: null, PrefabName)
		{
			DamageMultiplier = AddSetting("DamageMultiplier", 0.5f, 0.1f, 1f, "Share of each hit that still gets through while the Pills are in your pocket (0.5 = half the damage, 1 = no protection, 0.1 = a tenth).");
			MinimumHit = AddSetting("MinimumHit", 1f, 1f, 100f, "Smallest hit, in health points, that the Pills soften. Smaller hits pass through untouched and cost no wear.");
		}

		/// <summary>Share of the damage that still gets through (0.1 to 1).</summary>
		internal CharmSetting DamageMultiplier { get; }

		/// <summary>Hits smaller than this are ignored by the charm.</summary>
		internal CharmSetting MinimumHit { get; }
	}
}
