using System.ComponentModel;

namespace TcoIo
{
    public class Zoom : INotifyPropertyChanged
    {
        private double zoomValue = 1.0;
        public double ZoomValue
        {
            get { return this.zoomValue; }
            set
            {
                this.zoomValue = value;
                NotifyPropertyChanged(nameof(ZoomValue));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
