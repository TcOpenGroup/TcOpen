using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
	
///		<summary>
///			Describes the step status inside the sequencer.
///		</summary>				

	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("", "eStepStatus", "TcoCore", TypeComplexityEnum.Enumerator)]
	public enum eStepStatus
	{
		
///		<summary>
///			No status defined.
///		</summary>				

		None = 0,
		
///		<summary>
///			Step is disabled and it is not going to be executed.
///			Next enabled step in the sequence will be executed, when sequencer reach that step.
///		</summary>				

		Disabled = 10,
		
///		<summary>
///			Step is ready to run. This case occurs when sequence is in step mode, step is in order of the execution and step is enabled.
///			Code body of the current step is not executing. After calling StepIn method, step becames running and its code body starts to be executing.
///		</summary>				

		ReadyToRun = 20,
		
///		<summary>
///			Step is currently running and his code body is currently executing.
///		</summary>				

		Running = 30,
		
///		<summary>
///			Step has been finished. Its code body is not executing.
///		</summary>				

		Done = 40,
		
///		<summary>
///			Step is in error state. Its code body is not executing.
///		</summary>				

		Error = 50
	}
}