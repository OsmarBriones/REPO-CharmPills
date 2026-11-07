# CharmPills Architecture

A Charm mod (layer 3 of the PocketCharms family): one valuable, one power. It depends on **Pockets** (storage) and **CharmCore** (registry, wear, cooldown display, host settings, debug mode).

## The power: Numbness

incoming damage to the owner is multiplied by `DamageMultiplier` (default 0.5) while the Pills are in the pocket.

- `PillsCharm : Charm` registers the prefab name `Valuable Arctic Pills`, the defaults (wear 12% per softened hit, no cooldown) and the host-synced settings `DamageMultiplier` and `MinimumHit`.
- `Numbness` is a plain class. `Numbness.Register(charm)` (called once in the plugin `Awake`, after `CharmsApi.Register`) adds one handler with `CharmsApi.OnPlayerHurting` at priority 20 (the "damage reduction" slot, after cancelling effects at 0 and last-chance saves at 10).
- `Domain/DamageReduction.cs` holds the game-free maths (`Reduce`, `IsWorthReducing`), unit tested in `DamageReductionTests`.

### Flow of a hit
All damage to a player goes through `PlayerHealth.Hurt`. CharmCore patches it and only calls the handlers for hits the game would really apply (not invincible, not dead, own machine, level running). The handler then:
1. Returns if the charm is not active (the Pills are not in the local pocket, or Enabled is off) or the hit was already cancelled.
2. Returns if the hit is smaller than `MinimumHit`.
3. Computes `Reduce(damage, DamageMultiplier)`; if that is not lower than the damage (for example a 1-point hit), it returns without wear.
4. Otherwise calls `Charm.TryUse()` (no cooldown, so it applies the wear) and sets `context.Damage` to the reduced value.

### Settings
| Key | Type | Range | Default | Meaning |
|---|---|---|---|---|
| `DamageMultiplier` | float | 0.1 to 1 | 0.5 | Share of the hit that still gets through. 0 is excluded so the charm never becomes full immunity; 1 means no effect. |
| `MinimumHit` | float (whole points) | 1 to 100 | 1 | Hits below it are ignored and cost no wear. |
| `WearPercent` (CharmCore) | float | 0 to 100 | 12 | Wear per softened hit. |

## Game members used

- `PlayerHealth.Hurt(int damage, bool savingGrace, int enemyIndex, bool hurtByHeal)`: not patched here; CharmCore's hook relays on it (checked in the contract test).
- `ValuableObject.dollarValueCurrent`: the value that wear reduces (done by CharmCore).

The mod needs no Harmony patch of its own.

## Debug mode

Provided by CharmCore: the `Debug.DebugMode` setting (off by default) places the valuable on the floor at the start of each level and enables `Charm.LogDebug` output.

## Known limits

- Looked up by exact prefab name; a game update that renames the prefab needs a mod update.
- Only damage that goes through `PlayerHealth.Hurt` is softened. Damage that bypasses it (for example death outside the method) is not.
- Not verified in game: the build and unit tests prove the logic and that the game members exist, not the behaviour in a running game.

## Decisions

- **Rounding:** nearest whole point, halves up, then at least 1 when the hit was at least 1, never above the original damage. A 1-point hit therefore stays 1.
- **Wear only when reduced:** a hit that the maths leaves unchanged (1-point hits, multiplier 1) costs nothing.
- **Wear per hit, no cooldown:** `TryUse()` is used for the wear; with no cooldown it always succeeds while active. Handlers run once per hit, so one wear per hit.
- **Order:** priority 20, so a coin flip (0) or a last-chance save (10) from another charm decides first, and the halved damage is what is left. A cancelled hit is not softened and not worn.
- **Minimum multiplier 0.1:** full immunity is not offered.
- **`MinimumHit`:** extra tunable so tiny hits, if the host wants, do not eat the valuable.
- **Local only:** the hook runs on the damaged player's own machine, so no network message is needed.
