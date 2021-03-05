using Tco.Wpf.DynamicTree.DataTemplates;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Vortex.Connector;

namespace Tco.Wpf.CustomTree
{
    /// <summary>
    /// Interaction logic for CustomTree.xaml
    /// </summary>
    public partial class CustomTree : UserControl, IDragConsumer
    {
        public CustomTree()
        {
            InitializeComponent();

        }

        private CustomTreeViewModel _dataContext { get => DataContext.As<CustomTreeViewModel>(); }

        public void DragDrop(object sender, DragEventArgs dragArgs)
        {
            dragArgs
                 .GetFrom<ValueTag>()
                 .TryAs<IValueTag>()
                 ?.Let(_dataContext.AddNewItemCommand.Execute);

            dragArgs
                 .GetFrom<VortexObjectDataTemplate>()
                 .TryAs<IVortexObject>()
                 ?.Let(_dataContext.AddNewGroupCommand.Execute);
        }

      

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Closing += window_Closing;
            RetrieveTree();
        }

        private void RetrieveTree() => this
            .As<CustomTree>()
            .DataContext
            .As<CustomTreeViewModel>()
            .RetrieveTreeCommand
            .Execute(Tag);
        

        private void window_Closing(object sender, CancelEventArgs e)=> this
            .As<CustomTree>()
            .DataContext
            .As<CustomTreeViewModel>()
            .PersistTreeCommand
            .Execute(null);

        void IDragConsumer.DragEnter(object sender, DragEventArgs e)
        {
            //throw new System.NotImplementedException();
        }

    }
}

