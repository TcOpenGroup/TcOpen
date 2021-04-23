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

        //TcoContext
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

        //TcoSequencerAutoRestorable
        public static void AddEmptyCycle(this ITestTcoSequencerAutoRestorable sequencer)
        {
            sequencer.ContextOpen();
            sequencer.ContextClose();
        }
        public static void SingleCycleRun(this ITestTcoSequencerAutoRestorable sequencer, Action action)
        {
            sequencer.ContextOpen();
            action();
            sequencer.ContextClose();
        }

        public static void SequencerSingleCycleRun(this ITestTcoSequencerAutoRestorable sequencer, Action action)
        {
            sequencer.ContextOpen();
            sequencer.SequencerOpen();
            action();
            sequencer.SequencerClose();
            sequencer.ContextClose();
        }
        
        public static void SequencerMultipleCyclesRun(this ITestTcoSequencerAutoRestorable sequencer, Action action, ushort cycles)
        {
            ushort i = 0;
            while (i < cycles)
            {
                sequencer.ContextOpen();
                sequencer.SequencerOpen();
                action();
                sequencer.SequencerClose();
                sequencer.ContextClose();
                i++;
            }
        }

        public static void SequencerRunUntilEndConditionIsMet(this ITestTcoSequencerAutoRestorable sequencer, Action action, Func<bool> endCondition)
        {
            while (!endCondition())
            {
                sequencer.ContextOpen();
                sequencer.SequencerOpen();
                action();
                sequencer.SequencerClose();
                sequencer.ContextClose();
            }
        }

        public static void SequencerRunUntilActionDone(this ITestTcoSequencerAutoRestorable sequencer, Func<bool> action)
        {
            bool actionDone = false;
            while (!actionDone)
            {
                sequencer.ContextOpen();
                sequencer.SequencerOpen();
                action();
                sequencer.SequencerClose();
                sequencer.ContextClose();
            }
        }

        //TcoSequencerNonAutoRestorable
        public static void AddEmptyCycle(this ITestTcoSequencerNonAutoRestorable sequencer)
        {
            sequencer.ContextOpen();
            sequencer.ContextClose();
        }
        public static void SingleCycleRun(this ITestTcoSequencerNonAutoRestorable sequencer, Action action)
        {
            sequencer.ContextOpen();
            action();
            sequencer.ContextClose();
        }

        public static void SequencerSingleCycleRun(this ITestTcoSequencerNonAutoRestorable sequencer, Action action)
        {
            sequencer.ContextOpen();
            sequencer.SequencerOpen();
            action();
            sequencer.SequencerClose();
            sequencer.ContextClose();
        }

        public static void SequencerMultipleCyclesRun(this ITestTcoSequencerNonAutoRestorable sequencer, Action action, ushort cycles)
        {
            ushort i = 0;
            while (i < cycles)
            {
                sequencer.ContextOpen();
                sequencer.SequencerOpen();
                action();
                sequencer.SequencerClose();
                sequencer.ContextClose();
                i++;
            }
        }

        public static void SequencerRunUntilEndConditionIsMet(this ITestTcoSequencerNonAutoRestorable sequencer, Action action, Func<bool> endCondition)
        {
            while (!endCondition())
            {
                sequencer.ContextOpen();
                sequencer.SequencerOpen();
                action();
                sequencer.SequencerClose();
                sequencer.ContextClose();
            }
        }

        public static void SequencerRunUntilActionDone(this ITestTcoSequencerNonAutoRestorable sequencer, Func<bool> action)
        {
            bool actionDone = false;
            while (!actionDone)
            {
                sequencer.ContextOpen();
                sequencer.SequencerOpen();
                action();
                sequencer.SequencerClose();
                sequencer.ContextClose();
            }
        }



        
        //TcoMessengerContext
        public static void AddEmptyCycle(this ITestTcoMessengerContext context)
        {
            context.ContextOpen();
            context.ContextClose();
        }

        public static void SingleCycleRun(this ITestTcoMessengerContext context, Action action)
        {
            context.ContextOpen();
            action();
            context.ContextClose();
        }
        public static void MultipleCycleRun(this ITestTcoMessengerContext context, Action action, ushort cycles)
        {
            ushort i = 0;
            while (i < cycles)
            {
                context.ContextOpen();
                action();
                context.ContextClose();
                i++;
            }
        }

        public static void RunUntilEndConditionIsMet(this ITestTcoMessengerContext context, Action action, Func<bool> endCondition)
        {
            while (!endCondition())
            {
                context.ContextOpen();
                action();
                context.ContextClose();
            }
        }

        public static void RunUntilActionDone(this ITestTcoMessengerContext context, Func<bool> action)
        {
            bool actionDone = false;
            while (!actionDone)
            {
                context.ContextOpen();
                actionDone = action();
                context.ContextClose();
            }
        }

        //TcoComponent
        public static void AddEmptyCycle(this ITestTcoComponent context)
        {
            context.ContextOpen();
            context.ContextClose();
        }

        public static void SingleCycleRun(this ITestTcoComponent context, Action action)
        {
            context.ContextOpen();
            action();
            context.ContextClose();
        }
        public static void MultipleCycleRun(this ITestTcoComponent context, Action action, ushort cycles)
        {
            ushort i = 0;
            while (i < cycles)
            {
                context.ContextOpen();
                action();
                context.ContextClose();
                i++;
            }
        }

        public static void RunUntilEndConditionIsMet(this ITestTcoComponent context, Action action, Func<bool> endCondition)
        {
            while (!endCondition())
            {
                context.ContextOpen();
                action();
                context.ContextClose();
            }
        }

        public static void RunUntilActionDone(this ITestTcoComponent context, Func<bool> action)
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
