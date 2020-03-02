using System;
using System.Configuration;
using System.IO;
using System.Windows;

namespace DirectoryTransfer.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            RunListenDir();
        }

        private void RunListenDir()
        {
            const string configPath = "myConfig.json";

            var configuration = new DirectoryTransfer.Configuration
            {
                DirectoryForScan = @"C:\Users\gaevoy\Downloads",
                DirectoryForTransfer = @"C:\Users\gaevoy\Desktop\CFG",
                SearchPattern = "*.cfg"
            };

            if (File.Exists(configPath))
                configuration = Configuration.GetConfiguration(configPath);

            Configuration.SaveConfiguration(configuration, configPath);

            DirectoryScanner.DirectoryListener(configuration.DirectoryForScan, configuration.SearchPattern, configuration.DirectoryForTransfer);

        }

    }
}
