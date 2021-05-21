# Colors

When it comes to theming and colors of WPF applications, we rely on [MaterialDesignInXamlToolkit](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit).

It's very well explained here
- https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/wiki/Brush-Names
- https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/wiki/Advanced-Theming
- https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/wiki/Custom-Palette-Hues

## How to use 

In your `App.xaml` add this dictionary.

```xml
<Application
    ...
    xmlns:tcocore="clr-namespace:TcoCore.Wpf;assembly=TcoCore.Wpf"
    ...
    />
...
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <materialDesign:BundledTheme
                    BaseTheme="Inherit"
                    ColorAdjustment="{materialDesign:ColorAdjustment}"
                    PrimaryColor="BlueGrey"
                    SecondaryColor="LightGreen" />
                <ResourceDictionary Source="pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml" />
                <tcocore:TcoResources />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
```

The `materialDesign:BundledTheme` is a MatrialDesignXaml theme.

Second `ResourceDictionary` contains resources for the theme.

`TcoResources` is a resource dictionary generated from `TcoCore.Wpf.TcoColors`.  Adding this resource dictionary to your project will enable you to use static brushes defined in `TcoCore.Wpf.TcoColors`  like this
```xml
  <Button
            Background="Transparent"
            Command="{Binding TcoTask.Restore}"
            Foreground="{DynamicResource Error}" />
```

The `Foreground` is referring to the Error brush defined in `TcoCore.Wpf.TcoColors`.

Since these properties are static you can also acces them in codebehind, converters or anywhere you need them using `TcoColors` as an entry point. ie `TcoColors.Primary`.


