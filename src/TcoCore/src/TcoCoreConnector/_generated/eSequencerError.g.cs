using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
	
///		<summary>
///			Describes cause of the sequencer error.
///		</summary>				

	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("", "eSequencerError", "TcoCore", TypeComplexityEnum.Enumerator)]
	public enum eSequencerError
	{
		
///		<summary>
///			No error.
///		</summary>				

		noError = 0,
		
///		<summary>
///			Some of the StepId in the sequence is not unique.
///		</summary>				

		NotUniqueStepId = 10,
		
///		<summary>
///			StepId of the actually executed step has been changed during its execution.
///		</summary>				

		StepIdHasBeenChanged = 20,
		
///		<summary>
///			Order of the actually executed step has been changed during its execution.
///		</summary>				

		OrderOfTheStepHasBeenChanged = 40,
		
///		<summary>
///			Method RequestStep has been called with nonexisting StepId in the sequence.
///		</summary>				

		StepWithRequestedIdDoesNotExists = 50,
		
///		<summary>
///			Method RequestStep has been called while previous RequestStep call has not yet been completed.
///		</summary>				

		SeveralRequestStep = 60
	}
}