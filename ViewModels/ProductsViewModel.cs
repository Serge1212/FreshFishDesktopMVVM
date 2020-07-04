using Firebase.Database;
using FreshFishDesktopMVVM.Helpers;
using FreshFishDesktopMVVM.Models;
using FreshFishDesktopMVVM.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;

namespace FreshFishDesktopMVVM.ViewModels
{
    public class ProductsViewModel : INotifyPropertyChanged
    {

        private ProductHelper productHelper = new ProductHelper();
        static bool executed = true;
        public ObservableCollection<Products> ProductsCollection { get; set; }
        private Products _selectedProduct;
        public Products SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??= new RelayCommand(OpenWindowForNewProduct);
            }
        }

        private RelayCommand editCommand;

        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??= new RelayCommand(OpenWindowForSelectedProductEdit, (obj) => SelectedProduct != null);
            }         
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??= new RelayCommand(async obj =>
                  {
                      Products product = obj as Products;
                      if (product != null)
                      {
                          await productHelper.DeleteProduct(product.ID);
                      }
                  },
                 (obj) => ProductsCollection.Count > 0 && SelectedProduct != null);
            }
        }

        private void OpenWindowForSelectedProductEdit(object obj)
        {
            var selectedProductWindow = new SelectedProductWindow();
            var selectedProductViewModel = new SelectedProductViewModel(SelectedProduct);
            selectedProductWindow.DataContext = selectedProductViewModel;
            selectedProductWindow.Show();
        }

        private void OpenWindowForNewProduct(object obj)
        {
            var selectedProductWindow = new SelectedProductWindow();
            var selectedProductViewModel = new SelectedProductViewModel(null);
            selectedProductWindow.DataContext = selectedProductViewModel;
            selectedProductWindow.Show();
            
          
        }

        public ProductsViewModel()
        {
            if (executed)
            {
                ProductsCollection = productHelper.client
                .Child("freshfish")
                .AsObservable<Products>()
                .ObserveOnDispatcher()
                .AsObservableCollection();

                executed = false;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
