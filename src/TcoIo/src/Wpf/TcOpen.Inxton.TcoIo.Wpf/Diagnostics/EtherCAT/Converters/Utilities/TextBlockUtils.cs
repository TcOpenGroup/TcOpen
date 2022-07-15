using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace TcoIo.Converters.Utilities
{
    public static class TextBlockUtils
    {
        private static Size MeasureString(TextBlock textBlock)
        {

            var formattedText = new FormattedText(
                textBlock.Text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(textBlock.FontFamily, textBlock.FontStyle, textBlock.FontWeight, textBlock.FontStretch),
                textBlock.FontSize,
                Brushes.Black,
                new NumberSubstitution(),
                1);

            return new Size(formattedText.Width, formattedText.Height);
        }


        public static double UpdateFontSizeToFitTheTextBlockMaxWidth(TextBlock textBlock)
        {
            TextBlock defaultTextBlock = new TextBlock();

            if (double.TryParse(textBlock.MaxWidth.ToString(), out double width))
            {
                double origFontSize = textBlock.FontSize;
                Size size = MeasureString(textBlock);

                if (size.Width > width)
                {
                    double fontSize = width / size.Width * origFontSize;
                    //just to check the final dims after font change during debug
                    //TextBlock textBlock2 = value as TextBlock;
                    //textBlock2.FontSize = fontSize;
                    //Size size2 = MeasureString(textBlock2);

                    return fontSize;
                }
                else
                {
                    return origFontSize;
                }
            }
            else
            {
                return defaultTextBlock.FontSize;
            }
        }
    }
}
