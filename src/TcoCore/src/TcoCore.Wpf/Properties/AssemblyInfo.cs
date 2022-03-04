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
[assembly: AssemblyVersion("1.19.1.0")]
[assembly: AssemblyFileVersion("1.19.1.0")]
[assembly: AssemblyInformationalVersion("1.19.1-update-eagle.1+1780.Branch.update-eagle.Sha.8c5fbf414d74d43374f14ead64f8f30c90308870")]

[assembly: InternalsVisibleTo("TcOpen.Inxton.TcoCore.VMTests")]


