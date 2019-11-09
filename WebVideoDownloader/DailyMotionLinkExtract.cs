using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace WebVideoDownloader
{
    class DailyMotionLinkExtract
    {
        private string DMlink;
        private string title;
        private string nl = System.Environment.NewLine;
        public DailyMotionLinkExtract(string linktext) //constructor
        {
            DMlink = linktext;
        }
        public List<string> GetLinks()
        {
            Utilities MyUtility = new Utilities();            
            List<string> links = new List<string>();
            Uri MainLink = new Uri(DMlink);
            
            string get_video_info = "https://www.dailymotion.com/player/metadata" + MainLink.LocalPath;
            string htmltext = MyUtility.GetSource(get_video_info);
            JObject DMLinks = JObject.Parse(htmltext);
            title = MyUtility.RemoveInvalidChar((string)DMLinks["title"], "dailymotion");
            JObject qualities = (JObject)DMLinks["qualities"];
            string link = "";
            foreach (var x in qualities)
            {
                string quality = x.Key;                
                if(quality != "auto")
                {                    
                    JToken value = x.Value;
                    JObject linkOBJ = (JObject)value[1];
                    link = (string)linkOBJ["url"];
                    links.Add(title + "|mp4|" + "video/mp4[" + quality + "p]" + "|" + link);
                }             
            }
            return links;
        }
    }
}
}
