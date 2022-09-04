using Caliburn.Micro;
using NRMDesktopUI.library.API;
using NRMDesktopUI.library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        IProductEndPoint _productEndPoint;
       
        public  SalesViewModel(IProductEndPoint productEndPoint)
        {
            _productEndPoint = productEndPoint;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProduct();
        }
        public async Task LoadProduct()
        {
            var productList = await _productEndPoint.GetAll();
            Product = new BindingList<ProductModel>(productList);
        }

        private BindingList<ProductModel> _product;


        public BindingList<ProductModel> Product
        {
            get { return _product; }
            set
            {
                _product = value;
                NotifyOfPropertyChange(() => Product);
            }
        }

        private BindingList<string> _cart;

        public BindingList<string> Cart
        {
            get { return _cart; }
            set { _cart = value; }
        }


        private string _itemQuantity;

        public string ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
            }
        }


        public string SubTotal
        {
            get
            {
                //Todo - replace it calculation
                return "$0.00";
            }
        }
        public string Tax
        {
            get
            {
                //Todo - replace it calculation
                return "$0.00";
            }
        }

        public string Total
        {
            get
            {
                //Todo - replace it calculation
                return "$0.00";
            }
        }


        public bool CanAddToCart
        {
            get
            {
                bool output = false;
                //Make sure something is selected
                return output;
            }
        }

        public void AddToCart()
        {

        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;
                //Make sure something is selected
                return output;
            }
        }

        public void RemoveFromCart()
        {

        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;
                //Make sure something is on the cart
                return output;
            }
        }

        public void Checkout()
        {

        }

    }
}
