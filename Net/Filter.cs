using FileScraper.Core;
using FileScraper.Core.Configs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileScraper.Net
{
    public static class Filter
    {
        public static bool IsValid(string file) 
        {
            if (!Options.UseConfig)
                return true;

            return Options.Filters.Any(Path.GetExtension(file).Contains) && !file.Contains(Constants.CLOUDFLARE) && !file.Contains(Constants.TWEMOJI); 
        }

        //TODO: maybe add more?
    }
}
