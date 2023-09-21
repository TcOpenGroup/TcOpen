using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Vortex.Connector;

namespace TcoAimTtiPowerSupply
{
    public partial class TcoQl355P_v_1_x_x

    {

        public void Initialize()
        {
            _getCommandTask.InitializeExclusively(InvokeGetCommand);
            _setCommandTask.InitializeExclusively(InvokeSetCommand);

        }

        private void InvokeSetCommand()
        {
            this.Read();

            try
            {

                using (TcpClient client = new TcpClient(_config.IpAddress.LastValue, _config.Port.LastValue))
                {
                    client.SendTimeout = _config.Timeout.LastValue;
                    client.ReceiveTimeout = _config.Timeout.LastValue;

                    using (NetworkStream stream = client.GetStream())
                    using (StreamWriter writer = new StreamWriter(stream))
                    using (StreamReader reader = new StreamReader(stream))
                    {

                        string nVal = null;
                        // Send a string to the server
                        var cmd = QlSeriesCommands.Commands.Where(p => p.Type == eQlCommandType.SetCommand).Where(q => q.Key == Enum.GetName(typeof(eTcoQWlSeriesSupplySetCommands_v_1_x_x), _setCommandTask._command.Command.LastValue)).FirstOrDefault();
                        var message = cmd.Syntax.Replace("<N>", _setCommandTask._command.NValue.LastValue.ToString()).Replace("<NRF>", _setCommandTask._command.NrfValue.LastValue.ToString(" 00.00", System.Globalization.CultureInfo.InvariantCulture));
                       
                        writer.WriteLine(message);
                        writer.Flush(); // Flush the buffer to ensure data is sent immediately

                        //Console.WriteLine("Sent: " + message);

                        //// Receive a response from the server
                        //var response = reader.ReadLine();
                        //_status.State.Resposne.Cyclic = response;
                        //_status.State.Write();
                        //Console.WriteLine("Recieved: " + response);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

      

        private void InvokeGetCommand()
        {
            this.Read();

            try
            {

                using (TcpClient client = new TcpClient(_config.IpAddress.LastValue, _config.Port.LastValue))
                {
                    client.SendTimeout = _config.Timeout.LastValue;
                    client.ReceiveTimeout = _config.Timeout.LastValue;

                    using (NetworkStream stream = client.GetStream())
                    using (StreamWriter writer = new StreamWriter(stream))
                    using (StreamReader reader = new StreamReader(stream))
                    {
                       
                        string nVal = null;
                        // Send a string to the server
                        var cmd = QlSeriesCommands.Commands.Where(p => p.Type == eQlCommandType.GetCommand).Where(q => q.Key == Enum.GetName(typeof(eTcoQWlSeriesSupplyGetCommands_v_1_x_x), _getCommandTask._command.Command.LastValue)).FirstOrDefault();
                        var message = cmd.Syntax.Replace("<N>", _getCommandTask._command.NValue.LastValue.ToString());

                      
                        writer.WriteLine(message);
                        writer.Flush(); // Flush the buffer to ensure data is sent immediately

                        

                        // Receive a response from the server
                        var response = reader.ReadLine();

                        _status.State.Nr1Value.Cyclic = 0;
                        _status.State.Nr2Value.Cyclic = 0;
                        _status.State.NValue.Cyclic = 0;
                        _status.State.ConvertMessage.Cyclic =string.Empty ;
                        _status.State.Resposne.Cyclic = response;

                        if (!string.IsNullOrEmpty(cmd.ResponseSyntax))
                        {
                            
                            if (cmd.ResponseSyntax == "<NR1><RMT>")
                            {
                                short result;
                                if (short.TryParse(response, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                                    _status.State.Nr1Value.Cyclic = result;
                                else
                                    _status.State.ConvertMessage.Cyclic = "Unable to convert " + response;
                            }
              
                            else if (cmd.ResponseSyntax == "<NR2>V<RMT>" || cmd.ResponseSyntax == "<NR2>A<RMT>")
                            {
                                float result;
                                var splitted = response.Split('A', 'V');
                                if (float.TryParse(splitted[0], NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                                    _status.State.Nr2Value.Cyclic = result;
                                else
                                    _status.State.ConvertMessage.Cyclic = "Unable to convert " + splitted[0];
                            }

                            //"V<N> <NR2><RMT>"
                            else 
                            {
                                var splitted = response.Split(' ');
                                short nResult;
                                string nValue = new String(splitted[0].Where(Char.IsDigit).ToArray());
                                if (short.TryParse(nValue, NumberStyles.Any, CultureInfo.InvariantCulture, out nResult))
                                    _status.State.NValue.Cyclic = (byte)nResult;
                                else
                                    _status.State.ConvertMessage.Cyclic = "Unable to convert " + splitted[1];

                                float result;
                                if (float.TryParse(splitted[1], NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                                    _status.State.Nr2Value.Cyclic = result;
                                else
                                    _status.State.ConvertMessage.Cyclic = "Unable to convert " + splitted[1];
                            }
                           

                        }

                        _status.State.Write();
                        

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
    
}