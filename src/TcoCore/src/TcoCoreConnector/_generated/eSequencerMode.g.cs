using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
	
///		<summary>
///			Modes of the sequencer.
///		</summary>				

	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("", "eSequencerMode", "TcoCore", TypeComplexityEnum.Enumerator)]
	public enum eSequencerMode
	{
		
///		<summary>
///			If invalid, no logic is executed.
///		</summary>				

		Invalid = -10,
		
///		<summary>
///			If cyclic mode is selected, next step is automatically started after the previous has been finished.
///		</summary>				

		CyclicMode = 0,
		
///		<summary>
///			If step mode is selected, when current step has been finished next step has to be started by calling method <c>StepIn()</c>.
///			<para>
///				See <see cref="TcoSequencer.PlcTcoSequencer.StepIn()"/> for detailed description.
///			</para>
///		</summary>				

		StepMode = 10
	}
}