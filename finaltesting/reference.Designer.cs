namespace finaltesting
{
    partial class reference
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
            this.label6 = new System.Windows.Forms.Label();
            this.enroll = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.ComboBox();
            this.Prompt = new System.Windows.Forms.TextBox();
            this.name = new System.Windows.Forms.TextBox();
            this.StatusLine = new System.Windows.Forms.TextBox();
            this.refingerprint = new System.Windows.Forms.PictureBox();
            this.relation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.refingerprint)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(354, 302);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 16);
            this.label6.TabIndex = 32;
            this.label6.Text = "FAR:";
            // 
            // enroll
            // 
            this.enroll.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enroll.Location = new System.Drawing.Point(280, 334);
            this.enroll.Name = "enroll";
            this.enroll.Size = new System.Drawing.Size(179, 50);
            this.enroll.TabIndex = 30;
            this.enroll.Text = "ADD";
            this.enroll.UseVisualStyleBackColor = true;
            this.enroll.Click += new System.EventHandler(this.enroll_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(262, -1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(214, 40);
            this.label5.TabIndex = 29;
            this.label5.Text = "REFERENCE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(80, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 16);
            this.label3.TabIndex = 27;
            this.label3.Text = "FINGERPRINT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(283, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 24);
            this.label2.TabIndex = 25;
            this.label2.Text = "STATUS:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(283, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 24);
            this.label1.TabIndex = 24;
            this.label1.Text = "NAME:";
            // 
            // status
            // 
            this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.FormattingEnabled = true;
            this.status.Items.AddRange(new object[] {
            "STUDENT",
            "EMPLOYEE"});
            this.status.Location = new System.Drawing.Point(398, 97);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(244, 32);
            this.status.TabIndex = 23;
            // 
            // Prompt
            // 
            this.Prompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Prompt.Location = new System.Drawing.Point(287, 198);
            this.Prompt.Multiline = true;
            this.Prompt.Name = "Prompt";
            this.Prompt.ReadOnly = true;
            this.Prompt.Size = new System.Drawing.Size(439, 101);
            this.Prompt.TabIndex = 21;
            // 
            // name
            // 
            this.name.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.Location = new System.Drawing.Point(398, 62);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(328, 29);
            this.name.TabIndex = 20;
            // 
            // StatusLine
            // 
            this.StatusLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLine.Location = new System.Drawing.Point(287, 170);
            this.StatusLine.Name = "StatusLine";
            this.StatusLine.ReadOnly = true;
            this.StatusLine.Size = new System.Drawing.Size(439, 22);
            this.StatusLine.TabIndex = 18;
            // 
            // refingerprint
            // 
            this.refingerprint.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.refingerprint.Location = new System.Drawing.Point(12, 52);
            this.refingerprint.Name = "refingerprint";
            this.refingerprint.Size = new System.Drawing.Size(260, 247);
            this.refingerprint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.refingerprint.TabIndex = 17;
            this.refingerprint.TabStop = false;
            // 
            // relation
            // 
            this.relation.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.relation.Location = new System.Drawing.Point(398, 135);
            this.relation.Name = "relation";
            this.relation.Size = new System.Drawing.Size(328, 29);
            this.relation.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(283, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 24);
            this.label4.TabIndex = 34;
            this.label4.Text = "RELATION:";
            // 
            // reference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 396);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.relation);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.enroll);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.status);
            this.Controls.Add(this.Prompt);
            this.Controls.Add(this.name);
            this.Controls.Add(this.StatusLine);
            this.Controls.Add(this.refingerprint);
            this.Name = "reference";
            this.Text = "reference";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.reference_FormClosed);
            this.Load += new System.EventHandler(this.reference_Load);
            ((System.ComponentModel.ISupportInitialize)(this.refingerprint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button enroll;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox status;
        private System.Windows.Forms.TextBox Prompt;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox StatusLine;
        private System.Windows.Forms.PictureBox refingerprint;
        private System.Windows.Forms.TextBox relation;
        private System.Windows.Forms.Label label4;
    }
}