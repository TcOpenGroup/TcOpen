using System;
using System.Linq;

namespace TcOpen.Inxton.Data
{
    public enum eSearchMode
    {
        Exact,
        StartsWith,
        Contains,
    }

    public class SearchOptions
    {       
        public eSearchMode SearchMode { get; set; }
    }
}
