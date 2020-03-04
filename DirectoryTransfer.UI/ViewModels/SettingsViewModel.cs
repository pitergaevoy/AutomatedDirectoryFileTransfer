namespace DirectoryTransfer.UI
{
    public class SettingsViewModel : BaseViewModel
    {
        public SettingsConfig Settings { get; }

        public SettingsViewModel(SettingsConfig settings)
        {
            Settings = settings;
        }
    }
}   