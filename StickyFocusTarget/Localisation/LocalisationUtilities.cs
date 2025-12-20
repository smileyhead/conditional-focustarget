using System.Collections.Generic;
using System.Globalization;

namespace StickyFocusTarget.Localisation;

public class LocalisationUtilities
{
    public static readonly List<string> PluginSupportedLanguages = ["auto", "en", "hu", "ja"];

    public static List<string> GenerateLanguageList()
    {
        List<string> languageList = [Loc.settings_lang_dropdown_auto];

        for (var i = 1; i < PluginSupportedLanguages.Count; i++)
            languageList.Add(CultureInfo.GetCultureInfo(PluginSupportedLanguages[i]).NativeName);

        return languageList;
    }
}
