using BaseLib.Abstracts;
using Cryptid.CryptidCode.Extensions;
using Godot;

namespace Cryptid.CryptidCode.Character;

public class CryptidCardPool : CustomCardPoolModel
{
    public override string Title => CryptidCharacter.CharacterId;

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();

    // Eerie green tint for card backs
    public override float H => 0.38f;
    public override float S => 0.7f;
    public override float V => 0.6f;

    public override Color DeckEntryCardColor => new("22BB66");

    public override bool IsColorless => false;
}
