# OLEDScreenSaver
Small window utility that turns your OLED screen black when no input is detected. Works even when a program is keeping Windows from triggering screen sleep.

# Features
* Pause function
* Custom sleep timer in minutes
* Launch at Windows startup

# How to use

Get .NET framework > 4.72 here: https://dotnet.microsoft.com/download/dotnet-framework/net472  
Extract and use .exe anywhere you like. You can put it in your Documents directory to avoid any permissions problems.  
The startup function will automatically resolve the right path when it needs to launch it on Windows startup.

# Known bugs

`Point p = new Point();
p.X = OLED_screen.Bounds.Location.X;
p.Y = OLED_screen.Bounds.Location.Y;
this.Location = p;`

This does not restore the window location to the selected screen after it has been turned to sleep or powered off. The workaround for this is always using the primary screen for detecting the bounds. If you find a way to get Location to target the right screen without restarting the app, please let me know.  
In the meantime, we will use the primary screen for bounds detection. Seems to work regardless of past screen state.  

# Notes

Now uses polling of last windows input. Polling time can be adjusted in the settings, a range between 500-1000 is generally ok for performances.  
The reasoning behind this is to get rid of any windows hook on mouse or keyboard events. These can slow down your system and probably not good for games?
The counter part to this is the black screen could take the entire polling time to disappear.

Please, let me know if there are any performances impact in-game after the black screen has disappeared.  

Tested on Windows 10 18363.1316, not sure about the other versions.
