using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private HomeViewModel _homeVM;
        private LoginViewModel _loginVM;
        //public ShellViewModel(HomeViewModel homeVM)
        //{
        //   _homeVM = homeVM;
        //    ActivateItem(_homeVM);
        //}
        public ShellViewModel(LoginViewModel loginVM)
        {
            _loginVM = loginVM;
            ActivateItem(_loginVM);
        }
    }
}
