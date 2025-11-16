using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SimpleFuzzer
{
    class Program
    {
        private static readonly Random random = new Random();
        private static int totalTests = 0;
        private static int crashes = 0;
        private static readonly List<CrashInfo> crashInfoList = new List<CrashInfo>();

        static async Task Main(string[] args)
        {
            Console.WriteLine("🚀 Простой фаззер для .NET");
            Console.WriteLine("Нажмите Ctrl+C для остановки\n");

            // Целевая программа для тестирования (пример)
            string targetProgram = "/bin/echo";
            
            // Или протестируем собственную функцию
            Console.WriteLine("Выберите режим:");
            Console.WriteLine("1 - Тестирование внешней программы");
            Console.WriteLine("2 - Тестирование внутренней функции");
            
            var choice = Console.ReadLine();
            
            if (choice == "1")
            {
                await FuzzExternalProgram(targetProgram);
            }
            else
            {
                await FuzzInternalFunction();
            }
        }

        static async Task FuzzExternalProgram(string programPath)
        {
            while (true)
            {
                try
                {
                    string randomInput = GenerateRandomInput();
                    
                    var processStartInfo = new ProcessStartInfo
                    {
                        FileName = programPath,
                        Arguments = randomInput,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false
                    };

                    using var process = new Process();
                    process.StartInfo = processStartInfo;
                    
                    process.Start();
                    
                    // Ждем завершения (с таймаутом)
                    if (!process.WaitForExit(1000))
                    {
                        process.Kill();
                        Console.WriteLine("⏰ Таймаут");
                    }
                    
                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();
                    
                    if (process.ExitCode != 0)
                    {
                        crashes++;
                        Console.WriteLine($"💥 СБОЙ! Входные данные: '{randomInput}'");
                        Console.WriteLine($"   Код выхода: {process.ExitCode}");
                        Console.WriteLine($"   Ошибка: {error}");
                    }
                    else
                    {
                        Console.WriteLine($"✅ OK: '{Truncate(randomInput, 50)}'");
                    }
                    
                    totalTests++;
                    
                    // Небольшая пауза между тестами
                    await Task.Delay(10);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Ошибка фаззера: {ex.Message}");
                }
            }
        }

        static async Task FuzzInternalFunction()
        {
            Console.WriteLine("🔍 Тестирование функции ParseInteger...");
            Console.WriteLine("⏹️  Остановка после 5000 тестов\n");
            
            const int maxTests = 5000;
            int lastPercentage = -1;

            for (totalTests = 0; totalTests < maxTests; totalTests++)
            {
                try
                {
                    // Показать прогресс
                    int percentage = (totalTests * 100) / maxTests;
                    if (percentage != lastPercentage && percentage % 10 == 0)
                    {
                        Console.WriteLine($"📊 Прогресс: {percentage}% ({totalTests}/{maxTests})");
                        lastPercentage = percentage;
                    }

                    string randomInput = GenerateRandomInput();
                    
                    // Тестируем нашу "уязвимую" функцию
                    TestParseInteger(randomInput);
                    
                    if (totalTests % 100 == 0) // Показываем каждые 100 успешных тестов
                    {
                        Console.WriteLine($"✅ Тест {totalTests}: '{Truncate(randomInput, 30)}'");
                    }
                    
                    await Task.Delay(1); // Небольшая пауза
                }
                catch (Exception ex)
                {
                    crashes++;
                    string input = GenerateRandomInput(); // Получаем входные данные для логирования
                    
                    var crashInfo = new CrashInfo
                    {
                        TestNumber = totalTests,
                        Input = input,
                        ExceptionType = ex.GetType().Name,
                        Message = ex.Message,
                        StackTrace = ex.StackTrace
                    };
                    
                    crashInfoList.Add(crashInfo);
                    
                    Console.WriteLine($"💥 СБОЙ #{crashes} на тесте {totalTests}");
                    Console.WriteLine($"   Входные данные: '{Truncate(input, 100)}'");
                    Console.WriteLine($"   Тип исключения: {ex.GetType().Name}");
                    Console.WriteLine($"   Сообщение: {ex.Message}");
                    Console.WriteLine();
                }
            }

            // Показать финальную статистику
            PrintFinalReport();
        }

        // "Уязвимая" функция для тестирования
        static void TestParseInteger(string input)
        {
            // Имитация потенциально уязвимого кода
            if (string.IsNullOrEmpty(input))
                return;
                
            if (input.Length > 1000)
                throw new InvalidOperationException("Слишком длинная строка");
                
            if (input.Contains("CRASH"))
                throw new ArgumentException("Обнаружена ключевая строка CRASH");

            if (input.Contains("EXCEPTION"))
                throw new ApplicationException("Тестовое исключение");

            if (input.Contains("NULLREF"))
                throw new NullReferenceException("Тестовое NullReferenceException");
                
            // Имитация целочисленного переполнения
            if (int.TryParse(input, out int result))
            {
                if (result == int.MaxValue)
                    throw new OverflowException("Целочисленное переполнение");

                if (result < 0)
                    throw new ArgumentOutOfRangeException("Отрицательное число не допускается");
            }

            // Имитация проблем с памятью для очень длинных строк
            if (input.Length > 500 && input.Contains("MEMORY"))
                throw new OutOfMemoryException("Имитация нехватки памяти");

            // Имитация проблем с форматом
            if (input.StartsWith("%") && input.Length > 10)
                throw new FormatException("Неверный формат строки");
        }

        static string GenerateRandomInput()
        {
            // Разные стратегии генерации случайных данных
            var strategies = new Func<string>[]
            {
                () => GenerateRandomString(random.Next(1, 200)),
                () => GenerateRandomNumbers(random.Next(1, 50)),
                () => GenerateSpecialCharacters(),
                () => GenerateFormatStrings(),
                () => GenerateEdgeCases(),
                () => GenerateKnownCrashPatterns()
            };

            return strategies[random.Next(strategies.Length)]();
        }

        static string GenerateKnownCrashPatterns()
        {
            var patterns = new string[] { 
                "CRASH",
                "EXCEPTION",
                "NULLREF",
                "MEMORY" + new string('X', 600),
                "LONG_" + new string('A', 1000)
            };
            return patterns[random.Next(patterns.Length)];
        }

        static string GenerateRandomString(int length)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                // Случайные ASCII символы
                sb.Append((char)random.Next(32, 127));
            }
            return sb.ToString();
        }

        static string GenerateRandomNumbers(int length)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(random.Next(0, 10));
            }
            return sb.ToString();
        }

        static string GenerateSpecialCharacters()
        {
            var specialChars = new string[] { 
                "%s", "%n", "%x", 
                "../../etc/passwd", 
                "AAAAAAAA", 
                "\x00\x01\x02", 
                "{{{{{{{{", 
                ";;;;;;", 
                "\\\\\\",
                "2147483647",
                "-2147483648"
            };
            return specialChars[random.Next(specialChars.Length)];
        }

        static string GenerateFormatStrings()
        {
            var formats = new string[] { 
                "%s%s%s%s", 
                "%n%n%n%n", 
                "%08x%08x", 
                "%99999999d" 
            };
            return formats[random.Next(formats.Length)];
        }

        static string GenerateEdgeCases()
        {
            var edges = new string[] { 
                "", 
                " ", 
                "\0", 
                "\n", 
                "\r\n", 
                "\t", 
                "NULL", 
                "null", 
                "undefined",
                "0",
                "-1",
                "2147483647", // int.MaxValue
                "-2147483648" // int.MinValue
            };
            return edges[random.Next(edges.Length)];
        }

        static void PrintFinalReport()
        {
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("📊 ФИНАЛЬНЫЙ ОТЧЕТ ФАЗЗИНГА");
            Console.WriteLine(new string('=', 60));
            
            Console.WriteLine($"🔄 Всего тестов: {totalTests}");
            Console.WriteLine($"💥 Обнаружено сбоев: {crashes}");
            
            if (crashes > 0)
            {
                double crashRate = (double)crashes / totalTests * 100;
                Console.WriteLine($"📈 Процент сбоев: {crashRate:F2}%");
                
                Console.WriteLine($"\n🔍 Детали сбоев:");
                Console.WriteLine(new string('-', 60));
                
                // Группировка по типам исключений
                var crashGroups = crashInfoList.GroupBy(c => c.ExceptionType)
                                              .OrderByDescending(g => g.Count());
                
                foreach (var group in crashGroups)
                {
                    Console.WriteLine($"\n📂 {group.Key}: {group.Count()} сбоев");
                    foreach (var crash in group.Take(3)) // Показываем первые 3 примера каждого типа
                    {
                        Console.WriteLine($"   • Тест #{crash.TestNumber}: '{Truncate(crash.Input, 50)}'");
                        Console.WriteLine($"     Сообщение: {crash.Message}");
                    }
                    if (group.Count() > 3)
                    {
                        Console.WriteLine($"     ... и еще {group.Count() - 3} сбоев этого типа");
                    }
                }
                
                // Самые интересные сбои (с уникальными входными данными)
                Console.WriteLine($"\n🎯 Уникальные сбои:");
                var uniqueCrashes = crashInfoList.GroupBy(c => c.ExceptionType + c.Message)
                                                .OrderByDescending(g => g.Count());
                
                foreach (var group in uniqueCrashes.Take(5))
                {
                    var representative = group.First();
                    Console.WriteLine($"   • {group.Count()}x '{Truncate(representative.Input, 40)}'");
                    Console.WriteLine($"     → {representative.ExceptionType}: {representative.Message}");
                }
            }
            else
            {
                Console.WriteLine("🎉 Поздравляем! Сбоев не обнаружено.");
            }
            
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("🏁 Фаззинг завершен!");
        }

        static string Truncate(string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input) || input.Length <= maxLength)
                return input;
                
            return input.Substring(0, maxLength) + "...";
        }
    }

    public class CrashInfo
    {
        public int TestNumber { get; set; }
        public string Input { get; set; }
        public string ExceptionType { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}