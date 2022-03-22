## Enchancements

### TcoData

- Implementation of IRepository for RavenDB. [More here](https://github.com/TcOpenGroup/TcOpen/tree/dev/src/TcoData/src/Repository/RavenDb#readme)

# v0.5.x

# From version 0.5 there are notable changes in the way we initialize structures and nested TcoObjects.

## TcoCore

### TcoObject.FB_init 

**FB_init when object is in another FUNCTION_BLOCK**
(**No change just reminder**)

~~~
FUNCTION_BLOCK myFunctionBlock : EXTENDS TcoCore.TcoObject
VAR    
    myObject : SomeTcoObject(THIS^); 
END_VAR    
~~~

### TcoStruct initialization

`TcoStruct` has been added to allow structure and inner objects to access `TcoContext` and take advantage of the framework features.

~~~
Data : ExampleInspectorsStruct := (Parent := THIS^);
~~~

**IMPORTANT!!!** The compiler will not warn you about missing parent assignment. Missing parent assignment may result in invalid pointer/reference exceptions.

**FB_init when object is in another STRUCTURE**

~~~
TYPE
    MyStructure EXTENDS TcoCore.TcoStruct :
    STRUCT
        // Before not possible         
        // Now
        myObject : TcoObject(THISSTRUCT);
    END_STRUCT
END_TYPE
~~~



## Enchancements

### TcoCore

- [Dialogs](https://docs.tcopengroup.org/articles/TcOpenFramework/TcoCore/TcoDialogs.html)

### TcoInspectors
-  [Introduction](https://docs.tcopengroup.org/articles/TcOpenFramework/TcoInspectors/Introduction.html)

