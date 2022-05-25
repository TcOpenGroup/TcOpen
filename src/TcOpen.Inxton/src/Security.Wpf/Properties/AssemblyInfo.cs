using System.Windows;
using System.Reflection;
using System.Windows.Markup;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]

[assembly: XmlnsPrefix("http://vortex.security.mts/xaml", "vortexs")]
[assembly: XmlnsDefinition("http://vortex.security.mts/xaml", "TcOpen.Inxton.Local.Security.Wpf")]
[assembly: AssemblyVersion("0.6.0.0")]
[assembly: AssemblyFileVersion("0.6.0.0")]
[assembly: AssemblyInformationalVersion("0.6.0-386-Create-application-template.913+Branch.386-Create-application-template.Sha.4c84548ce65902faf186e6b2f45c239f599f2d02")]
