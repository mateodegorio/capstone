﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using System.IO;

using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace finaltesting
{
    public partial class enrollment : Form, DPFP.Capture.EventHandler
    {
        public DPFP.Capture.Capture Capturer;
        public static int regid;
        public static MySqlConnection con = new MySqlConnection("Server=localhost; Database=parkingthesis; Uid=root; Pwd = Fave0406;");

        public enrollment()
        {
            InitializeComponent();
        }

        protected virtual void Init()
        {
            try
            {
                Capturer = new DPFP.Capture.Capture();

                if (null != Capturer)
                    Capturer.EventHandler = this;
                else
                    SetPrompt("Can’t initiate capture operation!!");

            }
            catch
            {
                MessageBox.Show("Can’t initiate capture operation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected void Start()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StartCapture();
                    SetPrompt("Using the fingerprint reader, scan your fingerprint.");
                }
                catch
                {
                    SetPrompt("Can’t initiate capture!");
                }
            }
        }
        protected void Stop()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StopCapture();
                }
                catch
                {
                    MessageBox.Show("Capture Stop error");
                }
            }
        }

        #region EventHandler Members:

        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
            MakeReport("The fingerprint sample was captured.");
            SetPrompt("Scan the same fingerprint again.");
            Process(Sample);
        }
        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The finger was removed from the fingerprint reader.");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The finger was touched.");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The fingerprint reader was connected.");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The fingerprint reader was disconnected.");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
                MessageBox.Show("Good");
            else
                MessageBox.Show("Bad");
        }
        #endregion

        protected void SetStatus(string status)
        {
            this.Invoke((Action)(delegate ()
            {
                label6.Text = status;
            }));
        }

        public void SetPrompt(string prompt)
        {
            this.Invoke((Action)(delegate ()
            {
                StatusLine.Text = prompt;
            }));
        }

        public void DrawPicture(Bitmap bitmap)
        {
            this.Invoke((Action)(delegate ()
            {
                fingerprint.Image = new Bitmap(bitmap, fingerprint.Size);
            }));
        }

        protected virtual void Process(DPFP.Sample Sample)
        {
            DrawPicture(ConvertSampleToBitmap(Sample));
        }

        protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();
            Bitmap bitmap = null;
            Convertor.ConvertToPicture(Sample, ref bitmap);
            return bitmap;
        }

        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet features = new DPFP.FeatureSet();
            Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);
            if (feedback == DPFP.Capture.CaptureFeedback.Good)
                return features;
            else
                return null;
        }

        protected void MakeReport(string message)
        {
            this.Invoke((Action)(delegate ()
            {
                Prompt.AppendText(message + "\r\n");
            }));
        }

        private void enroll_Click(object sender, EventArgs e)
        {
            MySqlCommand comm = new MySqlCommand("SELECT * FROM parkingdetail;", con);
            con.Open();
            MySqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                regid = Convert.ToInt32(reader["regid"]);
            }
            con.Close();
            MySqlCommand cmd = new MySqlCommand("UPDATE parkingdetail SET fullname = '" + name.Text + "', licenseplate = '" + licenseplate.Text + "', employment = '" + status.Text + "', plateclass = '" + plateclass.Text + "', status = 'Not Parked' WHERE regid = " + regid + ";", con);

            con.Open(); cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show(regid.ToString());
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
            mainform main = new mainform();
            main.Show();
        }

        private void addreference_Click(object sender, EventArgs e)
        {
            Stop();
            refenroll reff = new refenroll();
            reff.OnTemplate += this.OnTemplate;
            reff.Show();
            this.Hide();
        }

        private void OnTemplate(DPFP.Template template)
        {
            this.Invoke((Action)(delegate ()
            {
                Template = template;
                //VerifyingButton.Enabled = SaveButton.Enabled  = (Template != null);
                if (Template != null)
                    MessageBox.Show("The Fingerprint template is ready for verification and saving", "Fingerprint Enrollment");
                else
                    MessageBox.Show("The fingerprint template is not valid. Repeat fingerprint enrollment.", "Fingerprint Enrollment");
            }));
        }

        private DPFP.Template Template;

        private void enrollment_Load(object sender, EventArgs e)
        {
            Init();
            Start();
        }

        private void enrollment_FormClosed(object sender, FormClosedEventArgs e)
        {
            Stop();
        }
    }
}
