using System;
using System.Collections.Generic;
using System.IO;

namespace DirectoryTransfer.UI
{
    public class MainApp
    {
        private const string ConfigPath = "myConfig.json";
        private const string SettingsPath = "mySettings.json";


        private readonly DirectoryScanner _scanner;

        #region Public Properties

        public Configuration Configuration { get; private set; }

        public SettingsConfig Settings { get; private set; }

        #endregion

        #region Constructor

        public MainApp()
        {
            Configuration = new Configuration
            {
                Units = new List<ScannerUnit>
                {
                    new ScannerUnit
                    {
                        DirectoryForScan = @"C:\",
                        DirectoryForTransfer = @"C:\Users\",
                        SearchPattern = "*.extension"
                    }
                }
            };

            Settings = new SettingsConfig();

            if (File.Exists(SettingsPath))
                Settings = SettingsConfig.GetSettings(SettingsPath);

            if (File.Exists(ConfigPath))
                Configuration = Configuration.GetConfiguration(ConfigPath);

            SettingsConfig.SaveSettings(Settings, SettingsPath);
            Configuration.SaveConfiguration(Configuration, ConfigPath);

            MainExtensions.SetIsRunWhenStartValue(Settings.IsRunWhenComputerStarts);

            _scanner = new DirectoryScanner();
        }

        #endregion
        
        public void StartListen()
        {
            try
            {
                _scanner.DirectoryListener(Configuration.Units);
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        public void Stop()
        {
            _scanner.Stop();
        }

        public void SetParams(Configuration configuration)
        {
            Configuration = configuration;

            Configuration.SaveConfiguration(configuration, ConfigPath);
        }

        public void ApplySettings(SettingsConfig settingsConfig)
        {
            Settings = settingsConfig;

            SettingsConfig.SaveSettings(settingsConfig, SettingsPath);

            MainExtensions.SetIsRunWhenStartValue(settingsConfig.IsRunWhenComputerStarts);
        }
    }
}