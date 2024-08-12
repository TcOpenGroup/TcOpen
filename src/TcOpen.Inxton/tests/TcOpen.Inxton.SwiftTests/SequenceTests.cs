using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TcOpen.Inxton.Swift;

namespace TcOpen.Inxton.Swift.Tests
{
    [TestFixture()]
    public class SequenceTests
    {
        [Test()]
        public void SequenceTest()
        {
            var actual = new Sequence();
            Assert.IsNotNull(actual.Steps);
        }

        [Test()]
        public void add_step()
        {
            var sequence = new Sequence();
            var actual = sequence.AddStep();

            Assert.AreEqual(1, sequence.Steps.Count());
            Assert.AreSame(actual, sequence.Steps.First());
        }
    }
}
