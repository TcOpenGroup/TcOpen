using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Abstractions.Input
{
    public interface ITcoCommand
    {
        bool CanExecute(object parameter);
        void Execute(object parameter);
    }
}
