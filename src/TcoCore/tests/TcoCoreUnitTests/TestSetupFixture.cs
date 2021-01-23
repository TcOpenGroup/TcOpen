using NUnit.Framework;
using System;

namespace TcoCoreUnitTests
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
            switch (Environment.MachineName)
            {
                case "WIN-8UTM78O6HB1":
                    TargetAmsId = "172.20.10.105.1.1";
                    TargetAmsPort = 852;
                    break;
                case "MTS2386":
                    TargetAmsId = "172.20.10.2.1.1";
                    TargetAmsPort = 852;
                    break;
                case "MTS_PK_LENOVUO_":
                    TargetAmsId = "172.20.10.223.1.1";
                    TargetAmsPort = 852;
                    break;
                case "MTS2216":
                    TargetAmsId = "172.20.10.104.1.1";
                    TargetAmsPort = 852;
                    break;
                default:
                    TargetAmsId = null;
                    TargetAmsPort = 852;
                    break;
            }

        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }
    }
}