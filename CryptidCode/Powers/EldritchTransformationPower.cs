using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Powers;

// Amount = 1 base, 2 upgraded.
// Base: whenever Madness is applied to any enemy, gain 1 temp Dexterity.
// Upgraded: also gain 1 temp Strength.
public class EldritchTransformationPower : CryptidPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power is not MadnessPower || amount <= 0 || applier != Owner) return;
        Flash();
        await PowerCmd.Apply<TempDexterityPower>(Owner, 1, Owner, null);
        if (Amount >= 2)
            await PowerCmd.Apply<TempStrengthPower>(Owner, 1, Owner, null);
    }
}
