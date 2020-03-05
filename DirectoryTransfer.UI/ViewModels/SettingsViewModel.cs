using Prism.Commands;
using System.Windows.Input;

namespace DirectoryTransfer.UI
{
    public class SettingsViewModel : BaseViewModel
    {
        public SettingsConfig Settings { get; }

        public bool IsRunWhenComputerStarts { get; set; }

        #region Commands

        public ICommand SaveCommand { get; }

        public ICommand CancelCommand { get; }

        #endregion

        public SettingsViewModel(SettingsConfig settings)
        {
            Settings = settings;

            SaveCommand = new DelegateCommand(SaveSettings);
            CancelCommand = new DelegateCommand(Cancel);

            IsRunWhenComputerStarts = settings.IsRunWhenComputerStarts;
        }

        private void Cancel()
        {
            MainExtensions.Close();
        }

        private void SaveSettings()
        {
            Settings.IsRunWhenComputerStarts = IsRunWhenComputerStarts;

            Cancel();
        }
    }
}