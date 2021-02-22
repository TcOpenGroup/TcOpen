namespace Tc.Prober.Recorder
{

    using Vortex.Connector;

    public class Recorder<T, P> where T : IVortexObject, new() where P : IPlain, new()
    {
        public Recorder(T obj, RecorderModeEnum mode, long minUniqueFrames = 10)
        {
            this.Mode = mode;

            switch (mode)
            {
                case RecorderModeEnum.None:
                    Actor = new DummyRecorder();
                    break;
                case RecorderModeEnum.Player:
                    Actor = new Player<T, P>(obj);
                    break;
                case RecorderModeEnum.Graver:
                    Actor = new Graver<T, P>(obj, minUniqueFrames);
                    break;
            }
        }

        public RecorderModeEnum Mode
        {
            get;
        }

        public IRecorder Actor
        {
            get;
        }
    }
}
