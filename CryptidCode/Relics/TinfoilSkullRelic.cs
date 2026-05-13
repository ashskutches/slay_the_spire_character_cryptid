using BaseLib.Abstracts;
using BaseLib.Utils;
using Cryptid.CryptidCode.Character;
using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace Cryptid.CryptidCode.Relics;

[Pool(typeof(CryptidRelicPool))]
public sealed class TinfoilSkullRelic : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Starter;
    public override bool IsAllowed(IRunState runState) => false;
    public override bool ShouldReceiveCombatHooks => true;

    private bool _triggeredThisTurn = false;

    public override Task BeforeTurnEnd(PlayerChoiceContext choiceContext, MegaCrit.Sts2.Core.Combat.CombatSide side)
    {
        if (side == MegaCrit.Sts2.Core.Combat.CombatSide.Player)
            _triggeredThisTurn = false;
        return Task.CompletedTask;
    }

    public override async Task AfterDamageReceived(
        PlayerChoiceContext ctx,
        Creature target,
        DamageResult result,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource)
    {
        if (target?.IsPlayer != true) return;
        if (dealer?.IsPlayer == true) return;
        if (result.TotalDamage <= 0) return;
        if (_triggeredThisTurn) return;
        _triggeredThisTurn = true;
        Flash();
        await PowerCmd.Apply<ParanormalPower>(target, 3, target, null);
    }
}
