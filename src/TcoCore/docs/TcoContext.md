# Context

**(TcoContext : ITcoContext)**

[API](~/api/TcoCore/PlcDocu.TcoCore.TcoContext.yml)

```TcoContext``` is an entry point for an application. It represents a station, functional unit, or a whole application. The `Main` method of the context is the **root of the call tree**.

```TcOpen``` application requires to have at least one ```TcoContex```.

Context can encapsulate units of different scope and size. Each context is an isolated island that manages only the objects declared within its declaration tree. Each ```TcoObject``` (more later) can have only one context. Inter-contextual access between the objects is not permitted. The context executes with ```Run``` method call from the PLC program. The ```Run``` method will take care of running ```Main``` method and other routines that are required for the context and its services.

<!-- Context usage scenarios:

![Context usage](Context003.png) -->

**Example of context implementation**

Implementation of abstract ```TcoCore.TcoContext``` class

~~~iecst
FUNCTION_BLOCK ExampleContext EXTENDS TcoCore.TcoContext
VAR    
    // State control variable
	_state : INT;    
    // Piston component
    _piston : Piston(THIS^); // About the construction via FB_init later.  
END_VAR
~~~

Implementation of abstract method ```Main```

~~~iecst
METHOD PROTECTED Main
//-------------------------------------------------------------
IF(_state = 0) THEN
    IF(_piston.MoveWork().Done) THEN
      _state := 10;
    END_IF;  
END_IF;

IF(_state = 10) THEN
    IF(_piston.MoveHome().Done) THEN
      _state := 20;
    END_IF;    
END_IF;
.
.
.
.
.
IF(_state = 20) THEN
    _state := 0;
END_IF;
~~~

Execution of the context. Here we call ```Run```. It will implicitly call the ```Main``` method implemented have above.

~~~iecst
PROGRAM MAIN
VAR
	_context : ExampleContext;
END_VAR
//-------------------------------------
_context.Run();
~~~