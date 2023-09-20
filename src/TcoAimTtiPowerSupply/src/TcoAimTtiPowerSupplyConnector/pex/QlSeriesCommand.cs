using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoAimTtiPowerSupply
{
    public class QlSeriesCommand
    {
        public string Key{get;set;}
        public string Command { get; set; }
        public string Syntax{get;set;}

        public string ResponseSyntax { get; set; } = string.Empty;
        public string HelpDescription { get; set; }

        public eQlCommandType Type { get; set; }

    }
    public static class QlSeriesCommands

    {
        public static IEnumerable<QlSeriesCommand> Commands = new List<QlSeriesCommand> ()
        {
            {new QlSeriesCommand(){ Key = "V",Syntax="V<N> <NRF>" ,HelpDescription=@"Set output <N> to <NRF> Volts. For AUX output <N>=3",Type = eQlCommandType.SetCommand} },
            {new QlSeriesCommand(){ Key = "V_Verif",Syntax="V<N>V <NRF> ",HelpDescription=@"Set output <N> to <NRF> Volts with verify. For AUX output <N>=3",Type = eQlCommandType.SetCommand} },
            {new QlSeriesCommand(){ Key = "OVP",Syntax="OVP<N> <NRF> ",HelpDescription=@"Set output <N> over voltage protection trip point to <NRF> Volts",Type = eQlCommandType.SetCommand} },
            {new QlSeriesCommand(){ Key = "I",Syntax="I<N> <NRF> ",HelpDescription=@"Set output <N> current limit to <NRF> Amps",Type = eQlCommandType.SetCommand} },
            {new QlSeriesCommand(){ Key = "OCP",Syntax="OCP<N> <NRF> ",HelpDescription=@"Set output <N> over current protection trip point to <NRF> Amps",Type = eQlCommandType.SetCommand} },
            {new QlSeriesCommand(){ Key = "V",Syntax="V<N>? ",HelpDescription=@"Return the set voltage of output <N> . For AUX output <N>=3 – response is V <N> <NR2><RMT> where <NR2> is in Volts" ,ResponseSyntax="V<N> <NR2><RMT>",Type = eQlCommandType.GetCommand} },
            {new QlSeriesCommand(){ Key = "I",Syntax="I<N>? ",HelpDescription=@"Return the set current limit of output <N> – response is I <N> <NR2><RMT> where <NR2> is in Amps",ResponseSyntax="I<N> <NR2><RMT>",Type = eQlCommandType.GetCommand} },
            {new QlSeriesCommand(){ Key = "OVP",Syntax="OVP<N>? ",HelpDescription=@"Return the voltage trip setting for output <N> – response is VP<N> <NR2><RMT> where <NR2> is in Volts",ResponseSyntax="VP<N> <NR2><RMT>",Type = eQlCommandType.GetCommand} },
            {new QlSeriesCommand(){ Key = "OCP",Syntax="OCP<N>? ",HelpDescription=@"Return the current trip setting for output <N> – response is IP<N> <NR2><RMT> where <NR2> is in Amps",ResponseSyntax="IP<N> <NR2><RMT>",Type = eQlCommandType.GetCommand} },
            {new QlSeriesCommand(){ Key = "VO",Syntax="V<N>O?",HelpDescription=@"Return the output readback voltage for output <N>. For AUX output <N>=3 – response is <NR2>V<RMT> where <NR2> is in Volts",ResponseSyntax="<NR2>V<RMT>",Type = eQlCommandType.GetCommand} },
            {new QlSeriesCommand(){ Key = "IO",Syntax="I<N>O? ",HelpDescription=@"Return the output readback current for output <N>. For AUX output <N>=3 – response is <NR2>A<RMT> where <NR2> is in Amps",ResponseSyntax="<NR2>A<RMT>",Type = eQlCommandType.GetCommand} },
            {new QlSeriesCommand(){ Key = "RANGE",Syntax="RANGE<N> <NRF> ",HelpDescription=@"Set the voltage range of output <N> to <NRF> where <NRF> has the following meaning: QL355 Models: 0=15V(5A), 1=35V(3A), 2=35V(500mA) QL564 Models: 0=25V(4A), 1=56V(2A), 2=56V(500mA)",Type = eQlCommandType.SetCommand} },
            {new QlSeriesCommand(){ Key = "RANGE",Syntax = "RANGE<N>? ",HelpDescription = @"Return the set voltage range of output <N>",Type = eQlCommandType.GetCommand}},
            {new QlSeriesCommand(){ Key = "DELTAV", Syntax = "DELTAV<N><NRF>", HelpDescription = @"Set the output <N> voltage step size to <NRF> Volts. For AUX output <N>=3", Type = eQlCommandType.SetCommand } },
            {new QlSeriesCommand(){ Key = "DELTAI", Syntax = "DELTAI<N> <NRF> ", HelpDescription = @"Set the output <N> current step size to <NRF> Amps", Type = eQlCommandType.SetCommand } },
            {new QlSeriesCommand(){ Key = "DELTAV", Syntax = "DELTAV<N>? ", HelpDescription = @"Return the output <N> voltage step size. For AUX output <N>=3 – response is DELTAV<N> <NR2><RMT> where <NR2> is in Volts.",ResponseSyntax="DELTAV<N> <NR2><RMT>", Type = eQlCommandType.GetCommand } },
            {new QlSeriesCommand(){ Key = "DELTAI", Syntax = "DELTAI<N>? ", HelpDescription = @"Return the output <N> current step size – response is DELTAI<N> <NR2><RMT> where <NR2> is in Amps.",ResponseSyntax="DELTAI<N> <NR2><RMT>", Type = eQlCommandType.GetCommand } },
            {new QlSeriesCommand(){ Key = "INCV", Syntax = "INCV<N> ", HelpDescription = @"Increment the output <N> voltage by the step size set for output <N>.For AUX output <N>=3", Type = eQlCommandType.SetCommand } },
            {new QlSeriesCommand(){ Key = "INCV_Verif", Syntax = "INCV<N>V", HelpDescription = @" Increment with verify the output <N> voltage by the step size set for output <N>.For AUX output <N>=3", Type = eQlCommandType.SetCommand } },
            {new QlSeriesCommand(){ Key = "DECV", Syntax = "DECV<N>", HelpDescription = @" Decrement the output <N> voltage by the step size set for output <N>.For AUX output <N>=3", Type = eQlCommandType.SetCommand } },
            {new QlSeriesCommand(){ Key = "DECV_Verif", Syntax = "DECV<N>V ", HelpDescription = @"Decrement with verify the output <N> voltage by the step size set for output <N>For AUX output <N>=3", Type = eQlCommandType.SetCommand } },
            {new QlSeriesCommand(){ Key = "INCI", Syntax = "INCI<N> ", HelpDescription = @"Increment the output <N> current limit by the step size set for output <N>", Type = eQlCommandType.SetCommand } },
            {new QlSeriesCommand(){ Key = "DECI", Syntax = "DECI<N>", HelpDescription = @"Decrement the output <N> current limit by the step size set for output <N>", Type = eQlCommandType.SetCommand } },
            {new QlSeriesCommand(){ Key = "OP", Syntax = "OP<N> <NRF>", HelpDescription = @"Set output <N> on/off where <NRF> has the following meaning: 0=OFF, 1=ONFor AUX output <N>=3", Type = eQlCommandType.SetCommand } },
            {new QlSeriesCommand(){ Key = "OP", Syntax = "OP<N>?", HelpDescription = @"Returns output <N> on/off status. For AUX output <N>=3 - response is <NR1><RMT> where 1 = ON, 0 = OFF.",ResponseSyntax="<NR1><RMT>", Type = eQlCommandType.GetCommand } },
            {new QlSeriesCommand(){ Key = "OPALL",Syntax="OPALL <NRF> ",HelpDescription=@"Simultaneously sets all outputs on/off where <NRF> has the following meaning: 0 = All OFF, 1 = ALL ON. If OPALL sets all outputs ON then any that were already on will remain ON. If OPALL sets all outputs OFF then any that were already off will remain OFF", Type = eQlCommandType.SetCommand} },
            {new QlSeriesCommand(){ Key = "SENSE", Syntax = "SENSE<N> <NRF> ", HelpDescription = @"Set the output <N> sense mode where <NRF> has the following meaning:0=local, 1=remote", Type = eQlCommandType.SetCommand } },
            {new QlSeriesCommand(){ Key = "MODE", Syntax = "MODE <NRF> ", HelpDescription = @"Set the instrument operating mode to LINK or assign control to output 1 or 2", Type = eQlCommandType.SetCommand } },
            {new QlSeriesCommand(){ Key = "MODE ", Syntax = "MODE? ", HelpDescription = @"<NRF> has the following meaning: – response is LINKED or CTRL<N> (control assigned to output <N>)", Type = eQlCommandType.GetCommand } },
            {new QlSeriesCommand(){ Key = "TRIPRST", Syntax = "TRIPRST ", HelpDescription = @"0 = linked, 1 = assign control to output 1, 2 = assign control to output 2.", Type = eQlCommandType.SetCommand } },
            {new QlSeriesCommand(){ Key = "LSR", Syntax = "LSR<N>? ", HelpDescription = @"Setting linked mode uniquely affects the way the instrument responds to some – response is <NR1><RMT>",ResponseSyntax="<NR1><RMT>", Type = eQlCommandType.GetCommand } },
            {new QlSeriesCommand(){ Key = "LSE", Syntax = "LSE<N> <NRF> ", HelpDescription = @"remote commands. Commands to set Range, Voltage, Current Limit, OVP or", Type = eQlCommandType.SetCommand } },
            {new QlSeriesCommand(){ Key = "LSE", Syntax = "LSE<N>? ", HelpDescription = @"OCP sent to either Output 1 or Output 2 will change the setting on both outputs – response is <NR1><RMT>",ResponseSyntax="<NR1><RMT>", Type = eQlCommandType.GetCommand } },
            {new QlSeriesCommand(){ Key = "SAV",Syntax="SAV<N> <NRF> ",HelpDescription=@"Save the current set-up of output <N> to the set-up store specified by <NRF> where<NRF> can be 0-49 for the main outputs or 0-9 for the AUX output on TP models. For AUX output <N>=3. If the instrument is operating in linked mode then the entire instrument set-up (excluding auxiliary output) will be stored in the linked mode set-up store specified by <NRF>. The <N> specification is ignored. This has no affect on the individual PSU<N> set-up stores available when not in linked mode", Type = eQlCommandType.SetCommand} }, 
            {new QlSeriesCommand(){ Key = "RCL",Syntax="RCL<N> <NRF> ",HelpDescription=@"Recall a set up for output <N> from the set-up store specified by <NRF> where < NRF > can be 0 - 49 for the main outputs or 0 - 9 for the AUX output on TP  models.For AUX output < N >= 3. If the instrument is operating in LINK mode then the entire instrument set - up  (excluding AUX output) will be recalled from the LINK mode set - up store specified by<NRF>.The < N > specification is ignored.", Type = eQlCommandType.SetCommand} },
            {new QlSeriesCommand(){ Key = "RST",Syntax="*RST",HelpDescription=@"Resets the instrument to the factory default settings − (see Factory Defaults section) with the exception of all remote interface settings.",Type = eQlCommandType.SetCommand} },
            {new QlSeriesCommand(){ Key = "QER", Syntax = "QER? ", HelpDescription = @"Query and clear Query Error Register. The response format is nr1<RMT>",ResponseSyntax="<NR1><RMT>", Type = eQlCommandType.GetCommand } },
            {new QlSeriesCommand(){ Key = "CLS",Syntax="*CLS",HelpDescription=@"Clear Status. Clears the Standard Event Status Register, Query Error Register and Execution Error Register. This indirectly clears the Status Byte Register.", Type = eQlCommandType.SetCommand} },
            {new QlSeriesCommand(){ Key = "EER", Syntax = "EER?", HelpDescription = @"Query and clear Execution Error Register. The response format is nr1<RMT>",ResponseSyntax="<NR1><RMT>", Type = eQlCommandType.GetCommand } },
            {new QlSeriesCommand(){ Key = "ESE", Syntax = "*ESE <NRF> ", HelpDescription = @"Set the Standard Event Status Enable Register to the value of <NRF>.", Type = eQlCommandType.SetCommand } },
            {new QlSeriesCommand(){ Key = "ESE",Syntax="*ESE? ",HelpDescription=@"Returns the value in the Standard Event Status Enable Register in <NR1> numeric format.The syntax of the response is <NR1><RMT>",ResponseSyntax="<NR1><RMT>", Type = eQlCommandType.GetCommand} },
            {new QlSeriesCommand(){ Key = "ESR",Syntax="*ESR?",HelpDescription=@"Returns the value in the Standard Event Status Register in <NR1> numeric format.The register is then cleared. The syntax of the response is <NR1><RMT>", ResponseSyntax="<NR1><RMT>",Type = eQlCommandType.GetCommand} },
            {new QlSeriesCommand(){ Key = "IST",Syntax="*IST?",HelpDescription=@"Returns ist local message as defined by IEEE Std. 488.2. The syntax of the response is 0 < RMT >, if the local message is false, or 1 < RMT >, if the local message is true.",ResponseSyntax="<NR1><RMT>",Type = eQlCommandType.GetCommand} },
            {new QlSeriesCommand(){ Key = "OPC",Syntax="*OPC",HelpDescription=@"Sets the Operation Complete bit (bit 0) in the Standard Event Status Register.This will happen immediately the command is executed because of the sequential nature of all operations.", Type = eQlCommandType.SetCommand} },
            {new QlSeriesCommand(){ Key = "OPC",Syntax="*OPC?",HelpDescription=@"Query Operation Complete status. The syntax of the response is 1<RMT>.The response will be available immediately the command is executed because of the sequential nature of all operations.",ResponseSyntax="<NR1><RMT>", Type = eQlCommandType.GetCommand} },
            {new QlSeriesCommand(){ Key = "PRE", Syntax = "*PRE <NRF> ", HelpDescription = @"Set the Parallel Poll Enable Register to the value <NRF>.", Type = eQlCommandType.SetCommand } },
            {new QlSeriesCommand(){ Key = "PRE", Syntax = "*PRE?", HelpDescription = @"Returns the value in the Parallel Poll Enable Register in <NR1> numeric format.The syntax of the response is <NR1><RMT>",ResponseSyntax="<NR1><RMT>", Type = eQlCommandType.GetCommand } },
            {new QlSeriesCommand(){ Key = "SRE", Syntax = "*SRE <NRF> ", HelpDescription = @"Set the Service Request Enable Register to <NRF>.", Type = eQlCommandType.SetCommand } },
            {new QlSeriesCommand(){ Key = "STB",Syntax="*STB? ",HelpDescription=@"Returns the value of the Status Byte Register in <NR1> numeric format. The syntax of the response is<NR1><RMT>",ResponseSyntax="<NR1><RMT>", Type = eQlCommandType.GetCommand} },
            {new QlSeriesCommand(){ Key = "WAI",Syntax="*WAI",HelpDescription=@"Wait for Operation Complete true. As all commands are completely executed before the next is started this command takes no additional action.", Type = eQlCommandType.SetCommand} },
            {new QlSeriesCommand(){ Key = "IFLOCK",Syntax="IFLOCK",HelpDescription=@"Request Instrument ‘lock’. This command requests exclusive access control of the instrument.The response is ‘1’ is successful or ‘-1’ if the lock is unavailable either because it is already in use or the user has disabled this interface from taking control using the web interface",Type = eQlCommandType.SetCommand} },
            {new QlSeriesCommand(){ Key = "SRE",Syntax="*SRE? ",HelpDescription=@"Returns the value of the Service Request Enable Register in <NR1> numeric format.The syntax of the response is<NR1><RMT>",ResponseSyntax="<NR1><RMT>", Type = eQlCommandType.GetCommand} },
            {new QlSeriesCommand(){ Key = "IFLOCK", Syntax = "IFLOCK?", HelpDescription = @"Query the status of the interface ‘lock’. The return value is ‘1’ if the lock is owned by the requesting interface instance; ‘0’ if there is no active lock or ‘-1’ if the lock is unavailable either because it is in use by another interface or the user has disabled the interface from taking control via the web interface.",ResponseSyntax="<NR1><RMT>", Type = eQlCommandType.GetCommand } },
            {new QlSeriesCommand(){ Key = "IFUNLOCK", Syntax = "IFUNLOCK", HelpDescription = @"Release the ‘lock’ if possible. Returns ‘0’ if successful. If this command is unsuccessful ‘-1’ is returned, 200 is placed in the Execution Error Register and bit 4 of the Event Status Register is set indicating that you do not have the authority to release the lock.",ResponseSyntax="<NR1><RMT>", Type = eQlCommandType.SetCommand } },
            {new QlSeriesCommand(){ Key = "LOCAL", Syntax = "LOCAL", HelpDescription = @"Go to local. This does not release any active interface lock so that the lock remains with the selected interface when the next remote command is", Type = eQlCommandType.SetCommand } },
            {new QlSeriesCommand(){ Key = "ADDRESS",Syntax="ADDRESS?",HelpDescription=@"Returns the bus address <NR1><RMT>. This number can be used to identify the unit",ResponseSyntax="<NR1><RMT>", Type = eQlCommandType.GetCommand} },
            {new QlSeriesCommand(){ Key = "IPADDR",Syntax="IPADDR? ",HelpDescription=@"Returns the present IP address of the LAN interface, provided it is connected. If it is not connected, the response will be the static IP if configured to always use that static IP, otherwise it will be 0.0.0.0 if waiting for DHCP or Auto-IP. The response is nnn.nnn.nnn.nnn<RMT>, where each nnn is 0 to 255", Type = eQlCommandType.GetCommand} },
            {new QlSeriesCommand(){ Key = "NETMASK",Syntax="NETMASK?",HelpDescription=@"Returns the present netmask of the LAN interface, provided it is connected. The response is nnn.nnn.nnn.nnn<RMT>, where each nnn is 0 to 255.", Type = eQlCommandType.GetCommand} },
            {new QlSeriesCommand(){ Key = "NETCONFIG",Syntax="NETCONFIG?",HelpDescription=@"Returns the first means by which an IP address will be sought. The response is <CRD><RMT> where <CRD> is DHCP, AUTO or STATIC.", Type = eQlCommandType.GetCommand} },
            {new QlSeriesCommand(){ Key = "NETCONFIG",Syntax="NETCONFIG <CPD>",HelpDescription=@"Specifies the means by which an IP address will be sought.< CPD > must be one of DHCP, AUTO or STATIC.", Type = eQlCommandType.SetCommand} },
            {new QlSeriesCommand(){ Key = "IPADDR",Syntax="IPADDR <quad>",HelpDescription=@"Sets the potential static IP address of the LAN interface (as on the webpage). The parameter must be strictly a dotted quad for the IP address, with each address part an <NR1> in the range 0 to 255, (e.g. 192.168.1.101).", Type = eQlCommandType.SetCommand} },
            {new QlSeriesCommand(){ Key = "NETMASK",Syntax="NETMASK <quad>",HelpDescription=@"Sets the netmask to accompany the static IP address of the LAN interface. The parameter must be strictly a dotted quad for the netmask, with each part an <NR1> in the range 0 to 255, (e.g. 255.255.255.0).", Type = eQlCommandType.SetCommand} },
            {new QlSeriesCommand(){ Key = "IDN",Syntax="*IDN?",HelpDescription=@"Returns the instrument identification. The exact response is determined by the instrument configuration and is of the form <NAME>,<model>, 0, <version><RMT> where <NAME> is the manufacturer's name, <model> defines the type of instrument and <version> is the revision level of the software installed.", Type = eQlCommandType.GetCommand} },
            {new QlSeriesCommand(){ Key = "TST", Syntax = "*TST? ", HelpDescription = @"The PSU has no self test capability and the response is always 0 <RMT>" ,ResponseSyntax="<NR1><RMT>", Type = eQlCommandType.GetCommand } },
            {new QlSeriesCommand(){ Key = "TRG", Syntax = "*TRG ", HelpDescription = @"The PSU has no trigger capability.", Type = eQlCommandType.SetCommand } },


        };


    }
   
}
