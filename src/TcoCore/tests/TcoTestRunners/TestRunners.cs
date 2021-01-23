using System;

namespace TcoTestRunners
{
    public static class TestRunners
    {
        public static void SingleCycleRun(this ITestContext context, Action action)
        {
            context.ContextOpen();
            action();
            context.ContextClose();
        }

        public static void RunUntilEndConditionIsMet(this ITestContext context, Action action, Func<bool> endCondition)
        {
            while (!endCondition())
            {
                context.ContextOpen();
                action();
                context.ContextClose();
            }
        }

        public static void RunUntilActionDone(this ITestContext context, Func<bool> action)
        {
            bool actionDone = false;
            while (!actionDone)
            {
                context.ContextOpen();
                actionDone = action();
                context.ContextClose();
            }
        }
    }

}
