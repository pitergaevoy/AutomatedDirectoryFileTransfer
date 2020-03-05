namespace DirectoryTransfer.UI
{
    public partial class SettingsView
    {
        public SettingsViewModel ViewModel { get; }

        public SettingsView(SettingsConfig settingsConfig)
        {
            InitializeComponent();

            ViewModel = new SettingsViewModel(settingsConfig);
            DataContext = ViewModel;
        }
    }
}