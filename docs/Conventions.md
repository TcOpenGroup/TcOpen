# TcOpen Naming Conventions
*First Draft of Naming Conventions*

My first reccomendation would be to follow the Backhoff TwinCAT naming conventions but with some modifications and extensions.  see <https://infosys.beckhoff.com/english.php?content=../content/1033/tc3_plc_intro/54043198675164555.html&id=>

## Naming overview

| Object Name               | Notation   | Plural | Prefix | Suffix | Char Mask          | Underscores |
|:--------------------------|:-----------|:-------|:-------|:-------|:-------------------|:------------|
| FB/Block name             | PascalCase | No     | No     | Yes    | [A-z][0-9]         | No          |
| Method name               | PascalCase | Yes    | No     | No     | [A-z][0-9]         | No          |
| Method arguments          | camelCase  | Yes    | No     | No     | [A-z][0-9]         | No          |
| Local variables           | camelCase  | Yes    | No     | No     | [A-z][0-9]         | No          |
| Constants name            | PascalCase | No     | No     | No     | [A-z][0-9]         | No          |
| Field name                | camelCase  | Yes    | No     | No     | [A-z][0-9]         | Yes         |
| Properties name           | PascalCase | Yes    | No     | No     | [A-z][0-9]         | No          |
| Enum type name            | PascalCase | Yes    | No     | No     | [A-z]              | No          |


## Member Variables
Class (FB) member variables should begin with 'm_' followd by the type identifier and then the variable name i.e. m_<TypeIdentifier><VariableName> 
ex. m_bTrigger, m_stAnalogStatus.
    
```ST
    VAR
        m_bTrigger : BOOL;
        m_nCounter : INT;
        m_stAnalogStatus : AnalogStatus;
    END_VAR
```
   
## Methods
Methods names should clearly state the intent. Method name should not have any prefix (??some exceptions for testing methods??). Methods in components should be used to perfrom an action (Movement, Measurement, Trigger etc.)

```
    piston.MoveToWork();
    laser.Measure();
    dmcReader.Trigger();    
```

The methods **MUST** return BOOL value where ```true``` indicates the process was completed. (?? or complex return type that would provide information about the state of the component??).

```
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
    
## Properties
Properties should **NOT** begin with prop like Beckhoff sample code demonstrates.  We already know that these objects are properties in the way they are exposed.  
// TODO talk with the group about property naming
ex. bBooleanProperty, BooleanProperty p_BooleanProperty

## Fluent Interfaces
It is possible in TwinCAT to create classes with fluent interfaces by returning an instance of the class (THIS^ in TwinCAT) in each method.  This allows chaining method calls together.  An example of this can be seen in Gerhard Barteling's blog post <https://www.plccoder.com/fluent-code/>.  This offers a very clean interface and usage pattern, especially in utility classes.
