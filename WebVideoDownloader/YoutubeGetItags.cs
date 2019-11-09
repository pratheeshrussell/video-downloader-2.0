using System;

namespace WebVideoDownloader
{
    class YoutubeGetItags
    {

        public string GetItagDescription(string mime, string quality, string qLabel)
        {
            string Desc = "Unknown video";
            string FileType = "video";
            string[] mimeType = mime.Split(';');
            Desc = mimeType[0] + "( " + qLabel + " " + quality + ")";

            FileType = mimeType[0].ToLower().Replace("video/", "").Replace("audio/", "");
            return ("|" + FileType + "|" + Desc + "|");
        }

    }
}
