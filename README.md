# TileIconifier
Creates tiles for any Windows 8.1 (Largely untested) and Windows 10 (Build 10586 and above) Start Menu icon. Now has support for 20H2 release.

--------------

**Features:**

 - Allows creation of Medium and Small tiles for your applications, modifying their current shortcut where possible.
 - Create custom shortcuts to Steam games, Chrome apps, Windows Store apps and explorer shortcuts, or any other shortcut target you wish using custom VBS script launchers. 
 - Extract icons from the software EXE/DLL, without using any other external program.
 - Move and resize the image to where you want it within the tile, allowing more customisation than simply a full sized image.
 - Supported Languages: English, Russian (happy to add other languages if anyone can help with translation).

--------------

## Instructions

**Basic tile creation usage:**

 - Find the shortcut you wish to modify in the list
 - Choose an image by clicking the 'Change Image' button or double clicking one of the logo boxes. Move/resize it as required by clicking, dragging and scrolling to where required.
 - Hit 'Tile Iconify!' to apply the changes
 - To restore, hit 'Remove Iconify'

--------------

**Custom shortcut creation:**

 - Navigate to Utilities -> Custom Shortcut Manager
 - From here you can create a new shortcut or delete other custom created items
 - When creating a new shortcut, it will take a moment to populate your Steam, Chrome and Windows Store libraries.
 - Most items will get a default icon based on what you choose. At this stage, you can change the icon to what you want.
 - Choose the shortcut name and which users it should be available to.
 - Generate the shortcut item. It will be Iconified using the icon chosen, but you can amend this as required back in the main page.
 - Finally, navigate through your Start Menu to 'All Apps' -> 'TileIconify'. From here, you can pin your new shortcuts to your Start Menu

--------------

## Frequently Asked Questions:

**Can you add Wide and Large tiles, or transparency?**

Unfortunately these options are only available for Live tiles, not standard LNK files with a VisualManifest, which is how this utility works. Unless Microsoft add support for it, this feature cannot be added in the near future.

**Microsoft Office/Mozilla Firefox/Other shortcuts aren't working, can these be tiled?**

There is some mechanism I have yet to understand overriding these from being tiled. The easiest way to resolve this is to go to your shortcut within TileIconifier and click 'Quick Build Custom Shortcut'. This will create a second shortcut with the same parameters that can be tiled, which you will then find in your start menu under 'TileIconify'. 

--------------

## Credits

Logo provided by AdamDesrosiers of XDA Developers.
Hugely improved skinning by @mcdenis - https://github.com/mcdenis

Translations:
Russian by @Zik - https://github.com/Zik
