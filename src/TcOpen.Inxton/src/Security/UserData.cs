using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using TcOpen.Inxton.Data;

namespace TcOpen.Inxton.Local.Security
{
    public class UserData : IBrowsableDataObject, INotifyPropertyChanged
    {
        private string _username;
        private string _email;
        private Boolean _canUserChangePassword;

        public UserData()
        {
            Roles = new ObservableCollection<string>();
        }
        public UserData(string username, string email, string password, IEnumerable<string> roles, string level, string authenticationToken)
        {
            Username = username;
            Email = email;
            HashedPassword = CalculateHash(password, username);
            Roles = new ObservableCollection<string>(roles);
            RoleHash = CalculateRoleHash(roles, username);
            Level = CalculateHash(level, username);
            AuthenticationToken = CalculateHash(authenticationToken, string.Empty);
        }
             
        public UserData(string username, string password, IEnumerable<string> roles)
        {
            Username = username;
            HashedPassword = CalculateHash(password, username);
            Roles = new ObservableCollection<string>(roles);
            RoleHash = CalculateRoleHash(roles, username);
        }
        public string SecurityStamp { get; set; }
        public dynamic _recordId { get; set; }
        public string Username
        {
            get => _username; set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Email
        {
            get => _email; set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        string hashedPassword;
        public string HashedPassword
        {
            get
            {
                if (!string.IsNullOrEmpty(hashedPassword))
                    return hashedPassword;

                return string.Empty;
            }
            set
            {
                hashedPassword = value;
            }
        }
        public ObservableCollection<string> Roles { get; set; }
        public Boolean CanUserChangePassword
        {
            get => _canUserChangePassword;
            set
            {
                _canUserChangePassword = value;
                OnPropertyChanged(nameof(CanUserChangePassword));
            }
        }

        public DateTime _Created { get; set; }
        public string _EntityId { get; set; }
        public DateTime _Modified { get; set; }
        public string RoleHash { get; set; }

        string level;
        public string Level
        {
            get
            {
                return level;
            }
            set
            {
                if (level == value)
                {
                    return;
                }

                level = value;
                OnPropertyChanged();
            }
        }

        string authenticationToken;
        public string AuthenticationToken
        {
            get
            {
                if (authenticationToken == null)
                {
                    authenticationToken = string.Empty;
                }

                return authenticationToken;
            }
            set
            {
                if (authenticationToken == value)
                {
                    return;
                }

                authenticationToken = value;
                OnPropertyChanged();
            }
        }

        public void UpdateRoleHash() => RoleHash = CalculateRoleHash(Roles, Username);

        public void SetPlainTextPassword(string plainTextPassowrd) => HashedPassword = CalculateHash(plainTextPassowrd, Username);

        private string CalculateHash(string textToHash, string salt)
        {
            // Convert the salted password to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(textToHash + salt);
            // Use the hash algorithm to calculate the hash
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // Return the hash as a base64 encoded string to be compared to the stored password
            return Convert.ToBase64String(hash);
        }

        private String CalculateRoleHash(IEnumerable<string> roles, string username) =>
             CalculateHash(String.Join(",", roles.OrderByDescending(x => x).ToList()), username);

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private List<string> _changes = new List<string>();
        public List<string> Changes
        {
            get { return this._changes; }
            set { this._changes = value; }
        }

        TimeSpan logoutTime;
        public TimeSpan LogoutTime
        {
            get
            {
                return logoutTime;
            }
            set
            {
                if (logoutTime == value)
                {
                    return;
                }

                logoutTime = value;
                OnPropertyChanged();
            }
        }
    }
}
