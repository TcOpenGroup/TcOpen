namespace Tc.Prober.Runners
{
    using System;
    using Vortex.Connector;

    /// <summary>
    /// Contains series of helper methods for executing RPC plc methods.
    /// </summary>
    public static class TaskRunners
    {       
        /// <summary>
        /// Runs the test method until returns true.
        /// </summary>
        /// <typeparam name="T">Type of subject under test. Must be of <see cref="IVortexObject"/></typeparam>
        /// <param name="sut">Subject under test.</param>
        /// <param name="testMethod"></param>
        public static void Run<T>(this T sut, Func<T, bool> testMethod) where T : IVortexObject
        {
            while (!testMethod(sut));
        }

        /// <summary>
        /// Runs the unit test given number of times.
        /// </summary>
        /// <typeparam name="T">Type of subject under test. Must be of <see cref="IVortexObject"/></typeparam>
        /// <typeparam name="R">Type of return value.</typeparam>
        /// <param name="sut">Subject under test</param>
        /// <param name="testMethod">Test method.</param>
        /// <param name="numberOfRuns">Number of runs.</param>
        /// <returns>Return value of the test method</returns>
        public static R Run<T, R>(this T sut, Func<T, R> testMethod, int numberOfRuns) where T : IVortexObject
        {
            object ret = null;
            for (int i = 0; i < numberOfRuns; i++)
            {
                ret = testMethod(sut);
            }

            return (R)ret;
        }       
    }
}
