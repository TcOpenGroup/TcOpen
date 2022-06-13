using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoInspectors
{
    public interface IsInspectorData
    {
        ///		<summary>
        ///			Error code.			  	
        ///		</summary>
        Vortex.Connector.ValueTypes.OnlinerString ErrorCode { get; }
        ///		<summary>
        ///			Fail time. Is the maximum test duration (or timeout). If the condition for the successul test is not met within this time, the test fails.			  	
        ///		</summary>
        Vortex.Connector.ValueTypes.OnlinerTime FailTime { get; }
        ///		<summary>
        ///			Verbatim description of the failure.				  	
        ///		</summary>
        Vortex.Connector.ValueTypes.OnlinerString FailureDescription { get; }
        ///		<summary>
        ///			Test is bypassed, no eveluation occurs.  
        ///			Evaluation of excluded test should be handled by user code.		  	
        ///		</summary>
        Vortex.Connector.ValueTypes.OnlinerBool IsByPassed { get; }
        ///		<summary>
        ///			Test is exclusion from evaluation. If TRUE the check is excluded, the test will run and provided detected value. 
        ///			Evaluation of excluded test should be handled by user code.		  	
        ///		</summary>
        Vortex.Connector.ValueTypes.OnlinerBool IsExcluded { get; }
        ///		<summary>
        ///			Counter of allowed inspections.				  	
        ///		</summary>
        Vortex.Connector.ValueTypes.OnlinerUInt NumberOfAllowedRetries { get; }
        ///		<summary>
        ///			Pass time, elapses when the measured value has required value (see `ReuiredStatus`). 
        ///			The signal must have required value continuosly for this time span for the check to pass.  	
        ///		</summary>
        Vortex.Connector.ValueTypes.OnlinerTime PassTime { get; }
        ///		<summary>
        ///			Test result.		  	
        ///		</summary>
        Vortex.Connector.ValueTypes.OnlinerInt Result { get; }
        ///		<summary>
        ///			Counter inspections.				  	
        ///		</summary>
        Vortex.Connector.ValueTypes.OnlinerUInt RetryAttemptsCount { get; }
        ///		<summary>
        ///			Inspection timestamp.	
        ///		</summary>
        Vortex.Connector.ValueTypes.OnlinerDateTime TimeStamp { get; }

        dynamic DetectedStatus { get; }
    }   

    public interface IsInspector
    {
        eInspectorResult ResultAsEnum { get; }
        IsInspectorData InspectorData { get; }
    }
}
