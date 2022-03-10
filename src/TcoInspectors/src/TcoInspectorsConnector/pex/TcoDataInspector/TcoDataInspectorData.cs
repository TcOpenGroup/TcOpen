using System;
using System.Linq;

namespace TcoInspectors
{
    public partial class TcoDataInspectorData : IsInspectorData
    {
        dynamic IsInspectorData.DetectedStatus => this.DetectedStatus;
    }
}
