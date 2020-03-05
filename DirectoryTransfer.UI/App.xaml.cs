using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Hardcodet.Wpf.TaskbarNotification;
using Prism.Commands;

namespace DirectoryTransfer.UI
{
    public partial class App
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
                    ItemsSource = new ObservableCollection<Control>
                    {
                        new MenuItem
                        {
                            Header = "Settings",
                            Command = new DelegateCommand(OpenSettings)
                        },

                        new MenuItem
                        {
                            Header = "Configuration",
                            Command = new DelegateCommand(OpenConfiguration)
                        },

                        new Separator(),

                        new MenuItem
                        {
                            Header = "Quit Directory Transfer",
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
           var settingsView = new SettingsView(_mainApp.Settings);

           settingsView.ShowDialog();

           var settingsConfig = settingsView.ViewModel.Settings;

           _mainApp.ApplySettings(settingsConfig);
        }
        
        private void OpenConfiguration()
        {
            var configurationView = new ConfigurationView(_mainApp.Configuration);

            configurationView.ShowDialog();

            var configuration = configurationView.ViewModel.Configuration;

            _mainApp.Stop();
            _mainApp.SetParams(configuration);
            _mainApp.StartListen();
        }
    }
}