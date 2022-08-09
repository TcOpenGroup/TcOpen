using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using TcoIo;

namespace TcoIo.Diagnostics.EtherCAT.Display
{
    public partial class AmsNetIdDisplaySlim : Grid, INotifyPropertyChanged
    {
        public AmsNetIdDisplaySlim()
        {
            InitializeComponent();
            DataContextChanged += DataContextChange;
        }
        private void DataContextChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            Binding binding = tbValue.GetBindingExpression(TextBox.TextProperty).ParentBinding;

            TcoAmsNetId tcoAmsNetId = DataContext as TcoAmsNetId;

            string formatString = string.Format("{0}.{1}.{2}.{3}.{4}.{5}",  tcoAmsNetId.netId[0].Synchron.ToString(),
                                                                            tcoAmsNetId.netId[1].Synchron.ToString(),
                                                                            tcoAmsNetId.netId[2].Synchron.ToString(),
                                                                            tcoAmsNetId.netId[3].Synchron.ToString(),
                                                                            tcoAmsNetId.netId[4].Synchron.ToString(),
                                                                            tcoAmsNetId.netId[5].Synchron.ToString());
            if (string.IsNullOrEmpty(formatString)) return;
            var b = new Binding
            {
                Source = formatString,
                Mode = binding.Mode,
                StringFormat = formatString
            };
            tbValue.SetBinding(TextBox.TextProperty, b);
            tbDescription.SetValue(TextBox.TextProperty, tcoAmsNetId.AttributeName);
        }

        public static readonly DependencyProperty ChildsForegroundProperty = DependencyProperty.Register("ChildsForeground", typeof(Brush), typeof(AmsNetIdDisplaySlim), new PropertyMetadata(OnChildsForegroundCallBack));
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
            AmsNetIdDisplaySlim c = sender as AmsNetIdDisplaySlim;
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
            tbDescription.Foreground = e.NewValue as Brush;
            tbValue.Foreground = e.NewValue as Brush;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
