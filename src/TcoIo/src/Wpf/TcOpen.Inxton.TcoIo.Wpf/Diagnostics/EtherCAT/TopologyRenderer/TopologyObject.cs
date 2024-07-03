using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace TcoIo
{
    public class TopologyObject
    {
        private string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.name = value;
                }
            }
        }

        private string physics;
        public string Physics
        {
            get { return this.physics; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.physics = value;
                }
            }
        }

        private string connection;
        public string Connection
        {
            get { return this.connection; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.connection = value;
                }
            }
        }

        private int row;
        public int Row
        {
            get { return this.row; }
            set { this.row = value; }
        }

        private int column;
        public int Column
        {
            get { return this.column; }
            set { this.column = value; }
        }

        private double pos_X;
        public double Pos_X
        {
            get { return this.pos_X; }
            set { this.pos_X = value; }
        }

        private double pos_Y;
        public double Pos_Y
        {
            get { return this.pos_Y; }
            set { this.pos_Y = value; }
        }

        private UniformGrid hardware;
        public UniformGrid Hardware
        {
            get { return this.hardware ?? new UniformGrid(); }
            set { this.hardware = value ?? new UniformGrid(); }
        }

        private WiringObject wiring;
        public WiringObject Wiring
        {
            get { return this.wiring ?? new WiringObject(); }
            set { this.wiring = value ?? new WiringObject(); }
        }

        public TopologyObject()
        {
            Name = "";
            Physics = "";
            Connection = "";
            Row = 0;
            Column = 0;
            Pos_X = 0;
            Pos_Y = 0;
            Hardware = new UniformGrid();
            Wiring = new WiringObject();
        }

        public TopologyObject(
            string name,
            string physics,
            string connection,
            int row,
            int column,
            double pos_X,
            double pos_Y
        )
        {
            Name = name;
            Physics = physics;
            Connection = connection;
            Row = row;
            Column = column;
            Pos_X = pos_X;
            Pos_Y = pos_Y;
            Hardware = new UniformGrid();
            Wiring = new WiringObject();
        }

        public TopologyObject(
            string name,
            string physics,
            string connection,
            int row,
            int column,
            double pos_X,
            double pos_Y,
            UniformGrid harwdware
        )
        {
            Name = name;
            Physics = physics;
            Connection = connection;
            Row = row;
            Column = column;
            Pos_X = pos_X;
            Pos_Y = pos_Y;
            Hardware = harwdware;
            Wiring = new WiringObject();
        }

        public TopologyObject(
            string name,
            string physics,
            string connection,
            int row,
            int column,
            double pos_X,
            double pos_Y,
            UniformGrid harwdware,
            WiringObject wiring
        )
        {
            Name = name;
            Physics = physics;
            Connection = connection;
            Row = row;
            Column = column;
            Pos_X = pos_X;
            Pos_Y = pos_Y;
            Hardware = harwdware;
            Wiring = wiring;
        }
    }
}
