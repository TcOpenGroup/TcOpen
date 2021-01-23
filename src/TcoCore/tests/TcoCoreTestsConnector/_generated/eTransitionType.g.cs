using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCoreTests
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("", "eTransitionType", "TcoCoreTests", TypeComplexityEnum.Enumerator)]
	public enum eTransitionType
	{
		None = 0,
		Linear = 1,
		Exponential = 2,
		S_Type = 3
	}
}