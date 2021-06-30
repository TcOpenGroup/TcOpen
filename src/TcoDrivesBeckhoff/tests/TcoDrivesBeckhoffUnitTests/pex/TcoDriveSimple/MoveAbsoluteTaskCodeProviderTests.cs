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
    public class MoveAbsoluteTaskCodeProviderTests
    {

        private static string InOneLine(string input)
        {
            return input.Replace("\n", "").Replace("\r", "").Replace("\t", "");
        }

        [Test()]
        public void CodeTest()
        {
            var drive = new TcoDriveSimple(new MockRootObject(), "servo", "servoSymbol");
            drive._moveAbsoluteTask._position.Synchron = 10;
            drive._moveAbsoluteTask._velocity.Synchron = 20;
            drive._moveAbsoluteTask._acceleration.Synchron = 30;
            drive._moveAbsoluteTask._deceleration.Synchron = 40;
            drive._moveAbsoluteTask._jerk.Synchron = 50;

            var provider = new MoveAbsoluteTaskCodeProvider(drive);

            var actual = InOneLine(provider.Code());
            var expected = "servoSymbol.MoveAbsolute(                                                         inPosition := 10,                                                         inVelocity := 20,                                                         inAcceleration := 30,                                                         inDeceleration := 40,                                                         inJerk := 50).Done";
            Console.WriteLine(actual);

            Assert.AreEqual(expected, actual);
        }        
    }
}