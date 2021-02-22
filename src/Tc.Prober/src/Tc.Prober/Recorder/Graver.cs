namespace Tc.Prober.Recorder
{
    using Newtonsoft.Json;
    using System.IO;
    using System.Linq;
    using Vortex.Connector;

    internal class Graver<T, P> : RecorderBase<T, P>, IRecorder where T : IVortexObject, new() where P : IPlain, new()
    {
        public Graver(T obj, long minUniqueFrames = 10) : base(obj)
        {
            MinUniqueFrames = minUniqueFrames;
        }

        private long MinUniqueFrames { get; }

        public void Stop(string fileName)
        {
            var squashedRecording = Squash(this.recording);


            if (squashedRecording.Frames.LongCount() < MinUniqueFrames)
            {
                throw new InsufficientNumberOfFramesException($"There is no sufficient number of unique frames recorded ('{squashedRecording.Frames.LongCount()}'). Minimum required number of frames is '{MinUniqueFrames}'. " +
                                                              $"You can modify the value by setting 'minUniqueFrames' construction parameter.");
            }

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    var serializer = new Newtonsoft.Json.JsonSerializer();
                    serializer.Serialize(writer, squashedRecording);
                }
            }
        }

        private string FileName { get; }
        private IValueTag Stamper { get; }

        public void StartRecording()
        {
            recording.StartRecording();
        }

        public void RecordFrame()
        {
            recording.AddRecordFrame(new RecordFrame<P>() { Object = (P)GetPlainerCopyNow(Object) });
        }

        public void Begin(string fileName)
        {
            this.StartRecording();
        }

        public void Act()
        {
            this.RecordFrame();
        }

        public void End(string fileName)
        {
            this.Stop(fileName);
        }
    }
}
