using NUnit.Framework;
using PlcTcProberTestsConnector;
using Tc.Prober.Runners;
using Vortex.Connector;

namespace Tc.Prober.RunnersTests
{
    public class TaskRunnersTests
    {
        const int __max_average_cycle_time = 10;

        [OneTimeSetUp]
        public void Setup()
        {
            Entry.Plc.Connector.BuildAndStart().ReadWriteCycleDelay = 1000;
            Entry.Plc.Tests.Read();
        }

        [Test]
        [Order(100)]
        [TestCase((ushort)10)]
        [TestCase((ushort)11)]        
        public void basic_runner_tests_run_count(ushort counts)
        {

            //-- Arrange
            var sw = new System.Diagnostics.Stopwatch();
            var sut = Entry.Plc.Tests._basicRunnerTests;            
            sut.ResetCounter();

            //-- Act
            sw.Start();
            var actual = sut.Run((p) => p.RunCount(), counts);
            sw.Stop();

            var actualACT = sw.ElapsedMilliseconds / counts;

            //-- Assert
            Assert.AreEqual(counts, actual);
            Assert.IsTrue(actualACT < __max_average_cycle_time, $"ACT exceeds expected: {actualACT} > {__max_average_cycle_time}");
        }

        [Test]
        [Order(200)]
        [TestCase((ushort)10)]
        [TestCase((ushort)11)]
        [Timeout(100)]
        public void basic_runner_tests_run_unit_returns_true(ushort counts)
        {
            
            //-- Arrange
            var sut = Entry.Plc.Tests._basicRunnerTests;
            var sw = new System.Diagnostics.Stopwatch();


            //-- Act

            sut.ResetCounter();
            var actual = 0;
            sw.Start();
            sut.Run((p) =>
            {
                actual++;
                return p.RunUntilReturnsTrue(actual >= counts);
            });
            sw.Stop();


            //-- Assert
            var actualACT = sw.ElapsedMilliseconds / counts;
            Assert.AreEqual(counts, actual);
            Assert.IsTrue(actualACT < __max_average_cycle_time, $"ACT exceeds expected: {actualACT} > {__max_average_cycle_time}");
        }        
    }
}