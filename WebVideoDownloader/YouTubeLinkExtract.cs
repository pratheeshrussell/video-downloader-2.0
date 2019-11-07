using System;
using System.Web;
using HtmlAgilityPack;
using System.Windows.Forms;

namespace WebVideoDownloader
{
    public class YouTubeLinkExtract
    {
        private string Ytlink;
        private string title;
        private string nl = System.Environment.NewLine;
        public YouTubeLinkExtract(string linktext) //constructor
        {
            Ytlink = linktext;
        }
        public string GetLinks()
        {
            Utilities GetSourceUtility = new Utilities();
            string source = GetSourceUtility.GetSource(Ytlink);
            return ParseLinks(source);
        }

        private string ParseLinks(string htmltext)
        {
            Utilities MyUtility = new Utilities();

            HtmlAgilityPack.HtmlDocument htmldoc = new HtmlAgilityPack.HtmlDocument();
            htmldoc.LoadHtml(htmltext);
            string links = "";
            string Jstext = "";
            TextBox multiline = new TextBox();
            multiline.Multiline = true;
            title = htmldoc.DocumentNode.SelectSingleNode("//head/title").InnerText.ToString();

            title = MyUtility.RemoveInvalidChar(title, "youtube");

            Uri myUri = new Uri(Ytlink);
            string VideoId = HttpUtility.ParseQueryString(myUri.Query).Get("v");

            string get_video_info = "http://youtube.com/get_video_info?video_id=" + VideoId;
            //"&sts=18205"
            string VideoInfo = MyUtility.GetSource(Ytlink);
            VideoInfo = MyUtility.DecodeUrlString(VideoInfo);
            htmldoc.LoadHtml(VideoInfo);

            HtmlNodeCollection allnodes = htmldoc.DocumentNode.SelectNodes("//script");
            foreach (HtmlNode nde in allnodes)
            {
                if (nde.InnerText.Contains("ytplayer"))
                {
                    Jstext = MyUtility.DecodeUrlString(nde.InnerText);
                    break;
                }
            }


            Jstext = Jstext.Replace("url=", nl + "url=");
            Jstext = Jstext.Replace("\\u0", nl);
            multiline.Text = Jstext;
            string fileext_desc = "";
            Uri dl_link;
            foreach (string line in multiline.Lines)
            {
                if (line.Contains("url=") & line.Contains("/videoplayback?"))
                {
                    dl_link = new Uri(line.Replace("url=", ""));
                    fileext_desc = Youtube_itag(HttpUtility.ParseQueryString(dl_link.Query).Get("itag"), HttpUtility.ParseQueryString(dl_link.Query).Get("mime"));
                    links += title + fileext_desc + dl_link + nl;
                }
            }
            //Form1.SetText(links);
            return links;
        }

        private string Youtube_itag(string itag, string mime)
        {
            String[] All_Itags = { "5", "6", "13", "17", "18", "22", "34", "35", "36", "37", "38", "43", "44", "45", "46", "82", "83", "84", "85", "100", "101", "102", "92", "93", "94", "95", "96", "132", "151", "133", "134", "135", "136", "137", "138", "160", "264", "298", "299", "266", "139", "140", "141", "167", "168", "169", "170", "218", "219", "220", "242", "243", "244", "245", "246", "247", "248", "271", "272", "302", "303", "308", "313", "315", "171", "172" };
            String[] All_Desc = { "Flv (400 x 240)", "Flv (450 x 270)", "3gp (Mobile phones, iPod friendly)", "3gp (176 x 144)", "mp4 (Medium Quality [360p])", "mp4 (HD High Quality [720p])", "Flv (360p)", "Flv [480p])", "3gp (240p)", "mp4 (HD High Quality [1080p])", "mp4 (HD High Quality [3072p])", "Webm (Medium Quality [360p])", "Webm (480p)", "mp4 (Webm [720p])", "Webm (1080p)", "mp4(640 x 360)", "mp4 (854 x 480)", "mp4 (1280 x 720)", "mp4(1920 x 1080p)", "Webm (640 x 360)", "Webm (854 x 480)", "Webm (1280 x 720)", "mp4(320 x 240)", "mp4 (640 x 360)", "mp4 (854 x 480)", "mp4 (1280 x 720)", "mp4 (1920 x 1080)", "mp4 (320 x 240)", "mp4 (* x 72)", "mp4(video only [320 x 240])", "mp4(video only [640 x 360])", "mp4(video only [854 x 480])", "mp4(video only [1280 x 720])", "mp4(video only [1920 x 1080])", "mp4(video only [* x 2160 ])", "mp4(video only [176 x 144])", "mp4(video only [176 x 1440])", "mp4(video only [1280 x 720])", "mp4(video only [1920 x 1080])", "mp4(video only [* x 2160])", "m4a(audio only)", "m4a(audio only)", "m4a(audio only)", "Webm(video only [640 x 360])", "Webm(video only [854 x 480])", "Webm(video only [1280 x 720])", "Webm(video only [1920 x 1080])", "Webm(video only [854 x 480])", "Webm(video only [854 x 480])", "Webm(video only [* x 144])", "Webm(video only [320 x 240])", "Webm(video only [640 x 360])", "Webm(video only [854 x 480])", "Webm(video only [854 x 480])", "Webm(video only [854 x 480])", "Webm(video only [1280 x 720])", "Webm(video only [1920 x 1080])", "Webm(video only [176 x 1440])", "Webm(video only [* x 2160])", "Webm(video only [* x 2160])", "Webm(video only [1920 x 1080])", "Webm(video only [176 x 1440])", "Webm(video only [* x 2160])", "Webm(video only [* x 2160])", "Webm(audio only)", "Webm(audio only)" };
            int Array_loc = Array.FindIndex(All_Itags,
                       element => element.Equals(itag,
                       StringComparison.Ordinal));
            string Desc = "Unknown video";
            string FileType = "video";
            if (Array_loc >= 0)
            {
                Desc = All_Desc[Array_loc];
            }
            else
            {
                Desc = "Unknown " + mime;
            }
            FileType = mime.ToLower().Replace("video/", "").Replace("audio/", "");
            return ("|" + FileType + "|" + Desc + "|");
        }

    }
}
