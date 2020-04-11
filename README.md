# VolturaTools
Small tools and utilities for Windows environment

# ScanDevices

Windows Service that automatically performs a Device Manager "Scan for changes" operations every second. Useful for devices that has been removed due to that the external device has been turned off. Use it to get my HDMI sound to work towards my stereo after it has been turned off.

# WeekNumber
<a href="https://github.com/voltura/VolturaTools/releases/download/v1.5.0.0/WeekNumber_x86_portable.exe">Download executable - version 1.5.0.1</a>

  Small Toolbar app that displays current week number.
  
Configuration options;
  - Context menu: About WeekNumber - displays about box with Github link
  - Context menu: Start with Windows - Toogle if WeekNumber.exe shall be automatically started with Windows or not (Checkbox indication)
  - Context menu: Save icon... - Lets user save current icon to file
  - Context menu: First day of week - Lets user select what day is the first of the week
  - Context menu: Calendar week rule - Lets user specify rule for first week of year
  - Context menu: Icon colors... - Lets user specify icon colors
  - Context menu: Exit WeekNumber - Closes the application

# DiskSpace 
<a href="https://github.com/voltura/VolturaTools/raw/master/DiskSpaceSetup.msi">Download installer</a>

  Small Toolbar enabled app that displays current amount of free space on a selected drive.
  Supports Email alerts, Balloon tip notifications, also visible via mouse hover over Taskbar icon or application form itself.
  Can be configured to only show/send notifications if specified GB amount limit of free disk space is passed.

Configuration options;
  - Auto start: Start application with Windows
  - Start minimized: Start application minimized (only visible in Taskbar when starting application)
  - Always on top: Application form shall remain top most (stay on top of other applications)
  - Drive to monitor: Select disk that the application should  monitor free disk space on
  - Show notifications: Enable/Disable notifications
  - Notification limit: Set free Gigabytes limit for when to show notifications
  - Email notifications: Send email when free space changes or when notification limit is reached
  - Email settings: Configure SMTP settings for email feature
  
Context menu options available via Taskbar icon click or right-click on main form title icon;
  - Show/Hide: Shows/hides the main form of the application that displays the amount of free space on monitored disk
  - Show Log: Displays the application log file
  - Check for update - Checks if new version exists online 
  - Quit: Quits the application
  - Disk clean-up: Launches Windows Disk Clean-up tool (DiskMgr.exe)
  - Disk Management: Launches Windows Disk Management
  
Main form features;
  - Displays available free space of selected drive
  - Icon to show settings form
  - Icon to show application log file
  - Context menu as described above
  - Click on free disk space will show next available disks free space (C -> D and so on), setting automatically updated to monitor selected disk
  - Title bar showns drive that is being monitored and the disk size
  
Email Settings form features;
  - Set SMTP server address and port
  - Set username and password
  - Set From/To Email address
  - Activate/Deactivate SSL usage
  
 # DiskSpaceSetup
   .MSI package for DiskSpace, supports install, uninstall via ControlPanel or through .MSI. Also Repair function.
   When installing options exist to configure DiskSpace (not all settings) and to start it after completed installation.

# ProxyToogle
<a href="https://github.com/voltura/VolturaTools/releases/tag/v0.0.0.0">Download ProxyToogle portable x64 executable - version 0.0.0.0</a>
Utility to toogle proxy on|off from a Windows taskbar icon.
- Click taskbar icon to toogle proxy on|off.
  - Red icon = proxy off
  - Green icon = proxy on
- Right-click to show context menu to exit the application.
