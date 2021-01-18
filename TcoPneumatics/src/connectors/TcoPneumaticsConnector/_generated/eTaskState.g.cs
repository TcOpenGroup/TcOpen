using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoPneumatics
{
	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("", "eTaskState", "TcoPneumatics", TypeComplexityEnum.Enumerator)]
	public enum eTaskState
	{
		Idle = 0,
		Request = 10,
		Executing = 20,
		Error = 30,
		Done = 40
	}
}