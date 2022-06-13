using System.Windows;
using System.Reflection;
using System.Windows.Markup;
using System.Runtime.CompilerServices;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]


[assembly: XmlnsPrefix("http://vortex.mts/xaml", "vortex")]
[assembly: XmlnsDefinition("http://vortex.mts/xaml", "TcoCore")]
[assembly: XmlnsDefinition("http://vortex.mts/xaml", "Tco.Wpf")]
[assembly: XmlnsDefinition("http://vortex.mts/xaml", "TcOpen.Inxton.TcoCore.Wpf")]
[assembly: XmlnsDefinition("http://vortex.mts/xaml", "TcOpen.Inxton.TcoCore.Wpf.TcoComponent")]

[assembly:Vortex.Presentation.Wpf.RenderableAssembly()]
[assembly: AssemblyVersion("0.6.0.0")]
[assembly: AssemblyFileVersion("0.6.0.0")]
[assembly: AssemblyInformationalVersion("0.6.0-386-Create-application-template.913+Branch.386-Create-application-template.Sha.4c84548ce65902faf186e6b2f45c239f599f2d02")]

[assembly: InternalsVisibleTo("TcOpen.Inxton.TcoCore.VMTests")]


