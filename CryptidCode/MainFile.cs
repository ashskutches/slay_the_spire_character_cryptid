using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;

namespace Cryptid.CryptidCode;

[ModInitializer(nameof(Initialize))]
public partial class MainFile : Node
{
    public const string ModId = "Cryptid";
    public const string ResPath = $"res://{ModId}";
    public const string Version = "1.3.0";

    public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } = new(ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);

    public static void Initialize()
    {
        Harmony harmony = new(ModId);
        harmony.PatchAll();
    }
}
