using System.Collections.Generic;

namespace WebVideoDownloader
{
    class YouTubeITag
    {
        private string[] itags = { "5", "6", "13", "17", "18", "22", "34", "35", "36", "37", "38", "43", "44", "45", "46", "82", "83", "84", "85", "100", "101", "102", "92", "93", "94", "95", "96", "132", "151", "133", "134", "135", "136", "137", "138", "160", "264", "298", "299", "266", "139", "140", "141", "167", "168", "169", "170", "218", "219", "220", "242", "243", "244", "245", "246", "247", "248", "271", "272", "302", "303", "308", "313", "315", "171", "172" };
        private string[] descriptions = { "Flv (400 x 240)", "Flv (450 x 270)", "3gp (Mobile phones, iPod friendly)", "3gp (176 x 144)", "mp4 (Medium Quality [360p])", "mp4 (HD High Quality [720p])", "Flv (360p)", "Flv [480p])", "3gp (240p)", "mp4 (HD High Quality [1080p])", "mp4 (HD High Quality [3072p])", "Webm (Medium Quality [360p])", "Webm (480p)", "mp4 (Webm [720p])", "Webm (1080p)", "mp4(640 x 360)", "mp4 (854 x 480)", "mp4 (1280 x 720)", "mp4(1920 x 1080p)", "Webm (640 x 360)", "Webm (854 x 480)", "Webm (1280 x 720)", "mp4(320 x 240)", "mp4 (640 x 360)", "mp4 (854 x 480)", "mp4 (1280 x 720)", "mp4 (1920 x 1080)", "mp4 (320 x 240)", "mp4 (* x 72)", "mp4(video only [320 x 240])", "mp4(video only [640 x 360])", "mp4(video only [854 x 480])", "mp4(video only [1280 x 720])", "mp4(video only [1920 x 1080])", "mp4(video only [* x 2160 ])", "mp4(video only [176 x 144])", "mp4(video only [176 x 1440])", "mp4(video only [1280 x 720])", "mp4(video only [1920 x 1080])", "mp4(video only [* x 2160])", "m4a(audio only)", "m4a(audio only)", "m4a(audio only)", "Webm(video only [640 x 360])", "Webm(video only [854 x 480])", "Webm(video only [1280 x 720])", "Webm(video only [1920 x 1080])", "Webm(video only [854 x 480])", "Webm(video only [854 x 480])", "Webm(video only [* x 144])", "Webm(video only [320 x 240])", "Webm(video only [640 x 360])", "Webm(video only [854 x 480])", "Webm(video only [854 x 480])", "Webm(video only [854 x 480])", "Webm(video only [1280 x 720])", "Webm(video only [1920 x 1080])", "Webm(video only [176 x 1440])", "Webm(video only [* x 2160])", "Webm(video only [* x 2160])", "Webm(video only [1920 x 1080])", "Webm(video only [176 x 1440])", "Webm(video only [* x 2160])", "Webm(video only [* x 2160])", "Webm(audio only)", "Webm(audio only)" };
        
        private readonly Dictionary<string, string> itagMap;

        public YouTubeITag()
        {
            this.itagMap = new Dictionary<string, string>();
            for (var i = 0; i < itags.Length; i++)
            {
                this.itagMap.Add(itags[i], descriptions[i]);
            }
        }

        internal string GetDescriptionForITag(string itag, string mime)
        {
            if (this.itagMap.ContainsKey(itag))
            {
                return itagMap[itag];
            }

            return "Unknown " + mime;
        }
    }
}
