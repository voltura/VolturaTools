# VolturaTools
Small tools and utilities for Windows environment

# DiskSpace
  Small Toolbar enabled app that displays current amount of free space on a selected drive
  Supports Balloon tip notifications, also visible via hover over Taskbar icon or application form itself
  Can be configured to only show notifications if specified GB amount limit of free disk space is passed

Configuration possibilites to;
  - Start application with Windows
  - Start application minimized (only visible in Taskbar when starting application)
  - Select disk that the application should  monitor free disk space of
  - Set free GB limit limit for when to show notifications
  - Enable/Disable balloon tip notifications
  
Context menu options available via Taskbar icon click or right-click on main form of application;
  - Show: Shows the main form of the application that displays the amount of free space on montitored disk
  - Hide: If applicatiopn form is visible then this menu option exist to hide it
  - Quit: Quits the application
  
 # DiskSpaceSetup
   .MSI package for DiskSpace, supports install, uninstall via ControlPanel or though .MSI. Also Repair function.
   When installing options exist to configure DiskSpace and to start it after completed installation.
