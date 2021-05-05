using System;
using TcoDrivesBeckhoffTests;

namespace TcoDrivesBeckhoffUnitTests
{
    public static class TestRunners
    {

        public static void AddEmptyCycle(this ITestTcoDriveSimpleTestContext context)
        {
            context.ContextOpen();
            context.ContextClose();
        }

        public static void SingleCycleRun(this ITestTcoDriveSimpleTestContext context, Action action)
        {
            context.ContextOpen();
            action();
            context.ContextClose();
        }
        public static void MultipleCycleRun(this ITestTcoDriveSimpleTestContext context, Action action, ushort cycles)
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

        public static void RunUntilEndConditionIsMet(this ITestTcoDriveSimpleTestContext context, Action action, Func<bool> endCondition)
        {
            while (!endCondition())
            {
                context.ContextOpen();
                action();
                context.ContextClose();
            }
        }

        public static void RunUntilActionDone(this ITestTcoDriveSimpleTestContext context, Func<bool> action)
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
