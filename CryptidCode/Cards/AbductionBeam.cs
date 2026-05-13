using Cryptid.CryptidCode.Orbs;
using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Cryptid.CryptidCode.Cards;

public sealed class AbductionBeam : CryptidCard
{
    public AbductionBeam() : base(1, CardType.Skill, CardRarity.Basic, TargetType.AnyEnemy) { }

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    private bool _upgraded = false;

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<AbductedPower>(play.Target, 1, Owner.Creature, this);
        await OrbCmd.Channel<GrayAlienOrb>(ctx, Owner);
    }

    protected override void OnUpgrade() => _upgraded = true;
}
