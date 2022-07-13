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
        public const double BordersVertical = 15.0;
        public const double BordersHorizontal = 10.0;

        public const double masterHeight = 185.0;
        public const double masterHeightWithBorders = masterHeight + BordersVertical;
        public const double masterWidth = 285.0;
        public const double masterWidthWithBorders = masterWidth + BordersHorizontal;

        public const double slaveHeight = masterHeight;
        public const double slaveHeightWithBorders = masterHeightWithBorders;
        public const double slaveWidth = 54.0;
        public const double slaveWidthWithBorders = slaveWidth + BordersHorizontal;

        public const double masterOutput = masterHeightWithBorders / 4;
        public const double slaveInput = slaveHeightWithBorders / 4;
        public const double slaveOutputBack = slaveHeightWithBorders / 4;
        public const double slaveOutputFront = slaveHeightWithBorders * 3 / 4;
        public const double juntionOutputX1 = -slaveWidthWithBorders / 4;
        public const double juntionOutputX2 = -slaveWidthWithBorders * 3 / 4;

    }
}
