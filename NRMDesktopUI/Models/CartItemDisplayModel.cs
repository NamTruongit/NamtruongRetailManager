using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRMDesktopUI.Models
{
    public class CartItemDisplayModel : INotifyPropertyChanged
    {
        public ProducDisplayModel Product { get; set; }

        private int _quantityInCart;
        public int QuantityInCart
        {
            get { return _quantityInCart; }
            set 
            { 
                _quantityInCart = value;
                CallPropertyChange(nameof(QuantityInCart));
                CallPropertyChange(nameof(DisplayText));
            }
        }

        public string DisplayText
        {
            get
            {
                return $"{Product.Productname}({QuantityInCart})";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CallPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
