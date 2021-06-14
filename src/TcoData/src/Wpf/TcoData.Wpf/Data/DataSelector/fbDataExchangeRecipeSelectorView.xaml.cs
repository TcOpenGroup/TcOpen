using System;
using System.Windows;
using System.Windows.Controls;
using TcoData;

namespace TcoData
{
    /// <summary>
    /// Interaction logic for fbDataExchangeControlView.xaml
    /// </summary>
    public partial class fbDataExchangeRecipeSelectorView : UserControl
    {
        public fbDataExchangeRecipeSelectorView()
        {
            InitializeComponent();
        }
        public TcoDataExchangeDisplayView ModelObject
        {
            get { return (TcoDataExchangeDisplayView)GetValue(ModelObjectProperty); }
            set { SetValue(ModelObjectProperty, value); }
        }

        public static readonly DependencyProperty ModelObjectProperty =
            DependencyProperty.Register("ModelObject", typeof(TcoDataExchangeDisplayView), typeof(fbDataExchangeRecipeSelectorView),
                new PropertyMetadata(default(TcoDataExchangeDisplayView), new PropertyChangedCallback(ModelObjectChanged)));

        private static void ModelObjectChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var uc = ((fbDataExchangeRecipeSelectorView)obj);

            if (uc.ModelObject != null)
            {
                uc.DataContext = new fbDataExchangeRecipeSelectorViewModel() { Model = uc.ModelObject };
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
                if (this.Visibility == Visibility.Visible && _context is fbDataExchangeRecipeSelectorViewModel)
                {
                    var maxNumberOfRecipies = 50;
                    var currentLimit = ((fbDataExchangeRecipeSelectorViewModel)_context).DataViewModel.Limit;
                    ((fbDataExchangeRecipeSelectorViewModel)_context).DataViewModel.Limit = maxNumberOfRecipies;
                    ((fbDataExchangeRecipeSelectorViewModel)_context).DataViewModel.FillObservableRecords();
                    ((fbDataExchangeRecipeSelectorViewModel)_context).DataViewModel.Limit = currentLimit;

                }
            }
            catch (Exception)
            {

                //++ Ignore
            }         
        }
    }
}
