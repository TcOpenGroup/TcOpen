using Tco.Wpf.DynamicTree.DataTemplates;
using System;
using System.Windows;
using System.Windows.Controls;
using Vortex.Connector;

namespace inxton.vortex.framework.dynamictreeview.wpf.sandbox
{

    public class ChildDataTemplateSelector : DataTemplateSelector
    {        
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            switch (item)
            {
                case IValueTag a:
                    return element.FindResource("ValueTagTemplate") as DataTemplate;                 
                case IVortexObject a:
                    return element.FindResource("VortexObjectTemplate") as DataTemplate;
                case Array a:
                    return element.FindResource("ArrayMemberTemplate") as DataTemplate;                    
                default:
                    return new DataTemplate();
            }             
        }
    }

}
