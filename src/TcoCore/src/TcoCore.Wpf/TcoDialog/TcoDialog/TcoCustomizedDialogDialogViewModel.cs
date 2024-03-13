using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TcOpen.Inxton.Input;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;
using Vortex.Presentation;

namespace TcoCore
{
    public class TcoCustomizedDialogDialogViewModel : TcoDialogBaseViewModel , IDisposable
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public TcoCustomizedDialogDialogViewModel() : base()
        {

           
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0,0,0,10);
           

            dispatcherTimer.Start();

       


            Option1Command = new RelayCommand((a) => { Dialog._answer.Synchron = (short)eCustomizedDialogAnswer.Option1; this.Close(this, new EventArgs()); },
                (b) => Dialog._hasOption1.Synchron,
                () => TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"{nameof(Option1Command)} of {Dialog.HumanReadable} was executed @{{payload}}.", new { Dialog.Symbol}));

            Option2Command = new RelayCommand((a) => { Dialog._answer.Synchron = (short)eCustomizedDialogAnswer.Option2; this.Close(this, new EventArgs());},
                (b) => Dialog._hasOption2.Synchron,
                () => TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"{nameof(Option2Command)} of {Dialog.HumanReadable} was executed @{{payload}}.", new { Dialog.Symbol }));

            Option3Command = new RelayCommand((a) => {Dialog._answer.Synchron = (short)eCustomizedDialogAnswer.Option3; this.Close(this, new EventArgs());}, 
                (b) => Dialog._hasOption3.Synchron,
                () => TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"{nameof(Option3Command)} of {Dialog.HumanReadable} was executed @{{payload}}.", new { Dialog.Symbol }));

            Option4Command = new RelayCommand((a) => {Dialog._answer.Synchron = (short)eCustomizedDialogAnswer.Option4; this.Close(this, new EventArgs());}, 
                (b) => Dialog._hasOption4.Synchron,
                () => TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"{nameof(Option4Command)} of {Dialog.HumanReadable} was executed @{{payload}}.", new { Dialog.Symbol }));

        
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Dialog.Read();
            if ( Dialog._answer.LastValue != (short)eCustomizedDialogAnswer.NoAnswer)
            {
                this.Close(this, new EventArgs());

                if (dispatcherTimer != null)
                {
                    dispatcherTimer.Stop(); // stop timer
                    dispatcherTimer.Tick -= dispatcherTimer_Tick;
                }
            }    

            

        }
   

        public void Dispose()
        {
            if (dispatcherTimer != null)
            {
                dispatcherTimer.Stop();
                dispatcherTimer.Tick -= dispatcherTimer_Tick;

            }
        }

        public TcoCustomizedDialog Dialog { get; private set; } = new TcoCustomizedDialog();
        public override object Model { get => Dialog; set => Dialog = (TcoCustomizedDialog)value; }
        public RelayCommand Option1Command { get; }
        public RelayCommand Option2Command { get; }
        public RelayCommand Option3Command { get; }
        public RelayCommand Option4Command { get; }        
    }

  
}
