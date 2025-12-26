using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StickyFocusTarget.Localisation;

public class LocalisationUtilities
{
    public static readonly List<string> PluginSupportedLanguages = ["auto", "en", "hu", "ja"];
    public static readonly List<string> EnglishLanguageNames = ["", "English", "Hungarian", "Japanese"];

    public static List<string> GenerateLanguageList()
    {
        List<string> languageList = 
            [$"{Loc.settings_lang_dropdown_auto} ({CultureInfo.GetCultureInfo(Plugin.PluginInterface.UiLanguage).NativeName})"];

        for (var i = 1; i < PluginSupportedLanguages.Count; i++)
            languageList.Add($"{EnglishLanguageNames[i]} ({CultureInfo.GetCultureInfo(PluginSupportedLanguages[i]).NativeName})");

        List<string> sortedLanguageList = languageList.Take(1).Concat(languageList.Skip(1).OrderBy(x => x)).ToList();
        return sortedLanguageList;
    }
}
