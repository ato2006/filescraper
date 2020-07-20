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

                var dmLinks = GetContent(dm.FullName, "(http|ftp|https)://([\\w_-]+(?:(?:\\.[\\w_-]+)+))([\\w.,@?^=%&:/~+#-]*[\\w@?^=%&/~+#-])?", Constants.DISCORD_LINK, false);
                string links = string.Join("\n", dmLinks);

                File.WriteAllText(Path.Combine(dmPath, "#" + Path.GetFileName(dm.FullName) + ".txt"), links);

                var dmFiles = GetContent(dm.FullName, "https?:\\/\\/cdn\\.discord(?:app)?\\.(?:com|net)\\/attachments\\/(?:\\d+)\\/(?:\\d+)\\/(?<filename>[^\\?\\s\"]+)(?<parameters>\\??[^\\s\"]*)", Constants.ATTACHMENT_LINK, true);
                await Download.DownloadFiles(dmFiles, dmPath);
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

                if (!string.IsNullOrEmpty(foundLink) && !foundLink.Contains(Constants.CLOUDFLARE))
                {
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
