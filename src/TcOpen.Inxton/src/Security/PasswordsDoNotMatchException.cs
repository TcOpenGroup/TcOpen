using System;

namespace TcOpen.Inxton.Local.Security
{
    public class PasswordsDoNotMatchException : Exception
    {
        public PasswordsDoNotMatchException(string str)
            : base(str) { }
    }
}
