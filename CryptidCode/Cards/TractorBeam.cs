using Cryptid.CryptidCode.Orbs;
using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Cards;

public sealed class TractorBeam : CryptidCard
{
    public TractorBeam() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies) { }

    private int _orbsToChannel = 1;

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        var state = ActiveCombatState;
        if (state != null)
            foreach (var enemy in state.Enemies.ToList())
                await PowerCmd.Apply<AbductedPower>(enemy, 1, Owner.Creature, this);
        for (int i = 0; i < _orbsToChannel; i++)
            await OrbCmd.Channel<GrayAlienOrb>(ctx, Owner);
    }

    protected override void OnUpgrade() => _orbsToChannel = 2;
}
