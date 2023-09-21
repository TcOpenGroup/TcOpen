﻿using System;
using System.Linq;
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
                if (ControllerEvents.Ids.ContainsKey(errorCode))
                    additionalInfo = ControllerEvents.Ids.Where(key => key.Key == errorCode).FirstOrDefault().Value;
            

                return additionalInfo;

            }
        }
    }
    
}

