# TcUnit Naming Conventions
*First Draft of Naming Conventions*

My first reccomendation would be to follow the Backhoff TwinCAT naming conventions but with some modifications and extensions.  see <https://infosys.beckhoff.com/english.php?content=../content/1033/tc3_plc_intro/54043198675164555.html&id=>

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
    
## Properties
Properties should **NOT** begin with prop like Beckhoff sample code demonstrates.  We already know that these objects are properties in the way they are exposed.  
// TODO talk with the group about property naming
ex. bBooleanProperty, BooleanProperty p_BooleanProperty

## Fluent Interfaces
It is possible in TwinCAT to create classes with fluent interfaces by returning an instance of the class (THIS^ in TwinCAT) in each method.  This allows chaining method calls together.  An example of this can be seen in Gerhard Barteling's blog post <https://www.plccoder.com/fluent-code/>.  This offers a very clean interface and usage pattern, especially in utility classes.
