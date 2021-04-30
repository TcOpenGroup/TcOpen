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


[assembly: XmlnsPrefix("http://inxton.mts/xaml", "inxton")]
[assembly: XmlnsDefinition("http://inxton.mts/xaml", "TcoCore")]
[assembly: XmlnsDefinition("http://inxton.mts/xaml", "Tco.Wpf")]

[assembly:Vortex.Presentation.Wpf.RenderableAssembly()]
[assembly: AssemblyVersion("0.4.0.0")]
[assembly: AssemblyFileVersion("0.4.0.0")]
[assembly: AssemblyInformationalVersion("0.4.0-initial-dev.333+Branch.initial-dev.Sha.6f6b2d446fdaad6ca830f6d6555024096e4ee36e")]


