using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Wpf.HelpProvider
{
    public class HelpProviderViewModel : INotifyPropertyChanged
    {
        private string componentName;

        public HelpProviderViewModel()
        {            
        }

        public string ComponentName { get => componentName; set { componentName = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ComponentName))); } }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
