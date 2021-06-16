using NUnit.Framework;
using TcOpen.Inxton.Swift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;

namespace TcOpen.Inxton.Swift.Tests
{
    [TestFixture()]
    public class RecorderTests
    {
        [Test()]
        public void RecorderTest()
        {           
            var actual = new Recorder(new TcoCoreTests.fbPiston(new MockRootObject(), string.Empty, string.Empty));            
            Assert.AreEqual(4, actual.Tasks.Count());
        }

        [Test()]
        public void GetCodeTest()
        {
            var recorderObject = new TcoCoreTests.fbPiston(new MockRootObject(), string.Empty, string.Empty);
            using (var recorder = new Recorder(recorderObject))
            {
                recorderObject._moveHomeTask._enabled.Synchron = true;
                recorderObject._moveHomeTask._isServiceable.Synchron = true;
                recorderObject._moveHomeTask.Execute(null);

                recorderObject._moveWorkTask._enabled.Synchron = true;
                recorderObject._moveWorkTask._isServiceable.Synchron = true;
                recorderObject._moveWorkTask.Execute(null);

                recorderObject._moveHomeToggleTask._enabled.Synchron = true;
                recorderObject._moveHomeToggleTask._isServiceable.Synchron = true;
                recorderObject._moveHomeToggleTask.Execute(null);

                recorderObject._moveHomeToggleTask._enabled.Synchron = true;
                recorderObject._moveHomeToggleTask._isServiceable.Synchron = true;
                recorderObject._moveHomeToggleTask.Execute(null);

                recorderObject._moveHomeMomentaryTask._enabled.Synchron = true;
                recorderObject._moveHomeMomentaryTask._isServiceable.Synchron = true;
                recorderObject._moveHomeMomentaryTask.Start();
                recorderObject._moveHomeMomentaryTask.Stop();

                var actual = recorder.EmitCode().Replace("\n", "").Replace("\r", "");
                var expected =
                #region
@"

IF Step(10,TRUE,'-')
//-------------------------------------------------------
	IF(_moveHomeTask.Invoke().Done)THEN
		StepCompleteWhen(TRUE);
	END_IF;
//-------------------------------------------------------
END_IF;

IF Step(20,TRUE,'-')
//-------------------------------------------------------
	IF(_moveWorkTask.Invoke().Done)THEN
		StepCompleteWhen(TRUE);
	END_IF;
//-------------------------------------------------------
END_IF;

IF Step(30,TRUE,'-')
//-------------------------------------------------------
	IF(_moveHomeToggleTask.Toggle())THEN
		StepCompleteWhen(TRUE);
	END_IF;
//-------------------------------------------------------
END_IF;

IF Step(40,TRUE,'-')
//-------------------------------------------------------
	IF(_moveHomeToggleTask.Toggle())THEN
		StepCompleteWhen(TRUE);
	END_IF;
//-------------------------------------------------------
END_IF;

IF Step(50,TRUE,'-')
//-------------------------------------------------------
	IF(_moveHomeMomentaryTask.On())THEN
		StepCompleteWhen(TRUE);
	END_IF;
//-------------------------------------------------------
END_IF;

IF Step(60,TRUE,'-')
//-------------------------------------------------------
	IF(_moveHomeMomentaryTask.Off())THEN
		StepCompleteWhen(TRUE);
	END_IF;
//-------------------------------------------------------
END_IF;
".Replace("\n", "").Replace("\r", "");
                #endregion
                        
                Assert.AreEqual(expected, actual);
            }            
        }
    }


    
}