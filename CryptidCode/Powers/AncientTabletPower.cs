using Cryptid.CryptidCode.Orbs;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Powers;

// The next Evoke triggers twice. Consumed on use.
public class AncientTabletPower : CryptidPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    private bool _triggering = false;

    public override async Task AfterOrbEvoked(PlayerChoiceContext ctx, OrbModel orb, IEnumerable<Creature> targets)
    {
        if (_triggering) return;
        _triggering = true;
        Flash();
        await PowerCmd.Remove(this);
        // Channel a new orb of the same type, then immediately evoke it
        if (orb is GrayAlienOrb) await OrbCmd.Channel<GrayAlienOrb>(ctx, orb.Owner);
        else if (orb is EldritchOrb) await OrbCmd.Channel<EldritchOrb>(ctx, orb.Owner);
        else if (orb is CryptidOrb) await OrbCmd.Channel<CryptidOrb>(ctx, orb.Owner);
        else { _triggering = false; return; }
        await OrbCmd.EvokeLast(ctx, orb.Owner, true);
        _triggering = false;
    }
}
