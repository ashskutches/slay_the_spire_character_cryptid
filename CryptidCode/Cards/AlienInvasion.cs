using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Cryptid.CryptidCode.Cards;

// Deal damage equal to (multiplier × cards in hand). Upgrade: cost 1 less.
public sealed class AlienInvasion : CryptidCard
{
    public AlienInvasion() : base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) { }

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(3m, ValueProp.Move),
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        int handCount = Owner.PlayerCombatState?.Hand.Cards.Count(c => c != this) ?? 0;
        int dmg = DynamicVars.Damage.IntValue * Math.Max(1, handCount);
        await DamageCmd.Attack(dmg)
            .FromCard(this).Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
            .Execute(ctx);
    }

    protected override void OnUpgrade() => EnergyCost.SetCustomBaseCost(1);
}
