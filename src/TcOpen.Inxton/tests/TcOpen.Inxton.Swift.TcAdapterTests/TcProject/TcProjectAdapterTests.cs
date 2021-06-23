using NUnit.Framework;
using TcOpen.Inxton.Swift.TcAdapter.TcProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace TcOpen.Inxton.Swift.TcAdapter.TcProject.Tests
{
    [TestFixture()]
    public class TcProjectAdapterTests
    {
        private string outputFiles;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            outputFiles = Path.GetFullPath(Assembly.GetExecutingAssembly().Location + "..\\..\\");
        }

        [Test()]
        public void SavePlcBlockTest()
        {
            var adapter = new TcProjectAdapter();
            adapter.PlcBlock.POU = new POU();
            adapter.PlcBlock.POU.Implementation = new Implementation();
            adapter.PlcBlock.POU.Implementation.ST = "IF THEN";
            adapter.SavePlcBlock(Path.Combine(outputFiles, "saveBlock.xml"));
        }
    }
}