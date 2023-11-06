using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Swift;
using Vortex.Connector;
using TcoCore;
using System.Text.RegularExpressions;

namespace TcoUrRobotics
{

    public static class ControllerEvents
    {
        public static IDictionary<uint, string> Ids = new Dictionary<uint, string>()
        {
            {0, "" },
            {90205, @"Auto stop."},
            {50204, @"Collision detected."}

            //todo errors from  controller waiting for list
        };
          

    }
    
}


