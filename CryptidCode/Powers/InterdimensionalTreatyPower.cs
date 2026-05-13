using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Powers;

// Amount = 1 (base) or 2 (upgraded: +1 extra Block per trigger).
// Whenever Paranormal is gained, also gain the same amount as Block.
// Upgrade: gain 1 additional Block per trigger on top.
public class InterdimensionalTreatyPower : CryptidPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override decimal ModifyPowerAmountGiven(PowerModel power, Creature giver, decimal amount, Creature? target, CardModel? cardSource)
    {
        if (power is ParanormalPower && target == Owner && amount > 0)
        {
            int blockGain = (int)amount + (Amount >= 2 ? 1 : 0);
            Flash();
            Owner.GainBlockInternal(blockGain);
        }
        return amount;
    }
}
