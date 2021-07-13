using System.Windows;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    public class WPFMessageBoxService : IMessageBoxService
    {         
        bool IMessageBoxService.ShowMessage(string text, string caption, MessageType messageType)
        {
            if(MessageType.YesNo == messageType)
                return MessageBox.Show(text, caption, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;
            else
                return MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK;
        }
    }
}
