using System;

namespace Индивидуальное_задание
{
    public enum TaskStatus //Набор целочисленных констант
    {
        Сделать,
        В_процессе,
        Готово
    }
    public class Task
    {
        public string Description { get; set; } //Поле описание задачи
        public DateTime Date { get; set; } //Поле даты
        public string Owner { get; set; } //Поле исполнителя
        public TaskStatus Status { get; set; } //Поле статуса
        public Task() { } // Конструктор по умолчанию
        public Task(string description, DateTime date, string owner, TaskStatus status) // Конструктор для инициализации дела
        {
            Description = description;
            Date = date;
            Owner = owner;
            Status = status;
        }
        public override string ToString() //Метод, который выводит нужные поля в listBox
        {
            return $"Описание дела: {Description}\nДата создания: {Date}, Исполнитель: {Owner}, Статус: {Status}";
        }
    }
}
