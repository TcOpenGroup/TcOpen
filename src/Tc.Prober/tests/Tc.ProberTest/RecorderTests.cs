namespace Tc.Prober.RecorderTests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Vortex.Connector;
    using PlcTcProberTests;
    using System.IO;
    using System.Reflection;
    using Tc.Prober.Recorder;
    using Vortex.Adapters.Connector.Tc3.Adapter;

    [TestFixture()]
    public class RecorderTests
    {

        private PlcTcProberTestsTwinController connector = null;
        private string RecordingFile;
        private string SquashTestFile;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {          
            var relativePath = Path.GetFullPath(Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, @"..\..\..\output\"));            
            RecordingFile = Path.Combine(relativePath, "baseRecord.rec");
            SquashTestFile = Path.Combine(relativePath, "squashRecord.rec");


            if (File.Exists(RecordingFile)) File.Delete(RecordingFile);
            if (File.Exists(SquashTestFile)) File.Delete(SquashTestFile);

            Assert.IsFalse(File.Exists(RecordingFile));
            Assert.IsFalse(File.Exists(SquashTestFile));


            connector = new PlcTcProberTestsTwinController(Tc3ConnectorAdapter.Create(null, 851, false));
            connector.Connector.BuildAndStart().ReadWriteCycleDelay = 10;
        }

        [Test()]
        [Order(100)]
        public void RecordTest()
        {                                                
            var recorder = new Graver<fbInheritanceLevel_5, PlainfbInheritanceLevel_5>(connector.MAIN.InheritanceRw);

            recorder.StartRecording();

            for (int i = 0; i < 254; i++)
            {
                connector.MAIN.InheritanceRw.level_0.BYTE_val.Cyclic = (byte)i;
                connector.MAIN.InheritanceRw.level_1.BYTE_val.Cyclic = (byte)i;
                connector.MAIN.InheritanceRw.level_2.WORD_val.Cyclic = (ushort)(i*2);
                connector.MAIN.InheritanceRw.level_2.STRING_val.Cyclic = (i * 180).ToString();
                connector.MAIN.InheritanceRw.level_3.WSTRING_val.Cyclic = (i * 258).ToString();
                connector.MAIN.InheritanceRw.Write();
                recorder.RecordFrame();
            }

            recorder.Stop(RecordingFile);

            Assert.IsTrue(File.Exists(RecordingFile), "Recroding file not created.");                                   
        }        

        [Test()]
        [Order(200)]
        public void PlaySelectedFramesTest()
        {           
            var recorder = new Player<fbInheritanceLevel_5, PlainfbInheritanceLevel_5>(connector.MAIN.InheritanceRw);
            recorder.StartPlay(RecordingFile);

            for (int i = 0; i < 254; i++)
            {
                var frame = recorder.PlayFrame((long)i);
                connector.MAIN.InheritanceRw.Read();
                Assert.AreEqual((byte)i, connector.MAIN.InheritanceRw.level_0.BYTE_val.Cyclic, $"Frame: {frame}:{i}");
                Assert.AreEqual((byte)i, connector.MAIN.InheritanceRw.level_1.BYTE_val.Cyclic, $"Frame: {frame}:{i}");
                Assert.AreEqual((ushort)(i * 2), connector.MAIN.InheritanceRw.level_2.WORD_val.Cyclic, $"Frame: {frame}:{i}");
                Assert.AreEqual((i * 180).ToString(), connector.MAIN.InheritanceRw.level_2.STRING_val.Cyclic, $"Frame: {frame}:{i}");
                Assert.AreEqual((i * 258).ToString(), connector.MAIN.InheritanceRw.level_3.WSTRING_val.Cyclic, $"Frame: {frame}:{i}");
            }
        }

        [Test()]
        [Order(300)]
        public void PlayFramesTest()
        {
            var recorder = new Player<fbInheritanceLevel_5, PlainfbInheritanceLevel_5>(connector.MAIN.InheritanceRw);
            recorder.StartPlay(RecordingFile);

            for (int i = 0; i < 254; i++)
            {
                var frame = recorder.PlayFrame();
                connector.MAIN.InheritanceRw.Read();
                Assert.AreEqual((byte)i, connector.MAIN.InheritanceRw.level_0.BYTE_val.Cyclic, $"Frame: {frame}:{i}");
                Assert.AreEqual((byte)i, connector.MAIN.InheritanceRw.level_1.BYTE_val.Cyclic, $"Frame: {frame}:{i}");
                Assert.AreEqual((ushort)(i * 2), connector.MAIN.InheritanceRw.level_2.WORD_val.Cyclic, $"Frame: {frame}:{i}");
                Assert.AreEqual((i * 180).ToString(), connector.MAIN.InheritanceRw.level_2.STRING_val.Cyclic, $"Frame: {frame}:{i}");
                Assert.AreEqual((i * 258).ToString(), connector.MAIN.InheritanceRw.level_3.WSTRING_val.Cyclic, $"Frame: {frame}:{i}");
            }
        }

        [Test()]
        [Order(1000)]
        public void SquashTest()
        {
            var recorder = new Graver<stAllTypes, PlainstAllTypes>(connector.MAIN.InheritanceRw.level_0);

            recorder.StartRecording();

            for (int i = 0; i < 254; i++)
            {
                if(i % 10 == 0)
                { 
                    connector.MAIN.InheritanceRw.level_0.STRING_val.Cyclic = i.ToString();
                    connector.MAIN.InheritanceRw.level_0.Write();
                }

                recorder.RecordFrame();
            }

            recorder.Stop(SquashTestFile);
            Assert.IsTrue(File.Exists(SquashTestFile), "Squash test recording file not created.");

            var player = new Player<stAllTypes, PlainstAllTypes>(connector.MAIN.InheritanceRw.level_0);

            player.StartPlay(SquashTestFile);

           

            foreach (var item in recorder.Recording.Frames)
            {
                Console.WriteLine($"{item.Stamp} : {item.Object.STRING_val}");
            }

            Assert.AreEqual(26, player.Recording.Frames.Count());
            Assert.AreEqual(9, player.Recording.Frames[0].Stamp);
            Assert.AreEqual("0", player.Recording.Frames[0].Object.STRING_val);
            Assert.AreEqual(19, player.Recording.Frames[1].Stamp);
            Assert.AreEqual("10", player.Recording.Frames[1].Object.STRING_val);
            Assert.AreEqual(253, player.Recording.Frames[25].Stamp);
            Assert.AreEqual("250", player.Recording.Frames[25].Object.STRING_val);
        }

        [Test()]
        [Order(1100)]
        public void PlaySquashedTest()
        {
            var recorder = new Player<stAllTypes, PlainstAllTypes>(connector.MAIN.InheritanceRw.level_0);

            recorder.StartPlay(SquashTestFile);


            var expected = 0;
            for (int i = 0; i < 254; i++)
            {                              
                Console.WriteLine($"{i}:{recorder.PlayFrame()} : {connector.MAIN.InheritanceRw.level_0.STRING_val.Cyclic}");
                if (i % 10 == 0)
                {
                    expected = i;
                }
                Assert.AreEqual(expected.ToString(), connector.MAIN.InheritanceRw.level_0.STRING_val.Cyclic);                
            }           
        }

        [Test()]
        [Order(30000)]
        public void PreventWriteTest()
        {
            var recorder = new Recorder<stAllTypes, PlainstAllTypes>(connector.MAIN.InheritanceRw.level_0, RecorderModeEnum.Graver).Actor;

            recorder.Begin(SquashTestFile);


            for (int i = 0; i < 254; i++)
            {
                if (i % 100 == 0)
                {
                    connector.MAIN.InheritanceRw.level_0.STRING_val.Cyclic = i.ToString();
                    connector.MAIN.InheritanceRw.level_0.Write();
                }

                recorder.Act();
            }
           
            Assert.Throws<InsufficientNumberOfFramesException>(() => recorder.End(SquashTestFile));
        }

        [Test()]
        [Order(30000)]
        public void PreventWriteTestMore()
        {
            var recorder = new Recorder<stAllTypes, PlainstAllTypes>(connector.MAIN.InheritanceRw.level_0, RecorderModeEnum.Graver, 100).Actor;

            recorder.Begin(SquashTestFile);


            for (int i = 0; i < 254; i++)
            {
                if (i % 10 == 0)
                {
                    connector.MAIN.InheritanceRw.level_0.STRING_val.Cyclic = i.ToString();
                    connector.MAIN.InheritanceRw.level_0.Write();
                }

                recorder.Act();
            }

            Assert.Throws<InsufficientNumberOfFramesException>(() => recorder.End(SquashTestFile));
        }
    }
}