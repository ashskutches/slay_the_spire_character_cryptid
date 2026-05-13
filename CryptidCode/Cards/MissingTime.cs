using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Cryptid.CryptidCode.Cards;

public sealed class MissingTime : CryptidCard
{
    public MissingTime() : base(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy) { }

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<MadnessPower>(2m),
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<MadnessPower>(play.Target, DynamicVars["MadnessPower"].IntValue, Owner.Creature, this);
        await CardPileCmd.Draw(ctx, Owner);
    }

    protected override void OnUpgrade() => DynamicVars["MadnessPower"].UpgradeValueBy(2m);
}
