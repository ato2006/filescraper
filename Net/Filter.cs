using FileScraper.Core;
using FileScraper.Core.Configs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileScraper.Net
{
    public static class Filter
    {
        private static List<string> GetBlacklist()
        {
            var blackList = new List<string>
            {
                "cdnjs.cloudflare.com",
                "twemoji.maxcdn.com",
            };

            return blackList;
        }

        public static bool IsValid(string file)
        {
            if (!Options.UseConfig)
                return true;

            string extension = Path.GetExtension(file);
            var blackList = GetBlacklist();

            return extension.ContainedIn(Options.Filters) &&
                !file.ContainedIn(blackList);
        }

        private static bool ContainedIn<T>(this T t, List<T> args) => args.Contains(t);

        //TODO: maybe add more?
    }
}
