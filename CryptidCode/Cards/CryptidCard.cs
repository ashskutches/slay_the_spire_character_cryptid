using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Cryptid.CryptidCode.Character;
using Cryptid.CryptidCode.Extensions;
using Cryptid.CryptidCode.Orbs;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Cards;

[Pool(typeof(CryptidCardPool))]
public abstract class CryptidCard(int cost, CardType type, CardRarity rarity, TargetType target)
    : CustomCardModel(cost, type, rarity, target)
{
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string BetaPortraitPath => $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

    protected static CombatState? ActiveCombatState { get; private set; }

    public override Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
    {
        ActiveCombatState = combatState;
        return Task.CompletedTask;
    }

    protected static async Task ChannelRandomEntity(PlayerChoiceContext ctx, Player owner)
    {
        switch (Random.Shared.Next(3))
        {
            case 0: await OrbCmd.Channel<AlienOrb>(ctx, owner); break;
            case 1: await OrbCmd.Channel<GhostOrb>(ctx, owner); break;
            default: await OrbCmd.Channel<CryptidOrb>(ctx, owner); break;
        }
    }
}
