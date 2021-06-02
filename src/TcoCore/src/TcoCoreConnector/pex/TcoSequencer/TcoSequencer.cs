using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TcoCore.Threading;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace TcoCore
{
    public partial class TcoSequencer
    {
        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            this._modeController._changeMode.LogPayloadDecoration = () => LogStepDetailsInfo();
            this._modeController._stepBackward.LogPayloadDecoration = () => LogStepDetailsInfo();
            this._modeController._stepForward.LogPayloadDecoration = () => LogStepDetailsInfo();
            this._modeController._stepIn.LogPayloadDecoration = () => LogStepDetailsInfo();
        }

        private object LogStepDetailsInfo()
        {
             var currentStepInfo = this._currentStep.CreatePlainerType();
             currentStepInfo.CopyCyclicToPlain(this._currentStep);
             return currentStepInfo;
        }
    }    
}
