using ConditionalFocusTarget.Windows;
using Dalamud.Game.ClientState.Objects;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace ConditionalFocusTarget;

public sealed class Plugin : IDalamudPlugin
{
    [PluginService] internal static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
    [PluginService] internal static ITextureProvider TextureProvider { get; private set; } = null!;
    [PluginService] internal static ICommandManager CommandManager { get; private set; } = null!;
    [PluginService] internal static IClientState ClientState { get; private set; } = null!;
    [PluginService] internal static IDataManager DataManager { get; private set; } = null!;
    [PluginService] internal static ITargetManager TargetManager { get; private set; } = null!;
    [PluginService] internal static IPluginLog Log { get; private set; } = null!;

    private const string CommandName = "/conditionalfocustarget";
    private const string CommandNameShort = "/cft";

    public static Localisation Loc { get; set; }

    public Configuration Configuration { get; init; }

    public readonly WindowSystem WindowSystem = new("ConditionalFocusTarget");
    private ConfigWindow ConfigWindow { get; init; }

    public Plugin()
    {
        Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();

        ConfigWindow = new ConfigWindow(this);

        Loc = new(Configuration.LanguageState.ToString());

        WindowSystem.AddWindow(ConfigWindow);

        CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
        {
            HelpMessage = Loc.Strings.command_description.@long
        });
        CommandManager.AddHandler(CommandNameShort, new CommandInfo(OnCommand)
        {
            HelpMessage = Loc.Strings.command_description.@short,
            ShowInHelp = false
        });

        PluginInterface.UiBuilder.Draw += DrawUI;

        // This adds a button to the plugin installer entry of this plugin which allows
        // toggling the display status of the configuration ui
        PluginInterface.UiBuilder.OpenConfigUi += ToggleConfigUI;
    }

    public void Dispose()
    {
        WindowSystem.RemoveAllWindows();

        ConfigWindow.Dispose();

        CommandManager.RemoveHandler(CommandName);
    }

    private void OnCommand(string command, string args)
    {
        if (ClientState.IsPvP)
        {
            if (!Configuration.AnnounceDisableInPvp) CommandManager.ProcessCommand($"/echo {Loc.Strings.announcement.unchanged_pvp}");
            return;
        }

        IGameObject? focusTarget = TargetManager.FocusTarget;

        if (focusTarget == null || focusTarget.IsDead)
        {
            CommandManager.ProcessCommand("/focustarget");
            if (Configuration.AnnounceFocusChange == AnnounceState.onChange) CommandManager.ProcessCommand($"/echo {Loc.Strings.announcement.changed}");
            return;
        }

        if (Configuration.AnnounceFocusChange == AnnounceState.onNoChange) CommandManager.ProcessCommand($"/echo {Loc.Strings.announcement.unchanged}");
    }

    private void DrawUI() => WindowSystem.Draw();

    public void ToggleConfigUI() => ConfigWindow.Toggle();
}
