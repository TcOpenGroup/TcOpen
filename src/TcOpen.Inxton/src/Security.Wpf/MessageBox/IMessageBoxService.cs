using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    public interface IMessageBoxService
    {
        bool ShowMessage(string text, string caption, MessageType messageType);
    }
}
