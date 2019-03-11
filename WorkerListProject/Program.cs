using System;
using System.Collections.Generic;

namespace WorkerListProject
{
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
    };

    class Program
    {
        static void Main(string[] args)
        {
           
        }
    }
}
