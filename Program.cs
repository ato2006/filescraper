using FileScraper.Core;
using FileScraper.Logs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace FileScraper
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            string argInput;

            try
            {
                argInput = args[0];
            }
            catch { argInput = null; }

            PathInput:
            Console.Clear();

            if (argInput == null)
            {
                Logger.Print("Path of files to be scraped: ", LogType.Info, false);
                Constants.DM_PATH = Console.ReadLine();
            }
            else
                Constants.DM_PATH = argInput;


            if (!Directory.Exists(Constants.DM_PATH))
            {
                Console.Clear();
                Logger.Print("Path was not found.", LogType.Error);
                Thread.Sleep(1000);
                goto PathInput;
            }
            else
                Constants.CHAT_DIR = new DirectoryInfo(Constants.DM_PATH);

            Console.Clear();

            Logger.Print("Press any key to start downloading...", LogType.Info);
            Console.Read();

            await Scraper.Execute();

            Console.WriteLine();

            Logger.Print($"{DateTime.Now} | Finished scraping and downloading all links and files!", LogType.Info);

            Console.ReadLine();
        }
    }
}
