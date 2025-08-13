using System.Globalization;
using Dalamud.Game.ClientState.Objects;
using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.UI.Misc;
using StickyFocusTarget.Localisation;
using StickyFocusTarget.Windows;

namespace StickyFocusTarget;

public sealed class Plugin : IDalamudPlugin
{
    private const string CommandName = "/stickyfocustarget";
    private const string CommandNameShort = "/sft";

    public readonly WindowSystem WindowSystem = new("StickyFocusTarget");

    public Plugin()
    {
        Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();

        if (Configuration.LanguageState == 0) Configuration.AutoSetCulture();
        else
        {
            Loc.Culture =
                CultureInfo.GetCultureInfo(LocalisationUtilities.PluginSupportedLanguages[Configuration.LanguageState]);
        }

        ConfigWindow = new ConfigWindow(this);

        WindowSystem.AddWindow(ConfigWindow);

        CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
        {
            HelpMessage = Loc.command_description_long.ToString(CultureInfo.GetCultureInfo("en"))
        });
        CommandManager.AddHandler(CommandNameShort, new CommandInfo(OnCommand)
        {
            HelpMessage = Loc.command_description_short.ToString(CultureInfo.GetCultureInfo("en")),
            ShowInHelp = false
        });

        PluginInterface.UiBuilder.Draw += DrawUI;

        // This adds a button to the plugin installer entry of this plugin which allows
        // toggling the display status of the configuration ui
        PluginInterface.UiBuilder.OpenConfigUi += ToggleConfigUI;
    }

    [PluginService] internal static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
    [PluginService] internal static ICommandManager CommandManager { get; private set; } = null!;
    [PluginService] internal static IClientState ClientState { get; private set; } = null!;
    [PluginService] internal static IChatGui ChatGui { get; private set; } = null!;
    [PluginService] internal static ITargetManager TargetManager { get; private set; } = null!;
    [PluginService] internal static IObjectTable ObjectTable { get; private set; } = null!;
    [PluginService] internal static IPluginLog Log { get; private set; } = null!;

    public Configuration Configuration { get; init; }
    private ConfigWindow ConfigWindow { get; init; }

    public void Dispose()
    {
        WindowSystem.RemoveAllWindows();

        ConfigWindow.Dispose();

        CommandManager.RemoveHandler(CommandName);
    }

    private unsafe void OnCommand(string command, string args)
    {
        if (ClientState.IsPvP)
        {
            if (!Configuration.AnnounceDisableInPvp)
                ChatGui.Print(Loc.announcement_unchanged_pvp);
            return;
        }

        var focusTarget = TargetManager.FocusTarget;

        if (focusTarget == null || focusTarget.IsDead)
        {
            if (args.Length > 0)
            {
                var phTarget = PronounModule.Instance()->ResolvePlaceholder(args, 0, 0);
                TargetManager.FocusTarget = ObjectTable.CreateObjectReference((nint)phTarget);
            }
            else TargetManager.FocusTarget = TargetManager.Target;

            if (Configuration.AnnounceFocusChange == AnnounceState.OnChange)
                ChatGui.Print(Loc.announcement_changed);
            return;
        }

        if (Configuration.AnnounceFocusChange == AnnounceState.OnNoChange)
            ChatGui.Print(Loc.announcement_unchanged);
    }

    private void DrawUI()
    {
        WindowSystem.Draw();
    }

    public void ToggleConfigUI()
    {
        ConfigWindow.Toggle();
    }
}
