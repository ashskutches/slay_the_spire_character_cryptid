using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Cryptid.CryptidCode.Powers;

// At end of your turn, deal damage to a random enemy equal to your current Paranormal.
public class BigfootPower : CryptidPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task BeforeTurnEnd(PlayerChoiceContext ctx, CombatSide side)
    {
        if (side != CombatSide.Player) return;
        var state = ActiveCombatState;
        if (state == null || !state.Enemies.Any()) return;
        var paranormal = Owner.GetPower<ParanormalPower>();
        if (paranormal == null || paranormal.Amount <= 0) return;
        Flash();
        await DamageCmd.Attack(paranormal.Amount)
            .FromCard(null)
            .TargetingRandomOpponents(state, false)
            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
            .Execute(ctx);
    }
}
