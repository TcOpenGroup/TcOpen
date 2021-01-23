using System;
using Vortex.Connector;
using Vortex.Connector.Peripheries;
using Vortex.Connector.Attributes;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.Identity;

namespace TcoCore
{
	
///		<summary>
///			Enumerator of message categories.
///		</summary>

	[Vortex.Connector.Attributes.TypeMetaDescriptorAttribute("", "eMessageCategory", "TcoCore", TypeComplexityEnum.Enumerator)]
	public enum eMessageCategory
	{
		None = 0,
		
///		<summary>
///			Debug message to be used for debugging purpose only.
///		</summary>

		Debug = 100,
		
///		<summary>
///			Trace message to be used for tracing purpose.
///		</summary>

		Trace = 200,
		
///		<summary>
///			Info message
///		</summary>

		Info = 300,
		
///		<summary>
///			Time-out message.
///		</summary>

		TimedOut = 400,
		
///		<summary>
///			Notification message.
///		</summary>

		Notification = 500,
		
///		<summary>
///			Warning message.
///		</summary>

		Warning = 600,
		
///		<summary>
///			Error message.
///		</summary>

		Error = 700,
		
///		<summary>
///			To be used when wrong setting have been provided by the user.
///		</summary>

		WrongSettings = 800,
		
///		<summary>
///			Programming issue. 
///		</summary>

		ProgrammingError = 900,
		
///		<summary>
///			Critical error. 
///		</summary>

		Critical = 1000,
		
///		<summary>
///			Catastrophic error. 
///		</summary>

		Catastrophic = 1100
	}
}