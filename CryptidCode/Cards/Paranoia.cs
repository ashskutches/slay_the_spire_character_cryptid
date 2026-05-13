using Cryptid.CryptidCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Cryptid.CryptidCode.Cards;

public sealed class Paranoia : CryptidCard
{
    public Paranoia() : base(0, CardType.Curse, CardRarity.Curse, TargetType.None) { }

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (!ReferenceEquals(card, this) || Owner?.Creature == null) return;
        await PowerCmd.Apply<VulnerablePower>(Owner.Creature, 1, Owner.Creature, this);
    }

    protected override Task OnPlay(PlayerChoiceContext ctx, CardPlay play) => Task.CompletedTask;

    protected override void OnUpgrade() { }
}
