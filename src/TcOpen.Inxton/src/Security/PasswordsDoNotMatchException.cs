using System;

namespace TcOpen.Inxton.Security
{
    public class PasswordsDoNotMatchException : Exception
    {
        public PasswordsDoNotMatchException(string str) : base(str)
        {
            
        }        
    }
}
