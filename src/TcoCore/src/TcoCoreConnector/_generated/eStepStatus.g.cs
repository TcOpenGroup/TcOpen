using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("", "eStepStatus", "TcoCore", TypeComplexityEnum.Enumerator)]
	public enum eStepStatus
	{
		None = 0,
		Disabled = 10,
		ReadyToRun = 20,
		Running = 30,
		Done = 40,
		Error = 50
	}
}