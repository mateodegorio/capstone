﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace finaltesting
{
    public partial class refenroll : reference
    {
        public delegate void OnTemplateEventHandler(DPFP.Template template);
        public event OnTemplateEventHandler OnTemplate;
        public static MySqlConnection con = new MySqlConnection("Server=localhost; Database=parkingthesis; Uid=root; Pwd = Fave0406;");

        protected override void Init()
        {
            base.Init();
            base.Text = "Fingerprint Enrollment";
            Enrollers = new DPFP.Processing.Enrollment();            // Create an enrollment.
            UpdateStatus();
        }

        protected override void Process(DPFP.Sample Sample)
        {
            base.Process(Sample);

            // Process the sample and create a feature set for the enrollment purpose.
            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Enrollment);
            DPFP.Capture.SampleConversion ToByte = new DPFP.Capture.SampleConversion();


            // Check quality of the sample and add to Enrollers if it's good
            if (features != null) try
                {
                    MakeReport("The fingerprint feature set was created.");
                    Enrollers.AddFeatures(features);
                }
                finally
                {
                    UpdateStatus();

                    // Check if template has been created.
                    switch (Enrollers.TemplateStatus)
                    {
                        case DPFP.Processing.Enrollment.Status.Ready:   // report success and stop capturing
                            {
                                OnTemplate(Enrollers.Template);
                                SetPrompt("Click Close, and then click Fingerprint Verification.");
                                Stop();
                                MemoryStream fingerprintData = new MemoryStream();
                                Enrollers.Template.Serialize(fingerprintData);
                                fingerprintData.Position = 0;
                                BinaryReader br = new BinaryReader(fingerprintData);
                                byte[] bytes = br.ReadBytes((Int32)fingerprintData.Length);

                                try
                                {
                                    MySqlCommand comm = new MySqlCommand("INSERT INTO referral(referfinger) VALUES(@img);", con);
                                    comm.Parameters.Add("@img", MySqlDbType.VarBinary).Value = bytes;

                                    con.Open(); comm.ExecuteNonQuery();
                                    con.Close();

                                }
                                catch (Exception e)
                                {
                                    MakeReport(e.Message);
                                }
                                break;
                            }

                        case DPFP.Processing.Enrollment.Status.Failed:  // report failure and restart capturing
                            {
                                Enrollers.Clear();
                                Stop();
                                UpdateStatus();
                                OnTemplate(null);
                                Start();
                                break;
                            }
                    }
                }
        }

        private void UpdateStatus()
        {
            // Show number of samples needed.
            SetStatus(String.Format("Show number of samples needed: {0}", Enrollers.FeaturesNeeded));
        }

        private DPFP.Processing.Enrollment Enrollers;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // enrolls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(911, 485);
            this.Name = "enrolls";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
