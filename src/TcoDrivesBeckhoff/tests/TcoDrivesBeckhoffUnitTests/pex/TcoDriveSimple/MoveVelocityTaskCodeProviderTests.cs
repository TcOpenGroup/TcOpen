using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TcoDrivesBeckhoff;
using TcoDrivesBeckhoffUnitTests;

namespace TcoDrivesBeckhoff.Pex.Tests
{
    [TestFixture()]
    public class MoveVelocityTaskCodeProviderTests
    {
        private static string InOneLine(string input)
        {
            return input.Replace("\n", "").Replace("\r", "").Replace("\t", "");
        }

        [Test()]
        public void CodeTest()
        {
            var drive = new TcoDriveSimple(new MockRootObject(), "servo", "servoSymbol");
            drive._moveVelocityTask._velocity.Synchron = 20;
            drive._moveVelocityTask._acceleration.Synchron = 30;
            drive._moveVelocityTask._deceleration.Synchron = 40;
            drive._moveVelocityTask._jerk.Synchron = 50;
            drive._moveVelocityTask._direction.Synchron = (short)eDirection.MC_Negative_Direction;

            var provider = new MoveVelocityTaskCodeProvider(drive);

            var actual = InOneLine(provider.Code());
            var expected =
                "servoSymbol.MoveVelocity(                                                                                                                  inVelocity := 20,                                                         inAcceleration := 30,                                                         inDeceleration := 40,                                                         inJerk := 50,                                                         inDirection := 3).Done";
            Console.WriteLine(actual);

            Assert.AreEqual(expected, actual);
        }
    }
}
