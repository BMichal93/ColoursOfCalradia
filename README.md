# The Withering Art — Installation & Compilation Guide

## What's In This Package

```
TheWitheringArt/
├── SubModule.xml               ← mod manifest
├── ModuleData/
│   └── items.xml               ← all 24 spell books + Awakening Mark
├── src/
│   ├── MagicSystem.cs          ← all C# logic (single file)
│   └── TheWitheringArt.csproj  ← build project
└── README.md                   ← this file
```

---

## Step 1: Set Up the Folder Structure

Inside your Bannerlord `Modules/` directory, create:

```
Modules/
└── TheWitheringArt/
    ├── SubModule.xml
    ├── ModuleData/
    │   └── items.xml
    └── bin/
        └── Win64_Shipping_Client/
            └── TheWitheringArt.dll   ← compiled output goes here
```

Copy `SubModule.xml` and `ModuleData/items.xml` from this package into place.

---

## Step 2: Compile the DLL

**Requirements:**
- Visual Studio 2019+ OR the .NET SDK (dotnet CLI)
- .NET Framework 4.7.2 developer pack

**Using dotnet CLI (recommended):**

```powershell
# Set your game path
$env:BannerlordPath = "C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord"

# Build
cd src/
dotnet build TheWitheringArt.csproj --configuration Release
```

The DLL is automatically output to `Modules/TheWitheringArt/bin/Win64_Shipping_Client/`.

**Using Visual Studio:**
1. Open `src/TheWitheringArt.csproj`
2. Set `BannerlordPath` in your environment variables or replace the `$(BannerlordPath)` references in the .csproj with your literal path
3. Build → Release

---

## Step 3: Activate the Mod

1. Launch the Bannerlord launcher
2. Go to **Mods** tab
3. Enable **The Withering Art**
4. Make sure it loads after: Native, SandBoxCore, Sandbox, StoryMode

---

## Step 4: Opt Into the Gift

The mod does not force magic on every character. To opt in:

1. Use the in-game console (`~` key, enable via launcher options) and run:
   ```
   campaign.give_item twa_gift_mark
   ```
2. On the next daily tick, the mod detects the Awakening Mark in your inventory, grants the Gift, and applies a **−1 penalty** to a random physical attribute (Vigor, Control, or Endurance).

> **Proper integration:** A full character-creation hook (via `CampaignGameStarter.AddGameMenu`) can be added in a future version to present this as a choice during character creation.

---

## How to Cast Spells

1. **Find a spell book** — sold at merchants (rare, expensive) or looted
2. **Carry it** — the next daily tick, the spell is learned automatically
3. **Hold Left Alt** to enter Focus State
4. **Type the combo** using WASD:
   - W = Up, S = Down, A = Left, D = Right
5. **Release Left Alt** to fire

The combo is visible on-screen as you type it. Minimum 4 inputs required.

---

## The Tyrant Ritual

While **waiting** (in a settlement or camp), the mod automatically consumes one prisoner per in-game hour and restores 30 days of youth.

- Floor: You cannot de-age below 20 years old
- Cost: −100 Honor, −5 party Morale per prisoner consumed

---

## Known Limitations & TODOs

| Spell | Status | Notes |
|---|---|---|
| Silver Tongue | Partial | Trait nudge; persuasion roll hook is TODO |
| Iron-Snap | Placeholder | Gate unlock requires `DestructibleComponent` API |
| Ignite Arrow | Placeholder | WeaponComponentData swap at runtime is TODO |
| Mist Veil | Placeholder | MissionProjectile velocity override is TODO |
| Wind-Sail | Partial | Speed model patch (`PartySpeedCalculatingModel`) is TODO |
| Falcon's Sight | Partial | `SeeingRange` is read-only; `ExploreModel` patch needed |
| Call Ancestors | Partial | Full respawn requires `SpawnAgentVisualsForSide` |
| Wall Breaker | Partial | Gate destruction via `UsableMachineAI` is TODO |

All other spells are fully functional.

---

## Spell Reference

| Spell | Combo | Cost | Context |
|---|---|---|---|
| Shadow Step | LLRR | 2 days | Mission |
| Spur of the Void | UURR | 3 days | Mission |
| Aegis Spark | DUDU | 3 days | Mission |
| Falcon's Sight | UULR | 4 days | Map |
| Repel | DDDD | 4 days | Mission |
| Mending | UDUD | 5 days | Mission |
| Silver Tongue | LRLR | 5 days | Map |
| Nature's Bounty | DLDR | 7 days | Map |
| Iron-Snap | DRDL | 8 days | Mission |
| Ignite Arrow | URDL | 8 days | Mission |
| Mist Veil | LRUU | 10 days | Mission |
| Serenity | DDUU | 15 days | Map |
| Wind-Sail | RRUUD | 10 days | Map |
| Voice of Khan | LRLRU | 14 days | Mission |
| Vanish | LLLLU | 20 days | Mission |
| Battanian Veil | DDLRUU | 20 days | Mission |
| Imperial Flare | UULRDD | 30 days | Mission |
| Sturgian Chill | LLDDRR | 60 days | Mission |
| Soul Harvest | DDDDD | 1 day | Mission |
| Rain of Iron | UDLRUDLR | 1 year | Mission |
| Wall Breaker | DDDUUULR | 1 year | Mission |
| Call Ancestors | UUDDLRLRUD | 2 years | Mission |
| Sun's Wrath | ULDRULDRUU | 3 years | Mission |
| Plague Wind | LRLRDDUULR | 5 years | Mission |
