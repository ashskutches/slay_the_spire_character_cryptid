using Godot;

namespace Cryptid.CryptidCode.Extensions;

public static class StringExtensions
{
    public static string ImagePath(this string path) =>
        Path.Join(MainFile.ResPath, "images", path);

    public static string CardImagePath(this string path)
    {
        path = Path.Join(MainFile.ResPath, "images", "card_portraits", path);
        if (ResourceLoader.Exists(path)) return path;
        return Path.Join(MainFile.ResPath, "images", "card_portraits", "card.png");
    }

    public static string BigCardImagePath(this string path)
    {
        path = Path.Join(MainFile.ResPath, "images", "card_portraits", "big", path);
        if (ResourceLoader.Exists(path)) return path;
        return Path.Join(MainFile.ResPath, "images", "card_portraits", "big", "card.png");
    }

    public static string PowerImagePath(this string path)
    {
        path = Path.Join(MainFile.ResPath, "images", "powers", path);
        if (ResourceLoader.Exists(path)) return path;
        return Path.Join(MainFile.ResPath, "images", "powers", "power.png");
    }

    public static string BigPowerImagePath(this string path)
    {
        path = Path.Join(MainFile.ResPath, "images", "powers", "big", path);
        if (ResourceLoader.Exists(path)) return path;
        return Path.Join(MainFile.ResPath, "images", "powers", "big", "power.png");
    }

    public static string RelicImagePath(this string path)
    {
        path = Path.Join(MainFile.ResPath, "images", "relics", path);
        if (ResourceLoader.Exists(path)) return path;
        return Path.Join(MainFile.ResPath, "images", "relics", "relic.png");
    }

    public static string BigRelicImagePath(this string path)
    {
        path = Path.Join(MainFile.ResPath, "images", "relics", "big", path);
        if (ResourceLoader.Exists(path)) return path;
        return Path.Join(MainFile.ResPath, "images", "relics", "big", "relic.png");
    }

    public static string CharacterUiPath(this string path) =>
        Path.Join(MainFile.ResPath, "images", "charui", path);
}
