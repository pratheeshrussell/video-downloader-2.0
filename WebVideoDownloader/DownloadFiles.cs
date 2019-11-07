using System;
using System.ComponentModel;

using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace WebVideoDownloader
{
    public class DownloadFile
    {
        private string DownloadURI;
        private string Filename;

        public ProgressBar progressbarname = null;
        public Label DownloadProgressLabel = null;
        public Label SpeedLabel = null;
        public Label DownloadPathLabel = null;
        public Label FileSizeLabel = null;

        private string SavePath = "";
        //user controls
        private BackgroundWorker BackgroundDownloader;
        private SaveFileDialog saveFileDialog1;

        //for cancelling
        public bool Cancel_work = false;
        //For reporting
        private long FileSize;
        private long FileDownloaded;
        private double CurrentSpeed;
        //something unwanted
        private string nl = System.Environment.NewLine;
        public DownloadFile(string linktoDownload, string savename) //constructor
        {
            DownloadURI = linktoDownload;
            Filename = savename;
        }

        public void StartDownload()
        {
            string ext = "All|*.*";
            saveFileDialog1 = new SaveFileDialog();
            if (Filename.ToLower().Contains("webm")) { ext = "webm|*.webm"; }
            else if (Filename.ToLower().Contains("flv")) { ext = "flv|*.flv"; }
            else if (Filename.ToLower().Contains("mp4")) { ext = "mp4|*.mp4"; }
            else if (Filename.ToLower().Contains("3gp")) { ext = "3gp|*.3gp"; }
            saveFileDialog1.FileName = Filename;
            saveFileDialog1.Filter = ext;

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SavePath = saveFileDialog1.FileName;
                //initialize background worker
                this.BackgroundDownloader = new BackgroundWorker();
                this.BackgroundDownloader.DoWork += new DoWorkEventHandler(Bd_DoWork);
                this.BackgroundDownloader.ProgressChanged += new ProgressChangedEventHandler(Bd_ProgressChanged);
                this.BackgroundDownloader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Bd_RunWorkerCompleted);
                this.BackgroundDownloader.WorkerReportsProgress = true;
                this.BackgroundDownloader.WorkerSupportsCancellation = true;
                if (!this.BackgroundDownloader.IsBusy)
                {
                    this.BackgroundDownloader.RunWorkerAsync();
                }
            }
        }


        private void Bd_DoWork(object sender, DoWorkEventArgs e)
        {

            WebRequest theRequest;
            WebResponse theresponse;

            try
            {
                theRequest = HttpWebRequest.Create(DownloadURI);
                theresponse = theRequest.GetResponse();
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while downloading file. Possibe causes:" + nl +
                            "1) File doesn't exist" + nl +
                            "2) Remote server error" + nl +
                            "Try clicking the 'Find links' button again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cancel_work = true;
                return;
                throw;
            }
            FileSize = theresponse.ContentLength;
            Stopwatch speedtimer = new Stopwatch();
            FileStream writestream = new FileStream(SavePath, FileMode.Create);
            int readings = 0;
            int nread = 0;
            int speedread = 0;
            CurrentSpeed = -1;
            int percent = 0;

            do
            {
                if (Cancel_work == true)
                {
                    break;
                }
                speedtimer.Start();
                byte[] readBytes = new byte[4095];
                int bytesread = theresponse.GetResponseStream().Read(readBytes, 0, 4095);
                nread += bytesread;
                speedread += bytesread;
                FileDownloaded = nread;

                BackgroundDownloader.ReportProgress(0);
                if (bytesread == 0) { break; }
                writestream.Write(readBytes, 0, bytesread);
                speedtimer.Stop();
                readings += 1;
                if ((readings >= 5) & (speedtimer.ElapsedMilliseconds > 0))
                { //the speed it's calculated only every five cicles
                  //CurrentSpeed = 20480 / (speedtimer.ElapsedMilliseconds / 1000);
                    CurrentSpeed = speedread / (speedtimer.ElapsedMilliseconds);
                    speedread = 0;
                    speedtimer.Reset();
                    readings = 0;
                }
                percent = (int)(FileDownloaded * 100 / FileSize);
            } while (percent < 100);


            theresponse.GetResponseStream().Close();
            writestream.Close();
            if (Cancel_work == true)
            {
                System.IO.File.Delete(SavePath);
            }


        }

        private void Bd_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (progressbarname != null)
            {
                progressbarname.Value = (int)(FileDownloaded * 100 / FileSize);
            }
            if (DownloadProgressLabel != null)
            {

                DownloadProgressLabel.Text = "Downloaded " + ((int)FileDownloaded/(1024)) + " KB of " + ((int)FileSize/ (1024)) + " KB";
            }
            if (SpeedLabel != null & CurrentSpeed != -1)
            {
                SpeedLabel.Text = "Download Speed: " + CurrentSpeed + " kb/s";
            }

            if (DownloadPathLabel != null)
            {
                DownloadPathLabel.Text = "Save to: " + SavePath;
            }
            if (FileSizeLabel != null)
            {
                FileSizeLabel.Text = "File Size: " + ((int)FileSize / (1024 * 1024)) + " MB";
            }
        }
        private void Bd_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Cancel_work == true)
            {
                MessageBox.Show("Download Cancelled");
            }
            else
            {
                MessageBox.Show("File Downloaded");
            }
            if (progressbarname != null)
            {
                progressbarname.Value = 0;
            }
            if (DownloadProgressLabel != null)
            {

                DownloadProgressLabel.Text = "Progress:";
            }
            if (SpeedLabel != null & CurrentSpeed != -1)
            {
                SpeedLabel.Text = "Download Speed: ";
            }

            if (DownloadPathLabel != null)
            {
                DownloadPathLabel.Text = "Save to: ";
            }
            if (FileSizeLabel != null)
            {
                FileSizeLabel.Text = "File Size: ";
            }

        }
    }
}