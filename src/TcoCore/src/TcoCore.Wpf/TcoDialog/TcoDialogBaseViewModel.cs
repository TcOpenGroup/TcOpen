using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Input;
using Vortex.Presentation;

namespace TcoCore
{
    public abstract class TcoDialogBaseViewModel : RenderableViewModelBase, ICloseable
    {
        public TcoDialogBaseViewModel()
            : base() { }

        public void Close(object sender, EventArgs e)
        {
            CloseRequestEventHandler?.Invoke(sender, e);
        }

        public event EventHandler CloseRequestEventHandler;
    }

    public interface ICloseable
    {
        event EventHandler CloseRequestEventHandler;
    }
}
