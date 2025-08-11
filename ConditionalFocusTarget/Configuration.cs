using Dalamud.Configuration;
using System;

namespace ConditionalFocusTarget;

[Serializable]
public class Configuration : IPluginConfiguration
{
    public int Version { get; set; } = 0;

    public bool IsConfigWindowMovable { get; set; } = true;
    public LangState LanguageState { get; set; } = LangState.auto;
    public AnnounceState AnnounceFocusChange { get; set; } = AnnounceState.off;
    public bool AnnounceDisableInPvp { get; set; } = false;

    // The below exist just to make saving less cumbersome
    public void Save()
    {
        Plugin.PluginInterface.SavePluginConfig(this);
    }
}

public enum LangState { auto, en, hu }
public enum AnnounceState { off, onChange, onNoChange }
