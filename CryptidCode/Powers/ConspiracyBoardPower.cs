using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Cryptid.CryptidCode.Powers;

// Every 3 Skills played in a turn, channel 1 random Entity.
// Upgraded: also gain 1 Paranormal per trigger.
public class ConspiracyBoardPower : CryptidPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    private const int Threshold = 3;
    private int _skillsThisTurn;

    public override async Task AfterCardPlayed(PlayerChoiceContext ctx, CardPlay cardPlay)
    {
        if (cardPlay.Card.Type != CardType.Skill) return;
        _skillsThisTurn++;
        if (_skillsThisTurn < Threshold) return;
        _skillsThisTurn -= Threshold;
        Flash();
        var player = ActivePlayer;
        if (player != null)
            await ChannelRandomEntity(ctx, player);
        if (Amount >= 2)
            await PowerCmd.Apply<ParanormalPower>(Owner, 1, Owner, null);
    }

    public override Task BeforeTurnEnd(PlayerChoiceContext ctx, CombatSide side)
    {
        if (side == CombatSide.Player) _skillsThisTurn = 0;
        return Task.CompletedTask;
    }
}
