using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Cryptid.CryptidCode.Cards;

public sealed class BlackEyedChildren : CryptidCard
{
    public BlackEyedChildren() : base(1, CardType.Power, CardRarity.Common, TargetType.Self) { }

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<BlackEyedChildrenPower>(1m),
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<BlackEyedChildrenPower>(Owner.Creature, DynamicVars["BlackEyedChildrenPower"].IntValue, Owner.Creature, this);
    }

    protected override void OnUpgrade() => DynamicVars["BlackEyedChildrenPower"].UpgradeValueBy(1m);
}
