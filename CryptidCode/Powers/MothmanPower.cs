using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Powers;

// Amount = damage dealt to all enemies when any enemy gains Madness.
public class MothmanPower : CryptidPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power is not MadnessPower || amount <= 0 || power.Owner?.IsPlayer != false) return;
        var ctx = ActiveContext;
        var state = ActiveCombatState;
        if (ctx == null || state == null) return;
        Flash();
        foreach (var enemy in state.Enemies.ToList())
            await DamageCmd.Attack(Amount)
                .FromCard(null)
                .Targeting(enemy)
                .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
                .Execute(ctx);
    }
}
