using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TcoCore;
using TcOpen.Inxton.Swift;
using Vortex.Connector;

namespace TcoUrRobotics
{
    public static class ControllerEvents
    {
        public static IDictionary<uint, string> Ids = new Dictionary<uint, string>()
        {
            { 0, "" },
            { 90205, @"Auto stop." },
            { 50204, @"Collision detected." }

            //todo errors from  controller waiting for list
        };
    }
}
