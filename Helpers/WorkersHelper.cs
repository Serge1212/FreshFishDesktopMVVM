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
    public class WorkersHelper
    {
        FirebaseClient client = new FirebaseClient("https://freshfish-bf927.firebaseio.com");//поле для зв'язку з віддаленим сервером Firebase

        //отримання всіх даних працівників з серверу
        public async Task<List<Workers>> GetAllWorkersAsync()
        {
            return (await client
                .Child("workers")
                .OnceAsync<Workers>()).Select(item => new Workers
                {
                    W_id = item.Object.W_id,
                    Name = item.Object.Name,
                    Surname = item.Object.Surname,
                    Patronymic = item.Object.Patronymic,
                    Position = item.Object.Position,
                    Salary = item.Object.Salary,
                    PhoneNumber = item.Object.PhoneNumber,
                    Address = item.Object.Address,
                    AdditionalInfo = item.Object.AdditionalInfo
                }).ToList();
        }

        //додавання нового працівника
        public async Task AddWorker(
            string name,
            string surname,
            string patronymic,
            string position,
            string salary,
            string phonenumber,
            string address,
            string additioninfo
            )
        {

            await client
                .Child("workers/")
                .PostAsync(new Workers()
                {
                    W_id = GetRandomId(),//отримуємо нове згенероване айді
                    Name = name,
                    Surname = surname,
                    Patronymic = patronymic,
                    Position = position,
                    Salary = salary,
                    PhoneNumber = phonenumber,
                    Address = address,
                    AdditionalInfo = additioninfo
                });
        }

        //отримання конкретного працівника за айді
        public async Task<Workers> GetWorker(string ID)
        {
            var allWorkers = await GetAllWorkersAsync();
            await client
                .Child("workers")
                .OnceAsync<Workers>();

            return allWorkers.Where(w => w.W_id == ID).FirstOrDefault();
        }

        //оновлення даних конкретного працівника
        public async Task UpdateWorker(
            string id,
            string name,
            string surname,
            string patronymic,
            string position,
            string salary,
            string phonenumber,
            string address,
            string additioninfo)
        {
            var toUpdateProduct = (await client
               .Child("workers")
               .OnceAsync<Workers>()).Where(a => a.Object.W_id == id).FirstOrDefault();

            await client
                .Child("workers")
                .Child(toUpdateProduct.Key)
                .PutAsync(new Workers
                {
                    W_id = id,
                    Name = name,
                    Surname = surname,
                    Patronymic = patronymic,
                    Position = position,
                    Salary = salary,
                    PhoneNumber = phonenumber,
                    Address = address,
                    AdditionalInfo = additioninfo

                });
        }
        //видалення конкретного працівника за айді
        public async Task DeleteWorker(string ID)
        {
            var toDeleteWorker = (await client
                .Child("workers")
                .OnceAsync<Workers>()).Where(w => w.Object.W_id == ID).FirstOrDefault();
            await client.Child("workers").Child(toDeleteWorker.Key).DeleteAsync();
        }


        //генерування нового айді
        #region Random ID FOR WORKERS
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
                "w";

        }
        #endregion
    }
}
