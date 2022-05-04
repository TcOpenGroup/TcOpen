using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TemplateTests
{
    internal class TemplateApp
    {
        private Process appProcess;
        private void StartApp()
        {
            var slnFolder = Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..\\..\\..\\..\\..\\..\\..\\..\\..\\")); //Environment.GetEnvironmentVariable("slnFolder") ?? @"..\";

#if DEBUG
            var templateFolder = @"src\TcOpenAppTemplates\StandardMtsTemplate\src\HmiTemplate.Wpf\bin\Debug\net48";
#else
            var templateFolder = @"src\TcOpenAppTemplates\StandardMtsTemplate\src\HmiTemplate.Wpf\bin\Release\net48";
#endif
            var templateExe = "HmiTemplate.Wpf.exe";
            var applicationPath = Path.GetFullPath(Path.Combine(slnFolder, templateFolder, templateExe));
            var app = Path.GetFullPath(applicationPath);
            if (File.Exists(applicationPath))
                appProcess = Process.Start(applicationPath);
            else
                throw new EntryPointNotFoundException(applicationPath + "Not found. Current PWD " + Environment.CurrentDirectory);

        }

        public void KillApp()
        {
            appProcess?.Kill();
            _instance = null;
        }

        private TemplateApp()
        {
            this.StartApp();
        }


        static TemplateApp _instance;

        public static TemplateApp Get
        {
            get 
            {
                if (_instance == null)
                {
                    _instance = new TemplateApp();                         
                }

                return _instance;
            }
            
        }
            
       
        
    }
}
