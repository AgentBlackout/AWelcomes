# AWelcomes
A basic MOTD system for SCP, displays in broadcasts across the top of the screen as soon as users finish authentication.

This mod makes use of [Atlas](https://gitlab.com/Androxanik/atlas/) for [SCP:SL](https://scpslgame.com/)

## Configuration
It's fairly easy to use, simply create a template, and assign role names to it in the config.

**Example template**:
```json
{
    "message_map": {
        "default": {
            "content":"This is a config message for everyone users",
            "period": 5,
        },
        "default2": {
            "content":"This is the second part of the message <color=red>You can use structured text</color>",
            "period":5
        },
        "admin": {
            "content":"You are an administrator",
            "period": 5
        }
    },
    "role_map": {
        "*": ["default","default2"],
        "admin": ["admin"]
    }
}
```
If you use a wildcard (`*`), it will show for everyone, using `""` with apply only to users with no role text.

This works on badges which are hidden by default.

The text in-game uses [TextMeshPro](https://assetstore.unity.com/packages/essentials/beta-projects/textmesh-pro-84126), which allows you to do Rich Text options, see [here](http://digitalnativestudios.com/textmeshpro/docs/rich-text/) for more options.

## Installation
Follow the [Atlas](https://gitlab.com/Androxanik/atlas/) installation guide for SCP:SL, then simply drop the DLL into the mods folder.
After the mod is loaded, simply run `atlas settings save` to flush the settings to disk, from there you can modify them to your heart's desires and use `atlas settings refresh` to update them in memory. 

#### FAQ

You can add delays in by putting a blank broadcast with a given period.

I have had no issues with this, but I'm sure there will be SL themed edge cases, please contact me if you have any issues.

**If you have any other concerns, you can find me in the Atlas Discord, linked on the page (@MASONIC#3992).**