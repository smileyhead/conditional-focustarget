using Dalamud.Configuration;
using System;
using System.Globalization;
using StickyFocusTarget.Localisation;

namespace StickyFocusTarget;

[Serializable]
public class Configuration : IPluginConfiguration
{
    public int Version { get; set; } = 0;
    public int LanguageState { get; set; } = 0; //0 == Automatic. More lang codes in Localisation.cs
    public AnnounceState AnnounceFocusChange { get; set; } = AnnounceState.Off;
    public bool AnnounceDisableInPvp { get; set; } = true;

    public static void AutoSetCulture()
    {
        Loc.Culture = CultureInfo.GetCultureInfo(Plugin.PluginInterface.UiLanguage);
        Plugin.Log.Info($"Culture auto-set to {Loc.Culture.Name}");
    }

    // The below exist just to make saving less cumbersome
    public void Save()
    {
        Plugin.PluginInterface.SavePluginConfig(this);
    }
}
public enum AnnounceState { Off, OnChange, OnNoChange }
