using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FileScraper.Logs;

namespace FileScraper.Net
{
    public static class Download
    {
        public static async Task DownloadFiles(List<string> urlList, string path)
        {
            int count = 0;
            int allFiles = urlList.Count();

            foreach (var url in urlList)
            {
                await DownloadFile(url, path + $@"\{count}{Path.GetFileName(new Uri(url).LocalPath)}");
                count++;
                Console.Title = $"{count}/{allFiles} Files Downloaded";
            }
        }

        private static async Task DownloadFile(string url, string path)
        {
            var fileName = Path.GetFileName(path);

            using (WebClient wc = new WebClient())
            {
                try
                {
                    await wc.DownloadFileTaskAsync(url, path);
                    Logger.Print($"Saved {fileName}", LogType.Success);
                }
                catch (Exception e)
                {
                    Logger.Print(e.Message, LogType.Error);
                }
            }
        }
    }
}
