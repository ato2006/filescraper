using FileScraper.Core.Configs;
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
    class Scraper
    {
        public static async Task Execute()
        {
            string dlPath = Path.Combine(Constants.DM_PATH, "Attachments");

            if (Directory.Exists(dlPath))
                Directory.Delete(dlPath, true);

            foreach (var dm in Constants.CHAT_DIR.GetFiles())
            {
                var dmPath = Path.Combine(dlPath, Path.GetFileName(dm.FullName));
                Directory.CreateDirectory(dmPath);

                if (Options.UseConfig)
                {
                    if (Options.IncludeLinks.First())
                    {
                        var dmLinks = GetContent(dm.FullName, Constants.HTTP_REGEX, Constants.DISCORD_LINK, false);
                        string links = string.Join("\n", dmLinks);
                        File.WriteAllText(Path.Combine(dmPath, "#" + Path.GetFileName(dm.FullName) + ".txt"), links);
                    }
                }
           
                var dmFiles = GetContent(dm.FullName, Constants.ATTC_REGEX, Constants.ATTACHMENT_LINK, true);
                await Download.DownloadFiles(dmFiles, dmPath);

                Download.TotalCount = 0;
            }

            DeleteUnusedDirectories();
        }

        private static void DeleteUnusedDirectories()
        {
            string dlPath = Path.Combine(Constants.DM_PATH, "Attachments");

            var dir = new DirectoryInfo(dlPath);

            foreach(var directory in dir.GetDirectories())
            {
                var tempDir = new DirectoryInfo(directory.FullName);

                if (tempDir.GetFiles().Count() < 2)
                    Directory.Delete(directory.FullName, true);
            }
        }

        private static List<string> GetContent(string path, string pattern, string constant, bool contains)
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
