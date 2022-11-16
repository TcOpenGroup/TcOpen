# TcoDataMerge #

## Introduction ##

`TcoDataMerge` is a package to provide merging identical objects (entities).There are two way how to merging data, first is one to one entity and second is
 one to many entities. 

## MergeEntitiesData class ###
Data for merging are stored in any repository that implements `TcOpen.Inxton.Data.IRepository` interface `MergeEntitiesData<T>` is generic class where T is type of data in collection stored in repository. T object must implemented `TcOpen.Inxton.Data.IBrowsableDataObject`, `Vortex.Connector.IPlain`

## Implemnetation Example 1 ##
First you must declare an instance of the class

```csharp
   var merge = new MergeEntitiesData<TestData>(
                      repositorySource
                    , repositoryTarget
                    , reqTypes
                    , reqProperties
                    , Exclusion
                    , Inclusion
                    );
```
where  
 - repositorySource and repositoryTarget `IRepository<T>` - here is defined repository where eitities are stored. 

    ```csharp
    private  IRepository<TestData> repositorySource;
    private  IRepository<TestData> repositoryTarget;
    ```

    
    ```csharp
     repositorySource = new MongoDbRepository<TestData>(new MongoDbRepositorySettings<TestData>(connectionString, databaseName, "SourceData"));
            repositoryTarget = new MongoDbRepository<TestData>(new MongoDbRepositorySettings<TestData>(connectionString, databaseName, "TargetData"));
    ```
    or for RavenDbRepository
    ```csharp
     repositorySource = new RavenDbRepository<TestData>(new RavenDbRepositorySettings<TestData>(new string[] { @"http://localhost:8080" }, "SourceData", "", ""));
    ```
 -  reqTypes - here you can define list of types witch you want to change in `T` object
 ```csharp
    List<Type> reqTypes = new List<Type>();

    reqTypes.Add(typeof(PlainTcoAnalogueInspectorData));
    reqTypes.Add(typeof(PlainTcodigitalInspectorData));
 ```
 -  reqProperties - here you can define list of properties witch you want to change in types defined above
    ReqProperties is a colection of string and can be populated like a string items or we can use a static method `PropertyHelper.GetPropertiesNames()`. This method help populate collection without mistakes, We are sure about this required properties are in required  type
 ```csharp
    List<string> reqProperties = new List<string>();

   reqProperties = PropertyHelper.GetPropertiesNames(new PlainTcoAnalogueInspectorData(), p => p.IsByPassed, p => p.IsExcluded, p => p.Minimum, p => p.Maximum, p => p.NumberOfAllowedRetries);
 ```
 *You can use a method PropertyHelper.GetPropertiesNames to search properties of object includet to merging*

- Exclusion - here you can define special conditions for exlude from merge operations.
 **Can be also null ,then is irelevant.**
```csharp
 private bool Exclusion(object obj)
        {
            switch (obj)
            {
                // here is definitions of  all types and condition witch are relevat not to merge 
                case TcoInspectors.PlainTcoInspectorData c:
                    return c is TcoInspectors.PlainTcoAnalogueInspectorData;
             
                default:
                    break;
            }

            return false;
        }
```

- Inclusion - define aditional special rules for object to change in merge operations.**Can be also null ,then is irelevant.**
```csharp
 private bool Inclusion(object obj)
        {
            switch (obj)
            {
                case TcoInspectors.PlainTcoInspectorData c:
                    return  c.Result != (short)TcoInspectors.eOverallResult.NoAction;
                case PlainCuHeader c:
                    return c is PlainCuHeader;
            }

            return false;
        }
```





### Merge method call ###
```csharp
 merge.Merge(testDataSourceId, testDataTargetId);
```
By calling a method Merge , where  SourceId and TargetId are inputs parameters of method. This parameters are identifiers of entities ( entities implemented `Vortex.Connector.IPlain`).
Merge operation consist of:

- read entities from specified repositiories defined in constructor
- search whole entity  object and find all required type and appropriate properties of this types  and change values from source to target (also is checked conditions from Exclude and Include methods)
- update entity in target repository

## Implemnetation Example 2 ##
First you must declare an instance of the class

```csharp
  var merge = new MergeEntitiesData<TestData>(
                      repositorySource
                    , repositoryTarget
                    );
```
where  
 - repositorySource and repositoryTarget `IRepository<T>` - here is defined repository where eitities are stored. 

    ```csharp
    private  IRepository<TestData> repositorySource;
    private  IRepository<TestData> repositoryTarget;
    ```

    
    ```csharp
     repositorySource = new MongoDbRepository<TestData>(new MongoDbRepositorySettings<TestData>(connectionString, databaseName, "SourceData"));
            repositoryTarget = new MongoDbRepository<TestData>(new MongoDbRepositorySettings<TestData>(connectionString, databaseName, "TargetData"));
    ```
 

### Merge method call ###

```csharp
 merger.Merge(sourceId, targetId, Exclude,Include, ReqProperty);
```
Call a method with this parameters , all rules for merging are defined  below (see examples). 
```csharp
   private IEnumerable<string> ReqProperty(object obj)
        {
             var retVal = new List<string>();
            switch (obj)
            {
                // here you define  properties witch are relevant for reqired types to change by rework 
                case TcoInspectors.PlainTcoDigitalInspectorData c:
                    return PropertyHelper.GetPropertiesNames(c,  p => p.RetryAttemptsCount ,p =>p.IsByPassed,p => p.IsExcluded);
                case TcoInspectors.PlainTcoAnalogueInspectorData c:
                    return PropertyHelper.GetPropertiesNames(c,  p => p.RetryAttemptsCount, p => p.IsByPassed, p => p.IsExcluded);
                case TcoInspectors.PlainTcoDataInspectorData c:
                    return PropertyHelper.GetPropertiesNames(c, p => p.RetryAttemptsCount, p => p.IsByPassed, p => p.IsExcluded);
                case PlainCuHeader c:
                    return PropertyHelper.GetPropertiesNames(c, p => p.NextOnPassed, p => p.NextOnFailed);

                default:
                    break;
            }

            return new List<string>();

            return new List<string>();
        }
```
```csharp
        private bool Exclude(object obj)
        {
            // some special con
            return false;
        }
```
```csharp
        private bool Include(object obj)
        {
            switch (obj)
            {
                // here is definitions of  all types and condition witch are relevat to change by rework 
                case IPlainstCheckerData c:
                    return c is IPlainstCheckerData;  //c.Result != (short)enumCheckResult.NoAction;
                //case PlainstAnalogueCheckerData c:
                //    return c is PlainstAnalogueCheckerData;
              
                default:
                    break;
            }

            return false;
        }
```
By calling a method Merge , where  SourceId and TargetId are inputs parameters of method. This parameters are identifiers of entities ( entities implemented `Vortex.Connector.IPlain`).
Merge operation consist of:

- read entities from specified repositiories defined in constructor
- search whole entity  object and find all required type and appropriate properties of this types  and change values from source to target (also is checked conditions from Exclude and Include methods)
- update entity in target repository



