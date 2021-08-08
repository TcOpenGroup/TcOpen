using NUnit.Framework;
using System;
using TcOpen.Inxton.Security;

namespace TcOpen.Inxton.Logging.SerilogTests
{

    [TestFixture()]
    public class SerilogAdapterWithUserEnricher


    {
        private IUser User { get; } = new DummyUser();

        [Test()]
        public void DebugLoggedUserTest()
        {
            //-- Arrange
            var userEnricher = new SerilogUserLogEnricher();

            var expectedLevel = "Debug";
            var expected = "This is debug message.  MessagePayload { SomeString = \"some payload\" }";
            var actor = new SerilogAdapter(new Serilog.LoggerConfiguration()
                                                .Enrich.FromLogContext()
                                                .WriteTo.MockConsoleForUser()
                                                .MinimumLevel.Debug());

            //-- Act
            userEnricher.UserLoggedIn(User);
            actor.Debug<object>(expected, new MessagePayload() { SomeString = "some payload" });
            //-- Assert
            Assert.AreEqual($"{expectedLevel}:[{User.UserName}] {expected}", MockFormatSink.LastLogEntry);
        }

        [Test()]
        public void VerboseLoggedUserTest()
        {
            //-- Arrange
            var userEnricher = new SerilogUserLogEnricher();
            var expectedLevel = "Verbose";
            var expected = "This is verbose message. MessagePayload { SomeString = \"some payload\" }";
            var actor = new SerilogAdapter(new Serilog.LoggerConfiguration()
                                                .Enrich.FromLogContext()
                                                .WriteTo.MockConsoleForUser()
                                                .MinimumLevel.Verbose());

            //-- Act
            userEnricher.UserLoggedIn(User);
            actor.Verbose(expected, new MessagePayload() { SomeString = "some payload" });


            //-- Assert
            Assert.AreEqual($"{expectedLevel}:[{User.UserName}] {expected}", MockFormatSink.LastLogEntry);
        }


        [Test()]
        public void WillLogLoggedOutUserAfterLogout()
        {
            //-- Arrange
            var userEnricher = new SerilogUserLogEnricher();
            var expectedLevel = "Verbose";
            var expected = "This is verbose message. MessagePayload { SomeString = \"some payload\" }";
            var actor = new SerilogAdapter(new Serilog.LoggerConfiguration()
                                                .Enrich.FromLogContext()
                                                .WriteTo.MockConsoleForUser()
                                                .MinimumLevel.Verbose());

            //-- Act
            userEnricher.UserLoggedIn(User);
            actor.Verbose(expected, new MessagePayload() { SomeString = "some payload" });
            Assert.AreEqual($"{expectedLevel}:[{User.UserName}] {expected}", MockFormatSink.LastLogEntry);
            userEnricher.UserLoggedOut();
            actor.Verbose(expected, new MessagePayload() { SomeString = "some payload" });

            //-- Assert
            Assert.AreEqual($"{expectedLevel}:[No-user] {expected}", MockFormatSink.LastLogEntry);
        }

    }


    class DummyUser : IUser
    {
        public bool CanUserChangePassword { get => true; set => throw new NotImplementedException(); }
        public string Email { get => "someemail@gmail.com"; set => throw new NotImplementedException(); }
        public string Level { get => "Admin"; set => throw new NotImplementedException(); }
        public string[] Roles { get => new string[] { "Admin" }; set => throw new NotImplementedException(); }
        public string UserName { get => "Joe Doe"; set => throw new NotImplementedException(); }
    }
}