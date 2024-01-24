using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Индивидуальное_задание
{
    public class TaskManager
    {
        private string filePath; //Поле названия текстого файла с задачами

        public TaskManager(string path) //Конструктор для инициализации задачи
        {
            filePath = path;
            // Инициализируем файл, если он ещё не создан
            if (!File.Exists(path))
            {
                File.WriteAllText(filePath, null);
            }
        }
        public List<Task> LoadTasks(string owner)  //Метод чтение текста из файла и десериализация в список задач
        {
            string json = File.ReadAllText(filePath);
            List<Task> tasks = JsonConvert.DeserializeObject<List<Task>>(json) ?? new List<Task>();
            if (owner != null)
            { return tasks.Where(t => t.Owner == owner).ToList(); }
            return tasks.ToList();
        }
        public void SaveTasks(List<Task> tasks, string owner) //Метод сериализация списка задач в текст и сохранение текста в файл
        {
            var allTasks = LoadTasks(null);
            allTasks.RemoveAll(t => t.Owner == owner);
            allTasks.AddRange(tasks.Where(t => t.Owner == owner));
            string json = JsonConvert.SerializeObject(allTasks, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
