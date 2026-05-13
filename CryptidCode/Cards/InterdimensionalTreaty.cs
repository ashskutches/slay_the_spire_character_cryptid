using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Cards;

public sealed class InterdimensionalTreaty : CryptidCard
{
    public InterdimensionalTreaty() : base(3, CardType.Power, CardRarity.Rare, TargetType.Self) { }

    private int _amount = 1;

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<InterdimensionalTreatyPower>(Owner.Creature, _amount, Owner.Creature, this);
    }

    protected override void OnUpgrade() => _amount = 2;
}
