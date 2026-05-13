using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Cryptid.CryptidCode.Cards;

// Deal 10(15) damage. Apply Madness to target. Upgrade: apply Madness to ALL enemies.
public sealed class HisPresence : CryptidCard
{
    public HisPresence() : base(2, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy) { }

    private bool _upgraded = false;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(10m, ValueProp.Move),
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
            .FromCard(this).Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
            .Execute(ctx);

        if (_upgraded)
        {
            var state = ActiveCombatState;
            if (state != null)
                foreach (var enemy in state.Enemies.ToList())
                    await PowerCmd.Apply<MadnessPower>(enemy, 1, Owner.Creature, this);
        }
        else
        {
            await PowerCmd.Apply<MadnessPower>(play.Target, 1, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(5m);
        _upgraded = true;
    }
}
