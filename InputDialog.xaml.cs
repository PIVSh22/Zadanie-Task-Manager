using System.Windows;

namespace Индивидуальное_задание
{
    public partial class InputDialog : Window
    {
        public string ResponseText //Поле введенного текста
        {
            get { return ResponseTextBox.Text; }
            set { ResponseTextBox.Text = value; }
        }
        public InputDialog(string question) //Конструктор для инициализации компонентов окна
        {
            InitializeComponent();
            QuestionTextBlock.Text = question;
        }
        private void okButton_Click(object sender, RoutedEventArgs e) //Метод обработки клика по кнопке ОК
        {
            DialogResult = true;
        }
    }
}
