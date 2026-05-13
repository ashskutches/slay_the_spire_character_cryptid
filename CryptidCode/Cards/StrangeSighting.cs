using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Cards;

// Consume 5 Paranormal. Gain a random Entity orb and 5 Block.
public sealed class StrangeSighting : CryptidCard
{
    public StrangeSighting() : base(1, CardType.Skill, CardRarity.Basic, TargetType.Self) { }

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);

        var paranormal = Owner.Creature.GetPower<ParanormalPower>();
        if (paranormal != null)
        {
            int remaining = paranormal.Amount - 5;
            await PowerCmd.Remove(paranormal);
            if (remaining > 0)
                await PowerCmd.Apply<ParanormalPower>(Owner.Creature, remaining, Owner.Creature, this);
        }

        Owner.Creature.GainBlockInternal(5);
        await ChannelRandomEntity(ctx, Owner);
    }

    protected override void OnUpgrade() { }
}
