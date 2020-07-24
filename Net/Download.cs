using FileScraper.Logs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FileScraper.Net
{
    public static class Download
    {
        public static int TotalCount = 0;
        public static async Task DownloadFiles(List<string> urlList, string path)
        {
            int allFiles = urlList.Count();
            foreach (var url in urlList)
            {
                await DownloadFile(url, path + $@"\{TotalCount}{Path.GetFileName(new Uri(url).LocalPath)}");

                Console.Title = $"{TotalCount}/{allFiles} Files Downloaded";
            }
        }

        private static async Task DownloadFile(string url, string path)
        {
            string fileName = Path.GetFileName(path);
            using var wc = new WebClient();

            try
            {
                await wc.DownloadFileTaskAsync(url, path);
                TotalCount++;
                Logger.Print($"Saved {fileName}", LogType.Success);
            }
            catch (Exception e)
            {
                Logger.Print(e.Message, LogType.Error);
            }
        }
    }
}

