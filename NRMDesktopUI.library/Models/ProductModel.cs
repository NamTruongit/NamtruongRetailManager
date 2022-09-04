using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRMDesktopUI.library.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Productname { get; set; }
        public string Decription { get; set; }
        public decimal RetailPrice { get; set; }
        public int QuantityInStock { get; set; }
    }
}
