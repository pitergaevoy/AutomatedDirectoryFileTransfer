using System.Windows;

namespace DirectoryTransfer.UI
{
    public partial class Settings : Window
    {
        public SettingsViewModel ViewModel { get; }

        public Settings(Configuration configuration)
        {
            InitializeComponent();

            ViewModel = new SettingsViewModel(configuration);
            DataContext = ViewModel;
        }
    }
}
