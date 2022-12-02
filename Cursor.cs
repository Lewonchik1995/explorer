namespace explorer
{
    class Cursor
    {
        static int min, max;
        public static int position = 5;
        public static int nullPosition = 5;
        public static int listCorrection = nullPosition - 1;

        public static void listSize(int first, int last)
        {
            min = first;
            max = last;
        }

        public static void PositionToStart()
        {
            position = nullPosition;
            Console.SetCursorPosition(0, position);
            Console.WriteLine("->");
        }

        private static int Fix(int position)
        {
            if (position < min)
            {
                position = max;
            }
            else if (position > max)
            {
                position = min;
            }
            return position;
        }

        private static void Cursors()
        {
            PositionToStart();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(0, position);
                    Console.WriteLine("  ");
                    position = Fix(position + 1);
                    Console.SetCursorPosition(0, position);
                    Console.WriteLine("->");
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    Console.SetCursorPosition(0, position);
                    Console.WriteLine("  ");
                    position = Fix(position - 1);
                    Console.SetCursorPosition(0, position);
                    Console.WriteLine("->");
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (Explorer.path == null)
                    {
                        Console.Clear();
                        Explorer.SelDrive();
                        Console.SetCursorPosition(0, 0);
                        PositionToStart();
                    }
                    else
                    {
                        Console.Clear();
                        Explorer.SelDir();
                        Console.SetCursorPosition(0, 0);
                        PositionToStart();
                    }
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Explorer.path = null;
                    Console.SetCursorPosition(0, 0);
                    Explorer.Drives();
                    PositionToStart();
                }
                else if (key.Key == ConsoleKey.F1)
                {
                    Explorer.CreateDir();
                    PositionToStart();
                }
                else if (key.Key == ConsoleKey.F2)
                {
                    Explorer.CreateFile();
                    PositionToStart();
                }
                else if (key.Key == ConsoleKey.F3)
                {
                    Explorer.DellDir();
                    PositionToStart();
                }
                else if (key.Key == ConsoleKey.F4)
                {
                    Explorer.DellFile();
                    PositionToStart();
                }
            }
        }

        public static void ToConsole()
        {
            Explorer.ToConsole();
            Cursors();
        }
    }
}
