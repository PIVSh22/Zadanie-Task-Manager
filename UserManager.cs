using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Индивидуальное_задание
{
    // Класс для управления пользователями
    public class UserManager
    {
        private List<User> users; //Поле, получающее ссылки на поля класса User
        private string usersFilePath = "users.json"; //Поле названия файла содержащего логины и пароли пользователей

        public UserManager() //Конструктор для инициализации пользователей
        {
            users = LoadUsers() ?? new List<User>();
        }
        private List<User> LoadUsers() //Метод загрузки логинов и паролей пользователей из файла, путем десериализации
        {
            if (File.Exists(usersFilePath))
            {
                string json = File.ReadAllText(usersFilePath);
                return JsonConvert.DeserializeObject<List<User>>(json);
            }
            return null;
        }
        public void SaveUsers() //Метод сохранения логинов и паролей пользователей в файл, путем сериализации
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(usersFilePath, json);
        }
        public bool RegisterUser(string username, string password) //Метод регистрации пользователей
        {
            if (users.Any(u => u.Username == username))
            {
                return false; // Имя пользователя уже существует
            }

            users.Add(new User(username, password));
            SaveUsers();
            return true;
        }
        public bool ValidateUser(string username, string password) //Метод аутентификации зарегестрированного пользователя
        {
            var user = users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return false; // Пользователь не найден
            }
            return user.PasswordHash == HashPassword(password);
        }
        private string HashPassword(string password)  //Метод шифрования пароля пользователя
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }

}
