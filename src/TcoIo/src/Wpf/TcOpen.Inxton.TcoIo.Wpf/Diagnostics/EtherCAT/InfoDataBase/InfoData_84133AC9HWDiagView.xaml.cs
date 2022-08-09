using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for fbSampleComponentView.xaml
    /// </summary>
    public partial class InfoData_84133AC9HWDiagView : UserControl/*,INotifyPropertyChanged*/
    {
        //private Brush foregroundBrush;

        //public Brush ForegroundBrush
        //{
        //    get { return foregroundBrush; }
        //    set { foregroundBrush = value; }
        //}

        public InfoData_84133AC9HWDiagView()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        //public static readonly DependencyProperty ChildsForegroundProperty = DependencyProperty.Register("ChildsForeground", typeof(Brush), typeof(InfoData_84133AC9HWDiagView), new PropertyMetadata(OnChildsForegroundCallBack));
        //public Brush ChildsForeground
        //{
        //    get { return (Brush)GetValue(ChildsForegroundProperty); }
        //    set
        //    {
        //        this.Dispatcher.Invoke(() => SetValue(ChildsForegroundProperty, value));
        //    }
        //}

        //private static void OnChildsForegroundCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        //{
        //    InfoData_84133AC9HWDiagView c = sender as InfoData_84133AC9HWDiagView;
        //    if (c != null)
        //    {
        //        c.OnChildsForegroundChanged();
        //        c.ChildsForegroundChange(sender, e);
        //    }
        //}

        //protected virtual void OnChildsForegroundChanged()
        //{
        //    OnPropertyChanged("ChildsForeground");
        //}
        //private void ChildsForegroundChange(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    dispChangeCount.ChildsForeground = e.NewValue as Brush;
        //    dispDevId.ChildsForeground = e.NewValue as Brush;
        //    dispAmsNetId.ChildsForeground = e.NewValue as Brush;
        //    dispCfgSlaveCount.ChildsForeground = e.NewValue as Brush;
        //    dispDcToTcTimeOffset.ChildsForeground = e.NewValue as Brush;
        //    dispDcToExtTimeOffset.ChildsForeground = e.NewValue as Brush;
        //}


        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    }
}
