using System;
using System.Globalization;
using System.Numerics;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Windowing;
using Dalamud.Utility;
using StickyFocusTarget.Localisation;

namespace StickyFocusTarget.Windows;

public class ConfigWindow : Window, IDisposable
{
    private readonly Configuration Configuration;

    // We give this window a constant ID using ###.
    // This allows for labels to be dynamic, like "{FPS Counter}fps###XYZ counter window",
    // and the window ID will always be "###XYZ counter window" for ImGui
    public ConfigWindow(Plugin plugin) : base("Sticky Focus Target###CFTConfigWindow")
    {
        Flags = ImGuiWindowFlags.AlwaysAutoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar |
                ImGuiWindowFlags.NoScrollWithMouse;

        Size = new Vector2(0, 0);
        SizeCondition = ImGuiCond.Always;

        Configuration = plugin.Configuration;
    }

    public void Dispose() { }

    public override void Draw()
    {
        var langSetting = Configuration.LanguageState;
        ImGui.TextUnformatted(Loc.settings_lang_dropdown_label);
        ImGui.SameLine();
        if (ImGui.Combo("", ref langSetting, LocalisationUtilities.GenerateLanguageList()))
        {
            Configuration.LanguageState = langSetting;
            Configuration.Save();
            if (langSetting == 0) Configuration.AutoSetCulture();
            else Loc.Culture = CultureInfo.GetCultureInfo(LocalisationUtilities.PluginSupportedLanguages[langSetting]);
        }

        ImGui.SameLine();

        var githubLink = "https://github.com/smileyhead/conditional-focustarget/blob/master/README.md#localisation-contribution";
        if (ImGui.Button(Loc.settings_lang_contribute_button))
            Util.OpenLink(githubLink);
        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(Loc.settings_lang_contribute_tooltip.Replace("{githubLink}", githubLink));

        ImGui.Separator();
        ImGui.Spacing();

        ImGui.Text(Loc.settings_chat_announce_title);
        var announce = (int)Configuration.AnnounceFocusChange;
        if (ImGui.RadioButton(Loc.settings_chat_announce_do_not, ref announce, 0))
        {
            Configuration.AnnounceFocusChange = (AnnounceState)announce;
            Configuration.Save();
        }

        if (ImGui.RadioButton(Loc.settings_chat_announce_yes_if_changed, ref announce, 1))
        {
            Configuration.AnnounceFocusChange = (AnnounceState)announce;
            Configuration.Save();
        }

        if (ImGui.RadioButton(Loc.settings_chat_announce_yes_if_not_changed, ref announce, 2))
        {
            Configuration.AnnounceFocusChange = (AnnounceState)announce;
            Configuration.Save();
        }

        ImGui.Spacing();

        if (Configuration.AnnounceFocusChange.ToString().StartsWith("On"))
        {
            var mutePvp = Configuration.AnnounceDisableInPvp;
            if (ImGui.Checkbox(Loc.settings_disable_in_pvp, ref mutePvp))
            {
                Configuration.AnnounceDisableInPvp = mutePvp;
                Configuration.Save();
            }
        }

        ImGui.Separator();
        ImGui.Spacing();

        ImGui.TextUnformatted(Loc.settings_commands_title);
        ImGui.TextUnformatted("/stickyfocustarget: ");
        ImGui.SameLine();
        ImGui.TextWrapped(Loc.command_description_long);
    }
}
