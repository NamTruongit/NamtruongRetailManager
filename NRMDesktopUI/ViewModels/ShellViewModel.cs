using Caliburn.Micro;
using NRMDesktopUI.EventModels;
using NRMDesktopUI.Helpers;
using NRMDesktopUI.library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>,IHandle<LogOnEvent>
    {
        private IEventAggregator _events;
        private SalesViewModel _salesVM;
        private ILoggedInUserModel _user;
        IAPIHelpers _apiHelper;


        public ShellViewModel(IEventAggregator events,SalesViewModel saleVM, ILoggedInUserModel user, IAPIHelpers apiHelper)
        {

            _events = events;
            _salesVM = saleVM;
            _events.Subscribe(this);
            _user = user;
            _apiHelper = apiHelper;
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void ExitApplication()
        {
            TryClose();
        }

        public bool IsAccountVisible
        {
            get
            {
                bool output = false;
                if (string.IsNullOrWhiteSpace(_user.Token)==false)
                {
                    output = true;
                }
                return output;
            }
        }

        public void UserManagement()
        {
            ActivateItem(IoC.Get<UserDisplayViewModel>());
        }

        public void LogOut()
        {
            _user.ResetUsetModel();
            _apiHelper.LogOffUser();
            ActivateItem(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsAccountVisible);

        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
            NotifyOfPropertyChange(() => IsAccountVisible);
        }
    }
}
