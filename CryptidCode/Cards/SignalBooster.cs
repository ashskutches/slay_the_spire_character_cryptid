using Cryptid.CryptidCode.Orbs;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Cards;

// Channel 2 Cryptid orbs. Upgrade: also gain 1 orb slot.
public sealed class SignalBooster : CryptidCard
{
    public SignalBooster() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self) { }

    private bool _upgraded = false;

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await OrbCmd.Channel<CryptidOrb>(ctx, Owner);
        await OrbCmd.Channel<CryptidOrb>(ctx, Owner);
        if (_upgraded)
            await OrbCmd.AddSlots(Owner, 1);
    }

    protected override void OnUpgrade() => _upgraded = true;
}
