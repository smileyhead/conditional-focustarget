# Sticky Focus Target

<p align="center">
  <img src="Assets/icon.png" alt="Dalamud" width="200"/>
</p>

Sticky Focus Target is a plugin for [Dalamud](https://github.com/goatcorp/Dalamud).

_(PvE-only.)_ Adds the /stickyfocustarget command, which focuses your current
target, as long as you do not already have a focus target set.

Have you ever felt focusing was under-utilised? Wanted to use it without a
dedicated keybind? Do you not even know what focus targeting is?
You've come to the right place.

You can add this command to your Attack Nearest macro and ensure that the
first enemy you target will also become focused, and stay that way, until it dies.
With this, you can focus enemies without having to consciously press a keybind,
and ensure that, for instance, when a focused boss spawns an add and you target it,
the boss' focus will NOT get overriden.

This plugin does not change the behaviour of the stock /focustarget command,
and must be triggered either manually or by adding it to a macro.
Placeholders are supported.

<table>
<tr>
<td>
<img src="Assets/focustarget.gif" alt="" />
<p>Attack Nearest macro with /focustarget</p>
</td>
<td>
<img src="Assets/stickyfocustarget.gif" alt="" />
<p>Attack Nearest macro with /stickyfocustarget</p>
</td>
</tr>
</table>

## Localisation Contribution

If you would like to supply a localisation to this plugin, you have one of two options:

### Option 1: Submit through GitHub *(preferred)*

If you are comfortable using GitHub's systems and editing .resx resource files, please use this method.

1. Fork this repository
2. Use your favourite editor to add a new culture to `StickyFocusTarget/Localisation/Loc.resx`, and translate the strings.
   - Alternatively, if you would prefer working with raw files instead, create a copy of `Loc.hu.resx`, replacing `hu` with your language's two-letter ISO 639 code (the ‘Set 1’ column in the table found [here](https://en.wikipedia.org/wiki/List_of_ISO_639_language_codes#Table)). You can find the English-language strings in `Loc.resx`.
3. Commit these changes to your fork and open a pull request against this repository.

### Option 2: Submit through Google Sheets
1. Contact me through any of the options listed on my [profile](https://github.com/smileyhead), or by tagging me in the [#plugin-translations channel](https://canary.discord.com/channels/581875019861328007/837457695337873428) of the XIVLauncher & Dalamud Discord server.
2. Give me your email address so that I can add you to a Google Sheets document.
3. Make your edits there and tell me when you are done.
4. I will manually import your work to the plugin.
