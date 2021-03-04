using Tco.Wpf.DynamicTree.DataTemplates;
using System;
using System.Windows;
using System.Windows.Input;

namespace Tco.Wpf
{
    class DragDropPublisher
    {
        public DragDropPublisher(IDraggable source)
        {
            TypeOfSource = source.GetType();
        }

        public Type TypeOfSource { get; set; }
        private Point StartPoint { get; set; }
        public void StoreMouseClick(MouseEventArgs e) => StartPoint = e.GetPosition(null);

        public bool IsDragging(MouseEventArgs e)
        {
            if (StartPoint == new Point(0, 0)) return false;

            Point mousePos = e.GetPosition(null);
            Vector diff = StartPoint - mousePos;

            return e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance;
        }

        public void DoDragDrop(object sender, object data)
        {
            DragDrop.DoDragDrop(sender.As<FrameworkElement>(), new DataObject(TypeOfSource, data), DragDropEffects.Move);
        }
    }

}
