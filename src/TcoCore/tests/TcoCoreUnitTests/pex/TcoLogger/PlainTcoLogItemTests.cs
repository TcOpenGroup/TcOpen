using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TcoCore;
using Vortex.Connector;

namespace TcoCore.PexTests
{
    [TestFixture()]
    public class PlainTcoLogItemTests
    {
        [Test()]
        public void CtorTest()
        {
            var plainTcoMessage = new PlainTcoLogItem();
            Assert.IsNotNull(plainTcoMessage);
        }

        [Test()]
        public void ToStringTest()
        {
            var plainTcoMessage = new PlainTcoMessage()
            {
                Text = "Hey",
                Category = (short)eMessageCategory.Critical
            };

            var actual = plainTcoMessage.ToString();

            Assert.AreEqual("1/1/0001 12:00:00 AM : 'Hey' | Critical ()", actual.ToString());
        }
    }
}
