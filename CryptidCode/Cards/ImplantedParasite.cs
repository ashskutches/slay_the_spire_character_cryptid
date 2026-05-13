using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Cards;

public sealed class ImplantedParasite : CryptidCard
{
    public ImplantedParasite() : base(0, CardType.Curse, CardRarity.Curse, TargetType.None) { }

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (!ReferenceEquals(card, this) || Owner?.Creature == null) return;
        var paranormal = Owner.Creature.GetPower<ParanormalPower>();
        if (paranormal == null) return;
        int lost = Math.Min(paranormal.Amount, 3);
        int remaining = paranormal.Amount - lost;
        await PowerCmd.Remove(paranormal);
        if (remaining > 0)
            await PowerCmd.Apply<ParanormalPower>(Owner.Creature, remaining, Owner.Creature, this);
    }

    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play) => Task.CompletedTask;

    protected override void OnUpgrade() { }
}
