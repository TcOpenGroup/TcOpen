using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Vortex.Connector;

namespace Tco.Wpf.DynamicTree.DataTemplates
{
    public partial class ValueTag : IDraggable
    {
        private DragDropPublisher dragdrop;
        public ValueTag()
        {
            dragdrop = new DragDropPublisher(this);
        }
        public void DragMouseLeftButtonDown(object sender, MouseEventArgs onMouseDown) => dragdrop.StoreMouseClick(onMouseDown);

        public void DragMouseMove(object sender, MouseEventArgs e)
        {
            if (dragdrop.IsDragging(e))
            {
                var dragingValueTag = sender
                    .As<FrameworkElement>()
                    .DataContext
                    .As<IValueTag>();

                dragdrop.DoDragDrop(sender, dragingValueTag);
            }
        }

        public static Type PublishType() => typeof(IValueTag);
    }
}
