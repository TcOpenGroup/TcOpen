using System;
using System.Windows;
using System.Windows.Input;

namespace Tco.Wpf.DynamicTree.DataTemplates
{
    public interface IDraggable
    {
        void DragMouseMove(object sender, MouseEventArgs e);

        /// <summary>
        /// Forces you to store the first posistion of mouse before dragging
        /// dragdrop.StoreMouseClick(e);
        /// Otherwise Drag won't work.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DragMouseLeftButtonDown(object sender, MouseEventArgs e);
    }
}
