using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Cards;

// Exhaust a card from your hand. Gain 2 Paranormal. Upgrade: also draw 1.
public sealed class ScrambledSignal : CryptidCard
{
    public ScrambledSignal() : base(0, CardType.Skill, CardRarity.Common, TargetType.Self) { }

    private bool _draw = false;

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        var toExhaust = Owner.PlayerCombatState?.Hand.Cards.FirstOrDefault(c => c != this);
        if (toExhaust != null)
            await CardCmd.Exhaust(ctx, toExhaust, false, true);
        await PowerCmd.Apply<ParanormalPower>(Owner.Creature, 2, Owner.Creature, this);
        if (_draw)
            await CardPileCmd.Draw(ctx, Owner);
    }

    protected override void OnUpgrade() => _draw = true;
}
