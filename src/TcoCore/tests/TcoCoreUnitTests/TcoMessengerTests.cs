using NUnit.Framework;
using System;
using System.Threading;
using TcoCore;
using TcoCoreTests;
using TcoCoreUnitTests;
using System.Linq;
using Vortex.Connector;

namespace TcoCoreUnitTests
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
            Assert.AreEqual((short)eMessageCategory.None, sut._messenger._mime.Category.Synchron);
            Assert.AreEqual(0, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(0, sut._messenger._mime.Identity.Synchron);
            Assert.AreEqual(0, sut._messenger._mime.PerCycleCount.Synchron);
        }

        [Test, Order(1000)]
        public void T810_PostLevelUpSeverityTest()
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
        [TestCase(new eMessageCategory[] { eMessageCategory.Trace,
                                           eMessageCategory.Debug
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

            sut.GetParent<TcoMessengerContextTest>()?.RefreshActiveMessages();

            Assert.IsTrue(sut._messenger._mime.IsActive);

            sut.SingleCycleRun(() => Console.WriteLine("a"));

            sut.GetParent<TcoMessengerContextTest>()?.RefreshActiveMessages();
            
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

            sut.GetParent<TcoMessengerContextTest>()?.RefreshActiveMessages();

            Assert.IsFalse(sut._messenger._mime.IsActive);


            // Resume
            sut.SingleCycleRun(() => sut.Resume());

            sut.SingleCycleRun(() => sut.Catastrophic(messageText));

            sut.GetParent<TcoMessengerContextTest>()?.RefreshActiveMessages();

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
            sut.GetParent<TcoMessengerContextTest>()?.RefreshActiveMessages();

            Assert.IsTrue(sut._messenger._mime.IsActive);

            // set min level to catastrophic
            sut.SingleCycleRun(() => { sut._minLevel.Synchron = (short)eMessageCategory.Catastrophic; sut.SetMinLevel(); });

            // post info
            sut.SingleCycleRun(() => sut.Info(messageText));

            sut.GetParent<TcoMessengerContextTest>()?.RefreshActiveMessages();

            Assert.IsFalse(sut._messenger._mime.IsActive);


            // post catastrophic

            sut.SingleCycleRun(() => sut.Catastrophic(messageText));

            sut.GetParent<TcoMessengerContextTest>()?.RefreshActiveMessages();

            Assert.IsTrue(sut._messenger._mime.IsActive);
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