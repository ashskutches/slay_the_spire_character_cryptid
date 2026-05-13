using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Cryptid.CryptidCode.Cards;

public sealed class CosmicDownload : CryptidCard
{
    public CosmicDownload() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self) { }

    private int _draws = 1;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<ParanormalPower>(3m),
    ];

    protected override async Task OnPlay(PlayerChoiceContext ctx, CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<ParanormalPower>(Owner.Creature, DynamicVars["ParanormalPower"].IntValue, Owner.Creature, this);
        if (EvokedThisTurn)
        {
            for (int i = 0; i < _draws; i++)
                await CardPileCmd.Draw(ctx, Owner);
        }
    }

    protected override void OnUpgrade() => _draws = 2;
}
