using PlcTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.PlcTemplate.Wpf
{
    public class MainWindowViewModel
    {
        public object PlcTemplatePlc { get; } = Entry.PlcTemplatePlc;
    }
}
