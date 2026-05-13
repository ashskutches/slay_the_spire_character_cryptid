using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Cards;

// Remove all debuffs from SELF. Gain 1 Paranormal per debuff removed. Upgrade: also draw 1 card.
public sealed class MenInBlack : CryptidCard
{
    public MenInBlack() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self) { }

    private bool _draw = false;

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        var debuffs = Owner.Creature.Powers.Where(p => p.Type == PowerType.Debuff).ToList();
        foreach (var debuff in debuffs)
            await PowerCmd.Remove(debuff);
        if (debuffs.Count > 0)
            await PowerCmd.Apply<ParanormalPower>(Owner.Creature, debuffs.Count, Owner.Creature, this);
        if (_draw)
            await CardPileCmd.Draw(ctx, Owner);
    }

    protected override void OnUpgrade() => _draw = true;
}
