using System;
using System.Linq;

namespace TcoInspectors
{
    public partial class TcoAnalogueInspectorData : IsInspectorData
    {
        dynamic IsInspectorData.DetectedStatus => this.DetectedStatus;
    }
}
