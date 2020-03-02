using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Input;
using Prism.Commands;

namespace DirectoryTransfer.UI
{
    public class SettingsViewModel : BaseViewModel
    {
        public Configuration Configuration { get; set; }

        public ObservableCollection<ScannerUnitViewModel> ScannerUnits { get; set; }

        public ScannerUnitViewModel SelectedUnit { get; set; }

        public ICommand SaveCommand { get; }

        public ICommand AddRowCommand { get; }

        public DelegateCommand RemoveRowCommand { get; }


        public SettingsViewModel(Configuration configuration)
        {
            Configuration = configuration;

            SaveCommand = new DelegateCommand(SaveSettings);
            AddRowCommand = new DelegateCommand(AddRow);
            RemoveRowCommand = new DelegateCommand(RemoveRow, CanRemoveRow);

            PropertyChanged += SettingsViewModel_PropertyChanged;

            ScannerUnits = new ObservableCollection<ScannerUnitViewModel>(configuration.Units.Select(unit => new ScannerUnitViewModel(unit)));
        }

        private void SettingsViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedUnit))
            {
                RemoveRowCommand.RaiseCanExecuteChanged();
            }
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

        private void SaveSettings()
        {
            Configuration.Units = ScannerUnits.Select(vm => vm.ToUnit()).ToList();
        }
    }


    public class ScannerUnitViewModel : BaseViewModel
    {
        public ScannerUnitViewModel(ScannerUnit unit)
        {
            DirectoryForScan = unit.DirectoryForScan;
            SearchPattern = unit.SearchPattern;
            DirectoryForTransfer = unit.DirectoryForTransfer;
        }

        [DataMember] public string DirectoryForScan { get; set; }
        [DataMember] public string SearchPattern { get; set; }
        [DataMember] public string DirectoryForTransfer { get; set; }

        public ScannerUnit ToUnit()
        {
            return new ScannerUnit
            {
                DirectoryForScan = DirectoryForScan,
                SearchPattern = SearchPattern,
                DirectoryForTransfer = DirectoryForTransfer,

            };
        }
    }
}
