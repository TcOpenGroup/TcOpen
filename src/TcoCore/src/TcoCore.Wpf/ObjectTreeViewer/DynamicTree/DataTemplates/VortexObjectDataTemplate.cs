using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using Vortex.Connector;

namespace Tco.Wpf.DynamicTree.DataTemplates
{
    public partial class VortexObjectDataTemplate : IDraggable
    {
        public VortexObjectDataTemplate()
        {
            dragDropPublisher = new DragDropPublisher(this);
        }
        private DragDropPublisher dragDropPublisher { get; set; }
        public void DragMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            dragDropPublisher.StoreMouseClick(e);
        }

        public void DragMouseMove(object sender, MouseEventArgs e)
        {
            if (dragDropPublisher.IsDragging(e))
            {
                var data = sender.As<FrameworkElement>()
                    .DataContext.As<IVortexObject>();
                dragDropPublisher.DoDragDrop(sender, data);
            }
        }

        public Type PublishType() => typeof(IVortexObject);
    }

    public class SymbolOrHumanReadableConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vob = value as IVortexObject;
            if (vob == null) return value;
            return string.IsNullOrEmpty(vob.HumanReadable) ? vob.Symbol : vob.HumanReadable;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
