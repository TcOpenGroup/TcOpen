using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TcOpen.Inxton.Swift.TcAdapter.TcProject;

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
