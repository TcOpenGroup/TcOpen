using System;
using TcoDrivesBeckhoffTests;

namespace TcoDrivesBeckhoffUnitTests
{
    public static class TestRunners
    {

        public static void AddEmptyCycle(this ITestTcoDrivesBeckhoffContext context)
        {
            context.ContextOpen();
            context.ContextClose();
        }

        public static void SingleCycleRun(this ITestTcoDrivesBeckhoffContext context, Action action)
        {
            context.ContextOpen();
            action();
            context.ContextClose();
        }
        public static void MultipleCycleRun(this ITestTcoDrivesBeckhoffContext context, Action action, ushort cycles)
        {
            ushort i = 0;
            while(i < cycles)
            {
                context.ContextOpen();
                action();
                context.ContextClose();
                i++;
            }
        }

        public static void RunUntilEndConditionIsMet(this ITestTcoDrivesBeckhoffContext context, Action action, Func<bool> endCondition)
        {
            while (!endCondition())
            {
                context.ContextOpen();
                action();
                context.ContextClose();
            }
        }

        public static void RunUntilActionDone(this ITestTcoDrivesBeckhoffContext context, Func<bool> action)
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
