using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Swift
{
    public class Step
    {
        public Step()
        {

        }

        readonly List<string> statements = new List<string>() ;
        public IEnumerable<string> Statements => statements;
        public string AddStatement(string operand)
        {            
            statements.Add(operand);
            return operand;
        }
    }    
}
