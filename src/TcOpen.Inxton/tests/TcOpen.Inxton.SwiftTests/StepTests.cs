using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TcoCore;
using TcOpen.Inxton.Swift;

namespace TcOpen.Inxton.Swift.Tests
{
    [TestFixture()]
    public class StepTests
    {
        [Test()]
        public void StepTest()
        {
            var actual = new Step(new TcoTask(new MockRootObject(), "SomeTask", "_someTask"));
            Assert.AreEqual("_someTask", actual.Origin.Symbol);
            Assert.IsNotNull(actual.Statements);
        }

        [Test()]
        public void add_statement()
        {
            var step = new Step(new TcoTask(new MockRootObject(), "SomeTask", "SomeTask"));

            var actual = step.AddStatement("_components.Piston.MoveOut().Done");

            Assert.AreEqual(1, step.Statements.Count());
            Assert.AreEqual("_components.Piston.MoveOut().Done", actual);
            Assert.AreEqual("_components.Piston.MoveOut().Done", step.Statements.First());
        }
    }
}
