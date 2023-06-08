using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TcOpen.Inxton.Wpf;

namespace TcoDrivesBeckhoff
{

    public partial class TcoSingleAxisServiceView : UserControl
    {
        public TcoSingleAxisServiceView()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                this.DataContext = new TcoSingleAxis();
            }

            InitializeComponent();
        }


        //protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        //{
        //    base.OnPropertyChanged(e);

        //    if (e.Property == DataContextProperty)
        //    {
        //        var a = this.DataContext as TcoSingleAxisServiceViewModel;
        //        if (this.positions.DataContext is stManipulatorPtpXYZ_DataRootViewModel)
        //            a.Positions = (stManipulatorPtpXYZ_DataRootViewModel)this.positions.DataContext;
        //    }
        //}


    }

   
}
