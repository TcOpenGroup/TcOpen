using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("", "eSequencerError", "TcoCore", TypeComplexityEnum.Enumerator)]
	public enum eSequencerError
	{
		noError = 0,
		NotUniqueStepId = 10,
		StepIdHasBeenChanged = 20,
		OrderOfTheStepHasBeenChanged = 40,
		StepWithRequestedIdDoesNotExists = 50,
		SeveralRequestStep = 60
	}
}