using NUnit.Framework;
using TcOpen.Inxton.Swift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;
using System.IO;
using System.Reflection;

namespace TcOpen.Inxton.Swift.Tests
{
    [TestFixture()]
    public class RecorderTests
    {

        private static void CompareSources(string expectedFile, string actualFile)
        {
            var actual = File.ReadAllText(actualFile).Replace("\n", "").Replace("\r", "");
            var expected = File.ReadAllText(expectedFile).Replace("\n", "").Replace("\r", ""); ;      
           
            // Assert.AreEqual(expected.Length, actual.Length, "Number of lines does not match");
            Assert.AreEqual(expected, actual);           
        }

        private string outputFiles;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var assemblyFile = new FileInfo(Assembly.GetExecutingAssembly().Location);
            outputFiles = Path.GetFullPath(assemblyFile.DirectoryName + "..\\..\\..\\..\\output");
        }

        [Test()]
        public void RecorderTest()
        {
            var actual = new Recorder(new TcoCoreTests.fbPiston(new MockRootObject(), string.Empty, string.Empty));
            Assert.AreEqual(4, actual.Tasks.Count());
        }

        [Test()]
        public void GetCodeTest()
        {
            Assert.Ignore($"we compare actual files in {nameof(SaveSequenceTest)}");

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

        [Test()]
        public void SaveSequenceTest()
        {
            var recorderObject = new TcoCoreTests.fbPiston(new MockRootObject(), string.Empty, string.Empty);
            var outputFile = Path.Combine(outputFiles, "fbPiston.TcPOU");
            var expectedOutputFile = Path.Combine(outputFiles, "fbPiston.expected.TcPOU");

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


                recorder.SaveSequence(outputFiles, "fbPiston", "41ce95b1-694a-4bd3-b773-5382b98402d8", "c86ecde6-1911-404c-86d2-9ddbfc0b3cb4");

                Assert.IsTrue(File.Exists(outputFile));

                CompareSources(expectedOutputFile, outputFile);
            }
        }
    }



}