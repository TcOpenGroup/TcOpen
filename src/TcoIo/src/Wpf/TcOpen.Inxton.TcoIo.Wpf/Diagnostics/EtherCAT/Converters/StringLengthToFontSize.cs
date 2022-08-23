using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace TcoIo.Converters
{
    public class StringLengthToFontSize : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TextBlock origTextBlock = value as TextBlock;
            TextBlock defTextBlock = new TextBlock();
            string sWidthFromParameter = parameter?.ToString();
            bool parameterIsNumber = double.TryParse(sWidthFromParameter, out double widthFromParameter);
            bool maxWidthIsNumber = false;
            double widthFromMaxWidth = 0;
            if (!parameterIsNumber || widthFromParameter <= 0)
            {
                string sWidthFromMaxWidth = origTextBlock.MaxWidth.ToString();
                maxWidthIsNumber = double.TryParse(sWidthFromMaxWidth, out widthFromMaxWidth);
            }
            bool widthDefined = false;
            double width = 0.0;
            if (parameterIsNumber && widthFromParameter > 0)
            {
                widthDefined = true;
                width = widthFromParameter;
            }
            else if(maxWidthIsNumber && widthFromMaxWidth > 0)
            {
                widthDefined = true;
                width = widthFromMaxWidth;
            }
            if (value.GetType().Equals(typeof(TextBlock)) && widthDefined)
            {
                double origFontSize = origTextBlock.FontSize;
                Size size = MeasureString(origTextBlock);
                
                if (size.Width> width)
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
                return defTextBlock.FontSize;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        private Size MeasureString(TextBlock textBlock)
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
    }
}
