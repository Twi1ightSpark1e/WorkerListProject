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
        /// <summary>
        /// Фамилия работника
        /// Значение должно быть непустым
        /// </summary>
        string fullName;

        /// <summary>
        /// Должность работника
        /// Значение должно быть непустым
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
        public void Print() {
            Console.WriteLine($"fullName: {fullName} position: {position}" +
                $" signingDate: {signingDate} contractDuration: {contractDuration}" +
                $" salary:{salary}");
        }

        /// <summary>
        /// Вывод всех работников
        /// </summary>
        /// <param type="List<Worker>">Список работников</param>
        public static void Print(List<Worker> listwrks)
        {
            foreach (Worker wrk in listwrks)
            {
                wrk.Print();
            }
        }

        /// <summary>
        /// Добавление одного элемента структуры в список работников
        /// </summary>
        /// <param type="List<Worker>">Список работников</param>
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

        /// <summary>
        /// Вывод фильтрованного списка структуры
        /// </summary>
        public static void PrintFiltered(List<Worker> workers, Filter filter)
        {
            foreach (var worker in workers)
            {
                bool shouldPrint = true;

                if ((filter.fullNameSubstring != null) &&
                    (!worker.fullName.Contains(filter.fullNameSubstring)))
                {
                    shouldPrint = false;
                }

                if ((filter.positionSubstring != null) &&
                    (!worker.position.Contains(filter.positionSubstring)))
                {
                    shouldPrint = false;
                }

                if ((worker.signingDate < filter.signingDateLowerBound) ||
                    (worker.signingDate > filter.signingDateUpperBound))
                {
                    shouldPrint = false;
                }

                if ((worker.contractDuration < filter.contractDurationLowerBound) ||
                    (worker.contractDuration > filter.contractDurationUpperBound))
                {
                    shouldPrint = false;
                }

                if ((worker.salary < filter.salaryLowerBound) ||
                    (worker.salary > filter.salaryUpperBound))
                {
                    shouldPrint = false;
                }

                if (shouldPrint)
                {
                    worker.Print();
                }
            }
        }
    }

    struct Filter
    {
        public string fullNameSubstring;
        public string positionSubstring;
        public DateTime? signingDateLowerBound, signingDateUpperBound;
        public uint? contractDurationLowerBound, contractDurationUpperBound;
        public float? salaryLowerBound, salaryUpperBound;
    }
}
