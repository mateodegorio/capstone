namespace finaltesting
{
    partial class mainform
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
            this.label2 = new System.Windows.Forms.Label();
            this.enrollment = new System.Windows.Forms.Button();
            this.verification = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(588, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Automated Parking System";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(95, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(410, 37);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ateneo de Davao University";
            // 
            // enrollment
            // 
            this.enrollment.BackColor = System.Drawing.Color.Tomato;
            this.enrollment.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.enrollment.Font = new System.Drawing.Font("Modern No. 20", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enrollment.Location = new System.Drawing.Point(32, 30);
            this.enrollment.Name = "enrollment";
            this.enrollment.Size = new System.Drawing.Size(239, 72);
            this.enrollment.TabIndex = 2;
            this.enrollment.Text = "ENROLLMENT";
            this.enrollment.UseVisualStyleBackColor = false;
            this.enrollment.Click += new System.EventHandler(this.enrollment_Click);
            // 
            // verification
            // 
            this.verification.BackColor = System.Drawing.Color.Tomato;
            this.verification.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.verification.Font = new System.Drawing.Font("Modern No. 20", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verification.Location = new System.Drawing.Point(325, 30);
            this.verification.Name = "verification";
            this.verification.Size = new System.Drawing.Size(239, 72);
            this.verification.TabIndex = 3;
            this.verification.Text = "VERIFICATION";
            this.verification.UseVisualStyleBackColor = false;
            this.verification.Click += new System.EventHandler(this.verification_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.enrollment);
            this.groupBox1.Controls.Add(this.verification);
            this.groupBox1.Location = new System.Drawing.Point(17, 143);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(604, 143);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(638, 312);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "mainform";
            this.Text = "mainform";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button enrollment;
        private System.Windows.Forms.Button verification;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}