using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRMDataManager.library.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FirstsName { get; set; }
        public string LastName  { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
