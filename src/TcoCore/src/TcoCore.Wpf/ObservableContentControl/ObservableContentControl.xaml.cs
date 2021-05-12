using System.Windows;
using System.Windows.Controls;
using Vortex.Presentation.Wpf;

namespace Tco.Wpf
{
    public partial class ObservableContentControl : UserControl
    {
        public ObservableContentControl()
        {
            InitializeComponent();
            DataContextChanged += DataChanged;
        }

        public object RenderedObject { get; set; }
        private void DataChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
                RenderedObject = e.NewValue;
            Rerender.Content = Renderer.Get.CreatePresentation(PresentationType, RenderedObject);
        }

        public string PresentationType
        {
            get { return (string)GetValue(PresentationTypeProperty); }
            set { SetValue(PresentationTypeProperty, value); }
        }

        public static readonly DependencyProperty PresentationTypeProperty =
            DependencyProperty.Register(
                nameof(PresentationType),
                typeof(string),
                typeof(ObservableContentControl),
                new PropertyMetadata("Display", PresentationTypeChanged));

        private static void PresentationTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var @this = (d as ObservableContentControl);
            @this.DataChanged(d, new DependencyPropertyChangedEventArgs(PresentationTypeProperty, null, null));
        }
    }
}
