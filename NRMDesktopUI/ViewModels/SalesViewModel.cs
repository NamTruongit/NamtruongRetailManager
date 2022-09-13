using AutoMapper;
using Caliburn.Micro;
using NRMDesktopUI.library.API;
using NRMDesktopUI.library.Helpers;
using NRMDesktopUI.library.Models;
using NRMDesktopUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        IProductEndPoint _productEndPoint;
        IConfigHelper _configHelper;
        ISaleEndPoint _saleEndPoint;
        IMapper _mapper;
        private readonly StatusInfoViewModel _status;
        private readonly IWindowManager _windown;

        public  SalesViewModel(IProductEndPoint productEndPoint, IConfigHelper configHelper,ISaleEndPoint saleEndPoint,IMapper mapper,StatusInfoViewModel status,IWindowManager windown)
        {
            _productEndPoint = productEndPoint;
            _configHelper = configHelper;
            _saleEndPoint = saleEndPoint;
            _mapper = mapper;
            _status = status;
            _windown = windown;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {
                await LoadProduct();
            }
            catch (Exception ex)
            {
                dynamic setting = new ExpandoObject();
                setting.WindowStartUpLocation = WindowStartupLocation.CenterOwner;
                setting.ReSizeMode = ResizeMode.NoResize;
                setting.Title = "System Error";

                if (ex.Message == "Unauthorized")
                {
                    _status.UpdateMessage("Unauthorized Access", "You do not have permission to interact with the sale Form.");
                    _windown.ShowDialog(_status, null, setting);
                }
                else
                {
                    _status.UpdateMessage("Fatal Exception", ex.Message);
                    _windown.ShowDialog(_status, null, setting);
                }



                TryClose();
            }
        }

        private async Task ResetSaleViewModel()
        {
            Cart = new BindingList<CartItemDisplayModel>();

            await LoadProduct();

            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(()=> CanAddToCart);
            NotifyOfPropertyChange(() => CanCheckOut);
        }

        private async Task LoadProduct()
        {
            var productList = await _productEndPoint.GetAll();
            var products = _mapper.Map<List<ProducDisplayModel>>(productList);
            Product = new BindingList<ProducDisplayModel>(products);
        }

        private BindingList<ProducDisplayModel> _product;


        public BindingList<ProducDisplayModel> Product
        {
            get { return _product; }
            set
            {
                _product = value;
                NotifyOfPropertyChange(() => Product);
            }
        }

        private ProducDisplayModel _selectedProduct;

        public ProducDisplayModel SelectedProduct
        {
            get { return _selectedProduct; }
            set 
            {
                _selectedProduct = value;
                 NotifyOfPropertyChange(()=>SelectedProduct);
            }
        }

        private CartItemDisplayModel _selectedCartItem;

        public CartItemDisplayModel SelectedCartItem
        {
            get { return _selectedCartItem; }
            set
            {
                _selectedCartItem = value;
                NotifyOfPropertyChange(() => SelectedCartItem);
                NotifyOfPropertyChange(() => CanRemoveFromCart);
            }
        }


        private BindingList<CartItemDisplayModel> _cart = new BindingList<CartItemDisplayModel>();

        public BindingList<CartItemDisplayModel> Cart
        {
            get { return _cart; }
            set
            { 
                _cart = value;
                NotifyOfPropertyChange(() =>Cart);
            }
        }


        private int _itemQuantity = 1 ;

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
            CartItemDisplayModel exitingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);
            if (exitingItem != null)
            {
                exitingItem.QuantityInCart += ItemQuantity;

            }
            else
            {
                CartItemDisplayModel item = new CartItemDisplayModel
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
                if (SelectedCartItem != null && SelectedCartItem?.QuantityInCart > 0)
                {
                    output = true;
                }
                return output;
            }
        }

        public void RemoveFromCart()
        {
            SelectedCartItem.Product.QuantityInStock += 1;
            if (SelectedCartItem.QuantityInCart > 1)
            {
                SelectedCartItem.QuantityInCart -= 1;
            }
            else
            {
                Cart.Remove(SelectedCartItem);
            }
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
            NotifyOfPropertyChange(() => CanAddToCart);
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

            await ResetSaleViewModel(); 
        }

    }
}
