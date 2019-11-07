using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace WebVideoDownloader
{
    public partial class Form1 : Form
    {
        public static Form1 form = null;
        private DownloadFile Startdownload;
        public Form1()
        {
            InitializeComponent();
            form = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void HandlesTextbox1_TextChange(object sender, EventArgs e)
        {
            //do nothing
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string linktext = textBox1.Text;
            string links = "";
            if (linktext.ToLower().Contains("/watch?v") & linktext.ToLower().Contains(".youtube"))
            {
                YouTubeLinkExtract ytdownload = new YouTubeLinkExtract(linktext);
                links = ytdownload.GetLinks();
                SetLinks(links);
            }
        }
        private static void SetLinks(string text)
        {
            TextBox multiline = new TextBox();
            multiline.Multiline = true;
            multiline.Text = text;
            String[] str = new string[3];
            foreach(string line in multiline.Lines)
            {
                str = line.Split('|');
                ListViewItem listitem = new ListViewItem(str);
                form.listView1.Items.Add(listitem);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                string filename = item.SubItems[0].Text + "." + item.SubItems[1].Text;
                Startdownload = new DownloadFile( item.SubItems[3].Text, filename);
                Startdownload.progressbarname = this.progressBar1;
                Startdownload.DownloadProgressLabel = this.label3;
                Startdownload.SpeedLabel = this.label4;
                Startdownload.FileSizeLabel = this.label5;
                Startdownload.DownloadPathLabel = this.label6;               
                Startdownload.StartDownload();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Startdownload.Cancel_work = true;
        }
    }


    



        







}
