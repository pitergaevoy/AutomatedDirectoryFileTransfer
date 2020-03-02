using System.Windows;

namespace DirectoryTransfer.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainApp _mainApp;

        public MainWindow(MainApp mainApp)
        {
            _mainApp = mainApp;
            InitializeComponent();

            _mainApp.StartListen();
        }

        private void OpenMenuItemClicked(object sender, RoutedEventArgs e)
        {
            var settingsView = new Settings(_mainApp.Configuration);
            settingsView.ShowDialog();

            var configuration = settingsView.ViewModel.Configuration;

            _mainApp.Stop();
            _mainApp.SetParams(configuration);
            _mainApp.StartListen();
        }

        private void CloseMenuItemClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
