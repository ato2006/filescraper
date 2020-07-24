## Discord Filescraper
Scrapes files and links from exported Discord conversations

### Functionality
#### Basic File Scraping
In order to import existing Discord conversations, you can either drag and drop the folder containing all conversations onto the release build or launch the console application without any command line arguments.
After launching, you can then select the path of the conversations manually, and move on to follow the on-screen directions.

#### Saved Files
Files are saved in the ``\Attachments`` directory. In the corresponding subdirectories for each scraped conversation, you can then find the downloaded files. If set in the config file, there will be a ``#NameOfConversation`` file, containing all found links.
Links are sorted chronologically, the earliest links will be placed near the top of the text file. All empty directories will automatically be deleted once all files were downloaded.

### Configurations
#### Structure
Editing a config file requires creating one in the first place. Configs **must be** placed in the same directory as the executable file and **must be** named ``config.xml``. Should the scraper fail to resolve such a file, no config will be loaded, and the
scraper will continue to work just like without one. The format of the config file follows a typical Extensible Markup Language file structure. Below, an example configuration is supplied.

```xml
<root>
<setting name="Filter">
<con>exe</con>
<con>png</con>
<con>cpp</con>
</setting>
<setting name="Links">
<con>False</con>
</setting>
</root>
```

The configuration above will only scrape links that contain the file extensions ``exe``, ``png``, or ``cpp``, and disables link scraping. Keep in mind that the link scraping attribute has to be a value that can be parsed as a boolean accordingly.
