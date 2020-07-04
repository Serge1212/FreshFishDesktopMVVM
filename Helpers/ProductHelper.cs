using Firebase.Database;
using Firebase.Database.Query;
using FreshFishDesktopMVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshFishDesktopMVVM.Helpers
{
    public class ProductHelper
    {
        double sum;//Змінна,яка буде містити суму проданих товарів і яка буде повертатися з методу GetPricesSumAsync()
        public FirebaseClient client = new FirebaseClient("https://freshfish-bf927.firebaseio.com");//поле для зв'язку з віддаленим сервером Firebase
        public async Task<List<Products>> GetAllProductsAsync()//метод отримання всіх продуктів
        {
            return (await client //за допомогою client отримуємо доступ до бд
                .Child("freshfish")//далі, звертаємось до вузла, який вміщує дані про продукти
                .OnceAsync<Products>()).Select(item => new Products
                {
                    ID = item.Object.ID,
                    ProductName = item.Object.ProductName,
                    Price = item.Object.Price,
                    Date = item.Object.Date,
                    Status = item.Object.Status
                }).ToList();//вибираємо кожен продукт по черзі і приводимо до списку
        }

        //Метод, який повертає суму проданих товарів
        public async Task<double> GetPricesSumAsync()
        {
            var productsList = await GetAllProductsAsync(); //отримуємо всі продукти

            var prices = from p in productsList
                         where p.Status == "Yes"//Якщо товар проданий, заносимо його у список prices
                         select p.Price;

            sum = prices.Sum(v => Convert.ToDouble(v));//заносимо отриману суму в змінну sum

            return sum;
        }

        public async Task AddProduct(string productNameValue, string priceValue, string dateValue, string statusValue)//метод додавання продуктів
        {
            await client
                .Child("freshfish/")//звернення до вузла продуктів
                .PostAsync(new Products()//додаємо передані в параметри методу дані на сервер
                {
                    ID = GetRandomId(),//отримання нового згенерованого айді
                    ProductName = productNameValue,
                    Price = priceValue,
                    Date = dateValue,
                    Status = statusValue,
                });
        }

        //Метод оновлення даних конкретного продукту
        public async Task UpdateProduct(string IDValue,
            string productNameValue,
            string priceValue,
            string dateValue,
            string statusValue)
        {
            var toUpdateProduct = (await client
                .Child("freshfish")
                .OnceAsync<Products>()).Where(a => a.Object.ID == IDValue).FirstOrDefault(); //шукаємо продукт за переданим в метод айді

            await client
                .Child("freshfish")
                .Child(toUpdateProduct.Key)//звертаємося до конкретного запису в сервері за ключем
                .PutAsync(new Products { ID = IDValue, ProductName = productNameValue, Price = priceValue, Date = dateValue, Status = statusValue });
        }

        //Метод видалення конкретного продукту
        public async Task DeleteProduct(string IDValue)
        {
            var toDeleteProduct = (await client
                .Child("freshfish")
                .OnceAsync<Products>()).Where(a => a.Object.ID == IDValue).FirstOrDefault();//шукаємо продукт за переданим в метод айді
            await client.Child("freshfish").Child(toDeleteProduct.Key).DeleteAsync();//видаляємо продукт, звертаючись до його ключа
        }
        //Метод генерування нового айді
        #region Random ID FOR PRODUCTS
        string GetRandomId()
        {
            Random rnd = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string x = new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
            const string nums = "123456789";
            string y = new string(Enumerable.Repeat(nums, 4)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());

            string sDate = DateTime.Now.ToString();
            DateTime value = (Convert.ToDateTime(sDate.ToString()));

            return x + y +
                value.Day.ToString() +
                value.Month.ToString() +
                value.Year.ToString() +
                value.Minute.ToString() +
                value.Hour.ToString() +
                value.Second.ToString() +
                "P";

        }
        #endregion
    }
}
