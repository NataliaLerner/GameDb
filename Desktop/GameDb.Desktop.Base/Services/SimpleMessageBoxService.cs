using System.Windows;
using GameDb.Desktop.Base.Interfaces;

namespace GameDb.Desktop.Base.Services
{
    public class SimpleMessageBoxService : ISimpleMessageBoxService
    {
        public MessageBoxResult Show(string text, string caption, MessageBoxButton buttons, MessageBoxImage image)
        {
            return MessageBox.Show(text, caption, buttons, image);
        }
    }
}