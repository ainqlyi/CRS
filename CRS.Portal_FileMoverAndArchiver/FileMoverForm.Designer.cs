namespace CRS.Portal_FileMoverAndArchiver
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.distributeFilesButton = new System.Windows.Forms.Button();
            this.openFileDialog3 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog4 = new System.Windows.Forms.OpenFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.archiveFilesButton = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.reportsToDistributeHeaderTextBox = new System.Windows.Forms.TextBox();
            this.reportsToArchiveHeaderTextbox = new System.Windows.Forms.TextBox();
            this.reportsToArchiveTextBox = new System.Windows.Forms.TextBox();
            this.reportsToDistributeTextBox = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBarDistributeFiles = new System.Windows.Forms.ProgressBar();
            this.progressBarArchiveFiles = new System.Windows.Forms.ProgressBar();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // distributeFilesButton
            // 
            this.distributeFilesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.distributeFilesButton.ForeColor = System.Drawing.Color.DarkGreen;
            this.distributeFilesButton.Location = new System.Drawing.Point(12, 473);
            this.distributeFilesButton.Name = "distributeFilesButton";
            this.distributeFilesButton.Size = new System.Drawing.Size(801, 47);
            this.distributeFilesButton.TabIndex = 1;
            this.distributeFilesButton.Text = "Distribute Files";
            this.distributeFilesButton.UseVisualStyleBackColor = true;
            this.distributeFilesButton.Click += new System.EventHandler(this.distributeFilesButton_Click);
            // 
            // openFileDialog3
            // 
            this.openFileDialog3.FileName = "openFileDialog3";
            // 
            // openFileDialog4
            // 
            this.openFileDialog4.FileName = "openFileDialog4";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(13, 43);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(800, 89);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // archiveFilesButton
            // 
            this.archiveFilesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.archiveFilesButton.ForeColor = System.Drawing.Color.DarkGreen;
            this.archiveFilesButton.Location = new System.Drawing.Point(821, 473);
            this.archiveFilesButton.Name = "archiveFilesButton";
            this.archiveFilesButton.Size = new System.Drawing.Size(800, 47);
            this.archiveFilesButton.TabIndex = 4;
            this.archiveFilesButton.Text = "Archive Files";
            this.archiveFilesButton.UseVisualStyleBackColor = true;
            this.archiveFilesButton.Click += new System.EventHandler(this.archiveFilesButton_Click);
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(821, 43);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(800, 89);
            this.textBox3.TabIndex = 6;
            this.textBox3.Text = resources.GetString("textBox3.Text");
            // 
            // reportsToDistributeHeaderTextBox
            // 
            this.reportsToDistributeHeaderTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportsToDistributeHeaderTextBox.ForeColor = System.Drawing.Color.Blue;
            this.reportsToDistributeHeaderTextBox.Location = new System.Drawing.Point(12, 138);
            this.reportsToDistributeHeaderTextBox.Name = "reportsToDistributeHeaderTextBox";
            this.reportsToDistributeHeaderTextBox.ReadOnly = true;
            this.reportsToDistributeHeaderTextBox.Size = new System.Drawing.Size(801, 30);
            this.reportsToDistributeHeaderTextBox.TabIndex = 7;
            this.reportsToDistributeHeaderTextBox.Text = "Reports to Distribute:";
            // 
            // reportsToArchiveHeaderTextbox
            // 
            this.reportsToArchiveHeaderTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportsToArchiveHeaderTextbox.ForeColor = System.Drawing.Color.Blue;
            this.reportsToArchiveHeaderTextbox.Location = new System.Drawing.Point(822, 138);
            this.reportsToArchiveHeaderTextbox.Name = "reportsToArchiveHeaderTextbox";
            this.reportsToArchiveHeaderTextbox.ReadOnly = true;
            this.reportsToArchiveHeaderTextbox.Size = new System.Drawing.Size(800, 30);
            this.reportsToArchiveHeaderTextbox.TabIndex = 8;
            this.reportsToArchiveHeaderTextbox.Text = "Reports to Archive:";
            // 
            // reportsToArchiveTextBox
            // 
            this.reportsToArchiveTextBox.Location = new System.Drawing.Point(822, 175);
            this.reportsToArchiveTextBox.Multiline = true;
            this.reportsToArchiveTextBox.Name = "reportsToArchiveTextBox";
            this.reportsToArchiveTextBox.ReadOnly = true;
            this.reportsToArchiveTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.reportsToArchiveTextBox.Size = new System.Drawing.Size(800, 262);
            this.reportsToArchiveTextBox.TabIndex = 12;
            // 
            // reportsToDistributeTextBox
            // 
            this.reportsToDistributeTextBox.Location = new System.Drawing.Point(12, 175);
            this.reportsToDistributeTextBox.Multiline = true;
            this.reportsToDistributeTextBox.Name = "reportsToDistributeTextBox";
            this.reportsToDistributeTextBox.ReadOnly = true;
            this.reportsToDistributeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.reportsToDistributeTextBox.Size = new System.Drawing.Size(801, 263);
            this.reportsToDistributeTextBox.TabIndex = 13;
            // 
            // progressBarDistributeFiles
            // 
            this.progressBarDistributeFiles.Location = new System.Drawing.Point(13, 444);
            this.progressBarDistributeFiles.Name = "progressBarDistributeFiles";
            this.progressBarDistributeFiles.Size = new System.Drawing.Size(800, 23);
            this.progressBarDistributeFiles.TabIndex = 14;
            // 
            // progressBarArchiveFiles
            // 
            this.progressBarArchiveFiles.Location = new System.Drawing.Point(822, 443);
            this.progressBarArchiveFiles.Name = "progressBarArchiveFiles";
            this.progressBarArchiveFiles.Size = new System.Drawing.Size(800, 23);
            this.progressBarArchiveFiles.TabIndex = 15;
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.ForeColor = System.Drawing.Color.Blue;
            this.textBox5.Location = new System.Drawing.Point(13, 8);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(800, 30);
            this.textBox5.TabIndex = 16;
            this.textBox5.Text = "Instructions for Distributing:";
            // 
            // textBox6
            // 
            this.textBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.ForeColor = System.Drawing.Color.Blue;
            this.textBox6.Location = new System.Drawing.Point(822, 7);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(800, 30);
            this.textBox6.TabIndex = 17;
            this.textBox6.Text = "Instructions for Archiving:";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 527);
            this.splitter1.TabIndex = 18;
            this.splitter1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1632, 527);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.progressBarArchiveFiles);
            this.Controls.Add(this.progressBarDistributeFiles);
            this.Controls.Add(this.reportsToDistributeTextBox);
            this.Controls.Add(this.reportsToArchiveTextBox);
            this.Controls.Add(this.reportsToArchiveHeaderTextbox);
            this.Controls.Add(this.reportsToDistributeHeaderTextBox);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.archiveFilesButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.distributeFilesButton);
            this.Name = "MainForm";
            this.Text = "CRS Portal File Distributor and Archiver";
            this.Load += new System.EventHandler(this.Main_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Button distributeFilesButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog3;
        private System.Windows.Forms.OpenFileDialog openFileDialog4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button archiveFilesButton;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox reportsToDistributeHeaderTextBox;
        private System.Windows.Forms.TextBox reportsToArchiveHeaderTextbox;
        private System.Windows.Forms.TextBox reportsToArchiveTextBox;
        private System.Windows.Forms.TextBox reportsToDistributeTextBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBarDistributeFiles;
        private System.Windows.Forms.ProgressBar progressBarArchiveFiles;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Splitter splitter1;
    }
}

