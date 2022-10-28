# TcOpen Conventions

| REVISION | DATE       | NOTES                 |
|----------|------------|-----------------------|
| 0.0      | April 2021 | Initial release       |
| 0.1      | April 2021 | STRUCT members naming |
| 0.2      | April 2021 | Component rules       |


## Introduction

Thanks for taking the time to read this document. Here we aim to outline a set of rules that will help us write consistent libraries that will serve their other consumers and us.

This document modifies recommendations from [Beckhoff TwinCAT conventions](https://infosys.beckhoff.com/english.php?content=../content/1033/tc3_plc_intro/54043198675164555.html&id=) for TcOpen project(s).

This document defines the minimum coding standards and will be updated as the project develops in time.

### Why do we need to agree on conventions

* They create a consistent look to the code so that readers can focus on content, not layout.
* They enable readers to understand the code more quickly by making assumptions based on previous experience.
* They facilitate copying, changing, and maintaining the code.
* They demonstrate Structured Text best practices.

### Terminology

For this document are terms 
- ```Function block``` and ```class``` **interchangeable**. 
- ``` Type``` is the common name for any ```FB```, ```STRUCT```, ```UNION```.

### A general note on naming

Names should be self-describing, readable considered in calling context. Use of prefixes is discouraged, except for those outlined in this document. For example to inform/warn about some property like a pointer, reference, IN_OUT reference, or to aid to CoDeSys IntelliSense to narrow the scope of search.

### General note on the use of PLC-Language

TcOpen uses exclusively IEC 61131-3 Structured Test (ST), strongly leaning to the OOP paradigm.

## Naming 

> **Type Naming**


| Block type     | Notation   | Prefix   | Example                  |
|----------------|------------|----------|--------------------------|
| FB/CLASS name  | PascalCase | NoPrefix | ```Cyclinder```          |
| ENUM type name | PascalCase | ```e```  | ```eMachineState.Start``` |
| INTERFACE name | PascalCase | ```I```  | ```ICyclinder```         |
| FUNCTION name  | PascalCase | NoPrefix | ```Add()```              |
| STRUCT name    | PascalCase | NoPrefix | ```Data```               |
| UNION name     | PascalCase | NoPrefix | ```Control```            |



> **Class member naming**

| Variable section | Notation   | Prefix   | Example                       |
|------------------|------------|----------|-------------------------------|
| METHOD name      | PascalCase | NoPrefix | ```MoveToWork()```            |
| METHOD arguments | camelCase  | NoPrefix | ```targetPosition  : LREAL``` |
| PROPERTY name    | PascalCase | NoPrefix | ```IsEnabled```               |



> **FB/class member naming**

| Variable section | Notation   | Prefix    | Example                          |
|------------------|------------|-----------|----------------------------------|
| VAR_INPUT        | camelCase  | ```in```  | ```inActualPosition  : LREAL```  |
| VAR_OUTPUT       | camelCase  | ```out``` | ```outActualPosition  : LREAL``` |
| VAR_IN_OUT       | camelCase  | ```ino``` | ```inoActualPosition  : LREAL``` |
| VAR              | camelCase  | ```_```   | ```_actualPosition  : LREAL```,  |
| VAR_STAT         | camelCase  | ```_```   | ```_actualPosition  : LREAL```,  |
| VAR_INST         | camelCase  | ```_```   | ```_actualPosition  : LREAL``` , |
| VAR CONSTANT     | UpperCase  | NoPrefix  | ```MAX_747_CRUISING_ALTITUDE```  |
| REFERENCE        | PascalCase | ```ref``` | ```refDrive```                   |
| POINTER          | PascalCase | ```p```   | ```pCyclinder```                 |
| INTERFACE name   | PascalCase | NoPrefix  | ```Cyclinder```                  |


> **STRUCT member naming**

| Group          | Notation   | Prefix    | Example              |
|----------------|------------|-----------|----------------------|
| VARIABLE       | PascalCase | NoPrefix  | ```ActualPosition``` |
| REFERENCE      | PascalCase | ```ref``` | ```refDrive```       |
| POINTER        | PascalCase | ```p```   | ```pCyclinder```     |
| INTERFACE name | PascalCase | NoPrefix  | ```Cyclinder```      |

> **Features to avoid**

| Avoid      | Use instead                               |
|------------|-------------------------------------------|
| ACTION     | METHOD                                    |
| TRANSITION | TcoOpen Framework coordination primitives |


> **Features to prefer**

| IF YOU REALLY MUST | EVERYWHERE ELSE         |
|--------------------|-------------------------|
| FUNCTION           | PRG.METHOD OR FB.METHOD |


## Identifiers

Any identifier (variable, methods, properties) should have an identifier that clearly expresses intent. Identifiers with less than ```4``` characters should be avoided (unless they represent well-known acronyms or expressions). There is no formal constraint on a maximum number of characters; however, about 25 characters should suffice to name the variable appropriately.

## Scope

All variables should be declared closest to the scope where they are used. Avoid using global declarations unless it is necessary or the global scope is required for the application.

### GVLs, PRGs

Generally, the global scope should be avoided where possible.
GVLs, PRGs, and Parameter lists must specify ```{attribute 'qualified_only'}``` to ensure access is fully scoped in the code. [Beckhoff Infosys Doc here](https://infosys.beckhoff.com/english.php?content=../content/1033/tc3_plc_intro/9007201784510091.html&id=8098035924341237087).

GVLs, PRGs should not be accessible from outside the library and should use the ```INTERNAL``` access modifier.

## Member Variables

Class (FB) member variables should begin with underscore ```_```, followed by the variable name.

~~~ iecst
VAR
    _trigger : BOOL;
    _counter : INT;
    _analogStatus : AnalogStatus;
END_VAR
~~~

### Constants

Use constants instead of magic numbers. Constants should be all caps. 

Where magic numbers are required for low-level domain-specific operations, these should be managed in the highest level block that makes sense without breaking an abstraction layer. I.e. Do not add ```SiemensTempSensorModBusStatusWordRegister : WORD:= 3;``` to a GVL of constants. Add this to the appropriate class that handles that device.

## Arrays

Arrays should be 0 based index due to consistency when used on HMI platforms (based on JavaScript, TypeScript, C#).

~~~ PASCAL
VAR
    myArray     : ARRAY[0..9] OF BOOL; // Prefered
    myArray1    : ARRAY[1..10] OF BOOL; // AVOID
END_VAR
~~~

## Namespaces

Variables defined in referenced libraries must be declared using a fully qualified name with the namespace.

~~~ Pascal
VAR
    _mixer : fbMixer; // AVOID!
    _mixer : Mixers.fbMixer; // Like this
END_VAR
~~~

## Methods

Methods names should clearly state the intent. Method name should not have any prefix. The methods should be used to perform some action (Movement, Measurement, Trigger etc.). For obtaining or setting values, prefer properties.

```Pascal
piston.MoveToWork();
laser.Measure();
dmcReader.Trigger();
```

## Properties

Property names should clearly describe intent. Properties should not start with any prefix. 

## Function Block parameter transfer

Whenever a parameter transfer is required during the construction of a class, use the ```FB_init``` method, the data passed at the object construction should be immutable and must not be changed at runtime.
For a cyclical update of parameters, use ```VAR_INPUT/OUTPUT/IN_OUT```. Cyclical logic is required to be placed in the body of the function block. The FB's body must be called in an appropriate place to be preferably executed in each cycle of the PLC task. 

## Static classes

For static-like behavior, use ```PROGRAM```.

## Component

* Component must inherit from ```TcoCore.TcoComponent```
* Components methods and properties should not be marked FINAL (sealed)
* Component should implement appropriate ```INTERFACE``` for a public contract; this is the interface that the consumers of the library will use to interact with the component. It represents the public contract that must not change during the lifetime of the particular major version of the library/framework. See [semantic versioning](https://semver.org/).
* Component members must explicitly state access modifier for methods and properties (```PUBLIC```, ```INTERNAL```, ```PROTECTED```, or ```PRIVATE```)
* Component should properly hide implementation details by marking methods preferably ```PROTECTED```.
* Consider using the ```PRIVATE``` access modifier to prevent any access to that member if you deem it necessary. Be aware, though, that private members cannot be overridden by a derived class.
* If there are any testing methods in the same library with the component, these must be marked ```INTERNAL```.
* Each action of the component should be implemented using the ```TcoTask``` class. There is no exception to this rule, even for the actions that require a single cycle to complete. Task's ```Invoke``` should be placed into a method with an appropriate name (MoveAbsolute, MoveHome, Measure).

### Cyclic call

Each component implements the logic required to run cyclically in the *body* of the Function Block. The body of the Function Block must be called from an appropriate place in the PLC program.

### Components methods

The methods that perform actions **MUST** return ```TcoCore.ITcoTaskStatus``` (typically ```TcoCore.TcoTask```). This rule applies even to the logic that requires a single-cycle execution.
