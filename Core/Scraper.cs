using FileScraper.Core.Configs;
using FileScraper.Interfaces;
using FileScraper.Logs;
using FileScraper.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileScraper.Core
{
    class Scraper : IScraper
    {
        private readonly DirectoryInfo ChatDirectory;
        private readonly string ScrapePath;  

        private readonly IDownloader _downloader;

        public Scraper(IDownloader downloader, DirectoryInfo directory, string path)
        {
            this._downloader = downloader;
 
            this.ChatDirectory = directory;
            this.ScrapePath = path;
        }

        /// <summary>
        /// Executes and downloads all files.
        /// </summary>
        /// <returns>A task to download all the files from the scraped links.</returns>
        public async Task Execute()
        {
            string dlPath = Path.Combine(ScrapePath, "Attachments");

            if (Directory.Exists(dlPath))
                Directory.Delete(dlPath, true);

            foreach (var dm in ChatDirectory.GetFiles())
            {
                var dmPath = Path.Combine(dlPath, Path.GetFileName(dm.FullName));
                Directory.CreateDirectory(dmPath);

                if (Options.UseConfig)
                {
                    if (Options.IncludeLinks.First())
                    {
                        var dmLinks = GetContent(dm.FullName, Constants.HttpRegex, Constants.DiscordUrl, false);
                        string links = string.Join("\n", dmLinks);

                        string path = Path.Combine(dmPath, "#" + Path.GetFileName(dm.FullName) + ".txt");
                        File.WriteAllText(path, links);
                    }
                }
           
                var dmFiles = GetContent(dm.FullName, Constants.AttachmentRegex, Constants.AttachmentUrl, true);
                await _downloader.DownloadFiles(dmFiles, dmPath);
            }

            DeleteUnusedDirectories();
        }

        /// <summary>
        /// Deletes empty directories that were created.
        /// </summary>
        private void DeleteUnusedDirectories()
        {
            string dlPath = Path.Combine(ScrapePath, "Attachments");

            var dir = new DirectoryInfo(dlPath);

            foreach(var directory in dir.GetDirectories())
            {
                var tempDir = new DirectoryInfo(directory.FullName);

                if (tempDir.GetFiles().Count() < 2)
                    Directory.Delete(directory.FullName, true);
            }
        }

        /// <summary>
        /// Scrapes links and files from chats.
        /// </summary>
        /// <param name="path">The path to scrape from.</param>
        /// <param name="pattern">The pattern to search for.</param>
        /// <param name="constant">Constant strings that should be taken into account.</param>
        /// <param name="contains">Whether the filter should be contained.</param>
        /// <returns>A list of all found lines.</returns>
        private List<string> GetContent(string path, string pattern, string constant, bool contains)
        {
            var fileLines = File.ReadAllLines(path);

            var contentLines = fileLines.Where(x => x.Contains(constant) == contains);

            var foundURLs = new List<string>();

            foreach (string line in contentLines)
            {
                string foundLink;

                foundLink = Regex.Match(line, pattern).Value;

                if (!string.IsNullOrEmpty(foundLink) && Filter.IsValid(foundLink)) {
                    foundURLs.Add(foundLink);
                    Logger.Print(foundLink, LogType.Debug);
                }
                else
                    continue;
            }

            return foundURLs;
        }

    }
}
