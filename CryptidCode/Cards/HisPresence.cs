using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Cryptid.CryptidCode.Cards;

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

        var state = ActiveCombatState;
        if (state == null) return;

        if (_upgraded)
        {
            foreach (var enemy in state.Enemies.ToList())
                await PowerCmd.Apply<MadnessPower>(enemy, 5, Owner.Creature, this);
        }
        else
        {
            // Only apply if the target survived the attack
            if (state.Enemies.Contains(play.Target))
                await PowerCmd.Apply<MadnessPower>(play.Target, 5, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(5m);
        _upgraded = true;
    }
}
