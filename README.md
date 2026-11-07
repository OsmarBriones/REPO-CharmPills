# Charm Pills

Numbness: while the Pills are in your pocket, every hit you take hurts only half as much.

> **Everyone in the lobby must install this mod, together with Pockets and CharmCore.** The power changes what your own character can do and its state shows on your inventory slot, so friends without the mods would see an empty slot and be unable to use the pocket.

## How to Use

1. Find the **Pills** valuable and store it in a free inventory slot with [Pockets](https://thunderstore.io/c/repo/p/OsmarBriones/Pockets/) (grab it, press the slot key).
2. Nothing else to do. Every time something hurts you, the damage is cut in half (never below 1 point), and the Pills lose a little of their value.
3. Take the valuable out of the pocket and the power stops.

## Features

- **Numbness:** you take half the damage from monsters, falls and thrown objects alike. A hit always leaves at least 1 point of damage, and a hit that does not land (you are invincible for a moment) costs nothing.
- **It costs the valuable:** each hit that is softened wears away a share of the Pills' value, and a worn-out valuable breaks. A 1-point hit cannot be softened, so it costs nothing.
- **Everything is configurable:** wear, how much damage gets through and the smallest hit that counts. The host's choices apply to everyone.

## Requirements
- [BepInEx Pack for R.E.P.O.](https://thunderstore.io/c/repo/p/BepInEx/BepInExPack/)
- [Pockets](https://thunderstore.io/c/repo/p/OsmarBriones/Pockets/)
- [CharmCore](https://thunderstore.io/c/repo/p/OsmarBriones/CharmCore/)

## Installation
1. Install through r2modman / Thunderstore Mod Manager (Pockets and CharmCore come with it), or place `CharmPills.dll` next to `Pockets.dll` and `CharmCore.dll` in `BepInEx/plugins`. **All players need it.**
2. Launch the game once; the configuration file is created inside `BepInEx/config`.

## Configuration
Settings are in `com.osmar.CharmPills.cfg` inside `BepInEx/config`, section `Charm.Pills`. The host's choices apply to everyone.

- `Enabled` (default `true`): turn this charm on or off.
- `WearPercent` (default `12`, 0 to 100): share of the valuable's original value lost on each softened hit.
- `DamageMultiplier` (default `0.5`, 0.1 to 1): share of each hit that still gets through. `1` means no protection.
- `MinimumHit` (default `1`, 1 to 100): smallest hit, in health points, that the Pills soften. Smaller hits pass through and cost no wear.

Section `Debug`: `DebugMode` (default `false`) places a Pills on the floor in front of you when a level starts (host or singleplayer) and writes extra log lines. For testing.

## Issues & Bug Reports
The official way to report issues, suggest improvements, or submit feedback is by opening an issue on the official GitHub repository:
👉 [GitHub Issues](https://github.com/OsmarBriones/REPO-CharmPills/issues)

## Credits
Developed by **Osmar Briones**
