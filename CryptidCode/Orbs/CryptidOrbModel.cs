using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Cryptid.CryptidCode.Orbs;

public abstract class CryptidOrbModel : CustomOrbModel
{
    // Wire up Passive() to fire at end of each turn (mirrors how native orbs work).
    public override async Task BeforeTurnEndOrbTrigger(PlayerChoiceContext choiceContext)
    {
        var target = CombatState?.Enemies.FirstOrDefault(e => e.IsAlive);
        await OrbCmd.Passive(choiceContext, this, target);
    }

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
