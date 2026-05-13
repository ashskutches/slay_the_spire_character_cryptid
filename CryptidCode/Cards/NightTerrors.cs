using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Cards;

// Curse. At the end of your turn, lose 1 Paranormal.
public sealed class NightTerrors : CryptidCard
{
    public NightTerrors() : base(-2, CardType.Curse, CardRarity.Curse, TargetType.Self) { }

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    public override async Task BeforeTurnEnd(PlayerChoiceContext ctx, CombatSide side)
    {
        if (side != CombatSide.Player) return;
        var paranormal = Owner?.Creature?.GetPower<ParanormalPower>();
        if (paranormal != null)
            await PowerCmd.Decrement(paranormal);
    }

    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play) => Task.CompletedTask;
    protected override void OnUpgrade() { }
}
