using System;
using System.Collections.Generic;
using System.Text;

namespace TcOpen.Inxton.Instructor
{
    public delegate void ChangeInstructionDelegate(string Key);

    public interface IInstructionControlProvider
    {        
        IEnumerable<InstructionItem> InstructionSteps { get; }

        string ProviderId { get; }

        void UpdateTemplate();        

        ChangeInstructionDelegate ChangeInstruction { get; set; }
    } 
}
