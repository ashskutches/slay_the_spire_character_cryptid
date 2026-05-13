using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Cryptid.CryptidCode.Cards;

// Spend ALL Paranormal. Gain Intangible 1 (or 2 if 8+ spent). Exhaust.
public sealed class LochNessMonster : CryptidCard
{
    public LochNessMonster() : base(3, CardType.Skill, CardRarity.Rare, TargetType.Self) { }

    private int _threshold = 8;

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);

        var paranormal = Owner.Creature.GetPower<ParanormalPower>();
        int spent = paranormal?.Amount ?? 0;
        if (paranormal != null)
            await PowerCmd.Remove(paranormal);

        int intangibleAmount = spent >= _threshold ? 2 : 1;
        await PowerCmd.Apply<IntangiblePower>(Owner.Creature, intangibleAmount, Owner.Creature, this);

        await CardCmd.Exhaust(ctx, this, false, false);
    }

    protected override void OnUpgrade() => _threshold = 5;
}
