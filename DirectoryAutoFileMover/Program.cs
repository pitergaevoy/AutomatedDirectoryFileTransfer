using System;
using System.IO;

namespace DirectoryAutoFileMover
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string configPath = "myConfig.json";

            var configuration = new Configuration
            {
                DirectoryForScan = @"C:\Users\gaevoy\Downloads",
                DirectoryForTransfer = @"C:\Users\gaevoy\Desktop\CFG",
                SearchPattern = "*.cfg"
            };

            if (File.Exists(configPath)) 
                configuration = Configuration.GetConfiguration(configPath);

            Configuration.SaveConfiguration(configuration, configPath);

            DirectoryScanner.DirectoryListener(configuration.DirectoryForScan, configuration.SearchPattern, configuration.DirectoryForTransfer);

            Console.ReadKey();
        }

    }
}
