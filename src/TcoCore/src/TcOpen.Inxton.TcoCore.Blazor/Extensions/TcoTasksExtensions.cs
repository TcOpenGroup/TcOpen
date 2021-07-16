using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoCore;

namespace TcOpen.Inxton.TcoCore.Blazor
{
    public static class TcoTasksExtensions
    {
        public static string StateToButtonClass(this TcoTask task)
        {
            var state = (eTaskState)((short)task._taskState.Synchron);
            switch (state)
            {
                case eTaskState.Ready:
                    return "btn btn-primary";
                case eTaskState.Done:
                    return "btn btn-primary";
                case eTaskState.Busy:
                case eTaskState.Requested:
                    return "btn btn-primary active";
                case eTaskState.Error:
                    return "btn btn-danger";
                default:
                    break;
            }

            return "btn btn-secondary";
        }
    }
}
