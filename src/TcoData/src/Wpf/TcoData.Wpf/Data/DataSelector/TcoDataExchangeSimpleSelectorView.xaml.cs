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
        }
        public TcoDataExchangeDisplayView ModelObject
        {
            get { return (TcoDataExchangeDisplayView)GetValue(ModelObjectProperty); }
            set { SetValue(ModelObjectProperty, value); }
        }

        public static readonly DependencyProperty ModelObjectProperty =
            DependencyProperty.Register("ModelObject", typeof(TcoDataExchangeDisplayView), typeof(TcoDataExchangeSimpleSelectorView),
                new PropertyMetadata(default(TcoDataExchangeDisplayView), new PropertyChangedCallback(ModelObjectChanged)));

        private static void ModelObjectChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var uc = ((TcoDataExchangeSimpleSelectorView)obj);

            if (uc.ModelObject != null)
            {
                uc.DataContext = new TcoDataExchangeSimpleSelectorViewModel() { Model = uc.ModelObject };
            }
        }

        private dynamic _context
        {
            get
            {
                return this.DataContext;
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (this.Visibility == Visibility.Visible && _context is TcoDataExchangeSimpleSelectorViewModel)
                {
                    var maxNumberOfRecipies = 50;
                    var currentLimit = ((TcoDataExchangeSimpleSelectorViewModel)_context).DataViewModel.Limit;
                    ((TcoDataExchangeSimpleSelectorViewModel)_context).DataViewModel.Limit = maxNumberOfRecipies;
                    ((TcoDataExchangeSimpleSelectorViewModel)_context).DataViewModel.FillObservableRecords();
                    ((TcoDataExchangeSimpleSelectorViewModel)_context).DataViewModel.Limit = currentLimit;

                }
            }
            catch (Exception)
            {

                //++ Ignore
            }         
        }
    }
}
