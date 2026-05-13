using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Cards;

// Channel 1(2) random Entity orb(s) and draw 1 card.
public sealed class CropCirclePattern : CryptidCard
{
    public CropCirclePattern() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self) { }

    private int _orbCount = 1;

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        for (int i = 0; i < _orbCount; i++)
            await ChannelRandomEntity(ctx, Owner);
        await CardPileCmd.Draw(ctx, Owner);
    }

    protected override void OnUpgrade() => _orbCount = 2;
}
