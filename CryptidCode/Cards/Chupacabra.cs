using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Cryptid.CryptidCode.Cards;

// Spend ALL Paranormal. Deal 8 + 4 per Paranormal. Heal 1 per 2 Paranormal.
public sealed class Chupacabra : CryptidCard
{
    public Chupacabra() : base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) { }

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(4m, ValueProp.Move),
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);

        var paranormal = Owner.Creature.GetPower<ParanormalPower>();
        int spent = paranormal?.Amount ?? 0;
        if (paranormal != null)
            await PowerCmd.Remove(paranormal);

        int totalDamage = 8 + DynamicVars.Damage.IntValue * spent;
        await DamageCmd.Attack(totalDamage)
            .FromCard(this).Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
            .Execute(ctx);

        int healAmount = spent / 2;
        if (healAmount > 0)
            await CreatureCmd.Heal(Owner.Creature, healAmount, true);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(2m);
}
