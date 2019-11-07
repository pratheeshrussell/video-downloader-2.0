using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WebVideoDownloader
{
    public partial class VideoDownloader : Form
    {
        private DownloadFile Startdownload;
        public VideoDownloader()
        {
            InitializeComponent();
        }

        private void ClearLinks()
        {
            this.DownloadLinksList.Items.Clear();
        }

        private void SetLinks(List<VideoLink> links)
        {
            foreach(var link in links)
            {
                var item = new ListViewItem(link.ToArray());
                this.DownloadLinksList.Items.Add(item);
            }
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            if (DownloadLinksList.SelectedItems.Count > 0)
            {
                ListViewItem item = DownloadLinksList.SelectedItems[0];
                string filename = item.SubItems[0].Text + "." + item.SubItems[1].Text;
                Startdownload = new DownloadFile(item.SubItems[3].Text, filename);
                Startdownload.progressbarname = this.progressBar1;
                Startdownload.DownloadProgressLabel = this.label3;
                Startdownload.SpeedLabel = this.label4;
                Startdownload.FileSizeLabel = this.label5;
                Startdownload.DownloadPathLabel = this.label6;               
                Startdownload.StartDownload();
            }
        }

        private void DownloadCancelButton_Click(object sender, EventArgs e)
        {
            Startdownload.Cancel_work = true;
        }

        private void FindLinksButton_Click(object sender, EventArgs e)
        {
            ClearLinks();
            string linktext = textBox1.Text;
            if (linktext.ToLower().Contains("/watch?v") & linktext.ToLower().Contains(".youtube"))
            {
                var links = new YouTubeLinkExtractor(linktext).GetLinks();
                SetLinks(links);
            }
        }
    }


    



        







}
