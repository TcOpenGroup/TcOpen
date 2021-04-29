using PlcAppExamples;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;
using Vortex.Presentation.Wpf;

namespace PlcAppExamples
{
    public partial class Station001View : UserControl
    {
        public Station001View()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Station001ViewModel viewModel)
                UpdateImage(viewModel);
        }

        private void UpdateImage(Station001ViewModel viewModel)
        {
            viewModel.Station001._sequence._currentStep.ID.Subscribe(ChangeImage);
        }

        FileInfo rootPath = new FileInfo(Assembly.GetExecutingAssembly().Location);

        private void ChangeImage(IValueTag sender, ValueChangedEventArgs args)
        {
           var scrollPosition = new Dictionary<short, double>() {
            {100,0.0},
            {200,220},
            {300,524},
            {400,819},
            {500,1011},
            {600,1203},
            {700,1395},
            {800,1504}
         };
            var fileName = $@"{rootPath.DirectoryName}\Tutorial\401_ConnectingDots\Images\100-800.png";
            if (File.Exists(fileName))
            {
                this.Dispatcher.Invoke(() =>
                {
                    if (scrollPosition.ContainsKey((dynamic)args.NewValue))
                    {
                        scrollImage.ScrollToVerticalOffset(scrollPosition[(dynamic)args.NewValue]);
                    }
                });
            }
        }
    }

    public class Station001ViewModel : RenderableViewModel
    {
        public Station001 Station001 { get; set; }
        public override object Model { get => Station001; set => Station001 = value as Station001; }
    }
}
