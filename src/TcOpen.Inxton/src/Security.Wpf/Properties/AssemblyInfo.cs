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

[assembly: XmlnsPrefix("http://vortex.mts/xaml", "vortex")]
[assembly: XmlnsDefinition("http://vortex.mts/xaml", "TcOpen.Inxton.Security.Wpf")]
[assembly: XmlnsDefinition("http://vortex.mts/xaml", "TcOpen.Inxton.Security.Wpf.UserManagement")]
[assembly: AssemblyVersion("0.4.2.0")]
[assembly: AssemblyFileVersion("0.4.2.0")]
[assembly: AssemblyInformationalVersion("0.4.2-185-Implement-task-recorder-with-logic-compiler.535+Branch.185-Implement-task-recorder-with-logic-compiler.Sha.1583eb8b750d87c2c317cd2a80e99ff907b0f74a")]
