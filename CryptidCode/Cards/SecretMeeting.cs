using Cryptid.CryptidCode.Orbs;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Cards;

// 0-cost Skill Rare. If you have a Gray Alien, Eldritch, and Cryptid orb channeled: gain 3 energy and draw 3 cards. Exhaust. Upgrade: Retain instead.
public sealed class SecretMeeting : CryptidCard
{
    public SecretMeeting() : base(0, CardType.Skill, CardRarity.Rare, TargetType.Self) { }

    private bool _retain = false;

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        var orbs = Owner.PlayerCombatState?.OrbQueue.Orbs ?? [];
        bool hasAll = orbs.Any(o => o is GrayAlienOrb)
                   && orbs.Any(o => o is EldritchOrb)
                   && orbs.Any(o => o is CryptidOrb);
        if (hasAll)
        {
            Owner.PlayerCombatState?.GainEnergy(3m);
            await CardPileCmd.Draw(ctx, Owner);
            await CardPileCmd.Draw(ctx, Owner);
            await CardPileCmd.Draw(ctx, Owner);
        }
        if (_retain)
            GiveSingleTurnRetain();
        else
            await CardCmd.Exhaust(ctx, this, false, false);
    }

    protected override void OnUpgrade() => _retain = true;
}
