using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace FreshFishDesktopMVVM.Models
{
    public class Products : INotifyPropertyChanged, IDataErrorInfo
    {
        public string Error { get { return null; } }

        public Dictionary<string, string> ProductsErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string this[string columnName]
        {
            get
            {
                string result = null;

                switch (columnName)
                {
                    case "productname":
                        if (string.IsNullOrEmpty(productname))
                        {
                            result = "Product name cannot be empty";
                        }
                        break;
                    case "price":
                        Regex regex = new Regex(@"^[0-9.]+$");//дозволяє тільки цифри і крапку
                        //Match match;
                        if (string.IsNullOrEmpty(price))
                        {
                            result = "Price cannot be empty";
                        }
                        else if (!regex.IsMatch(price))
                        {
                            result = "Price can contain only digits and a single dot";
                        }
                        break;
                    case "status":
                        if (string.IsNullOrEmpty(status))
                        {
                            result = "Status cannot be empty";
                        }
                        break;
                }
                if (ProductsErrorCollection.ContainsKey(columnName))
                {
                    ProductsErrorCollection[columnName] = result;
                }
                else if (result != null)
                    ProductsErrorCollection.Add(columnName, result);

                OnPropertyChanged("ProductsErrorCollection");

                if (result != null)
                    return "!";
                else return "";
            }
        }

        private string id { get; set; }
        private string productname { get; set; }
        private string price { get; set; }
        private string date { get; set; }
        private string status { get; set; }

        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        public string ProductName
        {
            get { return productname; }
            set
            {
                productname = value;
                OnPropertyChanged("ProductName");
            }
        }

        public string Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }

        public string Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
