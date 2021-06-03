using System;
using System.Threading.Tasks;

namespace TcoCore
{
    /// <summary>
    /// Partial class exposes Open and Close context methods for testing purposes.
    /// </summary>
    public partial class _internals_TcoContext : Testing.ITestContext
    {
        /// <summary>
        /// Opens the context from testing application.
        /// </summary>
        /// <returns></returns>
        public bool ContextOpen()
        {
            this._Open();
            return true;
        }

        /// <summary>
        /// Closes the context form testing application.
        /// </summary>
        /// <returns></returns>
        public bool ContextClose()
        {
            this._Close();
            return true;
        }  
        
        /// <summary>
        /// Executes context cycle given number of times. Optionally can provide <see cref="_testId"/> for test code isolation.
        /// In the plc you must ensure cyclical call of <see cref="TcoCore.TcoContext.PlcTcoContext.ProbeRun"/> method.
        /// </summary>
        /// <param name="counts">Number of time the context cycle executes</param>
        /// <param name="testId">Test Id</param>
        public void ExecuteProbeRun(ulong counts, ulong testId)
        {
            _probeCurrentCycleCount.Synchron = ulong.MaxValue;
            _testId.Synchron = testId;
            _probeRunRequiredCycles.Synchron = counts;
            _probeCurrentCycleCount.Synchron = 0;

            Task.Run(() => { while (_probeCurrentCycleCount.Synchron < _probeRunRequiredCycles.Synchron) System.Threading.Thread.Sleep(5); }).Wait();            
        }

        /// <summary>
        /// Executes context cycle until end condition is met. Optionally can provide <see cref="_testId"/> for test code isolation.
        /// In the plc you must ensure cyclical call of <see cref="TcoCore.TcoContext.PlcTcoContext.ProbeRun"/> method.
        /// </summary>       
        /// <param name="testId">Test Id</param>
        /// <param name="endCondition">Evaluates end condition for the test.</param>
        public void ExecuteProbeRun(ulong testId, Func<bool> endCondition)
        {
            _probeCurrentCycleCount.Synchron = ulong.MaxValue;
            _testId.Synchron = testId;
            _probeRunRequiredCycles.Synchron = ulong.MaxValue;
            _probeCurrentCycleCount.Synchron = 0;

            Task.Run(() => { while (!endCondition()); System.Threading.Thread.Sleep(5); }).Wait();

            _probeRunRequiredCycles.Synchron = 0;
            _testId.Synchron = 0;
        }
    }
}
