using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Cryptid.CryptidCode.Cards;

// Apply 2 Madness to ALL enemies. Upgrade: also draw 1 card.
public sealed class EldritchRitual : CryptidCard
{
    public EldritchRitual() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies) { }

    private bool _draw = false;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<MadnessPower>(2m),
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        var state = ActiveCombatState;
        if (state != null)
            foreach (var enemy in state.Enemies.ToList())
                await PowerCmd.Apply<MadnessPower>(enemy, DynamicVars["MadnessPower"].IntValue, Owner.Creature, this);
        if (_draw)
            await CardPileCmd.Draw(ctx, Owner);
    }

    protected override void OnUpgrade() => _draw = true;
}
