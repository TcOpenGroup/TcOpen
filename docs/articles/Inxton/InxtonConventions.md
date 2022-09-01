# TcOpen Conventions for Inxton extensions and components

| REVISION | DATE     | NOTES           |
|----------|----------|-----------------|
| 0.0      | May 2021 | Initial release |
|

This document provides guidelines for the creation of components and extensions in Inxton.Vortex.Framework (IVF).

*The aim of this document is not to explain IVF in detail but rather provide guidelines to those already familiar with IVF. If you want to know more about IVF, visit [here](https://docs.inxton.com/).*

## Partial extensions (pex)

The partial extension allows extending a [twin class](https://docs.inxton.com/docu/articles/units/Inxton.vortex.compiler.console/Conceptual/Twins.html) in a separate file which is not affected by the IVF compiler's output. More about partial classes [here](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/partial-classes-and-methods).

Partial extensions of any block must be placed in twin projects under ```pex``` folder and subfolder that corresponds to the project's tree of the respective PLC project. For example the PLC block in `..PLC/POU/Components/Counter` will have its partial extension in `pex/POU/Components/Counter.cs`

Extending constructor in partial class `PexPreContructor` and `PexConstructor`:

~~~CSharp
// Called prior to construction of other members.
partial void PexPreConstructor(IVortexObject parent, string readableTail, string symbolTail)
{
    // Additional logic
}

// Called after the members were instantiated.
partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
{
    // Additional logic
}
~~~

These partial methods have their parameterless version for parameterless object construction.

## WPF UI components

Wpf control must be located in a project named `{PlcProjectName}.Wpf`. The project must contain assembly attribute [RenderableAssemblyAttribute](https://docs.inxton.com/docu/api/presentation_wpf/Vortex.Presentation.Wpf.RenderableAssemblyAttribute.html).

Individual UI control must be placed into a folder that matches the location of the respective PLC block in the PLC project. For instance, `PLC/POU/Pneumatics/Cyclinder` will have its user controls placed in the .net project in folder `POU/Pneumatic/Cyclinder/'. Each presentation type should have an appropriate subfolder named after presentation type (Control, ShadowControl, Service, Diagnostics, etc.).

### Naming conventions

The following table shows how to name UI controls for standardized presentation types to make them usable for automated UI rendering with `RenderableContentControl`.


| Purpose                  | View naming                      | ViewModel naming                  | Example                                                     |
|--------------------------|----------------------------------|-----------------------------------|-------------------------------------------------------------|
| Basic UI control         | {BlockName}View                  | {BlockName}Model                  | SettingsDisplayView, DriveDisplayViewModel                  |
| Diagnostics control      | {BlockName}**Diagnostics**View   | {BlockName}**Diagnostics**Model   | DriveDiganosticsView, DriveDiagnosticsViewModel             |
| Service,Manual control   | {BlockName}**Service**View       | {BlockName}**Service**ViewModel   | DriveServiceView, DriveServiceViewModel                     |
| Edit online data         | {BlockName}**Control**View       | {BlockName}**Control**Model       | SettingsControlView, SettingsControlViewModel               |
| Display online data      | {BlockName}**Display**View       | {BlockName}**Display**Model       | SettingsDisplayView, DriveDisplayViewModel                  |
| Edit shadow data         | {BlockName}**ShadowControl**View | {BlockName}**ShadowControl**Model | SettingsShadowControlView, SettingsShadowControlViewModel   |
| Display shadow data      | {BlockName}**ShadowDisplay**View | {BlockName}**ShadowDisplay**Model | SettingsShadowDisplayView, DriveShadowDisplayViewModel      |
| Custom presentation type | {BlockName}**{Custom}**View      | {BlockName}**{Custom}**Model      | Settings*MyCustomized*View, Settings*MyCustomized*ViewModel |


Other pre-requisites for automated UI rendering

- The assembly that contains Views and ViewModels must have `RenderableAssemblyAttribute` defined.
- ViewModel must inherit from [RenderableViewModel](https://docs.inxton.com/docu/api/presentation_wpf/Vortex.Presentation.Wpf.RenderableViewModel.html).
- View and ViewModel must be in the same namespace as the twin of the `Block`.

Example of ViewModel for block ```TcoCore.TcoSequencer``` of presentation type `Base`.

~~~CSharp
using Vortex.Presentation;

namespace TcoCore
{
    public class TcoSequencerViewModel : RenderableViewModelBase
    {
        public TcoSequencerViewModel()
        {
            
        }

        public TcoSequencer TcoSequencer { get; private set; }

        public override object Model { get => TcoSequencer; set { TcoSequencer = value as TcoSequencer; } }        
    }
}
~~~


Example of View for block ```TcoCore.TcoSequencer``` of presentation type `Base`.

~~~XML
<UserControl
    x:Class="TcoCore.TcoSequencerView"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:local="clr-namespace:TcoCore"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:vortex="http://vortex.mts/xaml"
    d:DesignHeight="450"
    d:DesignWidth="800"
    mc:Ignorable="d">
~~~


Example of ViewModel for block ```TcoCore.TcoSequencer``` of presentation type `Service`.

~~~CSharp
using Vortex.Presentation;

namespace TcoCore
{
    public class TcoSequencerServiceViewModel : RenderableViewModelBase
    {
        public TcoSequencerViewModel()
        {
            
        }

        public TcoSequencer TcoSequencer { get; private set; }

        public override object Model { get => TcoSequencer; set { TcoSequencer = value as TcoSequencer; } }        
    }
}
~~~


Example of View for block ```TcoCore.TcoSequencer``` of presentation type `Service`.

~~~XML
<UserControl
    x:Class="TcoCore.TcoSequencerServiceView"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:local="clr-namespace:TcoCore"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:vortex="http://vortex.mts/xaml"
    d:DesignHeight="450"
    d:DesignWidth="800"
    mc:Ignorable="d">
~~~
