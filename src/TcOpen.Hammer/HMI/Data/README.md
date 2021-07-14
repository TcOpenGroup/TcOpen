# How to get started with TcoData

## The PLC part

- First of all you have to define what kind of data you'd like to collect. In this example I'd like to collect data defined in `Station001_ProductionData`.

- `Station001_ProductionData` has to extend from `TcoData.Entity` in order to be persisted in a database.

- Now create a `DataManager` function block, which will extend by `TcoData.TcoDataExchange`

Don't forget to call `SUPER^()` in the body of the `DataManager` function block!

- Create a variable called `_data` with type `Station001_ProductionData`. 

You will get an error if there's no variable called `_data` and if the type of the variable doesn't extend 'TcoData.Entity'.

- Create an instance of `DataManager`. I created the instance in `Station001`. Don't forget to call the instance in the body of the function block in `Station001`

```
FUNCTION_BLOCK Station001 EXTENDS TcoCore.TcoObject
VAR
	//previous variables...
	_dataManager : DataManager(THIS^);
END_VAR
---
//previous calls
_dataManager();
```

- Since I'd like to access `DataManager` and `ProductionData` outside of the function block I added two properties which act as a shortuct to correct instances of `DataManager` and `Station001_ProductionData`

- In `Station001_AutomatMode` I added a property `Station` which will provide an access to the `Station001` function block.

- In the automat mode, step `850` fill the structure with data and create a record in a database.
Note that I create a record with the same `Identifier` as `JigId`. If you create a record with different `Identifier` you must keep that in mind when you are retrieving the value from the db.


```
IF(Step(850, 
		TRUE, 
		'SAVE DATA')) THEN
//--------------------------------------------
	Station.ProductionData.Result := THIS^.Context.StartCycleCount MOD 2 = 0;
	Station.ProductionData.JigId := ULINT_TO_STRING(THIS^.Context.StartCycleCount);
	IF  Station.ProductionData.Result THEN
		Station.ProductionData.Errors := '';
	ELSE
		Station.ProductionData.Errors := 'something went wrong';
	END_IF
	StepCompleteWhen(Station.DataManager.Create(Identifier := Station.ProductionData.JigId).Done);
//--------------------------------------------	
END_IF; 

```

Now it's important to run the Inxton builder, since this implementation of `DataManager` works only with Inxton.


We're done with the PLC part, now it's time to do some plumbing on the C# side.


## PC part

- First of all we have to connect `DataManager` with some repository (a database). 

Do it at the startup of the application, since it's not necessary to do it more than once. I initialized `DataManager in `App.xaml.cs` like this

```csharp
	var mongoUri = "mongodb://localhost:27017";
	var databaseName = "Hammer";
	var collectionName = "HammerCollection";
	var mongoSettings = new MongoDbRepositorySettings<PlainStation001_ProductionData>(mongoUri,databaseName,collectionName);
	var repository = new MongoDbRepository<PlainStation001_ProductionData>(mongoSettings);
	Entry.PlcHammer.TECH_MAIN._app._station001._dataManager.InitializeRepository(repository); 
```

It's important not to forget the prefix `Plain` ! 

If the type of the structure I'd like to save is called 'Station001_ProductionData' the type that will be in database (so called DTO-data transfer object or POCO - plain old  CLR object) is of type `Plain`+`NameOfMyStructure`.

- Now that repo is connected to repository and PLC, we can use the default dataview. 

In `MainWindow.xaml` I added a new tab with the `DataView` and set the `DataContext` to the data manager instance.

```xml
	<vortex:DataView DataContext="{Binding PlcHammer.TECH_MAIN._app._station001._dataManager}" />
```

Note that the namespace must exist in the root tag.

```xml
<Window
    x:Class="PlcHammer.Hmi.MainWindow"
	//existing fields
    xmlns:vortex="http://vortex.mts/xaml"
    Title="MainWindow"
    //other fields
	>
```

## Start MongDB

Now start MongoDB. Please note taht you `--dbpath`  and the version can vary, so change it according to your mongo install.


```
"C:\Program Files\MongoDB\Server\4.4\bin\mongod.exe"  --dbpath C:\DATA\DB446\ 
```

## We're done!

Start the app, get the station into Ground position, then start the automatic sequence and data should magically appear in the database.
