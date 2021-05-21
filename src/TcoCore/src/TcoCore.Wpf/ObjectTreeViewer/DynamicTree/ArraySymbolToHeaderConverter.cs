using System;
using System.Globalization;
using System.Linq;
using Vortex.Connector;
using Vortex.Presentation.Wpf.Converters;

namespace inxton.vortex.framework.dynamictreeview.wpf.sandbox
{

    public class ArraySymbolToHeaderConverter : BaseConverter
    {
        public override object ToConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Array array)
            {
                //i can't get the first value of the array, so I'll try to iterate and return the first element
                foreach (var item in array)
                {
                    if (item is IValueTag vortexObject)
                        return RemoveArrayBrackets(vortexObject);
                }
            }
            return value;
        }

        private string RemoveArrayBrackets(IValueTag vortexObject)
        {
            var symbol = vortexObject.Symbol;
            var bracketPosition = symbol.IndexOf('[');
            return symbol.Substring(0, bracketPosition);
        }

    }


    public class ArrayToStringConverter : BaseConverter
    {
        public override object ToConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           if(value is IValueTag[] values)
            {
                return string.Join(",",values.Select(x => ((dynamic)x).Cyclic));
            }
            return "nope";
        }
    }
}
