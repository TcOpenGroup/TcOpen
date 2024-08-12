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
"WindowsTH-KB2693643-x64" - Remote Server Administration Tools for Windows 10
win+r - "ldp.exe"

This LDAP service will create a user based on the structure in Active Directory.
If you wish to override the creation of user implement `CreateUserOnBound` function.
This function will be invoked after user is authenticated with his credentials.

## Example using Synology LDAP

Here's how to use Synology LDAP server.

On Synology, ideally using the Package Center install LDAP Server
![image](https://user-images.githubusercontent.com/11136013/158380475-d473e50d-3a79-4809-9aaa-fa8d44933ffb.png)

After you install it you need to enable the LDAP Server in the app you just installed
![image](https://user-images.githubusercontent.com/11136013/158380604-20d1a15f-dd6a-4f06-9254-9a9862c4379c.png)

I was using `ldp.exe` to verify I can connect and bind.
![image](https://user-images.githubusercontent.com/11136013/158380714-7d9f2f01-c357-468e-8da5-ec41e4ef949f.png)

You can create a new user fairly easy on synology.

Here's a log from ldp.exe on some users I created

<details>
  <summary>Click to expand the log!</summary>

```
Searching...
ldap_search_s(ld, "cn=users,dc=inxton", 2, "(objectClass=*)", attrList,  0, &msg)
Getting 5 entries:
Dn: cn=users,dc=inxton
cn: users;
objectClass: organizationalRole;

Dn: uid=admin,cn=users,dc=inxton
apple-generateduid: AA04C5DE-AED6-4D1B-ABF5-4FB94FB41267;
authAuthority: ;basic;;
cn: admin;
displayName: admin;
gecos: Directory/Diskstation default admin user;
gidNumber: 1000001;
homeDirectory: /home/admin;
loginShell: /bin/sh;
memberOf (3): cn=users,cn=groups,dc=inxton; cn=Directory Operators,cn=groups,dc=inxton; cn=administrators,cn=groups,dc=inxton;
objectClass (10): top; posixAccount; shadowAccount; person; organizationalPerson; inetOrgPerson; apple-user; sambaSamAccount; sambaIdmapEntry; extensibleObject;
sambaAcctFlags: [U          ];
sambaLMPassword: XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX;
sambaNTPassword: A7A1C8029EA5A0A347896FFABCDD20A5;
sambaPasswordHistory: 0000000000000000000000000000000000000000000000000000000000000000;
sambaPwdLastSet: 1647333861;
sambaSID: S-1-5-21-2623049765-2560693991-2362637602-1005;
shadowExpire: 1;
shadowFlag: 0;
shadowInactive: 0;
shadowLastChange: 19066;
shadowMax: 99999;
shadowMin: 0;
shadowWarning: 7;
sn: admin;
uid: admin;
uidNumber: 1000000;
userPassword: {CRYPT}$6$cdVD7KlG$JTwf9ALm4rdQNwherd69niwgfZMWcun1a0aDuBdWD2dC/5z3.Fz0yhmXd8x83ylPF6ufPMXYl3JajcBrcirjN.;

Dn: uid=another_user,cn=users,dc=inxton
apple-birthday: 20220301000000Z;
apple-generateduid: 7A9800E8-4A91-4790-928D-FF33D70165CC;
authAuthority: ;basic;;
cn: another_user;
departmentNumber: dept;
displayName: another_user;
employeeNumber: employee_numma;
employeeType: type;
gecos: HIS DESCRIPTION;
gidNumber: 1000001;
homeDirectory: /home/another_user;
homePhone: 222;
loginShell: /bin/sh;
mail: HISEMAIL@email.com;
memberOf: cn=users,cn=groups,dc=inxton;
mobile: 3333;
objectClass (10): top; posixAccount; shadowAccount; person; organizationalPerson; inetOrgPerson; apple-user; sambaSamAccount; sambaIdmapEntry; extensibleObject;
postalAddress: rrrr 32;
sambaAcctFlags: [U          ];
sambaLMPassword: XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX;
sambaNTPassword: A7A1C8029EA5A0A347896FFABCDD20A5;
sambaPasswordHistory: 0000000000000000000000000000000000000000000000000000000000000000;
sambaPwdLastSet: 1647336984;
sambaSID: S-1-5-21-2623049765-2560693991-2362637602-1006;
shadowExpire: -1;
shadowFlag: 0;
shadowInactive: 0;
shadowLastChange: 19066;
shadowMax: 99999;
shadowMin: 0;
shadowWarning: 7;
sn: another_user;
telephoneNumber: 1111;
title: title;
uid: another_user;
uidNumber: 1000001;
userPassword: {CRYPT}$6$Jq1rO2WQ$DR.p78OgHfqka9g0NQV0IU2VrIN8vV9f1cwjrOB8ml27rwTcEAPCUr/ljQ0JeYK.DhTEc34s3JkEoug09EwrP.;

Dn: uid=tesla,cn=users,dc=inxton
apple-generateduid: F8065DC1-6CDF-459E-BADC-D36018377D35;
authAuthority: ;basic;;
cn: tesla;
displayName: tesla;
gidNumber: 1000001;
homeDirectory: /home/tesla;
loginShell: /bin/sh;
memberOf (3): cn=Directory Consumers,cn=groups,dc=inxton; cn=Directory Operators,cn=groups,dc=inxton; cn=users,cn=groups,dc=inxton;
objectClass (10): top; posixAccount; shadowAccount; person; organizationalPerson; inetOrgPerson; apple-user; sambaSamAccount; sambaIdmapEntry; extensibleObject;
sambaAcctFlags: [U          ];
sambaLMPassword: XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX;
sambaNTPassword: 8846F7EAEE8FB117AD06BDD830B7586C;
sambaPasswordHistory: 0000000000000000000000000000000000000000000000000000000000000000;
sambaPwdLastSet: 1647344681;
sambaSID: S-1-5-21-2623049765-2560693991-2362637602-1007;
shadowExpire: -1;
shadowFlag: 0;
shadowInactive: 0;
shadowLastChange: 19066;
shadowMax: 99999;
shadowMin: 0;
shadowWarning: 7;
sn: tesla;
uid: tesla;
uidNumber: 1000002;
userPassword: {CRYPT}$6$S6g2OSxO$DIwcYxBhEN4ZLmVAXlfpV8W0zfHIghjg/AdL0dPVd2pkncmsK0QPQCjrZbMjRH9Tp8KEcgTz0LmJVgdxTBCMN/;

Dn: uid=abcd,cn=users,dc=inxton
apple-generateduid: 8947F9A6-97DE-4178-B873-CF60E32A2222;
authAuthority: ;basic;;
cn: abcd;
displayName: abcd;
gidNumber: 1000001;
homeDirectory: /home/abcd;
loginShell: /bin/sh;
memberOf: cn=users,cn=groups,dc=inxton;
objectClass (10): top; posixAccount; shadowAccount; person; organizationalPerson; inetOrgPerson; apple-user; sambaSamAccount; sambaIdmapEntry; extensibleObject;
sambaAcctFlags: [U          ];
sambaLMPassword: XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX;
sambaNTPassword: EB4FF39B74B0CBCE20A4F62DBD1E3585;
sambaPasswordHistory: 0000000000000000000000000000000000000000000000000000000000000000;
sambaPwdLastSet: 1647347102;
sambaSID: S-1-5-21-2623049765-2560693991-2362637602-1008;
shadowExpire: -1;
shadowFlag: 0;
shadowInactive: 0;
shadowLastChange: 19066;
shadowMax: 99999;
shadowMin: 0;
shadowWarning: 7;
sn: abcd;
uid: abcd;
uidNumber: 1000003;
userPassword: {CRYPT}$6$WSXDmysJ$lgHccUgPW83WueHPz.S9lY41feMyqZFPcVYpo3ycskIAz37Hf0g/hKKbbloOmG20T5HzDo7H5TjLK/A8F/ZeJ.;
```

</details>

In TcOpen.Inxton C# app you can then write

```csharp
 SecurityManager.Create(new LdapService(
                new LdapConfig("10.10.23.232", 389, false, "cn=users,dc=inxton")));
```

And in order to authenticate see this test.

```csharp
  [Test()]
        public void AuthenticateUserSuccessTest()
        {
            //-- Arrange
            var userName = "uid=abcd,cn=users,dc=inxton";
            var password = "abcd";
            (authService as LdapService).CreateUserOnBound = (username, connection) => new LdapUser { UserName = username };

            //-- Act
            var actual = authService.AuthenticateUser(userName, password);

            //-- Assert
            Assert.AreEqual(userName, actual.UserName);
            Assert.AreEqual($"Success {userName}", AuthService_OnUserAuthenticateSuccessMessage);
        }
```
