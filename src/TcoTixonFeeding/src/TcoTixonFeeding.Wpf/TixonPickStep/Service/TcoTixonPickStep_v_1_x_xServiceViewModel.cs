using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcoTixonFeeding
{
    public class TcoTixonPickStep_v_1_x_xServiceViewModel
        : TcoTixonFeedingBaseServiceViewModel<
            TcoTixonPickStep_v_1_x_x,
            PlainTcoTixonPickStep_Config_v_1_x_x
        >
    {
        public TcoTixonPickStep_v_1_x_xServiceViewModel()
            : base() { }
    }
}
