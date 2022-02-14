# v0.5.x

# From version 0.5 there are notable breaking changes.

## TcoCore

### TcoObject.FB_init 

**FB_init when object is in another FUNCTION_BLOCK**


~~~
FUNCTION_BLOCK myFunctionBlock : EXTENDS TcoCore.TcoObject
VAR
    // Before
    // myObject : TcoObject(THIS^);

    // Now
    myObject : TcoObject(THIS^, TcoCore.NO.Struct);
END_VAR    
~~~

**FB_init when object is in another STRUCTURE**

~~~
TYPE
    MyStructure EXTENDS TcoCore.TcoStruct :
    STRUCT
        // Before not possible         
        // Now
        myObject : TcoObject(TcoCore.NO.Obj, THIS^);
    END_STRUCT
END_TYPE
~~~

### TcoStruct initialization

`TcoStruct` has been added to allow structure and inner objects to access `TcoContext` and take advantage of the framework features.

~~~
Data : ExampleInspectorsStruct := (Parent := THIS^);
~~~

**IMPORTANT!!!** The compiler will not warn you missing parent assignment. Missing parent assignment may result in invalid pointer/reference exceptions.