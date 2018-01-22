namespace finaltesting
{
    partial class verification
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
			this.components = new System.ComponentModel.Container();
			this.fingerprint = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.licenseplate = new Emgu.CV.UI.ImageBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.Prompt = new System.Windows.Forms.TextBox();
			this.StatusLine = new System.Windows.Forms.TextBox();
			this.close = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.webcama = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.fingerprint)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.licenseplate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.webcama)).BeginInit();
			this.SuspendLayout();
			// 
			// fingerprint
			// 
			this.fingerprint.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.fingerprint.Location = new System.Drawing.Point(12, 44);
			this.fingerprint.Name = "fingerprint";
			this.fingerprint.Size = new System.Drawing.Size(268, 282);
			this.fingerprint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.fingerprint.TabIndex = 0;
			this.fingerprint.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(90, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "FINGERPRINT";
			// 
			// licenseplate
			// 
			this.licenseplate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.licenseplate.Location = new System.Drawing.Point(301, 512);
			this.licenseplate.Name = "licenseplate";
			this.licenseplate.Size = new System.Drawing.Size(257, 113);
			this.licenseplate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.licenseplate.TabIndex = 2;
			this.licenseplate.TabStop = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(378, 496);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(102, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "LICENSE PLATE";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(724, 496);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "RESULT";
			// 
			// Prompt
			// 
			this.Prompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Prompt.Location = new System.Drawing.Point(12, 364);
			this.Prompt.Multiline = true;
			this.Prompt.Name = "Prompt";
			this.Prompt.ReadOnly = true;
			this.Prompt.Size = new System.Drawing.Size(268, 221);
			this.Prompt.TabIndex = 5;
			// 
			// StatusLine
			// 
			this.StatusLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.StatusLine.Location = new System.Drawing.Point(12, 332);
			this.StatusLine.Name = "StatusLine";
			this.StatusLine.ReadOnly = true;
			this.StatusLine.Size = new System.Drawing.Size(268, 22);
			this.StatusLine.TabIndex = 6;
			// 
			// close
			// 
			this.close.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.close.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.close.Location = new System.Drawing.Point(577, 430);
			this.close.Name = "close";
			this.close.Size = new System.Drawing.Size(130, 44);
			this.close.TabIndex = 7;
			this.close.Text = "CLOSE";
			this.close.UseVisualStyleBackColor = true;
			this.close.Click += new System.EventHandler(this.close_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(318, 1);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(263, 40);
			this.label4.TabIndex = 8;
			this.label4.Text = "VERIFICATION";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(9, 593);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(38, 16);
			this.label5.TabIndex = 9;
			this.label5.Text = "FAR:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(596, 546);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(69, 39);
			this.label6.TabIndex = 10;
			this.label6.Text = "LP:";
			// 
			// webcama
			// 
			this.webcama.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.webcama.Location = new System.Drawing.Point(301, 44);
			this.webcama.Name = "webcama";
			this.webcama.Size = new System.Drawing.Size(640, 380);
			this.webcama.TabIndex = 12;
			this.webcama.TabStop = false;
			// 
			// verification
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(956, 637);
			this.Controls.Add(this.webcama);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.close);
			this.Controls.Add(this.StatusLine);
			this.Controls.Add(this.Prompt);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.licenseplate);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.fingerprint);
			this.Name = "verification";
			this.Text = "verification";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.verification_FormClosed);
			this.Load += new System.EventHandler(this.verification_Load);
			((System.ComponentModel.ISupportInitialize)(this.fingerprint)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.licenseplate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.webcama)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox fingerprint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox StatusLine;
        private System.Windows.Forms.Button close;
        public System.Windows.Forms.TextBox Prompt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public Emgu.CV.UI.ImageBox licenseplate;
        private System.Windows.Forms.Label label6;
		public System.Windows.Forms.PictureBox webcama;
	}
}

