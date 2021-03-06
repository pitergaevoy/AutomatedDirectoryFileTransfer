﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;

namespace DirectoryTransfer
{
    [DataContract]
    public class Configuration
    {
        [DataMember] public List<ScannerUnit> Units { get; set; } = new List<ScannerUnit>();

        public static Configuration GetConfiguration(string path)
        {
            var jsonString = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Configuration>(jsonString);
        }

        public static void SaveConfiguration(Configuration configuration, string path)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var jsonString = JsonSerializer.Serialize(configuration, options);

            File.WriteAllText(path, jsonString);
        }
    }

    [DataContract]
    public class ScannerUnit
    {
        [DataMember] public string DirectoryForScan { get; set; }
        [DataMember] public string SearchPattern { get; set; }
        [DataMember] public string DirectoryForTransfer { get; set; }
    }
}
