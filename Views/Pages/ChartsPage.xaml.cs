
using FreshFishDesktopMVVM.Helpers;
using FreshFishDesktopMVVM.Models;
using FreshFishDesktopMVVM.ViewModels;
using LiveCharts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts.Wpf;
using LiveCharts.Helpers;

namespace FreshFishDesktopMVVM.Views.Pages
{
    /// <summary>
    /// Interaction logic for ChartsPage.xaml
    /// </summary>
    public partial class ChartsPage : Page
    {
        List<Products> ProductsList = new List<Products>();
        List<Workers> WorkersList = new List<Workers>();
        ProductHelper pHelper = new ProductHelper();
        WorkersHelper wHelper = new WorkersHelper();

        public SeriesCollection SeriesCollection { get; set; }
        public Func<double, string> Formatter { get; set; }
        //public Func<ChartPoint, string> PointLabel { get; set; }

        public List<double> WorkersSalaries;
        public List<string> WorkersNames { get; set; }

        public ChartsPage()
        {
            InitializeComponent();

            //PointLabel = chartPoint =>
            //    string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        }

        async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            #region Pie Chart Sold/Unsold relationship
            ProductsList = await pHelper.GetAllProductsAsync();

            int countSold = (from s in ProductsList
                             where s.Status.ToLower() == "yes"
                             select s).Count();
            int countUnsold = (from u in ProductsList
                               where u.Status.ToLower() == "no"
                               select u).Count();


            SoldPiece.Values = new ChartValues<int> { countSold };
            SoldPiece.LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            UnsoldPiece.Values = new ChartValues<int> { countUnsold };
            UnsoldPiece.LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            #endregion
            WorkersList = await wHelper.GetAllWorkersAsync();

            WorkersSalaries = (from w in WorkersList
                               select Convert.ToDouble(w.Salary)).ToList();

            SeriesCollection = new SeriesCollection
            {
                new RowSeries
                {
                    Title = "Salary",
                    Values = WorkersSalaries.AsChartValues()
                }
            };

            WorkersNames = (from n in WorkersList
                            select n.Name + " " + n.Surname).ToList();

            Formatter = value => value.ToString("N");


            DataContext = this;
        }

        private void AverageSalaryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AverageSalaryTextBox.Text.All(char.IsDigit) && AverageSalaryTextBox.Text != string.Empty)
            {
                double avSalary = Convert.ToDouble(AverageSalaryTextBox.Text);
                int HigherSalaryCount = (from h in WorkersSalaries
                                         where Convert.ToDouble(h) >= avSalary
                                         select h).Count();
                int LowerSalaryCount = (from l in WorkersSalaries
                                        where Convert.ToDouble(l) < avSalary
                                        select l).Count();
                UpSalary.Values = new ChartValues<double> { HigherSalaryCount };
                UpSalary.LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
                DownSalary.Values = new ChartValues<double> { LowerSalaryCount };
                DownSalary.LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            }
        }
    }
}
