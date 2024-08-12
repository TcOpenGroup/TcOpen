using System.Reflection;
using System.Windows;
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
[assembly: AssemblyInformationalVersion("0.6.0-dv.913+Branch.dev.Sha.4c845")]
