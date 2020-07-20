using System;

namespace FileScraper.Logs
{
    public static class Logger
    {
        public static void Print(string msg, LogType type, bool newLine = true)
        {
            switch (type)
            {
                case LogType.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("[+] ");
                    Console.ResetColor();
                    Console.Write(msg);
                    break;
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[-] ");
                    Console.ResetColor();
                    Console.Write(msg);
                    break;
                case LogType.Info:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("[!] ");
                    Console.ResetColor();
                    Console.Write(msg);
                    break;
                case LogType.Debug:
#if DEBUG
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("[*] ");
                    Console.ResetColor();
                    Console.Write(msg);
#endif
                    break;
            }

            if (newLine)
                Console.WriteLine();
        }
    }
}
