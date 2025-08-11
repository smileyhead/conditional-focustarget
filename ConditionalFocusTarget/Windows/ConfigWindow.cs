using System;
using System.Numerics;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Windowing;

namespace ConditionalFocusTarget.Windows;

public class ConfigWindow : Window, IDisposable
{
    private Configuration Configuration;

    // We give this window a constant ID using ###.
    // This allows for labels to be dynamic, like "{FPS Counter}fps###XYZ counter window",
    // and the window ID will always be "###XYZ counter window" for ImGui
    public ConfigWindow(Plugin plugin) : base("A Wonderful Configuration Window###With a constant ID")
    {
        Flags = ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar |
                ImGuiWindowFlags.NoScrollWithMouse;

        Size = new Vector2(232, 90);
        SizeCondition = ImGuiCond.Always;

        Configuration = plugin.Configuration;
    }

    public void Dispose() { }

    public override void Draw()
    {
        // Can't ref a property, so use a local copy
        var movable = Configuration.IsConfigWindowMovable;
        if (ImGui.Checkbox("Movable Config Window", ref movable))
        {
            Configuration.IsConfigWindowMovable = movable;
            Configuration.Save();
        }

        var lang = (int)Configuration.LanguageState;
        if (ImGui.Combo(Plugin.Loc.Strings.settings.lang_dropdown.label, ref lang, Configuration.LangsToImGuiList()))
        {
            Configuration.LanguageState = (LangState)lang;
            Configuration.Save();
        }

        ImGui.Text(Plugin.Loc.Strings.settings.lang_dropdown.crowdinNotePre); ImGui.SameLine();
        if (ImGui.Button(Plugin.Loc.Strings.settings.lang_dropdown.crowdinLink))
            Dalamud.Utility.Util.OpenLink("INSERT CROWDIN LINK HERE"); ImGui.SameLine();
        ImGui.Text(Plugin.Loc.Strings.settings.lang_dropdown.crowdinNotePost);

        ImGui.Text(Plugin.Loc.Strings.settings.chat_annouce.title);
        var announce = (int)Configuration.AnnounceFocusChange;
        if (ImGui.RadioButton(Plugin.Loc.Strings.settings.chat_annouce.do_not, ref announce, 0))
        {
            Configuration.AnnounceFocusChange = (AnnounceState)announce;
            Configuration.Save();
        }
        if (ImGui.RadioButton(Plugin.Loc.Strings.settings.chat_annouce.do_changed, ref announce, 1))
        {
            Configuration.AnnounceFocusChange = (AnnounceState)announce;
            Configuration.Save();
        }
        if (ImGui.RadioButton(Plugin.Loc.Strings.settings.chat_annouce.do_unchanged, ref announce, 2))
        {
            Configuration.AnnounceFocusChange = (AnnounceState)announce;
            Configuration.Save();
        }

        var mute_pvp = Configuration.AnnounceDisableInPvp;
        if (ImGui.Checkbox(Plugin.Loc.Strings.settings.chat_annouce.do_not_pvp, ref mute_pvp))
        {
            Configuration.AnnounceDisableInPvp = mute_pvp;
            Configuration.Save();
        }
    }
}
