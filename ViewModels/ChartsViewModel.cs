using Firebase.Database;
using FreshFishDesktopMVVM.Helpers;
using FreshFishDesktopMVVM.Models;
using FreshFishDesktopMVVM.Utilities;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FreshFishDesktopMVVM.ViewModels
{
    public class ChartsViewModel : ObservableObject
    {
        //private ProductHelper pHelper = new ProductHelper();
        //private ObservableCollection<Products> ProductsCollection { get; set; }
        //public Func<ChartPoint, string> PointLabel { get; set; }

        //private List<Products> _productsList;
        //public List<Products> ProductsList
        //{
        //    get
        //    {
        //        return _productsList;
        //    }
        //    set
        //    {
        //        _productsList = value;
        //        OnPropertyChanged("ProductsList");
        //    }
        //}

        //private int _countSold;
        //public int CountSold
        //{
        //    get
        //    {
        //        return _countSold;
        //    }
        //    set
        //    {
        //        _countSold = value;
        //        OnPropertyChanged("CountSold");
        //    }
        //}

        //private int _countUnsold;
        //public int CountUnsold
        //{
        //    get
        //    {
        //        return _countUnsold;
        //    }
        //    set
        //    {
        //        _countUnsold = value;
        //        OnPropertyChanged("CountUnsold");
        //    }
        //}

        //private RelayCommand _loadedCommand;

        //public RelayCommand LoadedCommand
        //{
        //    get
        //    {
        //        return _loadedCommand ??= new RelayCommand(async (obj) => await GetAllData());
        //    }
        //}

        //private async Task GetAllData()
        //{
        //    //ProductsList = await pHelper.GetAllProductsAsync();
        //    //CountSold = (from s in ProductsList
        //    //             where s.Status.ToLower() == "yes"
        //    //             select s).Count();

        //    //CountUnsold = (from s in ProductsList
        //    //               where s.Status.ToLower() == "no"
        //    //               select s).Count();
        //}
        //public ChartsViewModel()
        //{
        //    ProductsCollection = pHelper.client
        //          .Child("freshfish")
        //          .AsObservable<Products>()
        //          .ObserveOnDispatcher()
        //          .AsObservableCollection();
        //    CountSold = (from s in ProductsCollection
        //                 where s.Status.ToLower() == "yes"
        //                 select s).Count();

        //    CountUnsold = (from s in ProductsCollection
        //                   where s.Status.ToLower() == "no"
        //                   select s).Count();
        //    PointLabel = chartPoint =>
        //        string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

        }
    }

