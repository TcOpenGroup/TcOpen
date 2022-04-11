using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation.Wpf;
using TcoCore;
using System.Windows.Controls;
using System.Windows;
using TcOpen.Inxton.Local.Security;
using HmiTemplate.Wpf;

namespace MainPlc
{
    public class CUBaseViewModel : MenuRenderableControlViewModel
    {
        public CUBaseViewModel()
        {           
            this.OpenCommand(this.AddCommand(typeof(CUBaseTasksView), "Control", this ));       
            this.AddCommand(typeof(CUBaseDataView), "Data", this);
            this.AddCommand(typeof(CUBaseDiagView), "Diagnostics", this);
            this.OpenDetailsCommand = new TcOpen.Inxton.Input.RelayCommand((a) => OpenDetails());
        }

        private void OpenDetails()
        {
            if (AuthorizationChecker.HasAuthorization(Roles.controlled_unit_can_open_details))
            {
                var detailsView = Vortex.Presentation.Wpf.LazyRenderer.Get.CreatePresentation("Control", Component, new Grid(), false);
                Vortex.Presentation.Wpf.NavigableViewModelBase.Current.OpenView(detailsView as FrameworkElement);
            }
        }

        public IEnumerable<object> _tasks = new List<object>();
        public IEnumerable<object> Tasks
        {
            get
            {
                if (Component != null && Component.GetChildren() != null)
                {                                         
                    _tasks = Component.GetChildren<ITcoTasked>();                    
                }

                return _tasks;
            }
        }

        public CUBase Component { get; private set; } = new CUBase();

        public ProcessData OnlineData { get { return Component.GetChildren<TcoData.TcoDataExchange>().FirstOrDefault()?.GetChildren<TcoData.TcoEntity>().FirstOrDefault() as ProcessData; } }

        public EntityHeader EntityHeader { get { return OnlineData.EntityHeader; } }

        void Update()
        {
            var symbolOrName = new NameOrSymbolConverter();
            this.Title = (string)symbolOrName.Convert(Component, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture);
        }

        public override object Model { get => Component; set { Component = (CUBase)value; this.Update(); } }        

        public TcOpen.Inxton.Input.RelayCommand OpenDetailsCommand { get; }
    }
}
