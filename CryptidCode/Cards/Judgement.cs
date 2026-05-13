using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Cryptid.CryptidCode.Cards;

// Deal 5 damage. If the enemy has Madness, deal 2(5) additional damage.
public sealed class Judgement : CryptidCard
{
    public Judgement() : base(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy) { }

    private int _bonusDamage = 2;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(5m, ValueProp.Move),
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this).Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
            .Execute(ctx);

        if (play.Target.GetPower<MadnessPower>() != null)
            await DamageCmd.Attack(_bonusDamage)
                .FromCard(this).Targeting(play.Target)
                .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
                .Execute(ctx);
    }

    protected override void OnUpgrade() => _bonusDamage = 5;
}
