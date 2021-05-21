namespace Tc.Prober.Recorder
{
    public interface IRecorder
    {
        void Begin(string fileName);
        void Act();
        void End(string fileName);
    }
}
