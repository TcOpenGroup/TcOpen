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
[assembly: XmlnsDefinition("http://vortex.mts/xaml", "TcoInspectors")]

[assembly: Vortex.Presentation.Wpf.RenderableAssembly()]
[assembly: AssemblyVersion("1.19.0.0")]
[assembly: AssemblyFileVersion("1.19.0.0")]
[assembly: AssemblyInformationalVersion("1.19.0-update-eagle.1+1730.Branch.update-eagle.Sha.336adabb2768d57b682a05c2ced9d3a94422cac3")]




