using NUnit.Framework;
using TcoCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoCore.PexTests
{
    [TestFixture()]
    public class TcoObjectTests
    {
        [Test()]
        public void CtorTest()
        {
            var tcoObject = new TcoObject(new MockRootObject(), string.Empty, string.Empty);
            Assert.IsNotNull(tcoObject);
        }

        [Test()]
        public void GetMessageHandlerTest()
        {
            var tcoObject = new TcoObject(new MockRootObject(), string.Empty, string.Empty);
            Assert.IsInstanceOf<TcoObjectMessageHandler>(tcoObject.MessageHandler);
        }


        [Test()]
        public void GetActiveMessagesTest()
        {
            var context = new TcoContext(new MockRootObject(), string.Empty, string.Empty);
            var tcoObjectGreatParent = new TcoObject(context, string.Empty, string.Empty);
            var tcoObjectParent = new TcoObject(tcoObjectGreatParent, string.Empty, string.Empty);
            var tcoObject = new TcoObject(tcoObjectParent, string.Empty, string.Empty);

#pragma warning disable CS0618 // Type or member is obsolete
            context._startCycleCount.SetLastValue = 1875;
#pragma warning restore CS0618 // Type or member is obsolete

            var activeMessage = new TcoMessage(tcoObject, string.Empty, string.Empty);
#pragma warning disable CS0618 // Type or member is obsolete
            activeMessage.Cycle.SetLastValue = 1875;
#pragma warning restore CS0618 // Type or member is obsolete

            tcoObjectGreatParent.MessageHandler.DiagnosticsDepth = 1000;

            var actual = tcoObjectGreatParent.MessageHandler.GetActiveMessages();

            Assert.AreEqual(1, actual.Count());
        }
    }
}