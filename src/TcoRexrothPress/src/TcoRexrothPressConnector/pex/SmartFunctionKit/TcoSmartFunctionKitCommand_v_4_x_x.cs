using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TcoRexrothPress
{
    public partial class TcoSmartFunctionKitCommand_v_4_x_x
    {
        private const string blank = " ";
        private string onlineMsg = string.Empty;
        string additionalInfo = " ";

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
                var numberFromString = string.Join(
                    string.Empty,
                    Regex.Matches(onlineMsg, @"\d+").OfType<Match>().Select(m => m.Value)
                );

                int errorCode;

                Int32.TryParse(numberFromString, out errorCode);
                if (ErrorCodes.ErrorCode.ContainsKey(errorCode))
                    additionalInfo = ErrorCodes
                        .ErrorCode.Where(key => key.Key == errorCode)
                        .FirstOrDefault()
                        .Value;
                else if ((errorCode >= 30001) && (errorCode <= 31901))
                    additionalInfo = ErrorCodes
                        .ErrorCode.Where(key => key.Key == 30001)
                        .FirstOrDefault()
                        .Value;

                return additionalInfo;
            }
        }
    }

    public static class ErrorCodes
    {
        public static IDictionary<int, string> ErrorCode;

        static ErrorCodes()
        {
            ErrorCode = new Dictionary<int, string>()
            {
                { 10001, "No function or invalid input" },
                { 10205, "Invalid character given within alphanumeric customID" },
                { 10206, "Invalid entry" },
                { 10207, "Invalid data type" },
                { 10208, "Variable index exceeded" },
                { 10209, "Value too small" },
                { 10210, "Value too large" },
                { 10211, "Value is invalid" },
                { 20101, "Command active, command transition not permitted" },
                { 30001, "Internal error" },
                { 31901, "Internal error" },
                { 31007, "No available space left on IPC (PR21)" },
                { 40101, "Clear Error Command not possible" },
                { 40201, "Reading of SMC variables not possible" },
                { 40301, "Positioning command cannot be started" },
                { 40302, "Error in the processing of the stop command" },
                { 40303, "Target position was not reached" },
                { 40304, "Manual positioning command: Force limit exceeded" },
                { 40401, "Reading of motor feedback memory not possible" },
                { 40501, "Reboot of drive not possible" },
                { 40601, "Homing could not be started" },
                { 40602, "Referencing under load not possible" },
                { 40701, "Could not activate program" },
                { 40702, "Timeout while program will be activated" },
                { 40703, "Timeout after program is activated" },
                { 40801, "Could not write SMC variable" },
                { 40901, "Transition Error Parameter Mode <> Operation Mode" },
                { 41001, "Could not start program" },
                { 41002, "Could not stop program" },
                { 41003, "Program error: Max. position exceeded" },
                { 41004, "Program error: Max. force exceeded" },
                { 41005, "Program error: Cancelled manually" },
                { 41006, "Program error: Abort due to drive error" },
                { 41007, "Program error: Could not write Y-Parameter" },
                { 41008, "Program error: Timeout exceeded" },
                { 41009, "Program error: Safety active" },
                { 41010, "Program error: Out of target window, force too low" },
                { 41011, "Program error: Out of target window, force exceeded" },
                { 41012, "Program error: Out of target window, invalid force evaluation" },
                { 41013, "Program error: Out of target window, position too low" },
                { 41014, "Program error: Out of target window, force and position too low" },
                { 41015, "Program error: Out of target window, force exceeded, position too low" },
                {
                    41016,
                    "Program error: Out of target window, invalid force evaluation, position too low"
                },
                { 41017, "Program error: Out of target window, position exceeded" },
                { 41018, "Program error: Out of target window, force too low, position exceeded" },
                { 41019, "Program error: Out of target window, force and position exceeded" },
                {
                    41020,
                    "Program error: Out of target window, invalid force evaluation, position exceeded"
                },
                { 41021, "Program error: Out of target window, invalid position evaluation" },
                {
                    41022,
                    "Program error: Out of target window, force too low, invalid position evaluation"
                },
                {
                    41023,
                    "Program error: Out of target window, force exceeded, invalid position evaluation"
                },
                {
                    41024,
                    "Program error: Out of target window, invalid force and position evaluation"
                },
                { 41101, "S- or P-parameter could not be read" },
                { 41201, "S- or P-parameter could not be written" },
                { 41301, "Could not load Y-Parameter file" },
                { 41401, "Tare not possible" },
                { 41501, "Basic parameter loading not possible at drive commissioning" },
                { 41601, "Absolute dimension could not be set" },
                { 41701, "Could not read Y-Parameter" },
                { 41801, "Could not write Y-Parameter" },
                { 50001, "Character string too long, error while writing" },
            };
        }
    }
}
