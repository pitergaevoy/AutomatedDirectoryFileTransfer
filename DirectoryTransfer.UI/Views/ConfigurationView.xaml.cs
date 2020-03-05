namespace DirectoryTransfer.UI
{
    public partial class ConfigurationView
    {
        public ConfigurationViewModel ViewModel { get; }

        public ConfigurationView(Configuration configuration)
        {
            InitializeComponent();

            ViewModel = new ConfigurationViewModel(configuration);
            DataContext = ViewModel;
        }
    }
}