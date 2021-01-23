using Tco.Wpf.DynamicTree.DataTemplates;
using System.Windows;
using System.Windows.Input;

namespace Tco.Wpf.CustomTree.DataTemplates
{
    public partial class TreeItemTemplate : IDraggable
    {
        private DragDropPublisher Dragdrop { get; set; }
        public TreeItemTemplate()
        {
            Dragdrop = new DragDropPublisher(this);
        }

        public void DragMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            Dragdrop.StoreMouseClick(e);
        }

        public void DragMouseMove(object sender, MouseEventArgs e)
        {
            if (Dragdrop.IsDragging(e))
            {
                var data = sender.As<FrameworkElement>().DataContext;
                Dragdrop.DoDragDrop(sender, data);
            }
        }
    }
}
