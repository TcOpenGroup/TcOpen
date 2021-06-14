using NUnit.Framework;
using TcOpen.Inxton.Swift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Swift.Tests
{
    [TestFixture()]
    public class StepTests
    {
        [Test()]
        public void StepTest()
        {
            var actual = new Step();
            Assert.IsNotNull(actual.Statements);
        }

        [Test()]
        public void add_statement()
        {
            var step = new Step();

            var actual = step.AddStatement("_components.Piston.MoveOut().Done");

            Assert.AreEqual(1, step.Statements.Count());
            Assert.AreEqual("_components.Piston.MoveOut().Done", actual);                       
            Assert.AreEqual("_components.Piston.MoveOut().Done", step.Statements.First());
        }
    }
}