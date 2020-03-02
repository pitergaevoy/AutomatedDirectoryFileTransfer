using System.Collections.Generic;
using System.IO;

namespace DirectoryTransfer.UI
{
    public class MainApp
    {
        public Configuration Configuration { get; private set; }

        private readonly DirectoryScanner _scanner;
        private const string ConfigPath = "myConfig.json";
        public MainApp()
        {
          

            Configuration = new Configuration
            {
                Units = new List<ScannerUnit>
                {
                    new ScannerUnit
                    {
                        DirectoryForScan = @"C:\Users\gaevoy\Downloads",
                        DirectoryForTransfer = @"C:\Users\gaevoy\Desktop\CFG",
                        SearchPattern = "*.cfg"
                    }
                }
            };

            if (File.Exists(ConfigPath))
                Configuration = Configuration.GetConfiguration(ConfigPath);

            Configuration.SaveConfiguration(Configuration, ConfigPath);

            _scanner = new DirectoryScanner();
        }

        public void StartListen()
        {
            _scanner.DirectoryListener(Configuration.Units);
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

    }
}
