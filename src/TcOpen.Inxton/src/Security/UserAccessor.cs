using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Local.Security
{
    public class UserAccessor : INotifyPropertyChanged
    {
        private static UserAccessor _instance;

        private AppIdentity _identity;

        public AppIdentity Identity
        {
            get => _identity;
            set
            {
                _identity = value;
                this.DisplayUserName = _identity.Name;
                this.DisplayUserLevel = _identity.Level;
                OnPropertyChanged(nameof(Identity));
            }
        }

        public static UserAccessor Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserAccessor();
                return _instance;
            }
        }

        private UserAccessor() { }

        string displayUserName;
        public string DisplayUserName
        {
            get { return displayUserName; }
            set
            {
                if (displayUserName == value)
                {
                    return;
                }

                displayUserName = value;
                OnPropertyChanged();
            }
        }
        string displayUserLevel;
        public string DisplayUserLevel
        {
            get { return displayUserLevel; }
            set
            {
                if (displayUserLevel == value)
                {
                    return;
                }

                displayUserLevel = value;
                OnPropertyChanged();
            }
        }

        #region propertychanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}
