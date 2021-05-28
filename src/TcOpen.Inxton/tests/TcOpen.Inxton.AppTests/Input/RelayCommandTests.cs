using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Input;

namespace TcOpen.Inxton.InputTests
{
    [TestFixture()]
    public class RelayCommandTests
    {
        [Test()]
        public void RelayCommandTest()
        {
            var actual = new RelayCommand(action => Console.WriteLine("ctor test"));
            Assert.IsNotNull(actual);
        }

        [Test()]
        [TestCase(true)]
        public void CanExecuteTest(bool canExecute)
        {
            var actual = new RelayCommand(action => Console.WriteLine("can execute test"), () => canExecute);
            Assert.AreEqual(canExecute, actual.CanExecute(null));
        }

        [Test()]
        public void ExecuteTest_can_execute_true()
        {
            var didIdoIt = "No";
            var actual = new RelayCommand(action => didIdoIt = "Yes!", () => true);

            actual.Execute(null);

            Assert.AreEqual("Yes!", didIdoIt);
        }

        [Test()]
        public void ExecuteTest_can_execute_false()
        {
            var didIdoIt = "No";
            var actual = new RelayCommand(action => didIdoIt = "Yes!", () => false);

            actual.Execute(null);

            Assert.AreEqual("No", didIdoIt);
        }

        [Test()]
        public void ExecuteTest_with_log_action()
        {
            var didIdoIt = "No";
            var logActionResult = "";
            var actual = new RelayCommand(action => didIdoIt = "Yes!", () => true, () => logActionResult = "Command has been executed");

            actual.Execute(null);

            Assert.AreEqual("Command has been executed", logActionResult);
            Assert.AreEqual("Yes!", didIdoIt);
        }
    }
}