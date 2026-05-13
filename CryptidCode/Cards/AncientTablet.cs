using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Cards;

// 1-cost Skill Rare. Your next Evoke triggers twice. Upgrade: costs 0.
public sealed class AncientTablet : CryptidCard
{
    public AncientTablet() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self) { }

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<AncientTabletPower>(Owner.Creature, 1, Owner.Creature, this);
    }

    protected override void OnUpgrade() => EnergyCost.SetCustomBaseCost(0);
}
