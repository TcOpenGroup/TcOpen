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
    public partial class TcoTask : ICommand
    {        
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;
                             
        public bool CanExecute(object parameter)
        {
            return this._enabled.Cyclic;            
        }

        public void Execute(object parameter)
        {
            this._invokeRequest.Synchron = true;
        }
    }
}
