# Cryptid — Character Handoff

## Status
**Builds clean. 0 errors, 9 pre-existing nullability warnings (non-blocking).**
```
cd Z:\Projects\slay_spire2\created_characters\Cryptid
dotnet build
```

---

## Character Overview

| Field | Value |
|-------|-------|
| Class | `CryptidCharacter` (`PlaceholderCharacterModel`) |
| HP | 70 |
| Color | `#22BB66` (eerie green) |
| Gender | Neutral |
| Orb slots | 3 |
| Starting relic | TinfoilSkullRelic |

**Starting Deck (10 cards):**
- Probe × 5
- Static Blast × 2
- Strange Sighting × 1
- Hide Evidence × 1
- Abduction Beam × 1

**Starting Relic — Tinfoil Skull:**
Whenever you take damage from an enemy, gain 5 Paranormal. Enables immediate Paranormal generation in turn 1.

---

## Core Mechanics

### Paranormal (primary resource)
`ParanormalPower` — Counter stack buff on player.

At end of turn:
1. Gain Block equal to current Amount (1:1 conversion)
2. Reduce Amount by half (rounded down): `Amount = Amount / 2`

Example: 8 Paranormal → gain 8 Block, left with 4.

Many cards and powers interact with the *amount* of Paranormal: spending it, scaling off it, gaining resources from it.

### Abducted (enemy debuff)
`AbductedPower` — Single stack, no inherent behavior. Marker power.
- Applied by Abduction Beam, Tractor Beam, and Gray Alien orb evokes
- Multiple cards check for Abducted to trigger bonus effects (Cattle Mutilation, Flashlight Sweep, etc.)
- Mothership power deals 5 to all Abducted enemies each turn

### Madness (enemy debuff)
`MadnessPower` — Counter stack, decrements at end of player's turn.
- Deals 25% less damage (Weak)
- Gains 25% less Block (Frail)
- Applied by Eldritch orb passive, Missing Time, Eldritch Ritual
- Eldritch orb evoke *consumes* all Madness from all enemies and draws cards per stack consumed

### Entity Orbs
Three orb types. Channel with `OrbCmd.Channel<T>(ctx, player)`. Evoke via orb slot mechanics or Dimensional Shift / Cosmic Prayer.

| Orb | Passive (each turn it sits) | Evoke |
|-----|----------------------------|-------|
| Gray Alien | Deal 3 dmg to random enemy | Apply Abducted to ALL enemies. Deal dmg to all = count of Gray Alien orbs channeled |
| Eldritch | Apply 1 Madness to random enemy | Consume ALL Madness from ALL enemies. Draw 1 card per stack removed |
| Cryptid | Gain 1 Paranormal | Gain 1 Energy |

**AncientTabletPower:** Next Evoke triggers twice — channels a duplicate orb of the same type and immediately evokes it.

---

## Card Roster

### Basic (Starting Deck)

| Card | Cost | Effect | Upgrade |
|------|------|--------|---------|
| Probe | 0 | Gain 1 Paranormal. Channel Cryptid Entity | +1 Paranormal |
| Static Blast | 1 | Deal 6 dmg. Abducted: +3 dmg | +3 dmg |
| Strange Sighting | 1 | Draw 1 card. Gain 1 Paranormal | Draw 2 cards |
| Hide Evidence | 1 | Gain 6 Block. Exhaust a card from hand | +3 Block |
| Abduction Beam | 1 | Apply Abducted. Channel Gray Alien | +1 Weak |

### Common Attacks

| Card | Cost | Effect | Upgrade |
|------|------|--------|---------|
| Flashlight Sweep | 1 | Deal 7 dmg. Abducted: gain 1 Paranormal | +3 dmg |
| Cattle Mutilation | 2 | Deal 10 dmg. Abducted: deal that much again | +4 dmg |

### Common Skills

| Card | Cost | Effect | Upgrade |
|------|------|--------|---------|
| Men in Black | 1 | Remove all debuffs from self. Gain 1 Paranormal per removed | Also draw 1 |
| Blackout | 1 | Apply 2 Weak to ALL. Gain 1 Paranormal | +1 Weak, +1 Paranormal |
| Missing Time | 1 | Apply 2 Madness to enemy. Draw 1 | +2 Madness |
| Crop Circle Pattern | 1 | Channel 1 random Entity. Draw 1 | Channel 2 |
| Signal Booster | 1 | Channel 2 Cryptid Entities | Also +1 orb slot |
| Scrambled Signal | 0 | Exhaust a card from hand. Gain 2 Paranormal | Also draw 1 |
| Cosmic Download | 1 | Gain 3 Paranormal. If Evoked this turn: draw 1 | Draw 2 on Evoke |
| Bad Feeling | 0 | Gain 10 Paranormal. Exhaust | Gain 15 Paranormal |

### Common Powers

| Card | Cost | Effect | Upgrade |
|------|------|--------|---------|
| Radio Static | 1 | On Paranormal gain from card: deal 2 dmg to random enemy | +1 dmg |
| Black-Eyed Children | 1 | On Evoke: gain 1 Paranormal | Gain 2 Paranormal |

### Uncommon Skills

| Card | Cost | Effect | Upgrade |
|------|------|--------|---------|
| Hide in Plain Sight | 2 | Gain 8 Block. Gain 1 Paranormal per Skill this turn (incl. this) | +4 Block |
| Tractor Beam | 2 | Apply Abducted to ALL. Channel 1 Gray Alien | Channel 2 Gray Alien |
| Eldritch Ritual | 1 | Apply 2 Madness to ALL enemies | +2 Madness, draw 1 |
| Dimensional Shift | 0 | Evoke your next Entity | Also draw 1 |
| Cosmic Prayer | 2 | Evoke ALL channeled Entities. +1 Paranormal each | Also draw 1 |

### Uncommon Powers

| Card | Cost | Effect | Upgrade |
|------|------|--------|---------|
| Observation Drone | 2 | End of turn: Channel 1 random Entity | Also gain 1 Paranormal/turn |
| Conspiracy Board | 1 | Every 3 Skills/turn: Channel 1 random Entity | Also gain 1 Paranormal/trigger |
| Mothman | 2 | On enemy Madness gain: deal 3 dmg to ALL | +2 dmg (5 total) |
| Bigfoot | 2 | End of turn: deal Paranormal dmg to random enemy | Cost → 0 |
| Paranormal Reactor | 2 | Start of turn: if Paranormal ≥ 6, spend 3 → gain 1 Energy | Also draw 1 |

### Rare Attacks

| Card | Cost | Effect | Upgrade |
|------|------|--------|---------|
| Chupacabra | 2 | Spend ALL Paranormal. Deal 8+4/Paranormal. Heal 1 HP per 2 spent | +2 dmg/Paranormal |
| Alien Invasion | 2 | Spend ALL Paranormal. Deal 4 dmg to ALL per 2 Paranormal. Exhaust | +2 dmg |

### Rare Skills

| Card | Cost | Effect | Upgrade |
|------|------|--------|---------|
| Secret Meeting | 0 | If Gray Alien + Eldritch + Cryptid orbs channeled: gain 3 Energy, draw 3. Exhaust | Retain instead |
| Ancient Tablet | 1 | Next Evoke triggers twice (AncientTabletPower, consumed) | Cost → 0 |
| Loch Ness Monster | 3 | Spend ALL Paranormal. Intangible 1 (2 if 8+ spent). Exhaust | Threshold 5+ |

### Rare Powers

| Card | Cost | Effect | Upgrade |
|------|------|--------|---------|
| Glitch in the Sky | 2 | On Evoke: gain 1 Energy next turn start | Also draw 1 next turn |
| Hive Mind | 2 | All Paranormal gains +1 | +2 instead |
| Interdimensional Treaty | 3 | On Paranormal gain: also gain that much Block | +1 Block per trigger |
| The Mothership | 4 | End of turn: Channel 1 Entity. If any Abducted: deal 5 to ALL | Channel 2/turn |
| Eldritch Transformation | 2 | On Madness apply: gain 1 Temp Dexterity | Also +1 Temp Strength, cost → 0 |

### Curses

| Card | Cost | Effect |
|------|------|--------|
| Missing Memories | 0 | When drawn: gain 2 Weak |
| Paranoia | 0 | When drawn: gain 1 Vulnerable |
| Implanted Parasite | 0 | When drawn: lose 3 Paranormal |
| Whispers | -2 | When you Evoke an Entity: lose 1 HP |
| Night Terrors | -2 | End of your turn: lose 1 Paranormal |

---

## Powers Summary

| Power | Type | Stack | Trigger | Effect |
|-------|------|-------|---------|--------|
| ParanormalPower | Buff | Counter | BeforeTurnEnd | Gain Block = Amount, then Amount /= 2 |
| AbductedPower | Debuff | Single | — | Marker only |
| MadnessPower | Debuff | Counter | ModifyDamage/Block × 0.75; AfterTurnEnd: Decrement | Weak + Frail combined |
| RadioStaticPower | Buff | Counter | AfterCardPlayed (Paranormal gain check) | Deal Amount dmg to random enemy |
| ConspiracyBoardPower | Buff | Single | AfterCardPlayed (Skill) | Every 3 Skills: Channel random Entity (+Paranormal if Amount ≥ 2) |
| HiveMindPower | Buff | Single | ModifyPowerAmountGiven | All Paranormal gains +Amount |
| InterdimensionalTreatyPower | Buff | Single | ModifyPowerAmountGiven | On Paranormal gain: GainBlockInternal(amount + (Amount≥2?1:0)) |
| MothershipPower | Buff | Single | BeforeTurnEnd | Channel 1(2) random Entity. If Abducted enemy: deal 5 to ALL |
| ObservationDronePower | Buff | Single | BeforeTurnEnd | Channel 1 random Entity. Amount≥2: +1 Paranormal/turn |
| GlitchInTheSkyPower | Buff | Single | AfterOrbEvoked + AfterPlayerTurnStart | Bank energy from evokes; cash out at turn start |
| ParanormalReactorPower | Buff | Single | AfterPlayerTurnStart | If Paranormal ≥ 6: spend 3 → gain 1 Energy (Amount≥2: also draw 1) |
| BigfootPower | Buff | Single | BeforeTurnEnd | Deal current Paranormal dmg to random enemy |
| MothmanPower | Buff | Counter | AfterPowerAmountChanged (Madness) | Deal Amount dmg to ALL enemies |
| BlackEyedChildrenPower | Buff | Single | AfterOrbEvoked | Gain Amount Paranormal |
| EldritchTransformationPower | Buff | Single | AfterPowerAmountChanged (Madness) | Gain 1 TempDex (Amount≥2: also TempStr) |
| TempDexterityPower | Buff | Counter | ModifyBlockAdditive + BeforeTurnEnd (remove) | +Amount Block this turn |
| TempStrengthPower | Buff | Counter | ModifyDamageAdditive + BeforeTurnEnd (remove) | +Amount dmg this turn |
| AncientTabletPower | Buff | Single | AfterOrbEvoked (consumed) | Channel same orb type → EvokeLast (doubles evoke) |

---

## Code Architecture

```
CryptidCode/
  MainFile.cs                    # [ModInitializer] entry point
  Character/
    CryptidCharacter.cs          # PlaceholderCharacterModel — HP, deck, relics, color
    CryptidCardPool.cs           # CustomCardPoolModel — IsColorless = false, card back/energy color
    CryptidRelicPool.cs          # CustomRelicPoolModel — empty (pool required by framework)
    CryptidPotionPool.cs         # CustomPotionPoolModel — empty
  Cards/
    CryptidCard.cs               # Abstract base: [Pool(typeof(CryptidCardPool))],
                                 #   static state: ActiveCombatState, SkillsPlayedThisTurn, EvokedThisTurn
                                 #   helper: ChannelRandomEntity(ctx, owner)
    [card files]
  Powers/
    CryptidPower.cs              # Abstract base: static ActiveCombatState/Context/Player cache
                                 #   helper: ChannelRandomEntity(ctx, player)
    [power files]
  Orbs/
    GrayAlienOrb.cs              # Passive: 3 dmg. Evoke: Abduct ALL + damage scale
    EldritchOrb.cs               # Passive: 1 Madness. Evoke: consume Madness, draw cards
    CryptidOrb.cs                # Passive: 1 Paranormal. Evoke: 1 Energy
  Relics/
    TinfoilSkullRelic.cs         # Starter relic: [Pool(typeof(CryptidRelicPool))], gain 5 Paranormal on damage
  Extensions/
    StringExtensions.cs          # .CharacterUiPath(), .CardImagePath(), .BigCardImagePath(), .PowerImagePath()
```

### Critical API patterns

**Channeling orbs — always use generics, never constructors:**
```csharp
// CORRECT
await OrbCmd.Channel<GrayAlienOrb>(ctx, Owner);

// WRONG — throws DuplicateModelException on second call
await OrbCmd.Channel(ctx, new GrayAlienOrb(), Owner);
```

**Upgrading with private fields (behavioral switches):**
```csharp
private bool _upgraded = false;
protected override void OnUpgrade() => _upgraded = true;
```

**Upgrading numeric amounts:**
```csharp
protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<SomePower>(1m)];
protected override void OnUpgrade() => DynamicVars["SomePower"].UpgradeValueBy(1m);
```

**Static state in base classes** (refreshed by `BeforeHandDraw` each turn start):
- `CryptidCard.ActiveCombatState` — current combat state
- `CryptidCard.SkillsPlayedThisTurn` — count of skills played; incremented in `BeforeCardPlayed`
- `CryptidCard.EvokedThisTurn` — set to true by `AfterOrbEvoked`
- `CryptidPower.ActivePlayer` / `ActiveContext` / `ActiveCombatState`

**PowerCmd.Remove — takes instance, not generic:**
```csharp
await PowerCmd.Remove(this);  // inside the power itself
```

---

## Build & Test

```
cd Z:\Projects\slay_spire2\created_characters\Cryptid
dotnet build
```

Close the game before building (DLL locked while running).
Output copies automatically to `D:\SteamLibrary\steamapps\common\Slay the Spire 2\mods\Cryptid\`.

---

## Known Issues / Design Notes

1. **Placeholder art** — all images are placeholder PNGs. Portrait paths auto-derive from class name (lowercase, prefix stripped).

2. **No relics or potions** in pools yet — both pools are empty by design; `CryptidRelicPool` and `CryptidPotionPool` inherit defaults from base.

3. **Behavioral upgrade descriptions** — upgrades that change behavior (draw/exhaust/retain/cost) are hardcoded in localization as "Upgraded: also draw 1 card" etc. No dynamic var for these; the description is static.

4. **Orb constructor ban** — ALL `new GrayAlienOrb()` / `new EldritchOrb()` / `new CryptidOrb()` calls throw `DuplicateModelException` at runtime after the first call. Always use `OrbCmd.Channel<T>(ctx, player)`.

5. **CS8765 / CS8625 warnings** — pre-existing nullability warnings in orb evoke signatures and a few `FromCard(null)` calls. Non-blocking.

6. **GlitchInTheSkyPower** — banks energy via `_pendingEnergy` counter across evokes in a turn, then grants all at next turn start. Upgraded version also draws 1 card at turn start. The power does NOT remove itself between turns, so it accumulates correctly.
