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
                if (ControlerEvents.Ids.ContainsKey(errorCode))
                    additionalInfo = ControlerEvents.Ids.Where(key => key.Key == errorCode).FirstOrDefault().Value;
            

                return additionalInfo;

            }
        }
    }

    public partial class TcoOmnicore_v_1_x_x
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
                if (ControlerEvents.Ids.ContainsKey(errorCode))
                    additionalInfo = ControlerEvents.Ids.Where(key => key.Key == errorCode).FirstOrDefault().Value;


                return additionalInfo;

            }
        }
    }

    public static class ControlerEvents
    {
        public static IDictionary<uint, string> Ids = new Dictionary<uint, string>()
        {
            {0, "" },
            {90205, @"Auto stop."},
            {50204, @"Colision detected."}

            //todo errors from  controller waiting for list
        };
          

    }
    
}


