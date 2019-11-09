using System;
using System.Web;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace WebVideoDownloader
{
    // look here https://api.w3hills.com/
    public class YouTubeLinkExtract
    {
        private string Ytlink;
        public string SigGenerationKey = "SE1,R,EX13,R"; // this format
        private string nl = System.Environment.NewLine;

        public YouTubeLinkExtract(string linktext) //constructor
        {
            Ytlink = linktext;
        }
        
        public List<string> GetLinks()
        {
            Utilities MyUtility = new Utilities();

            string htmltext = MyUtility.GetSource(Ytlink);

            HtmlAgilityPack.HtmlDocument htmldoc = new HtmlAgilityPack.HtmlDocument();
            htmldoc.LoadHtml(htmltext);
            //string links = "";
            string Jstext = "";
            string title = "";
            title = htmldoc.DocumentNode.SelectSingleNode("//head/title").InnerText.ToString();

            title = MyUtility.RemoveInvalidChar(title, "youtube");

            Uri myUri = new Uri(Ytlink);
            string VideoId = HttpUtility.ParseQueryString(myUri.Query).Get("v");

            string get_video_info = "http://youtube.com/get_video_info?video_id=" + VideoId + "&el=detailpage"; //&el=detailpage 
            
            string VideoInfo = MyUtility.GetSource(Ytlink);
            VideoInfo = MyUtility.DecodeUrlString(VideoInfo);
            htmldoc.LoadHtml(VideoInfo);
 
            HtmlNodeCollection allnodes = htmldoc.DocumentNode.SelectNodes("//script");
            foreach (HtmlNode nde in allnodes)
            {
                if (nde.InnerText.Contains("ytplayer"))
                {
                    Jstext = MyUtility.DecodeUrlString(nde.InnerText);
                    Jstext = Jstext.Replace(@"\u0026", "&").Replace("\\\"", "\"").Replace("\\/", "/");
                    Jstext = Jstext.Replace("{\"itag\"", nl + "{\"itag\"");
                    Jstext = Jstext.Replace("\\\\\"", "");
                    Jstext = Jstext.Replace("\\&", "&");
                    Jstext = Jstext.Replace("\"playerAds\"", nl + "\"playerAds\"");
                    break;
                }
            }
            

            var multilines = Jstext.Split(System.Environment.NewLine.ToCharArray(),StringSplitOptions.RemoveEmptyEntries);

            // try to create object
            Jstext  = "";
            foreach (string line in multilines)
            {
                if (line.Contains("{\"itag\""))
                {
                    Jstext += line + nl;
                }
            }
            //create json
            Jstext = Jstext.Trim();
            Jstext = Jstext.Remove(Jstext.Length - 2, 2);
            Jstext = Jstext.Replace("\"adaptiveFormats\":", "");
            Jstext = Jstext.Replace("[", "").Replace("]", "");
            //Jstext = "{\"all_links\":[" + Jstext + "]}";
            Jstext = "[" + Jstext + "]";
            JArray parsed_Jstext = JArray.Parse(Jstext); // contains json of all links & description

            List<string> links = new List<string>();

            string fileext_desc = "";
            Uri dl_link;

            foreach(JObject jsonlinks in parsed_Jstext)
            {
                int count = jsonlinks.Count;
                string link = (string)jsonlinks["url"];
                string json_mime = (string)jsonlinks["mimeType"];
                string json_quality = (string)jsonlinks["quality"];
                string json_qualityLabel = (string)jsonlinks["qualityLabel"];

                if ((link == null) || (link == ""))
                {
                    //protected
                    link = (string)jsonlinks["cipher"];
                }
                string temp_link = MakeDlLink(link);
                    dl_link = new Uri(temp_link);
                string param_s = HttpUtility.ParseQueryString(dl_link.Query).Get("s");
                string param_itag = HttpUtility.ParseQueryString(dl_link.Query).Get("itag");

                string param_mime = HttpUtility.ParseQueryString(dl_link.Query).Get("mime");

                //if protected video
                if ((param_s != null) & (param_s != ""))
                {
                    string param_sig = "";
                    YouTubeGenerateSig newsig = new YouTubeGenerateSig(param_s, SigGenerationKey);
                    param_sig = newsig.GenerateSig();

                    var newQueryString = HttpUtility.ParseQueryString(dl_link.Query);

                    // changes keys
                    newQueryString.Remove("s");
                    newQueryString.Remove("sp");
                    newQueryString.Set("sig", param_sig);
                    
                    string EditedQueryString = dl_link.GetLeftPart(UriPartial.Path);
                    string newuri = newQueryString.Count > 0
        ? String.Format("{0}?{1}", EditedQueryString, newQueryString) : EditedQueryString;
                    dl_link = new Uri(newuri);

                }

                YoutubeGetItags ItagDesc = new YoutubeGetItags();
                fileext_desc = ItagDesc.GetItagDescription(json_mime, json_quality, json_qualityLabel);
                links.Add(title + fileext_desc + dl_link.ToString());
                //break; //remove this or only 1 link will be processed
            }        
            return links;
        }

        private string MakeDlLink(string input)
        {
            if (input.ToLower().StartsWith("url=") || input.ToLower().StartsWith("https"))
            {
                return input.Replace("url=", "");
                
            } else
            {
                string[] temp = input.Split(new[] { "&url=" }, StringSplitOptions.RemoveEmptyEntries);
                // Replace("&url=", "\n").Split(Environment.NewLine);
                return (temp[1] + "&" + temp[0]);
            }
            
        }
       

    }
}
