# !!!WORK IN PROGRESS!!!

# TcOpen Naming Conventions

This documents modifies recommendations from [Beckhoff TwinCAT conventions](https://infosys.beckhoff.com/english.php?content=../content/1033/tc3_plc_intro/54043198675164555.html&id=) for the purpose of TcOpen project(s).

## Introduction

Thanks for taking time to read this document. Here we aim to outline a set of rules that will help us all to write consistent libraries that will serve us and its other consumers.

### Why do we need agree on conventions

* They create a consistent look to the code, so that readers can focus on content, not layout.
* They enable readers to understand the code more quickly by making assumptions based on previous experience.
* They facilitate copying, changing, and maintaining the code.
* They demonstrate Structured Text best practices.

### Terminology

For the purpose of this document are terms ```Function block``` and ```class``` **interchangeable**.

### A General note on naming

Names should be self-describing, readable considered in calling context.

### General note on use of PLC-Language

These library projects use exclusively Structured Test (ST) language and OOP extensions.

## Naming overview

> **Type Naming**

--------
|   Block type   |  Notation  |    Prefix     |                       Example                        |
| :------------- | :--------- | :------------ | :--------------------------------------------------- |
| FB/CLASS name  | PascalCase | NoPrefix      | ```Cyclinder```                                    |
| ENUM type name | PascalCase | NoPrefix      | ```MachineState.Start``` |
| INTERFACE name | PascalCase | ```I```       | ```ICyclinder```                                     |
| FUNCTION name  | PascalCase | NoPrefix      | ```Add()```                                         |
| STRUCT name    | PascalCase | NoPrefix      | ```Data```                                         |
| UNION name     | PascalCase | NoPrefix      | ```Control```                                         |



> **Member naming**

|      Variable section      |  Notation  |  Prefix   |            Example            |
| :------------------------- | :--------- | :-------- | :---------------------------- |
| METHOD name                | PascalCase | NoPrefix  | ```MoveToWork()```            |
| METHOD arguments           | camelCase  | NoPrefix  | ```targetPosition  : LREAL``` |
| PROPERTY name              | PascalCase | NoPrefix  | ```IsEnabled```               |
| INTERFACE name             | PascalCase | ```ip```  | ```ipCyclinder```             |
| REFERENCE name Declaration | camelCase  | ```ref``` | ```refDrive```                |
| POINTER Declaration        | camelCase  | ```p```   | ```pCyclinder```              |

> **Variable naming**

| Variable section | Notation  |  Prefix   |                                  Example                                  |
| :--------------- | :-------- | :-------- | :------------------------------------------------------------------------ |
| VAR_INPUT        | camelCase | ```in```  | ```inActualPosition  : LREAL``` , ```_advancedCyclinder : fbCyclinder```  |
| VAR_OUTPUT       | camelCase | ```out``` | ```outActualPosition  : LREAL``` , ```_advancedCyclinder : fbCyclinder``` |
| VAR_IN_OUT       | camelCase | ```ino``` | ```inoActualPosition  : LREAL``` , ```_advancedCyclinder : fbCyclinder``` |
| VAR              | camelCase | ```_```   | ```_actualPosition  : LREAL```, ```_advancedCyclinder : fbCyclinder```    |
| VAR_STAT         | camelCase | ```_```   | ```_actualPosition  : LREAL```, ```_advancedCyclinder : fbCyclinder```    |
| VAR_INST         | camelCase | ```_```   | ```_actualPosition  : LREAL``` , ```_advancedCyclinder : fbCyclinder```   |
| VAR CONSTANT     | UpperCase | NoPrefix  | ```MAX_747_CRUISING_ALTITUDE```                                           |

> **Features to avoid**

| Avoid  | Use instead |
| :----- | :---------- |
| ACTION | METHOD      |

## Identifiers

Any identifier (variable, methods, properties...) should have an identifier that clearly express intent. Identifiers with less then 4 characters should be avoided (unless they express well known acronyms or expressions); there is no formal constraint on maximum number of characters, however about 25 characters should suffice.

### Constants

Constants should be ALLCAPS...

## Scope

### GVLs, PRGs and Parameters lists

Generally, global scope should be avoided where possible.
GVLs, PRGs and Parameter lists must specify ```{attribute 'qualified_only'}``` to ensure access is fully scoped in the code. [Beckhoff Infosys Doc here](https://infosys.beckhoff.com/english.php?content=../content/1033/tc3_plc_intro/9007201784510091.html&id=8098035924341237087).

GVLs, PRGs must not be accessible from outside the library and must use access modifier ```INTERNAL```.
Setting / modifying GVL variables should be done through static helpers or functions.

### Constants

Where magic numbers are required for low level domain specific operation, these should be managed in the highest level block that makes sense without breaking an abstraction layer. I.e. Do not add ```SiemensTempSensorModBusStatusWordRegister : WORD:= 3;``` to a GVL of constants. Add this to the appropriate class that handles that device.

Where sizing of types may change depending on the use of the library, a [Parameter List](https://infosys.beckhoff.com/english.php?content=../content/1033/tc3_plc_intro/18014401980319499.html&id=6895410617442004539) is preferred. This allows the library to be design-time condfigured without modification of the library. Keep the parameterlist as tightly scoped as possible, and as close (in the solution hierarchy) as possible.

i.e. 
```Pascal
// DrivesParams is a Parameter List, as array size needs to be flexible
drives : ARRAY[1..DrivesParams.NumberOfDrives] OF FB_Drive;
```

## Namespaces

Variables defined in referenced libraries must be declared using fully qualified name with namespace.

~~~ Pascal
VAR
    _mixer : fbMixer; // AVOID!
    _mixer : Mixers.fbMixer; // Like this
END_VAR
~~~

## Methods

Methods names should clearly state the intent. Method name should not have any prefix (with exception for testing methods). Methods in components should be used to perform an action (Movement, Measurement, Trigger etc.)

```Pascal
    piston.MoveToWork();
    laser.Measure();
    dmcReader.Trigger();
```

### Properties

Property name should clearly describe intent. In general the name

Properties should **NOT** begin with prop like Beckhoff sample code demonstrates.  We already know that these objects are properties in the way they are exposed.  
// TODO talk with the group about property naming
ex. bBooleanProperty, BooleanProperty p_BooleanProperty

## Fluent Interfaces

It is possible in TwinCAT to create classes with fluent interfaces by returning an instance of the class (THIS^ in TwinCAT) in each method.  This allows chaining method calls together.  An example of this can be seen in Gerhard Barteling's blog post <https://www.plccoder.com/fluent-code/>.  This offers a very clean interface and usage pattern, especially in utility classes.

## Function Block parameter transfer

Whenever a parameter transfer is required during construction of a class use ```FB_Init``` method
For cyclical update of parameters VAR_INPUT/OUTPUT/IN_OUT that are called.

## Static classes

For entry point of static classes and function use PROGRAMs or GVLs.

## Pointers

In general avoid use of pointers, use REFERENCE TO whenever possible. However, pointers are allowed in specific instances. The use of pointers must be approved and reviewed by the owners of the repository.

## Allocated variables (```AT %I* %Q*```)

All allocated variables should be defined in GVL (typically named ```IO```). GVL containing allocated variable should be used exclusively for the variables that are intended to map physical hardware. Such GVL should be structured in a way that resembles distribution and topology of actual hardware.

## Member Variables

Class (FB) member variables should begin with underscore ```_``` followed the variable name.

~~~Pascal
    VAR
        _Trigger : BOOL;
        _Counter : INT;
        _AnalogStatus : AnalogStatus;
    END_VAR
~~~

## Components

Component is any class of which the purpose is to control and describe a physical (Robot, Piston, Drive) or virtual (Operator, Warehouse) component.

Component must not contain application specific code to be reusable by other consumers. Components must also allow to be extended by the consumers.

* Component must inherit from ```TcoCore.fbComponent```
* Component must not be marked FINAL (sealed)
* Component should implement appropriate ```INTERFACE``` for public contract
* Component members must explicitly state access modifier for methods and properties (```PUBLIC```, ```INTERNAL```, ```PROTECTED```, or ```PRIVATE```)
* Component should properly hide implementation details by marking methods preferably ```PROTECTED```.
* Consider use of ```PRIVATE``` access modifier to prevent any access to that member if you deem it necessary, be aware though that private members cannot be overridden by the class that inherits your component and consumers will not be able to change
* Component's testing methods must be marked ```INTERNAL```. Testing classes/members that are in a separate testing project can be ```PUBLIC```.

### Cyclic call

Each component implements logic that is required to run cyclically in the *body* of the function block.

### Components methods

The methods that perform actions **MUST** return TcoCore.ITcoTask. Particular implementation of ```TcoCore.ITcoTask.Execute()``` must be called in the body of the function block.

```Pascal
 // Simple example of state driven by return values from components. Piston return true when _work or _home position sensor are reached respectively.
 CASE state OF
 0:
    IF(verticalPiston.MoveToHome().Done
    AND_THEN horizontalPiston.MoveToHome().Done
    AND_THEN gripper.MoveToHome().Done)
    )
    THEN
        state := state + 1;
    END_IF;
 1:
    IF(horizontalPiston.MoveToWork().Done) THEN
        state := state + 1;
    END_IF;
 1:
    IF(verticalPiston.MoveToWork().Done) THEN
        state := state + 1;
    END_IF;
 2:
    IF(gripper.MoveToWork().Done) THEN
        state := state + 1;
    END_IF;
```
