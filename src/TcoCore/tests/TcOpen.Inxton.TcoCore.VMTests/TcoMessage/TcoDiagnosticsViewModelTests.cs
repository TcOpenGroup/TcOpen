using NUnit.Framework;
using TcoCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;
using TcOpen.Inxton.Input;

namespace TcoCore.Tests
{
    [TestFixture()]
    public class TcoDiagnosticsViewModelTests
    {

        [Test()]
        public void CtorTcoDiagnosticsViewModelTest()
        {
            var vm = new TcoDiagnosticsViewModel();
            Assert.IsNotNull(vm);
            Assert.IsInstanceOf<RelayCommand>(vm.UpdateMessagesCommand);
        }

           
        [Test()]
        public void ModelTest()
        {
            var vm = new TcoDiagnosticsViewModel() { Model = new TcoContext(new MockRootObject(), string.Empty, string.Empty) };      
            Assert.IsInstanceOf<IsTcoObject>(vm.Model);
        }

        [Test()]
        public void CtorTcoDiagnosticsViewModelTest1()
        {
            var twin = new TcoContext(new MockRootObject(), string.Empty, string.Empty);

            var vm = new TcoDiagnosticsViewModel(twin);
            Assert.IsNotNull(vm);
        }

        [Test()]
        public void AutoUpdateTest()
        {
            var twin = new TcoContext(new MockRootObject(), string.Empty, string.Empty);

            var vm = new TcoDiagnosticsViewModel(twin);
            vm.AutoUpdate = true;
            var expected = true;

            Assert.AreEqual(expected, vm.AutoUpdate);
        }

        [Test()]
        public void DiagnosticsRunningTest()
        {
            var twin = new TcoContext(new MockRootObject(), string.Empty, string.Empty);

            var vm = new TcoDiagnosticsViewModel(twin);
            vm.DiagnosticsRunning = true;
            var expected = true;

            Assert.AreEqual(expected, vm.DiagnosticsRunning);
        }

        [Test()]
        public void GetCategoriesTest()
        {
            var twin = new TcoContext(new MockRootObject(), string.Empty, string.Empty);

            var vm = new TcoDiagnosticsViewModel(twin);
            var expected = Enum.GetValues(typeof(eMessageCategory)).Cast<eMessageCategory>().Skip(1);

            Assert.AreEqual(expected, vm.Categories);
        }

        [Test()]
        public void MessageDisplayTest()
        {
            var context = new TcoContext(new MockRootObject(), "context", "context");
            var objL1 = new TcoObject(context, "objL1", "objL1");
            var objL2 = new TcoObject(objL1, "objL2", "objL2");


            context._startCycleCount.Cyclic = 1458;
            objL1._messenger._mime.Category.Cyclic = (short)eMessageCategory.Catastrophic;
            objL1._messenger._mime.Cycle.Cyclic = context._startCycleCount.LastValue;
            objL1._messenger._mime.Text.Cyclic = "hello from sequence";

            objL2._messenger._mime.Category.Cyclic = (short)eMessageCategory.Catastrophic;
            objL2._messenger._mime.Cycle.Cyclic = context._startCycleCount.LastValue;
            objL2._messenger._mime.Text.Cyclic = "hello from mode controller";

            context.Write();

            var vm = new TcoDiagnosticsViewModel(context);
            vm.MinMessageCategoryFilter = eMessageCategory.Info;

            vm.UpdateMessages();

            Assert.AreEqual(2, vm.MessageDisplay.Count());

            var expected = new PlainTcoMessage();
            objL1._messenger._mime.FlushOnlineToPlain(expected);
            expected.Source = objL1.Symbol;

            Assert.AreEqual(expected.ToString(), vm.MessageDisplay.ToList()[0].ToString());

            expected = new PlainTcoMessage();
            objL2._messenger._mime.FlushOnlineToPlain(expected);
            expected.Source = objL2.Symbol;

            Assert.AreEqual(expected.ToString(), vm.MessageDisplay.ToList()[1].ToString());
        }

        [Test()]
        public void TcoDiagnosticsViewModelTest()
        {
            var twin = new TcoContext(new MockRootObject(), string.Empty, string.Empty);

            var vm = new TcoDiagnosticsViewModel(twin);
         
            Assert.IsInstanceOf<IsTcoObject>(vm.Model);
        }

        [Test()]
        public void SelectedMessageTest()
        {
            var twin = new TcoContext(new MockRootObject(), string.Empty, string.Empty);            
            var vm = new TcoDiagnosticsViewModel(twin);
            var message = new PlainTcoMessage() { Text = "message text" };
            vm.SelectedMessage = message;

            Assert.AreEqual(vm.SelectedMessage.ToString(), message.ToString());
        }

        [Test()]
        public void UpdateMessagesCommandTest()
        {
            var context = new TcoContext(new MockRootObject(), "context", "context");
            var objL1 = new TcoObject(context, "objL1", "objL1");
            var objL2 = new TcoObject(objL1, "objL2", "objL2");


            context._startCycleCount.Cyclic = 1458;
            objL1._messenger._mime.Category.Cyclic = (short)eMessageCategory.Catastrophic;
            objL1._messenger._mime.Cycle.Cyclic = context._startCycleCount.LastValue;
            objL1._messenger._mime.Text.Cyclic = "hello from sequence";

            objL2._messenger._mime.Category.Cyclic = (short)eMessageCategory.Catastrophic;
            objL2._messenger._mime.Cycle.Cyclic = context._startCycleCount.LastValue;
            objL2._messenger._mime.Text.Cyclic = "hello from mode controller";

            context.Write();

            var vm = new TcoDiagnosticsViewModel(context);
            vm.MinMessageCategoryFilter = eMessageCategory.Info;

            vm.UpdateMessagesCommand.Execute(null);

            Assert.AreEqual(2, vm.MessageDisplay.Count());

            var expected = new PlainTcoMessage();
            objL1._messenger._mime.FlushOnlineToPlain(expected);
            expected.Source = objL1.Symbol;

            Assert.AreEqual(expected.ToString(), vm.MessageDisplay.ToList()[0].ToString());

            expected = new PlainTcoMessage();
            objL2._messenger._mime.FlushOnlineToPlain(expected);
            expected.Source = objL2.Symbol;

            Assert.AreEqual(expected.ToString(), vm.MessageDisplay.ToList()[1].ToString());
        }
    }
}