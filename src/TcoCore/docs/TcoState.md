# State

**(TcoState : ITcoState)**

[API](~/api/TcoCore/PlcDocu.TcoCore.TcoStateBase.yml)


The state controller ```TcoState``` manages states of the system.

```TcoState``` holds the control variable and manages the change via ```TcoState.ChangeState(newState)```.

The override of the ```TcoState.OnStateChange(lastState, newState)``` method allows to perform operation on transition between the states.

~~~iecst
IF(State = 10) THEN
    IF(a.DoSomething().Done) THEN
        ChangeState(20).Restore(VerticalCylinder).Restore(HorizontalCylinder);  // Change state and restore objects
    END_IF;    
END_IF;    

IF(s.State = 20) THEN
    IF(VerticalCylinder.DoSomething().Done) THEN
        ChangeState(10);    
    END_IF;    
END_IF;    

//--------------------------------------------------
// Override of OnStateChange method
//--------------------------------------------------
METHOD PROTECTED OnStateChange
VAR_INPUT
	PreviousState	: INT;
	NewState 		: INT;
END_VAR

// On transition to state 10
IF(NewState = 10) THEN
    logger.Log('Transitioning to state no 10');
    VerticalCylinder.Restore();
END_IF;    
~~~

#### Restoring state of objects

The restorable objects alleviate the burden of finding the right place and time for restoring objects in the PLC program.

Any object that implements correctly ```ITcoRestorable``` is eligible for implicit auto-restore. ```ITcoRestorable.Restore()``` method must implement the logic that brings the object to the initial state ```Ready``` and thus make it ready for the new execution. 

We already mentioned restoring mechanisms in the section about ```ITcoTask```. The example above demonstrates two ways of performing **explicit** the auto restore:

1. In the state ```10``` call 
```
s.ChangeState(20).Restore(VerticalCylinder).Restore(HorizontalCylinder)
``` 
restores the state of the object ```VerticalCylinder``` and ```HorizontalCylinder```;

2. In the override ```OnStateChange```, we restore only object ```VerticalCylinder```.

**Implicit** restoring mechanism restores object without explicit coding. There are two ways the object can be restored:

1. The objects that are **directly declared** in the body of the object of ```ITcoState``` will be restored when

    1. The ```ITcoState``` changes state,
    1. ```ITcoState``` is configured auto-restorable,
    1. ```IRestorable``` object is a direct member of the ```ITcoState```.

In this case, the state of the child objects (```ITcoObject``` declared directly in the state block) is restored when the state of the parent ```ITcoState``` changes.

~~~iecst
//---------------------------------------------------------
FUNCTION_BLOCK MyContext EXTENDS TcoCore.TcoContext
VAR
    // AutoRestoreMembers INDICATES THE STATES MEMBERS ARE AUTO-RESTORABLE
    _myState : MyStateController(THIS^, eRestoreMode.AutoRestoreMembers); 
END_VAR   
//---------------------------------------------------------
FUNCTION_BLOCK MyStateController : EXTENDS TcoCore.TcoState
VAR
    VerticalCylinder : MyComponentThasDoesSomething(THIS^);
END_VAR;    

IF(State = 10) THEN
    IF(VerticalCylinder.DoSomething().Done) THEN
        ChangeState(20); 
    END_IF;    
END_IF;    

IF(s.State = 20) THEN
    IF(VerticalCylinder.DoSomething().Done) THEN
        ChangeState(10);    
    END_IF;    
END_IF;    
~~~

2. The classes that implement the auto-restorable mechanism (```TcoTask```) will restore its state upon the call of the method executing an action, provided that there were at least two consecutive cycles of the context where that executing method was not called.
