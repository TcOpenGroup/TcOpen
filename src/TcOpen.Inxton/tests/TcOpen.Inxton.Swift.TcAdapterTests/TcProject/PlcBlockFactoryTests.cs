using NUnit.Framework;
using TcOpen.Inxton.Swift.TcAdapter.TcProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace TcOpen.Inxton.Swift.TcAdapter.TcProject.Tests
{
    [TestFixture()]
    public class PlcBlockFactoryTests
    {
        private string outputFiles;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var assemblyFile = new FileInfo(Assembly.GetExecutingAssembly().Location);
            outputFiles = Path.GetFullPath(assemblyFile.DirectoryName + "..\\..\\..\\..\\output");
        }

        [Test()]
        public void CreateSequencerPlcBlockTest()
        {
            var blockName = "my_sequencer";
            var mainMethodImplementation = "IF TRUE THEN; END_IF;";
            var actual = PlcBlockHelpers.CreateSequencerPlcBlock(blockName, mainMethodImplementation);

            Assert.AreEqual($"FUNCTION_BLOCK {blockName} EXTENDS TcoCore.TcoSequencer", actual.POU.Declaration);
            Assert.AreEqual($"{mainMethodImplementation}", 
                actual.POU.Method.Where(p => p.Name == "Main").FirstOrDefault().Implementation.ST);
            
            
        }

        [Test()]
        public void EmitPlcBlockFileTest()
        {

            var blockName = "my_sequencer";
            var mainMethodImplementation = "IF TRUE THEN; END_IF;";
            var block = PlcBlockHelpers.CreateSequencerPlcBlock(blockName, mainMethodImplementation);
            var outputFile = Path.Combine(outputFiles, $"{blockName}.TcPOU");
            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
            }

            Assert.IsFalse(File.Exists(outputFile));

            PlcBlockHelpers.EmitPlcBlockFile(outputFile, block);

            Assert.IsTrue(File.Exists(outputFile));
        }
    }
}