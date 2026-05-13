using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Cards;

// 0-cost Skill Uncommon. Evoke your next Entity. Upgrade: also draw 1 card.
public sealed class DimensionalShift : CryptidCard
{
    public DimensionalShift() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

    private bool _draw = false;

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        if (Owner.PlayerCombatState?.OrbQueue.Orbs.Count > 0)
            await OrbCmd.EvokeNext(ctx, Owner, true);
        if (_draw)
            await CardPileCmd.Draw(ctx, Owner);
    }

    protected override void OnUpgrade() => _draw = true;
}
