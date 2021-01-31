# OLEDScreenSaver
Small window utility that turns your OLED screen black when no input is detected. Works even when a program is keeping Windows from triggering screen sleep.

# Features
* Custom sleep timer in minutes
* Launch at Windows startup

# Known bugs

`Point p = new Point();  
p.X = OLED_screen.Bounds.Location.X;  
p.Y = OLED_screen.Bounds.Location.Y;  
this.Location = p;`  

This does not restore the window location to the selected screen after it has been turned to sleep or powered off. The workaround for this is always using the primary screen for detecting the bounds. If you find a way to get Location to target the right screen without restarting the app, please let me know.
In the meantime, we will use the primary screen for bounds detection. Seems to work regardless of past screen state.

# Notes

Tested on Windows 10, not sure about the other versions.
