This repository contains all the code I have contributed to this game. To play the full game, please visit http://vncentwu.itch.io/soon.
Thank you.
-------------------------------------------------------------------------
Soon - a 2D combat-based platformer with 3D graphics in development.

Gameplay: The main resource is adrenaline, which decreases over periods of inactivity and regenerates upon dealing damage. Coupled with the teleport mechanic, the engaging combat experience allows for a very interactive gameplay. There is no objective yet, though I do have a plot lined up.

Video showing combat gameplay:
https://www.youtube.com/watch?feature=player_embedded&v=VyBJ-IUklBw

Controls:

    Left/Right - A/D
    Jump - Space
    Sprint - Shift
    Attack - Left Click
    Short tele - Q
    Portal tele - X
    Pick up item - Z
    Cast Fireball - 1
    Cast Lightning - 2
    Cast Fire Blast - 3
    Cast Ice Blast - 4
    Cast Heal - 5
    Cast shield - 6
    Open Menu - ESC
    Mute - F1
    Restart - F5
    Suicide - F6
    Toggle Controls - F7
    Open Console - ~

The console can be opened with the '~' key. There are various commands you can type to test out the game.

    Kill - Kills all mobs on the map.
    Godmode - Gives you godmode
    Mana - Gives you unlimited mana
    Onehit - Kills all mobs in onehit
    3D - Moves you into 3D mode where you can use the up and down keys to navigate the z-axis.

Patch notes:

Patch v1.2.0:

    Furthered the tutorial
    Added "broadcast" feature to conversation system, allowing for chatting that operates outside of pure text.

Patch v1.1.9:

    Added the beginnings of a tutorial level for the game. (new map).
    NPC system added. Added tutorial NPC Darien.
    Added new mob type: spider.
    Intricate interactive conversation system added.
    Cleaned up the temporary interface.
    New GUI textures.
    Added shield spell. Press 6 (temporary) to activate a 25 HP shield. Responds to shield health and taking damage.
    Fixed bug where mob hp bars regen above maximum.
    Fixed bug where fire sword particles did not match up.
    All mobs are now able to be walked through.

Patch v1.1.8:

    Main menu options window implemented.
    Volume controls + mute added
    Combo meter added: upon reaching 5 combo, the next auto attack is empowered and deals extra damage.
    Only normal attacks generate adrenaline and combo now
    Players can now move through other NPC characters. The NPCs will turn semi-transparent while being passed through. This seemed like the more logical solution to unhindered walking in a 2D plane, versus the other candidate of slightly unlocking the Z-axis.
    New "quit" button added; main menu slightly altered.
    Mobs now regenerate HP as well.
    Mobs no longer "spaz out", reversing direction very quickly, when on the same x coordinates as a player. Mobs stop moving when attacking. Knocking back a mob now is more responsive.
    Large changes gone through, many bugs to be expected.
    Known bugs:
        More than one enemy HP bar is shown at one time when in crowded areas.
        Mob HP bars sometimes go above the maximum.
        Glitching through terrain is possible with teleport, resulting in unintentional suicide.
        Empowered normal attack particles do not match with sword movement properly all the time.
        Many spells/actions do not have sounds yet.
        QUIT button opens up options menu..
        Mobs on second map have not been updated to allow for clipping through them.
        Ability to attack persists throughout death animation.


Patch v1.1.7:

    Added new commands to console (godmode, 3D mode [beta], onehit, help, unlimited mana.)
    Created new beta mode where movement on the Z-axis is unlocked.
    Created infrastructure for rework on how combos and adrenaline will be handled. Only auto attacks will generate adrenaline and combos, and upon completing the combo bar, an empowered normal attack will be available.
    Possible rework to allow for movement through other NPC's (mobs, passives, etc). Mobs will be transparent when passed through.

Patch v1.1.6:

    Upgraded main menu.

Patch v1.1.5:

    Added a console for developer commands (godmode, noclip, onehitkill, etc)
    Updated controls
    Normalized general sound levels so that they were more balanced (most notably thunder sound when dead).

Patch v1.1.4:

    Added a start menu (styling in progress) with animations and music.
    Added an in-game menu.
    Added user-feedback survey when exiting the game (http://goo.gl/forms/omUE7S7Iay)

Patch v1.1.3:

    Fixed a bug where other map data could not be properly accessed resulting in the player being trapped in limbo (null pointer issues).
    Increased the intensity of "blood-in-eyes" effect, due to advice from my mom.
    Fixed a bug where the "blood-in-eyes" effect would not go away upon death or would be stuck when going above the cut-off health.

Patch v1.1.2:

    Added flashing "blood-in-eyes" effect when low health.
    Display windows (messages, controls, and debugging) are now movable.
    Fixed a bug where not all instances of sword hitboxes were enlarged.
    Bug found in selecting enemies on multiple levels.
    Bug found in occasionally teleporting through terrains in areas of varied altitude.

Patch v1.1.1:

    Added level and exp system, combo + increasing hit system, and money drops with money system.
    A debugging display was added to display all this information until permanent displays can be added (HP bars included).
    A messages box is added to show actions (gain exp, gain money, etc), as well as a more detailed controls window.
    Map further extended.
    Level-up animation added.
    EXP/Money dropped reflect difficulty of enemy killed.

Patch v1.1.0:

    Introduced adrenaline as primary resource. Attacks and movements increase adrenaline, inactivity decreases it.
    Revamped targeting interface with a new AOE (area of effect) selector, which can select both areas for AOE spells and select single targets.
    Two AOE spells added: Fire Blast and Ice Blast.
    Heal spell added.
    Added new map for continuous dungeon troll killing at the bridge.
    Auto-attack hitbox size increased.

Patch v1.0.9:

    Single target projectile spell Fireball added.
    Single target point-and-click spell Lightning Strike added.
    Mouse-selected targeting interface added.
    Health-bar displayed now prioritizes targeted enemies over nearby enemies.
    Large file-size background audio removed to decrease file size.

Patch v1.0.0 - v1.0.8:

    Game began development
    Basic physics and controls added.
    Physics revamped to allow for time-based movement rather than frame-based movement.
    Sounds added.
    Basic attacks added.
    Animations added.
    Basic maps created.
    Established many significant supporting scripts.

