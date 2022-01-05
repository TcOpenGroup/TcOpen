using NUnit.Framework;
using TcoCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;

namespace TcoCore.PexTests
{
    [TestFixture()]
    public class TcoContextTests
    {
        [Test()]
        public void CtorTest()
        {
            var context = new TcoContext(new MockRootObject(), string.Empty, string.Empty);
            Assert.IsNotNull(context);
        }

        [Test()]
        public void AddMessageTest()
        {
            var context = new TcoContext(new MockRootObject(), string.Empty, string.Empty);
            context.AddMessage(new TcoMessage(new MockRootObject(), string.Empty, string.Empty));
            context.AddMessage(new TcoMessage(new MockRootObject(), string.Empty, string.Empty));

            // Additional message on context may arise when the RtcIsNot synchronized.
            Assert.IsTrue(context.Messages.Count() == 4 || context.Messages.Count() == 3);
        }

        [Test()]
        public void GetActiveMessagesTest()
        {
            var context = new TcoContext(new MockRootObject(), string.Empty, string.Empty);
            context._startCycleCount.SetLastValue = 1875;
           
            var activeMessage = new TcoMessage(context, string.Empty, string.Empty);
            activeMessage.Cycle.SetLastValue = 1875;
           

            context.AddMessage(activeMessage);
            context.AddMessage(new TcoMessage(context, string.Empty, string.Empty));
            context.AddMessage(new TcoMessage(context, string.Empty, string.Empty));

            var actual = context.MessageHandler.GetActiveMessages();

            Assert.AreEqual(1, actual.Count());
        }

        [Test()]
        public void ActiveMessagesTest()
        {
            var context = new TcoContext(new MockRootObject(), string.Empty, string.Empty);
            context._startCycleCount.SetLastValue = 1875;

            var activeMessage = new TcoMessage(context, string.Empty, string.Empty);
            activeMessage.Cycle.SetLastValue = 1875;


            context.AddMessage(activeMessage);
            context.AddMessage(new TcoMessage(context, string.Empty, string.Empty));
            context.AddMessage(new TcoMessage(context, string.Empty, string.Empty));

            var actual = context.MessageHandler.GetActiveMessages();

            Assert.AreEqual(1, actual.Count());
        }
    }
}