using BaseLib.Abstracts;
using Godot;

namespace Cryptid.CryptidCode.Orbs;

public abstract class CryptidOrbModel : CustomOrbModel
{
    public override Node2D CreateCustomSprite()
    {
        var texture = ResourceLoader.Load<Texture2D>(CustomSpritePath);
        if (texture == null)
            return new Node2D();
        var sprite = new Sprite2D();
        sprite.Texture = texture;
        return sprite;
    }
}
