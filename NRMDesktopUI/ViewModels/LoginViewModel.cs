using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRMDesktopUI.ViewModels
{
    public class LoginViewModel :Screen
    {
        private string _username;

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

        private string _password;

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
        public void LogIn(string userName,string passWord)
        {
            Console.WriteLine();
        }
    }
}
