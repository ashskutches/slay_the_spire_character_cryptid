using MegaCrit.Sts2.Core.Entities.Powers;

namespace Cryptid.CryptidCode.Powers;

public class AbductedPower : CryptidPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Single;
}
