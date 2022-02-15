using TcoCore;
using TcoInspectors;

namespace TcOpen.Inxton.TcoInspectors.Wpf
{
    public class InspectorProxyService
    {
        public InspectorProxyService(TcoObject tcoObject)
        {
            foreach (var inspector in tcoObject.GetDescendants<TcoInspector>())
            {
                inspector._dialogueTask.Initialize(() => Open(inspector));
            } 
        }

        public void Open(TcoInspector inspector)
        {
            TcoAppDomain.Current.Dispatcher.Invoke(
                () =>
                    {
                        var gialogue = new InspectorDialogueWindow();
                        gialogue.ShowDialog();
                    }
                );
        }
    }
}
