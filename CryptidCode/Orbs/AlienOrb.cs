using BaseLib.Abstracts;
using Cryptid.CryptidCode.Cards;
using Cryptid.CryptidCode.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Orbs;

public class AlienOrb : CryptidOrbModel
{
    public override bool IncludeInRandomPool => true;
    public override string CustomIconPath => "power.png".PowerImagePath();
    public override string CustomSpritePath => "power.png".BigPowerImagePath();
    public override decimal PassiveVal => 0m;
    public override decimal EvokeVal => 0m;
    public override Color DarkenedColor => new Color(0.4f, 0.7f, 0.5f);

    public override List<(string, string)> Localization => new OrbLoc(
        "Alien",
        "Passive: Add an [gold]Alien Technology[/gold] card to your hand.\nEvoke: Add an [gold]Advanced Technology[/gold] card to your hand.",
        "Passive: Alien Technology to hand. Evoke: Advanced Technology to hand.",
        []);

    public override async Task Passive(PlayerChoiceContext ctx, Creature target)
    {
        await CardPileCmd.AddGeneratedCardToCombat(
            ModelDb.Card<AlienTechnology>(), PileType.Hand, true, CardPilePosition.Top);
    }

    public override async Task<IEnumerable<Creature>> Evoke(PlayerChoiceContext ctx)
    {
        await CardPileCmd.AddGeneratedCardToCombat(
            ModelDb.Card<AdvancedTechnology>(), PileType.Hand, true, CardPilePosition.Top);
        return [];
    }
}
