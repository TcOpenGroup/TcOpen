using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoCoreTests;

namespace TcoCoreUnitTests
{
    public static class TestRunners
    {
        public static void AddEmptyCycle(this ITestTcoContext context)
        {
            context.ContextOpen();
            context.ContextClose();
        }
        public static void SingleCycleRun(this ITestTcoContext context, Action action)
        {
            context.ContextOpen();
            action();
            context.ContextClose();
        }
        public static void MultipleCycleRun(this ITestTcoContext context, Action action, ushort cycles)
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

        public static void RunUntilEndConditionIsMet(this ITestTcoContext context, Action action, Func<bool> endCondition)
        {
            while (!endCondition())
            {
                context.ContextOpen();
                action();
                context.ContextClose();
            }
        }

        public static void RunUntilActionDone(this ITestTcoContext context, Func<bool> action)
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
