using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebVideoDownloader
{
    class YouTubeLinkExtractor
    {
        private string url;

        public YouTubeLinkExtractor(string url)
        {
            this.url = url;
        }

        public List<VideoLink> GetLinks()
        {
            HtmlDocument htmldoc = new HtmlDocument();

            var videoPageSource = Utilities.GetPageSource(this.url);
            videoPageSource = HttpUtility.UrlDecode(videoPageSource);
            htmldoc.LoadHtml(videoPageSource);

            var title = htmldoc.DocumentNode.SelectSingleNode("//head/title").InnerText.ToString();
            title = Utilities.RemoveInvalidChar(title, "youtube");

            var allnodes = htmldoc.DocumentNode.SelectNodes("//script");
            var jstext = "";
            foreach (var node in allnodes)
            {
                if (node.InnerText.Contains("ytplayer"))
                {
                    jstext = HttpUtility.UrlDecode(node.InnerText);
                    break;
                }
            }

            jstext = jstext.Replace("url=", System.Environment.NewLine + "url=");
            jstext = jstext.Replace("\\u0", System.Environment.NewLine);

            var lines = jstext.Split(System.Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            List<VideoLink> links = new List<VideoLink>();
            foreach (string line in lines)
            {
                if (line.Contains("url=") & line.Contains("/videoplayback?"))
                {
                    var downloadLink = new Uri(line.Replace("url=", ""));
                    links.Add(new VideoLink(title, this.GetExtension(downloadLink), this.GetDescription(downloadLink), downloadLink.ToString()));
                }
            }
            return links;
        }

        private string GetDescription(Uri downloadLink)
        {
            var itag = HttpUtility.ParseQueryString(downloadLink.Query).Get("itag");
            var mime = HttpUtility.ParseQueryString(downloadLink.Query).Get("mime");

            var itagMap = new YouTubeITag();
            return itagMap.GetDescriptionForITag(itag,mime);
        }

        private string GetExtension(Uri downloadLink)
        {
            var mime = HttpUtility.ParseQueryString(downloadLink.Query).Get("mime");
            return mime.ToLower().Replace("video/", "").Replace("audio/", "");
        }
    }
}
