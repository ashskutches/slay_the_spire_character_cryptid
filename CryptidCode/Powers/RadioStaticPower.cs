using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Powers;

// Amount = damage dealt per trigger (2 base, 3 upgraded).
// cardSource != null means the Paranormal gain came directly from a card play.
public class RadioStaticPower : CryptidPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power is not ParanormalPower || amount <= 0 || power.Owner != Owner || cardSource == null) return;
        var state = ActiveCombatState;
        var ctx = ActiveContext;
        if (state == null || ctx == null) return;
        var enemies = state.Enemies.ToList();
        if (enemies.Count == 0) return;
        Flash();
        await DamageCmd.Attack(Amount)
            .Unpowered()
            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
            .Targeting(enemies[Random.Shared.Next(enemies.Count)])
            .Execute(ctx);
    }
}
