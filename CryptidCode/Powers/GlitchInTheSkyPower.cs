using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Powers;

// Whenever you Evoke an Entity, gain 1 Energy next turn.
// Upgraded (Amount >= 2): also draw 1 card when this triggers.
public class GlitchInTheSkyPower : CryptidPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    private int _pendingEnergy = 0;

    public override Task AfterOrbEvoked(PlayerChoiceContext ctx, OrbModel orb, IEnumerable<Creature> targets)
    {
        _pendingEnergy++;
        Flash();
        return Task.CompletedTask;
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext ctx, Player player)
    {
        if (_pendingEnergy <= 0) return;
        player.PlayerCombatState?.GainEnergy(_pendingEnergy);
        if (Amount >= 2)
            await CardPileCmd.Draw(ctx, player);
        Flash();
        _pendingEnergy = 0;
    }
}
