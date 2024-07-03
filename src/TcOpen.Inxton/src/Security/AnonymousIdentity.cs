namespace TcOpen.Inxton.Local.Security
{
    public partial class AppIdentity
    {
        public class AnonymousIdentity : AppIdentity
        {
            public AnonymousIdentity()
                : base(string.Empty, string.Empty, new string[] { }, false, string.Empty) { }
        }
    }
}
