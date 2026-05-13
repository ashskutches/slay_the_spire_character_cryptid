using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Cryptid.CryptidCode.Cards;

public sealed class StaticBlast : CryptidCard
{
    public StaticBlast() : base(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy) { }

    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(6m, ValueProp.Move),
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this).Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
            .Execute(ctx);

        if (play.Target.GetPower<AbductedPower>() != null)
            await DamageCmd.Attack(3m)
                .FromCard(this).Targeting(play.Target)
                .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
                .Execute(ctx);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(3m);
}
