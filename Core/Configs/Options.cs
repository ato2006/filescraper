using System;
using System.Collections.Generic;
using System.Text;

namespace FileScraper.Core.Configs
{
    public static class Options
    {
        public static bool UseConfig = false;
        public static List<string> Filters = new List<string>();
        public static List<bool> IncludeLinks = new List<bool>();
    }
}
