using System;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Assist
{
    public class ToLigthColorConverter :  IValueConverter
    {
        public ToLigthColorConverter()
        {

        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
          
            var x = (SolidColorBrush) SelectedTemplate ;
            var hslColor = new HslColor(x.Color);
            return new SolidColorBrush(hslColor.Lighten(-0.750).ToRgb());
        }

        private object SelectedTemplate { get => Application.Current.FindResource("Primary"); }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    internal static class ColorExt
    {
        public static System.Drawing.Color ToDrawingColor(this System.Windows.Media.Color colour)
            => System.Drawing.Color.FromArgb(colour.A, colour.R, colour.G, colour.B);

        public static System.Windows.Media.Color ToMediaColor(this System.Drawing.Color colour) 
            => System.Windows.Media.Color.FromArgb(colour.A, colour.R, colour.G, colour.B);

        public static SolidColorBrush ToLigthBrush(this SolidColorBrush brush)
        {
            return new SolidColorBrush(brush.Color);
            // return new SolidColorBrush(System.Windows.Forms.ControlPaint.DarkDark(brush.Color.ToDrawingColor()).ToMediaColor());
        }

    }


    public class HslColor
    {
        public readonly double h, s, l, a;

        public HslColor(double h, double s, double l, double a)
        {
            this.h = h;
            this.s = s;
            this.l = l;
            this.a = a;
        }

        public HslColor(System.Windows.Media.Color rgb)
        {
            RgbToHls(rgb.R, rgb.G, rgb.B, out h, out l, out s);
            a = rgb.A / 255.0;
        }

        public System.Windows.Media.Color ToRgb()
        {
            int r, g, b;
            HlsToRgb(h, l, s, out r, out g, out b);
            return System.Windows.Media.Color.FromArgb((byte)(a * 255.0), (byte)r, (byte)g, (byte)b);
        }

        public HslColor Lighten(double amount)
        {
            return new HslColor(h, s, Clamp(l * amount, 0, 1), a);
        }

        private static double Clamp(double value, double min, double max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;

            return value;
        }

        // Convert an RGB value into an HLS value.
        static void RgbToHls(int r, int g, int b,
            out double h, out double l, out double s)
        {
            // Convert RGB to a 0.0 to 1.0 range.
            double double_r = r / 255.0;
            double double_g = g / 255.0;
            double double_b = b / 255.0;

            // Get the maximum and minimum RGB components.
            double max = double_r;
            if (max < double_g) max = double_g;
            if (max < double_b) max = double_b;

            double min = double_r;
            if (min > double_g) min = double_g;
            if (min > double_b) min = double_b;

            double diff = max - min;
            l = (max + min) / 2;
            if (Math.Abs(diff) < 0.00001)
            {
                s = 0;
                h = 0;  // H is really undefined.
            }
            else
            {
                if (l <= 0.5) s = diff / (max + min);
                else s = diff / (2 - max - min);

                double r_dist = (max - double_r) / diff;
                double g_dist = (max - double_g) / diff;
                double b_dist = (max - double_b) / diff;

                if (double_r == max) h = b_dist - g_dist;
                else if (double_g == max) h = 2 + r_dist - b_dist;
                else h = 4 + g_dist - r_dist;

                h = h * 60;
                if (h < 0) h += 360;
            }
        }

        // Convert an HLS value into an RGB value.
        static void HlsToRgb(double h, double l, double s,
            out int r, out int g, out int b)
        {
            double p2;
            if (l <= 0.5) p2 = l * (1 + s);
            else p2 = l + s - l * s;

            double p1 = 2 * l - p2;
            double double_r, double_g, double_b;
            if (s == 0)
            {
                double_r = l;
                double_g = l;
                double_b = l;
            }
            else
            {
                double_r = QqhToRgb(p1, p2, h + 120);
                double_g = QqhToRgb(p1, p2, h);
                double_b = QqhToRgb(p1, p2, h - 120);
            }

            // Convert RGB to the 0 to 255 range.
            r = (int)(double_r * 255.0);
            g = (int)(double_g * 255.0);
            b = (int)(double_b * 255.0);
        }

        private static double QqhToRgb(double q1, double q2, double hue)
        {
            if (hue > 360) hue -= 360;
            else if (hue < 0) hue += 360;

            if (hue < 60) return q1 + (q2 - q1) * hue / 60;
            if (hue < 180) return q2;
            if (hue < 240) return q1 + (q2 - q1) * (240 - hue) / 60;
            return q1;
        }
    }
}
;