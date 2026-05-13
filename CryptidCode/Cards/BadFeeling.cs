using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Cryptid.CryptidCode.Cards;

// 0-cost Skill Common Exhaust. Gain 10(15) Paranormal.
public sealed class BadFeeling : CryptidCard
{
    public BadFeeling() : base(0, CardType.Skill, CardRarity.Common, TargetType.Self) { }

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<ParanormalPower>(10m),
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<ParanormalPower>(Owner.Creature, DynamicVars["ParanormalPower"].IntValue, Owner.Creature, this);
        await CardCmd.Exhaust(ctx, this, false, false);
    }

    protected override void OnUpgrade() => DynamicVars["ParanormalPower"].UpgradeValueBy(5m);
}
