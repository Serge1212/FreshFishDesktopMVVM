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
                    case "ProductName":
                        if (string.IsNullOrEmpty(productname) && productNamechanged)
                        {
                            result = "Product name cannot be empty";
                        }
                        break;
                    case "Price":
                        Regex regex = new Regex(@"^[0-9.]+$");//дозволяє тільки цифри і крапку
                        if (priceChanged)
                        {
                            if (string.IsNullOrEmpty(price))
                            {
                                result = "Price cannot be empty";
                            }
                            else if (!regex.IsMatch(price))
                            {
                                result = "Price can contain only digits and a single dot";
                            }
                        }
                        break;
                    case "Status":
                        if (string.IsNullOrEmpty(status) && statusChanged)
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

                if (result != null) CanSave = false;
                else CanSave = true;

                OnPropertyChanged("ProductsErrorCollection");

                return result == null ? string.Empty : "!";

                
            }
        }
        private bool canSave;
        public bool CanSave
        {
            get
            {
                return canSave;
            }
            set
            {
                canSave = value;
                OnPropertyChanged("CanSave");
            }
        }

        private bool productNamechanged = false;
        private bool priceChanged = false;
        private bool statusChanged = false;
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
                productNamechanged = true;
            }
        }

        public string Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
                priceChanged = true;
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
                statusChanged = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
