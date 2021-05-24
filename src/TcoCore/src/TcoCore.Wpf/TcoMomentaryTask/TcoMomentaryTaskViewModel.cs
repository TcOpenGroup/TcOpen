using System;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;
using Vortex.Presentation.Wpf;

namespace TcoCore
{
    public class TcoMomentaryTaskViewModel : RenderableViewModel
    {
        private string caption;

        public TcoMomentaryTaskViewModel()
        {
            ButtonDown = new RelayCommand(action => OnButtonDown());
            ButtonUp = new RelayCommand(action => OnButtonUp());
        }


        private void TcoMomentaryTask_StateChanged(IValueTag sender, ValueChangedEventArgs args)
        {
            UpdateCaption();
        }

        private void UpdateCaption()
        {
            lock(this)
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

        public TcoMomentaryTask TcoMomentaryTask { get; private set; }

        public override object Model { get => TcoMomentaryTask; set { TcoMomentaryTask = value as TcoMomentaryTask; ModeUpdate(); } }

        public RelayCommand ButtonDown { get; set; }
        public RelayCommand ButtonUp { get; set; }

        private void OnButtonUp()
        {
            this.TcoMomentaryTask._setOnRequest.Cyclic = false;
        }

        private void OnButtonDown()
        {
            this.TcoMomentaryTask._setOnRequest.Cyclic = true;
        }

        public string Caption { get => caption; set { caption = value; this.OnPropertyChanged(nameof(Caption)); } }

    }
}
