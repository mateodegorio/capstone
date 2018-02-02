using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge.Video;
using AForge.Video.DirectShow;

using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

using MySql.Data.MySqlClient;
using System.IO;

namespace finaltesting
{
    public partial class verification : Form, DPFP.Capture.EventHandler
    {
        private DPFP.Capture.Capture Capturer;
		public bool check = false;

        public verification()
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
                label5.Text = status;
            }));
        }

        protected void setLicPlate(string licenseplate)
        {
            this.Invoke((Action)(delegate ()
            {
                label6.Text = licenseplate;
            }));
        }

        protected void parkingSspace(string parkSspace)
        {
            this.Invoke((Action)(delegate ()
            {
                label7.Text = parkSspace;
            }));
        }

        protected void parkingEspace(string parkEspace)
        {
            this.Invoke((Action)(delegate ()
            {
                label8.Text = parkEspace;
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

		protected void passing()
		{
			Invoke(new Action(() =>
			{
				using (Bitmap bmp = new Bitmap(webcama.ClientSize.Width,
												webcama.ClientSize.Height))
				{
					webcama.DrawToBitmap(bmp, webcama.ClientRectangle);
					bmp.Save("1d.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
				}
			}));
		}

        private void close_Click(object sender, EventArgs e) //to close form 
        {
            this.Close();
            mainform main = new mainform();
            main.Show();
			check = true;

		}

        private void verification_Load(object sender, EventArgs e)
        {
            Init();
            Start();
        }

        private void verification_FormClosed(object sender, FormClosedEventArgs e)
        {
            Stop();
        }
    }
}
