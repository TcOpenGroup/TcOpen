using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using TcoIo;

namespace TcoIo.Diagnostics.EtherCAT.Display
{
    public partial class AmsNetIdDisplay 
    {
        public AmsNetIdDisplay()
        {
            InitializeComponent();
            DataContextChanged += DataContextChange;
        }
        private void DataContextChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            Binding binding = tbValue.GetBindingExpression(TextBox.TextProperty).ParentBinding;

            TcoAmsNetId tcoAmsNetId = DataContext as TcoAmsNetId;

            string formatString = string.Format("{0}.{1}.{2}.{3}.{4}.{5}", tcoAmsNetId.netId[0].Synchron.ToString(),
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
            gbDescription.SetValue(GroupBox.HeaderProperty, tcoAmsNetId.AttributeName);
        }
    }
}
