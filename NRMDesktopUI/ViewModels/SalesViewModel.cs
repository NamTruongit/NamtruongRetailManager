using Caliburn.Micro;
using NRMDesktopUI.library.API;
using NRMDesktopUI.library.Helpers;
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
        IConfigHelper _configHelper;
        ISaleEndPoint _saleEndPoint; 
        public  SalesViewModel(IProductEndPoint productEndPoint, IConfigHelper configHelper,ISaleEndPoint saleEndPoint)
        {
            _productEndPoint = productEndPoint;
            _configHelper = configHelper;
            _saleEndPoint = saleEndPoint;
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


        private int _itemQuantity ;

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
                return CalculatedSubTotal().ToString("C",CultureInfo.GetCultureInfo("en-us"));
            }
        }

        private decimal CalculatedSubTotal()
        {
            decimal subtotal = 0;

            foreach (var item in _cart)
            {
                subtotal += (item.Product.RetailPrice * item.QuantityInCart);
            }
            return subtotal;

        }

        public string Tax
        {
            get
            {
                //Todo - replace it calculation
                
                return CalculatedSubTax().ToString("C", CultureInfo.GetCultureInfo("en-us"));
            }
        }

        private decimal CalculatedSubTax()
        {
            decimal taxAmount = 0;
            decimal taxRate = _configHelper.GetTaxRate()/100;

            taxAmount = Cart
                .Where(x => x.Product.IsTaxable)
                .Sum(x => x.Product.RetailPrice * x.QuantityInCart * taxRate);
            //foreach (var item in _cart)
            //{
            //    if (item.Product.IsTaxable)
            //    {
            //        taxAmount += (item.Product.RetailPrice * item.QuantityInCart * taxRate);
            //    }
            //}
            return taxAmount;
        }

        public string Total
        {
            get
            {
                //Todo - replace it calculation
                decimal total = CalculatedSubTotal()+CalculatedSubTax();
                return total.ToString("C", CultureInfo.GetCultureInfo("en-us"));
            }
        }


        public bool CanAddToCart
        {
            get
            {
                bool output = false;
                //Make sure something is selected
                if (ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity)
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
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
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
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
        }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;
                //Make sure something is on the cart    
                if (Cart.Count > 0)
                {
                    output = true;
                }
                return output;
            }
        }

        public async Task CheckOut()
        {
            // Create a saleModel and post to the API
            SaleModel sale = new SaleModel();
            foreach (var item in Cart)
            {
                sale.SaleDetails.Add(new SaleDetailModel
                {
                    ProducID = item.Product.Id,
                    Quantity = item.QuantityInCart
                });
            }
            await _saleEndPoint.PostSale(sale);
        }

    }
}
