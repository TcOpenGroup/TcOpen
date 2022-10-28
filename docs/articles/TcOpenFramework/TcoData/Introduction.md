# TcoData

TcOpen Framework contains a set of libraries that provide a simple yet powerful data exchange between PLC and an arbitrary data repository. TcoData implements a series of `repository` operations known as [`CRUD`](https://en.wikipedia.org/wiki/Create,_read,_update_and_delete).

## Benefits
The main benefit of this solution is data scalability; once the repository is set up, any modification of the data structure(s) will result in an automatic update of mapping objects. And therefore, there is no need for additional coding and configuration.

## How it works

The basic PLC block is [TcoDataExchange](~/api/TcoData/PlcDocu.TcoData.TcoDataExchange.yml), which has its .NET counterpart (or .NET twin) that handles complex repository operations using a modified [TcoRemoteTask](~/api/TcoData/PlcDocu.TcoData.TcoDataTask.yml), which is a form of RPC (Remote Procedure Call), that allows you to execute the code from the PLC in a remote .NET application.

## Implemented repositories

The TcoData uses a predefined interface [IRepository](~/api/TcoData/TcOpen.Inxton.Data.RepositoryBase-1.yml) that allows for the unlimited implementation of different kinds of repositories.

At this point, TcOpen supports these repositories directly:

1. [InMemory](~/api/TcoData/TcOpen.Inxton.Data.InMemory.yml)
1. [Json](~/api/TcoData/TcOpen.Inxton.Data.Json.yml)
1. [MongoDB](~/api/TcoData/TcOpen.Inxton.Data.MongoDb.yml)

## Getting started

For the data exchange to work, we will need to create our block extending the `TcoDataExchange` block. We can call it `MyDataExchanger`. 

~~~iec
FUNCTION_BLOCK MyDataExchanger EXTENDS TcoData.TcoDataExchange
~~~

We will also need to add `_data` variable to our block.  There is no specific reason for the variable to be called `_data`; it is just convention; it is, however, important as this `_data` variable is the one that will contain the data that we want to exchange between PLC and the repository.

~~~iec
FUNCTION_BLOCK MyDataExchanger EXTENDS TcoData.TcoDataExchange
VAR
    _data : MyDataForExchange;
END_VAR    
~~~

The `_data` must be of a structure that extends `TcoEntity`. So let's just create `STRUCT` that will have some variables.

~~~iec
TYPE
    MyDataForExchange EXTENDS TcoData.TcoEntity :
    STRUCT
        sampleData : REAL;
        someInteger : INT;
        someString : STRING;
    END_STRUCT
END_TYPE
~~~

As mentioned earlier, we use `remote calls` to execute the `CRUD` operations. These calls are a variant of [TcoTask](~/api/TcoCore/PlcDocu.TcoCore.TcoTask.yml) which can operate asynchronously, and we will need to call it cyclically. To ensure that, we need to call `SUPER^` in the implementation part of the `MyDataExchanger` block.

~~~iec
// Implementation of body of the FB
SUPER^();
~~~

We will now need to create an instance of `MyDataExchanger` in some [TcoContext](~/api/TcoCore/PlcDocu.TcoCore.TcoContext.yml), and call `_myDataExchanger` in the `Main` method of the context. Just to remind ourselfes all logic in TcOpen framework must be placed in the call tree of a Main method of a context.

~~~iec
FUNCTION_BLOCK MyContext EXTENDS TcoCore.TcoContext
VAR
    _myDataExchanger : MyDataExchanger(THIS^);
END_VAR
//-------------------------------------------------
//-------------------------------------------------
METHOD Main()
//-------------------------------------------------
_myDataExchanger();
~~~

And we will also need to instantiate the context in a PROGRAM and call the `Run` method.

~~~iec
PROGRAM MAIN
VAR
    _myContext : MyContext;
END_VAR
//-------------------------------------------------
//-------------------------------------------------
_myContext.Run();
~~~

At this point, we have everything ready in the PLC. 

We will now need to tell the `_myDataExchanger` what repository we will use. First, we will work with data is stored in files in [Json](https://www.json.org/json-en.html) format.

Let's create a configuration for the repository:

~~~C#
var storageDir = "C:\MYPLCREPOZIRORY";
var repository = new JsonRepository(new JsonRepositorySettings<PlainMyDataForExchange>(storageDir));
~~~

Then we will need to associate the repository with the PLC object and initialize the data exchange operations.

~~~C#
// This will bind the TcoDataExchange object to a repository
Plc.MAIN.sandbox.DataManager.InitializeRepository(repository);

// This will initialize the remote tasks for CRUD operations
Plc.MAIN.sandbox.DataManager.InitializeRemoteDataExchange();
~~~


Now we can freely shuffle the data between PLC and the local folder.

~~~iec
IF(_create) THEN
    IF(_myDataExchanger.Create(_id).Done) THEN
        _create := FALSE;
    END_IF;
END_IF;

IF(_read) THEN
    IF(_myDataExchanger.Read(_id).Done) THEN
        _read := FALSE;
    END_IF;
END_IF;

IF(_update) THEN
    IF(_myDataExchanger.Update(_id).Done) THEN
        _update := FALSE;
    END_IF;
END_IF;

IF(_delete) THEN
    IF(_myDataExchanger.Delete(_id).Done) THEN
        _delete := FALSE;
    END_IF;
END_IF;
~~~


Let's now change the repository type; what if we have too much data and need a more robust solution? 

~~~C#
var repository = new MongoDBRepository(new MongoDbRepositorySettings<PlainMyDataForExchange>("mongodb://localhost:27017","MyDatabase","MyCollection"));
~~~


## Remarks

If you have more than one .NET program in your automation application (e.g., more HMIs), make sure you do not initialize the data exchange in multiple places.






