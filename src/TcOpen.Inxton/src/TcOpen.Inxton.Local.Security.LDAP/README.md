# LDAP Authentication
## Service for Security manager to authenicate via LDAP.
This service is intendted to be used when your whole user management is delegated to another service 
like Active Directory, Azure Active Directory or other LDAP based system.  
Usage:
```csharp
SecurityManager.Create(new LdapService(
    new LdapConfig(
    host: "yourLDAPdomain.com",
    port: 636,
    useSsl: true,
    searchBase: "OU=AADDC Users,DC=testldap,DC=com" ))
);
```

Note that searchBase may vary. To find out what base you should use I recommend to use the `ldp.exe` tool. Especially when accessing LDAP via SSL.
If you use unsecured LDAP connection ADExplorer.exe may be good as well.
https://docs.microsoft.com/en-us/previous-versions/windows/it-pro/windows-server-2012-r2-and-2012/cc771022(v=ws.11)
To get it, you need to install 
"WindowsTH-KB2693643-x64"  -  Remote Server Administration Tools for Windows 10 
win+r  -  "ldp.exe" 
