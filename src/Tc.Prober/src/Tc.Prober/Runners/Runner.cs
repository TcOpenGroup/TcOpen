namespace Tc.Prober.Recorder
{
    using System;    
    using System.Diagnostics;
    using System.IO;    
    using System.Runtime.CompilerServices;    
    using Vortex.Connector;

    /// <summary>
    /// Series of extension methods that are able to run the test with recording ability.
    /// </summary>
    public static class Runner
    {       
        /// <summary>
        /// Gets or sets the directory where the recodings are strored.
        /// </summary>
        public static string RecordingsShell { get; set; }

        /// <summary>
        /// Examines the stack and retrieves name of the method at the given level.
        /// </summary>
        /// <param name="frame">Stack frame from which to retrieve the method name.</param>
        /// <returns>Method name.</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string CallerMethodName(int frame = 1)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(frame);

            return sf.GetMethod().Name;
        }

        /// <summary>
        /// Gets the name of the method from the given level on the call stack.
        /// </summary>
        /// <param name="level">Stack level</param>
        /// <returns>Method name</returns>
        public static string GetRecordingFilePathWithMethodName(int level = 2)
        {
            return Path.Combine(RecordingsShell, $"{CallerMethodName(level)}.json");
        }
    

        private static string GetAutoName()
        {
            return Path.Combine(RecordingsShell, $"{CallerMethodName(3)}.json");
        }
        
        /// <summary>
        /// Runs test with recording/replaying.
        /// </summary>
        /// <typeparam name="T">Test method return type.</typeparam>
        /// <param name="sut">Subject under test.</param>
        /// <param name="action">Action to be performed (typically test method)</param>
        /// <param name="done">Indicates that the test has finished.</param>
        /// <param name="openCycle">What should be called prior to <see cref="action"/>.</param>
        /// <param name="closeCycle">What should be called after <see cref="action"/>.</param>
        /// <param name="recorder">Instance of the recorder/player.</param>
        /// <param name="recordingFileName">Name of the recording file</param>
        /// <returns>Last result of the <see cref="action"/></returns>
        public static T Run<T>(this IVortexElement sut, 
                                   Func<T> action,
                                   Func<bool> done,                                   
                                   Action openCycle = null,
                                   Action closeCycle = null,                                   
                                   IRecorder recorder = null,
                                   string recordingFileName = null
                                   )
                            
        {                               
            T retVal = default(T);

            recorder?.Begin(recordingFileName);

            while (!done())
            {
                recorder?.Act();
                openCycle?.Invoke();
                retVal = action();
                closeCycle?.Invoke();
                recorder?.Act();
            }

            recorder?.Act();

            recorder?.End(recordingFileName);

            return retVal;
        }

        
    }
}
