using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Swift;
using Vortex.Connector;
using TcoCore;
using System.Text.RegularExpressions;

namespace TcoAbbRobotics
{
    public partial class TcoIrc5_v_1_x_x
    {
        private const string blank = " ";
        private string onlineMsg = string.Empty; string additionalInfo = " ";


        public string AdvancedDiagnosticMessage
        {

            get
            {

                if (onlineMsg == _messenger._mime.Text.Cyclic)
                {
                    return additionalInfo;
                }


                onlineMsg = _messenger._mime.Text.Synchron;
                additionalInfo = blank;
                var numberFromString = string.Join(string.Empty, Regex.Matches(onlineMsg, @"\d+").OfType<Match>().Select(m => m.Value));


                uint errorCode;

                UInt32.TryParse(numberFromString, out errorCode);
                if (ControllerErrors.Errors.ContainsKey(errorCode))
                    additionalInfo = ControllerErrors.Errors.Where(key => key.Key == errorCode).FirstOrDefault().Value;
            

                return additionalInfo;

            }
        }
    }



    public static class ControllerErrors
    {
        public static IDictionary<uint, string> Errors = new Dictionary<uint, string>()
        {
            {0, "" },
            {16384, @"'Internal error' Internal system error in the NC on ring 0, no further details."}

            //todo errors fro irc5 controller
        };
          

    }
    
}


