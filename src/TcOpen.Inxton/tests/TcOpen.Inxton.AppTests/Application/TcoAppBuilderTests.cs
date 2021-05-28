using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Logging;
using TcOpen.Inxton.Abstractions.Logging;
using TcOpen.Inxton;

namespace TcOpen.Inxton.AppTests
{
    [TestFixture()]
    public class TcoAppBuilderTests
    {
        [Test()]
        public void CheckAppDomain_and_AppBuilder_defaults()
        {
            Assert.AreEqual(typeof(DummyLogger), TcoAppDomain.Current.Logger.GetType());
        }

        [Test()]
        public void GetBuilderTest()
        {
            Assert.IsNotNull(TcoAppDomain.Current.Builder);
            Assert.AreEqual(typeof(TcoAppBuilder), TcoAppDomain.Current.Builder.GetType());
        }

        [Test()]
        public void SetLoggerTest()
        {                        
            TcoAppDomain.Current.Builder.SetUpLogger(new MockLogger());
            Assert.AreEqual(typeof(MockLogger), TcoAppDomain.Current.Logger.GetType());
        }
    }
}