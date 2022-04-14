using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmiTemplate.Wpf.Views.Logs
{
    public class LogViewModel
    {
        public System.Windows.Controls.RichTextBox LogTextBox { get { return App.LogTextBox; } }
    }
}
