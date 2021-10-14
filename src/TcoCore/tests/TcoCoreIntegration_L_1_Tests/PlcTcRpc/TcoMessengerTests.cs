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


        [OneTimeSetUp]
        public void OneSetup()
        {
            suc._callMyPlcInstanceRtcUpdate.Synchron = true;
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.All; sut.SetMinLevel(); });
            sut.SingleCycleRun(() => sut.Resume());
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
            Assert.LessOrEqual(_diff.TotalMilliseconds, 1000);
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
            Assert.LessOrEqual(_diff.TotalMilliseconds, 1000);
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
            Assert.LessOrEqual(_diff.TotalMilliseconds, 1000);
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
            Assert.LessOrEqual(_diff.TotalMilliseconds, 1000);
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
            Assert.LessOrEqual(_diff.TotalMilliseconds, 1000);
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
            Assert.LessOrEqual(_diff.TotalMilliseconds, 1000);
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
            Assert.LessOrEqual(_diff.TotalMilliseconds, 1000);
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
            Assert.LessOrEqual(_diff.TotalMilliseconds, 1000);
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
            Assert.LessOrEqual(_diff.TotalMilliseconds, 1000);
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
            Assert.LessOrEqual(_diff.TotalMilliseconds, 1000);

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
                    Assert.LessOrEqual(_diff.TotalMilliseconds, 1000);
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

            //--Act
            sut.SingleCycleRun(() => sut.Catastrophic(messageText));

            var handler = sut.GetParent<TcoMessengerContextTest>()?.MessageHandler;

            handler.DiagnosticsDepth = 1000;

            handler.GetActiveMessages();

            Assert.IsTrue(sut._messenger._mime.IsActive);

            sut.SingleCycleRun(() => Console.WriteLine("a"));

            sut.GetParent<TcoMessengerContextTest>()?.MessageHandler.GetActiveMessages();


            Assert.IsFalse(sut._messenger._mime.IsActive);
        }

        [Test, Order(1400)]
        public void T813_SuspendResumeTest()
        {
            //--Arrange
            var messageText = "this is a message";
            sut.SingleCycleRun(() => Console.WriteLine("Empty cycle"));

            //--Act

            // Suspend
            sut.SingleCycleRun(() => sut.Suspend());

            sut.SingleCycleRun(() => sut.Catastrophic(messageText));

            sut.GetParent<TcoMessengerContextTest>()?.MessageHandler.GetActiveMessages();

            Assert.IsFalse(sut._messenger._mime.IsActive);


            // Resume
            sut.SingleCycleRun(() => sut.Resume());

            sut.SingleCycleRun(() => sut.Catastrophic(messageText));

            var handler = sut.GetParent<TcoMessengerContextTest>()?.MessageHandler;

            handler.DiagnosticsDepth = 1000;

            handler.GetActiveMessages();

            Assert.IsTrue(sut._messenger._mime.IsActive);

        }

        [Test, Order(1400)]
        public void T814_SetMinMessageCategoryTest()
        {
            //--Arrange
            var messageText = "this is a message";
            sut.SingleCycleRun(() => Console.WriteLine("Empty cycle"));

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

            sut.GetParent<TcoMessengerContextTest>()?.MessageHandler.GetActiveMessages();

            Assert.IsFalse(sut._messenger._mime.IsActive);


            // post catastrophic

            sut.SingleCycleRun(() => sut.Catastrophic(messageText));

            sut.GetParent<TcoMessengerContextTest>()?.MessageHandler.GetActiveMessages();

            Assert.IsTrue(sut._messenger._mime.IsActive);
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
            dummyLogger.QueueMessages();

            suc._logger.LogMessages(messages);
            

            (string message, object payload, string serverity) result;
            IList<(string message, object payload, string serverity)> dequeuedMessages = new List<(string message, object payload, string serverity)>();
            while (dummyLogger.MessageQueue.TryDequeue(out result))
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
        [TestCase(eMessageDigestMethod.CRC16, (uint)16986)]
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