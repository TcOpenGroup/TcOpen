# !!!UNDER CONTSTUCTION!!!

# MTS standard application template

## Preliminary note

This application template was developed for MTS company. We are making it available to the community for use or inspiration.

This application template will be further developed to meet the needs of the MTS. We will accept the input from the community. 
There will be though some limits imposed to the changes of this particular template. 
TcOpen will include different application templates that will be more open to the change from the community.

-------------------------------------------

# Application template architecture

The entry point of the application is the MAIN program cyclally called from the PLC task. 
MAIN declares the instance of the `Technology` type that is the context of the whole application. All your code should be placed within the `Main` method of technology object (`_technology.Main()`) that will properly contextualize all your code.


If you are not familiar with the architecture of TcOpen framework context concept you can find more 
[here](https://docs.tcopengroup.org/articles/TcOpenFramework/TcoCore/TcoContext.html) or more generic overview [here](https://docs.tcopengroup.org/articles/TcOpenFramework/TcoCore/Introduction.html).

*Following video introduces the application context*
<iframe width="560" height="315" src="https://www.youtube.com/embed/Nr8Y-5GHSxE" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

# Technology object

Technology object contains additional object that relate to the technology as whole and so called `controlled units` that represent sufficiently autonomous parts of the technology (e.g. stations).

## ProcessData

This application template aims to provide versastile model to allow for the extendend control of the program flow from manageble data set. Process data represent the set infomation that will be followed and processed during the production. One way of thinkig about the process data is the reciepe. Process data contain beside the instruction also data that arise during the production. Production data are being filled into the data set during the production operations.

Typically at the beginning of the production the process data are loaded into the first controlled unit (station) an Id of the production entity is assigned and stored into data repository. Each controlled unit (station) later retrives the data for the given produciton entity at the beginning of the process and returns the data (enriched by additional information about the production) to the repository at the end of the process.

## TechnologicalData

Technological data contain managable set of data that relate to the technology as a whole, such as drives settings, limits, global timers, etc. 

## ProcessTraceability

Is PLC placeholder for accessing the production data repostitory. This object points to the same traceability repository as the `ProcessData` of any controlled unit.

## GroundAll

Task that provides execution of ground task all controlled units within the the technology. Ground task of each should contain the control logic that brings the respective controlled unit into initial state.

## AutomatAll

Task that provides execution of automatic task all controlled units within the the technology. Automat task provides the nominal automatic cycle logic of each contolled unit.

## Controlled units

The technology can contain multiple controlled units. Controlled unit `CU00X` is a template from which other controlle units can derive.

# Controlled unit template

`CU00X` folder contains a template from which any controlled unit can be scaffolded. At this moment there is powershell script `Create-Controlled-Unit` located in the root of solution directory.

~~~
.\Create-Controlled-Unit.ps1 NEWCU
~~~

> The script may not work as expected when the solution is opened as filtered solution (slnf).

Running the script will modify the PLC project files; if the project is opened in the visual studio project reload will be required. You will need to add the call of the newly added controlled unit in the `Technology` manually.

~~~
FUNCTION_BLOCK Technology EXTENDS TcoCore.TcoContext
VAR
    _processSettings     : ProcessDataManager(THIS^);
    _technologySettings  : TechnologicalDataManager(THIS^);
    _processTraceability : ProcessDataManager(THIS^);
    {attribute addProperty Name "<#AUTOMAT ALL#>"}
    _automatAllTask : TcoCore.TcoTask(THIS^);
    {attribute addProperty Name "<#GROUND ALL#>"}
    _groundAllTask : TcoCore.TcoTask(THIS^);
    _cu00x         : CU00x(THIS^);
    
    _NEWCU : NEWCU(THIS^); <------ NEWLY ADDED
END_VAR
//-----------------------------------------------------

Main() <------ ATTENTION NOT BODY OF THE FUNCTION BLOCK BUT Main() METHOD!!!
//----------------------------------------------------
_processSettings();
THIS^.RtcSynchronize(true, '', 60);
_cu00x();

_NEWCU();  <------ NEWLY ADDED

//----------------------------------------------------
~~~






















