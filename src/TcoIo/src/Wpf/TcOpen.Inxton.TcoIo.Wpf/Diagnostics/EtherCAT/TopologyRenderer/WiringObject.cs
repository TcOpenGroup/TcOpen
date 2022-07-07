using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace TcoIo
{
    public class WiringObject
    {
        public enum ConectionType
        {
            K2K,        //direct E-bus connection to the previous E-Bus terminal (no wire)
            Y2Y,        //direct EtherCAT connection to the previous EtherCAT device of type Y, KY or YY (wire shape ----)
            Y2YKY,      //EtherCAT connection to the previous EtherCAT device of type YKY (wire shape C)
            Y2KYKY_X1,  //EtherCAT connection to the previous EtherCAT device of type KYKY X1 (wire shape L)
            Y2KYKY_X2,  //EtherCAT connection to the previous EtherCAT device of type KYKY X2 (wire shape L)
        }

        private ConectionType wiringType;
        public ConectionType WiringType
        {
            get { return this.wiringType; }
            set { this.wiringType = value ; }
        }


        private Line line;
        public Line Line
        {
            get { return this.line ?? new Line(); }
            set { this.line = value ?? new Line(); }
        }


        public WiringObject()
        {
            WiringType = ConectionType.K2K;
            Line = new Line();
        }

    }
}
