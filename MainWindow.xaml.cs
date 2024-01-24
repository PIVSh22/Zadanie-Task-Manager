using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Индивидуальное_задание
{
    public partial class MainWindow : Window
    {
        private TaskManager taskManager; //Поле присвоения класса TaskManager
        private List<Task> tasks; //Поле присвоения ссылки на поля класса Task
        private string currentUser;

        public MainWindow(string username)
        {
            InitializeComponent();
            currentUser = username;
            taskManager = new TaskManager("tasks.json");
            LoadTasks(currentUser);
        }
        private void LoadTasks(string owner) //Метод загрузки списка дел в соответствующие списки
        {
            tasks = taskManager.LoadTasks(owner);
            ToDoListBox.ItemsSource = tasks.Where(t => t.Status == TaskStatus.Сделать).ToList();
            InProgressListBox.ItemsSource = tasks.Where(t => t.Status == TaskStatus.В_процессе).ToList();
            DoneListBox.ItemsSource = tasks.Where(t => t.Status == TaskStatus.Готово).ToList();
        }
        private void DeleteTask_Click(object sender, RoutedEventArgs e) //Метод удаление выбранного дела из списка "Готово"
        {
            Task selectedTask = DoneListBox.SelectedItem as Task;
            if (selectedTask != null)
            {
                tasks.Remove(selectedTask);
                taskManager.SaveTasks(tasks, currentUser);
                LoadTasks(currentUser);
            }
        }
        private void AddTask_Click(object sender, RoutedEventArgs e) //Метод обработки клика по кнопке добавления дела
        {
            var inputDialog = new InputDialog("Введите описание дела:");
            if (inputDialog.ShowDialog() == true)
            {
                if (inputDialog.ResponseText == "") { MessageBox.Show("Вы ничего не ввели!\nВведите дело!"); return; }
                Task newTask = new Task
                {
                    Description = inputDialog.ResponseText,
                    Date = DateTime.Now,
                    Owner = currentUser,
                    Status = TaskStatus.Сделать
                };
                tasks.Add(newTask);
                taskManager.SaveTasks(tasks, currentUser);
                LoadTasks(currentUser);
            }
        }
        private void MoveTaskInProgress_Click(object sender, RoutedEventArgs e) //Метод обработки клика по кнопке перемещения дела в "В процессе"
        {
            MoveTask(ToDoListBox, TaskStatus.В_процессе);
        }
        private void MoveTaskDone_Click(object sender, RoutedEventArgs e) //Метод обработки клика по кнопке перемещения дела в "Готово"
        {
            MoveTask(InProgressListBox, TaskStatus.Готово);
        }
        private void MoveTask(ListBox listBox, TaskStatus targetStatus) // Общий метод для перемещения задач между статусами
        {
            Task selectedTask = listBox.SelectedItem as Task;
            if (selectedTask != null)
            {
                selectedTask.Status = targetStatus;
                taskManager.SaveTasks(tasks, currentUser);
                LoadTasks(currentUser);
            }
        }

    }
}
