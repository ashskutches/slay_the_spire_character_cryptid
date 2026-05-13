using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Cryptid.CryptidCode.Powers;

public class ParanormalPower : CryptidPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != CombatSide.Player || Amount <= 0) return;
        Flash();
        Owner.GainBlockInternal(Amount);
        int remaining = Amount / 2;
        Owner.RemovePowerInternal(this);
        if (remaining > 0)
            await PowerCmd.Apply<ParanormalPower>(Owner, remaining, Owner, null);
    }
}
