using BaseLib.Abstracts;
using Cryptid.CryptidCode.Extensions;
using Cryptid.CryptidCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Cryptid.CryptidCode.Orbs;

public class GrayAlienOrb : CryptidOrbModel
{
    public override bool IncludeInRandomPool => true;
    public override string CustomIconPath => "power.png".PowerImagePath();
    public override string CustomSpritePath => "power.png".BigPowerImagePath();
    public override decimal PassiveVal => 3m;
    public override decimal EvokeVal => 1m;
    public override Color DarkenedColor => new Color(0.4f, 0.6f, 0.8f);

    public override List<(string, string)> Localization => new OrbLoc(
        "Gray Alien",
        $"Passive: Deal {PassiveVal} damage to a random enemy.\nEvoke: Apply [debuff]Abducted[/debuff] to ALL enemies. Deal damage to all enemies equal to the number of Gray Alien Orbs channeled.",
        $"Passive: {PassiveVal} dmg. Evoke: Abduct all, damage = orb count.",
        []);

    public override async Task Passive(PlayerChoiceContext ctx, Creature target)
    {
        if (target == null) return;
        await DamageCmd.Attack(PassiveVal)
            .FromOsty(Owner.Creature, null)
            .Targeting(target)
            .Unpowered()
            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
            .Execute(ctx);
    }

    public override async Task<IEnumerable<Creature>> Evoke(PlayerChoiceContext ctx)
    {
        var state = CombatState;
        if (state == null) return [];

        int orbCount = Owner.PlayerCombatState?.OrbQueue?.Orbs.Count(o => o is GrayAlienOrb) ?? 0;

        foreach (var enemy in state.Enemies.ToList())
            await PowerCmd.Apply<AbductedPower>(enemy, 1, Owner.Creature, null);

        if (orbCount > 0)
            await DamageCmd.Attack((decimal)orbCount)
                .FromOsty(Owner.Creature, null)
                .TargetingAllOpponents(state)
                .Unpowered()
                .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
                .Execute(ctx);

        return [];
    }
}
