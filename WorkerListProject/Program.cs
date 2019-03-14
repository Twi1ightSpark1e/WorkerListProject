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
            Console.WriteLine($"fullName: {fullName} position: {position}" +
                $" signingDate: {signingDate} contractDuration: {contractDuration}" +
                $" salary:{salary}");
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
            Worker worker = new Worker();
            bool success = false;

            Console.WriteLine("Введите ФИО работника: ");
            worker.fullName = Console.ReadLine();

            Console.WriteLine("Введите должность работника: ");
            worker.position = Console.ReadLine();

            do
            {
                Console.WriteLine("Введите дату подписания контракта работника (dd.mm.yyyy): ");
                success = DateTime.TryParse(Console.ReadLine(), out worker.signingDate);
                if (!success)
                {
                    Console.WriteLine("Введите еще раз правильное значение!!!");
                }
            } while (!success);

            do
            {
                Console.WriteLine("Введите время действия контракта: ");
                success = uint.TryParse(Console.ReadLine(), out worker.contractDuration);
                if (!success)
                {
                    Console.WriteLine("Введите еще раз правильное значение!!!");
                }
            } while (!success);

            do
            {
                Console.WriteLine("Введите оклад работника: ");
                success = float.TryParse(Console.ReadLine(), out worker.salary);
                if (!success)
                {
                    Console.WriteLine("Введите еще раз правильное значение!!!");
                }
            } while (!success);

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
    }
}
