using NUnit.Framework;
using System;

namespace TcoCoreUnitTests
{
    [SetUpFixture]
    [Apartment(System.Threading.ApartmentState.STA)]
    public class TestSetupFixture
    {
        public static string TargetAmsId { get; private set; }
        public static int TargetAmsPort { get; private set; } = 852;

        [OneTimeSetUp]
        [Apartment(System.Threading.ApartmentState.STA)]
        [STAThread]
        public void OneTimeSetUp()
        {
            TargetAmsId = Environment.GetEnvironmentVariable("Tc3Target");         
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }
    }
}