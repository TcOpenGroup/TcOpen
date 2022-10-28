# TcoInspectors

[API](~/api/TcoInspectors/PlcDocu.TcoInspectors.TcoInspector.yml)

Inspectors provide mechanisms for checking the different types of data. Inspector compares input data with the required value. In addition to a simple comparison of values, the inspector provides stabilization timing and timeout for failed inspection. Inspectors can integrate with coordination primitives like `TcoSequencer` that offer extended capabilities in decision flow for failed checks.

## General

[API](~/api/TcoInspectors/PlcDocu.TcoInspectors.TcoInspector.yml)

[Data](~/api/TcoInspectors/PlcDocu.TcoInspectors.TcoInspectorData.yml)

Basic settings

<img width="1213" alt="image" src="https://user-images.githubusercontent.com/61538034/156206482-d4ac4536-f10e-453a-9cf5-726cbc5ed570.png">

Common inspector details
<img width="1202" alt="image" src="https://user-images.githubusercontent.com/61538034/156206656-ff1bacd3-b249-4ecb-91d6-8429f97510cb.png">

## Inspector types

### TcoDigitalInspector

[API](~/api/TcoInspectors/PlcDocu.TcoInspectors.TcoDigitalInspector.yml)

[Data](~/api/TcoInspectors/PlcDocu.TcoInspectors.TcoDigitalInspectorData.yml)

Inspector provides evaluation of `discrete` value. The input value compares against the `Required` value. The inspection passes when the input value matches the required value without interruption for the duration of stabilization time.


### TcoAnalogueInspector

[API](~/api/TcoInspectors/PlcDocu.TcoInspectors.TcoAnalogueInspector.yml)

[Data](~/api/TcoInspectors/PlcDocu.TcoInspectors.TcoAnalogueInspectorData.yml)

Provides evaluation of `continuous` values. The inspector checks that the input value falls within the limit of `Min` and `Max`. The inspection passes when the input value is within the required limit without interruption for the duration of stabilization time.

### TcoDataInspector

[API](~/api/TcoInspectors/PlcDocu.TcoInspectors.TcoDataInspector.yml)

[Data](~/api/TcoInspectors/PlcDocu.TcoInspectors.TcoDataInspectorData.yml)


Provides evaluation of `alphanumerical` values. The input value compares against the `Required` value. The inspection passes when the input value matches the required value without interruption for the duration of stabilization time. In addition to exact comparison, data inspector allows for simple pattern matching where `#` = any number and `*` = any character.

## Preserving overall result

Overall result of a series of inspections can be preserved in [`TcoComprehensiveResult`](~/api/TcoInspectors/PlcDocu.TcoInspectors.TcoComprehensiveResult.yml). Each inspector has `UpdateComprehensiveResult` method that provides the update function.
Once `UpdateComprehensiveResult` marks the overall result as `Failed`, successive inspection will not overwrite the result.


## Handling failure 

When an inspector fails, `OnFail()` provides a series of methods for making decisions about the process.
In order for this is feature to work the inspector needs to be aware of the coordinator of `ITcoCoordinator` (e.g. sequencer).
The coordinator must be passed to the inspector by `WithCoordinator(coordinator)` method.

| Method               | Description                                                                                                                                                                                                                                                                                                                                           |
|----------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Dialog(`RetryState`) | Opens dialog for the user to take a decision. Retry state parameter tells from which state the inspection should start again.                                                                                                                                                                                                                            |
| Retry(`RetryState`)  | Retries the inspector. Retry state parameter tells from which state the inspection should start again. When the parameter `Retries` is set (non zero), the inspection will run only a given number of times; if the inspection does not succeeds the it will continue with the failed result. `0` value of retries indicates no limit to the number of tests. |
| Override()           | Marks the inspection as failed but continues with the following states of the coordinator.                                                                                                                                                                                                                                                            |
| Terminate()          | Marks the inspection as failed and aborts the execution of the coordinator. (recovery step or returns to the beginning of the sequence)                                                                                                                                                                                                               |


### Single inspector with `Dialog` in case of failure

<img width="622" alt="image" src="https://user-images.githubusercontent.com/61538034/156205362-9e21f32f-5d3a-4790-9f28-b9ecd3fb346c.png">

~~~
IF (seq.Step(inStepID := 100,
    inEnabled := TRUE,
    inStepDescription := 'INSPECT DMC CODE')) THEN
    //--------------------------------------------------------
    Data.DmcInspector.WithCoordinator(seq)              // Coordinator                     
        .Inspect(Components.DmcReader)                  // Inspection input
        .UpdateComprehensiveResult(Data.OverallResult)  // Updated overall result
        .OnFail()                                       // Selector what should happen when inspection fails
        .Dialog(50);                                    // On failure dialogue is displayed for the operator to take decision    
    //--------------------------------------------------------          
END_IF;
~~~

## Single inspector with `CarryOn` in case of failure

~~~
IF (seq.Step(inStepID := 100,
    inEnabled := TRUE,
    inStepDescription := 'INSPECT DMC CODE')) THEN
    //--------------------------------------------------------
    Data.DmcInspector.WithCoordinator(seq)              // Coordinator                     
        .Inspect(Components.DmcReader)                  // Inspection input
        .UpdateComprehensiveResult(Data.OverallResult)  // Updated overall result
        .OnFail()                                       // Selector what should happen when inspection fails
        .CarryOn();                                   // On failure sets overall result to fails but continues with in the sequence
    //--------------------------------------------------------          
END_IF;
~~~

## Single inspector with `Terminate` in case of failure

~~~
IF (seq.Step(inStepID := 100,
    inEnabled := TRUE,
    inStepDescription := 'INSPECT DMC CODE')) THEN
    //--------------------------------------------------------
    Data.DmcInspector.WithCoordinator(seq)              // Coordinator                     
        .Inspect(Components.DmcReader)                  // Inspection input
        .UpdateComprehensiveResult(Data.OverallResult)  // Updated overall result
        .OnFail()                                       // Selector what should happen when inspection fails
        .Terminate();                                   // On failure sets overall result to fails and terminates the sequence
    //--------------------------------------------------------          
END_IF;
~~~

## Single inspector with `Retry` in case of failure

~~~
IF (seq.Step(inStepID := 100,
    inEnabled := TRUE,
    inStepDescription := 'INSPECT DMC CODE')) THEN
    //--------------------------------------------------------
    Data.DmcInspector.WithCoordinator(seq)              // Coordinator                     
        .Inspect(Components.DmcReader)                  // Inspection input
        .UpdateComprehensiveResult(Data.OverallResult)  // Updated overall result
        .OnFail()                                       // Selector what should happen when inspection fails
        .Retry(50);                                     // On failure retries the check until succeds.
    //--------------------------------------------------------          
END_IF;
~~~

## Evaluating multiple inspectors at once

### Multiple inspectors with `Dialog` in case of failure

<img width="636" alt="image" src="https://user-images.githubusercontent.com/61538034/156204670-a84aa7bb-f144-4b2c-a0cb-f9933f3468b1.png">

~~~
VAR
    // Helper for evaluation of multiple inspectors
    Eval : TcoInspectors.TcoInspectionGroup(THIS^);
END_VAR
//---------------------------------------------------------

IF MS.Step(Uid := 260, 
           Enabled := TRUE, 
           HmiMessage := '<#KONTROLA SPRAVNEHO PRIPRAVKU#>') THEN
//--------------------------------------------------------------        
    Eval.WithCoordinator(MS)                                                // Tells which coordinator (sequencer) is used
        .Act()                                                              // Make inspections available
        .Inspect(ProcessData.Data.Cu1.LogicCheck.Inspect(FALSE))            // Inspectors 
        .Inspect(ProcessData.Data.Cu1.LogicCheck1.Inspect(FALSE))
        .Inspect(ProcessData.Data.Cu1.LogicCheck1.Inspect(FALSE))
        .UpdateComprehensiveResult(ProcessData.Data.EntityHeader.Results)   // Update global result
        .OnFail()                                                           // Selection of action in case of failure
        .Dialog(260);                                                       // Shows dialogue
//--------------------------------------------------------------    
END_IF
~~~



### Multiple inspectors with `CarryOn` in case of failure
~~~
VAR
    // Helper for evaluation of multiple inspectors
    Eval : TcoInspectors.TcoInspectionGroup(THIS^);
END_VAR
//---------------------------------------------------------

IF MS.Step(Uid := 260, 
           Enabled := TRUE, 
           HmiMessage := '<#KONTROLA SPRAVNEHO PRIPRAVKU#>') THEN
//--------------------------------------------------------------        
    Eval.WithCoordinator(MS)                                                // Tells which coordinator (sequencer) is used
        .Act()                                                              // Make inspections available
        .Inspect(ProcessData.Data.Cu1.LogicCheck.Inspect(FALSE))            // Inspectors 
        .Inspect(ProcessData.Data.Cu1.LogicCheck1.Inspect(FALSE))
        .Inspect(ProcessData.Data.Cu1.LogicCheck1.Inspect(FALSE))
        .UpdateComprehensiveResult(ProcessData.Data.EntityHeader.Results)   // Update global result
        .OnFail()                                                           // Selection of action in case of failure
        .CarryOn();                                                         // Marks as failed but continues go ahead
//--------------------------------------------------------------    
END_IF
~~~

### Multiple inspectors with `Terminate` in case of failure
~~~
VAR
    // Helper for evaluation of multiple inspectors
    Eval : TcoInspectors.TcoInspectionGroup(THIS^);
END_VAR
//---------------------------------------------------------

IF MS.Step(Uid := 260, 
           Enabled := TRUE, 
           HmiMessage := '<#KONTROLA SPRAVNEHO PRIPRAVKU#>') THEN
//--------------------------------------------------------------        
    Eval.WithCoordinator(MS)                                                // Tells which coordinator (sequencer) is used
        .Act()                                                              // Make inspections available
        .Inspect(ProcessData.Data.Cu1.LogicCheck.Inspect(FALSE))            // Inspectors 
        .Inspect(ProcessData.Data.Cu1.LogicCheck1.Inspect(FALSE))
        .Inspect(ProcessData.Data.Cu1.LogicCheck1.Inspect(FALSE))
        .UpdateComprehensiveResult(ProcessData.Data.EntityHeader.Results)   // Update global result
        .OnFail()                                                           // Selection of action in case of failure
        .Terminate();                                                       // Marks as failed and abandons the sequence
//--------------------------------------------------------------    
END_IF
~~~

### Multiple inspectors with `Retry` in case of failure
~~~
VAR
    // Helper for evaluation of multiple inspectors
    Eval : TcoInspectors.TcoInspectionGroup(THIS^);
END_VAR
//---------------------------------------------------------

IF MS.Step(Uid := 260, 
           Enabled := TRUE, 
           HmiMessage := '<#KONTROLA SPRAVNEHO PRIPRAVKU#>') THEN
//--------------------------------------------------------------        
    Eval.WithCoordinator(MS)                                                // Tells which coordinator (sequencer) is used
        .Act()                                                              // Make inspections available
        .Inspect(ProcessData.Data.Cu1.LogicCheck.Inspect(FALSE))            // Inspectors 
        .Inspect(ProcessData.Data.Cu1.LogicCheck1.Inspect(FALSE))
        .Inspect(ProcessData.Data.Cu1.LogicCheck1.Inspect(FALSE))
        .UpdateComprehensiveResult(ProcessData.Data.EntityHeader.Results)   // Update global result
        .OnFail()                                                           // Selection of action in case of failure
        .Retry(200);                                                        // Tries again the inspection from the given step UID
//--------------------------------------------------------------    
END_IF
~~~