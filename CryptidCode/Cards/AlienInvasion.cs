using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Cryptid.CryptidCode.Cards;

// Spend ALL Paranormal. Deal 4 damage per 2 Paranormal to ALL enemies. Exhaust.
public sealed class AlienInvasion : CryptidCard
{
    public AlienInvasion() : base(2, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies) { }

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(4m, ValueProp.Move),
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);

        var paranormal = Owner.Creature.GetPower<ParanormalPower>();
        int spent = paranormal?.Amount ?? 0;
        if (paranormal != null)
            await PowerCmd.Remove(paranormal);

        int hits = spent / 2;
        if (hits > 0)
        {
            var state = ActiveCombatState;
            if (state != null)
            {
                int dmg = DynamicVars.Damage.IntValue;
                foreach (var enemy in state.Enemies.ToList())
                    for (int i = 0; i < hits; i++)
                        await DamageCmd.Attack(dmg)
                            .FromCard(this).Targeting(enemy)
                            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
                            .Execute(ctx);
            }
        }

        await CardCmd.Exhaust(ctx, this, false, false);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(2m);
}
