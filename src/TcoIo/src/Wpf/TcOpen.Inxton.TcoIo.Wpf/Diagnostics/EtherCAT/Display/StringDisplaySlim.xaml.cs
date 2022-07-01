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
