using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Cryptid.CryptidCode.Powers;

// At start of turn: if Paranormal >= 6, spend 3 Paranormal → gain 1 Energy (+ draw 1 if upgraded).
public class ParanormalReactorPower : CryptidPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext ctx, Player player)
    {
        var paranormal = Owner.GetPower<ParanormalPower>();
        if (paranormal == null || paranormal.Amount < 6) return;
        Flash();
        int remaining = paranormal.Amount - 3;
        await PowerCmd.Remove(paranormal);
        if (remaining > 0)
            await PowerCmd.Apply<ParanormalPower>(Owner, remaining, Owner, null);
        player.PlayerCombatState?.GainEnergy(1m);
        if (Amount >= 2)
            await CardPileCmd.Draw(ctx, player);
    }
}
