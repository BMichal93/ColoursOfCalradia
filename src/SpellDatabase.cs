// =============================================================================
// COLOURS OF CALRADIA — SpellDatabase.cs
// Mount & Blade II: Bannerlord Mod  v1.2.0.0
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
    // 3. SPELL DATABASE  (18 battle spells + 18 campaign map spells)
    // =========================================================================
    public enum SpellContext { Mission, Map }

    public class SpellEntry
    {
        public string      Name;
        public string      Combo;      // exactly 4 chars: U / L / R / D
        public ColorSchool School;
        public SpellContext Context;
        public string      ShortDesc;
        public string      Flavour;
    }

    public static class SpellDatabase
    {
        // Combos follow a strict structure: first 2 chars = Form, last 2 chars = Colour.
        // Forms: UU = Blast (cone), RL = Self (aura), LR = Create (area effect),
        //        UL = Affect (campaign map), LU = Invoke (campaign map, advanced), UR = Commune (campaign map, ambient)
        // Colours: RR = Red, LD = Orange, DD = Yellow, LL = Green, RU = Blue, DU = Purple
        public static readonly IReadOnlyList<SpellEntry> All = new List<SpellEntry>
        {
            // ── BLAST (UU prefix) — medium cone in front of the caster ──────────
            new SpellEntry { Name="Crimson Torrent",  Combo="UURR", School=ColorSchool.Red,
                Context=SpellContext.Mission,
                ShortDesc="Fire cone, damages all ahead.",
                Flavour="You do not strike — you erupt. The rage that has been building since the first wound finally finds its shape." },
            new SpellEntry { Name="Golden Tide",      Combo="UULD", School=ColorSchool.Orange,
                Context=SpellContext.Mission,
                ShortDesc="Force cone; enemies advance.",
                Flavour="An irresistible warmth crashes outward. Even your enemies cannot fight the urge to close the distance — and that is exactly what you wanted." },
            new SpellEntry { Name="Tide of Dread",    Combo="UUDD", School=ColorSchool.Yellow,
                Context=SpellContext.Mission,
                ShortDesc="Fear cone, breaks nerve.",
                Flavour="It does not look like anything. It does not sound like anything. It simply passes through them — and what it leaves behind has forgotten what courage felt like." },
            new SpellEntry { Name="Verdant Surge",    Combo="UULL", School=ColorSchool.Green,
                Context=SpellContext.Mission,
                ShortDesc="Heals allies in cone.",
                Flavour="Life knows its own. The tide moves only toward your people -- the Battanian druids taught this before the first Calradic road was ever laid. Enemy and caster untouched, allies mended as the green passes through." },
            new SpellEntry { Name="Azure Arrest",     Combo="UURU", School=ColorSchool.Blue,
                Context=SpellContext.Mission,
                ShortDesc="Freezes and unseats all ahead.",
                Flavour="The Scholar's word stops the world mid-sentence -- the old Calradic Academy named it. All before you simply halt. Riders find themselves seated on still air." },
            new SpellEntry { Name="Grey Harvest",     Combo="UUDU", School=ColorSchool.Purple,
                Context=SpellContext.Mission,
                ShortDesc="Kills 1–3 in the cone (scales with Vigor).",
                Flavour="The grey is not cruel. It is simply thorough -- as something once moved through the Calradic Empire's last years. It finds the weakest flames and extinguishes them without ceremony." },

            // ── SELF (RL prefix) — creates a glowing aura around the caster ─────
            new SpellEntry { Name="Scarlet Ward",     Combo="RLRR", School=ColorSchool.Red,
                Context=SpellContext.Mission,
                ShortDesc="Blocks next blow.",
                Flavour="One strike will land on iron instead of flesh. The ward does not care how large the blow is. It simply says: not yet." },
            new SpellEntry { Name="Warm Beacon",      Combo="RLLD", School=ColorSchool.Orange,
                Context=SpellContext.Mission,
                ShortDesc="Calls allies to your side.",
                Flavour="The gold light calls across the noise and the blood. Your companions hear it even through fear — and they come." },
            new SpellEntry { Name="Nausea Bloom",     Combo="RLDD", School=ColorSchool.Yellow,
                Context=SpellContext.Mission,
                ShortDesc="Sickening aura, weakens nearby.",
                Flavour="Something deeply wrong radiates from you. Those who come close enough feel it in the stomach, then the knees. The body knows before the mind does." },
            new SpellEntry { Name="Verdant Touch",    Combo="RLLL", School=ColorSchool.Green,
                Context=SpellContext.Mission,
                ShortDesc="Heals yourself.",
                Flavour="The green asks nothing of you except stillness. You lay a hand against the wound and wait. Sometimes that is enough." },
            new SpellEntry { Name="Cerulean Mirror",  Combo="RLRU", School=ColorSchool.Blue,
                Context=SpellContext.Mission,
                ShortDesc="Deflects missiles, 18s or 4 volleys.",
                Flavour="The Scholar turns arrows into observations. They pass through you — noted, categorised, dismissed. Four volleys. Eighteen seconds. Steel does not get a theory." },
            new SpellEntry { Name="Grey Reaping",     Combo="RLDU", School=ColorSchool.Purple,
                Context=SpellContext.Mission,
                ShortDesc="Kills 1–2 nearby enemies; drains nerve from the rest.",
                Flavour="The grey radiates outward like a sigh. The nearest go first — cleanly, without ceremony. Those who remain are left holding something they cannot name and cannot put down." },

            // ── CREATE (LR prefix) — special area effect, specific to each colour ─
            new SpellEntry { Name="Cinder Burst",     Combo="LRRR", School=ColorSchool.Red,
                Context=SpellContext.Mission,
                ShortDesc="Explosion, damages all nearby.",
                Flavour="You detonate. There is no other word for it. Everything within reach pays for the privilege of standing close." },
            new SpellEntry { Name="Golden Recoil",    Combo="LRLD", School=ColorSchool.Orange,
                Context=SpellContext.Mission,
                ShortDesc="Retribution zone, toggle.",
                Flavour="The zone does not protect — it remembers. Every blow landed within it earns a portion returned. Those who keep swinging learn this lesson at increasing cost. Cast again to dismiss." },
            new SpellEntry { Name="Creeping Dread",   Combo="LRDD", School=ColorSchool.Yellow,
                Context=SpellContext.Mission,
                ShortDesc="Drifting fear cloud, toggle.",
                Flavour="Nine clouds of formless wrongness drift where the wind takes them. You cannot aim them. You do not need to. Cast again to dismiss." },
            new SpellEntry { Name="Emerald Font",     Combo="LRLL", School=ColorSchool.Green,
                Context=SpellContext.Mission,
                ShortDesc="Healing pool, toggle.",
                Flavour="Three points of living light open in the earth. Friend and foe alike are mended within — the green does not take sides. Cast again to dismiss." },
            new SpellEntry { Name="Sapphire Bastion", Combo="LRRU", School=ColorSchool.Blue,
                Context=SpellContext.Mission,
                ShortDesc="Force wall, toggle.",
                Flavour="Six pillars of solid blue force seal the line. The wall does not argue, does not tire, does not yield. Cast again to dismiss." },
            new SpellEntry { Name="Hollow Gaze",      Combo="LRDU", School=ColorSchool.Purple,
                Context=SpellContext.Mission,
                ShortDesc="Freezes one enemy, toggle.",
                Flavour="One nearby enemy is simply removed from the battle — still standing, still breathing, empty of everything. They wait for nothing. Cast again to release them." },

            // ── INVOKE (LU prefix) — campaign map only, advanced forms ───────────
            new SpellEntry { Name="Red Lightning",    Combo="LURR", School=ColorSchool.Red,
                Context=SpellContext.Map,
                ShortDesc="Wounds a random enemy soldier.",
                Flavour="The red reaches across the distance and strikes without warning. One soldier in the nearest enemy host falls before a single order is given." },
            new SpellEntry { Name="Guidance",         Combo="LULD", School=ColorSchool.Orange,
                Context=SpellContext.Map,
                ShortDesc="Grants XP to a random ally.",
                Flavour="Purpose flows through the warmth. One soldier feels it land behind the eyes — a sudden understanding of what they were doing wrong, and how to do it right." },
            new SpellEntry { Name="Creeping Fear",    Combo="LUDD", School=ColorSchool.Yellow,
                Context=SpellContext.Map,
                ShortDesc="Nearest enemy party loses 15–30 morale.",
                Flavour="You do not shout. You do not threaten. Something quieter moves through the air toward them — and they feel it in their chests before they understand why." },
            new SpellEntry { Name="Animal Friendship",Combo="LULL", School=ColorSchool.Green,
                Context=SpellContext.Map,
                ShortDesc="A random animal or food appears.",
                Flavour="The green does not ask the world to make sense. Something living finds its way into your possession — grateful, or simply confused about where it came from." },
            new SpellEntry { Name="Blue Influence",   Combo="LURU", School=ColorSchool.Blue,
                Context=SpellContext.Map,
                ShortDesc="Gain 4 influence. Kingdom only.",
                Flavour="The right insight, spoken in the right hall, at exactly the right moment. Four times the value of a season of battles. The court was listening." },
            new SpellEntry { Name="Purple Isolation", Combo="LUDU", School=ColorSchool.Purple,
                Context=SpellContext.Map,
                ShortDesc="Random enemy lord loses 8 renown.",
                Flavour="The grey reaches into another life and takes something significant. They will not know precisely what is gone — only that the room feels slightly emptier when they enter it." },

            // ── AFFECT (UL prefix) — campaign map only, situation-dependent ─────
            new SpellEntry { Name="Burning Winds",    Combo="ULRR", School=ColorSchool.Red,
                Context=SpellContext.Map,
                ShortDesc="Curses a random enemy village. Hearth −10%.",
                Flavour="The red does not need presence to destroy. It rides the wind across the distance and finds something worth ruining before anyone even knows to watch for it." },
            new SpellEntry { Name="Rallying Call",    Combo="ULLD", School=ColorSchool.Orange,
                Context=SpellContext.Map,
                ShortDesc="Party morale +5.",
                Flavour="A warmth moves through the ranks like news of a victory. They stand a little straighter. The road looks shorter. The enemy looks smaller." },
            new SpellEntry { Name="Chains of Fear",   Combo="ULDD", School=ColorSchool.Yellow,
                Context=SpellContext.Map,
                ShortDesc="Conscripts a random prisoner. Own morale −1.",
                Flavour="One prisoner is made to understand that their options have narrowed considerably. They join the march. Your soldiers watch, and say nothing. The silence has a weight." },
            new SpellEntry { Name="Mending Touch",    Combo="ULLL", School=ColorSchool.Green,
                Context=SpellContext.Map,
                ShortDesc="50% chance to heal one wounded soldier.",
                Flavour="The green finds the nearest wound and reaches for it. Sometimes the magic holds long enough to do something useful. Sometimes it slips through." },
            new SpellEntry { Name="Philosopher's Stone", Combo="ULRU", School=ColorSchool.Blue,
                Context=SpellContext.Map,
                ShortDesc="Generate gold (scales with Blue attribute). Become 1 day younger (min age 22).",
                Flavour="The Scholar turns the great wheel. Base matter becomes coin; time concedes a small step. Twenty-two is the floor — the stone does not grant immortality. Only time." },
            new SpellEntry { Name="Purple Confusion", Combo="ULDU", School=ColorSchool.Purple,
                Context=SpellContext.Map,
                ShortDesc="Scatters nearby enemies.",
                Flavour="The grey settles like a question no one can answer. Those who were hunting you lose the thread entirely — they scatter as though they were never sure what they were looking for." },

            // ── COMMUNE (UR prefix) — campaign map only, ambient effects ─────────
            new SpellEntry { Name="Crimson Tithe",    Combo="URRR", School=ColorSchool.Red,
                Context=SpellContext.Map,
                ShortDesc="Sacrifice a soldier for skill XP. Morale −1.",
                Flavour="Blood spent is never wasted if spent with purpose. One life becomes many lessons. Your soldiers remember the cost." },
            new SpellEntry { Name="Good Word",        Combo="URLD", School=ColorSchool.Orange,
                Context=SpellContext.Map,
                ShortDesc="Random lord or notable gains +2 relations.",
                Flavour="A warmth in the right ear at the right moment — twice the value of a formal introduction. The recipient may not know why they think of you fondly. That is fine." },
            new SpellEntry { Name="Sow Doubt",        Combo="URDD", School=ColorSchool.Yellow,
                Context=SpellContext.Map,
                ShortDesc="Enemy settlement loyalty −10.",
                Flavour="You do not need to be present. Rumour and unnamed dread do the work you arrange from a distance. One enemy town grows uneasy in its own walls." },
            new SpellEntry { Name="Verdant Bond",     Combo="URLL", School=ColorSchool.Green,
                Context=SpellContext.Map,
                ShortDesc="Friendly village hearth +20.",
                Flavour="Life calls to life across the distance. The green finds a friendly field and breathes into it — the soil opens, the animals calm, the harvest thickens." },
            new SpellEntry { Name="Arcane Sight",     Combo="URRU", School=ColorSchool.Blue,
                Context=SpellContext.Map,
                ShortDesc="Locate the 10 nearest colour lords.",
                Flavour="The Scholar's eye opens across the whole of Calradia. Colour leaves a trace that time cannot fully erase -- and the Sight follows it, naming who is near, what they carry, how far they roam across her roads." },
            new SpellEntry { Name="The Waning",       Combo="URDU", School=ColorSchool.Purple,
                Context=SpellContext.Map,
                ShortDesc="Random enemy lord ages 7 days. Clan renown −3.",
                Flavour="The grey reaches into another life and takes a week from them — quietly, without asking. Seven days gone, and the reputation their clan built over years dims with it." },
        };

        public static SpellEntry Find(string combo) =>
            All.FirstOrDefault(s => s.Combo == combo);

        public static IEnumerable<SpellEntry> BySchool(ColorSchool school) =>
            All.Where(s => s.School == school);
    }
}
