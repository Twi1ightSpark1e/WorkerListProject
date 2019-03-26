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

            // Считывание номера выбранного пункта
            int numMenu = 0;

            // Пока не вышли из главного меню
            bool success = true;
            bool done = false;
            while (success) 
            {
                // Пока не ввели правильное значение
                numMenu = 0;
                do
                {
                    // Вывод сообщения о главном меню работы с программой
                    Console.WriteLine($"Выберите пункт главного меню:{Environment.NewLine}" +
                                $"1: Добавить сотрудника{Environment.NewLine}" +
                                $"2: Вывод всех работников{Environment.NewLine}" +
                                $"3: Задать фильтрацию{Environment.NewLine}" +
                                $"4: Вывод списка работников по заданному фильтру {Environment.NewLine}" +
                                $"5: Завершить работу программы{Environment.NewLine}");

                    // проверка введенного пункта главного меню
                    done = int.TryParse(Console.ReadLine(), out numMenu);

                    // если не выбрали пункт
                    if (!done && (numMenu > 0 || numMenu <= 5))
                    {
                        Console.WriteLine("Введите еще раз правильное значение!!!");
                    }
                } while (!done && (numMenu > 0 || numMenu <= 5)); 

                // Переход по выбранному номеру меню
                switch (numMenu)
                {
                    case 1: // Добавляем нового работника
                        Worker.AddWorker(workers);
                        break;
                    
                    case 2: // Выводим список работников
                        Worker.Print(workers);
                        break;
                    
                    case 3: // Задаем фильтрацию по списку работников
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

                // Сообщение об завершении работы выбранной функции
                Console.WriteLine($"Выход из пункта меню{Environment.NewLine}");
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
        /// Вывод текущего работника
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
        /// <param type="Worker">Работник</param>
        public void Print()
        {
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
        /// Вывод фильтрованного списка структуры
        /// </summary>
        /// <param name="workers">Список работников</param>
        /// <param name="filter">Значения фильтра</param>
        public static void PrintFiltered(List<Worker> workers, Filter filter)
        {
            foreach (var worker in workers) // Цикличная проверка каждого рабочего
            {
                // Проверка: включает ли ФИО рабочего соответствующую строку из фильтра
                if ((filter.fullNameSubstring != null) &&
                    (!worker.fullName.Contains(filter.fullNameSubstring)))
                {
                    continue;
                }

                // Проверка: включает ли должность работника соответствующую строку из фильтра
                if ((filter.positionSubstring != null) &&
                    (!worker.position.Contains(filter.positionSubstring)))
                {
                    continue;
                }

                // Проверка: принадлежит ли дата подписания контракта
                //           соответствующему диапазону из фильтра
                if ((worker.signingDate < filter.signingDateLowerBound) ||
                    (worker.signingDate > filter.signingDateUpperBound))
                {
                    continue;
                }

                // Проверка: принадлежит ли срок действия контракта
                //           соответствующему диапазону из фильтра
                if ((worker.contractDuration < filter.contractDurationLowerBound) ||
                    (worker.contractDuration > filter.contractDurationUpperBound))
                {
                    continue;
                }

                // Проверка: принадлежит ли оклад работника
                //           соответствующему диапазону из фильтра
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
        public static Worker AddWorker(List<Worker> workers)
        {
            // ФИО работника
            Worker worker = new Worker();
            Console.WriteLine("Введите ФИО работника: ");
            worker.fullName = Console.ReadLine();

            // Должность работника
            Console.WriteLine("Введите должность работника: ");
            worker.position = Console.ReadLine();

            // Дата подписания контракта работника
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

            // Время действия контракта
            do
            {
                Console.WriteLine("Введите время действия контракта: ");
                success = uint.TryParse(Console.ReadLine(), out worker.contractDuration);
                if (!success) // if ввели неправильно
                {
                    Console.WriteLine("Введите еще раз правильное значение!!!");
                }
            } while (!success); // Пока не ввели правильное значение

            // Оклад работника
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
                Console.WriteLine($"Какой фильтр хотите задать?:{Environment.NewLine}" +
                        $"1: ФИО{Environment.NewLine}" +
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
                    Console.WriteLine("Выберите номер пункта для работы с фильтром:");
                    done = int.TryParse(Console.ReadLine(), out numFilter);

                    // если не выбрали пункт
                    if (!done && (numFilter > 0 || numFilter < 6))
                    {
                        Console.WriteLine("Введите еще раз правильное значение!!!");
                    }
                } while (!done && (numFilter > 0 || numFilter < 6)); 

                // Выбран пункт фильтра ФИО
                if (numFilter == 1)
                {
                    Console.WriteLine("Введите подстроку для поиска по ФИО:");
                    filter.fullNameSubstring = Console.ReadLine();
                }
                
                // Выбран пункт фильтра Должности
                if (numFilter == 2)
                {
                    Console.WriteLine("Введите подстроку для поиска по Должности:");
                    filter.positionSubstring = Console.ReadLine();
                }

                // Выбран пункт фильтра Даты подписания контракта работника
                if (numFilter == 3)
                {
                    SetFilterDate(ref filter);
                }

                // Выбран пункт фильтра Срока действия контракта
                if (numFilter == 4)
                {
                    SetFilterContractDuration(ref filter);
                }

                // Выбран пункт фильтра Оклада
                if (numFilter == 5)
                {
                    SetFilterSalary(ref filter);
                }

                // Выбран пункт "Отменить"
                if (numFilter == 6)
                {
                    success = false;
                    Console.WriteLine($"Выход из ввода значений фильтра");
                }
            }
        }

        /// <summary>
        /// Проверка и установка введенных значений фильтра для даты
        /// </summary>
        /// <param name="filter">Значения фильтра</param>
        public static void SetFilterDate(ref Filter filter)
        {
            // Задаем значение для вверхней границы
            int isUpperOrLower = 0;

            // Задаем временное значение даты
            DateTime tmpDate;

            // Задаем об успешном сохранении значения фильтра
            bool done = false;

            // Пока не сделали для вверхней и нижней границы
            while (isUpperOrLower < 2)
            {
                // Задаем об успешном сохранении значения фильтра
                done = false;

                // Сообщение о вводе границы
                Console.WriteLine("Введите " +((isUpperOrLower == 0) ? "вверхнюю" : "нижнюю")+ " границу Даты подписания контракта работника (dd.mm.yyyy):");

                // Считываем ввод с консоли
                string input = Console.ReadLine();

                // Если пустая строка
                if (input == String.Empty)
                {
                    // Сохраняем значение фильтра
                    filter.signingDateUpperBound = (isUpperOrLower == 0) ? null : filter.signingDateUpperBound;
                    filter.signingDateLowerBound = (isUpperOrLower == 1) ? null : filter.signingDateLowerBound;

                    // Отмечаем, что успешно задали значение фильтра
                    done = true;
                }
                // Если что-то введено
                else
                {
                    // Проверяем выбранный тип поля фильтра на правильное значение
                    done = DateTime.TryParse(input, out tmpDate);

                    // Если неправильное значение - еще раз запускаем проверку
                    if (!done)
                    {
                        Console.WriteLine("Введите еще раз правильное значение!!!");
                        continue;
                    }

                    // Сохраняем значение фильтра
                    filter.signingDateUpperBound = (isUpperOrLower == 0) ? tmpDate : filter.signingDateUpperBound;
                    filter.signingDateLowerBound = (isUpperOrLower == 1) ? tmpDate : filter.signingDateLowerBound;
                }

                isUpperOrLower++;
            }
        }

        /// <summary>
        /// Проверка и установка введенных значений фильтра для срока действия
        /// </summary>
        /// <param name="filter">Значения фильтра</param>
        public static void SetFilterContractDuration(ref Filter filter)
        {
            // Задаем значение для вверхней границы
            int isUpperOrLower = 0;

            // Задаем временное значение даты
            uint tmpUint;

            // Задаем об успешном сохранении значения фильтра
            bool done = false;

            // Пока не сделали для вверхней и нижней границы
            while (isUpperOrLower < 2)
            {
                // Задаем об успешном сохранении значения фильтра
                done = false;

                // Сообщение о вводе границы
                Console.WriteLine("Введите " + ((isUpperOrLower == 0) ? "вверхнюю" : "нижнюю") + " границу");

                // Считываем ввод с консоли
                string input = Console.ReadLine();

                // Если пустая строка
                if (input == String.Empty)
                {
                    // Сохраняем значение фильтра
                    filter.contractDurationUpperBound = (isUpperOrLower == 0) ? null : filter.contractDurationUpperBound;
                    filter.contractDurationLowerBound = (isUpperOrLower == 1) ? null : filter.contractDurationLowerBound;

                    // Отмечаем, что успешно задали значение фильтра
                    done = true;
                }
                // Если что-то введено
                else
                {
                    // Проверяем выбранный тип поля фильтра на правильное значение
                    done = uint.TryParse(input, out tmpUint);

                    // Если неправильное значение - еще раз запускаем проверку
                    if (!done)
                    {
                        Console.WriteLine("Введите еще раз правильное значение!!!");
                        continue;
                    }

                    // Сохраняем значение фильтра
                    filter.contractDurationUpperBound = (isUpperOrLower == 0) ? tmpUint : filter.contractDurationUpperBound;
                    filter.contractDurationLowerBound = (isUpperOrLower == 1) ? tmpUint : filter.contractDurationLowerBound;
                }

                isUpperOrLower++;
            }
        }

        /// <summary>
        /// Проверка и установка введенных значений фильтра для срока действия
        /// </summary>
        /// <param name="filter">Значения фильтра</param>
        public static void SetFilterSalary(ref Filter filter)
        {
            // Задаем значение для вверхней границы
            int isUpperOrLower = 0;

            // Задаем временное значение даты
            float tmpFloat;

            // Задаем об успешном сохранении значения фильтра
            bool done = false;

            // Пока не сделали для вверхней и нижней границы
            while (isUpperOrLower < 2)
            {
                // Задаем об успешном сохранении значения фильтра
                done = false;

                // Сообщение о вводе границы
                Console.WriteLine("Введите " + ((isUpperOrLower == 0) ? "вверхнюю" : "нижнюю") + " границу");

                // Считываем ввод с консоли
                string input = Console.ReadLine();

                // Если пустая строка
                if (input == String.Empty)
                {
                    // Сохраняем значение фильтра
                    filter.salaryUpperBound = (isUpperOrLower == 0) ? null : filter.salaryUpperBound;
                    filter.salaryLowerBound = (isUpperOrLower == 1) ? null : filter.salaryLowerBound;

                    // Отмечаем, что успешно задали значение фильтра
                    done = true;
                }
                // Если что-то введено
                else
                {
                    // Проверяем выбранный тип поля фильтра на правильное значение
                    done = float.TryParse(input, out tmpFloat);

                    // Если неправильное значение - еще раз запускаем проверку
                    if (!done)
                    {
                        Console.WriteLine("Введите еще раз правильное значение!!!");
                        continue;
                    }

                    // Сохраняем значение фильтра
                    filter.salaryUpperBound = (isUpperOrLower == 0) ? tmpFloat : filter.salaryUpperBound;
                    filter.salaryLowerBound = (isUpperOrLower == 1) ? tmpFloat : filter.salaryLowerBound;
                }

                // Другой тип границы 
                isUpperOrLower++;
            }
        }
    }
}
