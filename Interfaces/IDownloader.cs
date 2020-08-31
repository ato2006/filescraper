using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileScraper.Interfaces
{
    public interface IDownloader
    {
        Task DownloadFiles(List<string> urlList, string path);
    }
}
