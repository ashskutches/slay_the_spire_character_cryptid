using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Cards;

public sealed class StrangeSighting : CryptidCard
{
    public StrangeSighting() : base(1, CardType.Skill, CardRarity.Basic, TargetType.Self) { }

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await CardPileCmd.Draw(ctx, Owner);
        await PowerCmd.Apply<ParanormalPower>(Owner.Creature, 3, Owner.Creature, this);
    }

    protected override void OnUpgrade() { }
}
