using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace DirectoryTransfer.UI
{
    public partial class SelectDirectoryTextBox
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(SelectDirectoryTextBox), new PropertyMetadata(default(string)));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public SelectDirectoryTextBox()
        {
            InitializeComponent();
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                var openDirectoryDialog = new FolderBrowserDialog
                {
                    SelectedPath = Text
                };

                var res = openDirectoryDialog.ShowDialog();

                if (res != DialogResult.OK)
                    return;

                Text = openDirectoryDialog.SelectedPath;
            }
        }
    }
}
