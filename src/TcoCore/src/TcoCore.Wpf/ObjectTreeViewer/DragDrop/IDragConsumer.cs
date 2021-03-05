using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace Tco.Wpf.DynamicTree.DataTemplates
{
    interface IDragConsumer
    {
        void DragEnter(object sender, DragEventArgs e);
        void DragDrop(object sender, DragEventArgs e);
    }
}
