using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNET_module_16_homework
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("ЗАДАЧА №1-6");

                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Просмотр содержимого директории");
                Console.WriteLine("2. Создание файла/директории");
                Console.WriteLine("3. Удаление файла/директории");
                Console.WriteLine("4. Копирование файла/директории");
                Console.WriteLine("5. Перемещение файла/директории");
                Console.WriteLine("6. Чтение файла");
                Console.WriteLine("7. Запись в файл");
                Console.WriteLine("8. Выход");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        ListDirectoryContents();
                        break;
                    case 2:
                        CreateFileOrDirectory();
                        break;
                    case 3:
                        DeleteFileOrDirectory();
                        break;
                    case 4:
                        CopyFileOrDirectory();
                        break;
                    case 5:
                        MoveFileOrDirectory();
                        break;
                    case 6:
                        ReadFile();
                        break;
                    case 7:
                        WriteToFile();
                        break;
                    case 8:
                        Console.WriteLine("До свидания!");
                        return;
                    default:
                        Console.WriteLine("Некорректный выбор.");
                        break;
                }
            }
        }

        static void ListDirectoryContents()
        {
            Console.WriteLine("Введите путь к директории:");
            string path = Console.ReadLine();

            try
            {
                string[] files = Directory.GetFiles(path);
                string[] directories = Directory.GetDirectories(path);

                Console.WriteLine("Файлы:");
                foreach (var file in files)
                {
                    Console.WriteLine(file);
                }

                Console.WriteLine("Директории:");
                foreach (var directory in directories)
                {
                    Console.WriteLine(directory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void CreateFileOrDirectory()
        {
            Console.WriteLine("Введите путь для создания файла или директории:");
            string path = Console.ReadLine();

            Console.WriteLine("Выберите тип (1 - файл, 2 - директория):");
            int type;
            if (!int.TryParse(Console.ReadLine(), out type))
            {
                Console.WriteLine("Некорректный ввод.");
                return;
            }

            try
            {
                if (type == 1)
                {
                    File.Create(path);
                    Console.WriteLine("Файл создан успешно.");
                }
                else if (type == 2)
                {
                    Directory.CreateDirectory(path);
                    Console.WriteLine("Директория создана успешно.");
                }
                else
                {
                    Console.WriteLine("Некорректный выбор типа.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void DeleteFileOrDirectory()
        {
            Console.WriteLine("Введите путь к файлу или директории для удаления:");
            string path = Console.ReadLine();

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    Console.WriteLine("Файл удален успешно.");
                }
                else if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    Console.WriteLine("Директория удалена успешно.");
                }
                else
                {
                    Console.WriteLine("Файл или директория не найдены.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void CopyFileOrDirectory()
        {
            Console.WriteLine("Введите путь к исходному файлу или директории:");
            string sourcePath = Console.ReadLine();

            Console.WriteLine("Введите путь к месту назначения:");
            string destinationPath = Console.ReadLine();

            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Copy(sourcePath, destinationPath);
                    Console.WriteLine("Файл скопирован успешно.");
                }
                else if (Directory.Exists(sourcePath))
                {
                    Directory.CreateDirectory(destinationPath);
                    CopyDirectory(sourcePath, destinationPath);
                    Console.WriteLine("Директория скопирована успешно.");
                }
                else
                {
                    Console.WriteLine("Файл или директория не найдены.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void MoveFileOrDirectory()
        {
            Console.WriteLine("Введите путь к исходному файлу или директории:");
            string sourcePath = Console.ReadLine();

            Console.WriteLine("Введите путь к месту назначения:");
            string destinationPath = Console.ReadLine();

            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Move(sourcePath, destinationPath);
                    Console.WriteLine("Файл перемещен успешно.");
                }
                else if (Directory.Exists(sourcePath))
                {
                    Directory.Move(sourcePath, destinationPath);
                    Console.WriteLine("Директория перемещена успешно.");
                }
                else
                {
                    Console.WriteLine("Файл или директория не найдены.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void ReadFile()
        {
            Console.WriteLine("Введите путь к файлу для чтения:");
            string path = Console.ReadLine();

            try
            {
                if (File.Exists(path))
                {
                    string content = File.ReadAllText(path);
                    Console.WriteLine("Содержимое файла:");
                    Console.WriteLine(content);
                }
                else
                {
                    Console.WriteLine("Файл не найден.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void WriteToFile()
        {
            Console.WriteLine("Введите путь к файлу для записи:");
            string path = Console.ReadLine();

            Console.WriteLine("Введите текст для записи в файл:");
            string content = Console.ReadLine();

            try
            {
                File.WriteAllText(path, content);
                Console.WriteLine("Текст записан в файл успешно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void CopyDirectory(string sourceDir, string destinationDir)
        {
            if (!Directory.Exists(destinationDir))
            {
                Directory.CreateDirectory(destinationDir);
            }

            string[] files = Directory.GetFiles(sourceDir);
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(destinationDir, fileName);
                File.Copy(file, destFile, true);
            }

            string[] dirs = Directory.GetDirectories(sourceDir);
            foreach (string dir in dirs)
            {
                string dirName = Path.GetFileName(dir);
                string destDir = Path.Combine(destinationDir, dirName);
                CopyDirectory(dir, destDir);
            }
        }
    }
}
