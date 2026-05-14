using BaseLib.Abstracts;
using BaseLib.Extensions;
using Cryptid.CryptidCode.Extensions;
using Cryptid.CryptidCode.Orbs;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Cryptid.CryptidCode.Powers;

public abstract class CryptidPower : CustomPowerModel
{
    public override string CustomPackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
    public override string CustomBigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();

    // Shared state cache — updated at the start of every turn.
    internal static CombatState? ActiveCombatState { get; private set; }
    internal static PlayerChoiceContext? ActiveContext { get; private set; }
    internal static Player? ActivePlayer { get; private set; }

    public override Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
    {
        ActiveCombatState = combatState;
        ActiveContext = choiceContext;
        ActivePlayer = player;
        return Task.CompletedTask;
    }

    protected static async Task ChannelRandomEntity(PlayerChoiceContext ctx, Player player)
    {
        switch (Random.Shared.Next(3))
        {
            case 0: await OrbCmd.Channel<AlienOrb>(ctx, player); break;
            case 1: await OrbCmd.Channel<GhostOrb>(ctx, player); break;
            default: await OrbCmd.Channel<CryptidOrb>(ctx, player); break;
        }
    }
}
