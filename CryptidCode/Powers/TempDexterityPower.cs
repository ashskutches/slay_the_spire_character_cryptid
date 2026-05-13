using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Cryptid.CryptidCode.Powers;

// Bonus block gain this turn. Removed at end of player's turn.
public class TempDexterityPower : CryptidPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override decimal ModifyBlockAdditive(Creature? target, decimal block, ValueProp props, CardModel? cardSource, CardPlay? cardPlay)
    {
        if (target == Owner) return block + Amount;
        return block;
    }

    public override async Task BeforeTurnEnd(PlayerChoiceContext ctx, CombatSide side)
    {
        if (side != CombatSide.Player) return;
        await PowerCmd.Remove(this);
    }
}
