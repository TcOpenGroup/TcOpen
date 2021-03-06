﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    public class Pwds : Freezable
    {
        public PasswordBox Pb1
        {
            get { return (PasswordBox)GetValue(Pb1Property); }
            set { SetValue(Pb1Property, value); }
        }

        // Using a DependencyProperty as the backing store for Pb1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Pb1Property =
            DependencyProperty.Register("Pb1", typeof(PasswordBox), typeof(Pwds));




        public PasswordBox Pb2
        {
            get { return (PasswordBox)GetValue(Pb2Property); }
            set { SetValue(Pb2Property, value); }
        }

        // Using a DependencyProperty as the backing store for Pb2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Pb2Property =
            DependencyProperty.Register("Pb2", typeof(PasswordBox), typeof(Pwds));

        protected override Freezable CreateInstanceCore()
        {
            return new Pwds();
        }
    }
}
