using System;
using System.Windows;
using System.Windows.Controls;
using TcoData;

namespace TcoData
{
    /// <summary>
    /// Interaction logic for fbDataExchangeControlView.xaml
    /// </summary>
    public partial class TcoDataExchangeSimpleSelectorView : UserControl
    {
        public TcoDataExchangeSimpleSelectorView()
        {
            InitializeComponent();
            this.DataContextChanged += TcoDataExchangeSimpleSelectorView_DataContextChanged;
        }

        private void TcoDataExchangeSimpleSelectorView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(!(DataContext is TcoDataExchangeSimpleSelectorViewModel) && DataContext is TcoDataExchange)
            {
                this.DataContext = new TcoDataExchangeSimpleSelectorViewModel() { Model = DataContext };
            }
        }
       
        private TcoDataExchangeSimpleSelectorViewModel _context
        {
            get
            {
                return this.DataContext as TcoDataExchangeSimpleSelectorViewModel;
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (this.DataContext == null)
                    return;

                if (this.Visibility == Visibility.Visible && _context is TcoDataExchangeSimpleSelectorViewModel)
                {
                    var maxNumberOfRecipies = 50;
                    var currentLimit = _context.DataViewModel.Limit;
                    _context.DataViewModel.Limit = maxNumberOfRecipies;
                    _context.DataViewModel.FillObservableRecords();
                    _context.DataViewModel.Limit = currentLimit;

                }
            }
            catch (Exception)
            {

                //++ Ignore
            }         
        }

        private void FilterField_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
