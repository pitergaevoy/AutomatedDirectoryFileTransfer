namespace DirectoryTransfer.UI
{
    public class ScannerUnitViewModel : BaseViewModel
    {
        #region Public Properties

        public string DirectoryForScan { get; set; }
        public string SearchPattern { get; set; }
        public string DirectoryForTransfer { get; set; }

        #endregion

        #region Constructor

        public ScannerUnitViewModel(ScannerUnit unit)
        {
            DirectoryForScan = unit.DirectoryForScan;
            SearchPattern = unit.SearchPattern;
            DirectoryForTransfer = unit.DirectoryForTransfer;
        }

        #endregion

        #region Public Methods
        
        public ScannerUnit ToUnit()
        {
            return new ScannerUnit
            {
                DirectoryForScan = DirectoryForScan,
                SearchPattern = SearchPattern,
                DirectoryForTransfer = DirectoryForTransfer,
            };
        }

        #endregion
    }
}