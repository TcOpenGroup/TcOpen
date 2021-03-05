using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace Tco.Wpf
{
    public static class Extensions
    {
        public static T As<T>(this object @object, [CallerMemberName] string caller = null) where T : class
        {
            var conversion = @object as T;
            if (conversion is null)
                throw new InvalidCastException($"{caller} is {@object.GetType()} not {typeof(T)}");
            return @object as T;
        }

        public static T TryAs<T>(this object @object) where T : class => @object as T;

        public static void  Let<T>(this T @object, Action<T> action) => action(@object);

    }

    public static class VisualTreeExtension
    {
        public static T FindParentOfType<T>(this DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentDepObj = child;
            do
            {
                parentDepObj = VisualTreeHelper.GetParent(parentDepObj);
                T parent = parentDepObj as T;
                if (parent != null) return parent;
            }
            while (parentDepObj != null);
            return null;
        }
    }
}
