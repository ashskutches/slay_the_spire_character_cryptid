using BaseLib.Abstracts;
using Cryptid.CryptidCode.Extensions;
using Cryptid.CryptidCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Cryptid.CryptidCode.Orbs;

public class AlienOrb : CryptidOrbModel
{
    public override bool IncludeInRandomPool => true;
    public override string CustomIconPath => "power.png".PowerImagePath();
    public override string CustomSpritePath => "power.png".BigPowerImagePath();
    public override decimal PassiveVal => 1m;
    public override decimal EvokeVal => 1m;
    public override Color DarkenedColor => new Color(0.4f, 0.7f, 0.5f);

    public override List<(string, string)> Localization => new OrbLoc(
        "Alien",
        "Passive: Draw 1 card.\nEvoke: Draw 2 cards and gain 1 [gold]Paranormal[/gold].",
        "Passive: Draw 1. Evoke: Draw 2 + 1 Paranormal.",
        []);

    public override async Task Passive(PlayerChoiceContext ctx, Creature target)
    {
        await CardPileCmd.Draw(ctx, Owner);
    }

    public override async Task<IEnumerable<Creature>> Evoke(PlayerChoiceContext ctx)
    {
        await CardPileCmd.Draw(ctx, Owner);
        await CardPileCmd.Draw(ctx, Owner);
        await PowerCmd.Apply<ParanormalPower>(Owner.Creature, 1, Owner.Creature, null);
        return [];
    }
}
