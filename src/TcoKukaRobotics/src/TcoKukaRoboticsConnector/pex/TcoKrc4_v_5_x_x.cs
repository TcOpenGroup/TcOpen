using System;
using System.Linq;
using System.Text.RegularExpressions;
using Vortex.Connector;

namespace TcoKukaRobotics
{
    public partial class TcoKrc4_v_5_x_x
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
                if (TcoKrc4ControllerEvents_v_5_x_x.Ids.ContainsKey(errorCode))
                    additionalInfo = TcoKrc4ControllerEvents_v_5_x_x.Ids.Where(key => key.Key == errorCode).FirstOrDefault().Value;


                return additionalInfo;

            }
        }


       





    }
    
}


