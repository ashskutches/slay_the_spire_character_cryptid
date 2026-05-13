using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Cryptid.CryptidCode.Cards;

public sealed class HideInPlainSight : CryptidCard
{
    public HideInPlainSight() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new BlockVar(8m, ValueProp.Move),
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        Owner.Creature.GainBlockInternal(DynamicVars.Block.IntValue);
        // SkillsPlayedThisTurn already includes this card since BeforeCardPlayed fires first
        int paranormalGained = SkillsPlayedThisTurn;
        if (paranormalGained > 0)
            await PowerCmd.Apply<ParanormalPower>(Owner.Creature, paranormalGained, Owner.Creature, this);
    }

    protected override void OnUpgrade() => DynamicVars.Block.UpgradeValueBy(4m);
}
