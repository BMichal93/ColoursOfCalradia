// =============================================================================
// COLOURS OF CALRADIA — SpellDatabase.cs
// Mount & Blade II: Bannerlord Mod  v2.0
// =============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Engine;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;
using TaleWorlds.CampaignSystem.MapEvents;

namespace ColoursOfCalradia
{
    // =========================================================================
    // 3. SPELL DATABASE  (18 battle spells, 4-char combos)
    // =========================================================================
    public enum SpellContext { Mission, Map }

    public class SpellEntry
    {
        public string      Name;
        public string      Combo;      // exactly 4 chars: U / L / R
        public ColorSchool School;
        public SpellContext Context;
        public string      Flavour;
    }

    public static class SpellDatabase
    {
        // Combos follow a strict structure: first 2 chars = Form, last 2 chars = Colour.
        // Forms: UU = Blast (cone), RL = Self (aura), LR = Create (area effect)
        // Colours: RR = Red, RU = Orange, LU = Yellow, LL = Green, UL = Blue, UR = Purple
        public static readonly IReadOnlyList<SpellEntry> All = new List<SpellEntry>
        {
            // ── BLAST (UU prefix) — medium cone in front of the caster ──────────
            new SpellEntry { Name="Crimson Torrent",  Combo="UURR", School=ColorSchool.Red,
                Context=SpellContext.Mission,
                Flavour="The rage of a thousand battles channelled into a single, devastating wave." },
            new SpellEntry { Name="Golden Tide",      Combo="UURU", School=ColorSchool.Orange,
                Context=SpellContext.Mission,
                Flavour="Wash over your foes with jubilant force; even enemies cannot resist the urge to advance." },
            new SpellEntry { Name="Tide of Dread",    Combo="UULU", School=ColorSchool.Yellow,
                Context=SpellContext.Mission,
                Flavour="A wave of creeping, nameless wrongness — it strips the nerve from all it touches and leaves behind only the urge to run." },
            new SpellEntry { Name="Verdant Surge",    Combo="UULL", School=ColorSchool.Green,
                Context=SpellContext.Mission,
                Flavour="A tide of living energy that flows toward your own — allies in the cone are mended, enemies and the caster untouched." },
            new SpellEntry { Name="Azure Arrest",     Combo="UUUL", School=ColorSchool.Blue,
                Context=SpellContext.Mission,
                Flavour="A freezing wave of scholarly force. All before you halt; riders are unseated." },
            new SpellEntry { Name="Grey Harvest",     Combo="UUUR", School=ColorSchool.Purple,
                Context=SpellContext.Mission,
                Flavour="The grey settles over one soul in the cone. They simply stop. The body follows the spirit out like a slow tide." },

            // ── SELF (RL prefix) — creates a glowing aura around the caster ─────
            // Note: DD prefix cannot be used — S with empty buffer opens the spellbook.
            new SpellEntry { Name="Scarlet Ward",     Combo="RLRR", School=ColorSchool.Red,
                Context=SpellContext.Mission,
                Flavour="The next blow lands on iron. One strike. The ward then shatters." },
            new SpellEntry { Name="Warm Beacon",      Combo="RLRU", School=ColorSchool.Orange,
                Context=SpellContext.Mission,
                Flavour="A golden light calls your companions from across the field to your side." },
            new SpellEntry { Name="Nausea Bloom",     Combo="RLLU", School=ColorSchool.Yellow,
                Context=SpellContext.Mission,
                Flavour="Something deeply wrong radiates from you. All who linger nearby feel it in their stomach before they know it in their mind." },
            new SpellEntry { Name="Verdant Touch",    Combo="RLLL", School=ColorSchool.Green,
                Context=SpellContext.Mission,
                Flavour="You lay hands upon yourself. The wounds knit closed." },
            new SpellEntry { Name="Cerulean Mirror",  Combo="RLUL", School=ColorSchool.Blue,
                Context=SpellContext.Mission,
                Flavour="Spells pass through you for 40 seconds. Steel does not." },
            new SpellEntry { Name="Grief's Veil",     Combo="RLUR", School=ColorSchool.Purple,
                Context=SpellContext.Mission,
                Flavour="The grey folds you from sight for 12 seconds. Nearby enemies lose track of you and pause. You cannot be touched while the veil holds." },

            // ── CREATE (LR prefix) — special area effect, specific to each colour ─
            new SpellEntry { Name="Cinder Burst",     Combo="LRRR", School=ColorSchool.Red,
                Context=SpellContext.Mission,
                Flavour="The world around you ignites. All nearby pay the price of your fury." },
            new SpellEntry { Name="Golden Snare",     Combo="LRRU", School=ColorSchool.Orange,
                Context=SpellContext.Mission,
                Flavour="A bright patch of golden earth waits for the first soul to step into it — then gives their formation a random, chaotic order and vanishes. Cast again to dismiss." },
            new SpellEntry { Name="Creeping Dread",   Combo="LRLU", School=ColorSchool.Yellow,
                Context=SpellContext.Mission,
                Flavour="A cloud of formless revulsion drifts across the field. Those it passes through feel their skin crawl and their courage hollow out. Cast again to dismiss." },
            new SpellEntry { Name="Emerald Font",     Combo="LRLL", School=ColorSchool.Green,
                Context=SpellContext.Mission,
                Flavour="A blessed circle of earth. All who stand within are slowly mended — friend and foe alike. Cast again to dismiss." },
            new SpellEntry { Name="Sapphire Bastion", Combo="LRUL", School=ColorSchool.Blue,
                Context=SpellContext.Mission,
                Flavour="A wall of solid blue force rises from the earth, repelling all who approach. Fades after two minutes." },
            new SpellEntry { Name="Hollow Gaze",      Combo="LRUR", School=ColorSchool.Purple,
                Context=SpellContext.Mission,
                Flavour="One nearby enemy empties out. They stand. They do not move. They wait for nothing. Cast again to release them." },
        };

        public static SpellEntry Find(string combo) =>
            All.FirstOrDefault(s => s.Combo == combo);

        public static IEnumerable<SpellEntry> BySchool(ColorSchool school) =>
            All.Where(s => s.School == school);
    }
}
