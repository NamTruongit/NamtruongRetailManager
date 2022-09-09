using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRMDesktopUI.Models
{
    public class ProducDisplayModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Productname { get; set; }
        public string Decription { get; set; }
        public decimal RetailPrice { get; set; }

        private int _quantityInStock;

        public int QuantityInStock
        {
            get { return _quantityInStock; }
            set
            { 
                _quantityInStock = value;
                CallPropertyChange(nameof(QuantityInStock));
            }
        }

        public bool IsTaxable { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CallPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
