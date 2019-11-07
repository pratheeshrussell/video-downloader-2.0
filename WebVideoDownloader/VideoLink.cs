namespace WebVideoDownloader
{
    class VideoLink
    {
        private readonly string title;
        private readonly string format;
        private readonly string description;
        private readonly string url;

        public VideoLink(string title, string format, string description, string url)
        {
            this.title = title;
            this.format = format;
            this.description = description;
            this.url = url;
        }

        internal string[] ToArray()
        {
            return new string[] { title, format, description, url };
        }
    }
}
