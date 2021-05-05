using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Vortex.Presentation.Wpf;

namespace TcoCore
{

    public class TaskDetailsView : UserControl
    {

    }

    public class TcoTaskViewModel : RenderableViewModel
    {
        public TcoTaskViewModel()
        {
            OpenDetailsDialog = new RelayCommand(action => OpenTaskDialogue());
            RestoreCommand = new RelayCommand(action => this.TcoTask._restoreRequest.Cyclic = true);         
        }

        object taskDetailContent;
        public object TaskDetailContent
        {
            get => taskDetailContent;
            set
            {
                if (taskDetailContent == value)
                {
                    return;
                }

                SetProperty(ref taskDetailContent, value);
            }
        }

        bool isTaskDetailDialogueOpened;
        public bool IsTaskDetailDialogueOpened
        {
            get => isTaskDetailDialogueOpened; set
            {
                if (isTaskDetailDialogueOpened == value)
                {
                    return;
                }

                SetProperty(ref isTaskDetailDialogueOpened, value);
            }
        }

        private void OpenTaskDialogue()
        {
            TaskDetailContent = new TaskDetailsView();
            IsTaskDetailDialogueOpened = true;
        }

        public TcoTask TcoTask { get; private set; }

        public override object Model { get => TcoTask; set => TcoTask = value as TcoTask; }

        public RelayCommand RestoreCommand { get; }   
        
        public RelayCommand OpenDetailsDialog { get; }
    }
}
