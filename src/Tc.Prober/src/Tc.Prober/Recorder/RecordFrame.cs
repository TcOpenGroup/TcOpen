namespace Tc.Prober.Recorder
{
    using Vortex.Connector;

    public class RecordFrame<P> where P : IPlain
    {
        public long Stamp { get; set; }
        public P Object { get; set; }
    }
}
