using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;


namespace TcOpen.Inxton.TcoIo.Wpf.Diagnostics.EtherCAT.Display
{
    public partial class StringDisplaySlim : INotifyPropertyChanged
    {
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(StringDisplaySlim), new PropertyMetadata(OnDescriptionChangedCallBack));

        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(uint), typeof(StringDisplaySlim), new PropertyMetadata(OnStateChangedCallBack));
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        private static void OnDescriptionChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            StringDisplaySlim c = sender as StringDisplaySlim;
            if (c != null)
            {
                c.OnDescriptionChanged();
                c.DescriptionChange(sender, e);
            }
        }

        protected virtual void OnDescriptionChanged()
        {
            OnPropertyChanged("Description");
        }

        public uint State
        {
            get { return (uint)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        private static void OnStateChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            StringDisplaySlim c = sender as StringDisplaySlim;
            if (c != null)
            {
                c.OnStateChanged();
                c.StateChange(sender, e);
            }
        }

        protected virtual void OnStateChanged()
        {
            OnPropertyChanged("State");
        }

        public StringDisplaySlim()
        {
            InitializeComponent();
            DataContextChanged += DataContextChange;
        }
        private void DataContextChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            StringDisplaySlim c = sender as StringDisplaySlim;
            if (c != null)
            {
                c.OnStateChanged();
                c.StateChange(sender, e);
            }
            Binding binding = tbValue.GetBindingExpression(TextBox.TextProperty).ParentBinding;
            
            string formatString = DataContext as string;
            if (!string.IsNullOrEmpty(formatString))
            {
                Binding b = new Binding
                {
                    Source = DataContext,
                    Mode = binding.Mode,
                    StringFormat = formatString
                };
                tbValue.SetBinding(TextBox.TextProperty, b);
            }
            else
            {
                dynamic dc;
                if (DataContext != null)
                {
                    dc = DataContext;
                    if (dc.Cyclic != null)
                    {
                        formatString = dc.Cyclic.ToString();
                        Binding b = new Binding
                        {
                            Path = new PropertyPath("Cyclic"),
                            Source = DataContext,
                            Mode = binding.Mode,
                            StringFormat = formatString
                        };
                        tbValue.SetBinding(TextBox.TextProperty, b);
                    }
                    if (dc.AttributeName != null)
                    {
                        formatString = dc.AttributeName;
                        Binding b = new Binding
                        {
                            Path = new PropertyPath("AttributeName"),
                            Source = DataContext,
                            Mode = binding.Mode,
                            StringFormat = formatString
                        };
                        tbDescription.SetBinding(TextBox.TextProperty, b);
                    }
                }
            }
        }

        private void DescriptionChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            Binding binding = tbDescription.GetBindingExpression(TextBox.TextProperty).ParentBinding;
            string formatString = Description as string;
            if (!string.IsNullOrEmpty(formatString))
            {
                Binding b = new Binding
                {
                    Source = Description,
                    Mode = binding.Mode,
                    StringFormat = formatString
                };
                tbDescription.SetBinding(TextBox.TextProperty, b);
            }
            else
            {
                dynamic dc;
                if (DataContext != null)
                {
                    dc = DataContext;
                    if (dc.AttributeName != null)
                    {
                        formatString = dc.AttributeName;
                        Binding b = new Binding
                        {
                            Path = new PropertyPath("AttributeName"),
                            Source = DataContext,
                            Mode = binding.Mode,
                            StringFormat = formatString
                        };
                        tbDescription.SetBinding(TextBox.TextProperty, b);
                    }
                }
            }
        }

        private void StateChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            ResourceDictionary ColorResorce = new ResourceDictionary();
            ColorResorce.Source = new Uri("/TcOpen.Inxton.TcoIo.Wpf;component/diagnostics/ethercat/colors/colors.xaml", UriKind.RelativeOrAbsolute);
            SolidColorBrush BackgroundColor = new SolidColorBrush();
            SolidColorBrush ForegroundColor = new SolidColorBrush();

            if ((ushort)State != 8)
            {
                BackgroundColor = new SolidColorBrush((Color)ColorResorce["ErrorColor"]);
                ForegroundColor = new SolidColorBrush((Color)ColorResorce["OnErrorColor"]);
            }
            else
            {
                BackgroundColor = new SolidColorBrush((Color)ColorResorce["InxtonGrayLightColor"]);
                ForegroundColor = new SolidColorBrush((Color)ColorResorce["OnSurfaceColor"]);
            }

            //tbDescription.SetBinding(TextBox.BackgroundProperty, new Binding() {Source= State, Converter =  TcoIo.InfoDataStateToForeground });
            //tbDescription.SetBinding(TextBox.ForegroundProperty, ForegroundColor);
            //tbValue.SetBinding(TextBox.BackgroundProperty, BackgroundColor);
            //tbValue.SetBinding(TextBox.ForegroundProperty, ForegroundColor);
            //tbDescription.SetValue(TextBox.BackgroundProperty, BackgroundColor);
            //tbDescription.SetValue(TextBox.ForegroundProperty, ForegroundColor);
            //tbValue.SetValue(TextBox.BackgroundProperty, BackgroundColor);
            //tbValue.SetValue(TextBox.ForegroundProperty, ForegroundColor);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
