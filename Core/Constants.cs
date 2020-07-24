﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileScraper.Core
{
    public static class Constants
    {
        public const string ATTACHMENT_LINK = "cdn.discordapp.com/attachments";
        public const string DISCORD_LINK = "discordapp.com";
        public const string CLOUDFLARE = "cdnjs.cloudflare.com";
        public const string TWEMOJI = "twemoji.maxcdn.com";
        public static string DM_PATH;
        public const string HTTP_REGEX = "(http|ftp|https)://([\\w_-]+(?:(?:\\.[\\w_-]+)+))([\\w.,@?^=%&:/~+#-]*[\\w@?^=%&/~+#-])?";
        public const string ATTC_REGEX = "https?:\\/\\/cdn\\.discord(?:app)?\\.(?:com|net)\\/attachments\\/(?:\\d+)\\/(?:\\d+)\\/(?<filename>[^\\?\\s\"]+)(?<parameters>\\??[^\\s\"]*)";
        public static DirectoryInfo CHAT_DIR;
    }
}
