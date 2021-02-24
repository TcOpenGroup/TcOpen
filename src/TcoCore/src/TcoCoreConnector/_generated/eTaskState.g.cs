using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
	
///		<summary>
///			Describes the state of the respective task.
///		</summary>				

	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("", "eTaskState", "TcoCore", TypeComplexityEnum.Enumerator)]
	public enum eTaskState
	{
		
///		<summary>
///			Task is in Idle state, the Execute() method returns FALSE. 
///			Task can be satrted by calling the Invoke() method. 
///		</summary>				

		Idle = 0,
		
///		<summary>
///			Task is in Request state, if Invoke() method has been called and the Execute() method has not been called yet. 
///			The first following call of the method Execute() change the task state to Executing and returns TRUE, until Done or Error state is reached.
///			Invoking the task is possible from Idle state.
///			Moreover it could be possible also from Done state in the following cases:
///			1.) The Invoke() method was not called in that PLC cycle in which the Done state was reached. 
///			2.) 
///		</summary>				

		Request = 10,
		
///		<summary>
///			When the task is in Executing state, Execute() method returns TRUE. Leaving this state is 
///		</summary>				

		Executing = 20,
		Error = 30,
		Done = 40
	}
}