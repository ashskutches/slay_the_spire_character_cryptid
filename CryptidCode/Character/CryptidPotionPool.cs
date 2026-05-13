using BaseLib.Abstracts;
using Cryptid.CryptidCode.Extensions;
using Godot;

namespace Cryptid.CryptidCode.Character;

public class CryptidPotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => new("22BB66");

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}
