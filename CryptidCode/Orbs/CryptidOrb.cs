using BaseLib.Abstracts;
using Cryptid.CryptidCode.Extensions;
using Cryptid.CryptidCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Cryptid.CryptidCode.Orbs;

public class CryptidOrb : CryptidOrbModel
{
    public override bool IncludeInRandomPool => true;
    public override string CustomIconPath => "power.png".PowerImagePath();
    public override string CustomSpritePath => "power.png".BigPowerImagePath();
    public override decimal PassiveVal => 1m;
    public override decimal EvokeVal => 5m;
    public override Color DarkenedColor => new Color(0.0f, 0.4f, 0.4f);

    public override List<(string, string)> Localization => new OrbLoc(
        "Cryptid",
        "Passive: Apply 1 [debuff]Madness[/debuff] to ALL enemies.\nEvoke: Apply 5 [debuff]Madness[/debuff] to ALL enemies.",
        "Passive: 1 Madness all. Evoke: 5 Madness all.",
        []);

    public override async Task Passive(PlayerChoiceContext ctx, Creature target)
    {
        var state = CombatState;
        if (state == null) return;
        foreach (var enemy in state.Enemies.Where(e => e.IsAlive).ToList())
            await PowerCmd.Apply<MadnessPower>(enemy, 1, Owner.Creature, null);
    }

    public override async Task<IEnumerable<Creature>> Evoke(PlayerChoiceContext ctx)
    {
        var state = CombatState;
        if (state == null) return [];
        foreach (var enemy in state.Enemies.Where(e => e.IsAlive).ToList())
            await PowerCmd.Apply<MadnessPower>(enemy, 5, Owner.Creature, null);
        return [];
    }
}
