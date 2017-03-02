namespace RestfulGeoCoder
{
    partial class GeoCoderForm
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.helpButton = new System.Windows.Forms.Button();
            this.outputBox = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.numAddressesTextBox = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.numberOfBatchesBox = new System.Windows.Forms.TextBox();
            this.textbox3 = new System.Windows.Forms.TextBox();
            this.numAddressesPerBatchTextBox = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.addressesProcessedTextBox = new System.Windows.Forms.TextBox();
            this.batchesProcessedTextBox = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.timeElapsedTextBox = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.addressesPerSecondTextBox = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.timeRemainingTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.addressesRemainingTextBox = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.batchesRemainingTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 281);
            this.progressBar.Maximum = 1000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(864, 55);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 1;
            // 
            // helpButton
            // 
            this.helpButton.Location = new System.Drawing.Point(792, 12);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(86, 29);
            this.helpButton.TabIndex = 2;
            this.helpButton.Text = "Help";
            this.helpButton.UseVisualStyleBackColor = true;
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // outputBox
            // 
            this.outputBox.AcceptsReturn = true;
            this.outputBox.Location = new System.Drawing.Point(13, 47);
            this.outputBox.Multiline = true;
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(458, 228);
            this.outputBox.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(24, 156);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(302, 22);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "Number of Addresses to Process: ";
            // 
            // numAddressesTextBox
            // 
            this.numAddressesTextBox.Location = new System.Drawing.Point(346, 156);
            this.numAddressesTextBox.Name = "numAddressesTextBox";
            this.numAddressesTextBox.ReadOnly = true;
            this.numAddressesTextBox.Size = new System.Drawing.Size(101, 22);
            this.numAddressesTextBox.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(24, 184);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(302, 22);
            this.textBox2.TabIndex = 6;
            this.textBox2.Text = "Number of Batches to Process: ";
            // 
            // numberOfBatchesBox
            // 
            this.numberOfBatchesBox.Location = new System.Drawing.Point(345, 183);
            this.numberOfBatchesBox.Name = "numberOfBatchesBox";
            this.numberOfBatchesBox.ReadOnly = true;
            this.numberOfBatchesBox.Size = new System.Drawing.Size(102, 22);
            this.numberOfBatchesBox.TabIndex = 7;
            // 
            // textbox3
            // 
            this.textbox3.Location = new System.Drawing.Point(24, 71);
            this.textbox3.Name = "textbox3";
            this.textbox3.ReadOnly = true;
            this.textbox3.Size = new System.Drawing.Size(301, 22);
            this.textbox3.TabIndex = 8;
            this.textbox3.Text = "Addresses Per Batch:";
            // 
            // numAddressesPerBatchTextBox
            // 
            this.numAddressesPerBatchTextBox.Location = new System.Drawing.Point(346, 71);
            this.numAddressesPerBatchTextBox.Name = "numAddressesPerBatchTextBox";
            this.numAddressesPerBatchTextBox.ReadOnly = true;
            this.numAddressesPerBatchTextBox.Size = new System.Drawing.Size(101, 22);
            this.numAddressesPerBatchTextBox.TabIndex = 9;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(477, 47);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(401, 228);
            this.textBox4.TabIndex = 10;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(487, 71);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(262, 22);
            this.textBox5.TabIndex = 11;
            this.textBox5.Text = "Addresses Processed:";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(487, 99);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(262, 22);
            this.textBox6.TabIndex = 12;
            this.textBox6.Text = "Batches Processed:";
            // 
            // addressesProcessedTextBox
            // 
            this.addressesProcessedTextBox.Location = new System.Drawing.Point(762, 71);
            this.addressesProcessedTextBox.Name = "addressesProcessedTextBox";
            this.addressesProcessedTextBox.ReadOnly = true;
            this.addressesProcessedTextBox.Size = new System.Drawing.Size(100, 22);
            this.addressesProcessedTextBox.TabIndex = 13;
            // 
            // batchesProcessedTextBox
            // 
            this.batchesProcessedTextBox.Location = new System.Drawing.Point(762, 99);
            this.batchesProcessedTextBox.Name = "batchesProcessedTextBox";
            this.batchesProcessedTextBox.ReadOnly = true;
            this.batchesProcessedTextBox.Size = new System.Drawing.Size(100, 22);
            this.batchesProcessedTextBox.TabIndex = 14;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(487, 127);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(262, 22);
            this.textBox7.TabIndex = 15;
            this.textBox7.Text = "Time Elapsed:";
            // 
            // timeElapsedTextBox
            // 
            this.timeElapsedTextBox.Location = new System.Drawing.Point(762, 126);
            this.timeElapsedTextBox.Name = "timeElapsedTextBox";
            this.timeElapsedTextBox.ReadOnly = true;
            this.timeElapsedTextBox.Size = new System.Drawing.Size(100, 22);
            this.timeElapsedTextBox.TabIndex = 16;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(487, 156);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(262, 22);
            this.textBox8.TabIndex = 17;
            this.textBox8.Text = "Addresses Per Second:";
            // 
            // addressesPerSecondTextBox
            // 
            this.addressesPerSecondTextBox.Location = new System.Drawing.Point(762, 156);
            this.addressesPerSecondTextBox.Name = "addressesPerSecondTextBox";
            this.addressesPerSecondTextBox.ReadOnly = true;
            this.addressesPerSecondTextBox.Size = new System.Drawing.Size(100, 22);
            this.addressesPerSecondTextBox.TabIndex = 18;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(487, 185);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(262, 22);
            this.textBox9.TabIndex = 19;
            this.textBox9.Text = "Minutes Remaining:";
            // 
            // timeRemainingTextBox
            // 
            this.timeRemainingTextBox.Location = new System.Drawing.Point(762, 184);
            this.timeRemainingTextBox.Name = "timeRemainingTextBox";
            this.timeRemainingTextBox.ReadOnly = true;
            this.timeRemainingTextBox.Size = new System.Drawing.Size(100, 22);
            this.timeRemainingTextBox.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "App.Config Settings:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = "Geocoding Info:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(487, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 17);
            this.label3.TabIndex = 23;
            this.label3.Text = "Runtime Info:";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(487, 214);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(262, 22);
            this.textBox10.TabIndex = 24;
            this.textBox10.Text = "Addresses Remaining:";
            // 
            // addressesRemainingTextBox
            // 
            this.addressesRemainingTextBox.Location = new System.Drawing.Point(762, 213);
            this.addressesRemainingTextBox.Name = "addressesRemainingTextBox";
            this.addressesRemainingTextBox.ReadOnly = true;
            this.addressesRemainingTextBox.Size = new System.Drawing.Size(100, 22);
            this.addressesRemainingTextBox.TabIndex = 25;
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(487, 243);
            this.textBox12.Name = "textBox12";
            this.textBox12.ReadOnly = true;
            this.textBox12.Size = new System.Drawing.Size(262, 22);
            this.textBox12.TabIndex = 26;
            this.textBox12.Text = "Batches Remaining:";
            // 
            // batchesRemainingTextBox
            // 
            this.batchesRemainingTextBox.Location = new System.Drawing.Point(762, 242);
            this.batchesRemainingTextBox.Name = "batchesRemainingTextBox";
            this.batchesRemainingTextBox.ReadOnly = true;
            this.batchesRemainingTextBox.Size = new System.Drawing.Size(100, 22);
            this.batchesRemainingTextBox.TabIndex = 27;
            // 
            // GeoCoderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 345);
            this.Controls.Add(this.batchesRemainingTextBox);
            this.Controls.Add(this.textBox12);
            this.Controls.Add(this.addressesRemainingTextBox);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timeRemainingTextBox);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.addressesPerSecondTextBox);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.timeElapsedTextBox);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.batchesProcessedTextBox);
            this.Controls.Add(this.addressesProcessedTextBox);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.numAddressesPerBatchTextBox);
            this.Controls.Add(this.textbox3);
            this.Controls.Add(this.numberOfBatchesBox);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.numAddressesTextBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.progressBar);
            this.Name = "GeoCoderForm";
            this.Text = "Restful Geocoder";
            this.Load += new System.EventHandler(this.GeoCoderForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.TextBox outputBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox numAddressesTextBox;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox numberOfBatchesBox;
        private System.Windows.Forms.TextBox textbox3;
        public System.Windows.Forms.TextBox numAddressesPerBatchTextBox;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox addressesProcessedTextBox;
        private System.Windows.Forms.TextBox batchesProcessedTextBox;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox timeElapsedTextBox;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox addressesPerSecondTextBox;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox timeRemainingTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox addressesRemainingTextBox;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox batchesRemainingTextBox;
    }
}

