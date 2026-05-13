using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Cryptid.CryptidCode.Cards;

public sealed class ObservationDrone : CryptidCard
{
    public ObservationDrone() : base(2, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<ObservationDronePower>(1m),
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<ObservationDronePower>(Owner.Creature, DynamicVars["ObservationDronePower"].IntValue, Owner.Creature, this);
    }

    protected override void OnUpgrade() => DynamicVars["ObservationDronePower"].UpgradeValueBy(1m);
}
