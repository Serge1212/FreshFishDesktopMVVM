using FreshFishDesktopMVVM.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FreshFishDesktopMVVM.ViewModels
{
    public class IncomeViewModel : INotifyPropertyChanged
    {
        class Rate
        {
            [JsonProperty("ccy")]
            public string Ccy { get; set; }

            [JsonProperty("base_ccy")]
            public string BaseCcy { get; set; }

            [JsonProperty("buy")]
            public double Buy { get; set; }

            [JsonProperty("sale")]
            public string Sale { get; set; }
        }

        private ProductHelper helper = new ProductHelper();
        List<Rate> rates;
        private double _calculatedIncome;
        public double CalculatedIncome
        {
            get
            {
                return _calculatedIncome;
            }
            set
            {
                _calculatedIncome = value;
                OnPropertyChanged("CalculatedIncome");
            }
        }

        private string _dollar;
        public string Dollar
        {
            get
            {
                return _dollar;
            }
            set
            {
                _dollar = value;
                OnPropertyChanged("Dollar");
            }
        }

        private string _euro;
        public string Euro
        {
            get
            {
                return _euro;
            }
            set
            {
                _euro = value;
                OnPropertyChanged("Euro");
            }
        }

        private string _ruble;
        public string Ruble
        {
            get
            {
                return _ruble;
            }
            set
            {
                _ruble = value;
                OnPropertyChanged("Ruble");
            }
        }

        private string _bitcoin;
        public string Bitcoin
        {
            get
            {
                return _bitcoin;
            }
            set
            {
                _bitcoin = value;
                OnPropertyChanged("Bitcoin");
            }
        }

        private RelayCommand _getIncomeCommand;
        public RelayCommand GetIncomeCommand
        {
            get
            {
                return _getIncomeCommand ??= new RelayCommand(async (obj) => await GetIncomeAsync());
            }
        }

        async Task GetIncomeAsync()
        {
            CalculatedIncome = await helper.GetPricesSumAsync();
            await Task.Run(() => { rates = GetCurrencyRates(); });
            try
            {
                Dollar = (CalculatedIncome / rates[0].Buy).ToString("F2");
                Euro = (CalculatedIncome / rates[1].Buy).ToString("F2");
                Ruble = (CalculatedIncome / rates[2].Buy).ToString("F2");
                Bitcoin = (CalculatedIncome / rates[0].Buy / rates[3].Buy).ToString();
            }
            catch
            {
                Dollar = "0";
                Euro = "0";
                Ruble= "0";
                Bitcoin= "0";

                MessageBox.Show("Не вдалося конвертувати валюти!");
            }

        }

        List<Rate> GetCurrencyRates()
        {
            string json;
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString("https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5");
            }

            return JsonConvert.DeserializeObject<List<Rate>>(json);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
