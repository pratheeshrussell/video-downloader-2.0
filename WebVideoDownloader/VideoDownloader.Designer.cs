namespace WebVideoDownloader
{
    partial class VideoDownloader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.FindLinksButton = new System.Windows.Forms.Button();
            this.DownloadLinksList = new System.Windows.Forms.ListView();
            this.ColName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColFormat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColLink = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DownloadButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DownloadCancelButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Link";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(46, 18);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(344, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "https://www.youtube.com/watch?v=Fv1JJ227CQk";
            // 
            // FindLinksButton
            // 
            this.FindLinksButton.Location = new System.Drawing.Point(395, 10);
            this.FindLinksButton.Margin = new System.Windows.Forms.Padding(2);
            this.FindLinksButton.Name = "FindLinksButton";
            this.FindLinksButton.Size = new System.Drawing.Size(72, 35);
            this.FindLinksButton.TabIndex = 2;
            this.FindLinksButton.Text = "Find Links";
            this.FindLinksButton.UseVisualStyleBackColor = true;
            this.FindLinksButton.Click += new System.EventHandler(this.FindLinksButton_Click);
            // 
            // DownloadLinksList
            // 
            this.DownloadLinksList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColName,
            this.ColFormat,
            this.ColDesc,
            this.ColLink});
            this.DownloadLinksList.FullRowSelect = true;
            this.DownloadLinksList.HideSelection = false;
            this.DownloadLinksList.Location = new System.Drawing.Point(11, 50);
            this.DownloadLinksList.Margin = new System.Windows.Forms.Padding(2);
            this.DownloadLinksList.Name = "DownloadLinksList";
            this.DownloadLinksList.Size = new System.Drawing.Size(534, 183);
            this.DownloadLinksList.TabIndex = 5;
            this.DownloadLinksList.UseCompatibleStateImageBehavior = false;
            this.DownloadLinksList.View = System.Windows.Forms.View.Details;
            // 
            // ColName
            // 
            this.ColName.Text = "Name";
            this.ColName.Width = 150;
            // 
            // ColFormat
            // 
            this.ColFormat.Text = "Format";
            // 
            // ColDesc
            // 
            this.ColDesc.Text = "Description";
            this.ColDesc.Width = 100;
            // 
            // ColLink
            // 
            this.ColLink.Text = "Link";
            this.ColLink.Width = 400;
            // 
            // DownloadButton
            // 
            this.DownloadButton.Location = new System.Drawing.Point(472, 9);
            this.DownloadButton.Margin = new System.Windows.Forms.Padding(2);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(72, 36);
            this.DownloadButton.TabIndex = 6;
            this.DownloadButton.Text = "Download";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(14, 97);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(441, 28);
            this.progressBar1.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.DownloadCancelButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Location = new System.Drawing.Point(11, 250);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(532, 168);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Download";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 49);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Save to:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 73);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "File Size:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(288, 73);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Download Speed:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 137);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Progress:";
            // 
            // DownloadCancelButton
            // 
            this.DownloadCancelButton.Location = new System.Drawing.Point(459, 97);
            this.DownloadCancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.DownloadCancelButton.Name = "DownloadCancelButton";
            this.DownloadCancelButton.Size = new System.Drawing.Size(56, 28);
            this.DownloadCancelButton.TabIndex = 9;
            this.DownloadCancelButton.Text = "Cancel";
            this.DownloadCancelButton.UseVisualStyleBackColor = true;
            this.DownloadCancelButton.Click += new System.EventHandler(this.DownloadCancelButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Downloading: Link";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 448);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.DownloadLinksList);
            this.Controls.Add(this.FindLinksButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Video Downloader";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button FindLinksButton;
        private System.Windows.Forms.ListView DownloadLinksList;
        private System.Windows.Forms.ColumnHeader ColName;
        private System.Windows.Forms.ColumnHeader ColFormat;
        private System.Windows.Forms.ColumnHeader ColLink;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.ColumnHeader ColDesc;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button DownloadCancelButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

