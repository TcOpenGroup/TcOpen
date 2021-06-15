﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Abstractions.Security
{
    public abstract class IExternalAuthorization
    {        
        public delegate IUser AuthorizationRequestDelegate(string token);
        public delegate void AuthorizationTokenChangeRequestDelegate(string token);
        public event AuthorizationRequestDelegate AuthorizationRequest;        
        public event AuthorizationTokenChangeRequestDelegate AuthorizationTokenChange;
        private IUser Authorize(string token)
        {
            return AuthorizationRequest(token);            
        }

        private void ChangeToken(string token)
        {
            AuthorizationTokenChange(token);
        }

        protected abstract string GetToken();
        public IUser RequestAuthorization()
        {
            AuthorizationErrorMessage = string.Empty;
            try
            {
                if (WillChangeToken)
                {
                    RequestTokenChange();
                }

                return Authorize(GetToken());
            }
            catch (Exception ex)
            {
                AuthorizationErrorMessage = ex.Message;
            }

            return null;
        }

        public string AuthorizationErrorMessage
        {
            get;
            private set;
        }

        public void RequestTokenChange()
        {
            ChangeToken(GetToken());
            WillChangeToken = false;
        }

        public bool WillChangeToken
        {
            get;
            set;
        }
    }
}
