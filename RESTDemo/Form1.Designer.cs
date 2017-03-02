namespace RESTDemo
{
    partial class Form1
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
            this.GeocodeButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.StreetTextBox = new System.Windows.Forms.TextBox();
            this.ZoneTextBox = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // GeocodeButton
            // 
            this.GeocodeButton.Location = new System.Drawing.Point(24, 105);
            this.GeocodeButton.Name = "GeocodeButton";
            this.GeocodeButton.Size = new System.Drawing.Size(75, 23);
            this.GeocodeButton.TabIndex = 0;
            this.GeocodeButton.Text = "Geocode";
            this.GeocodeButton.UseVisualStyleBackColor = true;
            this.GeocodeButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Location:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Street:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Zone:";
            // 
            // StreetTextBox
            // 
            this.StreetTextBox.Location = new System.Drawing.Point(65, 18);
            this.StreetTextBox.Name = "StreetTextBox";
            this.StreetTextBox.Size = new System.Drawing.Size(167, 20);
            this.StreetTextBox.TabIndex = 5;
            this.StreetTextBox.Text = "1000 Elysian Park Avenue";
            // 
            // ZoneTextBox
            // 
            this.ZoneTextBox.Location = new System.Drawing.Point(65, 47);
            this.ZoneTextBox.Name = "ZoneTextBox";
            this.ZoneTextBox.Size = new System.Drawing.Size(167, 20);
            this.ZoneTextBox.TabIndex = 6;
            this.ZoneTextBox.Text = "90012";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Enabled = false;
            this.linkLabel1.Location = new System.Drawing.Point(88, 154);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(63, 13);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "[coordinate]";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 187);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.ZoneTextBox);
            this.Controls.Add(this.StreetTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GeocodeButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GeocodeButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox StreetTextBox;
        private System.Windows.Forms.TextBox ZoneTextBox;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

