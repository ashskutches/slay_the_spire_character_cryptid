using BaseLib.Abstracts;
using BaseLib.Utils;
using BaseLib.Utils.NodeFactories;
using Cryptid.CryptidCode.Cards;
using Cryptid.CryptidCode.Extensions;
using Cryptid.CryptidCode.Relics;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace Cryptid.CryptidCode.Character;

public class CryptidCharacter : PlaceholderCharacterModel
{
    public const string CharacterId = "CryptidCharacter";

    public static readonly Color Color = new("22BB66");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Neutral;
    public override int StartingHp => 70;
    public override int BaseOrbSlotCount => 3;

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<Probe>(),
        ModelDb.Card<Probe>(),
        ModelDb.Card<Probe>(),
        ModelDb.Card<Probe>(),
        ModelDb.Card<Judgement>(),
        ModelDb.Card<Judgement>(),
        ModelDb.Card<Judgement>(),
        ModelDb.Card<Judgement>(),
        ModelDb.Card<StrangeSighting>(),
        ModelDb.Card<HisPresence>(),
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<TinfoilSkullRelic>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<CryptidCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<CryptidRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<CryptidPotionPool>();

    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }

    public override NCreatureVisuals CreateCustomVisuals() =>
        NodeFactory<NCreatureVisuals>.CreateFromResource("combat_idle.png".CharacterUiPath());

    public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();
}
