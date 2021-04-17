using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TcoCore
{
    public partial class TcoTask : ICommand,INotifyPropertyChanged
    {

        private bool isEnabled;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool IsEnabled
        {
            get
            {
                isEnabled = this._enabled.Synchron;
                NotifyPropertyChanged();
                return isEnabled;
            }
            set
            {
                if (value != this.isEnabled)
                {
                    this.isEnabled = value;
                    NotifyPropertyChanged();
                }
            }
        }

        
        public bool CanExecute(object parameter)
        {
            //return IsEnabled;
            return true;
        }

        public void Execute(object parameter)
        {
            this._invokeRequest.Synchron = true;
        }

    }

}
