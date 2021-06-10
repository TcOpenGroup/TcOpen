using NUnit.Framework;
using TcoCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoCore.Tests
{
    [TestFixture()]
    public class TcoMomentaryTaskViewModelTests
    {
        [Test()]
        public void TcoMomentaryTaskViewModelTest()
        {
            var task = new TcoMomentaryTask(new MockRootObject(), string.Empty, string.Empty);
            var vm = new TcoMomentaryTaskViewModel() { Model = task };

            Assert.IsNotNull(vm);
            Assert.IsNotNull(vm.TcoMomentaryTask);
            Assert.IsNotNull(vm.Model);

            Assert.IsInstanceOf<TcoMomentaryTask>(vm.TcoMomentaryTask);
            Assert.IsInstanceOf<TcoMomentaryTask>(vm.Model);
        }

        [Test()]
        public void button_down_task_enabled_execute()
        {
            var task = new TcoMomentaryTask(new MockRootObject(), string.Empty, string.Empty);
            task._isServiceable.Synchron = true;
            task._enabled.Synchron = true;
            var vm = new TcoMomentaryTaskViewModel() { Model = task };

            vm.ButtonDown.Execute(null);

            Assert.IsTrue(vm.TcoMomentaryTask._setOnRequest.Synchron);
        }

        [Test()]
        public void button_up_task_enabled_execute()
        {
            var task = new TcoMomentaryTask(new MockRootObject(), string.Empty, string.Empty);
            task._isServiceable.Synchron = true;
            task._enabled.Synchron = true;
            var vm = new TcoMomentaryTaskViewModel() { Model = task };

            vm.ButtonUp.Execute(null);

            Assert.IsFalse(vm.TcoMomentaryTask._setOnRequest.Synchron);
        }

        [Test()]
        public void get_caption()
        {
            var task = new TcoMomentaryTask(new MockRootObject(), string.Empty, string.Empty);

            task.GetConnector().BuildAndStart();

            task._isServiceable.Synchron = true;
            task._enabled.Synchron = true;
            task.AttributeName = "momentary task button";
            task.AttributeStateOffDesc = "is off";
            task.AttributeStateOnDesc = "is on";
            var vm = new TcoMomentaryTaskViewModel() { Model = task };

            Assert.AreEqual("momentary task button : is off", vm.Caption);

            task._state.Synchron = true;

            vm.UpdateCaption();

            Assert.AreEqual("momentary task button : is on", vm.Caption);
        }
    }
}