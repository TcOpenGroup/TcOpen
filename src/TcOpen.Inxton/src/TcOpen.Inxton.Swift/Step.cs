using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;

namespace TcOpen.Inxton.Swift
{
    public class Step
    {
        public Step(IVortexElement origin)
        {
            Origin = origin;
        }

        public IVortexElement Origin { get; }

        readonly List<string> statements = new List<string>();
        public IEnumerable<string> Statements => statements;
        public string AddStatement(string operand)
        {            
            statements.Add(operand);
            return operand;
        }

        public StringBuilder EmitCode(StringBuilder sb, int stepId)
        {
            sb.Append($"\n\nIF Step({stepId * 10},\nTRUE,\n'{Origin?.Symbol}') THEN\n" +
                          $"//-------------------------------------------------------\n");
            sb.Append("\tIF(");

            for (int i = 0; i < Statements.Count(); i++)
            {                
                sb.Append($"{Statements.ElementAt(i)}");
            }
            sb.Append(")THEN\n");
            sb.Append("\t\tStepCompleteWhen(TRUE);\n");
            sb.Append("\tEND_IF;\n");

            sb.Append($"//-------------------------------------------------------" +
                         $"\nEND_IF;");
            return sb;
        }
    }    
}
