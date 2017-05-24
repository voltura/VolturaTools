﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiskSpace.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.1.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        /// <summary>
        /// Drive to report free space on
        /// </summary>
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsDescriptionAttribute("Drive to report free space on")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C")]
        public string driveLetter {
            get {
                return ((string)(this["driveLetter"]));
            }
            set {
                this["driveLetter"] = value;
            }
        }
        
        /// <summary>
        /// Show application form topmost
        /// </summary>
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsDescriptionAttribute("Show application form topmost")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool alwaysOnTop {
            get {
                return ((bool)(this["alwaysOnTop"]));
            }
            set {
                this["alwaysOnTop"] = value;
            }
        }
        
        /// <summary>
        /// Start application without displaying form
        /// </summary>
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsDescriptionAttribute("Start application without displaying form")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool startMinimized {
            get {
                return ((bool)(this["startMinimized"]));
            }
            set {
                this["startMinimized"] = value;
            }
        }
        
        /// <summary>
        /// Start application automatically with Windows
        /// </summary>
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsDescriptionAttribute("Start application automatically with Windows")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool startWithWindows {
            get {
                return ((bool)(this["startWithWindows"]));
            }
            set {
                this["startWithWindows"] = value;
            }
        }
        
        /// <summary>
        /// Display Balloon notification when free disk space changes
        /// </summary>
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsDescriptionAttribute("Display Balloon notification when free disk space changes")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool notifyWhenSpaceChange {
            get {
                return ((bool)(this["notifyWhenSpaceChange"]));
            }
            set {
                this["notifyWhenSpaceChange"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Autorun")]
        public string StartWithWindowsText {
            get {
                return ((string)(this["StartWithWindowsText"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Start minimized")]
        public string StartMinimizedText {
            get {
                return ((string)(this["StartMinimizedText"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Free space notifications")]
        public string ShowNotificationsText {
            get {
                return ((string)(this["ShowNotificationsText"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Form always on top")]
        public string AlwaysOnTopText {
            get {
                return ((string)(this["AlwaysOnTopText"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Drive to monitor")]
        public string DriveToMonitorText {
            get {
                return ((string)(this["DriveToMonitorText"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Close")]
        public string SaveButtonTitle {
            get {
                return ((string)(this["SaveButtonTitle"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Settings")]
        public string SettingsFormTitle {
            get {
                return ((string)(this["SettingsFormTitle"]));
            }
        }
    }
}
