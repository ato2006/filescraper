using FileScraper.Core;
using FileScraper.Core.Configs;
using FileScraper.Logs;
using FileScraper.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace FileScraper
{
    class Program
    {
        private static string ConfigPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config.xml");
        private static Scraper _scraper;

        private static async Task Main(string[] args)
        {
            if (File.Exists(ConfigPath))
                Xml.LoadConfig(ConfigPath);

            string argInput;

            try
            {
                argInput = args[0];
            }
            catch { argInput = null; }

            string dmPath;
            DirectoryInfo chatDir;

            PathInput:
            Console.Clear();

            if (argInput == null)
            {
                Logger.Print("Path of files to be scraped: ", LogType.Info, false);
                dmPath = Console.ReadLine();
            }
            else
                dmPath= argInput;


            if (!Directory.Exists(dmPath))
            {
                Console.Clear();
                Logger.Print("Path was not found.", LogType.Error);
                Thread.Sleep(1000);
                goto PathInput;
            }
            else
                chatDir = new DirectoryInfo(dmPath);

            Console.Clear();

            Logger.Print("Press any key to start downloading...", LogType.Info);
            Console.Read();

            var downloader = new Downloader();
            _scraper = new Scraper(downloader, chatDir, dmPath);

            await _scraper.Execute();

            Console.WriteLine();

            Logger.Print($"{DateTime.Now} | Finished scraping and downloading all links and files!", LogType.Info);

            Console.ReadLine();
        }
    }
}
