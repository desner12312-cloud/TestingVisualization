using System;
using System.Threading;

namespace TestingVisualization
{
    internal class Program
    {
        static Random rnd = new Random();

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            PrintBanner();

            // ЭТАП 1: запрос числа
            int userNumber = RequestNumber();

            // ЭТАП 2: визуальная проверка числа
            bool valid = SimulateScanning(userNumber);

            if (!valid)
            {
                // ЧИСЛО НЕВЕРНОЕ — тестирование мгновенно провалено
                PrintCriticalFailure(userNumber);
            }
            else
            {
                // ЧИСЛО ВЕРНОЕ — запускаем полное тестирование
                RunAllTests();
            }

            Console.WriteLine();
            Console.WriteLine("  Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        // ==================================================
        // Баннер
        // ==================================================
        static void PrintBanner()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("  ========================================================");
            Console.WriteLine("  |       СИСТЕМА АВТОМАТИЧЕСКОГО ТЕСТИРОВАНИЯ          |");
            Console.WriteLine("  |              Testing Framework v2.1                 |");
            Console.WriteLine("  ========================================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("  Допустимый диапазон входных данных: 40 - 43");
            Console.WriteLine();
        }

        // ==================================================
        // ЭТАП 1. Запрос числа
        // ==================================================
        static int RequestNumber()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  Для запуска тестирования введите любое число.");
            Console.ResetColor();
            Console.WriteLine();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("  Введите число >>> ");
                Console.ResetColor();

                string input = Console.ReadLine();

                int number;
                if (int.TryParse(input, out number))
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("  Число принято: " + number);
                    Console.ResetColor();
                    Thread.Sleep(500);
                    return number;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  Это не похоже на число. Попробуйте снова.");
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        // ==================================================
        // ЭТАП 2. Проверка числа — возвращает true если 40..43
        // ==================================================
        static bool SimulateScanning(int number)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  ========================================================");
            Console.WriteLine("  |              ПРОВЕРКА ВВЕДЁННОГО ЧИСЛА               |");
            Console.WriteLine("  ========================================================");
            Console.ResetColor();
            Console.WriteLine();

            string numberStr = number.ToString();
            Console.WriteLine("  Сканирование числа: " + numberStr);
            Console.Write("  [");

            for (int i = 0; i < numberStr.Length; i++)
            {
                Thread.Sleep(200);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(numberStr[i]);
                Console.ResetColor();
                Thread.Sleep(150);
            }
            Console.WriteLine("]");
            Console.WriteLine();

            string[] checks = new string[]
            {
                "Проверка формата числа",
                "Проверка на целочисленность",
                "Проверка диапазона 40-43",
                "Контрольная сумма",
                "Регистрация в тестовой среде"
            };

            // Проверка диапазона — ключевая
            bool inRange = number >= 40 && number <= 43;
            int failIndex = inRange ? -1 : 2; // индекс "Проверка диапазона 40-43"

            int current = 0;
            foreach (var check in checks)
            {
                Console.Write("  > " + check.PadRight(45));

                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(rnd.Next(120, 250));
                    Console.Write(".");
                }
                Thread.Sleep(rnd.Next(80, 180));

                if (current == failIndex)
                {
                    // КРАСНЫЙ ПРОВАЛ
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  [FAILED]");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("      -> Число " + number + " вне допустимого диапазона [40-43]!");
                    Console.ResetColor();

                    Thread.Sleep(600);
                    return false;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  [OK]");
                Console.ResetColor();

                DrawInlineProgress(current + 1, checks.Length);
                current++;
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Число " + number + " успешно прошло проверку.");
            Console.ResetColor();
            Thread.Sleep(500);
            return true;
        }

        static void DrawInlineProgress(int current, int total)
        {
            int width = 30;
            int filled = current * width / total;

            Console.Write("      [");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(new string('#', filled));
            Console.ResetColor();
            Console.Write(new string('-', width - filled));
            Console.WriteLine("] " + (current * 100 / total) + "%");
        }

        // ==================================================
        // КРИТИЧЕСКИЙ ПРОВАЛ
        // ==================================================
        static void PrintCriticalFailure(int number)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;

            // Мигающее сообщение
            for (int i = 0; i < 3; i++)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine("  ########################################################");
                Console.WriteLine("  #                                                      #");
                Console.WriteLine("  #            !!! КРИТИЧЕСКАЯ ОШИБКА !!!                #");
                Console.WriteLine("  #                                                      #");
                Console.WriteLine("  ########################################################");
                Thread.Sleep(180);

                Console.Clear();
                Thread.Sleep(120);
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine("  ########################################################");
            Console.WriteLine("  #                                                      #");
            Console.WriteLine("  #            !!! КРИТИЧЕСКАЯ ОШИБКА !!!                #");
            Console.WriteLine("  #                                                      #");
            Console.WriteLine("  ########################################################");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("  Введённое число:      " + number);
            Console.WriteLine("  Допустимый диапазон:  40 - 43");
            Console.WriteLine("  Статус:               ЗА ПРЕДЕЛАМИ ДОПУСТИМОГО");
            Console.ResetColor();
            Console.WriteLine();

            Thread.Sleep(500);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  Прерывание тестирования на этапе валидации входных данных...");
            Thread.Sleep(700);
            Console.WriteLine("  Отмена всех тестовых наборов...");
            Thread.Sleep(500);
            Console.WriteLine("  Освобождение ресурсов тестовой среды...");
            Thread.Sleep(400);
            Console.WriteLine("  Формирование отчёта об ошибке...");
            Thread.Sleep(600);
            Console.ResetColor();
            Console.WriteLine();

            PrintFailureReport(number);
        }

        // ==================================================
        // Отчёт о полном провале
        // ==================================================
        static void PrintFailureReport(int number)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  ########################################################");
            Console.WriteLine("  #                  ОТЧЁТ О ПРОВАЛЕ                    #");
            Console.WriteLine("  ########################################################");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  Всего групп тестов:      3");
            Console.WriteLine("  Всего тестов:            0  (не запущены)");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Успешно:                 0   (0.0%)");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  Провалено:               17  (100.0%)   <-- ВСЕ ТЕСТЫ");
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine("  Покрытие кода:");

            DrawMetricBarFail("Строки", 0);
            DrawMetricBarFail("Ветки", 0);
            DrawMetricBarFail("Функции", 0);
            DrawMetricBarFail("Классы", 0);

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("  Причина провала:");
            Console.WriteLine("    Входное значение " + number + " не входит в допустимый");
            Console.WriteLine("    диапазон 40-43. Тестирование прервано.");
            Console.ResetColor();
            Console.WriteLine();

            // Три строки = драматический финал
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  ########################################################");
            Console.WriteLine("  #                                                      #");
            Console.WriteLine("  #            ТЕСТИРОВАНИЕ ПОЛНОСТЬЮ ПРОВАЛЕНО          #");
            Console.WriteLine("  #                                                      #");
            Console.WriteLine("  ########################################################");
            Console.ResetColor();
        }

        static void DrawMetricBarFail(string name, int percent)
        {
            int width = 30;
            int filled = percent * width / 100;

            Console.Write("    " + name.PadRight(10) + " [");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(new string('#', filled));
            Console.ResetColor();
            Console.Write(new string('-', width - filled));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("] " + percent + "%");
            Console.ResetColor();
        }

        // ==================================================
        // ЭТАП 3. Полное тестирование (при верном числе)
        // ==================================================
        static void RunAllTests()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  ========================================================");
            Console.WriteLine("  |              ЗАПУСК ТЕСТИРОВАНИЯ                     |");
            Console.WriteLine("  ========================================================");
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("  Инициализация тестового окружения...");
            Thread.Sleep(500);
            Console.WriteLine("  Загрузка тестовых сборок...");
            Thread.Sleep(400);
            Console.WriteLine("  Подготовка окружения завершена.");

            RunTestGroup("1. МОДУЛЬНОЕ ТЕСТИРОВАНИЕ", new string[]
            {
                "Test_Add_ReturnsCorrectSum",
                "Test_Subtract_PositiveNumbers",
                "Test_Multiply_ByZero",
                "Test_Divide_ValidInput",
                "Test_Divide_ByZero_ThrowsException",
                "Test_IsEven_TrueCase",
                "Test_IsEven_FalseCase"
            });

            RunTestGroup("2. ИНТЕГРАЦИОННОЕ ТЕСТИРОВАНИЕ", new string[]
            {
                "Integration_Database_Connection",
                "Integration_UserRepository_Save",
                "Integration_UserService_GetById",
                "Integration_API_ResponseFormat",
                "Integration_AuthFlow_Login"
            });

            RunTestGroup("3. СИСТЕМНОЕ ТЕСТИРОВАНИЕ", new string[]
            {
                "System_EndToEnd_UserRegistration",
                "System_EndToEnd_OrderProcessing",
                "System_Performance_UnderLoad",
                "System_Security_Authorization",
                "System_Compatibility_Browsers"
            });

            PrintFinalReport();
        }

        static void RunTestGroup(string groupName, string[] tests)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine("  +------------------------------------------------------+");
            Console.WriteLine("  | " + groupName.PadRight(52) + " |");
            Console.WriteLine("  +------------------------------------------------------+");
            Console.ResetColor();

            int passed = 0;
            int failed = 0;

            foreach (var test in tests)
            {
                Console.Write("  > Запуск: " + test.PadRight(45));

                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(rnd.Next(120, 280));
                    Console.Write(".");
                }
                Thread.Sleep(rnd.Next(80, 200));

                bool success = rnd.Next(100) < 85;

                if (success)
                {
                    passed++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("  [PASSED]");
                    Console.ResetColor();
                }
                else
                {
                    failed++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  [FAILED]");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("      -> AssertionError: expected 'true' but was 'false'");
                    Console.ResetColor();
                }
            }

            Console.WriteLine();
            DrawProgressBar(passed, tests.Length);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  Результат группы: " + passed + " успешно, " + failed + " провалено из " + tests.Length);
            Console.ResetColor();

            Thread.Sleep(350);
        }

        static void DrawProgressBar(int current, int total)
        {
            int width = 40;
            int filled = (int)((double)current / total * width);

            Console.Write("  [");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(new string('#', filled));
            Console.ResetColor();
            Console.Write(new string('-', width - filled));
            Console.Write("] " + (current * 100 / total) + "%");
            Console.WriteLine();
        }

        static void PrintFinalReport()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  ========================================================");
            Console.WriteLine("  |                  ИТОГОВЫЙ ОТЧЁТ                      |");
            Console.WriteLine("  ========================================================");
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine("  Всего групп тестов:      3");
            Console.WriteLine("  Всего тестов:            17");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  Успешно:                 15  (88.2%)");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  Провалено:               2   (11.8%)");
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("  Покрытие кода:");

            DrawMetricBar("Строки", 92);
            DrawMetricBar("Ветки", 85);
            DrawMetricBar("Функции", 100);
            DrawMetricBar("Классы", 95);

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  ========================================================");
            Console.WriteLine("     ТЕСТИРОВАНИЕ ЗАВЕРШЕНО УСПЕШНО");
            Console.WriteLine("  ========================================================");
            Console.ResetColor();
        }

        static void DrawMetricBar(string name, int percent)
        {
            int width = 30;
            int filled = percent * width / 100;

            Console.Write("    " + name.PadRight(10) + " [");
            Console.ForegroundColor = percent >= 90 ? ConsoleColor.Green :
                                      percent >= 70 ? ConsoleColor.Yellow :
                                                      ConsoleColor.Red;
            Console.Write(new string('#', filled));
            Console.ResetColor();
            Console.Write(new string('-', width - filled));
            Console.WriteLine("] " + percent + "%");
        }
    }
}