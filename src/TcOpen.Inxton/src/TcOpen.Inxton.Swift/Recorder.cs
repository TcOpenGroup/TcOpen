using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoCore;
using Vortex.Connector;

namespace TcOpen.Inxton.Swift
{
    public class Recorder : ICodeRecorder, IDisposable
    {
        public IEnumerable<IsTask> Tasks { get; } = new List<IsTask>();

        public Recorder(IVortexObject vortexObject)
        {
            _recordedObject = vortexObject;            
            Tasks = _recordedObject.GetDescendants<IsTask>();

            foreach (var task in Tasks)
            {
                task.RecordTaskAction = this.Record;
            }
        }

        private readonly IVortexObject _recordedObject;

        readonly Sequence sequence = new Sequence();
        public Sequence Sequence => sequence;
        
        public delegate void RecordAddedDelegate();

        public event RecordAddedDelegate OnRecordAdded;

        public void Record(ICodeProvider codeProvider, params object[] args)
        {
            var step =  Sequence.AddStep(codeProvider.Origin);
            step.AddStatement(codeProvider.Code(args));
            OnRecordAdded?.Invoke();
        }

        public string EmitCode()
        {                        
            return Sequence.EmitCode(new StringBuilder()).ToString();
        }

        public void SaveSequence(string outputDirectory, string blockName, string blockGuid = null, string mainMethodGuid = null)
        {
            var outputFile = Path.Combine(outputDirectory, $"{blockName}.TcPOU");
            var sequenceBlock = TcAdapter.TcProject.PlcBlockHelpers.CreateSequencerPlcBlock(blockName, this.EmitCode(), blockGuid, mainMethodGuid);
            TcAdapter.TcProject.PlcBlockHelpers.EmitPlcBlockFile(outputFile, sequenceBlock);
        }

        public void Dispose()
        {
            foreach (var task in Tasks)
            {
                task.RecordTaskAction = null;
            }
        }
    }
}
