using System.ComponentModel;

namespace TcoCore
{
    public partial class PlainStepDetails
    {        
        public eStepStatus StatusAsEnum { get { return (eStepStatus)this.Status; } }

        bool isActive;
        public bool IsActive
        {
            get => isActive;
            set
            {
                if (isActive == value)
                {
                    return;
                }

                isActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsActive)));
            }
        }
    }
}