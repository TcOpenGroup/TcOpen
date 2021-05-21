using NUnit.Framework;
using System;

namespace TcoPneumaticsTests
{
    [SetUpFixture]
    [Apartment(System.Threading.ApartmentState.STA)]
    public class TestSetupFixture
    {
        public static string TargetAmsId { get; private set; }
        public static int TargetAmsPort { get; private set; }

        [OneTimeSetUp]
        [Apartment(System.Threading.ApartmentState.STA)]
        [STAThread]
        public void OneTimeSetUp()
        {
            TargetAmsId = Environment.GetEnvironmentVariable("Tc3Target");
            TargetAmsPort = 852;            
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }
    }
}