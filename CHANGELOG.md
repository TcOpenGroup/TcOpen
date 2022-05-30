# v0.7.x

## TcoIo

Added TcoIo library for details see [here](src\TcoIo\README.md)

# v0.6.x

## TcoCore

### Changes
- WPF Update MaterialDesign 4.4.0
- TcoTask now accesses the PLC data via cyclic or lastvalue instead of synchron due to performance degradation.
- Clears sequencers cycle timer on `Restore`
- TcoTask now accesses the PLC data via cyclic or lastvalue instead of synchron due to performance degradation.
- `TcoTask` has `ExecuteDialog` delegate that is executed prior to actual task execution when called from UI it can contain arbitrary logic to prevent or allow the execution of the task.
- `TcoTask` Checks for authentication when `Roles` property is assigned.
- WPF `ViewModelizer` simple mechanism to create ViewModel when required for the view, the call must be placed in the view like this:
- ViewModelizer simple mechanism to create ViewModel when required for the view, the call must be placed in the view like this:

~~~
protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
{
       base.OnPropertyChanged(e);

       if (e.Property == DataContextProperty)
       {                
           this.DataContext = this.DataContext.ViewModelizeDataContext<TcoTaskViewModel, TcoTask>();
       }
}
~~~

### Breaking
- `GetSignal` is now an FB the symbol is retrieved only when the pointer changes (performance issue)


## TcOpen.Inxton.Local.Security

- User login and logout are now logged
- `IAuthenticationService` now includes `HasAuthorization` method to allow access to check the user roles from `ApplicationDomain`

## TcoData

### Enhancements

- Implementation of IRepository for RavenDB. [More here](https://github.com/TcOpenGroup/TcOpen/tree/dev/src/TcoData/src/Repository/RavenDb#readme)
- WPF Added filter to recipe selector user control.

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

