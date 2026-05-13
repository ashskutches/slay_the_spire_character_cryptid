using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Cards;

// Curse. Whenever you Evoke an Entity, lose 1 HP.
public sealed class Whispers : CryptidCard
{
    public Whispers() : base(-2, CardType.Curse, CardRarity.Curse, TargetType.Self) { }

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    public override Task AfterOrbEvoked(PlayerChoiceContext choiceContext, OrbModel orb, IEnumerable<Creature> targets)
    {
        // base sets EvokedThisTurn; this curse also drains 1 HP on each evoke
        Owner?.Creature?.HealInternal(-1);
        return base.AfterOrbEvoked(choiceContext, orb, targets);
    }

    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play) => Task.CompletedTask;
    protected override void OnUpgrade() { }
}
