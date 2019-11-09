using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace WebVideoDownloader
{
    class VeohLinkExtract
    {
        private string Vlink;
        private string title;
        public VeohLinkExtract(string linktext) //constructor
        {
            Vlink = linktext;
        }
        public List<string> GetLinks()
        {
            Utilities MyUtility = new Utilities();
            List<string> links = new List<string>();
            Uri MainLink = new Uri(Vlink);
            string VideoID = MainLink.LocalPath.Replace("/watch/", "");
            string get_video_info = "https://www.veoh.com/watch/getVideo/" + VideoID;
            string htmltext = MyUtility.GetSource(get_video_info);
            JObject VLinks = JObject.Parse(htmltext);
            title = MyUtility.RemoveInvalidChar((string)VLinks["video"]["title"], "veoh");
            JObject src = (JObject)VLinks["video"]["src"];
            string link;
            foreach (var x in src)
            {
                string quality = x.Key;
                if (quality != "poster")
                {
                    JToken value = x.Value;
                    link = value.Value<string>();
                    links.Add(title + "|mp4|" + "video/mp4[" + quality + "]" + "|" + link);
                }                
            }
            return links;
        }
    }
}
