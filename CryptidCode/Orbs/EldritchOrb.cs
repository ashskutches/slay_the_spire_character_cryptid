using BaseLib.Abstracts;
using Cryptid.CryptidCode.Extensions;
using Cryptid.CryptidCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Cryptid.CryptidCode.Orbs;

public class EldritchOrb : CryptidOrbModel
{
    public override bool IncludeInRandomPool => true;
    public override string CustomIconPath => "power.png".PowerImagePath();
    public override string CustomSpritePath => "power.png".BigPowerImagePath();
    public override decimal PassiveVal => 1m;
    public override decimal EvokeVal => 0m;
    public override Color DarkenedColor => new Color(0.25f, 0.0f, 0.35f);

    public override List<(string, string)> Localization => new OrbLoc(
        "Eldritch",
        "Passive: Apply 1 Madness to a random enemy.\nEvoke: Consume all Madness from all enemies. Draw 1 card per Madness removed.",
        "Passive: 1 Madness. Evoke: Consume all Madness, draw per stack removed.",
        []);

    public override async Task Passive(PlayerChoiceContext ctx, Creature target)
    {
        if (target == null) return;
        await PowerCmd.Apply<MadnessPower>(target, 1, Owner.Creature, null);
    }

    public override async Task<IEnumerable<Creature>> Evoke(PlayerChoiceContext ctx)
    {
        var state = CombatState;
        if (state == null) return [];

        int totalMadness = 0;
        foreach (var enemy in state.Enemies.ToList())
        {
            var madness = enemy.GetPower<MadnessPower>();
            if (madness == null) continue;
            totalMadness += madness.Amount;
            await PowerCmd.Remove(madness);
        }

        for (int i = 0; i < totalMadness; i++)
            await CardPileCmd.Draw(ctx, Owner);

        return [];
    }
}
