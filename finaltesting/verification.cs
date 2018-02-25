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
        public static MySqlConnection con = new MySqlConnection("Server=localhost; Database=parkingthesis; Uid=root; Pwd = Fave0406;");

        private DPFP.Capture.Capture Capturer;
		public bool check = false;
        public int who = 0;
        public string date1, date2;

        public int parkstud = 2;
        public int parkemp = 2;

        public int inempnum = 0;
        public int instunum = 0;
        public int outempnum = 0;
        public int outstunum = 0;

        public int temp1, temp2, chckS, chckE;

        public verification()
        {
            InitializeComponent();

            DateTime today = DateTime.Now.Date;
            DateTime tomorrow = today.AddDays(1);
            date1 = today.ToString("yyyy/MM/dd");
            date2 = tomorrow.ToString("yyyy/MM/dd");
            display();
            compute();
        }
      
        private void display()
        {
            MySqlConnection con = new MySqlConnection("Server=localhost; Database=parkingthesis; Uid=root; Pwd = Fave0406;");

            string query = "select username as NAME,logdate as TIMESTAMP,usertype as EMPLOYMENT,logdet as STATUS from logbook WHERE logdate BETWEEN '" + date1 + "' AND '" + date2 + "' order by logid desc;";

            using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, con))
            {

                DataSet dset = new DataSet();

                adpt.Fill(dset);

                dataGridView1.DataSource = dset.Tables[0];

            }
            con.Close();
        }

        private void compute()
        {
            ///////////////////// GOING IN //////////////////////////////
            /////////////////////////////////////////////////////////////////// FOR GOING IN EMPLOYEES
            try
            {
                con.Open();
                MySqlCommand comm1 = new MySqlCommand("SELECT COUNT(username) FROM logbook WHERE logdet = 'IN' AND logdate BETWEEN '" + date1 + "' AND '" + date2 + "' AND usertype = 'EMPLOYEE' GROUP BY usertype;", con);
                inempnum = Convert.ToInt32(comm1.ExecuteScalar());
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            /////////////////////////////////////////////////////////////////// FOR GOING IN STUDENTS
            try
            {
                con.Open();
                MySqlCommand comm2 = new MySqlCommand("SELECT COUNT(username) FROM logbook WHERE logdet = 'IN' AND logdate BETWEEN '" + date1 + "' AND '" + date2 + "' AND usertype = 'STUDENT' GROUP BY usertype;", con);
                instunum = Convert.ToInt32(comm2.ExecuteScalar());
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



            //////////////////// GOING OUT //////////////////////////////
            /////////////////////////////////////////////////////////////////// FOR GOING OUT EMPLOYEES
            try
            {
                con.Open();
                MySqlCommand comm3 = new MySqlCommand("SELECT COUNT(username) FROM logbook WHERE logdet = 'OUT' AND logdate BETWEEN '" + date1 + "' AND '" + date2 + "' AND usertype = 'EMPLOYEE' GROUP BY usertype;", con);
                outempnum = Convert.ToInt32(comm3.ExecuteScalar());
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            /////////////////////////////////////////////////////////////////// FOR GOING OUT STUDENTS
            try
            {
                con.Open();
                MySqlCommand comm4 = new MySqlCommand("SELECT COUNT(username) FROM logbook WHERE logdet = 'OUT' AND logdate BETWEEN '" + date1 + "' AND '" + date2 + "' AND usertype = 'STUDENT' GROUP BY usertype;", con);
                outstunum = Convert.ToInt32(comm4.ExecuteScalar());
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            int temp1 = instunum - outstunum;
            int temp2 = inempnum - outempnum;

            int display1 = parkstud - temp1;
            int display2 = parkemp - temp2;

            textBox1.Text = Convert.ToString(display1);
            textBox2.Text = Convert.ToString(display2);
            setval(display1, display2);
        }

        protected void setval(int a, int b)
        {
            chckS = a;
            chckE = b;
        }

        protected void logboook()
        {

            Invoke(new Action(() =>
            {

                display();



                             ///////////////////// GOING IN //////////////////////////////
                /////////////////////////////////////////////////////////////////// FOR GOING IN EMPLOYEES
                try
                {
                    con.Open();
                    MySqlCommand comm1 = new MySqlCommand("SELECT COUNT(username) FROM logbook WHERE logdet = 'IN' AND logdate BETWEEN '" + date1 + "' AND '" + date2 + "' AND usertype = 'EMPLOYEE' GROUP BY usertype;", con);
                    inempnum = Convert.ToInt32(comm1.ExecuteScalar());
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                /////////////////////////////////////////////////////////////////// FOR GOING IN STUDENTS
                try
                {
                    con.Open();
                    MySqlCommand comm2 = new MySqlCommand("SELECT COUNT(username) FROM logbook WHERE logdet = 'IN' AND logdate BETWEEN '" + date1 + "' AND '" + date2 + "' AND usertype = 'STUDENT' GROUP BY usertype;", con);
                    instunum = Convert.ToInt32(comm2.ExecuteScalar());
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                


                            //////////////////// GOING OUT //////////////////////////////
                /////////////////////////////////////////////////////////////////// FOR GOING OUT EMPLOYEES
                try
                {
                    con.Open();
                    MySqlCommand comm3 = new MySqlCommand("SELECT COUNT(username) FROM logbook WHERE logdet = 'OUT' AND logdate BETWEEN '" + date1 + "' AND '" + date2 + "' AND usertype = 'EMPLOYEE' GROUP BY usertype;", con);
                    outempnum = Convert.ToInt32(comm3.ExecuteScalar());
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                /////////////////////////////////////////////////////////////////// FOR GOING OUT STUDENTS
                try
                {
                    con.Open();
                    MySqlCommand comm4 = new MySqlCommand("SELECT COUNT(username) FROM logbook WHERE logdet = 'OUT' AND logdate BETWEEN '" + date1 + "' AND '" + date2 + "' AND usertype = 'STUDENT' GROUP BY usertype;", con);
                    outstunum = Convert.ToInt32(comm4.ExecuteScalar());
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                int temp1 = instunum - outstunum;
                int temp2 = inempnum - outempnum;

                int display1 = parkstud - temp1;
                int display2 = parkemp - temp2;

                textBox1.Text = Convert.ToString(display1);
                textBox2.Text = Convert.ToString(display2);


            }));
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

        private void button2_Click(object sender, EventArgs e)
        {
            who = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            who = 2;
        }

    }
}
