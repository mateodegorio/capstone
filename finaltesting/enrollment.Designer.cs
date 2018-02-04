namespace finaltesting
{
    partial class enrollment
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
            this.fingerprint = new System.Windows.Forms.PictureBox();
            this.StatusLine = new System.Windows.Forms.TextBox();
            this.licenseplate = new System.Windows.Forms.TextBox();
            this.name = new System.Windows.Forms.TextBox();
            this.Prompt = new System.Windows.Forms.TextBox();
            this.status = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.enroll = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.addreference = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.plateclass = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.fingerprint)).BeginInit();
            this.SuspendLayout();
            // 
            // fingerprint
            // 
            this.fingerprint.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.fingerprint.Location = new System.Drawing.Point(12, 55);
            this.fingerprint.Name = "fingerprint";
            this.fingerprint.Size = new System.Drawing.Size(260, 247);
            this.fingerprint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.fingerprint.TabIndex = 0;
            this.fingerprint.TabStop = false;
            // 
            // StatusLine
            // 
            this.StatusLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLine.Location = new System.Drawing.Point(12, 308);
            this.StatusLine.Name = "StatusLine";
            this.StatusLine.ReadOnly = true;
            this.StatusLine.Size = new System.Drawing.Size(260, 22);
            this.StatusLine.TabIndex = 1;
            // 
            // licenseplate
            // 
            this.licenseplate.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.licenseplate.Location = new System.Drawing.Point(282, 188);
            this.licenseplate.Name = "licenseplate";
            this.licenseplate.Size = new System.Drawing.Size(254, 62);
            this.licenseplate.TabIndex = 3;
            // 
            // name
            // 
            this.name.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.Location = new System.Drawing.Point(373, 61);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(387, 29);
            this.name.TabIndex = 4;
            // 
            // Prompt
            // 
            this.Prompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Prompt.Location = new System.Drawing.Point(12, 343);
            this.Prompt.Multiline = true;
            this.Prompt.Name = "Prompt";
            this.Prompt.ReadOnly = true;
            this.Prompt.Size = new System.Drawing.Size(260, 188);
            this.Prompt.TabIndex = 5;
            // 
            // status
            // 
            this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.FormattingEnabled = true;
            this.status.Items.AddRange(new object[] {
            "STUDENT",
            "EMPLOYEE"});
            this.status.Location = new System.Drawing.Point(432, 108);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(244, 32);
            this.status.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(278, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "NAME:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(278, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 24);
            this.label2.TabIndex = 9;
            this.label2.Text = "EMPLOYMENT:";
            // 
            // groupBox1
            // 
            this.groupBox1.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(293, 298);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(471, 200);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "REFERENCIAL:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(80, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "FINGERPRINT";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(278, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 24);
            this.label4.TabIndex = 12;
            this.label4.Text = "LICENSE PLATE";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(275, -1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(245, 40);
            this.label5.TabIndex = 13;
            this.label5.Text = "ENROLLMENT";
            // 
            // enroll
            // 
            this.enroll.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enroll.Location = new System.Drawing.Point(306, 504);
            this.enroll.Name = "enroll";
            this.enroll.Size = new System.Drawing.Size(179, 50);
            this.enroll.TabIndex = 14;
            this.enroll.Text = "ENROLL";
            this.enroll.UseVisualStyleBackColor = true;
            this.enroll.Click += new System.EventHandler(this.enroll_Click);
            // 
            // close
            // 
            this.close.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close.Location = new System.Drawing.Point(545, 504);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(179, 50);
            this.close.TabIndex = 15;
            this.close.Text = "CLOSE";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // addreference
            // 
            this.addreference.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addreference.Location = new System.Drawing.Point(293, 266);
            this.addreference.Name = "addreference";
            this.addreference.Size = new System.Drawing.Size(139, 26);
            this.addreference.TabIndex = 7;
            this.addreference.Text = "REFERENCE";
            this.addreference.UseVisualStyleBackColor = true;
            this.addreference.Click += new System.EventHandler(this.addreference_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 534);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "FAR:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(572, 161);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 24);
            this.label7.TabIndex = 17;
            this.label7.Text = "PLATE CLASS";
            // 
            // plateclass
            // 
            this.plateclass.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.plateclass.FormattingEnabled = true;
            this.plateclass.Items.AddRange(new object[] {
            "OLD",
            "NEW"});
            this.plateclass.Location = new System.Drawing.Point(576, 188);
            this.plateclass.Name = "plateclass";
            this.plateclass.Size = new System.Drawing.Size(182, 63);
            this.plateclass.TabIndex = 18;
            // 
            // enrollment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 567);
            this.Controls.Add(this.plateclass);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.addreference);
            this.Controls.Add(this.close);
            this.Controls.Add(this.enroll);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.status);
            this.Controls.Add(this.Prompt);
            this.Controls.Add(this.name);
            this.Controls.Add(this.licenseplate);
            this.Controls.Add(this.StatusLine);
            this.Controls.Add(this.fingerprint);
            this.Controls.Add(this.groupBox1);
            this.Name = "enrollment";
            this.Text = "enrollment";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.enrollment_FormClosed);
            this.Load += new System.EventHandler(this.enrollment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fingerprint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox fingerprint;
        private System.Windows.Forms.TextBox StatusLine;
        private System.Windows.Forms.TextBox licenseplate;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox Prompt;
        private System.Windows.Forms.ComboBox status;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button enroll;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Button addreference;
        private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox plateclass;
	}
}