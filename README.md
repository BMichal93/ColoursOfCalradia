# Colours of Calradia — v2.0

A Mount & Blade II: Bannerlord magic overhaul built around six colour schools, named mage lords, persistent magical units, and a personality system that shifts with every cast.

---

## Package Structure

```
ColoursOfCalradia/
├── SubModule.xml               mod manifest
├── ModuleData/
│   ├── items.xml               (reserved for future spell items)
│   └── troops.xml              (reserved for future mage troops)
├── src/
│   ├── MagicSystem.cs          all C# logic (~3 500 lines, one file)
│   └── TheWitheringArt.csproj  build project (outputs ColoursOfCalradia.dll)
└── README.md                   this file
```

---

## Installation

1. Copy the entire folder into `Modules/ColoursOfCalradia/` inside your Bannerlord directory.
2. Build the project (see **Build** below). The DLL output is `ColoursOfCalradia.dll`.
3. Place the DLL in `Modules/ColoursOfCalradia/bin/Gaming.Desktop.x64_Shipping_Client/` (Xbox/Game Pass) or `bin/Win64_Shipping_Client/` (Steam).
4. Enable **Colours of Calradia** in the Bannerlord launcher.

---

## Build

```powershell
# Xbox / Game Pass
$env:BannerlordPath = "C:\XboxGames\Mount & Blade II- Bannerlord\Content"

# Steam
# $env:BannerlordPath = "C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord"

cd src
dotnet build TheWitheringArt.csproj
```

Output DLL: `src/bin/Debug/ColoursOfCalradia.dll`

The project targets `.NET Framework 4.7.2` to match the game's runtime. All logic lives in the single file `MagicSystem.cs`.

---

## Starting the Mod — Colour Selection

When a new campaign starts you will see a multi-selection screen titled **"The Colours of Calradia"**. Hover each colour to read its school flavour text, attribute penalty, and two permanent limitations.

- Select any combination of colours (including none — you walk an uncoloured path).
- Your chosen colours are **permanent** for that playthrough.
- Each colour you pick reduces one attribute by 1 and locks in two permanent limitations.
- You may select as many as all six colours simultaneously.

### Adjacency and Madness

The six colours form a ring: **Red — Orange — Yellow — Green — Blue — Purple** (wrapping back to Red).

If you pick two or more colours that are **not contiguous** on the ring (e.g. Red and Yellow, skipping Orange), the incompatible natures fracture your sense of self. **Madness** is applied at game start: two random personality traits shift by ±1, chosen from Mercy, Valor, Honor, Generosity, and Calculating.

Contiguous selections (e.g. Red + Orange + Yellow, or Blue + Purple + Red wrapping around) carry no madness penalty regardless of how many schools you choose.

---

## The Six Colour Schools

| Colour | Identity | Attribute penalty | Personality drift |
|--------|----------|-------------------|-------------------|
| **Red** | Blood Price — violent, fiery | Control | Calculating −1 |
| **Orange** | Generous Hunger — joyful, open-handed | Intelligence | Generosity +1 |
| **Yellow** | The Fearful Eye — dread, revulsion | Social | Valor −1 |
| **Green** | Gentle Burden — kind, restorative | Endurance | Mercy +1 |
| **Blue** | Scholar's Weight — cold, ordered | Vigor | Calculating +1 |
| **Purple** | The Waning Art — melancholic, fading | Cunning | Mercy −1 |

### Limitations by School

Each school carries two permanent limitations. The first category (A) applies every cast in battle; the second (B) may add a passive daily effect or a further cast cost.

**Red — Blood Price**
- **(A) Furious:** Each Red spell automatically issues a Charge order to all your formations.
- **(B) Blood Price:** Each Red spell opens a wound on the caster — 8 HP self-damage.

**Orange — Generous Hunger**
- **(A) Overindulgent:** Your party consumes food faster and army upkeep is higher. 2 food units are drained daily.
- **(B) Lighthearted:** Each Orange spell costs gold — 5% of your total, minimum 50 gold. You cannot cast without it.

**Yellow — The Fearful Eye**
- **(A) Paranoia:** Each Yellow spell costs your party 8 morale. The fear bleeds inward.
- **(B) Blurred Judgment:** Each Yellow spell increases your criminal rating by 3 in the relevant kingdom. You begin to see threats everywhere.

**Green — Gentle Burden**
- **(A) Pacifist:** You cannot use Green magic while wielding a weapon. Sheathe it first.
- **(B) Gentle Burden:** If enemies are within 4 m when you cast, the magic recoils — 5 HP self-damage.

**Blue — Scholar's Weight**
- **(A) Scholar's Fatigue:** Each Blue spell tires the body — 5 HP self-damage.
- **(B) Heavy Knowledge:** Blue magic settles in the flesh passively (flavour; the fatigue cost covers it mechanically).

**Purple — The Waning Art**
- **(A) Waning Cost:** Each Purple spell ages the caster by approximately 7 days.
- **(B) The Slow Unravelling:** Purple magic does not announce itself. It simply continues until there is less of you.

---

## Casting — Controls

### Requirement: Daylight

**All magic requires sunlight.** You cannot cast at night, in underground locations, or in any environment where day hours (06:00–20:00 in-game) do not apply. The same restriction applies to NPC mages.

### Keyboard

1. Hold **Left Alt** to enter spell mode.
2. Input any 6-key combo using **W / A / S / D**, which map to:
   - **W** → U (Up)
   - **A** → L (Left)
   - **D** → R (Right)
   - **S** → D (Down) — but only when the buffer is non-empty. Pressing S with an empty buffer opens your spellbook instead.
3. Release **Left Alt** to fire.

### Gamepad

1. Hold **Left Trigger** to enter spell mode.
2. Push the **right stick** in a direction:
   - Stick Up → U
   - Stick Down → D
   - Stick Left → L
   - Stick Right → R
3. Press **L3 (left stick click)** to open the spellbook (instead of casting).
4. Release **Left Trigger** to fire.

### Spellbook

Press **S** (keyboard, empty buffer) or **L3** (gamepad, while holding Alt/LT) to cycle through your known spells. The spellbook shows each spell's name, combo, and effect.

---

## The Spell System

All 18 battle spells follow a strict **Form + Colour** combo structure:

- The **first two characters** encode the *form* (how the spell behaves).
- The **last four characters** encode the *colour school*.

### Forms

| Prefix | Form | Shape |
|--------|------|-------|
| `UU` | **Blast** | Cone in front of the caster |
| `DD` | **Self** | Aura or effect centred on the caster |
| `LR` | **Create** | Persistent area effect placed on the battlefield |

### Colour Suffixes

| Suffix | School |
|--------|--------|
| `UURR` | Red |
| `LLRR` | Orange |
| `LRLU` | Yellow |
| `RRLL` | Green |
| `LLUU` | Blue |
| `RRLU` | Purple |

---

## Spell List

### Blast Spells (UU prefix) — Cone effects

| Spell | Combo | School | Effect |
|-------|-------|--------|--------|
| **Crimson Torrent** | `UUUURR` | Red | 40 damage to all enemies in a forward cone; pushes them back 6 m (smooth lerp over 0.4 s). |
| **Golden Tide** | `UULLRR` | Orange | 8 damage to cone enemies; forces all enemy formations to Charge. |
| **Tide of Dread** | `UULRLU` | Yellow | 8 damage to cone enemies; drains 30 morale from each. |
| **Verdant Surge** | `UURRLL` | Green | Heals all creatures in the cone (allies and enemies) for 15 HP — entirely indiscriminate. |
| **Azure Arrest** | `UULLUU` | Blue | 8 damage to cone enemies; halts all enemy formations; dismounts riders. |
| **Grey Harvest** | `UURRLU` | Purple | Instantly kills one random creature in the cone. |

### Self Spells (DD prefix) — Caster-centred effects

| Spell | Combo | School | Effect |
|-------|-------|--------|--------|
| **Scarlet Ward** | `DDUURR` | Red | 5 s invulnerability. |
| **Warm Beacon** | `DDLLRR` | Orange | Pulls all allies within 30 m to a ring around the caster (smooth lerp). |
| **Nausea Bloom** | `DDLRLU` | Yellow | Persistent 30 s aura (radius 8 m) that deals 5 damage every 2 s to all nearby creatures. |
| **Verdant Touch** | `DDRRLL` | Green | Heals the caster for 20 HP. |
| **Cerulean Mirror** | `DDLLUU` | Blue | 30 s invulnerability (scholarly approximation of full magic immunity). |
| **Grief's Veil** | `DDRRLU` | Purple | Drains morale of all enemies within 20 m to zero; grants the caster 15 s invulnerability while the veil holds. |

### Create Spells (LR prefix) — Persistent battlefield effects

| Spell | Combo | School | Effect |
|-------|-------|--------|--------|
| **Cinder Burst** | `LRUURR` | Red | Instant 45 damage to all creatures within 10 m. |
| **Gilded Ground** | `LRLLRR` | Orange | Toggle: places a persistent dismounting zone at the caster's feet. Any horse entering the radius is separated from its rider. Cast again to dismiss. |
| **Creeping Dread** | `LRLRLU` | Yellow | Toggle: releases a wandering cloud (radius 5 m) that roams the field, dealing 5 damage every 2 s to creatures it passes through. Changes direction randomly every ~3 s. Cast again to dismiss. |
| **Emerald Font** | `LRRRLL` | Green | Toggle: creates a healing circle (radius 8 m) that restores 8 HP every 2 s to all within it — friend and foe alike. Cast again to dismiss. |
| **Sapphire Bastion** | `LRLLUU` | Blue | Places a persistent repulsion field (radius 10 m) for 3 minutes. Any creature entering the radius is smoothly pushed outward every 0.5 s. |
| **Hollow Gaze** | `LRRRLU` | Purple | Pins one random nearby non-hero enemy (within 15 m) into a catatonic state — they stand still and do nothing. The effect is maintained until cancelled. Cast again to release them. |

### Notes on Create Spells

- **Toggled effects** (Gilded Ground, Creeping Dread, Emerald Font, Hollow Gaze) are cancelled by casting the same spell again.
- **Timed effects** (Nausea Bloom at 30 s, Sapphire Bastion at 3 minutes) expire automatically and display a message when they fade.
- **Area glows**: Affected agents pulse in the school's colour on each effect tick (approximately every 2 s for most effects, every 0.5 s for Sapphire Bastion). Glow is not applied every frame to avoid performance cost.
- **Smooth movement**: Push and pull effects (Crimson Torrent, Warm Beacon, Sapphire Bastion) move agents using a smoothstep lerp over 0.4 s rather than instant teleportation.

---

## Visual Effects

Each school has a distinct colour used for both combat messages and agent glow:

| School | Glow / Message Colour |
|--------|-----------------------|
| Red | Bright red |
| Orange | Orange |
| Yellow | Bright yellow |
| Green | Green |
| Blue | Blue |
| Purple | Purple |

When a spell affects an agent, that agent is outlined in the casting school's colour for 1.5–3 s depending on the effect. The caster themselves glow when activating Self or Create spells.

---

## Personality Drift

Every **25 casts** of the same school shifts one personality trait — but only if the trait actually has room to move (clamped at ±2). A message appears only when the trait moves.

| School | Trait | Direction |
|--------|-------|-----------|
| Red | Calculating | −1 |
| Orange | Generosity | +1 |
| Yellow | Valor | −1 |
| Green | Mercy | +1 |
| Blue | Calculating | +1 |
| Purple | Mercy | −1 |

---

## Tournament Rule

Casting **any** spell during a tournament **immediately disqualifies you** — your character is killed and removed from the match with a warning message.

---

## Level-Up Learning

At character levels 10, 20, 30 … (while you have at least one school but fewer than six), a prompt appears offering you one new colour to learn. You may decline. Accepting applies the new school's attribute penalty and limitations immediately.

---

## Mage Lords (NPC)

### Seeding

- At campaign start, lords across all factions are seeded with 1–3 colour schools based on party size and random chance.
- Companion heroes have a **10 % chance** to be granted 1–2 colours when they join your party.
- When a mage lord dies, their colours are extinguished. After **7 days**, the colours pass to a randomly selected younger lord (under 50) in the same kingdom.

### Battle AI

NPC mage lords cast in battle using a priority-driven AI:

1. **Self-heal** with Verdant Touch if below 35 % HP (Green lords only, if not wielding a weapon).
2. **8 % random wild cast** — fires a random applicable school spell.
3. **Swarm response** — if 3+ enemies within 8 m, prefer Cinder Burst (Red) or Grey Harvest (Purple).
4. **Cone attack** — Crimson Torrent (Red) or Azure Arrest (Blue) if enemies are in the forward arc.
5. **Ally healing** — Verdant Surge (Green) if allies in the forward arc are wounded.
6. **Morale drain** — Tide of Dread (Yellow).
7. **Reinforcement** — Calling (Orange) if outnumbered; Warm Beacon as fallback.

All NPC battle spells **require daylight** (same global restriction as the player). NPC lords have a 12-second cast cooldown between spells.

### Campaign Map Spells

Each mage lord has a **20 % daily chance** to cast a campaign-map spell. At most **3 lord map-casts fire per day** across the entire world to keep the log readable. Each lord has a cooldown of 6–9 days after casting.

| School | Effect A (50 %) | Effect B (50 %) |
|--------|-----------------|-----------------|
| **Red** | Own party morale +10 (*Bloodlust*) | Random enemy village hearth ×0.8 (*Carnage*) |
| **Orange** | Nearest friendly village hearth ×1.05 (*Celebrate*) | Transfer 1–2 troops from rival lord to own party (*Bribe*) |
| **Yellow** | Random enemy lord renown −10 (*Fade*) | Random enemy party morale −15 (*Melancholy*) |
| **Green** | Heal up to 3 wounded in own party per troop type (*Rejuvenate*) | Add 5 grain to own inventory (*Crops*) |
| **Blue** | Own clan influence +8 (*Schemes*) | Enemy clan influence −8 (*Plots*) |
| **Purple** | Enemy influence −5, enemy morale −10 (*Curse*) | Add 10 recruits to own party (*Bind*) |

---

## Named Magical Units

### Seeding

Small numbers of named magical soldiers are seeded into lord and bandit parties:

| Party Type | Condition | Units seeded |
|------------|-----------|--------------|
| Lord party | 50–149 troops | 1 unit |
| Lord party | 150+ troops | 2 units |
| Bandit party | 15+ troops, 12 % chance | 1 unit |

Each named unit receives 1–2 colour schools. Their names are generated from a pool of 20 first names combined with school-specific suffixes:

| School | Suffixes |
|--------|----------|
| Red | the Ember, Bloodhanded, Pyremark |
| Orange | the Bright, Goldenvoiced, the Warm |
| Yellow | the Pale, of Dread, the Craven |
| Green | the Tender, Root-spoken, the Verdant |
| Blue | the Still, Coldwater, the Patient |
| Purple | the Hollow, the Grey, the Fading |

New qualifying parties are seeded during daily maintenance: 15 % chance for lord parties with 50+ troops, 3 % for bandit parties with 15+ troops.

### Battle Behaviour

Named magical units cast spells in battle using the same AI rules as mage lords, with a **20-second cooldown** between casts and a **0.5 s AI evaluation tick**. They require daylight to cast.

### Campaign Map Effects

Each named unit has a **10 % daily chance** to trigger a minor campaign map effect. At most **2 unit map-casts fire per day** total. After casting, a unit cannot cast on the map again for 3–5 days. Effects are generally positive for the unit's own party (morale, experience, food).

### Respawn

When a named magical unit is killed in battle:

1. They enter a **respawn queue** with a delay of **3–5 days**.
2. After the delay, they reappear in their original lord's party, or in a fallback lord's party within the same faction if the original party has disbanded.

---

## Children & Companions

### Children

Children of the main hero born during the campaign have a **30 % chance** to inherit magical colours. When they do:

- They always inherit at least one of the parent's schools.
- Their total number of schools equals the parent's count ±1, clamped to 1–6.
- Any remaining slots are filled from the pool of colours the parent does not already hold.

### Companions

When a companion joins your party they have a **10 % chance** to carry colour gifts:

- 10 % of that chance: 2 colours.
- 20 % of that chance: 1 colour.
- Otherwise: no colours.

A message announces how many colours they carry when they join.

---

## Colour Ring Reference

```
         Red (0)
        /        \
  Purple (5)    Orange (1)
      |               |
  Blue (4)      Yellow (2)
        \        /
         Green (3)
```

Adjacent colours on the ring do not cause Madness. Non-adjacent combinations (e.g. Red + Yellow, or Green + Red) do.
