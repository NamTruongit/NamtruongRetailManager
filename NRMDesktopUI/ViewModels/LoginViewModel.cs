﻿using Caliburn.Micro;
using NRMDesktopUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NRMDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _username;
        private string _password;
        private IAPIHelpers _apihelper;
        public LoginViewModel(IAPIHelpers apihelper)
        {
            _apihelper = apihelper;
        }
        public string Username
        {
            get { return _username; }
            set 
            {
                _username = value; 
                NotifyOfPropertyChange(() => Username);
                NotifyOfPropertyChange(()=>CanLogIn);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            { 
                _password = value;
                NotifyOfPropertyChange(()=>Password);
                NotifyOfPropertyChange(()=>CanLogIn);
            }
        }

        public bool IsErrorVisible
        {
            get 
            {
                bool output = false;
                if(ErrorMessage?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
            set { }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set 
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => IsErrorVisible);
                NotifyOfPropertyChange(() => ErrorMessage);
               
            }
        }

        public bool CanLogIn
        {
            get
            {
                bool output = false;
                if (Username?.Length > 0 && Password?.Length > 0)
                {
                    return true;
                }
                return output;
            }
        }

        public async Task LogIn()
        {
            try
            {
                ErrorMessage = "";
                var result = await _apihelper.Authenticate(Username, Password);
                //More informantion about user
                await _apihelper.GetLoggedInUserInfo(result.Access_Token);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
