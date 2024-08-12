using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TcOpen.Inxton;
using TcOpen.Inxton.App.Logging;
using TcOpen.Inxton.Logging;

namespace TcOpen.Inxton.AppTests
{
    [TestFixture()]
    public class TcoAppBuilderTests
    {
        [Test()]
        public void CheckAppDomain_and_AppBuilder_defaults()
        {
            Assert.AreEqual(typeof(DummyLoggerAdapter), TcoAppDomain.Current.Logger.GetType());
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

        [Test()]
        public void SetEditValueChangeLoggingTest()
        {
            var mock = new TcoCoreTests.fbPiston(new MockRootObject(), string.Empty, string.Empty);
            TcoAppDomain
                .Current.Builder.SetUpLogger(new MockLogger())
                .SetEditValueChangeLogging(mock);

            foreach (var item in mock.GetValueTags())
            {
                if (item.GetType().IsPrimitive && item.GetType() != typeof(bool))
                {
                    ((dynamic)item).Edit = 1;

                    var log = ((MockLogger)TcoAppDomain.Current.Logger).LastLog;
                    var expectedLog =
                        $"INFO:'{item.Symbol}' value has been changed from '0' to '1' {{@payload}}[{{ Path = {item.HumanReadable}, Symbol = {item.Symbol} }}]";
                    Console.WriteLine(log);
                    Console.WriteLine(expectedLog);
                    Assert.AreEqual(expectedLog, log);
                }
            }
        }
    }
}
