using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Security;

namespace TcOpen.Inxton.Local.Security
{
    public class GroupData : IBrowsableDataObject
    {
        private string _name;
        public ObservableCollection<string> Roles { get; set; }
        public string SecurityStamp { get; set; }
        public dynamic _recordId { get; set; }
        public DateTime _Created { get; set; }
        public string _EntityId { get; set; }
        public DateTime _Modified { get; set; }

        public GroupData(string name)
        {
            Name = name;
            Roles = new ObservableCollection<string>();
        }

        public string Name
        {
            get => _name;
            set { _name = value; }
        }

        private List<string> _changes = new List<string>();
        public List<string> Changes
        {
            get { return this._changes; }
            set { this._changes = value; }
        }
    }
}
