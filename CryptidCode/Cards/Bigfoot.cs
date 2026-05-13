using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Cards;

public sealed class Bigfoot : CryptidCard
{
    public Bigfoot() : base(2, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<BigfootPower>(Owner.Creature, 1, Owner.Creature, this);
    }

    protected override void OnUpgrade() => EnergyCost.SetCustomBaseCost(0);
}
