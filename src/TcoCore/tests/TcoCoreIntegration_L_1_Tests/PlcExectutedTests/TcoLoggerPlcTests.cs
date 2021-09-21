﻿using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TcoCore;
using TcoCoreTests;
using TcOpen.Inxton.Logging;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

/// <summary>
/// NOTE! we use here the logger from the context instance
/// </summary>

namespace TcoCoreUnitTests.PlcExecutedTests
{
    [Timeout(1500000)]
    public class T12_TcoLoggerPlcTests
    {
        TcoLoggerTestContext tc = ConnectorFixture.Connector.MAIN._tcoLoggerTestContext;
       
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            tc.GetConnector().SuspendWriteProtection("Hoj morho vetvo mojho rodu, kto kramou rukou siahne na tvoju slobodu a co i dusu das v tom boji divokom vol nebyt ako byt otrokom!");
            tc._logger._plcCarret.Synchron = 0;         
            var emptyMessage = new PlainTcoMessage();
            foreach (var item in tc._logger._buffer)
            {
                item.FlushPlainToOnline(emptyMessage);
            }
             
        }

        [SetUp]
        public void SetUp()
        {
          
        }

        [Test, Order((int)eTcoLoggerTests.Push)]
        [TestCase("This is debug message", TcoCore.eMessageCategory.Debug)]
        [TestCase("This is trace message", TcoCore.eMessageCategory.Trace)]        
        [TestCase("This is info message", TcoCore.eMessageCategory.Info)]
        [TestCase("This is timed-out message", TcoCore.eMessageCategory.TimedOut)]
        [TestCase("This is warning message", TcoCore.eMessageCategory.Warning)]
        [TestCase("This is error message", TcoCore.eMessageCategory.Error)]
        [TestCase("This is programming error message", TcoCore.eMessageCategory.ProgrammingError)]
        [TestCase("This is critical message", TcoCore.eMessageCategory.Critical)]
        [TestCase("This is catastrophic message", TcoCore.eMessageCategory.Catastrophic)]        
        public void PushTest(string message, eMessageCategory category)
        {
            //Arrange
            tc._msg.Text.Synchron = message;
            tc._msg.Category.Synchron = (short)category;
            tc._logger._plcCarret.Synchron = 0;
            tc._logger.MinLogLevelCategory = category;
            var index = tc._logger._plcCarret.Synchron;
            

            //Act
            tc.ExecuteProbeRun(1, (int)eTcoLoggerTests.Push);


            tc._logger.LogMessages(tc._logger.Pop());

            //Assert
            Assert.AreEqual(tc._msg.Text.Synchron, tc._logger._buffer[index].Text.Synchron);
            Assert.AreEqual(tc._msg.Category.Synchron, tc._logger._buffer[index].Category.Synchron);
          
            var logger = TcOpen.Inxton.TcoAppDomain.Current.Logger as DummyLoggerAdapter;
            Assert.AreEqual(($"{message} {{@sender}}"), logger.LastMessage.message);
            Assert.AreEqual(TransleMessageCategoryToLogCategory(category), logger.LastMessage.serverity);          
        }

        [Test, Order((int)eTcoLoggerTests.PushMultiple)]        
        public void PushMultipleTest()
        {
            // Arrange
            var dummyLogger = TcOpen.Inxton.TcoAppDomain.Current.Logger as DummyLoggerAdapter;
            dummyLogger.QueueMessages();


            // for (int i = 0; i < 10; i++)
            {
                //Act
                tc.ExecuteProbeRun(1, (int)eTcoLoggerTests.PushMultiple);

                var sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                tc._logger.LogMessages(tc._logger.Pop());
                sw.Stop();

                System.Console.WriteLine(sw.ElapsedMilliseconds);
            }            
            

            (string message, object payload, string serverity) result;
            IList<(string message, object payload, string serverity)> dequeuedMessages = new List<(string message, object payload, string serverity)>();
            while (dummyLogger.MessageQueue.TryDequeue(out result))
            {
                dequeuedMessages.Add(result);
            }
            
            Assert.AreEqual(1001, dequeuedMessages.Count);

        }

        [Test, Order((int)eTcoLoggerTests.PushMultipleInMoreCycles)]
        public void PushMultipleInDistictCyclesTest_same_messages_in_consecutive_cycles()
        {
            tc._multiplesCount.Synchron = 25;
            tc._logger.MinLogLevelCategory = eMessageCategory.Info;
            tc._msg.Category.Synchron = (short)eMessageCategory.Info;
            tc._logger.Pop();

            tc.ExecuteProbeRun(1, (int)eTcoLoggerTests.PushMultipleInMoreCycles);            
            tc.ExecuteProbeRun(1, (int)eTcoLoggerTests.PushMultipleInMoreCycles);
            tc.ExecuteProbeRun(1, (int)eTcoLoggerTests.PushMultipleInMoreCycles);
            tc.ExecuteProbeRun(1, (int)eTcoLoggerTests.PushMultipleInMoreCycles);
            tc.ExecuteProbeRun(1, (int)eTcoLoggerTests.PushMultipleInMoreCycles);
            tc.ExecuteProbeRun(1, (int)eTcoLoggerTests.PushMultipleInMoreCycles);

            var actual = tc._logger.Pop();

            Assert.AreEqual(25, actual.Count());                                                          
        }

        [Test, Order((int)eTcoLoggerTests.PushMultipleInMoreCycles)]
        public void PushMultipleInDistictCyclesTest_same_messages_non_consecutive_cycles()
        {
            tc._multiplesCount.Synchron = 25;
            tc._logger.MinLogLevelCategory = eMessageCategory.Info;
            tc._msg.Category.Synchron = (short)eMessageCategory.Info;

            tc.ExecuteProbeRun(1, (int)eTcoLoggerTests.PushMultipleInMoreCycles);            
            tc.ExecuteProbeRun(2, 0);
            tc.ExecuteProbeRun(1, (int)eTcoLoggerTests.PushMultipleInMoreCycles);
            tc.ExecuteProbeRun(2, 0);
            tc.ExecuteProbeRun(1, (int)eTcoLoggerTests.PushMultipleInMoreCycles);

            var actual = tc._logger.Pop();

            Assert.AreEqual(75, actual.Count());
        }

        private string TransleMessageCategoryToLogCategory(eMessageCategory category)
        {            
            switch (category)
            {              
                case eMessageCategory.Debug:
                    return "Debug";                    
                case eMessageCategory.Trace:
                    return "Verbose";
                case eMessageCategory.Info:
                    return "Information";
                case eMessageCategory.TimedOut:                    
                case eMessageCategory.Notification:                    
                case eMessageCategory.Warning:
                    return "Warning";
                case eMessageCategory.Error:                   
                case eMessageCategory.ProgrammingError:
                    return "Error";
                case eMessageCategory.Critical:
                case eMessageCategory.Catastrophic:
                    return "Fatal";
                case eMessageCategory.None:
                    break;
                default:
                    return string.Empty;
            }

            return string.Empty;
        }
    }
}