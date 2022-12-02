using ConsoleTables;
using System.Diagnostics;

namespace explorer
{
    public static class Explorer
    {
        static DriveInfo[] drives;
        static int first = Cursor.nullPosition, last;
        public static string path = null;
        static DirectoryInfo[] dirs;
        static FileInfo[] files;
        static string dirName;
        static string[] pathToOpen;


        public static void Drives()
        {
            path = null;
            dirName = null;
            drives = DriveInfo.GetDrives();

            Console.WriteLine();
            Console.WriteLine("====================================================================================");
            var table = new ConsoleTable("                            ", "Этот компьютер", "               ");
            table.AddRow("                            ", "              ", "                               ");
            foreach (DriveInfo drive in drives)
            {
                table.AddRow("  " + drive.Name + " " +
                    Math.Round(drive.TotalFreeSpace / Math.Pow(2, 30), 1) + " Гб свободно из " +
                    Math.Round(drive.TotalSize / Math.Pow(2, 30), 1) + " Гб", "", "");
            }
            table.Write(Format.Minimal);
            Console.WriteLine();

            last = drives.Count() + Cursor.listCorrection;
            Cursor.listSize(first, last);
        }

        public static void SelDrive()
        {
            path = drives[Cursor.position - Cursor.nullPosition].Name;
            dirName = drives[Cursor.position - Cursor.nullPosition].Name;
            DirectoriesFiles();
        }

        static void DirectoriesFiles()
        {
            DirectoryInfo DPath = new DirectoryInfo(path);
            dirs = DPath.GetDirectories();
            files = DPath.GetFiles();
            string? strToArray = null;

            Console.WriteLine("  F1 - Создать папку   F2 - Создать файл   F3 - Удалить папку  F4 - Удалить файл");
            Console.WriteLine("====================================================================================");
            var table = new ConsoleTable("                          " + dirName, "               ", "               ");
            table.AddRow("                  Название                    ", "  Дата создания   ", " Тип       ");
            foreach (var dir in dirs)
            {
                table.AddRow("  " + dir.Name, dir.CreationTime, dir.Extension);
                strToArray = strToArray + "," + dir.FullName;
            }
            foreach (var file in DPath.GetFiles())
            {
                table.AddRow("  " + file.Name, file.CreationTime, file.Extension);
                strToArray = strToArray + "," + file.FullName;
            }
            if (strToArray != null)
            {
                pathToOpen = strToArray.Split(',');
            }

            table.Write(Format.Minimal);
            Console.WriteLine();

            last = dirs.Count() + files.Count() + Cursor.listCorrection;
            Cursor.listSize(first, last);
        }
        public static void SelDir()
        {
            path = Convert.ToString(pathToOpen[Cursor.position - Cursor.nullPosition + 1] + @"\");
            dirName = pathToOpen[Cursor.position - Cursor.nullPosition + 1];
            if (path.Contains('.'))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = pathToOpen[Cursor.position - Cursor.nullPosition + 1],
                    UseShellExecute = true
                });
                Console.Clear();
                Drives();

            }
            else
            {
                DirectoriesFiles();
                Cursor.PositionToStart();
            }
        }

        public static void CreateDir()
        {
            Console.Clear();
            Console.WriteLine("Введите названия создаваемой папки \n");
            string newDirName = Console.ReadLine();
            path = path + newDirName + @"\";
            Directory.CreateDirectory(path);
            Console.WriteLine("Папка успешно создана!");
            Thread.Sleep(1000);
            Console.Clear();
            Drives();

        }

        public static void CreateFile()
        {
            Console.Clear();
            Console.WriteLine("Введите названия создаемого файла.тип \n");
            string newFileName = Console.ReadLine();
            path = path + newFileName;
            FileStream fs = File.Create(path);
            Console.WriteLine("Файл успешно создан!");
            Thread.Sleep(1000);
            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            });
            Console.Clear();
            Drives();
        }

        public static void DellDir()
        {
            path = Convert.ToString(pathToOpen[Cursor.position - Cursor.nullPosition + 1] + @"\");
            if (Directory.Exists(path))
            {
                string[] paths = Directory.GetFiles(path);
                foreach (string path in paths)
                {
                    File.Delete(path);
                }
                Directory.Delete(path);
                Console.Clear();
                Console.WriteLine("Папка успешно удалена!");
                Thread.Sleep(1000);
                Console.Clear();
                Drives();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Такой папки нет");
            }
        }

        public static void DellFile()
        {
            path = Convert.ToString(pathToOpen[Cursor.position - Cursor.nullPosition + 1]);
            File.Delete(path);
            Console.Clear();
            Console.WriteLine("Файл успешно удален!");
            Thread.Sleep(1000);
            Console.Clear();
            Drives();
        }

        public static void ToConsole()
        {
            Drives();
        }

    }
}
