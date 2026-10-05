using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wt_lab3_3mvc_pravotorova.Models;

namespace wt_lab3_3mvc_pravotorova.Views
{

    // представление - только выводит данные, Действий с ними не выполняет
    public class RoomView
    {
        public void ShowRooms(List<Room> rooms)
        {
            Console.WriteLine("СПИСОК НОМЕРОВ ");

            if (rooms.Count == 0)
            {
                Console.WriteLine("(список пуст)");
                return;
            }

            foreach (var room in rooms)
            {
                Console.WriteLine(
                    $"{room.Id}. " +
                    $"№{room.Number} | " +
                    $"{room.Category} | " +
                    $"мест: {room.Capacity} | " +
                    $"{room.PricePerNight} руб./ночь | " +
                    $"Доступен: {(room.IsAvailable ? "да" : "нет")}"
                );
            }
        }

        public void ShowRoom(Room room)
        {
            Console.WriteLine("НОМЕР ");
            Console.WriteLine($"ID: {room.Id}");
            Console.WriteLine($"Номер: {room.Number}");
            Console.WriteLine($"Категория: {room.Category}");
            Console.WriteLine($"Мест: {room.Capacity}");
            Console.WriteLine($"Цена за ночь: {room.PricePerNight} руб.");
            Console.WriteLine($"Доступен: {(room.IsAvailable ? "да" : "нет")}");
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        // ДОП отображение - статистика
        public void ShowStatistics(int count, decimal avgPrice, decimal minPrice, decimal maxPrice, decimal total)
        {
            Console.WriteLine("СТАТИСТИКА ПО НОМЕРАМ ");
            Console.WriteLine($"Всего номеров: {count}");
            Console.WriteLine($"Средняя цена: {avgPrice:F2} руб.");
            Console.WriteLine($"Минимальная цена: {minPrice} руб.");
            Console.WriteLine($"Максимальная цена: {maxPrice} руб.");
            Console.WriteLine($"Общая стоимость всех номеров: {total} руб.");
        }
    }
}

