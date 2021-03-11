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

        TcoCoreTests.TcoMessengerTests sut = ConnectorFixture.Connector.MAIN._TcoContextTest_A._TcoMessangerTests;
        TcoCoreTests.TcoContextTest suc = ConnectorFixture.Connector.MAIN._TcoContextTest_A;


        [OneTimeSetUp]
        public void OneSetup()
        {
            // This forces the time update. Multiple cycles are required to initialize.
            //sut.RunUntilEndConditionIsMet(() => Console.WriteLine(), () => sut.GetParent<TcoContext>()._rtc._isValid.Synchron);
            sut.RunUntilEndConditionIsMet(() => Console.WriteLine(), () => suc.IsRtcValid());
        }

        [SetUp]
        public void Setup()
        {
            sut._messenger._mime.Text.Synchron = string.Empty;
            sut._messenger._mime.TimeStamp.Synchron = new System.DateTime(1970, 12, 12);
            sut._messenger._mime.Category.Synchron = (short)eMessageCategory.None;
        }

        [Test, Order(100)]
        public void T801_DebugTest()
        {
            //--Arrange
            var messageText = "this is a debug message";

            //--Act
            sut.SingleCycleRun(() => sut.Debug(messageText));

            //--Assert

            Assert.AreEqual(sut.GetParent<TcoContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(sut._messenger._mime.Category.Synchron, (short)eMessageCategory.Debug);
            Assert.IsTrue(sut._messenger._mime.TimeStamp.Synchron >= DateTime.Now.Subtract(new TimeSpan(0, 0, 0)));
        }

        [Test, Order(200)]
        public void T802_TraceTest()
        {
            //--Arrange
            var messageText = "this is a trace message";

            //--Act
            sut.SingleCycleRun(() => sut.Trace(messageText));

            //--Assert

            Assert.AreEqual(sut.GetParent<TcoContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(sut._messenger._mime.Category.Synchron, (short)eMessageCategory.Trace);
            Assert.IsTrue(sut._messenger._mime.TimeStamp.Synchron >= DateTime.Now.Subtract(new TimeSpan(0, 0, 0)));
        }

        [Test, Order(300)]
        public void T803_NotifyTest()
        {
            //--Arrange
            var messageText = "this is a notify message";

            //--Act
            sut.SingleCycleRun(() => sut.Notify(messageText));

            //--Assert

            Assert.AreEqual(sut.GetParent<TcoContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(sut._messenger._mime.Category.Synchron, (short)eMessageCategory.Notification);
            Assert.IsTrue(sut._messenger._mime.TimeStamp.Synchron >= DateTime.Now.Subtract(new TimeSpan(0, 0, 0)));
        }

        [Test, Order(400)]
        public void T804_WarningTest()
        {
            //--Arrange
            var messageText = "this is a warning message";

            //--Act
            sut.SingleCycleRun(() => sut.Warning(messageText));

            //--Assert

            Assert.AreEqual(sut.GetParent<TcoContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(sut._messenger._mime.Category.Synchron, (short)eMessageCategory.Warning);
            Assert.IsTrue(sut._messenger._mime.TimeStamp.Synchron >= DateTime.Now.Subtract(new TimeSpan(0, 0, 0)));
        }


        [Test, Order(500)]
        public void T805_ErrorTest()
        {
            //--Arrange
            var messageText = "this is a error message";

            //--Act
            sut.SingleCycleRun(() => sut.Error(messageText));

            //--Assert

            Assert.AreEqual(sut.GetParent<TcoContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(sut._messenger._mime.Category.Synchron, (short)eMessageCategory.Error);
            Assert.IsTrue(sut._messenger._mime.TimeStamp.Synchron >= DateTime.Now.Subtract(new TimeSpan(0, 0, 0)));
        }

        [Test, Order(600)]
        public void T806_InfoTest()
        {
            //--Arrange
            var messageText = "this is a info message";

            //--Act
            sut.SingleCycleRun(() => sut.Info(messageText));

            //--Assert

            Assert.AreEqual(sut.GetParent<TcoContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(sut._messenger._mime.Category.Synchron, (short)eMessageCategory.Info);
            Assert.IsTrue(sut._messenger._mime.TimeStamp.Synchron >= DateTime.Now.Subtract(new TimeSpan(0, 0, 0)));
        }

        [Test, Order(700)]
        public void T807_ProgrammingTest()
        {
            //--Arrange
            var messageText = "this is a programming error message";

            //--Act
            sut.SingleCycleRun(() => sut.Programming(messageText));

            //--Assert

            Assert.AreEqual(sut.GetParent<TcoContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(sut._messenger._mime.Category.Synchron, (short)eMessageCategory.ProgrammingError);
            Assert.IsTrue(sut._messenger._mime.TimeStamp.Synchron >= DateTime.Now.Subtract(new TimeSpan(0, 0, 0)));
        }

        [Test, Order(800)]
        public void T808_CriticalTest()
        {
            //--Arrange
            var messageText = "this is a critical message";

            //--Act
            sut.SingleCycleRun(() => sut.Critical(messageText));

            //--Assert

            Assert.AreEqual(sut.GetParent<TcoContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(sut._messenger._mime.Category.Synchron, (short)eMessageCategory.Critical);
            Assert.IsTrue(sut._messenger._mime.TimeStamp.Synchron >= DateTime.Now.Subtract(new TimeSpan(0, 0, 0)));
        }

        [Test, Order(900)]
        public void T809_CatastrophicTest()
        {
            //--Arrange
            var messageText = "this is a catastrophic message";

            //--Act
            sut.SingleCycleRun(() => sut.Catastrophic(messageText));

            //--Assert

            Assert.AreEqual(sut.GetParent<TcoContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
            Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
            Assert.AreEqual(sut._messenger._mime.Category.Synchron, (short)eMessageCategory.Catastrophic);
            Assert.IsTrue(sut._messenger._mime.TimeStamp.Synchron >= DateTime.Now.Subtract(new TimeSpan(0, 0, 0)));
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

                    Assert.AreEqual(sut.GetParent<TcoContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Cycle.Synchron);
                    Assert.AreEqual(messageText, sut._messenger._mime.Text.Synchron);
                    Assert.AreEqual((eMessageCategory)sut._messenger._mime.Category.Synchron, category);
                    Assert.IsTrue(sut._messenger._mime.TimeStamp.Synchron >= DateTime.Now.Subtract(new TimeSpan(0, 0, 0)));
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

                    Assert.AreEqual(sut.GetParent<TcoContextTest>()._startCycleCount.Synchron, sut._messenger._mime.Message.Cycle);
                    Assert.AreEqual(sut._messenger._mime.Text.Synchron, sut._messenger._mime.Message.Text);
                    Assert.AreEqual((eMessageCategory)sut._messenger._mime.Category.Synchron, (eMessageCategory)sut._messenger._mime.Message.Category);
                    Assert.AreEqual(sut._messenger._mime.TimeStamp.Synchron, sut._messenger._mime.Message.TimeStamp);
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

            sut.GetParent<TcoContext>()?.UpdateMessages();

            Assert.IsTrue(sut._messenger._mime.IsActive);

            sut.SingleCycleRun(() => Console.WriteLine("a"));

            sut.GetParent<TcoContext>()?.UpdateMessages();
            
            Assert.IsFalse(sut._messenger._mime.IsActive);
        }
    }
}