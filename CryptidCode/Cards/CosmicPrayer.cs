using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Cards;

// 2-cost Skill Uncommon. Evoke ALL channeled Entities, gaining 1 Paranormal each. Upgrade: also draw 1.
public sealed class CosmicPrayer : CryptidCard
{
    public CosmicPrayer() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

    private bool _draw = false;

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        int orbCount = Owner.PlayerCombatState?.OrbQueue.Orbs.Count ?? 0;
        for (int i = 0; i < orbCount; i++)
        {
            await OrbCmd.EvokeNext(ctx, Owner, true);
            await PowerCmd.Apply<ParanormalPower>(Owner.Creature, 1, Owner.Creature, this);
        }
        if (_draw)
            await CardPileCmd.Draw(ctx, Owner);
    }

    protected override void OnUpgrade() => _draw = true;
}
