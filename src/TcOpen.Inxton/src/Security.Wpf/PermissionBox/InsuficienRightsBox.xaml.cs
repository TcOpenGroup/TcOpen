﻿using System;
using System.Collections.Generic;
using System.Linq;
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

namespace TcOpen.Inxton.Local.Security.Wpf
{
    /// <summary>
    /// Interaction logic for InsufficientRightsBox.xaml
    /// </summary>
    public partial class InsufficientRightsBox : UserControl
    {
        public InsufficientRightsBox()
        {
            InitializeComponent();
        }

        internal InsufficientRightsBoxViewModel ViewModel { get { return this.DataContext as InsufficientRightsBoxViewModel; } } 
    }
}
