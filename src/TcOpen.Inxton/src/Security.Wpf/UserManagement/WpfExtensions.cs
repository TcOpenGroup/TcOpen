using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    internal static class WpfExtensions
    {
        public static ObservableCollection<T> ToObservable<T>(this IList<T> l) => new ObservableCollection<T>(l);

        public static void ReplaceWith<T>(this ObservableCollection<T> l, IList<T> replace) {
            l.Clear();
            replace.ToList().ForEach(l.Add);
        }

    }
}
