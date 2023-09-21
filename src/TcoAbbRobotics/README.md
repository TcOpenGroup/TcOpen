# TcoAbbRobotics

## Introduction

The **TcoAbbRobotics** is a set of libraries that cover two product platforms in ABB's manufacturing portfolio: the **IRC5** and the **Omnicore** platform." for the target PLC platform [Twincat](https://www.beckhoff.com/en-en/products/automation/twincat/twincat-3-build-4024/) and [TcOpen](https://github.com/TcOpenGroup/TcOpen#readme) framework.

The package consists of a PLC library providing control logic and its .NET twin counterpart aimed at the visualization part.

## General TcOpen Prerequisites

**Check general prerequisites for TcOpen [here](https://github.com/TcOpenGroup/TcOpen#prerequisites).**

 ## TcoAbbRobotics
### PLC enviroment
--- 
#### **_Preconditions:_** The **`gsdml`** file(s) should be copied into the subfolder ..\Config\Io\EtherCat\ of the TwinCAT3 instalation folder, before opening Visual Studio. The Profinet interface of the slave device is activated. The file depends on manufacturer of drive. Robot settings needs to by done in settings   by RobotStudio sfotware by ABB or directly via robot teach pendant. 
---

#### **_Preconditions:_** The robot software is part of repository. Use **RobotStudio** to open project. And transfer it  to robot.
---

#### Implementation steps.
#### 1. Declare the hardware structures in the Global Variable list (GVL).
```csharp

VAR_GLOBAL
  	Robot1	:	TcoAbbRobotics.TcoIrc5_IO_v_1_x_x;
	Robot2	:	TcoAbbRobotics.TcoOmnicore_IO_v_1_x_x;
END_VAR
```
#### 2. Build the XAE project.

#### 3. Add Profinet master device, set its network adapter and network parameters.

#### 4. Scann for new Profinet devices, or use already prepared `xti` files and  use `Add Existing`. This files are localized in `.\src\TcoAbbRobotics\src\TcoAbbRoboticsConnector\ddf\`. `Robot1.xti` is valid for Irc5 and `Robot2.xti` for `Omnicore` 

#### 5. Connect your Gvl structures with  hardware. Refers to bechokff drives documentation if there are some issues, or for guidance how to mapping. 

#### 6. Create the Function Block that extends the **`TcoCore.TcoContext`** function block.

#### 7. Inside the declaration part of the function block created, add an instance of the **`TcoAbbRobotics.TcoIrc5_v_1_x_x`** , **`TcoAbbRobotics.TcoSingleAxis`** or **`TcoAbbRobotics.TcoOmnicore_v_1_x_x`**. Add the **`Main`** method into this function block  and insert the instances. Call with passing the mapped hardware structure

```csharp
FUNCTION_BLOCK WpfContext EXTENDS TcoCore.TcoContext
VAR
     {attribute addProperty Name "<#Abb IRC 5#>"}
    _robot1 : TcoAbbRobotics.TcoIrc5_v_1_x_x(THIS^);
	{attribute addProperty Name "<#Abb Omnicore#>"}
    _robot2 : TcoAbbRobotics.TcoOmnicore_v_1_x_x(THIS^);


END_VAR

```

#### 8. Here  instances are called with passing the mapped hardware structure. By calling method `Service()` , all control elements of this component are accessible later in the visualization. By calling method `Service` is allowed control elements(components) via visualisation (service/ manual mode)

```csharp
/IF _serviceModeActive THEN
   	_robot1.Service();
  	_robot2.Service();
 


END_IF
_robot1(
    inoData := GVL.Robot1);


_robot2(
    inoData := GVL.Robot2);
```

#### 9. In the declaration part of the **`MAIN(PRG)`** create an instance of the function block created in the step 7 according to the example. 
```csharp
PROGRAM MAIN
VAR
      _wpfContext : WpfContext;
END_VAR
```

#### 10. Into the body of the **`MAIN(PRG)`** add the call of the **`Run()`** method of the instance created in the previous step, according to the example.    

```csharp
_wpfContext.Run();
```

#### 14. Build and save the XAE project.

#### 15. Activate configuration, load the PLC program and swith the PLC into the run mode.

-------------------------------

- ### .NET enviroment

--- 

#### **_Preconditions:_** All neccessary packages are installed, all neccessary references are set, connector to the target PLC is set.
- #### Implementation steps.

#### 1. Run the **`Vortex Builder`**.

#### 2. Into the **`MainWindow.xaml`** define **`DataContext`** to the **`MAIN`** of the **`TcoAbbRoboticsTestsPlc`**.
```xml
<Window x:Class="TcoAbbRobotics.Wpf.Sandbox.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:tcopen="clr-namespace:TcoAbbRoboticsTests;assembly=TcoAbbRoboticsTestsConnector"
        xmlns:vortex="http://vortex.mts/xaml" 
        Title="MainWindow"
        Width="800"
        Height="450"
        DataContext="{x:Static tcopen:Entry.TcoAbbRoboticsTestsPlc}"
        mc:Ignorable="d">


</Window>
```

```csharp
       static string TargetAmsId = Environment.GetEnvironmentVariable("Tc3Target");
        static int TargetAmsPort = 852;

        public static TcoAbbRoboticsTestsTwinController TcoAbbRoboticsTestsPlc { get; }
            = new TcoAbbRoboticsTestsTwinController(Vortex.Adapters.Connector.Tc3.Adapter.Tc3ConnectorAdapter.Create(TargetAmsId, TargetAmsPort, true));
```

#### 3. Into the container added, insert the **`RenderableContentControl`** and bind its **`DataContext`** to the **`   _robot1 : TcoAbbRobotics.TcoIrc5_v_1_x_x(THIS^)`** or **` _robot2 : TcoAbbRobotics.TcoOmnicore_v_1_x_x(THIS^)`**, using the **`PresentationType`** of the value **`Service`**.
```XML
    <vortex:RenderableContentControl Grid.Row="0" DataContext="{Binding MAIN._wpfContext._robot1}" PresentationType="Service"/>
	<vortex:RenderableContentControl Grid.Row="0" DataContext="{Binding MAIN._wpfContext._robot2}" PresentationType="Service"/>
```

```
#### 4. After starting the application and expanding the view, final view should look as follows:
```


Collapsed Service view 

![](assets/robotCollapsed.png)



Expanded (detailed info) view

![](assets/robotExpanded.png)

Service view report an error notification

![](assets/robotEstopActive.png)




### Plc Example usage in sequencer

```csharp
seq REF= _sequence1;

seq.Open();
seq.Observer := _observer;




 
		
IF (seq.Step(inStepID := 0,
    inEnabled := TRUE,
    inStepDescription := 'READY TO START')) THEN
    //--------------------------------------------------------
	_param.GlobalSpeed :=100;
		_noOfAttmets:=0;

	answer := _dialog			
			.Show()	
			.WithType(eDialogType.Question)				
			.WithCaption('Ready to go?')
			.WithText('Do you really want to start movements? Do we go ahead?')			
			.WithYesNo().Answer;
			//
			
	 IF (answer = TcoCore.eDialogAnswer.Yes) THEN    	 	
		seq.CompleteStep();
	 ELSIF(answer = TcoCore.eDialogAnswer.No) THEN
		_sequence1Task.Restore();
	 END_IF;	

    //--------------------------------------------------------			
END_IF;


IF (seq.Step(inStepID := 10,
    inEnabled := TRUE,
    inStepDescription := 'RESTORE')) THEN
//--------------------------------------------------------   
	
	_robot1.Restore();
	seq.CompleteStep();
	
		
//--------------------------------------------------------			
END_IF;
IF (seq.Step(inStepID := 11,
    inEnabled := TRUE,
    inStepDescription := 'STOP MOVEMENTS AND PROGRAM')) THEN
//--------------------------------------------------------   
	
	 	IF _robot1.StopMovementsAndProgram(inStopType:=eTcoRoboticsStopType.Soft).Done  THEN
			seq.CompleteStep();
		END_IF
		
//--------------------------------------------------------			
END_IF;

IF (seq.Step(inStepID := 12,
    inEnabled := TRUE,
    inStepDescription := 'START AT MAIN')) THEN
//--------------------------------------------------------   
	
	 	IF _robot1.StartAtMain().Done  THEN
			seq.CompleteStep();
		END_IF;
		
//--------------------------------------------------------			
END_IF;

IF (seq.Step(inStepID := 13,
    inEnabled := TRUE,
    inStepDescription := 'START MOTORS AND PROGRAM')) THEN
//--------------------------------------------------------   
	
	 	IF _robot1.StartMotorsAndProgram().Done  THEN
			seq.CompleteStep();
		END_IF;
		
//--------------------------------------------------------			
END_IF;

IF (seq.Step(inStepID := 14,
    inEnabled := TRUE,
    inStepDescription := 'START MOVEMENTS')) THEN
//--------------------------------------------------------   
	_maxAllowedAttempts:=10;
	
	
	seq.CompleteStep();
	
	
//--------------------------------------------------------			
END_IF;

IF (seq.Step(inStepID := 15,
    inEnabled := TRUE,
    inStepDescription := 'START MOVEMENTS')) THEN
//--------------------------------------------------------   
	_param.ActionNo:=100;
	
	IF _robot1.StartMovements(inData:=_param).Done   THEN
		seq.CompleteStep();
		_robot1.Restore();
	END_IF
	
//--------------------------------------------------------			
END_IF;


IF (seq.Step(inStepID := 16,
    inEnabled := TRUE,
    inStepDescription := 'START ANOTHER MOVE MOVEMENTS')) THEN
//--------------------------------------------------------   
	_param.ActionNo:=1;
	_param.GlobalSpeed:=90;
	IF _robot1.StartMovements(inData:=_param).Done  THEN
		IF _noOfAttmets<_maxAllowedAttempts THEN
			_noOfAttmets:=_noOfAttmets+1;
			seq.RequestStep(14);
			_robot1.Restore();
		ELSE 
		seq.CompleteStep();
		end_if;
	END_IF
	
//--------------------------------------------------------			
END_IF;















//
IF (seq.Step(inStepID := 500,
    inEnabled := TRUE,
    inStepDescription := 'EXAMPLE OF MOVMENT ABORTED')) THEN
//    --------------------------------------------------------

	answer := _dialog			
			.Show()	
			.WithType(eDialogType.Question)				
			.WithCaption('Abort movement?')
			.WithText('Do you  want to start movements and abort it?')			
			.WithYesNo().Answer;
			
			
	 IF (answer = TcoCore.eDialogAnswer.Yes) THEN    	 	
		seq.CompleteStep();
	 ELSIF(answer = TcoCore.eDialogAnswer.No) THEN
		_sequence1Task.Restore();
	 END_IF;	

//    --------------------------------------------------------			
END_IF;

IF (seq.Step(inStepID := 525,
    inEnabled := TRUE,
    inStepDescription := 'STOP MOVEMENT  EXAMPLE')) THEN
//    --------------------------------------------------------    
		

		answer := _dialog			
			.Show()	
			.WithType(eDialogType.Question)				
			.WithCaption('Movement running')
			.WithText('Do you  want to abort movement?')			
			.WithYesNo().Answer;
			
			
		 IF (answer = TcoCore.eDialogAnswer.Yes) THEN 
			_robot1.StopMovements(inStopType:=TcoAbstractions.eTcoRoboticsStopType.Quick);   	 	
			seq.CompleteStep();
		 ELSIF(answer = TcoCore.eDialogAnswer.No) THEN
			_sequence1Task.Restore();
		 END_IF;	
		
	_param.ActionNo:=100;	
	_param.GlobalSpeed :=20;
	IF _robot1.StartMovements(inData:=_param).Done  THEN
		seq.CompleteStep();
	END_IF
	
		
		
//    --------------------------------------------------------			
END_IF;

IF (seq.Step(inStepID := 600,
    inEnabled := TRUE,
    inStepDescription := 'ASKING FOR RESTORING')) THEN
//    --------------------------------------------------------

	answer := _dialog			
			.Show()	
			.WithType(eDialogType.Question)				
			.WithCaption('Question?')
			.WithText('Do you want to restore TcoIrc5 component?')			
			.WithYesNo().Answer;
			
			
	 IF (answer = TcoCore.eDialogAnswer.Yes) THEN    	 	
		_robot1.Restore();
		seq.CompleteStep();
	 ELSIF(answer = TcoCore.eDialogAnswer.No) THEN
		_sequence1Task.Restore();
	 END_IF;	
//    --------------------------------------------------------			
END_IF;

IF (seq.Step(inStepID := seq.RESTORE_STEP_ID,
    inEnabled := TRUE,
    inStepDescription := 'RETURN TO THE START OF THE SEQUENCE')) THEN
    //--------------------------------------------------------	
    	seq.CompleteSequence();
    //--------------------------------------------------------			
END_IF;

seq.Close();