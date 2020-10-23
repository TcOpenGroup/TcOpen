# TcOpen Naming Conventions
*First Draft of Naming Conventions*

My first reccomendation would be to follow the Backhoff TwinCAT naming conventions but with some modifications and extensions.  see <https://infosys.beckhoff.com/english.php?content=../content/1033/tc3_plc_intro/54043198675164555.html&id=>

## Naming overview

| Object Name               | Notation   |  Prefix   |  Example                                               |
|:--------------------------|:-----------|:----------|:-------------------------------------------------------|
| FB/Block name Definition  | PascalCase | FB_?or fb? | FB_Cylinder or fbCyclinder                             |
| FB/Block name Declaration | PascalCase | FB_?or fb? | FB_AdvancedCylinder or fbAdvancedCyclinder             |
| Method name               | PascalCase |   No or n?  | MoveToWork()                                           |
| Method arguments          | camelCase  |     No    | targetPosition  : LREAL                                |
| Local variables           | camelCase  |     _     | _ actualPosition  : LREAL                              |
| Constants name            | UpprerCase |     No    | MAX_747_CRUISING_ALTIDUDE                              |
| Properties name           | PascalCase |           | IsEnabled                                              |
| ENUM type name            | PascalCase | E_? or no? | E_MachineState.Start or  MachineState.Start            |
| Interface name Definition | PascalCase | I_? or I?   | I_Cylinder or ICyclinder                               |
| Interface Declaration     | PascalCase | No? or ip?  | Cylinder or ipCyclinder                                |
| Reference name Declaration| camelCase  | _ ?or ref? | _ drive or refDrive                                    |
| Pointer Declaration       | camelCase  | p? or _ ?  | Cylinder or ipCyclinder                                |
| FUNCTION name Declaration | PascalCase | F? or no?  | F_Add() or Add()                                       |
| STRUCT name Declaration   | PascalCase | ST_? or no?  | ST_Data or Data                                      |



## Cyclic call?

Each component MUST implment abstractFB ```Component``` (abstract method Cyclic)

## Function Block parameter transfer

FB_Init || Properties || VAR_INPUT/OUTPUT/IN_OUT

## Static classes?

Use PROGAM || Use FUNCTION_BLOCK in a GVL

## Pointers

Pointer are || are not allowed?

## Allocated variables (AT %I* %Q*)

Same as other variables? || Specificly marked? || Should be defined only in GVL


## Member Variables
Class (FB) member variables should begin with 'm_' followd by the type identifier and then the variable name i.e. m_<TypeIdentifier><VariableName> 
ex. m_bTrigger, m_stAnalogStatus.
    
```Pascal
    VAR
        m_bTrigger : BOOL;
        m_nCounter : INT;
        m_stAnalogStatus : AnalogStatus;
    END_VAR
```
=======
Coding conventions serve the following purposes:
<br/>_Adapted from MSDN_
* They create a consistent look to the code, so that readers can focus on content, not layout.
* They enable readers to understand the code more quickly by making assumptions based on previous experience.
* They facilitate copying, changing, and maintaining the code.
* They demonstrate Structured Text best practices.

## A General note on naming
// TODO Elegant. Self Describing. Readable. considered in calling context. 
<br/>

## Type Naming
These convention shall be followed for creating any new instantiable, extensible or invokeable type (Structures, FBs, Interfaces, Functions, Enums etc).
<br/>
### Structures
// TODO
<br/>
### FBs & Classes
// TODO Classic FB (VAR_IN / VAR_OUT) vs Classes (method and prop access) differences? FB_ & T_?
<br/>

### Interfaces
// TODO
<br/>

### Functions
Functions shall be prefixed with "F_"

### Enums
// TODO
<br/>

### GVLs and Parameter Lists
// TODO
Generally, global scope should be avoided where possible.
GVLs and Parameter lists must specify `{attribute 'qualified_only'} ` to ensure access is fully scoped in the code. [Beckhoff Infosys Doc here](https://infosys.beckhoff.com/english.php?content=../content/1033/tc3_plc_intro/9007201784510091.html&id=8098035924341237087).
GVLs should generally not be accessible from outside the library. Setting / modifying GVL variables should be done through static helpers or functions.

#### Constants
Where magic numbers are required for low level domain specific operation, these should be managed in the highest level block that makes sense without breaking an abstraction layer. I.e. Do not add `SiemensTempSensorModBusStatusWordRegister : WORD:= 3;` to a GVL of constants. Add this to the appropriate class that handles that device.

Where sizing of types may change depending on the use of the library, a [Parameter List](https://infosys.beckhoff.com/english.php?content=../content/1033/tc3_plc_intro/18014401980319499.html&id=6895410617442004539) is preferred. This allows the library to be design-time condfigured without modification of the library. Keep the parameterlist as tightly scoped as possible, and as close (in the solution hierarchy) as possible.

i.e. 
```Pascal
// DrivesParams is a Parameter List, as array size needs to be flexible
drives : ARRAY[1..DrivesParams.NumberOfDrives] OF FB_Drive; 
```

## FB/Class features naming.
This section covers the naming features of classes, such as methods and properties. For naming of FBs/Class, see above.

### Member Variables // MLAZ: can we not just make some rules for naming variables in general?
Class (FB) member variables should begin with 'm_' followd by the type identifier and then the variable name i.e. m_<TypeIdentifier><VariableName> 
ex. m_bTrigger, m_stAnalogStatus.
    
```Pascal
VAR
    m_bTrigger : BOOL;
    m_nCounter : INT;
    m_stAnalogStatus : AnalogStatus;
END_VAR
```    
   
## Methods
Methods names should clearly state the intent. Method name should not have any prefix (??some exceptions for testing methods??). Methods in components should be used to perfrom an action (Movement, Measurement, Trigger etc.)

```Pascal
    piston.MoveToWork();
    laser.Measure();
    dmcReader.Trigger();    
```

The methods **MUST** return BOOL value where ```true``` indicates the process was completed. (?? or complex return type that would provide information about the state of the component??).

```Pascal
 // Simple example of state driven by return values from components. Piston return true when _work or _home position sensor are reached respectively.
 CASE state OF
 0:
    IF(verticalPiston.MoveToHome() 
    AND_THEN horizontalPiston.MoveToHome()
    AND_THEN gripper.MoveToHome()) 
    ) 
    THEN
        state := state + 1;
    END_IF;
 1:
    IF(horizontalPiston.MoveToWork()) THEN
        state := state + 1;
    END_IF;
 1:
    IF(verticalPiston.MoveToWork()) THEN
        state := state + 1;
    END_IF;
 2:
    IF(gripper.MoveToWork()) THEN
        state := state + 1;
    END_IF;
```
    
### Properties
Properties should **NOT** begin with prop like Beckhoff sample code demonstrates.  We already know that these objects are properties in the way they are exposed.  
// TODO talk with the group about property naming
ex. bBooleanProperty, BooleanProperty p_BooleanProperty

## Fluent Interfaces
It is possible in TwinCAT to create classes with fluent interfaces by returning an instance of the class (THIS^ in TwinCAT) in each method.  This allows chaining method calls together.  An example of this can be seen in Gerhard Barteling's blog post <https://www.plccoder.com/fluent-code/>.  This offers a very clean interface and usage pattern, especially in utility classes.
