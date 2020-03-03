using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Hardcodet.Wpf.TaskbarNotification;
using Prism.Commands;

namespace DirectoryTransfer.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon _taskBarIcon;
        private MainApp _mainApp;

        protected override void OnStartup(StartupEventArgs e)
        {
            _mainApp = new MainApp();

            var resource = TryFindResource("TaskbarIcon");

            if (resource is TaskbarIcon taskBarIcon)
            {
                _taskBarIcon = taskBarIcon;

                _taskBarIcon.ContextMenu = new ContextMenu
                {
                    ItemsSource = new ObservableCollection<MenuItem>
                    {
                        new MenuItem
                        {
                            Header = "Open settings",
                            Command = new DelegateCommand(OpenSettings)
                        },
                        new MenuItem
                        {
                            Header = "Close program",
                            Command = new DelegateCommand(() => Current.Shutdown())
                        },
                    }
                };

                _mainApp.StartListen();

                ShutdownMode = ShutdownMode.OnExplicitShutdown;
            }
            
            base.OnStartup(e);
        }

        private void OpenSettings()
        {
            var settingsView = new SettingsView(_mainApp.Configuration)
            {
            };

            settingsView.ShowDialog();

            var configuration = settingsView.ViewModel.Configuration;

            _mainApp.Stop();
            _mainApp.SetParams(configuration);
            _mainApp.StartListen();
        }
    }
}