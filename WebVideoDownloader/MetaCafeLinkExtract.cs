using System;
using HtmlAgilityPack;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace WebVideoDownloader
{
    class MetaCafeLinkExtract
    {
        private string MClink;
        private string title;
        private string nl = System.Environment.NewLine;
        public MetaCafeLinkExtract(string linktext) //constructor
        {
            MClink = linktext;
        }
        
        private List<string> GetLinks()
        {
            
            Utilities MyUtility = new Utilities();
            string htmltext = MyUtility.GetSource(MClink);
            
            HtmlAgilityPack.HtmlDocument htmldoc = new HtmlAgilityPack.HtmlDocument();
            htmldoc.LoadHtml(htmltext);
            List<string> links = new List<string>();
            string Jstext = "";
            //TextBox multiline = new TextBox(); //remove this
           // multiline.Multiline = true;
            title = htmldoc.DocumentNode.SelectSingleNode("//head/title").InnerText.ToString();
            title = MyUtility.RemoveInvalidChar(title, "metacafe");
            HtmlNodeCollection allnodes = htmldoc.DocumentNode.SelectNodes("//script[@id='json_video_data']");
            // getting the m3u8 file
            Jstext = MyUtility.DecodeUrlString(allnodes[0].InnerText);
            Jstext = Jstext.Replace("\\/", "/");
            JObject parsed_Jstext = JObject.Parse(Jstext);
            
            var MSrc = (string)parsed_Jstext["sources"][0]["src"];
            var Mvidurl = (string)parsed_Jstext["flashvars"]["video_url"];
            var Mvidurlalt = (string)parsed_Jstext["flashvars"]["video_alt_url"];
            //get value of src to get video quality   
            string[] multiline;
            string source = MyUtility.GetSource(MSrc);
            multiline = source.Split(System.Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            List<string> quality = new List<string>();
            foreach (string line in multiline)
            {
                if ((!line.Contains("#")) & (line != ""))
                {
                    quality.Add(line);
                }
            }
            string main_link = System.IO.Path.GetDirectoryName(MSrc);
            main_link = main_link.Replace("\\","/").Replace("https:/", "https://");
            //create array to store smaller files
            string[] Download_links = new string[quality.Count()];
            int i = 0;
            foreach (string q in quality)
            {
                string qsource = MyUtility.GetSource(main_link+"/"+q);
                string[] multiline2;
                multiline2 = qsource.Split(System.Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string small_links = "";
                foreach (string line in multiline2)
                {
                    if ((!line.Contains("#")) & (line != ""))
                    {
                        small_links += main_link +"/" + line + ";";
                    }
                }
                //remove trailing ;
                small_links = small_links.Remove(small_links.Length - 1, 1); 
                Download_links[i] = small_links;
                i += 1;
            }
            // format and return them
            for (i=0;i<= (quality.Count()-1); i++)
            {
                var qtext = quality[i].Split('_');
                links.Add(title + "|mp4|" + qtext[1].Replace(".m3u8", "") + "|"+ Download_links[i]);
            }
            return links;
        }
    }
}

