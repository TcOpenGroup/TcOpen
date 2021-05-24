using System;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;
using Vortex.Presentation.Wpf;

namespace TcoCore
{
    public class TcoToggleTaskViewModel : RenderableViewModel
    {
        private string caption;

        public TcoToggleTaskViewModel()
        {
            ToggleCommand = new RelayCommand(action => this.TcoToggleTask._toggleRequest.Cyclic = true);
        }


        private void TcoToggleTask_StateChanged(IValueTag sender, ValueChangedEventArgs args)
        {
            UpdateCaption();
        }

        private void UpdateCaption()
        {
            lock(this)
            {
                TcoToggleTask val = TcoToggleTask;

                string name = (val != null ? string.IsNullOrEmpty(val.AttributeName) ? "Missing object name" : val.AttributeName : "Missing object information").ToString();
                string valueOn = (val != null ? string.IsNullOrEmpty(val.AttributeStateOnDesc) ? "On" : val.AttributeStateOnDesc : "Missing object information").ToString();
                string valueOff = (val != null ? string.IsNullOrEmpty(val.AttributeStateOffDesc) ? "Off" : val.AttributeStateOffDesc : "Missing object information").ToString();

                Caption = val != null ? val._state.Synchron == true ? name + " : " + valueOn : name + " : " + valueOff : "Missing object information";
            }
        }

        private void ModeUpdate()
        {
            TcoToggleTask?._state.Subscribe(TcoToggleTask_StateChanged);
            UpdateCaption();
            this.OnPropertyChanged(nameof(Caption));

        }

        public TcoToggleTask TcoToggleTask { get; private set; }

        public override object Model { get => TcoToggleTask; set { TcoToggleTask = value as TcoToggleTask; ModeUpdate(); } }

        public RelayCommand ToggleCommand { get; }

        public string Caption { get => caption; set { caption = value; this.OnPropertyChanged(nameof(Caption)); } }

    }
}
