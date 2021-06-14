
using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;
using localized = TcOpen.Inxton.Data.Wpf.Properties.strings;

namespace TcoData
{
    public class HumanizeDateConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object ovalue, Type targetType, object parameter, CultureInfo culture)
        {
            var value = (DateTime)ovalue ;
            var approximate = true;
            StringBuilder sb = new StringBuilder();

            string suffix =" " + ((value > DateTime.Now) ? localized.FromNow : localized.Ago);

            TimeSpan timeSpan = new TimeSpan(Math.Abs(DateTime.Now.Subtract(value).Ticks));

            if (timeSpan.Days > 30)
            {
                sb.AppendFormat("{0} {1}", Math.Round(timeSpan.Days/30.0),
                  (timeSpan.Days > 1) ? localized.Months : localized.Month);
                if (approximate) return sb.ToString() + suffix;
            }

            if (timeSpan.Days > 0)
            {
                sb.AppendFormat("{0} {1}", timeSpan.Days,
                  (timeSpan.Days > 1) ? localized.Days : localized.Day);
                if (approximate) return sb.ToString() + suffix;
            }
            if (timeSpan.Hours > 0)
            {
                sb.AppendFormat("{0}{1} {2}", (sb.Length > 0) ? ", " : string.Empty,
                  timeSpan.Hours, (timeSpan.Hours > 1) ? localized.Hours : localized.Hour);
                if (approximate) return sb.ToString() + suffix;
            }
            if (timeSpan.Minutes > 0)
            {
                sb.AppendFormat("{0}{1} {2}", (sb.Length > 0) ? ", " : string.Empty,
                  timeSpan.Minutes, (timeSpan.Minutes > 1) ? localized.Minute : localized.Minute);
                if (approximate) return sb.ToString() + suffix;
            }
            if (timeSpan.Seconds > 0)
            {
                sb.AppendFormat("{0}{1} {2}", (sb.Length > 0) ? ", " : string.Empty,
                  timeSpan.Seconds, (timeSpan.Seconds > 1) ? localized.Seconds : localized.Second);
                if (approximate) return sb.ToString() + suffix;
            }
            if (sb.Length == 0) return localized.RightNow;

            sb.Append(suffix);
            return sb.ToString();
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

}
