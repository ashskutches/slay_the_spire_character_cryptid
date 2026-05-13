using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Cryptid.CryptidCode.Powers;

// At end of turn: Channel 1 random Entity.
// Upgraded (Amount >= 2): also gain 1 Paranormal each turn.
public class ObservationDronePower : CryptidPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task BeforeTurnEnd(PlayerChoiceContext ctx, CombatSide side)
    {
        if (side != CombatSide.Player) return;
        var player = ActivePlayer;
        if (player == null) return;
        Flash();
        await ChannelRandomEntity(ctx, player);
        if (Amount >= 2)
            await PowerCmd.Apply<ParanormalPower>(Owner, 1, Owner, null);
    }
}
