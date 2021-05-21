namespace Tc.Prober.Recorder
{    
    using System.Collections.Generic;
    using Vortex.Connector;

    internal class Player<T, P> : RecorderBase<T, P>, IRecorder where T : IVortexObject, new() where P : IPlain, new()
    {
        public Player(T obj) : base(obj)
        {

        }

        public IEnumerable<bool> Play(string fileName)
        {
            var valueTags = this.Object.RetrieveValueTags();
            Recording<P> rec = LoadRecording(fileName);

            foreach (var item in rec.Frames)
            {
                CopyToOnline(this.Object, item.Object);
                yield return false;
            }

            yield return true;
        }

        public void StartPlay(string fileName)
        {
            this.recording = LoadRecording(fileName);
            recording.GoTop();
        }

        public long PlayFrame()
        {
            var frame = this.recording.ReadFrame();
            if (frame != null)
            {
                CopyToOnline(this.Object, frame.Object);
                return frame.Stamp;
            }

            return -1;
        }

        public long PlayFrame(long position)
        {
            var frame = this.recording.ReadFrame(position);
            if (frame != null)
            {
                CopyToOnline(this.Object, frame.Object);
                return frame.Stamp;
            }

            return -1;
        }

        public void Begin(string fileName)
        {
            this.StartPlay(fileName);
        }

        public void Act()
        {
            this.PlayFrame();
        }

        public void End(string fileName)
        {

        }
    }
}
