using System;
using System.Collections.Generic;
using System.Windows;

namespace TcoCognexVision
{
    public enum eDisplayFormat { Array_of_decimals, Array_of_hexdecimals, String };

    /// <summary>
    /// Interaction logic for DisplayFormatDialog.xaml
    /// </summary>
    public partial class DisplayFormatDialog : Window
    {
        private List<String> _displayFormats = new List<string> { "array of decimal numbers", "array of hexadecimal numbers", "string" };

        private eDisplayFormat _currentDisplayFormat;

        public eDisplayFormat CurrentDisplayFormat
        {
            get
            {
                return _currentDisplayFormat;
            }

            set
            {
                _currentDisplayFormat = value;
            }
        }
        public DisplayFormatDialog(eDisplayFormat initDisplayFormat)
        {
            InitializeComponent();
            this.cbRequiredFormat.ItemsSource = _displayFormats;
            this.cbRequiredFormat.SelectedIndex = (int)initDisplayFormat;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            int index = this.cbRequiredFormat.SelectedIndex;
            CurrentDisplayFormat = (eDisplayFormat) index;
            this.Close();
        }

        private void btnDialogCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbRequiredFormat_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.IsVisible) 
            {             
                int index = this.cbRequiredFormat.SelectedIndex;
                CurrentDisplayFormat = (eDisplayFormat)index;
                this.Close();
            }
        }
    }
}
