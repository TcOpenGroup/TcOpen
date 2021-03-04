using Tco.Wpf.DynamicTree.DataTemplates;
using System;
using System.Windows;
using System.Windows.Input;

namespace Tco.Wpf
{
    class DragDropConsumer
    {
    }

    public static class DragExtension
    {
        public static object GetFrom<T>(this DragEventArgs dragEvent) where T : IDraggable
        {
            return dragEvent.Data.GetData(typeof(T));
        }
    }

}
