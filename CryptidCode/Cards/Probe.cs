using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Cryptid.CryptidCode.Cards;

public sealed class Probe : CryptidCard
{
    public Probe() : base(0, CardType.Skill, CardRarity.Basic, TargetType.Self) { }

    private bool _draw = false;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<ParanormalPower>(3m),
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<ParanormalPower>(Owner.Creature, DynamicVars["ParanormalPower"].IntValue, Owner.Creature, this);
        if (_draw)
            await CardPileCmd.Draw(ctx, Owner);
    }

    protected override void OnUpgrade() => _draw = true;
}
