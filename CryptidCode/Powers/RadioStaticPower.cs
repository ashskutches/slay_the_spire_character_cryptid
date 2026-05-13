using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Cryptid.CryptidCode.Powers;

// Amount = damage dealt per trigger (2 base, 3 upgraded).
// Fires once per card played that causes Paranormal to increase.
public class RadioStaticPower : CryptidPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    private int _paranormalBefore;

    public override Task BeforeCardPlayed(CardPlay cardPlay)
    {
        _paranormalBefore = Owner.GetPower<ParanormalPower>()?.Amount ?? 0;
        return Task.CompletedTask;
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        int paranormalNow = Owner.GetPower<ParanormalPower>()?.Amount ?? 0;
        if (paranormalNow <= _paranormalBefore || ActiveCombatState == null) return;
        var enemies = ActiveCombatState.Enemies.ToList();
        if (enemies.Count == 0) return;
        var target = enemies[Random.Shared.Next(enemies.Count)];
        Flash();
        await DamageCmd.Attack(Amount)
            .Unpowered()
            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
            .Targeting(target)
            .Execute(context);
    }
}
