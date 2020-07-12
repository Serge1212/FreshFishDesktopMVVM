using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FreshFishDesktopMVVM.Models
{
    public class Workers : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Data Validation
        //Поле, яке слугує для відправки винятків при некоректному вводі, в даному випадку, воно нічого не відправляє, так як не потрібно:
        public string Error { get { return null; } }

        //Словник помилок, який використовується для підказок(ToolTip), які з'являються біля контролу для визначення конкретної помилки
        public Dictionary<string, string> WorkersErrorCollection { get; private set; } = new Dictionary<string, string>();

        //Поле, яке безпосередньо робить перевірки:
        public string this[string columnName]
        {
            get
            {
                //на початку ніяких помилок немає, тому null
                string result = null;
                //"перебираємо" ймовірні помилки
                switch (columnName)
                {
                    case "name":
                        if (string.IsNullOrEmpty(name)) //якщо ввід нульовий, тобто, немає ніякого значення
                        {
                            result = "Worker name cannot be empty";//"Ім'я користувача не може бути порожнім"
                        }
                        break;
                    case "surname":
                        if (string.IsNullOrEmpty(surname))
                        {
                            result = "Worker last name cannot be empty";
                        }
                        break;
                    case "patronymic":
                        if (string.IsNullOrEmpty(patronymic))
                        {
                            result = "Worker middle name cannot be empty";
                        }
                        break;
                    case "position":
                        if (string.IsNullOrEmpty(position))
                        {
                            result = "Worker position cannot be empty";
                        }
                        break;
                    case "salary":
                        if (string.IsNullOrEmpty(salary))
                        {
                            result = "Worker salary cannot be empty";
                        }
                        else if (salary.All(char.IsDigit) == false) //Якщо в полі "зарплата" не всі введені значення є числові - помилка
                        {
                            result = "Worker salary cannot include any letter";//"Зарплата працівника не може вміщувати будь-які літери"
                        }
                        break;
                    case "phonenumber":
                        if (string.IsNullOrEmpty(phonenumber))
                        {
                            result = "Worker phone number cannot be empty";
                        }
                        else if (phonenumber.All(char.IsDigit) == false)//Якщо в полі "номер телефону" не всі введені значення є числові - помилка
                        {
                            result = "Worker phone number cannot include any letter";
                        }
                        break;
                    case "address":
                        if (string.IsNullOrEmpty(address))
                        {
                            result = "Worker address cannot be empty";
                        }
                        break;
                }

                //Додавання помилок у словник
                if (WorkersErrorCollection.ContainsKey(columnName))//Якщо колекція вже має ключ(тобто, наше поле), більше його не треба створювати, натомість, додати тільки текст помилки
                {
                    WorkersErrorCollection[columnName] = result;
                }
                else if (result != null)
                    WorkersErrorCollection.Add(columnName, result);//Якщо колекція ще не має такого ключа - додати і ключ, і текст помилки

                OnPropertyChanged("WorkersErrorCollection");//Поле WorkersErrorCollection підписалось на подію OnPropertyChanged

                if (result != null)//якщо в полі result є помилка, показати в повідомленні "!"
                    return "!";
                else return "";//Якщо ні - нічого не показувати
            }
        }
        #endregion
        private string w_id;
        private string name;
        private string surname;
        private string patronymic;
        private string position;
        private string salary;
        private string phonenumber;
        private string address;
        private string additioninfo;

        public string W_id
        {
            get
            {
                return w_id;
            }
            set
            {
                w_id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }
        public string Patronymic
        {
            get
            {
                return patronymic;
            }
            set
            {
                patronymic = value;
                OnPropertyChanged("Patronymic");
            }
        }
        public string Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }
        public string Salary
        {
            get
            {
                return salary;
            }
            set
            {
                salary = value;
                OnPropertyChanged("Salary");
            }
        }
        public string PhoneNumber
        {
            get
            {
                return phonenumber;
            }
            set
            {
                phonenumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }
        public string AdditionalInfo
        {
            get
            {
                return additioninfo;
            }
            set
            {
                additioninfo = value;
                OnPropertyChanged("AdditionalInfo");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;//Подія, яка буде відслідковувати зміни при введенні

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));//коли виникла зміни, викликається потік
        }

        public override string ToString()//Перевизначення методу для подальшого відображення у графічному інтерфейсі
        {
            return name + " " + surname;
        }
    }
}

