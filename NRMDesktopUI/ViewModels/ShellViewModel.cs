using Caliburn.Micro;
using NRMDesktopUI.EventModels;
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


        public ShellViewModel(IEventAggregator events,SalesViewModel saleVM)
        {

            _events = events;
            _salesVM = saleVM;
            _events.Subscribe(this);
            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
        }
    }
}
