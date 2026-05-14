using Cryptid.CryptidCode.Orbs;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Cards;

public sealed class CropCirclePattern : CryptidCard
{
    public CropCirclePattern() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self) { }

    private int _drawCount = 2;

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await OrbCmd.Channel<AlienOrb>(ctx, Owner);
        for (int i = 0; i < _drawCount; i++)
            await CardPileCmd.Draw(ctx, Owner);
    }

    protected override void OnUpgrade() => _drawCount = 3;
}
