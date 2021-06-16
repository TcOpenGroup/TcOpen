using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Swift.TcAdapter.TcProject
{
    public class PlcBlockFactory
    {
        const string EMPTY_CODE_SNIPPET = "![CDATA[]]";

        public static TcPlcObject CreateSequencerPlcBlock(string blockName, string mainImplementation)
        {          
            var plcObject = new TcPlcObject()
            {
                Version = "1.1.0.1",
                ProductVersion = "3.1.4024.9",
                POU = new POU()
                {
                    Name = blockName,
                    SpecialFunc = "None",
                    Implementation = new Implementation() { ST = string.Empty },
                    Declaration = $"FUNCTION_BLOCK {blockName} EXTENDS TcoCore.TcoSequencer",
                    Id = "{" + $"{Guid.NewGuid()}" + "}",
                    Method = new List<Method>()
                                  {
                                      new Method()
                                      {
                                          Name = "Main",
                                          Id = "{" + $"{Guid.NewGuid()}" + "}",
                                          Declaration = "METHOD PROTECTED Main : BOOL",
                                          Implementation = new Implementation()
                                          {
                                              ST = $"{mainImplementation}"
                                          }
                                      }
                                  }
                }
            };

            return plcObject;
        }

        public static void EmitPlcBlockFile(string fileName, TcPlcObject obj)
        {
            using (var sw = new System.IO.StreamWriter(fileName))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(TcPlcObject));
                serializer.Serialize(sw, obj);
            }
        }

        //public static TcPlcObject ReadPlcBlockFile(string fileName)
        //{
        //    using (var sr = new System.IO.StreamReader(fileName))
        //    {
        //        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(TcPlcObject));
        //        return serializer.Deserialize(sr) as TcPlcObject;
        //    }
        //}
    }
}
