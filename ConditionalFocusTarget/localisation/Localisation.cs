using ConditionalFocusTarget;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

public class Localisation
{
    public Root Strings { get; set; }

    public Localisation(string langCode)
    {
        string jsonPath = $"localisation{Path.PathSeparator}{langCode}.json";

        string jsonText = File.ReadAllText(jsonPath);
        Strings = JsonSerializer.Deserialize<Root>(jsonText);
    }
}

public class Announcement
{
    public string changed { get; set; }
    public string unchanged { get; set; }
    public string unchanged_pvp { get; set; }

    public string Get(int key = 2)
    {
        switch (key)
        {
            case 0: return changed;
            case 1: return unchanged;
            default: return unchanged_pvp;
        }
    }
}

public class ChatAnnouce
{
    public string title { get; set; }
    public string do_not { get; set; }
    public string do_changed { get; set; }
    public string do_unchanged { get; set; }
    public string do_not_pvp { get; set; }
}

public class CommandDescription
{
    public string @short { get; set; }
    public string @long { get; set; }
}

public class LangDropdown
{
    public string label { get; set; }
    public string auto { get; set; }
    public string crowdinNote { get; set; }
    public string crowdinLink { get; set; }
}

public class Root
{
    public CommandDescription command_description { get; set; }
    public Settings settings { get; set; }
    public Announcement announcement { get; set; }
}

public class Settings
{
    public LangDropdown lang_dropdown { get; set; }
    public ChatAnnouce chat_annouce { get; set; }
}
