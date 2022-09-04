using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRMDataManager.library.Internal.Models
{
    public class ProductModel
    {
        /// <summary>
        /// The unique identifier for a given product
        /// </summary>
        public int Id { get; set; }
        public string Productname { get; set; }
        public string Decription { get; set; }
        public decimal RetailPrice { get; set; }
        public int QuantityInStock { get; set; }
    }
}
