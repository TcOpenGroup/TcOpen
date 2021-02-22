using System;
using System.Collections.Generic;
using System.Linq;
namespace Tc.Prober.Recorder
{
    using Vortex.Connector;

    public class Recording<P> where P : IPlain
    {
        public void StartRecording()
        {
            CurrentFramePosition = 0;
            Frames.Clear();
        }
        public void GoTop()
        {
            CurrentFramePosition = 0;
        }

        public long CurrentFramePosition
        {
            get;
            private set;
        }
        public void AddRecordFrame(RecordFrame<P> frame)
        {
            frame.Stamp = CurrentFramePosition++;
            Frames.Add(frame);
        }

        private long lastFrameSector = long.MaxValue;
        public RecordFrame<P> ReadFrame()
        {            
            var frame = Frames.Where(p => p.Stamp >= CurrentFramePosition && p.Stamp <= lastFrameSector).FirstOrDefault();
            var closestFrame = Frames.Where(p => p.Stamp > CurrentFramePosition).FirstOrDefault();
            lastFrameSector = closestFrame == null ? -1 : closestFrame.Stamp;
            CurrentFramePosition++;
            return frame;
        }

        public RecordFrame<P> ReadFrame(long position)
        {
            var frame = Frames.Where(p => p.Stamp >= position && p.Stamp <= lastFrameSector).FirstOrDefault();
            var closestFrame = Frames.Where(p => p.Stamp > position).FirstOrDefault();
            lastFrameSector = closestFrame == null ? -1 : closestFrame.Stamp;
            return frame;
        }

        public IList<RecordFrame<P>> Frames
        {
            get;
        } = new List<RecordFrame<P>>();
    }
}
