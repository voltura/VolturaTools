# VolturaTools
Small tools and utilities for Windows environment

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
  
Context menu options available via Taskbar icon click or right-click on main form of application;
  - Show/Hide: Shows/hides the main form of the application that displays the amount of free space on monitored disk
  - Log: Displays the application log file
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
   .MSI package for DiskSpace, supports install, uninstall via ControlPanel or though .MSI. Also Repair function.
   When installing options exist to configure DiskSpace (not all settings) and to start it after completed installation.
