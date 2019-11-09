using System.Text;
using System;
using HtmlAgilityPack;
using System.Linq;
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
            List<string> links = new List<string>();
            return links;
        }
}
