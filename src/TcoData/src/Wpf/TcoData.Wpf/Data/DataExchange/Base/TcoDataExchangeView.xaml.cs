using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TcoData
{
    /// <summary>
    /// Interaction logic for fbDataExchangeControlView.xaml
    /// </summary>
    public partial class TcoDataExchangeView : UserControl
    {
        public TcoDataExchangeView()
        {
            InitializeComponent();
            this.DataContextChanged += TcoDataExchangeView_DataContextChanged;
        }

        private void TcoDataExchangeView_DataContextChanged(
            object sender,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (
                !(this.DataContext is TcoDataExchangeViewModel)
                && this.DataContext is TcoDataExchange
            )
            {
                this.DataContext = new TcoDataExchangeViewModel() { Model = this.DataContext };
            }
        }

        public ObservableCollection<DataGridColumn> DataListColumns
        {
            get => this.DataView.DataListColumns;
            set { this.DataView.DataListColumns = value; }
        }
    }

    public class TcoDataExchangeControlView : TcoDataExchangeView
    {
        public TcoDataExchangeControlView()
            : base() { }
    }

    public class TcoDataExchangeDisplayView : TcoDataExchangeView
    {
        public TcoDataExchangeDisplayView()
            : base() { }
    }
}
