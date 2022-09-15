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

        private UserModel _SelectedUser;

        public UserModel SelectedUser
        {
            get { return _SelectedUser; }
            set 
            { 
                _SelectedUser = value;
                SelectedUserName = value.Email;
                UserRole = new BindingList<string>(value.Roles.Select(x => x.Value).ToList());
                Loadroles();
                NotifyOfPropertyChange(() => SelectedUser);
            }
        }

        private string _selectedUserName;

        public string SelectedUserName
        {
            get { return _selectedUserName; }
            set 
            { 
                _selectedUserName = value;
                NotifyOfPropertyChange(() => SelectedUserName);
            }
        }

        private BindingList<string> _userRole = new BindingList<string>();

        public BindingList<string> UserRole
        {
            get { return _userRole; }
            set 
            { 
                _userRole = value;
                NotifyOfPropertyChange(() => UserRole);
            }
        }

        private BindingList<string> _availableRole = new BindingList<string>();

        public BindingList<string> AvailableRole
        {
            get { return _availableRole; }
            set
            {
                _availableRole = value;
                NotifyOfPropertyChange(() => AvailableRole);
            }
        }

        private string _selectedAvailableRole;

        public string SelectedAvailableRole
        {
            get { return _selectedAvailableRole; }
            set
            {
                _selectedAvailableRole = value;
                NotifyOfPropertyChange(() => SelectedAvailableRole);
            }
        }

        private string _selectedUserRole;

        public string SelectedUserRole
        {
            get { return _selectedUserRole; }
            set
            { 
                _selectedUserRole = value;
                NotifyOfPropertyChange(() => SelectedUserRole); 
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

        private async Task Loadroles()
        {
            var roles = await _userEndPoint.GetAllRolls();
            foreach (var role in roles)
            {
                if (UserRole.IndexOf(role.Value) < 0)
                {
                    AvailableRole.Add(role.Value);
                }
            }
        }

        public async void AddSelectedRole()
        {
            await _userEndPoint.AddUserToRole(SelectedUser.Id, SelectedAvailableRole);
            
            UserRole.Add(SelectedAvailableRole);
            AvailableRole.Remove(SelectedAvailableRole);
        }
        public async void RemoveSelectedRole()
        {
            await _userEndPoint.RemoveUserFromRole(SelectedUser.Id, SelectedUserRole);

            AvailableRole.Add(SelectedUserRole);
            UserRole.Remove(SelectedUserRole);
            
        }
    }
}
