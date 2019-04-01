using System;
using System.Collections.Generic;

namespace WorkerListProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // Инициализируем список работников
            List<Worker> workers = new List<Worker>();

            // Инициализируем фильтр
            Filter filter = new Filter();

            int numMenu = 0;     // номер выбранного пункта меню
            bool done = false;   // флаг успешного ввода

            // Пока не вышли из главного меню
            while (true)
            {
                // Пока не ввели правильное значение
                numMenu = 0;
                do
                {
                    // Вывод сообщения о главном меню работы с программой
                    PrettyPrinter.PrintCaption($"Главное меню:{Environment.NewLine}");
                    Console.Write($"1: Добавить сотрудника{Environment.NewLine}" +
                                  $"2: Вывод всех работников{Environment.NewLine}" +
                                  $"3: Ввод значений фильтра{Environment.NewLine}" +
                                  $"4: Вывод списка работников по заданному фильтру {Environment.NewLine}" +
                                  $"5: Завершить работу программы{Environment.NewLine}");
                    PrettyPrinter.PrintCaption("Выберите пункт меню: ");

                    // проверка введенного пункта главного меню
                    done = int.TryParse(Console.ReadLine(), out numMenu);
                    if (!done || (numMenu < 1 || numMenu > 5))
                    {
                        PrettyPrinter.PrintError($"Введите еще раз правильное значение{Environment.NewLine}");
                    }
                    Console.WriteLine();
                } while (!done || (numMenu < 1 || numMenu > 5));

                // Обработка по выбранному номеру меню
                switch (numMenu)
                {
                    case 1: // Добавляем нового работника
                        Worker.Add(workers);
                        break;

                    case 2: // Выводим список работников
                        Worker.Print(workers);
                        break;

                    case 3: // Ввод значений фильтра
                        Filter.SetFilter(ref filter);
                        break;

                    case 4: // Выводим список работников, используя фильтр
                        Worker.PrintFiltered(workers, filter);
                        break;

                    case 5:
                        // Завершение работы программ
                        Console.WriteLine("Завершение работы программы");
                        return;
                }
            }
        }
    }

    /// <summary>
    /// Работник
    /// </summary>
    struct Worker
    {
        /// <summary>
        /// Фамилия работника
        /// Значение должно быть непустым
        /// </summary>
        string fullName;

        /// <summary>
        /// Должность работника
        /// </summary>
        string position;

        /// <summary>
        /// Дата подписания контракта
        /// Значение должно быть непустым
        /// </summary>
        DateTime signingDate;

        /// <summary>
        /// Срок действия контракта
        /// Значение должно быть больше 0
        /// </summary>
        uint contractDuration;

        /// <summary>
        /// Оклад
        /// Значение должно быть неотрицательным
        /// </summary>
        float salary;

        /// <summary>
        /// Вывод одного работника
        /// </summary>
        public void Print()
        {
            // Вывод ФИО работника
            PrettyPrinter.PrintFieldName("ФИО работника: ");
            Console.WriteLine($"{fullName}");

            // Вывод должности
            PrettyPrinter.PrintFieldName("Должность: ");
            Console.WriteLine($"{position}");

            // Вывод даты подписания контракта
            PrettyPrinter.PrintFieldName("Дата подписания контракта: ");
            Console.WriteLine($"{signingDate}");

            // Вывод срока действия контракта
            PrettyPrinter.PrintFieldName("Срок действия контракта: ");
            Console.WriteLine($"{contractDuration}");

            // Вывод оклада
            PrettyPrinter.PrintFieldName("Оклад: ");
            Console.WriteLine($"{salary}{Environment.NewLine}");
        }

        /// <summary>
        /// Вывод всех работников
        /// </summary>
        /// <param name="listwrks">Список работников</param>
        public static void Print(List<Worker> listwrks)
        {
            foreach (Worker wrk in listwrks)
            {
                wrk.Print();
            }
        }

        /// <summary>
        /// Вывод фильтрованного списка структуры
        /// </summary>
        /// <param name="workers">Список работников</param>
        /// <param name="filter">Значения фильтра</param>
        public static void PrintFiltered(List<Worker> workers, Filter filter)
        {
            // Цикличная проверка каждого рабочего
            foreach (var worker in workers)
            {
                // Соответствует ли ФИО работника фильтру?
                if ((filter.fullNameSubstring != null) &&
                    (!worker.fullName.Contains(filter.fullNameSubstring)))
                {
                    continue;
                }

                // Соответствует ли должность работника фильтру?
                if ((filter.positionSubstring != null) &&
                    (!worker.position.Contains(filter.positionSubstring)))
                {
                    continue;
                }

                // Соответствует ли дата подписания контракта фильтру?
                if ((worker.signingDate < filter.signingDateLowerBound) ||
                    (worker.signingDate > filter.signingDateUpperBound))
                {
                    continue;
                }

                // Соответствует ли срок действия контракта фильтру?
                if ((worker.contractDuration < filter.contractDurationLowerBound) ||
                    (worker.contractDuration > filter.contractDurationUpperBound))
                {
                    continue;
                }

                // Соответствует ли оклад работника фильтру?
                if ((worker.salary < filter.salaryLowerBound) ||
                    (worker.salary > filter.salaryUpperBound))
                {
                    continue;
                }

                // Если данный рабочий прошёл все проверки — вывести его
                worker.Print();
            }
        }

        /// <summary>
        /// Добавление одного элемента структуры в список работников
        /// </summary>
        /// <param name="workers">Список работников</param>
        public static Worker Add(List<Worker> workers)
        {
            // ФИО работника
            Worker worker = new Worker();
            PrettyPrinter.PrintFieldName("Введите ФИО работника: ");
            worker.fullName = Console.ReadLine();

            // Должность работника
            PrettyPrinter.PrintFieldName("Введите должность работника: ");
            worker.position = Console.ReadLine();

            // Дата подписания контракта работника
            bool success = false;
            do
            {
                PrettyPrinter.PrintFieldName("Введите дату подписания контракта работника(dd.mm.yyyy): ");
                success = DateTime.TryParse(Console.ReadLine(), out worker.signingDate);
                if (!success) // if ввели неправильно
                {
                    PrettyPrinter.PrintError($"Введите еще раз правильное значение{Environment.NewLine}");
                }
            } while (!success); // Пока не ввели правильное значение

            // Время действия контракта
            do
            {
                PrettyPrinter.PrintFieldName("Введите время действия контракта: ");
                success = uint.TryParse(Console.ReadLine(), out worker.contractDuration);
                if (!success) // if ввели неправильно
                {
                    PrettyPrinter.PrintError($"Введите еще раз правильное значение{Environment.NewLine}");
                }
            } while (!success); // Пока не ввели правильное значение

            // Оклад работника
            do
            {
                PrettyPrinter.PrintFieldName("Введите оклад работника: ");
                success = float.TryParse(Console.ReadLine(), out worker.salary);
                if (!success) // if ввели неправильно
                {
                    PrettyPrinter.PrintError($"Введите еще раз правильное значение{Environment.NewLine}");
                }
            } while (!success); // Пока не ввели правильное значение

            // Добавление нового работника в список
            workers.Add(worker);
            Console.WriteLine();
            return worker;
        }
    }

    /// <summary>
    /// Фильтр
    /// </summary>
    struct Filter
    {
        public string fullNameSubstring;            // Подстрока, которая должна быть в ФИО
        public string positionSubstring;            // Подстрока, которая должна быть в должности
        public DateTime? signingDateLowerBound;     // Нижняя граница диапазона даты подписания
        public DateTime? signingDateUpperBound;     // Верхняя граница диапазона даты подписания
        public uint? contractDurationLowerBound;    // Нижняя граница диапазона срока действия контракта
        public uint? contractDurationUpperBound;    // Верхняя граница диапазона срока действия контракта
        public float? salaryLowerBound;             // Нижняя граница диапазона оклада
        public float? salaryUpperBound;             // Верхняя граница диапазона оклада

        /// <summary>
        /// Ввод значений фильтра
        /// </summary>
        /// <param name="filter">Значения фильтра</param>
        public static void SetFilter(ref Filter filter)
        {
            // Задаем первоначальные значения
            bool success = true;
            bool done = false;

            // Пока не вышли из фильтра
            while (success)
            {
                // Выводим сообщение об вариации фильтра
                PrettyPrinter.PrintCaption($"Возможные поля фильтра:{Environment.NewLine}");
                Console.Write($"1: ФИО{Environment.NewLine}" +
                              $"2: Должность{Environment.NewLine}" +
                              $"3: Дата подписания контракта{Environment.NewLine}" +
                              $"4: Срока действия контракта{Environment.NewLine}" +
                              $"5: Оклад{Environment.NewLine}" +
                              $"6: Выйти из работы с фильтра{Environment.NewLine}");

                // Считывание номера выбранного пункта, пока не ввели правильное значение
                int numFilter = 0;
                do
                {
                    // Считываем номер пункта для работы с фильтром
                    PrettyPrinter.PrintCaption("Выберите номер пункта для работы с фильтром: ");
                    done = int.TryParse(Console.ReadLine(), out numFilter);

                    // если не выбрали пункт
                    if (!done && (numFilter > 0 || numFilter < 6))
                    {
                        PrettyPrinter.PrintError($"Введите еще раз правильное значение{Environment.NewLine}");
                    }
                } while (!done && (numFilter > 0 || numFilter < 6)); 
                Console.WriteLine();

                // Выбран пункт фильтра ФИО
                if (numFilter == 1)
                {
                    PrettyPrinter.PrintFieldName("Введите подстроку для поиска по ФИО: ");
                    filter.fullNameSubstring = Console.ReadLine();
                }

                // Выбран пункт фильтра Должности
                if (numFilter == 2)
                {
                    PrettyPrinter.PrintFieldName("Введите подстроку для поиска по должности: ");
                    filter.positionSubstring = Console.ReadLine();
                }

                // Выбран пункт фильтра Даты подписания контракта работника
                if (numFilter == 3)
                {
                    filter.signingDateUpperBound = ReadValue<DateTime?>("Введите верхнюю границу (dd.mm.yyyy): ");
                    filter.signingDateLowerBound = ReadValue<DateTime?>("Введите нижнюю границу (dd.mm.yyyy): ");
                    Console.WriteLine();
                }

                // Выбран пункт фильтра Срока действия контракта
                if (numFilter == 4)
                {
                    filter.contractDurationUpperBound = ReadValue<uint?>("Введите верхнюю границу: ");
                    filter.contractDurationLowerBound = ReadValue<uint?>("Введите нижнюю границу: ");
                    Console.WriteLine();
                }

                // Выбран пункт фильтра Оклада
                if (numFilter == 5)
                {
                    filter.salaryUpperBound = ReadValue<float?>("Введите верхнюю границу: ");
                    filter.salaryLowerBound = ReadValue<float?>("Введите нижнюю границу: ");
                    Console.WriteLine();
                }

                // Выбран пункт "Отменить"
                if (numFilter == 6)
                {
                    success = false;
                }
            }
        }

        /// <summary>
        /// Считывание значения с проверкой правильности
        /// </summary>
        /// <param name="message">Сообщение-приглашение ввода значения</param>
        /// <returns>Введенное пользователем значение или значение по умолчанию для выбранного типа</returns>
        public static T ReadValue<T>(string message)
        {
            // Получение настоящего типа обобщенного аргумента, необходимо для правильной работы с Nullable типами
            Type type = typeof(T);
            type = Nullable.GetUnderlyingType(type) ?? type;

            // Пока не будет введено правильное значение
            while (true)
            {
                // Вывод сообщения-приглашения ввода значения
                PrettyPrinter.PrintFieldName(message);

                // Считываем ввод с консоли
                string input = Console.ReadLine();

                // Если была введена пустая строка
                if (input == String.Empty)
                {
                    // Возвращаем значение по умолчанию для данного типа
                    return default(T);
                }

                // Иначе проверяем введенное значение на правильность
                try
                {
                    // Если значение введено правильно - возвращаем его
                    return (T)Convert.ChangeType(input, type);
                }
                // Иначе сообщаем об ошибке ввода
                catch (Exception e)
                {
                    PrettyPrinter.PrintError($"Введите еще раз правильное значение{Environment.NewLine}");
                }
            }
        }
    }

    /// <summary>
    /// Набор методов для красивого вывода сообщений
    /// </summary>
    class PrettyPrinter
    {
        /// <summary>
        /// Вывод ошибки
        /// </summary>
        /// <param name="message">Выводимое сообщение</param>
        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Вывод заголовка
        /// </summary>
        /// <param name="message">Выводимое сообщение</param>
        public static void PrintCaption(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(message);
            Console.ResetColor();
        }

        /// <<summary>
        /// Вывод названия поля рабочего
        /// </summary>
        /// <param name="message">Выводимое сообщение</param>
        public static void PrintFieldName(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(message);
            Console.ResetColor();
        }
    }
}
