using System;

namespace NRMDesktopUI.library
{
    public interface ILoggedInUserModel
    {
         string Token { get; set; }
         string Id { get; set; }
         string FirstsName { get; set; }
         string LastName { get; set; }
         string EmailAddress { get; set; }
         DateTime CreateDate { get; set; }
        void ResetUsetModel();
    }
}