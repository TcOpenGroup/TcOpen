# TcoCore

[INTRODUCTION](docs/Introduction.md)

## TcoCore Blazor

In your Blazor application in file `Startup.cs` add following line to `ConfigureServices` method to support external libraries used in TcoCore components. Extensions are available in namespace: 

``using TcOpen.Inxton.TcoCore.Blazor.Extensions;``

```
services.AddTcoCoreExtensions();
```


