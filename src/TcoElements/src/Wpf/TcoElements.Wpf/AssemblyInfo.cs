using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Markup;
using Vortex.Presentation.Wpf;

[assembly:RenderableAssembly]
[assembly: XmlnsPrefix("http://vortex.mts/xaml", "vortex")]
[assembly: XmlnsDefinition("http://vortex.mts/xaml", "TcoElements")]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]
