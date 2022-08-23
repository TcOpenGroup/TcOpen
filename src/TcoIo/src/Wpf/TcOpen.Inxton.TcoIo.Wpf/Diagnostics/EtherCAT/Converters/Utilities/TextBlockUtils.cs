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

        private static Size MeasureString(TextBox textBox)
        {

            var formattedText = new FormattedText(
                textBox.Text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(textBox.FontFamily, textBox.FontStyle, textBox.FontWeight, textBox.FontStretch),
                textBox.FontSize,
                Brushes.Black,
                new NumberSubstitution(),
                1);

            return new Size(formattedText.Width, formattedText.Height);
        }

        public static double UpdateFontSizeToFitTheTextBoxMaxWidth(TextBox textBox)
        {
            TextBox defaultTextBox = new TextBox();

            if (double.TryParse(textBox.MaxWidth.ToString(), out double width))
            {
                double origFontSize = textBox.FontSize;
                Size size = MeasureString(textBox);

                if (size.Width > width)
                {
                    double fontSize = (width - 5 ) / size.Width * origFontSize;
                    //just to check the final dims after font change during debug
                    //TextBox textBox2 = textBox as TextBox;
                    //textBox2.FontSize = fontSize;
                    //Size size2 = MeasureString(textBox2);
                    return fontSize;
                }
                else
                {
                    return origFontSize;
                }
            }
            else
            {
                return defaultTextBox.FontSize;
            }
        }
    }
}
