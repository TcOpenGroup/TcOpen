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
    public class TcoDriveSimpleTests
    {
        [Test()]
        public void TcoDriveSimpleTest()
        {
            var drive = new TcoDriveSimple(new MockRootObject(), "servo", "servoSymbol");
            Assert.IsInstanceOf<MoveAbsoluteTaskCodeProvider>(drive._moveAbsoluteTask.CodeProvider);
            Assert.IsInstanceOf<MoveModuloTaskCodeProvider>(drive._moveModuloTask.CodeProvider);
            Assert.IsInstanceOf<MoveVelocityTaskCodeProvider>(drive._moveVelocityTask.CodeProvider);
            Assert.IsInstanceOf<MoveRelativeTaskCodeProvider>(drive._moveRelativeTask.CodeProvider);
        }
    }
}
