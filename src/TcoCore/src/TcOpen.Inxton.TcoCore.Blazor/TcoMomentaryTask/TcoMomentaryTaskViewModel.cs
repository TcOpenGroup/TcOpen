using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoCore;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;
using Vortex.Presentation;
using TcOpen.Inxton.Input;

namespace TcoCore
{
    public class TcoMomentaryTaskViewModel : RenderableViewModelBase
    {
        private string caption;
        public string Caption { get => caption; set { caption = value; this.OnPropertyChanged(nameof(Caption)); } }


        public TcoMomentaryTask TcoMomentaryTask { get; private set; }
        public RelayCommand ButtonDown { get; }
        public RelayCommand ButtonUp { get; }

        public string ButtonState { get; set; } = "btn-primary";
        public override object Model { get => TcoMomentaryTask; set { TcoMomentaryTask = value as TcoMomentaryTask; ModeUpdate(); } }


        public TcoMomentaryTaskViewModel()
        {
            ButtonDown = new RelayCommand(action => OnButtonDown());
            ButtonUp = new RelayCommand(action => OnButtonUp());
        }

        private void TcoMomentaryTask_StateChanged(IValueTag sender, ValueChangedEventArgs args)
        {
            UpdateCaption();
        }

        internal void UpdateCaption()
        {
            lock (this)
            {
                TcoMomentaryTask val = TcoMomentaryTask;

                string name = (val != null ? string.IsNullOrEmpty(val.AttributeName) ? "Missing object name" : val.AttributeName : "Missing object information").ToString();
                string valueOn = (val != null ? string.IsNullOrEmpty(val.AttributeStateOnDesc) ? "On" : val.AttributeStateOnDesc : "Missing object information").ToString();
                string valueOff = (val != null ? string.IsNullOrEmpty(val.AttributeStateOffDesc) ? "Off" : val.AttributeStateOffDesc : "Missing object information").ToString();

                Caption = val != null ? val._state.Synchron == true ? name + " : " + valueOn : name + " : " + valueOff : "Missing object information";
            }
        }

        private void ModeUpdate()
        {
            TcoMomentaryTask?._state.Subscribe(TcoMomentaryTask_StateChanged);
            UpdateCaption();
            this.OnPropertyChanged(nameof(Caption));

        }

        private void OnButtonUp()
        {
            ButtonState = "btn-primary";
            this.TcoMomentaryTask.Stop();
        }

        private void OnButtonDown()
        {
            ButtonState = "btn-success";
            this.TcoMomentaryTask.Start();
        }
    }


}
