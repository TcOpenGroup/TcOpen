namespace TcOpen.Inxton.Security
{
    public partial class VortexIdentity
    {
        public class AnonymousIdentity : VortexIdentity
        {
            public AnonymousIdentity()
                : base(string.Empty, string.Empty, new string[] { }, false, string.Empty)
            { }
        }
    }
}
