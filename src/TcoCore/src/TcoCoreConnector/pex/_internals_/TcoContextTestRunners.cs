using System;

namespace TcoCore.Testing
{
    /// <summary>
    /// Series of extension method for execution action within a `TcoContext`.
    /// </summary>
    public static class TcoContextTestRunners
    {
        /// <summary>
        /// [Call from external environment] Runs the remote action once.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="action">Action to run.</param>
        public static void Run(this ITestContext context, Action action)
        {
            context.ContextOpen();
            action();
            context.ContextClose();
        }

        /// <summary>
        /// [Call from external environment] Runs with cycle with open-action-close context.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="action">Acton to run</param>
        /// <param name="endCondition">End condition. When `true` cycle stops.</param>
        public static void Run(this ITestContext context, Action action, Func<bool> endCondition)
        {
            while (!endCondition())
            {
                context.ContextOpen();
                action();
                context.ContextClose();
            }
        }

        /// <summary>
        /// [Call from external environment] Runs with cycle with open-action-close context.
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="action">Action to run, when returns `true` cycle stops.</param>
        public static void Run(this ITestContext context, Func<bool> action)
        {
            bool actionDone = false;
            while (!actionDone)
            {
                context.ContextOpen();
                actionDone = action();
                context.ContextClose();
            }
        }

        /// <summary>
        /// [Executes call on PLC task] Executes with cycle with open-action-close context. 
        /// Make sure you call `_internals_TcoContext_.ProbeRun` instead of `Run` method in your PLC program. (<seealso cref="_internals_TcoContext.ExecuteProbeRun(ulong, ulong)"/>)
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="action">Action to run, when returns `true` cycle stops.</param>
        public static void Run(this _internals_TcoContext context, ulong numberOfCycles = 1, ulong testId = 0)
        {
            context.ExecuteProbeRun(numberOfCycles, testId);          
        }
    }
}
