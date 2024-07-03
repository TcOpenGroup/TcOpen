using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TcoCore;

namespace TcoCore.PexTests
{
    [TestFixture()]
    public class PlainTcoMessageTests
    {
        [Test()]
        public void CtorTest()
        {
            var plainTcoMessage = new PlainTcoMessage();
            Assert.IsNotNull(plainTcoMessage);
        }

        [Test()]
        public void ShallowCloneTest()
        {
            var plainTcoMessage = new PlainTcoMessage()
            {
                Text = "Hey",
                Category = (short)eMessageCategory.Critical
            };

            var actual = plainTcoMessage.ShallowClone();

            Assert.IsTrue(actual.Equals(plainTcoMessage));
            Assert.AreEqual("1/1/0001 12:00:00 AM : 'Hey' | Critical ()", actual.ToString());
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

        [Test()]
        public void plain_message_properties_test()
        {
            var parentObject = new TcoObject(
                new MockRootObject(),
                "parentObjectMessageTail",
                "parentObjectMessageSymbolTail"
            );
            var message = parentObject._messenger._mime;

            var plainTcoMessage = message.PlainMessage;

            Assert.AreEqual("parentObjectMessageTail", plainTcoMessage.Location);
            Assert.AreEqual("parentObjectMessageTail", plainTcoMessage.ParentsHumanReadable);
            Assert.AreEqual("parentObjectMessageSymbolTail", plainTcoMessage.ParentsObjectSymbol);
            Assert.AreEqual(0, plainTcoMessage.SubCategory);
            Assert.AreEqual("parentObjectMessageSymbolTail", plainTcoMessage.Source);
        }
    }
}
