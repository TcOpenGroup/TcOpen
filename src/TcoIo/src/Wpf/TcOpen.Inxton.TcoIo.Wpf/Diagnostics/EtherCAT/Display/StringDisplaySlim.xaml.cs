using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;


namespace TcoIo.Diagnostics.EtherCAT.Display
{
    public partial class StringDisplaySlim : INotifyPropertyChanged
    {
        /******************************************/
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(StringDisplaySlim), new PropertyMetadata(OnDescriptionChangedCallBack));
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

        /******************************************/
        /******************************************/
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(ushort), typeof(StringDisplaySlim), new PropertyMetadata(OnStateChangedCallBack));

        public ushort State
        {
            get { return (ushort)GetValue(StateProperty); }
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

        private void StateChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Binding binding = tbValue.GetBindingExpression(TextBox.BackgroundProperty).ParentBinding;

            //string formatString = DataContext as string;
            //if (!string.IsNullOrEmpty(formatString))
            //{
            //    Binding b = new Binding
            //    {
            //        Source = DataContext,
            //        Mode = binding.Mode,
            //        StringFormat = formatString
            //    };
            //    tbValue.SetBinding(TextBox.TextProperty, b);
            //}
            //State = 8;
            tbDescription.SetBinding(TextBox.ForegroundProperty, new Binding() { Source = State, Converter = new Converters.InfoDataStateToForeground(), Mode = BindingMode.OneWay });
            tbValue.SetBinding(TextBox.BackgroundProperty, new Binding() { Source = State, Converter = new Converters.InfoDataStateToBackground(), Mode = BindingMode.OneWay });
            tbDescription.SetBinding(TextBox.BackgroundProperty, new Binding() { Source = State, Converter = new Converters.InfoDataStateToBackground(), Mode = BindingMode.OneWay });
            tbValue.SetBinding(TextBox.ForegroundProperty, new Binding() { Source = State, Converter = new Converters.InfoDataStateToForeground(), Mode = BindingMode.OneWay });
        }

        /******************************************/
        /******************************************/
        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register("BackgroundColor", typeof(Brush), typeof(StringDisplaySlim), new PropertyMetadata(OnBackgroundColorChangedCallBack));
        public Brush BackgroundColor
        {
            get { return (Brush)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        private static void OnBackgroundColorChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            StringDisplaySlim c = sender as StringDisplaySlim;
            if (c != null)
            {
                c.OnBackgroundColorChanged();
                c.BackgroundColorChange(sender, e);
            }
        }

        protected virtual void OnBackgroundColorChanged()
        {
            OnPropertyChanged("BackgroundColor");
        }

        private void BackgroundColorChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            int x = 6;
            //Binding binding = tbValue.GetBindingExpression(TextBox.BackgroundProperty).ParentBinding;

            //string formatString = DataContext as string;
            //if (!string.IsNullOrEmpty(formatString))
            //{
            //    Binding b = new Binding
            //    {
            //        Source = DataContext,
            //        Mode = binding.Mode,
            //        StringFormat = formatString
            //    };
            //    tbValue.SetBinding(TextBox.TextProperty, b);
            //}
            //State = 8;
            //tbDescription.SetBinding(TextBox.ForegroundProperty, new Binding() { Source = State, Converter = new Converters.InfoDataStateToForeground(), Mode = BindingMode.OneWay });
            //tbValue.SetBinding(TextBox.BackgroundProperty, new Binding() { Source = State, Converter = new Converters.InfoDataStateToBackground(), Mode = BindingMode.OneWay });
            //tbDescription.SetBinding(TextBox.BackgroundProperty, new Binding() { Source = State, Converter = new Converters.InfoDataStateToBackground(), Mode = BindingMode.OneWay });
            //tbValue.SetBinding(TextBox.ForegroundProperty, new Binding() { Source = State, Converter = new Converters.InfoDataStateToForeground(), Mode = BindingMode.OneWay });
        }

        /******************************************/
        public StringDisplaySlim()
        {
            InitializeComponent();
            DataContextChanged += DataContextChange;
        }
        private void DataContextChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            //StringDisplaySlim c = sender as StringDisplaySlim;
            //if (c != null)
            //{
            //    c.OnStateChanged();
            //    c.StateChange(sender, e);
            //}
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

 
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
