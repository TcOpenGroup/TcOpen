#define UNDER_TESTING
using System;
using System.Windows;

namespace TcOpen.Inxton.Security.Wpf.Internal
{
    public class ActionRunner
    {

        private static ActionRunner _actionRunner;

        public static ActionRunner Runner
        {
            get
            {
                if (_actionRunner == null)
                {
                    _actionRunner = new ActionRunner();
                }

                return _actionRunner;
            }
        }


        private ActionRunner() { }

        public void ReportException(Exception exception)
        {
            MessageBox.Show(exception.Message, "Unfortunately we cannot do this.", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        private bool DefaulApprove(object parameter = null)
        {
            return true;
        }

        public delegate bool UserApprovedActionDelegate();

        public void Execute(Action action, string info = "", UserApprovedActionDelegate userApproval = null)
        {
            try
            {

#if UNDER_TESTING
                if (userApproval != null)
                {
                    if (userApproval())
                    {
                        action();
                        Journaling.Journal.LogUserActionEvent($"{GetCaller()} : '{info}'");
                    }
                }

                else
#endif
                {
                    action();
                    Journaling.Journal.LogUserActionEvent($"{GetCaller()} : '{info}'");
                }


            }
            catch (Exception ex)
            {
                ReportException(ex);
            }
        }

        private static string GetCaller(int level = 2)
        {
            var m = new System.Diagnostics.StackTrace().GetFrame(level).GetMethod();

            // .Name is the name only, .FullName includes the namespace
            var className = m.DeclaringType.FullName;

            //the method/function name you are looking for.
            var methodName = m.Name;

            //returns a composite of the namespace, class and method name.
            return className + "->" + methodName;
        }
    }
}
