using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Swift.TcAdapter.TcProject
{
    public class TcProjectAdapter
    {
        public TcProjectAdapter()
        {
            
        }

        public TcPlcObject PlcBlock { get; } = new TcPlcObject();

        public void SavePlcBlock(string fileName)
        {
            using(var sr = new System.IO.StreamWriter(fileName))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(PlcBlock.GetType());
                serializer.Serialize(sr, PlcBlock);
            }
        }
    }


}
