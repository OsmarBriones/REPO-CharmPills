using CharmCore.Api;
using CharmPills.Domain;

namespace CharmPills
{
	/// <summary>
	/// Numbness: while the Pills are in the pocket, damage to the owner is multiplied by <c>DamageMultiplier</c>.
	/// CharmCore owns the hook on <c>PlayerHealth.Hurt</c> and only offers hits the game would really apply, so this
	/// class just registers one handler (damage-reduction slot, priority 20) and wears the valuable when it softened a hit.
	/// </summary>
	internal sealed class Numbness
	{
		/// <summary>The "damage reduction" priority suggested by <see cref="CharmsApi.OnPlayerHurting"/>.</summary>
		internal const int Priority = 20;

		private readonly PillsCharm charm;

		private Numbness(PillsCharm charm)
		{
			this.charm = charm;
		}

		/// <summary>Registers the hurt handler once; call it after <see cref="CharmsApi.Register"/>.</summary>
		internal static void Register(PillsCharm charm)
		{
			Numbness numbness = new Numbness(charm);
			CharmsApi.OnPlayerHurting(numbness.OnHurting, Priority);
		}

		private void OnHurting(PlayerHurtContext context)
		{
			if (!charm.IsActive || context.Cancel)
			{
				return;
			}

			int before = context.Damage;
			if (!DamageReduction.IsWorthReducing(before, charm.MinimumHit.Value))
			{
				return;
			}

			int after = DamageReduction.Reduce(before, charm.DamageMultiplier.Value);
			if (after >= before)
			{
				if (charm.DebugMode)
				{
					charm.LogDebug($"Numbness: hit of {before} left as it was");
				}
				return;
			}

			// No cooldown, so TryUse always succeeds while the charm is active; it also applies the wear.
			if (charm.TryUse())
			{
				context.Damage = after;
				if (charm.DebugMode)
				{
					charm.LogDebug($"Numbness: hit of {before} reduced to {after} (x{charm.DamageMultiplier.Value:0.##})");
				}
			}
		}
	}
}
