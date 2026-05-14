using BaseLib.Abstracts;
using Cryptid.CryptidCode.Extensions;
using Cryptid.CryptidCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Cryptid.CryptidCode.Orbs;

public class GhostOrb : CryptidOrbModel
{
    public override bool IncludeInRandomPool => true;
    public override string CustomIconPath => "power.png".PowerImagePath();
    public override string CustomSpritePath => "power.png".BigPowerImagePath();
    public override decimal PassiveVal => 1m;
    public override decimal EvokeVal => 1m;
    public override Color DarkenedColor => new Color(0.9f, 0.9f, 1.0f);

    public override List<(string, string)> Localization => new OrbLoc(
        "Ghost",
        "Passive: Gain 1 [gold]Paranormal[/gold].\nEvoke: Gain 1 Energy.",
        "Passive: 1 Paranormal. Evoke: 1 Energy.",
        []);

    public override async Task Passive(PlayerChoiceContext ctx, Creature target)
    {
        await PowerCmd.Apply<ParanormalPower>(Owner.Creature, 1, Owner.Creature, null);
    }

    public override async Task<IEnumerable<Creature>> Evoke(PlayerChoiceContext ctx)
    {
        Owner.PlayerCombatState?.GainEnergy(1m);
        return [];
    }
}
