using inxton.vortex.framework.dynamictreeview.wpf.sandbox;
using Tco.Wpf.DynamicTree.DataTemplates;
using System.Windows;
using Vortex.Connector;

namespace Tco.Wpf.CustomTree.DataTemplates
{
    public partial class TreeGroupTemplate : IDragConsumer
    {
        private TreeGroup DataContext { get; set; }
        public void DragDrop(object sender, DragEventArgs dragArgs)
        {
            DataContext = sender.As<FrameworkElement>().DataContext.As<TreeGroup>();

            dragArgs
                .GetFrom<ValueTag>()
                .TryAs<IValueTag>()
                ?.Let(AddNewItem);

            dragArgs
                .GetFrom<VortexObjectDataTemplate>()
                .TryAs<TreeWrapperObject>()
                ?.Let(AddNewItem);

            dragArgs
                .GetFrom<TreeItemTemplate>()
                .TryAs<IValueTag>()
                ?.Let(AddToFavourites);

            dragArgs.Handled = true;
        }

        private void AddToFavourites(IValueTag valueTag) => DataContext.FavouriteCommand.Execute(valueTag);

        private void AddNewItem(IValueTag tag) => DataContext.AddItemCommand.Execute(parameter: tag);

        private void AddNewItem(TreeWrapperObject vortexObject) => DataContext.AddGroupCommand.Execute(parameter: new TreeGroup(vortexObject));


        public void DragEnter(object sender, DragEventArgs e)
        {
            ;
        }

    }
}
