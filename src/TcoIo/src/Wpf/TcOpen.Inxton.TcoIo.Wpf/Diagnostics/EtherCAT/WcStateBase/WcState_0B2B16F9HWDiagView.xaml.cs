using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace TcoIo
{

    public partial class WcState_0B2B16F9HWDiagView : UserControl, INotifyPropertyChanged
    {

        public static readonly DependencyProperty ChildsForegroundProperty = DependencyProperty.Register("ChildsForeground", typeof(Brush), typeof(WcState_0B2B16F9HWDiagView), new PropertyMetadata(OnChildsForegroundCallBack));

        public Brush ChildsForeground
        {
            get { return (Brush)GetValue(ChildsForegroundProperty); }
            set
            {
                this.Dispatcher.Invoke(() => SetValue(ChildsForegroundProperty, value));
            }
        }

        private static void OnChildsForegroundCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            WcState_0B2B16F9HWDiagView c = sender as WcState_0B2B16F9HWDiagView;
            if (c != null)
            {
                c.OnChildsForegroundChanged();
                c.ChildsForegroundChange(sender, e);
            }
        }

        protected virtual void OnChildsForegroundChanged()
        {
            OnPropertyChanged("ChildsForeground");
        }
        private void ChildsForegroundChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            tbAttributeName.Foreground = e.NewValue as Brush;
            tbAttributeName.Foreground = e.NewValue as Brush;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public WcState_0B2B16F9HWDiagView()
        {
            InitializeComponent();
        }

    }
}
