using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoCore;

namespace TcoCore
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

        public static bool GetTaskState(this TcoTask task)
        {
            try
            {
                var taskState = (eTaskState)((short)task._taskState.Synchron);

                switch (taskState)
                {
                    case eTaskState.Requested:
                    case eTaskState.Busy:
                        return true;
                    case eTaskState.Ready:
                    case eTaskState.Error:
                    case eTaskState.Done:
                    default:
                        return false;
                }
            }
            catch (Exception)
            {
                // Swallow
            }

            return false;
        }

        public static bool TaskStateToVisibility(this TcoTask task)
        {
            try
            {
                var taskState = (eTaskState)((short)task._taskState.Synchron);

                switch (taskState)
                {
                    case eTaskState.Requested:
                    case eTaskState.Busy:
                    case eTaskState.Error:
                        return true;
                    case eTaskState.Ready:
                    case eTaskState.Done:
                        return false;
                    default:
                        return false;
                }
            }
            catch (Exception)
            {
                // Swallow
            }

            return false;
        }
    }
}
