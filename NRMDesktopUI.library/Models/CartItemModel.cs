using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRMDesktopUI.library.Models
{
    public class CartItemModel
    {
        public ProductModel Product { get; set; } 
        public int QuantityInCart { get; set; }

    }
}
