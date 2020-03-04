using Prism.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DirectoryTransfer.UI
{
    public class ConfigurationViewModel : BaseViewModel
    {
        #region Public Properties

        public Configuration Configuration { get; }

        public ObservableCollection<ScannerUnitViewModel> ScannerUnits { get; }

        public ScannerUnitViewModel SelectedUnit { get; set; }

        #endregion

        #region Commands

        public ICommand SaveCommand { get; }

        public ICommand AddRowCommand { get; }

        public ICommand CancelCommand { get; }

        public DelegateCommand RemoveRowCommand { get; }

        #endregion

        #region Constructor

        public ConfigurationViewModel(Configuration configuration)
        {
            Configuration = configuration;

            SaveCommand = new DelegateCommand(SaveSettings);
            CancelCommand = new DelegateCommand(Cancel);
            AddRowCommand = new DelegateCommand(AddRow);
            RemoveRowCommand = new DelegateCommand(RemoveRow, CanRemoveRow);

            PropertyChanged += SettingsViewModel_PropertyChanged;

            ScannerUnits =
                new ObservableCollection<ScannerUnitViewModel>(
                    configuration.Units.Select(unit => new ScannerUnitViewModel(unit)));
        }

        #endregion

        #region Private Methods

        private void SettingsViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedUnit))
                RemoveRowCommand.RaiseCanExecuteChanged();
        }

        private bool CanRemoveRow()
        {
            if (SelectedUnit != null)
                return true;

            return false;
        }

        private void RemoveRow()
        {
            ScannerUnits.Remove(SelectedUnit);
        }

        private void AddRow()
        {
            ScannerUnits.Add(new ScannerUnitViewModel(new ScannerUnit
            {
                DirectoryForScan = "C:\\",
                SearchPattern = "*.extension",
                DirectoryForTransfer = "D:\\",
            }));
        }

        private void Cancel()
        {
            MainExtensions.Close();
        }

        private void SaveSettings()
        {
            Configuration.Units = ScannerUnits.Select(vm => vm.ToUnit()).ToList();

            Cancel();
        }

        #endregion
    }
}