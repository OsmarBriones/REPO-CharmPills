# QA battery: Numbness (CharmPills)

Deploy: Pockets, CharmCore and CharmPills are in the r2modman `Debug` profile (compare hashes with the builds). Restart the game first.

## Quick start with debug mode
Set `DebugMode = true` in the `Debug` section of `BepInEx/config/com.osmar.CharmPills.cfg` (off by default). When a level starts, a Pills is placed on the floor about 2.5 m in front of you; the log then also shows the detailed lines. Any level works.

## Single player
- [ ] Log shows `Charm registered: Pills - Numbness` and `CharmPills 1.0.0 loaded!`.
- [ ] Store the valuable in a slot: the slot shows its picture and value.
- [ ] Without the Pills in a pocket (carried in hand or on the floor), let a monster or a fall hurt you: normal damage, no wear.
- [ ] With the Pills in a slot, take a hit that normally does about 10: you lose about 5. The Pills' value drops by about 12% of its original value (log: `Numbness: hit of 10 reduced to 5`).
- [ ] Take a 1-point hit if you can: damage stays 1, no wear.
- [ ] Hits during the invincibility moment after a hit: no damage and no wear.
- [ ] No cooldown veil appears on the slot.
- [ ] Take the valuable out: the power stops.
- [ ] Use it until the valuable breaks: the slot empties, the power stops, no errors in the log.

## Settings
- [ ] `WearPercent = 0`: halved damage, no wear.
- [ ] `DamageMultiplier = 1`: no protection and no wear. `0.1`: a 20 hit becomes 2.
- [ ] `MinimumHit = 6`: hits of 5 or less pass untouched and cost nothing; larger ones are halved.
- [ ] Charm `Enabled = false`: no effect and no wear. CharmCore `Enabled = false`: nothing works.

## Multiplayer (host and client)
- [ ] The client takes half damage and the Pills' value drops on both machines.
- [ ] A client without the Pills takes full damage while a friend with them is halved (each player's own pocket counts).
- [ ] Host settings override the client's.
- [ ] Client leaves and rejoins: no stuck veil.

Log patterns: `Charm registered`, `threw`, `Exception`.
