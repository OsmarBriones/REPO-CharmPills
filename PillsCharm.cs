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
			// Extra host-synced numbers for this power go here, for example:
			// Amount = AddSetting("Amount", 30f, 1f, 100f, "Description for the config file.");
		}
	}
}
