using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Input;
using Vortex.Connector;
using Vortex.Presentation;

namespace TcOpen.Inxton.Swift.Wpf
{
    public class SwiftRecorderViewModel : BindableBase
    {
        public SwiftRecorderViewModel(IVortexObject treeRootObject)
        {
            TreeRootObject = treeRootObject;
            StartRecordingCommand = new RelayCommand(a => StartRecording());
            StopRecordingCommand = new RelayCommand(a => StopRecording());
        }

        public SwiftRecorderViewModel()
        {
            StartRecordingCommand = new RelayCommand(a => StartRecording());
            StopRecordingCommand = new RelayCommand(a => StopRecording());
        }

        private Recorder _recorder;

        private void StartRecording()
        {
            _recorder = new Recorder(this.SelectedObject);
            _recorder.OnRecordAdded += _recorder_OnRecordAdded;
        }

        private void _recorder_OnRecordAdded()
        {
            Code = _recorder.EmitCode();
        }

        private void StopRecording()
        {
            Code = _recorder.EmitCode();
            _recorder.OnRecordAdded -= _recorder_OnRecordAdded;
            _recorder.Dispose();
        }

        string code;
        public string Code
        {
            get => code;
            set
            {
                if (code == value)
                {
                    return;
                }

                SetProperty(ref code, value);
            }
        }

        IVortexObject treeRootObject;
        public IVortexObject TreeRootObject
        {
            get => treeRootObject;
            set
            {
                if (treeRootObject == value)
                {
                    return;
                }

                SetProperty(ref treeRootObject, value);
            }
        }

        IVortexObject selectedObject;
        public IVortexObject SelectedObject
        {
            get => selectedObject;
            set
            {
                if (selectedObject == value)
                {
                    return;
                }

                SetProperty(ref selectedObject, value);
            }
        }

        public RelayCommand StartRecordingCommand { get; }
        public RelayCommand StopRecordingCommand { get; }
    }
}
