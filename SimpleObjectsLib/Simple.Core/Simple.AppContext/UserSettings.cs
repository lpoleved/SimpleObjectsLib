﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.AppContext
{
    public class UserSettings : AppSettings
    {
		public int DefaultRibbonPagePlacement;
        public int DefaultRibbonStyle = 1; // RibbonControlStyle.Office2007 = 1;

        public int DefaultRibbonControlColorScheme = 5; // RibbonControlColorScheme.Default
        public string DefaultRibonSkinName = "Black"; //"Office 2010 Blue";
        public int DefaultRibbonSkinMaskColor = -1;
		public int DefaultRibbonSkinMaskColor2 = -1;
		public bool DefaultCompactUI = false;
        public string DefaultServer = "localhost";
        public int DefaultPort = 2020;
        public string DefaultUsername = "Admin"; //"System Admin";

        public string DarkModeSkinName = "Visual Studio 2013 Dark";
        
        
        public const string SettingDarkMode = "DarkMode";

        public const string SettingRibbonStyle = "RibbonStyle";
		public const string SettingRibbonColorScheme = "RibbonColorScheme";
		public const string SettingRibbonPagePlacement = "RibbonPagePlacement";
		public const string SettingRibbonSkinMaskColor = "RibbonSkinMaskColor";
		public const string SettingRibbonSkinMaskColor2 = "RibbonSkinMaskColor2";
		public const string SettingRibbonSkinName = "RibbonSkinName";
        public const string SettingCompactUI = "CompactUI";
        public const string SettingServer = "Server";
        public const string SettingPort = "Port";
        public const string SettingProxy = "Proxy";
        public const string SettingLastUsername = "LastUsername";

		public const string SettingWindowLocationX = "WindowLocationX";
        public const string SettingWindowLocationY = "WindowLocationY";
        public const string SettingWindowWidth = "WindowWidth";
        public const string SettingWindowHeight = "WindowHeight";
        public const string SettingWindowState = "WindowState";

        public const string SettingFormDefaultChangeContainerWindowLocationX = "FormDefaultChangeContainerWindowLocationX";
        public const string SettingFormDefaultChangeContainerWindowLocationY = "FormDefaultChangeContainerWindowLocationY";
        public const string SettingFormDefaultChangeContainerWindowWidth = "FormDefaultChangeContainerWindowWidth";
        public const string SettingFormDefaultChangeContainerWindowHeight = "FormDefaultChangeContainerWindowHeight";
        public const string SettingFormDefaultChangeContainerWindowState = "FormDefaultChangeContainerWindowState";

        public UserSettings(string filePath)
            : base(filePath)
        {
        }

        public string Server
        {
            get { return this.GetValue<string>(SettingServer, defaultValue: DefaultServer); }
            set { this.SetValue(SettingServer, value, defaultValue: DefaultServer); }
        }

        public int Port
        {
            get { return this.GetValue<int>(SettingPort, defaultValue: DefaultPort); }
            set { this.SetValue(SettingPort, value, defaultValue: DefaultPort); }
        }

        public string Proxy
        {
            get { return this.GetValue<string>(SettingProxy); }
            set { this.SetValue(SettingProxy, value); }
        }

        public string LastUsername
        {
            get { return this.GetValue<string>(SettingLastUsername, defaultValue: DefaultUsername); }
            set { this.SetValue(SettingLastUsername, value, defaultValue: DefaultUsername); }
        }

		public int RibbonPagePlacement
		{
			get { return this.GetValue<int>(SettingRibbonPagePlacement, defaultValue: this.DefaultRibbonPagePlacement); }
			set { this.SetValue(SettingRibbonPagePlacement, (int)value, defaultValue: this.DefaultRibbonPagePlacement); }
		}

		public int RibbonSkinMaskColor
		{
			get { return this.GetValue<int>(SettingRibbonSkinMaskColor, defaultValue: this.DefaultRibbonSkinMaskColor); }
			set { this.SetValue(SettingRibbonSkinMaskColor, (int)value, defaultValue: this.DefaultRibbonSkinMaskColor); }
		}

		public int RibbonSkinMaskColor2
		{
			get { return this.GetValue<int>(SettingRibbonSkinMaskColor2, defaultValue: this.DefaultRibbonSkinMaskColor2); }
			set { this.SetValue(SettingRibbonSkinMaskColor2, (int)value, defaultValue: this.DefaultRibbonSkinMaskColor2); }
		}

		public int RibbonStyle
		{
			get { return this.GetValue<int>(SettingRibbonStyle, defaultValue: this.DefaultRibbonStyle); }
			set { this.SetValue(SettingRibbonStyle, (int)value, defaultValue: this.DefaultRibbonStyle); }
		}

		public int RibbonColorScheme
		{
			get { return this.GetValue<int>(SettingRibbonColorScheme, defaultValue: this.DefaultRibbonControlColorScheme); }
			set { this.SetValue(SettingRibbonColorScheme, (int)value, defaultValue: this.DefaultRibbonControlColorScheme); }
		}

        public bool DarkMode
        {
            get { return this.GetValue<bool>(SettingDarkMode, defaultValue: false); }
            set { this.SetValue(SettingDarkMode, value, defaultValue: false); }
        }

        public string RibbonSkinName
        {
            get { return this.GetValue<string>(SettingRibbonSkinName, defaultValue: this.DefaultRibonSkinName); }
            set { this.SetValue(SettingRibbonSkinName, value, defaultValue: this.DefaultRibonSkinName); }
        }
        
        public bool CompactUI
		{
            get { return this.GetValue<bool>(SettingCompactUI, defaultValue: this.DefaultCompactUI); }
            set { this.SetValue(SettingCompactUI, value, defaultValue: this.DefaultCompactUI); }
        }

        public string GetRibbonSkinName()
		{
            string skinName = String.Empty;

            if (this.DarkMode)
            {
                skinName = DarkModeSkinName;
            }
            else
			{
                object result = this.RibbonSkinName;
                    
                skinName = (result != null) ? result.ToString().Trim() : String.Empty;
            }

            return skinName;
		}

		public int WindowLocationX
        {
            get { return this.GetValue<int>(SettingWindowLocationX); }
            set { this.SetValue(SettingWindowLocationX, value); }
        }

        public int WindowLocationY
        {
            get { return this.GetValue<int>(SettingWindowLocationY); }
            set { this.SetValue(SettingWindowLocationY, value); }
        }

        public int WindowWidth
        {
            get { return this.GetValue<int>(SettingWindowWidth); }
            set { this.SetValue(SettingWindowWidth, value); }
        }

        public int WindowHeight
        {
            get { return this.GetValue<int>(SettingWindowHeight); }
            set { this.SetValue(SettingWindowHeight, value); }
        }

        public int WindowState
        {
            get { return this.GetValue<int>(SettingWindowState); }
            set { this.SetValue(SettingWindowState, value); }
        }

        public int FormDefaultChangeContainerWindowLocationX
        {
            get { return this.GetValue<int>(SettingFormDefaultChangeContainerWindowLocationX); }
            set { this.SetValue(SettingFormDefaultChangeContainerWindowLocationX, value); }
        }

        public int FormDefaultChangeContainerWindowLocationY
        {
            get { return this.GetValue<int>(SettingFormDefaultChangeContainerWindowLocationY); }
            set { this.SetValue(SettingFormDefaultChangeContainerWindowLocationY, value); }
        }

        public int FormDefaultChangeContainerWindowWidth
        {
            get { return this.GetValue<int>(SettingFormDefaultChangeContainerWindowWidth); }
            set { this.SetValue(SettingFormDefaultChangeContainerWindowWidth, value); }
        }

        public int FormDefaultChangeContainerWindowHeight
        {
            get { return this.GetValue<int>(SettingFormDefaultChangeContainerWindowHeight); }
            set { this.SetValue(SettingFormDefaultChangeContainerWindowHeight, value); }
        }

        public int FormDefaultChangeContainerWindowState
        {
            get { return this.GetValue<int>(SettingFormDefaultChangeContainerWindowState); }
            set { this.SetValue(SettingFormDefaultChangeContainerWindowState, value); }
        }
    }
}
