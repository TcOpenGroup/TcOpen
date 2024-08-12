using System;
using System.Diagnostics;

namespace TcoRexrothPressConnector.SmartfunctionKit.RestApi
{
    class Logger
    {
        public static Logger Create(Type type)
        {
            return new Logger(type);
        }

        public Logger(Type type)
        {
            m_loggerType = type;
            this.TraceInfo = true;
        }

        public void LogIf(TraceLevel level, string s)
        {
            System.Diagnostics.Debug.WriteLine(s);
        }

        public void Log(TraceLevel level, string s)
        {
            System.Diagnostics.Debug.WriteLine(s);
        }

        public void Log(string s)
        {
            System.Diagnostics.Debug.WriteLine(s);
        }

        public bool TraceInfo { get; }

        private Type m_loggerType;
    }
}
