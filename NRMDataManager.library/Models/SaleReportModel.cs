using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRMDataManager.library.Models
{
    public class SaleReportModel
    {
        public DateTime SaleDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public string FirstsName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}
