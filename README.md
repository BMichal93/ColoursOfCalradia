# The Withering Art - Installation and Mechanics Guide

The Withering Art is a Bannerlord magic overhaul built around spell books, mage lords, mage units, and a dark sacrifice ritual. This README describes the current mechanics in the project, not an older spell sheet.

## What's In This Package

```text
TheWitheringArt/
|-- SubModule.xml               mod manifest
|-- ModuleData/
|   `-- items.xml               all 39 spell books + Awakening Mark
|-- src/
|   |-- MagicSystem.cs          all C# logic
|   `-- TheWitheringArt.csproj  build project
`-- README.md                   this file
```

## Installation

1. Copy `SubModule.xml` and `ModuleData/items.xml` into your Bannerlord `Modules/TheWitheringArt/` folder.
2. Build `src/TheWitheringArt.csproj` with Visual Studio or `dotnet build`.
3. Put the resulting DLL in `Modules/TheWitheringArt/bin/Win64_Shipping_Client/`.
4. Enable the mod in the Bannerlord launcher.

## Opt Into The Gift

The mod does not force magic on every character.

1. Use the in-game console and run:

```text
campaign.give_item twa_gift_mark
```

2. On the next daily tick, the mod detects the Awakening Mark, grants the Gift, and applies a narrative penalty to one random physical attribute.

## Casting Spells

1. Find a spell book by buying it from merchants or looting it.
2. Carry the book. The next daily tick reveals the spell automatically.
3. Hold Left Alt.
4. Type the combo with WASD.
5. Release Left Alt to cast.

The combo is shown in the grimoire and on discovery messages. Books are for discovery and reminders; once the Gift is learned, casting is handled by the combo system.

Mission spells work in battles, map spells work on the campaign map, and some spells are allowed in both contexts.

## Spell Schools

The current color coding is:

- Red: offensive damage spells
- Blue: control and manipulation spells
- Green: support and healing spells

That color is used for glow, particles, and some cast feedback.

## Tournament Rule

Spellcasting during a tournament is forbidden. If you successfully cast a spell in a tournament mission, you are immediately disqualified with a warning message.

## Spell Economy

- Every cast has an age cost.
- Player casts use the spell's listed age cost.
- Mage lord casts use a heavier age cost multiplier.
- Mage lords also age a little every day, reflecting the magic they use off-screen and in battles the player does not personally witness.

## World Map Lord Magic

Mage lords are a separate system from the player.

- The mod seeds a limited number of mage lords per faction.
- Each day, living mage lords have a chance to cast a world map spell.
- Their world map spells are faction-flavored and can affect morale, supplies, renown, villages, or enemy lords.
- When a mage lord casts, the age cost is scaled up.
- Mage lords also drift older every day even if they did not cast that day.

### Aserai Mage Lords

Aserai mage lords get special treatment through `Dark Bargain`.

- Before age 40, Dark Bargain can still target an enemy lord.
- At age 40 and above, they start spending prisoners first and then soldiers to reduce their own age.
- That reduction uses the same sacrifice logic as the player ritual, including morale penalties.

## Battle Mage Lords

Mage lords who appear in battle use AI spellcasting.

Typical battle casts include:

- Mending when badly wounded
- Repel when enemies swarm them
- Blast when enemies cluster in front
- Confuse against nearby enemies
- Battle command spells such as Halt, Enrage, Dismount, and Stop Arrows
- Severe Life for Aserai mage lords

They also use the same age-cost system when they cast.

## Break Spirits

`Break Spirits` is a battle spell unlocked by winning a fight while your warband is in a very poor morale state.

Mechanically, it breaks nearby enemy resolve and keeps them pinned down briefly. It is a control spell, so it uses the blue school color.

## Mage Units

Mage units are the void channelers that appear in mage-lord armies.

- They are identified by the `twa_mage_` troop prefix.
- Mage lord parties are padded to about 5 percent mage units.
- Allied mage units near the player create a battery effect that improves spell damage.
- They cast a small combat bolt on a cooldown.
- Each cast can trigger burn-out, which can kill the unit and deal splash damage.

## Tyrant Ritual

The Hollow Covenant / Tyrant Ritual is the age-reversal system.

### Unlocking the ritual

- Find the ritual scroll item.
- Carry it for 3 days.
- After the third day, the ritual knowledge is unlocked.

### Running the ritual

While waiting in a settlement or camp:

- The ritual consumes prisoners first.
- If no prisoners remain, it sacrifices soldiers instead.
- Prisoner sacrifice restores 30 days of youth per victim.
- Soldier sacrifice restores 60 days of youth per victim.
- The player cannot de-age below 20 years old.
- The ritual applies party morale penalties and a narrative cost.

## Spell Roster

The current project contains 39 spells.

The complete combo sheet is handled in-game through spell books and the grimoire, but the current roster is:

- Memory
- Vortex
- Detonate
- Mark
- Blast
- Accelerate
- Suppress
- Halt
- Shroud
- Bane
- Enrage
- Dismount
- Repel
- Stop Arrows
- Scatter
- Dark Bargain
- Rejuvenate
- Restore
- Featherfall
- Inspire
- Mending
- Charm
- Sinister Will
- Severe Life
- Clairvoyance
- Relocate
- Pacify
- Weightless
- Levitate
- Devour
- Confuse
- Swap
- Calling
- Aura of Hate
- Hollow Name
- Break Spirits
- Long Road
- Unname
- Crush

## Notes

- Some spells are mission-only.
- Some spells are map-only.
- Some spells are available in both contexts.
- The project intentionally uses one C# file so the systems stay centralized and easy to audit.

## Build

```powershell
$env:BannerlordPath = "C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord"
cd src
dotnet build TheWitheringArt.csproj --configuration Release
```

The DLL is output to `Modules/TheWitheringArt/bin/Win64_Shipping_Client/`.
