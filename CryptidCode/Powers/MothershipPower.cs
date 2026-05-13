using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Cryptid.CryptidCode.Powers;

// At end of turn: Channel 1 random Entity. If any enemy is Abducted, deal 5 to ALL enemies.
// Upgraded (Amount >= 2): triggers twice.
public class MothershipPower : CryptidPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task BeforeTurnEnd(PlayerChoiceContext ctx, CombatSide side)
    {
        if (side != CombatSide.Player) return;
        var state = ActiveCombatState;
        var player = ActivePlayer;
        if (state == null || player == null) return;
        Flash();
        int triggers = Amount >= 2 ? 2 : 1;
        for (int i = 0; i < triggers; i++)
            await ChannelRandomEntity(ctx, player);

        if (state.Enemies.Any(e => e.HasPower<AbductedPower>()))
            foreach (var enemy in state.Enemies.ToList())
                await DamageCmd.Attack(5m)
                    .FromCard(null)
                    .Targeting(enemy)
                    .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
                    .Execute(ctx);
    }
}
