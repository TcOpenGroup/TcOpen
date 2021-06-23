using FlaUI.UIA3;
using NUnit.Framework;
using System.Reflection;

namespace IntegrationProjectTests
{
    public class ExecuteWpfIntegrationProject
    {       
        [Test]
        [Timeout(10000)]
        public void OpenApplicationTest()
        {
            var someAssemblyType = new Sandbox.IntegrationProjects.Wpf.App();
            var assembly = someAssemblyType.GetType().Assembly;
            var app = FlaUI.Core.Application.Launch(assembly.Location);
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);              
                System.Threading.Thread.Sleep(2000);
                window.Close();
            }
        }
    }
}