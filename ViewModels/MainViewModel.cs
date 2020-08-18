using FreshFishDesktopMVVM.Models;
using FreshFishDesktopMVVM.Utilities;
using FreshFishDesktopMVVM.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FreshFishDesktopMVVM.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private Page ProductsPage;
        private Page WorkersPage;
        private Page IncomePage;
        private Page ChartsPage;
        private Page BreedingPage;
        private Page _currentPage;
        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        private int index;
        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
                OnPropertyChanged("Index");
            }
        }

        public MainViewModel()
        {
            ProductsPage = new Views.Pages.ProductsPage();
            WorkersPage = new Views.Pages.WorkersPage();
            IncomePage = new Views.Pages.IncomePage();
            ChartsPage = new Views.Pages.ChartsPage();
            BreedingPage = new Views.Pages.FishBreedingPage();

            CurrentPage = WorkersPage;
            Index = 0;
        }

        private RelayCommand openWorkersPage;
        public RelayCommand OpenWorkersPage
        {
            get
            {
                return openWorkersPage ??= new RelayCommand((obj) =>
                {
                    CurrentPage = WorkersPage;
                    Index = 0;
                });
            }

        }

        private RelayCommand openProductsPage;
        public RelayCommand OpenProductsPage
        {
            get
            {
                return openProductsPage ??= new RelayCommand((obj) =>
                {
                    CurrentPage = ProductsPage;
                    Index = 1;
                });
            }
        }
        private RelayCommand openIncomePage;
        public RelayCommand OpenIncomePage
        {
            get
            {
                return openIncomePage ??= new RelayCommand((obj) =>
                {
                    CurrentPage = IncomePage;
                    Index = 2;
                });
            }
        }
        private RelayCommand openChartsPage;
        public RelayCommand OpenChartsPage
        {
            get
            {
                return openChartsPage ??= new RelayCommand((obj) =>
                {
                    CurrentPage = new ChartsPage();
                    Index = 3;
                });
            }
        }
        private RelayCommand openFishBreedingPage;
        public RelayCommand OpenFishBreedingPage
        {
            get
            {
                return openFishBreedingPage ??= new RelayCommand((obj) =>
                {
                    CurrentPage = BreedingPage;
                    Index = 4;
                });
            }
        }
    }
}
