using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Powers;

// Amount = 1 base, 2 upgraded. All Paranormal gains increased by Amount.
public class HiveMindPower : CryptidPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override decimal ModifyPowerAmountGiven(PowerModel power, Creature? giver, decimal amount, Creature? target, CardModel? cardSource)
    {
        if (power is ParanormalPower && target == Owner && amount > 0)
        {
            Flash();
            return amount + Amount;
        }
        return amount;
    }
}
