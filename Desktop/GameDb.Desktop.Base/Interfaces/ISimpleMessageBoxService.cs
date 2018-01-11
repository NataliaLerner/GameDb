using System.Windows;

namespace GameDb.Desktop.Base.Interfaces
{
    public interface ISimpleMessageBoxService
    {
        MessageBoxResult Show(
            string text,
            string caption,
            MessageBoxButton buttons,
            MessageBoxImage image);
    }
}