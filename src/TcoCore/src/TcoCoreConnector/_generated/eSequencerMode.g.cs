using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("", "eSequencerMode", "TcoCore", TypeComplexityEnum.Enumerator)]
	public enum eSequencerMode
	{
		Invalid = -10,
		CyclicMode = 0,
		StepMode = 10
	}
}