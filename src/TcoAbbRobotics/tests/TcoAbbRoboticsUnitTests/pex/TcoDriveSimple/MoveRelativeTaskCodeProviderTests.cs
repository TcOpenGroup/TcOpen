using NUnit.Framework;
using TcoDrivesBeckhoff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoDrivesBeckhoffUnitTests;

namespace TcoDrivesBeckhoff.Pex.Tests
{
    [TestFixture()]
    public class MoveRelativeTaskCodeProviderTests
    {

        private static string InOneLine(string input)
        {
            return input.Replace("\n", "").Replace("\r", "").Replace("\t", "");
        }

        [Test()]
        public void CodeTest()
        {
            var drive = new TcoDriveSimple(new MockRootObject(), "servo", "servoSymbol");
            drive._moveRelativeTask._distance.Synchron = 10;            
            drive._moveRelativeTask._velocity.Synchron = 20;
            drive._moveRelativeTask._acceleration.Synchron = 30;
            drive._moveRelativeTask._deceleration.Synchron = 40;
            drive._moveRelativeTask._jerk.Synchron = 50;

            var provider = new MoveRelativeTaskCodeProvider(drive);

            var actual = InOneLine(provider.Code());
            var expected = "servoSymbol.MoveRelative(                                                         inDistance := 10,                                                         inVelocity := 20,                                                         inAcceleration := 30,                                                         inDeceleration := 40,                                                         inJerk := 50).Done";
            Console.WriteLine(actual);

            Assert.AreEqual(expected, actual);
        }  
    }
}