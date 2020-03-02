using System.Windows;

namespace DirectoryTransfer.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainApp = new MainApp();

            var mw = new MainWindow(mainApp);
            mw.Show();

            base.OnStartup(e);
        }
    }
}
