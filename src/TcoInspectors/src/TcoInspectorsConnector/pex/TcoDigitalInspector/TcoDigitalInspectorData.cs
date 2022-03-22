using System;
using System.Linq;

namespace TcoInspectors
{
    public partial class TcoDigitalInspectorData : IsInspectorData
    {
        dynamic IsInspectorData.DetectedStatus => this.DetectedStatus;
    }
}
