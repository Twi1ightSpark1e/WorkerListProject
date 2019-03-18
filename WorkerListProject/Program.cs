using System;
using System.Collections.Generic;

namespace WorkerListProject
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    /// <summary>
    /// Работник
    /// </summary>
    struct Worker {
        string fullName;        // Фамилия работника
        string position;        // Должность работника
        DateTime signingDate;   // Дата подписания контракта
        uint contractDuration;  // Срок действия контракта
        float salary;           // Оклад

        /// <summary>
        /// Вывод текущего работника
        /// </summary>
        public void Print() {
            Console.WriteLine($"ФИО работника: {fullName} Должность: {position}" +
                $" Дата подписания контракта: {signingDate} Срок действия контракта: {contractDuration}" +
                $" Оклад: {salary}");
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
        /// Ввод значений фильтра
        /// </summary>
        /// <param name="filter">Значения фильтра</param>
        public static void SetFilter(ref Filter filter)
        {
            
            bool success = true;
            // Пока не вышли из фильтра
            while(success) 
            {
                Console.WriteLine($"Меню:{Environment.NewLine}"+
                        $"1: ФИО{Environment.NewLine}" +
                        $"2: Должность{Environment.NewLine}" +
                        $"3: Дата подписания контракта{Environment.NewLine}" +
                        $"4: Срока действия контракта{Environment.NewLine}" +
                        $"4: Оклад{Environment.NewLine}" +
                        $"5: Выйти из работы с фильтра{Environment.NewLine}");
                
                // Считывание номера выбранного пункта
                int numFilter = 0;
                do
                {
                    Console.WriteLine("Выберите номер пункта для работы с фильтром:");

                    numFilter = Console.ReadLine();
                    
                    if (numFilter > 0 || numFilter <= 5) // если не выбрали пункт
                    {
                        Console.WriteLine("Введите еще раз правильное значение!!!");
                    }
                } while (numFilter > 0 || numFilter <= 5); // Пока не ввели правильное значение

                

                //выбран пункт фильтра ФИО
                if (numFilter == 1)
                {
                    Console.WriteLine("Введите подстроку для поиска по ФИО:");
                    filter.fullNameSubstring = Console.ReadLine();
                }
                //выбран пункт фильтра Должности
                if (numFilter == 2)
                {
                    Console.WriteLine("Введите подстроку для поиска по Должности:");
                    filter.positionSubstring = Console.ReadLine();
                }

                //выбран пункт фильтра Даты подписания контракта работника
                if (numFilter == 2)
                {
                    // Считывание Нижней границы диапазона даты подписания контракта работника
                    bool done = false;
                    do
                    {
                        Console.WriteLine("Введите нижнюю дату подписания контракта работника (dd.mm.yyyy): ");
                        done = DateTime.TryParse(Console.ReadLine(), out filter.signingDateLowerBound);
                        if (!done) // if ввели неправильно
                        {
                            Console.WriteLine("Введите еще раз правильное значение!!!");
                        }
                    } while (!done); // Пока не ввели правильное значение

                    // Считывание Вверхней границы диапазона даты подписания контракта работника
                    done = false;
                    do
                    {
                        Console.WriteLine("Введите вверхнюю границу даты подписания контракта работника (dd.mm.yyyy): ");
                        done = DateTime.TryParse(Console.ReadLine(), out filter.signingDateUpperBound);
                        if (!done) // if ввели неправильно
                        {
                            Console.WriteLine("Введите еще раз правильное значение!!!");
                        }
                    } while (!done); // Пока не ввели правильное значение
                }

                //выбран пункт фильтра Срок действия контракта
                if (numFilter == 3)
                {
                    // Считывание Нижней границы диапазона срока действия контракта работника
                    bool done = false;
                    do
                    {
                        Console.WriteLine("Введите Нижнюю границу диапазона срока действия контракта работника: ");
                        done = uint.TryParse(Console.ReadLine(), out filter.contractDurationLowerBound);
                        if (!done) // if ввели неправильно
                        {
                            Console.WriteLine("Введите еще раз правильное значение!!!");
                        }
                    } while (!done); // Пока не ввели правильное значение

                    // Считывание Вверхней границы диапазона срока действия контракта работника
                    done = false;
                    do
                    {
                        Console.WriteLine("Введите Вверхнюю границу диапазона срока действия контракта работника: ");
                        done = uint.TryParse(Console.ReadLine(), out filter.contractDurationUpperBound);
                        if (!done) // if ввели неправильно
                        {
                            Console.WriteLine("Введите еще раз правильное значение!!!");
                        }
                    } while (!done); // Пока не ввели правильное значение
                }

                //выбран пункт фильтра Оклада
                if (numFilter == 4)
                {
                    // Считывание Нижней границы диапазона оклада работника
                    bool done = false;
                    do
                    {
                        Console.WriteLine("Введите Нижнюю границу диапазона оклада работника: ");
                        done = uint.TryParse(Console.ReadLine(), out filter.salaryLowerBound);
                        if (!done) // if ввели неправильно
                        {
                            Console.WriteLine("Введите еще раз правильное значение!!!");
                        }
                    } while (!done); // Пока не ввели правильное значение

                    // Считывание Вверхней границы диапазона оклада работника
                    done = false;
                    do
                    {
                        Console.WriteLine("Введите Вверхнюю границу диапазона оклада работника: ");
                        done = uint.TryParse(Console.ReadLine(), out filter.salaryUpperBound);
                        if (!done) // if ввели неправильно
                        {
                            Console.WriteLine("Введите еще раз правильное значение!!!");
                        }
                    } while (!done); // Пока не ввели правильное значение
                }


                //выбран пункт Отменить
                if (numFilter == 5)
                {
                    success = false;
                    Console.WriteLine($"Выход из ввода значений фильтра");
                }
            }
        }

        /// <summary>
        /// Вывод фильтрованного списка структуры
        /// </summary>
        /// <param name="workers">Список работников</param>
        /// <param name="filter">Значения фильтра</param>
        public static void PrintFiltered(List<Worker> workers, Filter filter)
        {
            foreach (var worker in workers) // цикличная проверка каждого рабочего
            {
                bool shouldPrint = true; // флаг, означающий нужно ли выводить данного рабочего

                // Проверка: включает ли ФИО рабочего соответствующую строку из фильтра
                if ((filter.fullNameSubstring != null) &&
                    (!worker.fullName.Contains(filter.fullNameSubstring)))
                {
                    shouldPrint = false;
                }

                // Проверка: включает ли должность работника соответствующую строку из фильтра
                if ((filter.positionSubstring != null) &&
                    (!worker.position.Contains(filter.positionSubstring)))
                {
                    shouldPrint = false;
                }

                // Проверка: принадлежит ли дата подписания контракта
                //           соответствующему диапазону из фильтра
                if ((worker.signingDate < filter.signingDateLowerBound) ||
                    (worker.signingDate > filter.signingDateUpperBound))
                {
                    shouldPrint = false;
                }

                // Проверка: принадлежит ли срок действия контракта
                //           соответствующему диапазону из фильтра
                if ((worker.contractDuration < filter.contractDurationLowerBound) ||
                    (worker.contractDuration > filter.contractDurationUpperBound))
                {
                    shouldPrint = false;
                }

                // Проверка: принадлежит ли оклад работника
                //           соответствующему диапазону из фильтра
                if ((worker.salary < filter.salaryLowerBound) ||
                    (worker.salary > filter.salaryUpperBound))
                {
                    shouldPrint = false;
                }

                // Если данный рабочий прошёл все проверки — вывести его
                if (shouldPrint)
                {
                    worker.Print();
                }
            }
        }

        /// <summary>
        /// Добавление одного элемента структуры в список работников
        /// </summary>
        /// <param name="workers">Список работников</param>
        public static Worker AddWorker(List<Worker> workers)
        {
            // Считывание ФИО работника
            Worker worker = new Worker();
            Console.WriteLine("Введите ФИО работника: ");
            worker.fullName = Console.ReadLine();

            // Считывание должности работника
            Console.WriteLine("Введите должность работника: ");
            worker.position = Console.ReadLine();

            // Считывание даты подписания контракта работника
            bool success = false;
            do
            {
                Console.WriteLine("Введите дату подписания контракта работника (dd.mm.yyyy): ");
                success = DateTime.TryParse(Console.ReadLine(), out worker.signingDate);
                if (!success) // if ввели неправильно
                {
                    Console.WriteLine("Введите еще раз правильное значение!!!");
                }
            } while (!success); // Пока не ввели правильное значение

            // Считывание времени действия контракта
            do
            {
                Console.WriteLine("Введите время действия контракта: ");
                success = uint.TryParse(Console.ReadLine(), out worker.contractDuration);
                if (!success) // if ввели неправильно
                {
                    Console.WriteLine("Введите еще раз правильное значение!!!");
                }
            } while (!success); // Пока не ввели правильное значение

            // Считывание оклада работника
            do
            {
                Console.WriteLine("Введите оклад работника: ");
                success = float.TryParse(Console.ReadLine(), out worker.salary);
                if (!success) // if ввели неправильно
                {
                    Console.WriteLine("Введите еще раз правильное значение!!!");
                }
            } while (!success); // Пока не ввели правильное значение

            // Добавление нового работника в список
            workers.Add(worker);
            // Вернуть нового работника
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
    }
}
