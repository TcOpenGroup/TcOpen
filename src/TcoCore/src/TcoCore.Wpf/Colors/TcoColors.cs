using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TcoCore.Wpf
{
    /// <summary>
    /// Base color pallete is inspired by https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/wiki/Brush-Names
    /// It's a facade of MaterialDesignXaml color system.
    /// </summary>
    public class TcoColors
    {

        public static Brush Primary => GetDynamicBrush($"PrimaryHueMidBrush", fallBackBrush: BrushFromHex("#5a7785"));
        public static Brush OnPrimary => GetDynamicBrush($"PrimaryHueMidForegroundBrush", fallBackBrush: Brushes.White);

        //public static Brush Secondary => GetDynamicBrush($"SecondaryHueMidBrush", fallBackBrush: BrushFromHex("#bc5052"));
        //public static Brush OnSecondary => GetDynamicBrush($"SecondaryHueMidForegroundBrush", fallBackBrush: Brushes.White);

        public static Brush Accent => GetDynamicBrush($"SecondaryHueMidBrush", fallBackBrush: Brushes.OrangeRed);
        public static Brush OnAccent => GetDynamicBrush($"SecondaryHueMidForegroundBrush", fallBackBrush: Brushes.White);

        public static Brush Alert => GetDynamicBrush($"SecondaryHueDarkBrush", fallBackBrush: BrushFromHex("#bc5052"));
        public static Brush OnAlert => GetDynamicBrush($"SecondaryHueDarkForegroundBrush", fallBackBrush: Brushes.White);

        public static Brush Error => GetDynamicBrush("MaterialDesignValidationErrorBrush", fallBackBrush: BrushFromHex("#dd2c00"));
        public static Brush OnError => GetDynamicBrush("MaterialDesignDarkForeground", fallBackBrush: BrushFromHex("#dd2c00"));



        public static Brush Debug => GetMaterialDesignBackgrondBrush(MaterialDesignColor.Green500);
        public static Brush Trace => GetMaterialDesignBackgrondBrush(MaterialDesignColor.Green600);
        public static Brush Info => GetMaterialDesignBackgrondBrush(MaterialDesignColor.Green700);
        public static Brush TimedOut => GetMaterialDesignBackgrondBrush(MaterialDesignColor.Green800);
        public static Brush Notification => GetMaterialDesignBackgrondBrush(MaterialDesignColor.Green900);
        public static Brush Warning => GetMaterialDesignBackgrondBrush(MaterialDesignColor.Yellow700);
        public static Brush Errors => GetMaterialDesignBackgrondBrush(MaterialDesignColor.Red600);
        public static Brush ProgrammingError => GetMaterialDesignBackgrondBrush(MaterialDesignColor.Red700);
        public static Brush Critical => GetMaterialDesignBackgrondBrush(MaterialDesignColor.Red800);
        public static Brush Catastrophic => GetMaterialDesignBackgrondBrush(MaterialDesignColor.Red900);

        public static Brush OnDebug => GetMaterialDesignForegroundBrush(MaterialDesignColor.Green500);
        public static Brush OnTrace => GetMaterialDesignForegroundBrush(MaterialDesignColor.Green600);
        public static Brush OnInfo => GetMaterialDesignForegroundBrush(MaterialDesignColor.Green700);
        public static Brush OnTimedOut => GetMaterialDesignForegroundBrush(MaterialDesignColor.Green800);
        public static Brush OnNotification => GetMaterialDesignForegroundBrush(MaterialDesignColor.Green900);
        public static Brush OnWarning => GetMaterialDesignForegroundBrush(MaterialDesignColor.Yellow700);
        public static Brush OnErrors => GetMaterialDesignForegroundBrush(MaterialDesignColor.Red600);
        public static Brush OnProgrammingError => GetMaterialDesignForegroundBrush(MaterialDesignColor.Red700);
        public static Brush OnCritical => GetMaterialDesignForegroundBrush(MaterialDesignColor.Red800);
        public static Brush OnCatastrophic => GetMaterialDesignForegroundBrush(MaterialDesignColor.Red900);



        private static Brush GetDynamicBrush(string brushResourceKey, Brush fallBackBrush) =>
            Application.Current?.TryFindResource(brushResourceKey)?.As<Brush>() ?? fallBackBrush;


        private static Brush GetMaterialDesignForegroundBrush(MaterialDesignColor brushResourceKey)
        {
            Color color;
            SwatchHelper.Lookup.TryGetValue(brushResourceKey, out color);
            return new SolidColorBrush(new ColorPair(color).GetForegroundColor());
    }

        private static Brush GetMaterialDesignBackgrondBrush(MaterialDesignColor brushResourceKey)
        {
            Color color;
            SwatchHelper.Lookup.TryGetValue(brushResourceKey ,out color);
            return new SolidColorBrush(color);
        }


        private static Color GetDynamicColor(string colorKey, Color fallbackColor)
        {
            var foundColor = Application.Current.TryFindResource(colorKey);
            if (foundColor != null && foundColor is Color color)
            {
                return color;
            }
            else
            {
                return fallbackColor;
            }

        }

        private static Brush BrushFromHex(string hex) => new SolidColorBrush(ColorFromHex(hex)).Apply(x => x.Freeze());
        private static Color ColorFromHex(string hex) => (Color)ColorConverter.ConvertFromString(hex);
    }

    public class TcoResources : ResourceDictionary
    {
        public TcoResources()
        {
            AddTcoColors();
            UseDefaultGroupBox();
        }

        private void UseDefaultGroupBox()
        {
            var groupbox = new GroupBox();
            var defualtStyle = new Style(typeof(GroupBox), groupbox.Style);
            Add(typeof(GroupBox), defualtStyle);
        }

        private void AddTcoColors() => typeof(TcoColors)
                .GetProperties()
                .Where(x => x.PropertyType == typeof(Brush))
                .ForEach(x => Add(x.Name, x.GetValue(null)));
    }

    internal static class ObjectExtensions
    {
        public static T As<T>(this object @this) where T : class => (@this as T);
        public static T Apply<T>(this T @this, Action<T> func)
        {
            func.Invoke(@this);
            return @this;
        }

        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> predicate)
        {
            foreach (var item in source)
            {
                predicate.Invoke(item);
            }
        }

    }
}
