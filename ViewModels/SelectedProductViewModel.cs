using FreshFishDesktopMVVM.Helpers;
using FreshFishDesktopMVVM.Models;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace FreshFishDesktopMVVM.ViewModels
{
    public class SelectedProductViewModel : INotifyPropertyChanged
    {
        #region Fields  
        private ProductHelper productsHelper = new ProductHelper();
        private bool edited = true;
        private Products _selectedProduct;
        private Grid productTextBoxes;

        public Grid ProductTextBoxes
        {
            get { return productTextBoxes; }
            set { productTextBoxes = value; }
        }

        #endregion

        #region Properties
        public Products SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??= new RelayCommand(SaveProduct);
            }
        }
        #endregion

        #region Helpers
        private async void SaveProduct(object obj)
        {
            if (edited == false)
            {
                await productsHelper.AddProduct(
                    SelectedProduct?.ProductName ?? "no data",
                    SelectedProduct?.Price ?? "0",
                    SelectedProduct?.Date ?? "0/0/0000",
                    SelectedProduct?.Status ?? "no"
                    );
            }
            if (edited == true)
            {
                await productsHelper.UpdateProduct(
                    SelectedProduct.ID,
                    SelectedProduct.ProductName,
                    SelectedProduct.Price,
                    SelectedProduct.Date,
                    SelectedProduct.Status);
            }
            
        }

        #endregion
        public SelectedProductViewModel(Products product = null)
        {
            SelectedProduct = product;
            if (product == null)
            {
                SelectedProduct = new Products();
                edited = false;
            }
            
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
