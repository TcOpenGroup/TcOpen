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
    public class MoveModuloTaskCodeProviderTests
    {
        private static string InOneLine(string input)
        {
            return input.Replace("\n", "").Replace("\r", "").Replace("\t", "");
        }

        [Test()]
        public void CodeTest()
        {
            var drive = new TcoDriveSimple(new MockRootObject(), "servo", "servoSymbol");
            drive._moveModuloTask._position.Synchron = 10;
            drive._moveModuloTask._direction.Synchron = (short)eDirection.MC_Positive_Direction;
            drive._moveModuloTask._velocity.Synchron = 20;
            drive._moveModuloTask._acceleration.Synchron = 30;
            drive._moveModuloTask._deceleration.Synchron = 40;
            drive._moveModuloTask._jerk.Synchron = 50;

            var provider = new MoveModuloTaskCodeProvider(drive);

            var actual = InOneLine(provider.Code());
            var expected =
                "servoSymbol.MoveModulo(                                                         inPosition := 10,                                                         inVelocity := 20,                                                         inAcceleration := 30,                                                         inDeceleration := 40,                                                         inJerk := 50,                                                         inDirection := 1).Done";

            Assert.AreEqual(expected, actual);
        }
    }
}
