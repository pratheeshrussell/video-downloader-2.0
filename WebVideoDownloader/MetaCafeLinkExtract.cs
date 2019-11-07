using System;
using System.Web;
using System.Web.Script.Serialization;
using HtmlAgilityPack;
using System.Windows.Forms;
using System.Linq;
using Newtonsoft;
using Microsoft.CSharp;
using Newtonsoft.Json;
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
        public string GetLinks()
        {
            Utilities GetSourceUtility = new Utilities();
            string source = GetSourceUtility.GetSource(MClink);
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
            string source = MyUtility.GetSource(MSrc);
            multiline.Text = source;
            List<string> quality = new List<string>();
            foreach (string line in multiline.Lines)
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
                multiline.Text = qsource;
                string small_links = "";
                foreach (string line in multiline.Lines)
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
                links += title + "|mp4|" + qtext[1].Replace(".m3u8", "") + "|"+ Download_links[i] + nl;
            }
            return links;
        }
    }
}

