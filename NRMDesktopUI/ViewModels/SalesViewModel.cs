using Caliburn.Micro;
using NRMDesktopUI.library.API;
using NRMDesktopUI.library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
        private async Task LoadProduct()
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

        private ProductModel _selectedProduct;

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set 
            {
                _selectedProduct = value;
                 NotifyOfPropertyChange(()=>SelectedProduct);
            }
        }


        private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();

        public BindingList<CartItemModel> Cart
        {
            get { return _cart; }
            set { _cart = value; }
        }


        private int _itemQuantity = 1;

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }


        public string SubTotal
        {
            get
            {
                //Todo - replace it calculation
                decimal subtotal = 0M;

                foreach (var item in _cart)
                {
                    subtotal += (item.Product.RetailPrice * item.QuantityInCart);
                }

                return subtotal.ToString("C",CultureInfo.GetCultureInfo("en-us"));
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
                if (ItemQuantity > 0&& SelectedProduct?.QuantityInStock >= ItemQuantity)
                {
                    output = true;
                }
                return output;
            }
        }

        public void AddToCart()
        {
            CartItemModel exitingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);
            if (exitingItem != null)
            {
                exitingItem.QuantityInCart += ItemQuantity;
                //// Hack - there should be better way of refreshing cart display
                Cart.Remove(exitingItem);
                Cart.Add(exitingItem);
            }
            else
            {
                CartItemModel item = new CartItemModel()
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity,
                };
                Cart.Add(item);
            }
            
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(()=>SubTotal);
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
            NotifyOfPropertyChange(() => SubTotal);
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
