using System;
using System.Text;

namespace Индивидуальное_задание
{
    public class User
    {
        public string Username { get; set; } //Поле логина пользователя
        public string PasswordHash { get; set; } //Поле пароля пользователя

        public User(string username, string password) //Конструктор инициализации логина и пароля пользователя
        {
            Username = username;
            if (PasswordHash == null & password == null)
            {
                return;
            }
            PasswordHash = HashPassword(password);
        }
        private string HashPassword(string password) //Метод шифрование пароля пользователя
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
