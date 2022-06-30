using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.TcoIo.Wpf
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
            set
            {
                this.row = value;
            }
        }

        private int column;
        public int Column
        {
            get { return this.column; }
            set
            {
                this.column = value;
            }
        }
        public TopologyObject()
        {
            Name = "";
            Physics = "";
            Connection = "";
            Row = 0;
            Column = 0;
        }
        public TopologyObject(string name, string physics, string connection, int row, int column)
        {
            Name = name;
            Physics = physics;
            Connection = connection;
            Row = row;
            Column = column;
        }
    }
}
