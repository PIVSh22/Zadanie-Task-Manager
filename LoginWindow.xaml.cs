using System.Windows;
using System.Windows.Controls;

namespace Индивидуальное_задание
{
    public partial class LoginWindow : Window
    {
        private UserManager userManager; //Поле присваивания класса UserManager

        public LoginWindow() //Конструктор инициализации окна аутентификации
        {
            InitializeComponent();
            userManager = new UserManager();
        }
        private void Login_Click(object sender, RoutedEventArgs e) //Метод аутентификации пользователя в системе
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;
            if (userManager.ValidateUser(username, password))
            {
                // Пользователь аутентифицирован
                var mainWindow = new MainWindow(username);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.");
            }
        }
        private void Register_Click(object sender, RoutedEventArgs e) //Метод регистрации пользователя в системе
        {
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            if (userManager.RegisterUser(username, password))
            {
                MessageBox.Show("Пользователь зарегистрирован.");
            }
            else
            {
                MessageBox.Show("Имя пользователя уже занято.");
            }
        }
    }
}
