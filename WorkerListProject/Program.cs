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
            Console.WriteLine($"Фамилия работника: {fullName} Должность: {position}" +
                $" Дата подписания контракта: {signingDate} Срок действия контракта: {contractDuration}" +
                $" Оклад:{salary}");
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
            bool IsInRange(IComparable subject, IComparable lower, IComparable upper)
            {
#region Логика работы
                /// В таблице ниже,    'lower' — нижняя граница диапазона
                ///                    'upper' — верхняя граница диапазона
                ///                   'result' — возвращаемое значение:
                ///                              true если subject попадает в диапазон,
                ///                              false в противном случае;
                ///                'compareto' — результат вызова subject.CompareTo
                ///                     'null' — такая граница не указана;
                ///  ------------------------------------------------------------------
                /// | # | lower | upper | result | compareto(lower) | compareto(upper) |
                ///  ------------------------------------------------------------------
                /// | 1 | null  | null  |  true  |        1         |         1        |
                /// | 2 | null  |  >=   |  true  |        1         |        < 1       |
                /// | 3 | null  |   <   | false  |        1         |         1        |
                /// | 4 |   >   | null  | false  |       -1         |         1        |
                /// | 5 |  <=   | null  |  true  |      > -1        |         1        |
                /// | 6 |   >   |  >=   | false  |       -1         |        < 1       |
                /// | 7 |   >   |   <   | false  |       -1         |         1        |
                /// | 8 |  <=   |  >=   | true   |      > -1        |        < 1       |
                /// | 9 |  <=   |   <   | false  |      > -1        |        -1        |
                ///  ------------------------------------------------------------------
                /// В данной таблице существуют пересечения значений compareto у правильных и
                /// неправильных диапазонов:
                ///     Вторая строка пересекается с девятой;
                ///     Первая строка пересекается с третьей;
                ///     Пятая  строка пересекается с третьей.
                /// В связи с этим также были добавлены проверки на null.
#endregion
                return
                    ((subject.CompareTo(lower) > -1) &&
                     (subject.CompareTo(upper) <  1))
                    ||
                    ((subject.CompareTo(lower) > -1) &&
                     (subject.CompareTo(upper) == 1) &&
                     (lower != null) &&
                     (upper == null))
                    ||
                    ((subject.CompareTo(lower) == 1) &&
                     (subject.CompareTo(upper) <  1))
                    ||
                    ((subject.CompareTo(lower) == 1) &&
                     (subject.CompareTo(upper) == 1) &&
                     (upper == null));
            }

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
                if ((filter.signingDateLowerBound != null) ||
                    (filter.signingDateUpperBound != null))
                {
                    shouldPrint = IsInRange(worker.signingDate,
                            filter.signingDateLowerBound,
                            filter.signingDateUpperBound);
                }
                if ((filter.contractDurationLowerBound != null) ||
                    (filter.contractDurationUpperBound != null))
                {
                    shouldPrint = IsInRange(worker.contractDuration,
                            filter.contractDurationLowerBound,
                            filter.contractDurationUpperBound);
                }
                if ((filter.salaryLowerBound != null) ||
                    (filter.salaryUpperBound != null))
                {
                    shouldPrint = IsInRange(worker.salary,
                            filter.salaryLowerBound,
                            filter.salaryUpperBound);
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
