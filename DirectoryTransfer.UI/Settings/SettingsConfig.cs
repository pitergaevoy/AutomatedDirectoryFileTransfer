using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;

namespace DirectoryTransfer.UI
{
    [DataContract]
    public class SettingsConfig
    {
        public static SettingsConfig GetSettings(string path)
        {
            var jsonString = File.ReadAllText(path);
            return JsonSerializer.Deserialize<SettingsConfig>(jsonString);
        }

        public static void SaveSettings(SettingsConfig settings, string path)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var jsonString = JsonSerializer.Serialize(settings, options);

            File.WriteAllText(path, jsonString);
        }
    }
}