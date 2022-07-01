using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TcoIo.Topology
{
    public static class DimsDef
    {
        public const double masterHeight = 185.0;
        public const double masterWidth = 285.0;

        public const double slaveHeight = masterHeight;
        public const double slaveWidth = 54.0;

        public const double masterOutput = masterHeight / 4;
        public const double slaveInput = slaveHeight / 4;
        public const double slaveOutputBack = slaveHeight / 4;
        public const double slaveOutputFront = slaveHeight * 3 / 4;
        public const double juntionOutputX1 = - slaveWidth / 4;
        public const double juntionOutputX2 = - slaveWidth * 3 / 4;

    }
}
