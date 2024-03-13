# Inspectors

### Plc Example usage in sequencer

```csharp
seq REF= _sequence1;

seq.Open();
seq.Observer := _observer;

IF (seq.Step(inStepID := 0,
    inEnabled := TRUE,
    inStepDescription := 'ASKING SOME QUESTION')) THEN
    //--------------------------------------------------------
//<StandardDialog>	
	answer := _dialog			
			.Show()	
			.WithType(eDialogType.Question)				
			.WithCaption('Ready to go?')
			.WithText('We are about to start the inspection sequence. Do we go ahead?')			
			.WithYesNo().Answer;
			//
			
	 IF (answer = TcoCore.eDialogAnswer.Yes) THEN    	 	
		seq.CompleteStep();
	 ELSIF(answer = TcoCore.eDialogAnswer.No) THEN
		_sequence1Task.Restore();
	 END_IF;	
//</StandardDialog>	 
    //--------------------------------------------------------			
END_IF;

IF (seq.Step(inStepID := 25,
    inEnabled := TRUE,
    inStepDescription := 'SETUP DATA')) THEN
    //--------------------------------------------------------    
	 SetupData();
	seq.CompleteStep();
    //--------------------------------------------------------			
END_IF;

IF (seq.Step(inStepID := 50,
    inEnabled := TRUE,
    inStepDescription := 'INIT')) THEN
    //--------------------------------------------------------    	 	       
		seq.CompleteStep();
    //--------------------------------------------------------			
END_IF;

IF (seq.Step(inStepID := 100,
    inEnabled := TRUE,
    inStepDescription := 'INSPECT DMC CODE - WHEN CARRY ON')) THEN
    //--------------------------------------------------------
    Data.DmcInspector.WithCoordinator(seq)
		.Inspect(Components.DmcReader)		
        .UpdateComprehensiveResult(Data.OverallResult)
        .OnFail()
		.CarryOn();
    //--------------------------------------------------------			
END_IF;

//<TcoDigitalInspector>	
IF (seq.Step(inStepID := 200,
    inEnabled := true,
    inStepDescription := 'INSPECT BOLT PRESENCE - ASK WHAT TO DO')) THEN
    //--------------------------------------------------------	
   IF seq.IsFreshState THEN
	    SetupData();
   END_IF
	 Data.BoltPresenceInspector
		.WithCoordinator(seq)
		.Inspect(Components.BoltPresenceSensor)
		.UpdateComprehensiveResult(Data.OverallResult)
		.OnFail()
		.Dialog(seq.CurrentStep.ID+1);
    //--------------------------------------------------------			
END_IF;

IF (seq.Step(inStepID := 201,
    inEnabled := true,
    inStepDescription := 'INSPECT BOLT PRESENCE - ASK WHAT TO DO WITH INSTRUCTION')) THEN
    //--------------------------------------------------------	
   IF seq.IsFreshState THEN
	    SetupData();
   END_IF
    Data.BoltPresenceInspector
		.WithCoordinator(seq)
		.Inspect(Components.BoltPresenceSensor)
		.UpdateComprehensiveResult(Data.OverallResult)
		.Instruct('Something is wrong')
        .OnFail()
		.Dialog(seq.CurrentStep.ID+1);
    //--------------------------------------------------------			
END_IF;

IF (seq.Step(inStepID := 202,
    inEnabled := true,
    inStepDescription := 'INSPECT BOLT PRESENCE - ASK WHAT TO DO WITH INSTRUCTION AND PICTURE')) THEN
    //--------------------------------------------------------	
	IF seq.IsFreshState THEN
	    SetupData();
   	END_IF
    Data.BoltPresenceInspector
		.WithCoordinator(seq)
		.Inspect(Components.BoltPresenceSensor)
		.UpdateComprehensiveResult(Data.OverallResult)
		.Instruct('Something is wrong')
		.InstructWithImage('d:\MTS\Develop\TcOpenGroup\TcOpen\assets\logo\TcOpenWide.png',200,200)
        .OnFail()
		.Dialog(seq.CurrentStep.ID+1);
    //--------------------------------------------------------			
END_IF;
IF (seq.Step(inStepID := 203,
    inEnabled := true,
    inStepDescription := 'INSPECT BOLT PRESENCE - ASK WHAT TO DO WITH INSTRUCTION AND BIGGER PICTURE')) THEN
    //--------------------------------------------------------	
	IF seq.IsFreshState THEN
	    SetupData();
   	END_IF 
	Data.BoltPresenceInspector
		.WithCoordinator(seq)
		.Inspect(Components.BoltPresenceSensor)
		.UpdateComprehensiveResult(Data.OverallResult)
		.Instruct('Something is wrong')		//provide cuastomized instruction instead of automaticaly generated
		.InstructWithImage('d:\MTS\Develop\TcOpenGroup\TcOpen\assets\logo\TcOpenWide.png',1200,1200)
        .OnFail()
		.Dialog(seq.CurrentStep.ID+1);
    //--------------------------------------------------------			
END_IF;
IF (seq.Step(inStepID := 204,
    inEnabled := true,
    inStepDescription := 'INSPECT BOLT PRESENCE - ASK WHAT TO DO WITH FAILURE DESCRIPTION')) THEN
    //--------------------------------------------------------	
	IF seq.IsFreshState THEN
	    SetupData();
   	END_IF    

	 Data.BoltPresenceInspector
		.WithCoordinator(seq)
		.Inspect(Components.BoltPresenceSensor)
		.UpdateComprehensiveResult(Data.OverallResult)
		.WithFailureDescription()
		.OnFail()
		.Dialog(seq.CurrentStep.ID+1);
    //--------------------------------------------------------			
END_IF;

IF (seq.Step(inStepID := 205,
    inEnabled := true,
    inStepDescription := 'INSPECT BOLT PRESENCE - ASK WHAT TO DO WITH DESCRIPTION')) THEN
    //--------------------------------------------------------	
	IF seq.IsFreshState THEN
	    SetupData();
   	END_IF    
	 Data.BoltPresenceInspector
		.WithCoordinator(seq)
		.Inspect(Components.BoltPresenceSensor)
		.UpdateComprehensiveResult(Data.OverallResult)
		.WithDescription()	//provide description of Inspector in this case will be Bolt presence inspection because...	{attribute addProperty Name "Bolt presence inspection"}
		.OnFail()
		.Dialog(seq.CurrentStep.ID+1);
    //--------------------------------------------------------			
END_IF;

IF (seq.Step(inStepID := 206,
    inEnabled := true,
    inStepDescription := 'INSPECT BOLT PRESENCE - ASK WHAT TO DO WITH FAILURE DESCRIPTION AND ERROR CODE(IF DEFINED)')) THEN
    //--------------------------------------------------------	
   	IF seq.IsFreshState THEN
	    SetupData();
   	END_IF    
	 Data.BoltPresenceInspector
		.WithCoordinator(seq)
		.Inspect(Components.BoltPresenceSensor)
		.UpdateComprehensiveResult(Data.OverallResult)
		.WithErrorCode()
		.WithFailureDescription()
		.OnFail()
		.Dialog(250);
    //--------------------------------------------------------			
END_IF;



IF (seq.Step(inStepID := 250,
    inEnabled := TRUE,
    inStepDescription := 'INSPECT BOLT PRESENCE - ASK WHAT TO DO WITH EXTERNAL CLOSE')) THEN
    //--------------------------------------------------------	
	IF seq.IsFreshState THEN
	    SetupData();
   	END_IF    
	Data.BoltPresenceInspector
		.WithCoordinator(seq)
		.Inspect(Components.BoltPresenceSensor)
		.UpdateComprehensiveResult(Data.OverallResult)	
        .OnFail()
        .DialogWithExternalClose (250,inRetrySignal:=testRetry,inTerminateSignal:=testTerminate);
    //--------------------------------------------------------			
END_IF;





IF (seq.Step(inStepID := 300,
    inEnabled := TRUE,
    inStepDescription := 'INSPECT DIMENSIONS - WITH NORMALIZE RETRIES (lower `number of allowed retries` for this call of inspection group)')) THEN
    //--------------------------------------------------------
   	IF seq.IsFreshState THEN
	    SetupData();
   	END_IF    
	 GroupInspection
		.WithCoordinator(seq)
		.Act()	
		.NormalizeRetries(Data.Dimensions.DimX_Inspector)
		.NormalizeRetries(Data.Dimensions.DimY_Inspector)
		.NormalizeRetries(Data.Dimensions.DimZ_Inspector)
        .Inspect(Data.Dimensions.DimX_Inspector.Inspect(Components.DimX_Sensor))
        .Inspect(Data.Dimensions.DimY_Inspector.Inspect(Components.DimY_Sensor))
        .Inspect(Data.Dimensions.DimZ_Inspector.Inspect(Components.DimZ_Sensor))	
		.UpdateComprehensiveResult(Data.OverallResult)		
		.OnFail()
		.Dialog(300);			
    //--------------------------------------------------------			
END_IF;

IF (seq.Step(inStepID := 309,
    inEnabled := TRUE,
    inStepDescription := 'INSPECT MULTI DIMENSIONS - EXTERNAL CLOSE')) THEN
    //--------------------------------------------------------
    IF seq.IsFreshState THEN
	    SetupData();
   	END_IF    
	 
	GroupInspection
		.WithCoordinator(seq)
		.Act();
	
		GroupInspection.Inspect(Data.Dimensions.DimX_Inspector.Inspect(Components.DimX_Sensor));
		GroupInspection.Inspect(Data.Dimensions.DimY_Inspector.WithCoordinator(seq).Inspect(Components.DimY_Sensor));
		GroupInspection.Inspect(Data.Dimensions.DimZ_Inspector.WithCoordinator(seq).Inspect(Components.DimZ_Sensor));		
		GroupInspection.Inspect(Data.Dimensions.Dim_Inspector_1.WithCoordinator(seq).Inspect(Components.DimZ_Sensor));	
		GroupInspection.Inspect(Data.Dimensions.Dim_Inspector_2.WithCoordinator(seq).Inspect(Components.DimZ_Sensor));	
		GroupInspection.Inspect(Data.Dimensions.Dim_Inspector_3.WithCoordinator(seq).Inspect(Components.DimZ_Sensor));	
		
  	
		
	GroupInspection.UpdateComprehensiveResult(Data.OverallResult)	
		.Instruct('Something is wrong')
		.InstructWithImage('d:\MTS\Develop\TcOpenGroup\TcOpen\assets\logo\TcOpenWide.png',1000,1000)	
		.OnFail()
		.DialogWithExternalClose(309,inRetrySignal:=testRetry,inTerminateSignal:=testTerminate);			
		
		
    //--------------------------------------------------------			
END_IF

IF (seq.Step(inStepID := 310,
    inEnabled := TRUE,
    inStepDescription := 'INSPECT MULTI DIMENSIONS - EXTERNAL CLOSE')) THEN
    //--------------------------------------------------------
    IF seq.IsFreshState THEN
	    SetupData();
   	END_IF    
	 
	GroupInspection
		.WithCoordinator(seq)
		.Act();
	
		
		GroupInspection.Inspect(Data.Dimensions.Dim_Inspector_4.WithCoordinator(seq).Inspect(Components.DimZ_Sensor));	
		GroupInspection.Inspect(Data.Dimensions.Dim_Inspector_5.WithCoordinator(seq).Inspect(Components.DimZ_Sensor));	
		GroupInspection.Inspect(Data.Dimensions.Dim_Inspector_6.WithCoordinator(seq).Inspect(Components.DimZ_Sensor));	
		GroupInspection.Inspect(Data.Dimensions.Dim_Inspector_7.WithCoordinator(seq).Inspect(1));	
		GroupInspection.Inspect(Data.Dimensions.Dim_Inspector_8.WithCoordinator(seq).Inspect(Components.DimZ_Sensor));	
		GroupInspection.Inspect(Data.Dimensions.Dim_Inspector_9.WithCoordinator(seq).Inspect(Components.DimZ_Sensor));
		GroupInspection.Inspect(Data.Dimensions.Dim_Inspector_10.WithCoordinator(seq).Inspect(Components.DimZ_Sensor));	
  	
		
	GroupInspection.UpdateComprehensiveResult(Data.OverallResult)	
		.Instruct('Something is wrong')
		.InstructWithImage('d:\MTS\Develop\TcOpenGroup\TcOpen\assets\logo\TcOpenWide.png',1000,1000)	
		.OnFail()
		.DialogWithExternalClose(310,inRetrySignal:=testRetry,inTerminateSignal:=testTerminate);			
		
		
    //--------------------------------------------------------			
END_IF

IF (seq.Step(inStepID := 311,
    inEnabled := TRUE,
    inStepDescription := 'INSPECT MULTI DIMENSIONS - EXTERNAL CLOSE WITH FAILED DESCRIPTION')) THEN
    //--------------------------------------------------------
    IF seq.IsFreshState THEN
	    SetupData();
   	END_IF    
	 
	GroupInspection
		.WithCoordinator(seq)
		.Act();
	
		GroupInspection.Inspect(Data.Dimensions.DimX_Inspector.Inspect(Components.DimX_Sensor));
		GroupInspection.Inspect(Data.Dimensions.DimY_Inspector.WithCoordinator(seq).Inspect(10));
		GroupInspection.Inspect(Data.Dimensions.DimZ_Inspector.WithCoordinator(seq).Inspect(Components.DimZ_Sensor));		
	
  	
		
	GroupInspection.UpdateComprehensiveResult(Data.OverallResult)	
		.Instruct('Something is wrong')
		.InstructWithImage('d:\MTS\Develop\TcOpenGroup\TcOpen\assets\logo\TcOpenWide.png',1000,1000)	
		.WithDescription()
		.OnFail()
		.DialogWithExternalClose(311,inRetrySignal:=testRetry,inTerminateSignal:=testTerminate);			
		
		
    //--------------------------------------------------------			
END_IF;


IF (seq.Step(inStepID := 314,
    inEnabled := TRUE,
    inStepDescription := 'SETUP DATA MANY')) THEN
    //--------------------------------------------------------    
	 	Data := EmptyData;
	
		FOR i:=0 TO 100 DO
			Data.Dimensions.Inspectors[i].Data.PassTime := T#1500MS;    
			Data.Dimensions.Inspectors[i].Data.FailTime := T#2000MS;
			Data.Dimensions.Inspectors[i].Data.RequiredMin := 9.75;
			Data.Dimensions.Inspectors[i].Data.RequiredMax := 10.25;
			Data.Dimensions.Inspectors[i].Data.FailureDescription := 'Dimz inspection failed';
			Data.Dimensions.Inspectors[i].Data.ErrorCode := '5';		
		END_FOR

		Data.Dimensions.Inspectors[i].Data.PassTime := T#1500MS;    
		Data.Dimensions.Inspectors[i].Data.FailTime := T#2000MS;
		Data.Dimensions.Inspectors[i].Data.RequiredMin := 9.75;
		Data.Dimensions.Inspectors[i].Data.RequiredMax := 10.25;
		Data.Dimensions.Inspectors[i].Data.FailureDescription := 'Dimz inspection failed';
		Data.Dimensions.Inspectors[i].Data.ErrorCode := '5';		
		
	
		seq.CompleteStep();
    //--------------------------------------------------------			
END_IF;
IF (seq.Step(inStepID := 315,
    inEnabled := TRUE,
    inStepDescription := 'INSPECT MANY - WITH EXTERNAL CLOSE')) THEN
    //--------------------------------------------------------
    
	 
	GroupInspection
		.WithCoordinator(seq)
		.Act();
	
	FOR i:=0 TO 100 DO
			GroupInspection.Inspect(Data.Dimensions.Inspectors[i].Inspect(Components.DimX_Sensor));
	END_FOR
	
		
	
  	
		
	GroupInspection.UpdateComprehensiveResult(Data.OverallResult)	
		.Instruct('Something is wrong')
		.InstructWithImage('d:\MTS\Develop\TcOpenGroup\TcOpen\assets\logo\TcOpenWide.png',1000,1000)
		.WithDescription()
		.WithErrorCode()
		.WithFailureDescription()	
		.OnFail()
		.DialogWithExternalClose(314,inRetrySignal:=testRetry,inTerminateSignal:=testTerminate);			
		
		
    //--------------------------------------------------------			
END_IF;

IF (seq.Step(inStepID := seq.RESTORE_STEP_ID,
    inEnabled := TRUE,
    inStepDescription := 'RETURN TO THE START OF THE SEQUENCE')) THEN
    //--------------------------------------------------------	
    	seq.CompleteSequence();
    //--------------------------------------------------------			
END_IF;

seq.Close();