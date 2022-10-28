# Object

**(TcoObject : ITcoObject)**

[API](~/api/TcoCore/PlcDocu.TcoCore.TcoObject.yml)

`TcoObject` provides access to :
- [Context](TcoContext.md)
- Reference to the parent object
- [Identity] (unique identifier across application)
- Access to a [*messaging system*](~/api/TcoCore/PlcDocu.TcoCore.TcoMessenger.yml)
- and other useful functions. (e.g. RTC)

Each block in ```TcOpen``` framework should derive from ```TcoObject```. 

If we stretch our imagination, we can think of ```TcoObject``` as ```object``` in C# (all objects derive from ```System.Object```);

### TcoObject construction (FB_init)

```TcoObject``` must be constructed via ```FB_init``` method passing in a parameter of parent ```ITcoObject```; that is usualy another ```TcoObject``` or a ```TcoContext``` eventualy other type that implements ```ITcoObject``` interface.

> As a rule, all objects should be constructed as follows:

**FB_init when object is in another FUNCTION_BLOCK**

~~~
FUNCTION_BLOCK myFunctionBlock : EXTENDS TcoCore.TcoObject
VAR
    myObject : MyTcoObject(THIS^);
    
END_VAR    
~~~

where ```THIS^``` is of ```ITcoObject```.

**FB_init when object is in another STRUCTURE**

~~~
TYPE
    MyStructure EXTENDS TcoCore.TcoStruct :
    STRUCT
        // Before not possible         
        // Now
        myObject : MyTcoObject(THISSTRUCT);
    END_STRUCT
END_TYPE
~~~

where data structure must be initialized within an function block of `TcoObject` type in the following way:

~~~
Data : ExampleInspectorsStruct := (Parent := THIS^);
~~~

> **IMPORTANT!!!** The compiler will not warn you missing parent assignment in structure declaration. Missing parent assignment may result in invalid pointer/reference exceptions.

### Messenger

[API](~/api/TcoCore/PlcDocu.TcoCore.TcoMessenger.yml)

Any ```TcoObject``` can post messages of different severity ulterior use in higher-level applications (HMI/SCADA).

 Each ```TcoObject``` contains a single message holder ```Mime``` or Most Important Message.
 
The message will be **replaced** by another message only when the incoming **message is of higher severity**.


The **persistence** of the message is within the cycle in which it was created. 
**More about messaging [here](TcoMessenger.md)**
 
> An example implementation of station object with messaging

~~~ iecst
FUNCTION_BLOCK Station001 EXTENDS TcoCore.TcoObject
~~~

~~~ iecst
METHOD CheckStationsSensors()
//------------------------------
Messenger.Debug(CONCAT('Checking stations sesnors, context cycle ', ULINT_TO_STRING(Context.StartCycleCount));
Messenger.Debug(DT_TO_STRING(Context.Rtc.NowLocal()));

IF(failed) THEN
    // This message will appear as MIME (Most important message on the object Station001)
   Messenger.Error('Some sensor just failed'); 
END_IF;    
~~~ 
