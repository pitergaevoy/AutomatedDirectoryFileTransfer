namespace DirectoryTransfer.UI
{
    public partial class SettingsView 
    {
        public SettingsViewModel ViewModel { get; }

        public SettingsView(Configuration configuration)
        {
            InitializeComponent();

            ViewModel = new SettingsViewModel(configuration);
            DataContext = ViewModel;
        }
    }
}