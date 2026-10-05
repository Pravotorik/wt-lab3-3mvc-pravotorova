using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wt_lab3_3mvc_pravotorova.Models;
using wt_lab3_3mvc_pravotorova.Views;

namespace wt_lab3_3mvc_pravotorova.Controllers
{
    public class RoomController
    {
        private readonly List<Room> rooms;
        private readonly RoomView view;

        public RoomController()
        {
            rooms = new List<Room>
        {
            new Room { Id = 1, Number = "101", Category = "Стандарт", Capacity = 2, PricePerNight = 3500, IsAvailable = true },
            new Room { Id = 2, Number = "102", Category = "Стандарт", Capacity = 2, PricePerNight = 3500, IsAvailable = true },
            new Room { Id = 3, Number = "201", Category = "Комфорт", Capacity = 2, PricePerNight = 5200, IsAvailable = true },
            new Room { Id = 4, Number = "202", Category = "Комфорт", Capacity = 3, PricePerNight = 6100, IsAvailable = false },
            new Room { Id = 5, Number = "301", Category = "Люкс", Capacity = 3, PricePerNight = 8900, IsAvailable = true },
            new Room { Id = 6, Number = "401", Category = "Апартаменты", Capacity = 4, PricePerNight = 15000, IsAvailable = false }
        };

            view = new RoomView();
        }

        //  базовые операции 

        public void Index()
        {
            view.ShowRooms(rooms);
        }

        public void Show(int id)
        {
            var room = rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                view.ShowMessage("Номер не найден.");
                return;
            }
            view.ShowRoom(room);
        }

        public void Add(Room room)
        {
            rooms.Add(room);
            view.ShowMessage("Номер успешно добавлен.");
        }

        public void Delete(int id)
        {
            var room = rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                view.ShowMessage("Номер не найден.");
                return;
            }
            rooms.Remove(room);
            view.ShowMessage("Номер успешно удалён.");
        }

        //  ДОП: поиск по категории + фильтр доступных 

        public void Search(string text)
        {
            var result = rooms
                .Where(r => r.Number.Contains(text, StringComparison.OrdinalIgnoreCase)
                         || r.Category.Contains(text, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (result.Count == 0)
            {
                view.ShowMessage("Ничего не найдено.");
                return;
            }
            view.ShowRooms(result);
        }

        public void ShowAvailable()
        {
            var available = rooms.Where(r => r.IsAvailable).ToList();
            if (available.Count == 0)
            {
                view.ShowMessage("Свободных номеров нет.");
                return;
            }
            view.ShowRooms(available);
        }

        public void ShowStatistics()
        {
            if (rooms.Count == 0)
            {
                view.ShowMessage("Список пуст, статистики нет.");
                return;
            }

            int count = rooms.Count;
            decimal avg = rooms.Average(r => r.PricePerNight);
            decimal min = rooms.Min(r => r.PricePerNight);
            decimal max = rooms.Max(r => r.PricePerNight);
            decimal total = rooms.Sum(r => r.PricePerNight);

            view.ShowStatistics(count, avg, min, max, total);
        }

        //  обработка пользовательского ввода 

        private void ShowRoomByUserInput()
        {
            Console.Write("Введите ID номера: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                view.ShowMessage("Некорректный ID.");
                return;
            }
            Show(id);
        }

        private void AddRoomByUserInput()
        {
            Console.Write("Введите ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                view.ShowMessage("Некорректный ID.");
                return;
            }

            if (rooms.Any(r => r.Id == id))
            {
                view.ShowMessage("Номер с таким ID уже существует.");
                return;
            }

            Console.Write("Введите номер комнаты: ");
            string number = Console.ReadLine() ?? "";

            Console.Write("Введите категорию: ");
            string category = Console.ReadLine() ?? "";

            Console.Write("Введите количество мест: ");
            if (!int.TryParse(Console.ReadLine(), out int capacity) || capacity <= 0)
            {
                view.ShowMessage("Некорректное количество мест.");
                return;
            }

            Console.Write("Введите цену за ночь: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price <= 0)
            {
                view.ShowMessage("Некорректная цена.");
                return;
            }

            Console.Write("Доступен? (y/n): ");
            string availInput = Console.ReadLine() ?? "";
            bool isAvailable = availInput.Trim().ToLower() == "y";

            var room = new Room
            {
                Id = id,
                Number = number,
                Category = category,
                Capacity = capacity,
                PricePerNight = price,
                IsAvailable = isAvailable
            };

            Add(room);
        }

        private void DeleteRoomByUserInput()
        {
            Console.Write("Введите ID номера для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                view.ShowMessage("Некорректный ID.");
                return;
            }
            Delete(id);
        }

        private void SearchByUserInput()
        {
            Console.Write("Введите номер или категорию для поиска: ");
            string text = Console.ReadLine() ?? "";
            Search(text);
        }

        //  главное меню 

        public void Run()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("МЕНЮШКА");
                Console.WriteLine("1. Показать все номера");
                Console.WriteLine("2. Найти номер по ID");
                Console.WriteLine("3. Добавить номер");
                Console.WriteLine("4. Удалить номер");
                Console.WriteLine("5. Поиск по номеру/категории");
                Console.WriteLine("6. Показать только доступные номера");
                Console.WriteLine("7. Статистика по номерам");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                var command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        Index();
                        break;
                    case "2":
                        ShowRoomByUserInput();
                        break;
                    case "3":
                        AddRoomByUserInput();
                        break;
                    case "4":
                        DeleteRoomByUserInput();
                        break;
                    case "5":
                        SearchByUserInput();
                        break;
                    case "6":
                        ShowAvailable();
                        break;
                    case "7":
                        ShowStatistics();
                        break;
                    case "0":
                        view.ShowMessage("Выход из программы.");
                        return;
                    default:
                        view.ShowMessage("Неизвестная команда.");
                        break;
                }
            }
        }

    }

}
