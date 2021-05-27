using NUnit.Framework;
using TcOpen.Inxton.Logging;

namespace TcOpen.Inxton.Logging.SerilogTests
{

    [TestFixture()]
    public class SerilogAdapterTests

    {
        [Test()]
        public void SerilogLoggerTest()
        {
            var actual = new SerilogAdapter();
        }

        [Test()]
        public void SerilogLoggerTest1()
        {
            var actual = new SerilogAdapter(new Serilog.LoggerConfiguration().WriteTo.MockConsole());
        
        }   

        [Test()]
        public void DebugTest()
        {
            //-- Arrange
            var expectedLevel = "Debug";
            var expected = "This is debug message. MessagePayload { SomeString = \"some payload\" }";
            var actor = new SerilogAdapter(new Serilog.LoggerConfiguration()
                                                .WriteTo.MockConsole()
                                                .MinimumLevel.Debug());            

            //-- Act
            actor.Debug<object>(expected, new MessagePayload() { SomeString = "some payload" });
            

            //-- Assert
            Assert.AreEqual($"{expectedLevel}:{expected}", MockSink.LastLogEntry);
        }

        [Test()]
        public void VerboseTest()
        {
            //-- Arrange
            var expectedLevel = "Verbose";
            var expected = "This is verbose message. MessagePayload { SomeString = \"some payload\" }";
            var actor = new SerilogAdapter(new Serilog.LoggerConfiguration()
                                                .WriteTo.MockConsole()
                                                .MinimumLevel.Verbose());

            //-- Act
            actor.Verbose(expected, new MessagePayload() { SomeString = "some payload" });


            //-- Assert
            Assert.AreEqual($"{expectedLevel}:{expected}", MockSink.LastLogEntry);
        }

        [Test()]
        public void InformationTest()
        {
            //-- Arrange
            var expectedLevel = "Information";
            var expected = "This is information message. MessagePayload { SomeString = \"some payload\" }";
            var actor = new SerilogAdapter(new Serilog.LoggerConfiguration()
                                                .WriteTo.MockConsole()
                                                .MinimumLevel.Information());

            //-- Act
            actor.Information(expected, new MessagePayload() { SomeString = "some payload" });


            //-- Assert
            Assert.AreEqual($"{expectedLevel}:{expected}", MockSink.LastLogEntry);
        }

        [Test()]
        public void WarningTest()
        {
            //-- Arrange
            var expectedLevel = "Warning";
            var expected = "This is warning message. MessagePayload { SomeString = \"some payload\" }";
            var actor = new SerilogAdapter(new Serilog.LoggerConfiguration()
                                                .WriteTo.MockConsole()
                                                .MinimumLevel.Warning());

            //-- Act
            actor.Warning(expected, new MessagePayload() { SomeString = "some payload" });


            //-- Assert
            Assert.AreEqual($"{expectedLevel}:{expected}", MockSink.LastLogEntry);
        }

        [Test()]
        public void ErrorTest()
        {
            //-- Arrange
            var expectedLevel = "Error";
            var expected = "This is error message. MessagePayload { SomeString = \"some payload\" }";
            var actor = new SerilogAdapter(new Serilog.LoggerConfiguration()
                                                .WriteTo.MockConsole()
                                                .MinimumLevel.Error());

            //-- Act
            actor.Error(expected, new MessagePayload() { SomeString = "some payload" });


            //-- Assert
            Assert.AreEqual($"{expectedLevel}:{expected}", MockSink.LastLogEntry);
        }

        [Test()]
        public void FatalTest()
        {
            //-- Arrange
            var expectedLevel = "Fatal";
            var expected = "This is fatal message. MessagePayload { SomeString = \"some payload\" }";
            var actor = new SerilogAdapter(new Serilog.LoggerConfiguration()
                                                .WriteTo.MockConsole()
                                                .MinimumLevel.Fatal());

            //-- Act
            actor.Fatal(expected, new MessagePayload() { SomeString = "some payload" });


            //-- Assert
            Assert.AreEqual($"{expectedLevel}:{expected}", MockSink.LastLogEntry);
        }
    }
}