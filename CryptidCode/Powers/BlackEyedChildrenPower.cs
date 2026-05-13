using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Powers;

// Amount = 1 base, 2 upgraded. Gain Amount Paranormal each time an Entity is Evoked.
public class BlackEyedChildrenPower : CryptidPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task AfterOrbEvoked(PlayerChoiceContext ctx, OrbModel orb, IEnumerable<Creature> targets)
    {
        Flash();
        await PowerCmd.Apply<ParanormalPower>(Owner, Amount, Owner, null);
    }
}
