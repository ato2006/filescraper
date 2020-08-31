using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileScraper.Core
{
    public static class Constants
    {
        // TODO: Clean this up (preferably abstract those strings into their own methods?), it's horrible

        public const string AttachmentUrl = "cdn.discordapp.com/attachments";
        public const string DiscordUrl = "discordapp.com";

        public const string HttpRegex = "(http|ftp|https)://([\\w_-]+(?:(?:\\.[\\w_-]+)+))([\\w.,@?^=%&:/~+#-]*[\\w@?^=%&/~+#-])?";
        public const string AttachmentRegex = "https?:\\/\\/cdn\\.discord(?:app)?\\.(?:com|net)\\/attachments\\/(?:\\d+)\\/(?:\\d+)\\/(?<filename>[^\\?\\s\"]+)(?<parameters>\\??[^\\s\"]*)";
    }
}
