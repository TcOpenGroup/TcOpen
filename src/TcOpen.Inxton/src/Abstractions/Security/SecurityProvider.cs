using System;
using System.Collections.Generic;
using System.Text;

namespace TcOpen.Inxton.Abstractions.Security
{   
    public class SecurityProvider
    {
        private SecurityProvider(IAuthenticationService layoutProvider)
        {
            AuthenticationService = layoutProvider;
        }

        public IAuthenticationService AuthenticationService
        {
            get;
        }       

        private static SecurityProvider provider;
        private static volatile object mutex = new object();
        public static SecurityProvider Get
        {
            get
            {
                if (provider == null)
                {
                    provider = Create(new DummyAuthenticationService());
                }

                return provider;

            }
        }

        public static event OnUserAuthentication OnAnyAuthenticationEvent;

        public static SecurityProvider Create(IAuthenticationService authenticationService)
        {
            if (provider == null)
            {
                lock (mutex)
                {
                    if (provider == null)
                    {
                        provider = new SecurityProvider(authenticationService);
                        provider.AuthenticationService.OnDeAuthenticated += OnAuthenticationEvent;
                        provider.AuthenticationService.OnUserAuthenticateFailed += OnAuthenticationEvent;
                        provider.AuthenticationService.OnUserAuthenticateSuccess += OnAuthenticationEvent;                        
                    }
                }
            }

            return provider;
        }

        private static void OnAuthenticationEvent(string username)
        {
            OnAnyAuthenticationEvent?.Invoke(username);
        }
    }
}
