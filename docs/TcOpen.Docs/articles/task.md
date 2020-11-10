# Execution of tasks in a component

** We are drafting concept here**

In order to achieve use of the components across different programming approaches yet using OOP implementation we need to establish a way of consuming the components that would allow wide usability across different scenarios.

## Task

Each specific task that component can perform should be called in a ```Task``` coordinator represented by ```fbTask``` class.

Tasks can have  these states:

|   State    |                                                                 Description                                                                  |     Method      |
| :--------- | :------------------------------------------------------------------------------------------------------------------------------------------- | :-------------- |
| Idle       | Task is idle and will be executed when ```Invoke()``` called.                                                                                | Invoke()        |
| Requesting | Task has been requested by ```Invoke``` method. When the ```Execute()``` in the body of the function block is hit the task will be executed. |                 |
| Executing  | The handler was hit at least once and the task is being executed form the body of the function block.                                        | Execute()       |
| Error      | Task has failed and will not continue execution nor can be executed again until ```Reset()``` method of the task is called.                  | ThrowWhen(BOOL) |
| Done       | Task was completed. ```Done``` state remains active until Tasks' ```Done``` property is queried.                                             | DoneWhen(BOOL)  |


**EXAMPLE**

~~~ PASCAL
FUNCTION_BLOCK fbCylinder EXTENDS fbComponent IMPLEMENTS ICylinder
//---------------------------------------------------------------
//                     FB DECLARATIONS
//---------------------------------------------------------------
VAR_INPUT
	inAtHomePos : BOOL;		
	inAtWorkPos : BOOL; 
END_VAR
VAR_OUTPUT	
	outToHomePos : BOOL;	
	outToWorkPos : BOOL;
END_VAR

VAR		
	_moveHomeTask : fbTask(THIS^);
	_moveWorkTask : fbTask(THIS^);
	_stopTask : fbTask(THIS^);
END_VAR
//---------------------------------------------------------------
//                     FB BODY
//---------------------------------------------------------------

// Task handlers

IF(_moveHomeTask.Execute()) THEN 
	outToHomePos := TRUE;
	outToWorkPos := FALSE;
	_moveHomeTask.DoneWhen(inAtHomePos);
END_IF

IF(_moveWorkTask.Execute()) THEN
	outToWorkPos := TRUE;
	outToHomePos := FALSE;
	_moveWorkTask.DoneWhen(inAtWorkPos);
END_IF;	 

IF(_stopTask.Execute()) THEN	
	outToWorkPos := FALSE; 
	outToHomePos := FALSE;
	_moveWorkTask.Reset();
	_moveHomeTask.Reset();
	_stopTask.Reset();
	_stopTask.DoneWhen(TRUE);	
END_IF

//----------------------------------------------------
//           MoveToHome method 
//----------------------------------------------------
METHOD MoveToHome : ITask
//----------------------------------------------------
THIS^._moveHomeTask.Invoke();
MoveToHome := THIS^._moveHomeTask;

//----------------------------------------------------
//           MoveToWork method declarations
//----------------------------------------------------

METHOD MoveToWork : ITask
//----------------------------------------------------
THIS^._moveWorkTask.Invoke();
MoveToWork := THIS^._moveWorkTask;
.
.
.
~~~
