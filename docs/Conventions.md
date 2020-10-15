# TcUnit Naming Conventions
*First Draft of Naming Conventions*

My first reccomendation would be to follow the Backhoff TwinCAT naming conventions but with some modifications and extensions.  see <https://infosys.beckhoff.com/english.php?content=../content/1033/tc3_plc_intro/54043198675164555.html&id=>

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
GVLs should generally not be generally accessible from outside the library. Setting of modifying GVL variables should be done through static helpers or functions.

#### Constants
Where magic numbers are required for low level domain specific operation, these should be managed in the highest level block that makes sense without breaking an abstraction layer. I.e. Do not add `SiemensTempSensorModBusStatusWordRegister : WORD:= 3;` to a GVL of constants. Add this to the appropriate class that handles that device.

Where sizing of types may change depending on the use of the library, a [Parameter List](https://infosys.beckhoff.com/english.php?content=../content/1033/tc3_plc_intro/18014401980319499.html&id=6895410617442004539) is preferred. This allows the library to be design-time condfigured without modification of the library. Keep the parameterlist as tightly scoped as possible, and as close (in the solution hierarchy) as possible.

## FB/Class features naming.
This section covers the naming features of classes, such as methods and properties. For naming of FBs/Class, see above.

### Member Variables // MLAZ: can we not just make some rules for naming variables in general?
Class (FB) member variables should begin with 'm_' followd by the type identifier and then the variable name i.e. m_<TypeIdentifier><VariableName> 
ex. m_bTrigger, m_stAnalogStatus.
```ST
    VAR
        m_bTrigger : BOOL;
        m_nCounter : INT;
        m_stAnalogStatus : AnalogStatus;
    END_VAR
```
    
### Properties
Properties should **NOT** begin with prop like Beckhoff sample code demonstrates.  We already know that these objects are properties in the way they are exposed.  
// TODO talk with the group about property naming
ex. bBooleanProperty, BooleanProperty p_BooleanProperty

## Fluent Interfaces
It is possible in TwinCAT to create classes with fluent interfaces by returning an instance of the class (THIS^ in TwinCAT) in each method.  This allows chaining method calls together.  An example of this can be seen in Gerhard Barteling's blog post <https://www.plccoder.com/fluent-code/>.  This offers a very clean interface and usage pattern, especially in utility classes.
