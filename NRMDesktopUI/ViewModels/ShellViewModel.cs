using Caliburn.Micro;
using NRMDesktopUI.EventModels;
using NRMDesktopUI.Helpers;
using NRMDesktopUI.library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NRMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>,IHandle<LogOnEvent>
    {
        private IEventAggregator _events;
        private ILoggedInUserModel _user;
        IAPIHelpers _apiHelper;


        public ShellViewModel(IEventAggregator events, ILoggedInUserModel user, IAPIHelpers apiHelper)
        {

            _events = events;
            _events.SubscribeOnPublishedThread(this);
            _user = user;
            _apiHelper = apiHelper;
             ActivateItemAsync(IoC.Get<LoginViewModel>(),new CancellationToken());
        }

        public void ExitApplication()
        {
            TryCloseAsync();
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

        public async Task UserManagement()
        {
            await ActivateItemAsync(IoC.Get<UserDisplayViewModel>());
        }

        public async Task LogOut()
        {
            _user.ResetUsetModel();
            _apiHelper.LogOffUser();
            await ActivateItemAsync(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsAccountVisible);

        }

        //public void Handle(LogOnEvent message)
        //{
        //    ActivateItem(_salesVM);
        //    NotifyOfPropertyChange(() => IsAccountVisible);
        //}

        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(IoC.Get<SalesViewModel>(), cancellationToken);
            NotifyOfPropertyChange(() => IsAccountVisible);
        }
    }
}
