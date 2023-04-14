using NUnit.Framework;
using System;
using System.Threading;
using TcoCore;
using TcoCoreTests;
using TcoCoreUnitTests;
using System.Linq;
using Vortex.Connector;
using System.Collections.Generic;

namespace TcoCoreUnitTests.PlcTcRpc
{

    public class T08_TcoMessengerTests
    {

        TcoCoreTests.TcoMessengerTests sut = ConnectorFixture.Connector.MAIN._tcoMessengerContextTest._tcoMessangerTests;
        TcoCoreTests.TcoMessengerContextTest suc = ConnectorFixture.Connector.MAIN._tcoMessengerContextTest;
        TimeSpan offsetPlcLocal = new TimeSpan();
        double allowedMessageTimeDeviation;

        [OneTimeSetUp]
        public void OneSetup()
        {
            suc._callMyPlcInstanceRtcUpdate.Synchron = true;
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); });
            sut.SingleCycleRun(() => sut.Resume());
            offsetPlcLocal = DateTime.Now - ConnectorFixture.Connector.MAIN._tcoContextTest_A._rtc._LocalTimeDT.Synchron;

            allowedMessageTimeDeviation = offsetPlcLocal.Add(new TimeSpan(0, 0, 0, 0, 1000)).TotalMilliseconds;

        }


        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
        }

        [SetUp]
        public void Setup()
        {
            
        }

        [Test, Order(100)]
        public void T801_DebugTest()
        {
            //--Arrange
            var messageText = "this is a debug message";

            //--Act
            sut.SingleCycleRun(() => sut.Debug(messageText));

            //--Assert

            Assert.AreEqual(sut.GetParent<TcoMessengerContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(sut._messenger._mime.Category.Synchron, (short)eMessageCategory.Debug);

            //Code changed by Tomas
            DateTime _plcTimeStamp = sut._messenger._mime.TimeStamp.Synchron;
            DateTime _dotNetTime = DateTime.Now;
            TimeSpan _diff = _dotNetTime - _plcTimeStamp;
            Assert.LessOrEqual(_diff.TotalMilliseconds, allowedMessageTimeDeviation);
        }

        [Test, Order(200)]
        public void T802_TraceTest()
        {
            //--Arrange
            var messageText = "this is a trace message";

            //--Act
            sut.SingleCycleRun(() => sut.Trace(messageText));

            //--Assert

            Assert.AreEqual(sut.GetParent<TcoMessengerContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(sut._messenger._mime.Category.Synchron, (short)eMessageCategory.Trace);


            //Code changed by Tomas
            DateTime _plcTimeStamp = sut._messenger._mime.TimeStamp.Synchron;
            DateTime _dotNetTime = DateTime.Now;
            TimeSpan _diff = _dotNetTime - _plcTimeStamp;
            Assert.LessOrEqual(_diff.TotalMilliseconds, allowedMessageTimeDeviation);
        }

        [Test, Order(300)]
        public void T803_NotifyTest()
        {
            //--Arrange
            var messageText = "this is a notify message";

            //--Act
            sut.SingleCycleRun(() => sut.Notify(messageText));

            //--Assert

            Assert.AreEqual(sut.GetParent<TcoMessengerContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(sut._messenger._mime.Category.Synchron, (short)eMessageCategory.Notification);

            //Code changed by Tomas
            DateTime _plcTimeStamp = sut._messenger._mime.TimeStamp.Synchron;
            DateTime _dotNetTime = DateTime.Now;
            TimeSpan _diff = _dotNetTime - _plcTimeStamp;
            Assert.LessOrEqual(_diff.TotalMilliseconds, allowedMessageTimeDeviation);
        }

        [Test, Order(400)]
        public void T804_WarningTest()
        {
            //--Arrange
            var messageText = "this is a warning message";

            //--Act
            sut.SingleCycleRun(() => sut.Warning(messageText));

            //--Assert

            Assert.AreEqual(sut.GetParent<TcoMessengerContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(sut._messenger._mime.Category.Synchron, (short)eMessageCategory.Warning);

            //Code changed by Tomas
            DateTime _plcTimeStamp = sut._messenger._mime.TimeStamp.Synchron;
            DateTime _dotNetTime = DateTime.Now;
            TimeSpan _diff = _dotNetTime - _plcTimeStamp;
            Assert.LessOrEqual(_diff.TotalMilliseconds, allowedMessageTimeDeviation);
        }


        [Test, Order(500)]
        public void T805_ErrorTest()
        {
            //--Arrange
            var messageText = "this is a error message";

            //--Act
            sut.SingleCycleRun(() => sut.Error(messageText));

            //--Assert

            Assert.AreEqual(sut.GetParent<TcoMessengerContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(sut._messenger._mime.Category.Synchron, (short)eMessageCategory.Error);

            //Code changed by Tomas
            DateTime _plcTimeStamp = sut._messenger._mime.TimeStamp.Synchron;
            DateTime _dotNetTime = DateTime.Now;
            TimeSpan _diff = _dotNetTime - _plcTimeStamp;
            Assert.LessOrEqual(_diff.TotalMilliseconds, allowedMessageTimeDeviation);
        }

        [Test, Order(600)]
        public void T806_InfoTest()
        {
            //--Arrange
            var messageText = "this is a info message";

            //--Act
            sut.SingleCycleRun(() => sut.Info(messageText));

            //--Assert

            Assert.AreEqual(sut.GetParent<TcoMessengerContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(sut._messenger._mime.Category.Synchron, (short)eMessageCategory.Info);

            //Code changed by Tomas
            DateTime _plcTimeStamp = sut._messenger._mime.TimeStamp.Synchron;
            DateTime _dotNetTime = DateTime.Now;
            TimeSpan _diff = _dotNetTime - _plcTimeStamp;
            Assert.LessOrEqual(_diff.TotalMilliseconds, allowedMessageTimeDeviation);
        }

        [Test, Order(700)]
        public void T807_ProgrammingTest()
        {
            //--Arrange
            var messageText = "this is a programming error message";

            //--Act
            sut.SingleCycleRun(() => sut.Programming(messageText));

            //--Assert

            Assert.AreEqual(sut.GetParent<TcoMessengerContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(sut._messenger._mime.Category.Synchron, (short)eMessageCategory.ProgrammingError);

            //Code changed by Tomas
            DateTime _plcTimeStamp = sut._messenger._mime.TimeStamp.Synchron;
            DateTime _dotNetTime = DateTime.Now;
            TimeSpan _diff = _dotNetTime - _plcTimeStamp;
            Assert.LessOrEqual(_diff.TotalMilliseconds, allowedMessageTimeDeviation);
        }

        [Test, Order(800)]
        public void T808_CriticalTest()
        {
            //--Arrange
            var messageText = "this is a critical message";

            //--Act
            sut.SingleCycleRun(() => sut.Critical(messageText));

            //--Assert

            Assert.AreEqual(sut.GetParent<TcoMessengerContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(sut._messenger._mime.Category.Synchron, (short)eMessageCategory.Critical);

            //Code changed by Tomas
            DateTime _plcTimeStamp = sut._messenger._mime.TimeStamp.Synchron;
            DateTime _dotNetTime = DateTime.Now;
            TimeSpan _diff = _dotNetTime - _plcTimeStamp;
            Assert.LessOrEqual(_diff.TotalMilliseconds, allowedMessageTimeDeviation);
        }

        [Test, Order(900)]
        public void T809_CatastrophicTest()
        {
            //--Arrange
            var messageText = "this is a catastrophic message";

            //--Act
            sut.SingleCycleRun(() => sut.Catastrophic(messageText));

            //--Assert

            Assert.AreEqual(sut.GetParent<TcoMessengerContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(sut._messenger._mime.Category.Synchron, (short)eMessageCategory.Catastrophic);
            //Peter's original code
            //Assert.IsTrue(sut._messenger._mime.TimeStamp.Synchron >= DateTime.Now.Subtract(new TimeSpan(0, 0, 0)));

            //Code changed by Tomas
            DateTime _plcTimeStamp = sut._messenger._mime.TimeStamp.Synchron;
            DateTime _dotNetTime = DateTime.Now;
            TimeSpan _diff = _dotNetTime - _plcTimeStamp;
            Assert.LessOrEqual(_diff.TotalMilliseconds, allowedMessageTimeDeviation);
        }

        [Test, Order(950)]
        public void T809_ClearTest()
        {
            //--Arrange
            var messageText = "this is a catastrophic message";

            //--Act
            sut.SingleCycleRun(() => sut.Catastrophic(messageText));

            //--Assert

            Assert.AreEqual(sut.GetParent<TcoMessengerContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(sut._messenger._mime.Category.Synchron, (short)eMessageCategory.Catastrophic);
            //Peter's original code
            //Assert.IsTrue(sut._messenger._mime.TimeStamp.Synchron >= DateTime.Now.Subtract(new TimeSpan(0, 0, 0)));

            //Code changed by Tomas
            DateTime _plcTimeStamp = sut._messenger._mime.TimeStamp.Synchron;
            DateTime _dotNetTime = DateTime.Now;
            TimeSpan _diff = _dotNetTime - _plcTimeStamp;
            Assert.LessOrEqual(_diff.TotalMilliseconds, allowedMessageTimeDeviation);

            sut._messenger.Clear();
            Assert.AreEqual(string.Empty, sut._messenger._mime.Text.Synchron);
            Assert.AreEqual((short)eMessageCategory.All, sut._messenger._mime.Category.Synchron);
            Assert.AreEqual(0, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(0, sut._messenger._mime.Identity.Synchron);
            Assert.AreEqual(0, sut._messenger._mime.PerCycleCount.Synchron);
        }

        [Test, Order(1000)]
        public void T810_PostLevelUpSeverityTest()
        {

            var msgCategories = new eMessageCategory[] { 
                                                    eMessageCategory.Trace,
                                                    eMessageCategory.Debug,
                                                    eMessageCategory.Info,
                                                    eMessageCategory.TimedOut,
                                                    eMessageCategory.Notification,
                                                    eMessageCategory.Warning,
                                                    eMessageCategory.Error,
                                                    eMessageCategory.ProgrammingError,
                                                    eMessageCategory.Critical,
                                                    eMessageCategory.Catastrophic
                                                  };
            sut.SingleCycleRun(() => {
                foreach (var category in msgCategories)
                {
                    //--Arrange              
                    var messageText = $"this is a {category} message";

                    //--Act
                    sut._category.Synchron = (short)category;
                    sut.Post(messageText);

                    //--Assert

                    Assert.AreEqual(sut.GetParent<TcoMessengerContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
                    Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
                    Assert.AreEqual((eMessageCategory)sut._messenger._mime.Category.Synchron, category);
                    //Peter's original code
                    //Assert.IsTrue(sut._messenger._mime.TimeStamp.Synchron >= DateTime.Now.Subtract(new TimeSpan(0, 0, 0)));

                    //Code changed by Tomas
                    DateTime _plcTimeStamp = sut._messenger._mime.TimeStamp.Synchron;
                    DateTime _dotNetTime = DateTime.Now;
                    TimeSpan _diff = _dotNetTime - _plcTimeStamp;
                    // Assert.LessOrEqual(_diff.TotalMilliseconds, allowedMessageTimeDeviation);
                }
            });
        }

        [Test, Order(1000)]
        [TestCase(new eMessageCategory[] { eMessageCategory.Catastrophic,
                                           eMessageCategory.Critical
                                         })]
        [TestCase(new eMessageCategory[] { eMessageCategory.Critical,
                                           eMessageCategory.ProgrammingError
                                         })]
        [TestCase(new eMessageCategory[] { eMessageCategory.ProgrammingError,
                                           eMessageCategory.Error
                                         })]
        [TestCase(new eMessageCategory[] { eMessageCategory.Error,
                                           eMessageCategory.Warning
                                         })]
        [TestCase(new eMessageCategory[] { eMessageCategory.Warning,
                                           eMessageCategory.Notification
                                         })]
        [TestCase(new eMessageCategory[] { eMessageCategory.Notification,
                                           eMessageCategory.Info
                                         })]
        [TestCase(new eMessageCategory[] { eMessageCategory.Info,
                                           eMessageCategory.Trace
                                         })]
        [TestCase(new eMessageCategory[] { 
                                           eMessageCategory.Debug,
                                           eMessageCategory.Trace
                                         })]
        public void T810_PostMaintainLevelUpSeverityTest(eMessageCategory[] categories)
        {

            sut.SingleCycleRun(() =>
            {
                var messageText = $"this is a {categories[0]} message";
                var expected = sut._messenger._mime.CreatePlainerType();

                sut._category.Synchron = (short)categories[0];
                sut.Post(messageText);

                //--Arrange                
                sut._messenger._mime.FlushOnlineToPlain(expected);

                //--Act
                sut._category.Synchron = (short)categories[1];
                sut.Post(messageText);

                //--Assert                                                                                             

                sut._messenger._mime.Read();
                Assert.AreEqual(expected.Cycle, sut._messenger._mime.Cycle.LastValue);
                Assert.AreEqual(expected.Text, sut._messenger._mime.Text.LastValue);
                Assert.AreEqual(expected.Category, sut._messenger._mime.Category.LastValue);
                Assert.AreEqual(expected.TimeStamp, sut._messenger._mime.TimeStamp.LastValue);

            });
        }

        [Test, Order(1100)]
        public void T811_flush_message_to_plain_test()
        {
            var msgCategories = new eMessageCategory[] { eMessageCategory.Debug,
                                                    eMessageCategory.Trace,
                                                    eMessageCategory.Info,
                                                    eMessageCategory.TimedOut,
                                                    eMessageCategory.Notification,
                                                    eMessageCategory.Warning,
                                                    eMessageCategory.Error,
                                                    eMessageCategory.ProgrammingError,
                                                    eMessageCategory.Critical,
                                                    eMessageCategory.Catastrophic
                                                  };
            sut.SingleCycleRun(() => {
                foreach (var category in msgCategories)
                {
                    //--Arrange              
                    var messageText = $"this is a {category} message";

                    //--Act
                    sut._category.Synchron = (short)category;
                    sut.Post(messageText);

                    //--Assert

                    Assert.AreEqual(sut.GetParent<TcoMessengerContextTest>()._startCycleCount.Synchron, sut._messenger._mime.PlainMessage.Cycle);
                    Assert.AreEqual(sut._messenger._mime.Text.Synchron, sut._messenger._mime.PlainMessage.Text);
                    Assert.AreEqual((eMessageCategory)sut._messenger._mime.Category.Synchron, (eMessageCategory)sut._messenger._mime.PlainMessage.Category);
                    Assert.AreEqual(sut._messenger._mime.TimeStamp.Synchron, sut._messenger._mime.PlainMessage.TimeStamp);
                }
            });
        }

        [Test, Order(1200)]
        [Repeat(3)]
        public void T812_IsActiveTest()
        {
            //--Arrange
            var messageText = "this is a message";
            sut._minLevel.Synchron = (short)eMessageCategory.All;
            sut.GetConnector().ReadWriteCycleDelay = 10;

            //--Act
            sut.SingleCycleRun(() => { sut.Resume(); sut.Catastrophic(messageText); });

            var handler = sut.GetParent<TcoMessengerContextTest>()?.MessageHandler;

            handler.DiagnosticsDepth = 1000;

            handler.GetActiveMessages();

            Assert.IsTrue(sut._messenger._mime.IsActive);

            sut.MultipleCycleRun(() => { }, (ushort)(sut.GetConnector().ReadWriteCycleDelay * 4));

            sut.GetParent<TcoMessengerContextTest>()?.MessageHandler.GetActiveMessages();


            Assert.IsFalse(sut._messenger._mime.IsActive);

            sut.GetConnector().ReadWriteCycleDelay = 100;
        }

        [Test, Order(1400)]        
        public void T813_SuspendResumeTest()
        {
            //--Arrange
            var messageText = "this is a message";
            sut.SingleCycleRun(() => Console.WriteLine("Empty cycle"));
            sut.GetConnector().ReadWriteCycleDelay = 10;

            //--Act

            // Suspend
            sut.SingleCycleRun(() => sut.Suspend());

            sut.SingleCycleRun(() => sut.Catastrophic(messageText));

            sut.MultipleCycleRun(() => { }, (ushort)(sut.GetConnector().ReadWriteCycleDelay * 5));

            sut.GetParent<TcoMessengerContextTest>()?.MessageHandler.GetActiveMessages();

            Assert.IsFalse(sut._messenger._mime.IsActive);


            // Resume
            sut.SingleCycleRun(() => sut.Resume());

            sut.SingleCycleRun(() => sut.Catastrophic(messageText));

            var handler = sut.GetParent<TcoMessengerContextTest>()?.MessageHandler;

            handler.DiagnosticsDepth = 1000;
            
            handler.GetActiveMessages();

            sut.MultipleCycleRun(() => { }, (ushort)(sut.GetConnector().ReadWriteCycleDelay * 5));

            Assert.IsTrue(sut._messenger._mime.IsActive);

            sut.GetConnector().ReadWriteCycleDelay = 100;
        }

        [Test, Order(1400)]
        public void T814_SetMinMessageCategoryTest()
        {
            //--Arrange
            var messageText = "this is a message";
            sut.SingleCycleRun(() => Console.WriteLine("Empty cycle"));
            sut.GetConnector().ReadWriteCycleDelay = 10;

            //--Act

            // Set min level to info
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.Info; sut.SetMinLevel(); });


            sut.SingleCycleRun(() => sut.Info(messageText));
            sut.GetParent<TcoMessengerContextTest>()?.MessageHandler.GetActiveMessages();

            Assert.IsTrue(sut._messenger._mime.IsActive);

            // set min level to catastrophic
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.Catastrophic; sut.SetMinLevel(); });

            // post info
            sut.SingleCycleRun(() => sut.Info(messageText));

            sut.MultipleCycleRun(() => { }, (ushort)(sut.GetConnector().ReadWriteCycleDelay * 5));

            sut.GetParent<TcoMessengerContextTest>()?.MessageHandler.GetActiveMessages();
            
            Assert.IsFalse(sut._messenger._mime.IsActive);


            // post catastrophic

            sut.SingleCycleRun(() => sut.Catastrophic(messageText));

            sut.GetParent<TcoMessengerContextTest>()?.MessageHandler.GetActiveMessages();

            Assert.IsTrue(sut._messenger._mime.IsActive);

            sut.GetConnector().ReadWriteCycleDelay = 100;
        }

        [Test, Order(1500)]
        [TestCase("This is debug message", TcoCore.eMessageCategory.Debug)]
        [TestCase("This is trace message", TcoCore.eMessageCategory.Trace)]
        [TestCase("This is info message", TcoCore.eMessageCategory.Info)]
        [TestCase("This is timed-out message", TcoCore.eMessageCategory.TimedOut)]
        [TestCase("This is warning message", TcoCore.eMessageCategory.Warning)]
        [TestCase("This is error message", TcoCore.eMessageCategory.Error)]
        [TestCase("This is programming error message", TcoCore.eMessageCategory.ProgrammingError)]
        [TestCase("This is critical message", TcoCore.eMessageCategory.Critical)]
        [TestCase("This is catastrophic message", TcoCore.eMessageCategory.Catastrophic)]
        public void T1500_MessageLoggerTest(string messageText, eMessageCategory category)
        {
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;
            sut._category.Synchron = (short)category;
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.OnEventRisen;
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();

            //--Act
            sut.SingleCycleRun(
                () => 
                sut.Post(messageText));

            var message = suc._logger.Pop().FirstOrDefault();

            Assert.AreEqual(category, message.CategoryAsEnum);
            Assert.AreEqual(messageText, message.Text);


        }

        [Test, Order(1550)]
        [TestCase("This is debug message", TcoCore.eMessageCategory.Debug)]
        [TestCase("This is trace message", TcoCore.eMessageCategory.Trace)]
        [TestCase("This is info message", TcoCore.eMessageCategory.Info)]
        [TestCase("This is timed-out message", TcoCore.eMessageCategory.TimedOut)]
        [TestCase("This is warning message", TcoCore.eMessageCategory.Warning)]
        [TestCase("This is error message", TcoCore.eMessageCategory.Error)]
        [TestCase("This is programming error message", TcoCore.eMessageCategory.ProgrammingError)]
        [TestCase("This is critical message", TcoCore.eMessageCategory.Critical)]
        [TestCase("This is catastrophic message", TcoCore.eMessageCategory.Catastrophic)]
        public void T1550_MessageLoggerTest_continuous_add_to_buffer_check_that_duplicates(string messageText, eMessageCategory category)
        {
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.Continuous;
            sut._category.Synchron = (short)category;

            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();

            //--Act
            sut.SingleCycleRun(
             () =>
             {
                 sut.Post(messageText);
                 sut.Post(messageText);
                 sut.Post(messageText);
                 sut.Post(messageText);
             });

            sut.SingleCycleRun(
            () =>
            {
                sut.Post(messageText);
                sut.Post(messageText);
                sut.Post(messageText);
                sut.Post(messageText);
            });

            sut.SingleCycleRun(
             () =>
             {
                 sut.Post(messageText);
                 sut.Post(messageText);
                 sut.Post(messageText);
                 sut.Post(messageText);
             });

            var messages = suc._logger.Pop();

            Assert.AreEqual(12, messages.Count());

            foreach (var message in messages)
            {                
                Assert.AreEqual(category, message.CategoryAsEnum);
                Assert.AreEqual(messageText, message.Text);
            }          
        }

        [Test, Order(1600)]
        [TestCase("This is debug message", TcoCore.eMessageCategory.Debug)]
        [TestCase("This is trace message", TcoCore.eMessageCategory.Trace)]
        [TestCase("This is info message", TcoCore.eMessageCategory.Info)]
        [TestCase("This is timed-out message", TcoCore.eMessageCategory.TimedOut)]
        [TestCase("This is warning message", TcoCore.eMessageCategory.Warning)]
        [TestCase("This is error message", TcoCore.eMessageCategory.Error)]
        [TestCase("This is programming error message", TcoCore.eMessageCategory.ProgrammingError)]
        [TestCase("This is critical message", TcoCore.eMessageCategory.Critical)]
        [TestCase("This is catastrophic message", TcoCore.eMessageCategory.Catastrophic)]
        public void T1600_MessageLoggerTest_on_event_risen_add_to_buffer_check_that_no_duplicates(string messageText, eMessageCategory category)
        {
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.OnEventRisen;
            sut._category.Synchron = (short)category;
            
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();

            //--Act
            sut.SingleCycleRun(
                () =>
                sut.Post(messageText));

            sut.SingleCycleRun(
               () =>
               sut.Post(messageText));

            sut.SingleCycleRun(
             () =>
             sut.Post(messageText));

            var messages = suc._logger.Pop();
            var message = messages.FirstOrDefault();


            Assert.AreEqual(1, messages.Count());
            Assert.AreEqual(category, message.CategoryAsEnum);
            Assert.AreEqual(messageText, message.Text);


        }

        [Test, Order(1700)]
        [TestCase("This is debug message", TcoCore.eMessageCategory.Debug)]
        [TestCase("This is trace message", TcoCore.eMessageCategory.Trace)]
        [TestCase("This is info message", TcoCore.eMessageCategory.Info)]
        [TestCase("This is timed-out message", TcoCore.eMessageCategory.TimedOut)]
        [TestCase("This is warning message", TcoCore.eMessageCategory.Warning)]
        [TestCase("This is error message", TcoCore.eMessageCategory.Error)]
        [TestCase("This is programming error message", TcoCore.eMessageCategory.ProgrammingError)]
        [TestCase("This is critical message", TcoCore.eMessageCategory.Critical)]
        [TestCase("This is catastrophic message", TcoCore.eMessageCategory.Catastrophic)]
        public void T1700_MessageLoggerTest_on_event_rised_log_in_several_cycles_and_log_to_logger(string messageText, eMessageCategory category)
        {
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;
            sut._category.Synchron = (short)category;
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.OnEventRisen;
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();

            int mi = 0;
            //--Act
            sut.SingleCycleRun(
                () =>
                sut.Post(messageText + (++mi).ToString()));

            sut.SingleCycleRun(
               () =>
               sut.Post(messageText + (++mi).ToString()));

            sut.SingleCycleRun(
             () =>
             sut.Post(messageText + (++mi).ToString()));

            var messages = suc._logger.Pop().ToArray();
            var message = messages.FirstOrDefault();

          
            Assert.AreEqual(3, messages.Count());

            mi = 0;
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(category, messages[i].CategoryAsEnum);
                Assert.AreEqual(messageText + (++mi).ToString(), messages[i].Text);
            }

            var dummyLogger = TcOpen.Inxton.TcoAppDomain.Current.Logger as TcOpen.Inxton.Logging.DummyLoggerAdapter;
#pragma warning disable CS0618 // Type or member is obsolete
            dummyLogger.QueueMessages();
#pragma warning restore CS0618 // Type or member is obsolete

            suc._logger.LogMessages(messages);
            

            (string message, object payload, string serverity) result;
            IList<(string message, object payload, string serverity)> dequeuedMessages = new List<(string message, object payload, string serverity)>();
#pragma warning disable CS0618 // Type or member is obsolete
            while (dummyLogger.MessageQueue.TryDequeue(out result))
#pragma warning restore CS0618 // Type or member is obsolete
            {
                dequeuedMessages.Add(result);
            }

            Assert.AreEqual(3, dequeuedMessages.Count);
            var serverityDictionary = new Dictionary<eMessageCategory, string>();

            serverityDictionary[eMessageCategory.Catastrophic] = "Fatal";
            serverityDictionary[eMessageCategory.Critical] = "Fatal";
            serverityDictionary[eMessageCategory.Debug] = "Debug";
            serverityDictionary[eMessageCategory.Error] = "Error";
            serverityDictionary[eMessageCategory.Info] = "Information";
            serverityDictionary[eMessageCategory.Notification] = "Warning";
            serverityDictionary[eMessageCategory.ProgrammingError] = "Error";
            serverityDictionary[eMessageCategory.Trace] = "Verbose";
            serverityDictionary[eMessageCategory.Warning] = "Warning";
            serverityDictionary[eMessageCategory.TimedOut] = "Warning";

            mi = 0;
            foreach (var msg in dequeuedMessages)
            {
                Assert.AreEqual($"{messageText}{++mi} {{@sender}}", msg.message);
                Assert.AreEqual(serverityDictionary[category], msg.serverity);
                Assert.AreEqual("MAIN._tcoMessengerContextTest._logger", DestructPayload<string>(msg.payload, "PlcLogger"));
                Assert.AreEqual("MAIN._tcoMessengerContextTest._tcoMessangerTests", DestructPayload<string>(msg.payload, "ParentSymbol"));
                Assert.AreEqual("this is the name of messenger test", DestructPayload<string>(msg.payload, "ParentName"));                
            }
        }

        [Test, Order(1700)]
        [TestCase("This is debug message", TcoCore.eMessageCategory.Debug)]
        [TestCase("This is trace message", TcoCore.eMessageCategory.Trace)]
        [TestCase("This is info message", TcoCore.eMessageCategory.Info)]
        [TestCase("This is timed-out message", TcoCore.eMessageCategory.TimedOut)]
        [TestCase("This is warning message", TcoCore.eMessageCategory.Warning)]
        [TestCase("This is error message", TcoCore.eMessageCategory.Error)]
        [TestCase("This is programming error message", TcoCore.eMessageCategory.ProgrammingError)]
        [TestCase("This is critical message", TcoCore.eMessageCategory.Critical)]
        [TestCase("This is catastrophic message", TcoCore.eMessageCategory.Catastrophic)]
        public void T1700_MessageLoggerTest_none_log_in_several_cycles_and_log_to_logger(string messageText, eMessageCategory category)
        {
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;
            sut._category.Synchron = (short)category;
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.NoLogging;
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();

            int mi = 0;
            //--Act
            sut.SingleCycleRun(
                () =>
                sut.Post(messageText + (++mi).ToString()));

            sut.SingleCycleRun(
               () =>
               sut.Post(messageText + (++mi).ToString()));

            sut.SingleCycleRun(
             () =>
             sut.Post(messageText + (++mi).ToString()));

            var messages = suc._logger.Pop().ToArray();
            var message = messages.FirstOrDefault();


            Assert.AreEqual(0, messages.Count());                     
        }

        [Test, Order(1750)]
        [TestCase(eMessageDigestMethod.CRC8)]
        [TestCase(eMessageDigestMethod.CRC16)]
        [TestCase(eMessageDigestMethod.CRC32)]
        [TestCase(eMessageDigestMethod.NONE)]
        public void T1850_MessageDigestMethod_Set(eMessageDigestMethod mdMethod)
        {
            //--Arrange            
            suc._tcoMessangerTests._messageDigestMethod.Synchron = (short)mdMethod;
            sut.SingleCycleRun(() => { sut.SetMessageDigestMethod(); });

            Assert.AreEqual(suc._settings._messenger._messageDigestMethod.Synchron, (short)mdMethod);
        }

        [Test, Order(1800)]
        [TestCase(eMessageDigestMethod.CRC8, (uint)139)]
        [TestCase(eMessageDigestMethod.CRC16, (uint)38100)]
        [TestCase(eMessageDigestMethod.CRC32, (uint)4257669427)]
        [TestCase(eMessageDigestMethod.NONE, (uint)0)]
        public void T1950_message_calulates_digest(eMessageDigestMethod mdMethod, uint md)
        {
            //--Arrange            
            suc._tcoMessangerTests._messageDigestMethod.Synchron = (short)mdMethod;
            sut.SingleCycleRun(() => { sut.SetMessageDigestMethod(); });

            sut.SingleCycleRun(
               () =>
               sut.Post("digets me"));

            Assert.AreEqual(sut._messenger._mime.MessageDigest.Synchron, md);
        }

        [Test, Order(1900)]
        [TestCase(eMessengerLogMethod.OnEventRisen)]
        [TestCase(eMessengerLogMethod.Continuous)]
        [TestCase(eMessageDigestMethod.NONE)]        
        public void T1900_MessageLogginMethod_Set(eMessengerLogMethod logMethod)
        {
            //--Arrange            
            suc._tcoMessangerTests._messageLoggingMethod.Synchron = (short)logMethod;
            sut.SingleCycleRun(() => { sut.SetMessageLoggingMethod(); });

            Assert.AreEqual(suc._settings._messenger._messengerLoggingMethod.Synchron, (short)logMethod);
        }

        [Test, Order(2000)]     
        public void T2000_MessengerLoggerTest_bug_on_event_logging_repeats_messages_when_count_changes()
        {
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;           
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.OnEventRisen;
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();

            
            //--Act
            sut.SingleCycleRun(
                () =>
                {                    
                    sut._category.Synchron = (short)eMessageCategory.Trace;
                    sut.Post($"this is message '{sut._category.Synchron}'");

                    sut._category.Synchron = (short)eMessageCategory.Info;
                    sut.Post($"this is message '{sut._category.Synchron}'");

                    sut._category.Synchron = (short)eMessageCategory.Error;
                    sut.Post($"this is message '{sut._category.Synchron}'");
                });
            
          
            Assert.AreEqual(3, suc._logger.Pop().Count());

            sut.SingleCycleRun(
                () =>
                {                 
                    sut._category.Synchron = (short)eMessageCategory.Info;
                    sut.Post($"this is message '{sut._category.Synchron}'");

                    sut._category.Synchron = (short)eMessageCategory.Error;
                    sut.Post($"this is message '{sut._category.Synchron}'");
                });
            
            Assert.AreEqual(0, suc._logger.Pop().Count());

            sut.SingleCycleRun(
               () =>
               {
                   sut._category.Synchron = (short)eMessageCategory.Trace;
                   sut.Post($"this is message '{sut._category.Synchron}'");

                   sut._category.Synchron = (short)eMessageCategory.Info;
                   sut.Post($"this is message '{sut._category.Synchron}'");

                   sut._category.Synchron = (short)eMessageCategory.Error;
                   sut.Post($"this is message '{sut._category.Synchron}'");
               });

            var messages = suc._logger.Pop();
            Assert.AreEqual(1, messages.Count());

            Assert.AreEqual("this is message '100'", messages.FirstOrDefault().Text);
        }


        [Test, Order(2100)]
        public void T2100_MessengerLoggerTest_bug_on_event_logging_repeats_messages_when_order_changes()
        {
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.OnEventRisen;
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();


            //--Act
            sut.SingleCycleRun(
                () =>
                {
                    sut._category.Synchron = (short)eMessageCategory.Trace;
                    sut.Post($"this is message '{sut._category.Synchron}'");

                    sut._category.Synchron = (short)eMessageCategory.Info;
                    sut.Post($"this is message '{sut._category.Synchron}'");

                    sut._category.Synchron = (short)eMessageCategory.Error;
                    sut.Post($"this is message '{sut._category.Synchron}'");
                });


            Assert.AreEqual(3, suc._logger.Pop().Count());

            sut.SingleCycleRun(
                () =>
                {
                    sut._category.Synchron = (short)eMessageCategory.Error;
                    sut.Post($"this is message '{sut._category.Synchron}'");

                    sut._category.Synchron = (short)eMessageCategory.Trace;
                    sut.Post($"this is message '{sut._category.Synchron}'");

                    sut._category.Synchron = (short)eMessageCategory.Info;
                    sut.Post($"this is message '{sut._category.Synchron}'");                   
                });

            Assert.AreEqual(0, suc._logger.Pop().Count());

            sut.SingleCycleRun(
               () =>
               {
                  

                   sut._category.Synchron = (short)eMessageCategory.Info;
                   sut.Post($"this is message '{sut._category.Synchron}'");

                   sut._category.Synchron = (short)eMessageCategory.Error;
                   sut.Post($"this is message '{sut._category.Synchron}'");

                   sut._category.Synchron = (short)eMessageCategory.Trace;
                   sut.Post($"this is message '{sut._category.Synchron}'");
               });

            var messages = suc._logger.Pop();
            Assert.AreEqual(0, messages.Count());
           
        }

        [Test, Order(2200)]
        public void T2200_MessengerLoggerTest_bug_on_event_logging_repeats_messages_when_order_changes()
        {
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.OnEventRisen;
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();


            //--Act
            sut.SingleCycleRun(
                () =>
                {
                    sut._category.Synchron = (short)eMessageCategory.Trace;
                    sut.Post($"this is message '{sut._category.Synchron}'");

                    sut._category.Synchron = (short)eMessageCategory.Info;
                    sut.Post($"this is message '{sut._category.Synchron}'");

                    sut._category.Synchron = (short)eMessageCategory.Error;
                    sut.Post($"this is message '{sut._category.Synchron}'");
                });


            Assert.AreEqual(3, suc._logger.Pop().Count());

            sut.MultipleCycleRun(
                () =>
                {
                    sut._category.Synchron = (short)eMessageCategory.Trace;
                    sut.Post($"this is message '{sut._category.Synchron}'");                  

                    sut._category.Synchron = (short)eMessageCategory.Error;
                    sut.Post($"this is message '{sut._category.Synchron}'");
                }, 5);

            Assert.AreEqual(0, suc._logger.Pop().Count());

           

            sut.SingleCycleRun(
               () =>
               {
                   sut._category.Synchron = (short)eMessageCategory.Trace;
                   sut.Post($"this is message '{sut._category.Synchron}'");

                   sut._category.Synchron = (short)eMessageCategory.Info;
                   sut.Post($"this is message '{sut._category.Synchron}'");

                   sut._category.Synchron = (short)eMessageCategory.Error;
                   sut.Post($"this is message '{sut._category.Synchron}'");
               });

            var messages = suc._logger.Pop();
            Assert.AreEqual(1, messages.Count());

        }

        [Test, Order(3000)]
        [TestCase("This is debug message", TcoCore.eMessageCategory.Debug)]
        [TestCase("This is trace message", TcoCore.eMessageCategory.Trace)]
        [TestCase("This is info message", TcoCore.eMessageCategory.Info)]
        [TestCase("This is timed-out message", TcoCore.eMessageCategory.TimedOut)]
        [TestCase("This is warning message", TcoCore.eMessageCategory.Warning)]
        [TestCase("This is error message", TcoCore.eMessageCategory.Error)]
        [TestCase("This is programming error message", TcoCore.eMessageCategory.ProgrammingError)]
        [TestCase("This is critical message", TcoCore.eMessageCategory.Critical)]
        [TestCase("This is catastrophic message", TcoCore.eMessageCategory.Catastrophic)]
        public void T3000_OnConditionMessageTest_true(string messageText, eMessageCategory category)
        {
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;
            sut._category.Synchron = (short)category;
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.OnEventRisen;
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();

            //--Act
            sut.SingleCycleRun(
                () =>
                sut.OnCondition(true, messageText));

            var message = sut._messenger._mime.PlainMessage;

            Assert.AreEqual(category, message.CategoryAsEnum);
            Assert.AreEqual(messageText, message.Text);
        }

        [Test, Order(3100)]
        [TestCase("This is debug message", TcoCore.eMessageCategory.Debug)]
        [TestCase("This is trace message", TcoCore.eMessageCategory.Trace)]
        [TestCase("This is info message", TcoCore.eMessageCategory.Info)]
        [TestCase("This is timed-out message", TcoCore.eMessageCategory.TimedOut)]
        [TestCase("This is warning message", TcoCore.eMessageCategory.Warning)]
        [TestCase("This is error message", TcoCore.eMessageCategory.Error)]
        [TestCase("This is programming error message", TcoCore.eMessageCategory.ProgrammingError)]
        [TestCase("This is critical message", TcoCore.eMessageCategory.Critical)]
        [TestCase("This is catastrophic message", TcoCore.eMessageCategory.Catastrophic)]
        public void T3100_OnConditionMessageTest_false(string messageText, eMessageCategory category)
        {
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;
            sut._category.Synchron = (short)category;
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.OnEventRisen;
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();

            //--Act
            sut.SingleCycleRun(
                () =>
                sut.OnCondition(false, messageText));

            var message = sut._messenger._mime.PlainMessage;

            Assert.AreEqual(eMessageCategory.All, message.CategoryAsEnum);
            Assert.AreEqual(string.Empty, message.Text);
        }

        [Test, Order(3200)]
        [TestCase("This is debug message", TcoCore.eMessageCategory.Debug)]
        [TestCase("This is trace message", TcoCore.eMessageCategory.Trace)]
        [TestCase("This is info message", TcoCore.eMessageCategory.Info)]
        [TestCase("This is timed-out message", TcoCore.eMessageCategory.TimedOut)]
        [TestCase("This is warning message", TcoCore.eMessageCategory.Warning)]
        [TestCase("This is error message", TcoCore.eMessageCategory.Error)]
        [TestCase("This is programming error message", TcoCore.eMessageCategory.ProgrammingError)]
        [TestCase("This is critical message", TcoCore.eMessageCategory.Critical)]
        [TestCase("This is catastrophic message", TcoCore.eMessageCategory.Catastrophic)]
        public void T3200_PersistMessageTest(string messageText, eMessageCategory category)
        {
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;
            sut._category.Synchron = (short)category;
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.OnEventRisen;
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();

            //--Act
            sut.SingleCycleRun(
                () =>
                sut.Pin(true, messageText));

            var message = sut._messenger._mime.PlainMessage;


            Assert.AreEqual(true, sut.Pinned());
            Assert.AreEqual(category, message.CategoryAsEnum);
            Assert.AreEqual(messageText, message.Text);
            Assert.AreEqual(true, message.Pinned);
        }

        [Test, Order(3300)]
        [TestCase("This is debug message", TcoCore.eMessageCategory.Debug)]
        [TestCase("This is trace message", TcoCore.eMessageCategory.Trace)]
        [TestCase("This is info message", TcoCore.eMessageCategory.Info)]
        [TestCase("This is timed-out message", TcoCore.eMessageCategory.TimedOut)]
        [TestCase("This is warning message", TcoCore.eMessageCategory.Warning)]
        [TestCase("This is error message", TcoCore.eMessageCategory.Error)]
        [TestCase("This is programming error message", TcoCore.eMessageCategory.ProgrammingError)]
        [TestCase("This is critical message", TcoCore.eMessageCategory.Critical)]
        [TestCase("This is catastrophic message", TcoCore.eMessageCategory.Catastrophic)]
        public void T3300_DontPersistMessageTest(string messageText, eMessageCategory category)
        {
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;
            sut._category.Synchron = (short)category;
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.OnEventRisen;
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();

            //--Act
            sut.SingleCycleRun(
                () =>
                sut.Pin(false, messageText));

            var message = sut._messenger._mime.PlainMessage;

            Assert.AreEqual(category, message.CategoryAsEnum);
            Assert.AreEqual(messageText, message.Text);
            Assert.AreEqual(false, message.Pinned);
        }

        [Test, Order(3400)]
        [TestCase("This is debug message", TcoCore.eMessageCategory.Debug)]
        [TestCase("This is trace message", TcoCore.eMessageCategory.Trace)]
        [TestCase("This is info message", TcoCore.eMessageCategory.Info)]
        [TestCase("This is timed-out message", TcoCore.eMessageCategory.TimedOut)]
        [TestCase("This is warning message", TcoCore.eMessageCategory.Warning)]
        [TestCase("This is error message", TcoCore.eMessageCategory.Error)]
        [TestCase("This is programming error message", TcoCore.eMessageCategory.ProgrammingError)]
        [TestCase("This is critical message", TcoCore.eMessageCategory.Critical)]
        [TestCase("This is catastrophic message", TcoCore.eMessageCategory.Catastrophic)]
        public void T3400_PersistentMessageCancelledTest(string messageText, eMessageCategory category)
        {
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;
            sut._category.Synchron = (short)category;
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.OnEventRisen;
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();

            //--Act
            sut.SingleCycleRun(
                () =>
                sut.Pin(true, messageText));

            sut.SingleCycleRun(
                () => {  }) ;

            sut.SingleCycleRun(
                () =>
                sut.Post(messageText));

            var message = sut._messenger._mime.PlainMessage;
           
            Assert.AreEqual(true, message.Pinned);

            sut._messenger._mime.Pinned.Synchron = false;

            sut.SingleCycleRun(
               () => {  });

            sut.SingleCycleRun(
                () =>
                sut.Post(messageText));

            message = sut._messenger._mime.PlainMessage;

            Assert.AreEqual(false, message.Pinned);
        }

        [Test, Order(3400)]
        [TestCase("This is debug message", TcoCore.eMessageCategory.Debug)]
        [TestCase("This is trace message", TcoCore.eMessageCategory.Trace)]
        [TestCase("This is info message", TcoCore.eMessageCategory.Info)]
        [TestCase("This is timed-out message", TcoCore.eMessageCategory.TimedOut)]
        [TestCase("This is warning message", TcoCore.eMessageCategory.Warning)]
        [TestCase("This is error message", TcoCore.eMessageCategory.Error)]
        [TestCase("This is programming error message", TcoCore.eMessageCategory.ProgrammingError)]
        [TestCase("This is critical message", TcoCore.eMessageCategory.Critical)]
        [TestCase("This is catastrophic message", TcoCore.eMessageCategory.Catastrophic)]
        public void T3400_UnPinMessageTest(string messageText, eMessageCategory category)
        {
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;
            sut._category.Synchron = (short)category;
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.OnEventRisen;
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();

            //--Act
            sut.SingleCycleRun(
                () =>
                sut.Pin(true, messageText));

            sut.SingleCycleRun(
                () => {  });

            sut.SingleCycleRun(
                () =>
                sut.Post(messageText));

            var message = sut._messenger._mime.PlainMessage;

            Assert.AreEqual(true, message.Pinned);

            sut.SingleCycleRun(
               () => sut.UnPin(true, $"{messageText} dnu"));

            Assert.AreEqual(true, message.Pinned);

            sut.SingleCycleRun(
               () => sut.UnPin(false, $"{messageText}"));

            message = sut._messenger._mime.PlainMessage;

            Assert.AreEqual(false, message.Pinned);            
        }

        [Test, Order(4000)]     
        public void T4000_AsTrace()
        {
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;            
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.OnEventRisen;
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();

            sut.SingleCycleRun(
                () => sut.AsTrace("Text 1","Text 2 ", 10.5));


            var plain = sut._messenger._mime.PlainMessage;
            var popped = suc._logger.Pop().FirstOrDefault();

            Assert.AreEqual("Text 1Text 2 10.5", plain.Text);
            Assert.AreEqual(eMessageCategory.Trace, plain.CategoryAsEnum);

            Assert.AreEqual("Text 1Text 2 10.5", popped.Text);
            Assert.AreEqual(eMessageCategory.Trace, popped.CategoryAsEnum);

        }

        [Test, Order(4100)]
        public void T4100_AsDebug()
        {
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.OnEventRisen;
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();

            sut.SingleCycleRun(
                () => sut.AsDebug("Text 1", "Text 2 ", 10.5));


            var plain = sut._messenger._mime.PlainMessage;
            var popped = suc._logger.Pop().FirstOrDefault();

            Assert.AreEqual("Text 1Text 2 10.5", plain.Text);
            Assert.AreEqual(eMessageCategory.Debug, plain.CategoryAsEnum);

            Assert.AreEqual("Text 1Text 2 10.5", popped.Text);
            Assert.AreEqual(eMessageCategory.Debug, popped.CategoryAsEnum);

        }

        [Test, Order(4200)]
        public void T4200_AsInfo()
        {
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.OnEventRisen;
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();

            sut.SingleCycleRun(
                () => sut.AsInfo("Text 1", "Text 2 ", 10.5));


            var plain = sut._messenger._mime.PlainMessage;
            var popped = suc._logger.Pop().FirstOrDefault();

            Assert.AreEqual("Text 1Text 2 10.5", plain.Text);
            Assert.AreEqual(eMessageCategory.Info, plain.CategoryAsEnum);

            Assert.AreEqual("Text 1Text 2 10.5", popped.Text);
            Assert.AreEqual(eMessageCategory.Info, popped.CategoryAsEnum);

        }

        [Test, Order(4300)]
        public void T4300_AsWarning()
        {
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.OnEventRisen;
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();

            sut.SingleCycleRun(
                () => sut.AsWarning("Text 1", "Text 2 ", 10.5));


            var plain = sut._messenger._mime.PlainMessage;
            var popped = suc._logger.Pop().FirstOrDefault();

            Assert.AreEqual("Text 1Text 2 10.5", plain.Text);
            Assert.AreEqual(eMessageCategory.Warning, plain.CategoryAsEnum);

            Assert.AreEqual("Text 1Text 2 10.5", popped.Text);
            Assert.AreEqual(eMessageCategory.Warning, popped.CategoryAsEnum);
        }

        [Test, Order(4400)]
        public void T4400_AsError()
        {
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.OnEventRisen;
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();

            sut.SingleCycleRun(
                () => sut.AsError("Text 1", "Text 2 ", 10.5));


            var plain = sut._messenger._mime.PlainMessage;
            var popped = suc._logger.Pop().FirstOrDefault();

            Assert.AreEqual("Text 1Text 2 10.5", plain.Text);
            Assert.AreEqual(eMessageCategory.Error, plain.CategoryAsEnum);

            Assert.AreEqual("Text 1Text 2 10.5", popped.Text);
            Assert.AreEqual(eMessageCategory.Error, popped.CategoryAsEnum);

        }

        [Test, Order(4500)]
        public void T4500_AsFatal()
        {
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.OnEventRisen;
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();

            sut.SingleCycleRun(
                () => sut.AsFatal("Text 1", "Text 2 ", 10.5));


            var plain = sut._messenger._mime.PlainMessage;
            var popped = suc._logger.Pop().FirstOrDefault();

            Assert.AreEqual("Text 1Text 2 10.5", plain.Text);
            Assert.AreEqual(eMessageCategory.Fatal, plain.CategoryAsEnum);

            Assert.AreEqual("Text 1Text 2 10.5", popped.Text);
            Assert.AreEqual(eMessageCategory.Fatal, popped.CategoryAsEnum);

        }

        [Test, Order(4600)]
        public void T4600_AppendAnys_1()
        {
            //! THIS IS TO TEST AppendAny from the messenger. In StringBuilder test we get an obscure compile time error see 'Utilities/StringBuilderTests/AppendAnyTest'
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.OnEventRisen;
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();

            sut.SingleCycleRun(
                () => sut.AppendAnys_1());


            var plain = sut._messenger._mime.PlainMessage;
            var popped = suc._logger.Pop().FirstOrDefault();

            Console.WriteLine(plain.Text);
            Assert.AreEqual("TRUE1223334444121314556677", plain.Text);
            Assert.AreEqual(eMessageCategory.Debug, plain.CategoryAsEnum);

            Assert.AreEqual("TRUE1223334444121314556677", popped.Text);
            Assert.AreEqual(eMessageCategory.Debug, popped.CategoryAsEnum);

        }

        [Test, Order(4600)]
        public void T4600_AppendAnys_2()
        {
            //! THIS IS TO TEST AppendAny from the messenger. In StringBuilder test we get an obscure compile time error see 'Utilities/StringBuilderTests/AppendAnyTest'
            //--Arrange            
            suc._logger.MinLogLevelCategory = eMessageCategory.All;
            sut._messageDigestMethod.Synchron = (short)eMessageDigestMethod.CRC32;
            sut._messageLoggingMethod.Synchron = (short)eMessengerLogMethod.OnEventRisen;
            sut._messenger.Clear();
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); sut.SetMessageDigestMethod(); sut.SetMessageLoggingMethod(); });
            suc._logger.Pop();

            sut.SingleCycleRun(
                () => sut.AppendAnys_2());


            var plain = sut._messenger._mime.PlainMessage;
            var popped = suc._logger.Pop().FirstOrDefault();

            Console.WriteLine(plain.Text);
            Assert.AreEqual("8899LTIME#551us615ns99.988.8DT#2106-02-06-06:28:15D#2106-02-06T#49d17h2m47s295msW1TOD#23:59:59.999S1", plain.Text);
            Assert.AreEqual(eMessageCategory.Debug, plain.CategoryAsEnum);

            Assert.AreEqual("8899LTIME#551us615ns99.988.8DT#2106-02-06-06:28:15D#2106-02-06T#49d17h2m47s295msW1TOD#23:59:59.999S1", popped.Text);
            Assert.AreEqual(eMessageCategory.Debug, popped.CategoryAsEnum);

        }

        private static T DestructPayload<T>(object payload, string propertyName)
        {
            try
            {
                var obj = payload.GetType().GetProperty("Payload").GetValue(payload);
                return (T)obj.GetType().GetProperty(propertyName).GetValue(obj);
            }
            catch (Exception)
            {

                return default(T);
            }
            
        }

#if EXT_LOCAL_TESTING
        [Test, Order(1300)]
        //[TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(200)]
        public void T813_MessagingPerfTest(short nofmessages)
        {
            var sut = ConnectorFixture.Connector.MAIN._tcoContextMessagingPerf;

            if (nofmessages > TcoContextMessagingPerf.__const_messagesUpperBound)
            {
                throw new ArgumentOutOfRangeException($"Testing message upper bound out of range");
            }

            sut._numberOfTestMessages.Synchron = nofmessages;

            sut.Read();

            var sw = new System.Diagnostics.Stopwatch();

            sw.Start();
            sut.RefreshActiveMessages();
            sw.Stop();

            Console.WriteLine($"Refresh active messages {sw.ElapsedMilliseconds} ms");
            Assert.IsTrue(sw.ElapsedMilliseconds < 100, $"{sw.ElapsedMilliseconds}");


            sw.Reset();
            sw.Start();
            var messages = sut.ActiveMessages.ToList();
            sw.Stop();


            Console.WriteLine($"Messages Online to Plain {sw.ElapsedMilliseconds} ms");
        }
#endif
    }
}