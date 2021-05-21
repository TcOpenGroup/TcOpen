using NUnit.Framework;
using System.Windows;
using System.Windows.Media;
using TcoCore.Wpf;

namespace TcoCoreUnitTests
{

    [Apartment(System.Threading.ApartmentState.STA)]
    public class TcoColorTest
    {

        [Test]
        public void If_color_is_not_found_fallback_to_default()
        {
            //Arrange
            //Act
            var primary = TcoColors.Primary as SolidColorBrush;
            //Assert
            Assert.AreEqual(primary.Color, FromHex("#5a7785"));
        }

        [Test]
        public void If_primary_color_is_from_material_design_use_it_instead()
        {
            //Arrange
            var app = new Application();
            var primaryColor = FromHex("#FF0047");
            Application.Current.Resources.Add("PrimaryHueLightBrush", new SolidColorBrush(FromHex("#FF0047")));
            //Act
            var primary = TcoColors.Primary as SolidColorBrush;
            //Assert
            Assert.AreNotEqual(primary.Color, FromHex("#5a7785"));
            Assert.AreEqual(primary.Color, primaryColor);
        }

        private Color FromHex(string hex) => (Color)ColorConverter.ConvertFromString(hex);
    }
}