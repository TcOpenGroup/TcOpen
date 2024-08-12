using System;
using NUnit.Framework;
using TcOpen.Inxton.Local.Security.Readers;

namespace TcOpen.Inxton.Local.Security.ReadersTests
{
    public class Tests
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void CreatePlcTokenReader()
        {
            var tokenValueSource = new Vortex.Connector.ValueTypes.OnlinerString();
            var tokenPresence = new Vortex.Connector.ValueTypes.OnlinerBool();
            var reader = ExternalTokenAuthorization.CreatePlcTokenReader(
                tokenValueSource,
                tokenPresence
            );
        }

        [Test]
        public void CreateComTokenReader()
        {
            var tokenValueSource = new Vortex.Connector.ValueTypes.OnlinerString();
            var tokenPresence = new Vortex.Connector.ValueTypes.OnlinerBool();
            Assert.Throws<System.IO.IOException>(
                () => ExternalTokenAuthorization.CreateComReader("COM1")
            );
        }
    }
}
