# Inxton.Vortex.Framework.Blazor

This is Inxton.Vortex.Framework.Blazor framework. Providing automatic generation of simple UI from PLC objects for Blazor Server application.

---

## Prerequisites
Look at [this README](https://github.com/TcOpenGroup/TcOpen/blob/dev/README.md) file and make sure, you have everything from the prerequisites section.

- Blazor Server project based on .NET5
- TwinCAT 3 project
- Inxton.Vortex.Framework installed with PLC Connector set up and running
- Entry.cs class with PLC instance (look at app example for this class) 

 ---
## Installing
After you have created your Blazor Server project, you need to do the following steps:

**Install latest NuGet package Inxton.Vortex.Framework.Blazor**

NuGet: https://www.nuget.org/packages/Inxton.Vortex.Presentation.Controls.Blazor-experimental/


**In `_Host.cshtml` add reference to Vortex.Framework Bootstrap theme. Replace Blazor built-in Bootstrap with the following line:**

```
 <link rel="stylesheet" href="/_content/Inxton.Vortex.Presentation.Controls.Blazor-experimental/css/inxton-bootstrap.min.css">
```
**Add Vortex.Framework renderable namespace to Blazor application.**

Add the following line to your `_Imports.razor` file:
```
@using Vortex.Presentation.Controls.Blazor.RenderableContent
```
**Register Vortex.Framework.Blazor services in DI container of your application.**

Add the following line to your `ConfigureServices` method located in `Startup.cs`:
```
services.AddVortexBlazorServices();
```
Vortex.Framework.Blazor services are located in *Vortex.Presentation.Blazor.Services* namespace.

**Build and start your PLC Connector on startup of Blazor app.**

Add the following line to your `Configure` method located in `Startup.cs`:
```
Entry.Plc.Connector.BuildAndStart();
```
Note: Replace Plc with the name of your PLC instance.

---
## How to use


To access PLC variables and enable two-way binding between source PLC object and UI elements you can look at the following example.

```C#
@page "/"
@inherits RenderableComponentBase

<p>@Entry.Plc.MAIN.Hello_World.Cyclic</p>

@code
{       
    protected override void OnInitialized()
    {
        UpdateValuesOnChange(Entry.Plc.MAIN);
    }
}
```
 
Thanks to the inheritance of *RenderableComponentBase* class and availability of *UpdateValuesOnChange* method values from PLC will be automatically updated in UI.

---

## RenderableContentControl
When you've done all steps above correctly, you should have access to the RenderableContentControl component, which is able to auto-generate UI from Vortex object. You can see some examples at [Inxton Webinar 3.0](https://github.com/Inxton/Examples.Webinar3.0/tree/main/Blazor%20Example) Blazor example.

The following line demonstrates the basic usage of the RenderableContentControl component:

```
<RenderableContentControl Presentation="Control"
                          Context="@Entry.Plc.prgWeatherStations"/>
```

You can find the RenderableContentControl documentation in **[Renderablecontentcontrol](Renderablecontentcontrol.md)** file.

---
Developed with 💗 at [MTS](https://www.mts.sk/en) - putting the heart into manufacturing.