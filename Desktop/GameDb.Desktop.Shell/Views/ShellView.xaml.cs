using GameDb.Desktop.Shell.ViewModels;

namespace GameDb.Desktop.Shell.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView
    {
        public ShellView()
        {
            DataContext = new ShellViewModel();
            InitializeComponent();
        }
    }
}
