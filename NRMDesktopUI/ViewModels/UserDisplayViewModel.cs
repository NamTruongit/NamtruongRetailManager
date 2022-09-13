using Caliburn.Micro;
using NRMDesktopUI.library.API;
using NRMDesktopUI.library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NRMDesktopUI.ViewModels
{
    public class UserDisplayViewModel : Screen
    {
        private readonly StatusInfoViewModel _status;
        private readonly IWindowManager _window;
        private readonly IUserEndPoint _userEndPoint;

        BindingList<UserModel> _users;
        public BindingList<UserModel> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
                NotifyOfPropertyChange(() => Users);
            }
        }
        public UserDisplayViewModel(StatusInfoViewModel status,IWindowManager window,IUserEndPoint userEndPoint)
        {
            _status = status;
            _window = window;
            _userEndPoint = userEndPoint;
        }
        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {
                await Loadusers();
            }
            catch (Exception ex)
            {
                dynamic setting = new ExpandoObject();
                setting.WindowStartUpLocation = WindowStartupLocation.CenterOwner;
                setting.ReSizeMode = ResizeMode.NoResize;
                setting.Title = "System Error";

                if (ex.Message == "Unauthorized")
                {
                    _status.UpdateMessage("Unauthorized Access", "You do not have permission to interact with the sale Form.");
                    _window.ShowDialog(_status, null, setting);
                }
                else
                {
                    _status.UpdateMessage("Fatal Exception", ex.Message);
                    _window.ShowDialog(_status, null, setting);
                }

                TryClose();
            }
        }

        private async Task Loadusers()
        {
            var userList = await _userEndPoint.GetAll();
            Users = new BindingList<UserModel>(userList);
        }

    }
}
