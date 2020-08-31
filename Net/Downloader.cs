using FileScraper.Interfaces;
using FileScraper.Logs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FileScraper.Net
{
    public class Downloader : IDownloader
    {
        private int TotalCount = 0;

        /// <summary>
        /// Downloads multiple files synchronously without blocking.
        /// </summary>
        /// <param name="urlList">The list of URLs to download from.</param>
        /// <param name="path">The folder directory to download all the files to.</param>
        /// <returns>A task to download the files.</returns>
        public async Task DownloadFiles(List<string> urlList, string path)
        {
            int allFiles = urlList.Count();

            foreach (var url in urlList)
            {
                await DownloadFile(url, path + $@"\{TotalCount}{Path.GetFileName(new Uri(url).LocalPath)}");

                Update($"{TotalCount}/{allFiles} Files Downloaded");
            }

            TotalCount = 0;
        }

        /// <summary>
        /// Downloads a single file asynchronously.
        /// </summary>
        /// <param name="url">The URL to download the file from.</param>
        /// <param name="path">The path to download the file to.</param>
        /// <returns>A task to download the file.</returns>
        private async Task DownloadFile(string url, string path)
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

        private void Update(string msg)
        {
            Console.Title = msg;
        }
    }
}

