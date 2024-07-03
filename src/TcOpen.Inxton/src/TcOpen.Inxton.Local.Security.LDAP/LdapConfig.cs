namespace TcOpen.Inxton.Local.Security.LDAP
{
    public class LdapConfig
    {
        /// <summary>
        /// IP Address or Host name.
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Port for LDAP connection.
        /// Default values are :
        /// Without SSL : 389
        /// With SSL : 636
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// If you're using the port 389 set it to false
        /// If you're using the port 636 set it to true
        /// </summary>
        public bool UseSsl { get; }

        /// <summary>
        /// In case of Azure it may look like this :
        ///     OU=AADDC Users,DC=testldap,DC=com
        /// On premise might look like this :
        ///     CN=Users,DC=tcopen,DC=org
        /// Use LDP.exe to find out your searchbase.
        /// </summary>
        public string SearchBase { get; }

        public LdapConfig(string host, int port, bool useSsl, string searchBase)
        {
            Host = host;
            Port = port;
            UseSsl = useSsl;
            SearchBase = searchBase;
        }
    }
}
