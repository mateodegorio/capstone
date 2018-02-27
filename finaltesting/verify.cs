using System;
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

using AForge.Video;
using AForge.Video.DirectShow;

using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

using Tesseract;

namespace finaltesting
{
    public partial class verify : verification
    {
        public static MySqlConnection con = new MySqlConnection("Server=localhost; Database=parkingthesis; Uid=root; Pwd = Fave0406;");

        private FilterInfoCollection webcam;
        public VideoCaptureDevice cam;
        public string star = "";

        public void Verify(DPFP.Template template)
        {
            Template = template;
            ShowDialog();

			if (check == true)
			{
				cam.Stop();
				check = false;
			}
		}

        protected override void Init()
        {
            base.Init();
            base.Text = "Finger verification";
            Verificator = new DPFP.Verification.Verification();
            UpdateStatus(0);

			webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            cam = new VideoCaptureDevice(webcam[0].MonikerString);
            cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);
            cam.Start();

		}

        void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bit = (Bitmap)eventArgs.Frame.Clone();
			webcama.Image = bit;
		}

        protected override void Process(DPFP.Sample Sample)
        {
            if (who == 1)
            {
                con.Open();
                MySqlCommand comm = new MySqlCommand("Select * from parkingdetail", con);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                con.Close();
                DataTable result_ = dt;

                foreach (DataRow dr in result_.Rows)
                {

                    byte[] _img_ = (byte[])dr["fingerprint"];
                    MemoryStream ms = new MemoryStream(_img_);

                    DPFP.Template Template = new DPFP.Template();

                    Template.DeSerialize(ms);

                    DPFP.Verification.Verification Verificator = new DPFP.Verification.Verification();

                    base.Process(Sample);

                    DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);
                    if (features != null)
                    {
                        DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();
                        Verificator.Verify(features, Template, ref result);
                        UpdateStatus(result.FARAchieved);
                        if (result.Verified)
                        {
                            int trylang = (int)dr["regid"];
                            string name = (string)dr["fullname"];
                            string licplt = (string)dr["licenseplate"];
                            string stat = (string)dr["employment"];
                            string wat = (string)dr["status"];
                            string clas = (string)dr["plateclass"];

                            #region LICENSE PLATE RECOGNITION CODE

                            passing();
                            compute();

                            FileStream Fs = new FileStream(Application.StartupPath + @"\1d.jpg", FileMode.Open, FileAccess.Read);
                            Image tmp = Image.FromStream(Fs);
                            Fs.Close();
                            Bitmap pictkn = new Bitmap(tmp);

                            Mat cropped = new Mat();
                            Image<Bgr, byte> origni = new Image<Bgr, byte>(pictkn);

                            Mat ORIG = origni.Mat;
                            Image<Gray, byte> wow = new Image<Gray, byte>(pictkn);

                            Mat otsus = new Mat();
                            CvInvoke.Threshold(wow, otsus, 0, 255, ThresholdType.Otsu);

                            List<RotatedRect> boxList = new List<RotatedRect>();
                            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
                            {
                                CvInvoke.FindContours(otsus, contours, null, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);
                                int count = contours.Size;

                                for (int i = 0; i < count; i++)
                                {
                                    using (VectorOfPoint contour = contours[i]) using (VectorOfPoint approxContour = new VectorOfPoint())
                                    {
                                        CvInvoke.ApproxPolyDP(contour, approxContour, 3, true);
                                        Rectangle rectangle = CvInvoke.BoundingRectangle(approxContour);
                                        float h = rectangle.Size.Height;
                                        float w = rectangle.Size.Width; float wid, hei, area;
                                        area = w * h;
                                        wid = w / h;
                                        hei = h / w;
                                        if ((hei > 0.2 && hei < 0.6) && (wid > 1.2 && wid < 3.0) && area > 3000)
                                        {
                                            boxList.Add(CvInvoke.MinAreaRect(approxContour));
                                            cropped = new Mat(ORIG, rectangle);
                                            cropped.Save("trynato.jpg");
                                        }
                                    }
                                }
                            }

                            licenseplate.Image = cropped;

                            con.Open();
                            MySqlCommand cmd = new MySqlCommand("SELECT logdet FROM logbook WHERE username = '" + name + "';", con);
                            MySqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                star = (string)reader["logdet"];
                            }
                            con.Close();

                            #endregion

                            if (stat == "STUDENT")
                            {
                                if(chckS == 0)
                                {
                                    if(star == "IN")
                                    {
                                        if (clas == "NEW")
                                        {
                                            Mat graychar = new Mat();
                                            CvInvoke.CvtColor(cropped, graychar, ColorConversion.Bgr2Gray);
                                            Mat adapt = new Mat();
                                            CvInvoke.AdaptiveThreshold(graychar, adapt, 255, AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 35, 9);
                                            Mat structElem = new Mat();
                                            structElem = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
                                            Mat dilation = new Mat();
                                            CvInvoke.MorphologyEx(adapt, dilation, MorphOp.Dilate, structElem, new Point(-1, 1), 1, BorderType.Default, new MCvScalar());
                                            Mat close = new Mat();
                                            CvInvoke.MorphologyEx(dilation, close, MorphOp.Close, structElem, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                                            Mat closeotsu = new Mat();
                                            CvInvoke.Threshold(close, closeotsu, 0, 255, ThresholdType.Otsu);
                                            Mat cerode = new Mat();
                                            CvInvoke.MorphologyEx(closeotsu, cerode, MorphOp.Erode, structElem, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());

                                            cerode.Save("nn.jpg");
                                            FileStream fs = new FileStream(Application.StartupPath + @"\nn.jpg", FileMode.Open, FileAccess.Read);
                                            Image tempe = Image.FromStream(fs);
                                            fs.Close();
                                            Bitmap imgnew = new Bitmap(tempe);

                                            var img = new Bitmap(imgnew);
                                            var ocr = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
                                            var pagenew = ocr.Process(img);

                                            int count = 0;

                                            string newplt = pagenew.GetText();
                                            char[] checknew = newplt.ToCharArray();
                                            char[] dbnewplt = licplt.ToCharArray();

                                            foreach (char i in checknew)
                                            {
                                                foreach (char c in dbnewplt)
                                                {
                                                    if (i == c)
                                                    {
                                                        count++;
                                                        break;
                                                    }
                                                }
                                            }

                                            if (count < 3)
                                            {

                                                try
                                                {


                                                    if (star == "IN")
                                                    {

                                                        MySqlCommand command = new MySqlCommand("INSERT INTO logbook(username, logdate, usertype, logdet) VALUES('" + name + "', now(), '" + stat + "', 'OUT'); UPDATE parkingdetail SET parkingdetail.status = 'Not Parked' WHERE fullname = '" + name + "';", con);

                                                        con.Open(); command.ExecuteNonQuery();
                                                        con.Close();
                                                    }
                                                    else
                                                    {
                                                        MySqlCommand cmnd = new MySqlCommand("INSERT INTO logbook(username, logdate, usertype, logdet) VALUES('" + name + "', now(), '" + stat + "', 'IN'); UPDATE parkingdetail SET parkingdetail.status = 'Parked' WHERE fullname = '" + name + "';", con);

                                                        con.Open(); cmnd.ExecuteNonQuery();
                                                        con.Close();
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show(ex.Message);
                                                }


                                                logboook();
                                                SetLP(licplt);

                                            }
                                            else
                                            {
                                                MessageBox.Show("SORRY THE PLATE DOES NOT MATCH!");
                                                break;
                                            }
                                        }

                                        if (clas == "OLD")
                                        {
                                            Mat grayplate = new Mat();
                                            CvInvoke.CvtColor(cropped, grayplate, ColorConversion.Bgr2Gray);

                                            Mat otsu = new Mat();
                                            CvInvoke.Threshold(grayplate, otsu, 0, 255, ThresholdType.Otsu);

                                            Mat canny = new Mat();
                                            CvInvoke.Canny(otsu, canny, 100, 50, 3, false);

                                            otsu.Save("oo.jpg");
                                            FileStream FS = new FileStream(Application.StartupPath + @"\oo.jpg", FileMode.Open, FileAccess.Read);
                                            Image temp = Image.FromStream(FS);
                                            FS.Close();
                                            Bitmap imgold = new Bitmap(temp);

                                            var img = new Bitmap(imgold);
                                            var ocr = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
                                            var pageold = ocr.Process(img);

                                            int count = 0;

                                            string oldplt = pageold.GetText();
                                            char[] checkold = oldplt.ToCharArray();
                                            char[] dboldplt = licplt.ToCharArray();

                                            foreach (char i in checkold)
                                            {
                                                foreach (char c in dboldplt)
                                                {
                                                    if (i == c)
                                                    {
                                                        count++;
                                                        break;
                                                    }
                                                }
                                            }
                                            if (count > 3)
                                            {
                                                try
                                                {

                                                    if (star == "IN")
                                                    {

                                                        MySqlCommand command = new MySqlCommand("INSERT INTO logbook(username, logdate, usertype, logdet) VALUES('" + name + "', now(), '" + stat + "', 'OUT');", con);

                                                        con.Open(); command.ExecuteNonQuery();
                                                        con.Close();
                                                    }
                                                    else
                                                    {
                                                        MySqlCommand cmnd = new MySqlCommand("INSERT INTO logbook(username, logdate, usertype, logdet) VALUES('" + name + "', now(), '" + stat + "', 'IN');", con);

                                                        con.Open(); cmnd.ExecuteNonQuery();
                                                        con.Close();
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show(ex.Message);
                                                }


                                                logboook();
                                                SetLP(licplt);
                                            }
                                            else
                                            {
                                                MessageBox.Show("SORRY THE PLATE DOES NOT MATCH!");
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Sorry but the parking space for students is full!");
                                    }
                                }
                                else
                                {

                                    if (clas == "NEW")
                                    {
                                        Mat graychar = new Mat();
                                        CvInvoke.CvtColor(cropped, graychar, ColorConversion.Bgr2Gray);
                                        Mat adapt = new Mat();
                                        CvInvoke.AdaptiveThreshold(graychar, adapt, 255, AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 35, 9);
                                        Mat structElem = new Mat();
                                        structElem = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
                                        Mat dilation = new Mat();
                                        CvInvoke.MorphologyEx(adapt, dilation, MorphOp.Dilate, structElem, new Point(-1, 1), 1, BorderType.Default, new MCvScalar());
                                        Mat close = new Mat();
                                        CvInvoke.MorphologyEx(dilation, close, MorphOp.Close, structElem, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                                        Mat closeotsu = new Mat();
                                        CvInvoke.Threshold(close, closeotsu, 0, 255, ThresholdType.Otsu);
                                        Mat cerode = new Mat();
                                        CvInvoke.MorphologyEx(closeotsu, cerode, MorphOp.Erode, structElem, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());

                                        cerode.Save("nn.jpg");
                                        FileStream fs = new FileStream(Application.StartupPath + @"\nn.jpg", FileMode.Open, FileAccess.Read);
                                        Image tempe = Image.FromStream(fs);
                                        fs.Close();
                                        Bitmap imgnew = new Bitmap(tempe);

                                        var img = new Bitmap(imgnew);
                                        var ocr = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
                                        var pagenew = ocr.Process(img);

                                        int count = 0;

                                        string newplt = pagenew.GetText();
                                        char[] checknew = newplt.ToCharArray();
                                        char[] dbnewplt = licplt.ToCharArray();

                                        foreach (char i in checknew)
                                        {
                                            foreach (char c in dbnewplt)
                                            {
                                                if (i == c)
                                                {
                                                    count++;
                                                    break;
                                                }
                                            }
                                        }

                                        if (count < 3)
                                        {

                                            try
                                            {

                                                if (star == "IN")
                                                {

                                                    MySqlCommand command = new MySqlCommand("INSERT INTO logbook(username, logdate, usertype, logdet) VALUES('" + name + "', now(), '" + stat + "', 'OUT'); UPDATE parkingdetail SET parkingdetail.status = 'Not Parked' WHERE fullname = '" + name + "';", con);

                                                    con.Open(); command.ExecuteNonQuery();
                                                    con.Close();
                                                }
                                                else
                                                {
                                                    MySqlCommand cmnd = new MySqlCommand("INSERT INTO logbook(username, logdate, usertype, logdet) VALUES('" + name + "', now(), '" + stat + "', 'IN'); UPDATE parkingdetail SET parkingdetail.status = 'Parked' WHERE fullname = '" + name + "';", con);

                                                    con.Open(); cmnd.ExecuteNonQuery();
                                                    con.Close();
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.Message);
                                            }


                                            logboook();
                                            SetLP(licplt);

                                        }
                                        else
                                        {
                                            MessageBox.Show("SORRY THE PLATE DOES NOT MATCH!");
                                            break;
                                        }
                                    }

                                    if (clas == "OLD")
                                    {
                                        Mat grayplate = new Mat();
                                        CvInvoke.CvtColor(cropped, grayplate, ColorConversion.Bgr2Gray);

                                        Mat otsu = new Mat();
                                        CvInvoke.Threshold(grayplate, otsu, 0, 255, ThresholdType.Otsu);

                                        Mat canny = new Mat();
                                        CvInvoke.Canny(otsu, canny, 100, 50, 3, false);

                                        otsu.Save("oo.jpg");
                                        FileStream FS = new FileStream(Application.StartupPath + @"\oo.jpg", FileMode.Open, FileAccess.Read);
                                        Image temp = Image.FromStream(FS);
                                        FS.Close();
                                        Bitmap imgold = new Bitmap(temp);

                                        var img = new Bitmap(imgold);
                                        var ocr = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
                                        var pageold = ocr.Process(img);

                                        int count = 0;

                                        string oldplt = pageold.GetText();
                                        char[] checkold = oldplt.ToCharArray();
                                        char[] dboldplt = licplt.ToCharArray();

                                        foreach (char i in checkold)
                                        {
                                            foreach (char c in dboldplt)
                                            {
                                                if (i == c)
                                                {
                                                    count++;
                                                    break;
                                                }
                                            }
                                        }
                                        if (count > 3)
                                        {
                                            try
                                            {

                                                if (star == "IN")
                                                {

                                                    MySqlCommand command = new MySqlCommand("INSERT INTO logbook(username, logdate, usertype, logdet) VALUES('" + name + "', now(), '" + stat + "', 'OUT');", con);

                                                    con.Open(); command.ExecuteNonQuery();
                                                    con.Close();
                                                }
                                                else
                                                {
                                                    MySqlCommand cmnd = new MySqlCommand("INSERT INTO logbook(username, logdate, usertype, logdet) VALUES('" + name + "', now(), '" + stat + "', 'IN');", con);

                                                    con.Open(); cmnd.ExecuteNonQuery();
                                                    con.Close();
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.Message);
                                            }


                                            logboook();
                                            SetLP(licplt);
                                        }
                                        else
                                        {
                                            MessageBox.Show("SORRY THE PLATE DOES NOT MATCH!");
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if(chckE == 0)
                                {
                                    if(star == "IN")
                                    {
                                        if (clas == "NEW")
                                        {
                                            Mat graychar = new Mat();
                                            CvInvoke.CvtColor(cropped, graychar, ColorConversion.Bgr2Gray);
                                            Mat adapt = new Mat();
                                            CvInvoke.AdaptiveThreshold(graychar, adapt, 255, AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 35, 9);
                                            Mat structElem = new Mat();
                                            structElem = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
                                            Mat dilation = new Mat();
                                            CvInvoke.MorphologyEx(adapt, dilation, MorphOp.Dilate, structElem, new Point(-1, 1), 1, BorderType.Default, new MCvScalar());
                                            Mat close = new Mat();
                                            CvInvoke.MorphologyEx(dilation, close, MorphOp.Close, structElem, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                                            Mat closeotsu = new Mat();
                                            CvInvoke.Threshold(close, closeotsu, 0, 255, ThresholdType.Otsu);
                                            Mat cerode = new Mat();
                                            CvInvoke.MorphologyEx(closeotsu, cerode, MorphOp.Erode, structElem, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());

                                            cerode.Save("nn.jpg");
                                            FileStream fs = new FileStream(Application.StartupPath + @"\nn.jpg", FileMode.Open, FileAccess.Read);
                                            Image tempe = Image.FromStream(fs);
                                            fs.Close();
                                            Bitmap imgnew = new Bitmap(tempe);

                                            var img = new Bitmap(imgnew);
                                            var ocr = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
                                            var pagenew = ocr.Process(img);

                                            int count = 0;

                                            string newplt = pagenew.GetText();
                                            char[] checknew = newplt.ToCharArray();
                                            char[] dbnewplt = licplt.ToCharArray();

                                            foreach (char i in checknew)
                                            {
                                                foreach (char c in dbnewplt)
                                                {
                                                    if (i == c)
                                                    {
                                                        count++;
                                                        break;
                                                    }
                                                }
                                            }

                                            if (count < 3)
                                            {

                                                try
                                                {

                                                    if (star == "IN")
                                                    {

                                                        MySqlCommand command = new MySqlCommand("INSERT INTO logbook(username, logdate, usertype, logdet) VALUES('" + name + "', now(), '" + stat + "', 'OUT'); UPDATE parkingdetail SET parkingdetail.status = 'Not Parked' WHERE fullname = '" + name + "';", con);

                                                        con.Open(); command.ExecuteNonQuery();
                                                        con.Close();
                                                    }
                                                    else
                                                    {
                                                        MySqlCommand cmnd = new MySqlCommand("INSERT INTO logbook(username, logdate, usertype, logdet) VALUES('" + name + "', now(), '" + stat + "', 'IN'); UPDATE parkingdetail SET parkingdetail.status = 'Parked' WHERE fullname = '" + name + "';", con);

                                                        con.Open(); cmnd.ExecuteNonQuery();
                                                        con.Close();
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show(ex.Message);
                                                }


                                                logboook();
                                                SetLP(licplt);

                                            }
                                            else
                                            {
                                                MessageBox.Show("SORRY THE PLATE DOES NOT MATCH!");
                                                break;
                                            }
                                        }

                                        if (clas == "OLD")
                                        {
                                            Mat grayplate = new Mat();
                                            CvInvoke.CvtColor(cropped, grayplate, ColorConversion.Bgr2Gray);

                                            Mat otsu = new Mat();
                                            CvInvoke.Threshold(grayplate, otsu, 0, 255, ThresholdType.Otsu);

                                            Mat canny = new Mat();
                                            CvInvoke.Canny(otsu, canny, 100, 50, 3, false);

                                            otsu.Save("oo.jpg");
                                            FileStream FS = new FileStream(Application.StartupPath + @"\oo.jpg", FileMode.Open, FileAccess.Read);
                                            Image temp = Image.FromStream(FS);
                                            FS.Close();
                                            Bitmap imgold = new Bitmap(temp);

                                            var img = new Bitmap(imgold);
                                            var ocr = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
                                            var pageold = ocr.Process(img);

                                            int count = 0;

                                            string oldplt = pageold.GetText();
                                            char[] checkold = oldplt.ToCharArray();
                                            char[] dboldplt = licplt.ToCharArray();

                                            foreach (char i in checkold)
                                            {
                                                foreach (char c in dboldplt)
                                                {
                                                    if (i == c)
                                                    {
                                                        count++;
                                                        break;
                                                    }
                                                }
                                            }
                                            if (count > 3)
                                            {
                                                try
                                                {

                                                    if (star == "IN")
                                                    {

                                                        MySqlCommand command = new MySqlCommand("INSERT INTO logbook(username, logdate, usertype, logdet) VALUES('" + name + "', now(), '" + stat + "', 'OUT');", con);

                                                        con.Open(); command.ExecuteNonQuery();
                                                        con.Close();
                                                    }
                                                    else
                                                    {
                                                        MySqlCommand cmnd = new MySqlCommand("INSERT INTO logbook(username, logdate, usertype, logdet) VALUES('" + name + "', now(), '" + stat + "', 'IN');", con);

                                                        con.Open(); cmnd.ExecuteNonQuery();
                                                        con.Close();
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show(ex.Message);
                                                }


                                                logboook();
                                                SetLP(licplt);
                                            }
                                            else
                                            {
                                                MessageBox.Show("SORRY THE PLATE DOES NOT MATCH!");
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Sorry but the parking space for employees is full!");
                                    }
                                }
                                else
                                {
                                    if (clas == "NEW")
                                    {
                                        Mat graychar = new Mat();
                                        CvInvoke.CvtColor(cropped, graychar, ColorConversion.Bgr2Gray);
                                        Mat adapt = new Mat();
                                        CvInvoke.AdaptiveThreshold(graychar, adapt, 255, AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 35, 9);
                                        Mat structElem = new Mat();
                                        structElem = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
                                        Mat dilation = new Mat();
                                        CvInvoke.MorphologyEx(adapt, dilation, MorphOp.Dilate, structElem, new Point(-1, 1), 1, BorderType.Default, new MCvScalar());
                                        Mat close = new Mat();
                                        CvInvoke.MorphologyEx(dilation, close, MorphOp.Close, structElem, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                                        Mat closeotsu = new Mat();
                                        CvInvoke.Threshold(close, closeotsu, 0, 255, ThresholdType.Otsu);
                                        Mat cerode = new Mat();
                                        CvInvoke.MorphologyEx(closeotsu, cerode, MorphOp.Erode, structElem, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());

                                        cerode.Save("nn.jpg");
                                        FileStream fs = new FileStream(Application.StartupPath + @"\nn.jpg", FileMode.Open, FileAccess.Read);
                                        Image tempe = Image.FromStream(fs);
                                        fs.Close();
                                        Bitmap imgnew = new Bitmap(tempe);

                                        var img = new Bitmap(imgnew);
                                        var ocr = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
                                        var pagenew = ocr.Process(img);

                                        int count = 0;

                                        string newplt = pagenew.GetText();
                                        char[] checknew = newplt.ToCharArray();
                                        char[] dbnewplt = licplt.ToCharArray();

                                        foreach (char i in checknew)
                                        {
                                            foreach (char c in dbnewplt)
                                            {
                                                if (i == c)
                                                {
                                                    count++;
                                                    break;
                                                }
                                            }
                                        }

                                        if (count < 3)
                                        {

                                            try
                                            {

                                                if (star == "IN")
                                                {

                                                    MySqlCommand command = new MySqlCommand("INSERT INTO logbook(username, logdate, usertype, logdet) VALUES('" + name + "', now(), '" + stat + "', 'OUT'); UPDATE parkingdetail SET parkingdetail.status = 'Not Parked' WHERE fullname = '" + name + "';", con);

                                                    con.Open(); command.ExecuteNonQuery();
                                                    con.Close();
                                                }
                                                else
                                                {
                                                    MySqlCommand cmnd = new MySqlCommand("INSERT INTO logbook(username, logdate, usertype, logdet) VALUES('" + name + "', now(), '" + stat + "', 'IN'); UPDATE parkingdetail SET parkingdetail.status = 'Parked' WHERE fullname = '" + name + "';", con);

                                                    con.Open(); cmnd.ExecuteNonQuery();
                                                    con.Close();
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.Message);
                                            }


                                            logboook();
                                            SetLP(licplt);

                                        }
                                        else
                                        {
                                            MessageBox.Show("SORRY THE PLATE DOES NOT MATCH!");
                                            break;
                                        }
                                    }

                                    if (clas == "OLD")
                                    {
                                        Mat grayplate = new Mat();
                                        CvInvoke.CvtColor(cropped, grayplate, ColorConversion.Bgr2Gray);

                                        Mat otsu = new Mat();
                                        CvInvoke.Threshold(grayplate, otsu, 0, 255, ThresholdType.Otsu);

                                        Mat canny = new Mat();
                                        CvInvoke.Canny(otsu, canny, 100, 50, 3, false);

                                        otsu.Save("oo.jpg");
                                        FileStream FS = new FileStream(Application.StartupPath + @"\oo.jpg", FileMode.Open, FileAccess.Read);
                                        Image temp = Image.FromStream(FS);
                                        FS.Close();
                                        Bitmap imgold = new Bitmap(temp);

                                        var img = new Bitmap(imgold);
                                        var ocr = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
                                        var pageold = ocr.Process(img);

                                        int count = 0;

                                        string oldplt = pageold.GetText();
                                        char[] checkold = oldplt.ToCharArray();
                                        char[] dboldplt = licplt.ToCharArray();

                                        foreach (char i in checkold)
                                        {
                                            foreach (char c in dboldplt)
                                            {
                                                if (i == c)
                                                {
                                                    count++;
                                                    break;
                                                }
                                            }
                                        }
                                        if (count > 3)
                                        {
                                            try
                                            {

                                                if (star == "IN")
                                                {

                                                    MySqlCommand command = new MySqlCommand("INSERT INTO logbook(username, logdate, usertype, logdet) VALUES('" + name + "', now(), '" + stat + "', 'OUT');", con);

                                                    con.Open(); command.ExecuteNonQuery();
                                                    con.Close();
                                                }
                                                else
                                                {
                                                    MySqlCommand cmnd = new MySqlCommand("INSERT INTO logbook(username, logdate, usertype, logdet) VALUES('" + name + "', now(), '" + stat + "', 'IN');", con);

                                                    con.Open(); cmnd.ExecuteNonQuery();
                                                    con.Close();
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.Message);
                                            }


                                            logboook();
                                            SetLP(licplt);
                                        }
                                        else
                                        {
                                            MessageBox.Show("SORRY THE PLATE DOES NOT MATCH!");
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (who == 2)
            {
                con.Open();
                MySqlCommand comm = new MySqlCommand("SELECT  rf.referfinger as referfinger, rf.refid as refid, rf.refername as refername, pd.licenseplate as licenseplate, rf.employment as employment, rf.status as status, pd.plateclass as plateclass FROM parkingthesis.parkingdetail pd LEFT JOIN referral rf ON pd.regid = rf.regisid;", con);
                MySqlDataAdapter adp = new MySqlDataAdapter(comm);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                con.Close();
                DataTable result_ = dt;

                foreach (DataRow dr in result_.Rows)
                {

                    byte[] _imge_ = (byte[])dr["referfinger"];
                    MemoryStream ms = new MemoryStream(_imge_);

                    DPFP.Template Template = new DPFP.Template();

                    Template.DeSerialize(ms);

                    DPFP.Verification.Verification Verificator = new DPFP.Verification.Verification();

                    base.Process(Sample);

                    DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);
                    if (features != null)
                    {
                        DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();
                        Verificator.Verify(features, Template, ref result);
                        UpdateStatus(result.FARAchieved);
                        if (result.Verified)
                        {
                            int rtry = (int)dr["refid"];
                            string rnem = (string)dr["refername"];
                            string licplat = (string)dr["licenseplate"];
                            string rsat = (string)dr["employment"];
                            string rwho = (string)dr["status"];
                            string rclass = (string)dr["plateclass"];

                            #region LICENSE PLATE RECOGNITION CODE

                            passing();

                            FileStream Fs = new FileStream(Application.StartupPath + @"\1d.jpg", FileMode.Open, FileAccess.Read);
                            Image tmp = Image.FromStream(Fs);
                            Fs.Close();
                            Bitmap pictkn = new Bitmap(tmp);

                            Mat cropped = new Mat();
                            Image<Bgr, byte> origni = new Image<Bgr, byte>(pictkn);

                            Mat ORIG = origni.Mat;
                            Image<Gray, byte> wow = new Image<Gray, byte>(pictkn);

                            Mat otsus = new Mat();
                            CvInvoke.Threshold(wow, otsus, 0, 255, ThresholdType.Otsu);

                            List<RotatedRect> boxList = new List<RotatedRect>();
                            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
                            {
                                CvInvoke.FindContours(otsus, contours, null, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);
                                int count = contours.Size;

                                for (int i = 0; i < count; i++)
                                {
                                    using (VectorOfPoint contour = contours[i]) using (VectorOfPoint approxContour = new VectorOfPoint())
                                    {
                                        CvInvoke.ApproxPolyDP(contour, approxContour, 3, true);
                                        Rectangle rectangle = CvInvoke.BoundingRectangle(approxContour);
                                        float h = rectangle.Size.Height;
                                        float w = rectangle.Size.Width; float wid, hei, area;
                                        area = w * h;
                                        wid = w / h;
                                        hei = h / w;
                                        if ((hei > 0.2 && hei < 0.6) && (wid > 1.2 && wid < 3.0) && area > 3000)
                                        {
                                            boxList.Add(CvInvoke.MinAreaRect(approxContour));
                                            cropped = new Mat(ORIG, rectangle);
                                            cropped.Save("trynato.jpg");
                                        }
                                    }
                                }
                            }

                            licenseplate.Image = cropped;

                            #endregion

                            if (rclass == "NEW")
                            {
                                Mat graychar = new Mat();
                                CvInvoke.CvtColor(cropped, graychar, ColorConversion.Bgr2Gray);
                                Mat adapt = new Mat();
                                CvInvoke.AdaptiveThreshold(graychar, adapt, 255, AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 35, 9);
                                Mat structElem = new Mat();
                                structElem = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));
                                Mat dilation = new Mat();
                                CvInvoke.MorphologyEx(adapt, dilation, MorphOp.Dilate, structElem, new Point(-1, 1), 1, BorderType.Default, new MCvScalar());
                                Mat close = new Mat();
                                CvInvoke.MorphologyEx(dilation, close, MorphOp.Close, structElem, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                                Mat closeotsu = new Mat();
                                CvInvoke.Threshold(close, closeotsu, 0, 255, ThresholdType.Otsu);
                                Mat cerode = new Mat();
                                CvInvoke.MorphologyEx(closeotsu, cerode, MorphOp.Erode, structElem, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());

                                cerode.Save("WAT.jpg");
                                FileStream fs = new FileStream(Application.StartupPath + @"\WAT.jpg", FileMode.Open, FileAccess.Read);
                                Image tempe = Image.FromStream(fs);
                                fs.Close();
                                Bitmap imgnew = new Bitmap(tempe);

                                var img = new Bitmap(imgnew);
                                var ocr = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
                                var pagenew = ocr.Process(img);

                                int count = 0;

                                string newplt = pagenew.GetText();
                                char[] checknew = newplt.ToCharArray();
                                char[] dbnewplt = licplat.ToCharArray();

                                foreach (char i in checknew)
                                {
                                    foreach (char c in dbnewplt)
                                    {
                                        if (i == c)
                                        {
                                            count++;
                                            break;
                                        }
                                    }
                                }

                                if (count > 3)
                                {
                                    try
                                    {
                                        con.Open();
                                        MySqlCommand cmd = new MySqlCommand("SELECT * FROM logbook WHERE username = '" + rnem + "';", con);
                                        MySqlDataReader reader = cmd.ExecuteReader();

                                        while (reader.Read())
                                        {
                                            star = reader["logdet"].ToString();
                                        }
                                        con.Close();

                                        if (star == "IN")
                                        {

                                            MySqlCommand command = new MySqlCommand("INSERT INTO logbook(username, logdate, usertype, logdet) VALUES('" + rnem + "', now(), '" + rsat + "', 'OUT');", con);

                                            con.Open(); command.ExecuteNonQuery();
                                            con.Close();
                                        }
                                        else
                                        {
                                            MySqlCommand cmnd = new MySqlCommand("INSERT INTO logbook(username, logdate, usertype, logdet) VALUES('" + rnem + "', now(), '" + rsat + "', 'IN');", con);

                                            con.Open(); cmnd.ExecuteNonQuery();
                                            con.Close();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }


                                    logboook();
                                    SetLP(licplat);

                                }
                                else
                                {
                                    MessageBox.Show("SORRY THE PLATE DOES NOT MATCH!");
                                    break;
                                }
                            }

                            if (rclass == "OLD")
                            {
                                Mat grayplate = new Mat();
                                CvInvoke.CvtColor(cropped, grayplate, ColorConversion.Bgr2Gray);

                                Mat otsu = new Mat();
                                CvInvoke.Threshold(grayplate, otsu, 0, 255, ThresholdType.Otsu);

                                Mat canny = new Mat();
                                CvInvoke.Canny(otsu, canny, 100, 50, 3, false);

                                otsu.Save("oo.jpg");
                                FileStream FS = new FileStream(Application.StartupPath + @"\oo.jpg", FileMode.Open, FileAccess.Read);
                                Image temp = Image.FromStream(FS);
                                FS.Close();
                                Bitmap imgold = new Bitmap(temp);

                                var img = new Bitmap(imgold);
                                var ocr = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
                                var pageold = ocr.Process(img);

                                int count = 0;

                                string oldplt = pageold.GetText();
                                char[] checkold = oldplt.ToCharArray();
                                char[] dboldplt = licplat.ToCharArray();

                                foreach (char i in checkold)
                                {
                                    foreach (char c in dboldplt)
                                    {
                                        if (i == c)
                                        {
                                            count++;
                                            break;
                                        }
                                    }
                                }
                                if (count > 3)
                                {
                                    try
                                    {
                                        con.Open();
                                        MySqlCommand cmd = new MySqlCommand("SELECT * FROM logbook WHERE username = '" + rnem + "';", con);
                                        MySqlDataReader reader = cmd.ExecuteReader();

                                        while (reader.Read())
                                        {
                                            star = reader["logdet"].ToString();
                                        }
                                        con.Close();

                                        if (star == "IN")
                                        {

                                            MySqlCommand command = new MySqlCommand("INSERT INTO logbook(username, logdate, usertype, logdet) VALUES('" + rnem + "', now(), '" + rsat + "', 'OUT');", con);

                                            con.Open(); command.ExecuteNonQuery();
                                            con.Close();
                                        }
                                        else
                                        {
                                            MySqlCommand cmnd = new MySqlCommand("INSERT INTO logbook(username, logdate, usertype, logdet) VALUES('" + rnem + "', now(), '" + rsat + "', 'IN');", con);

                                            con.Open(); cmnd.ExecuteNonQuery();
                                            con.Close();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }


                                    logboook();
                                    SetLP(licplat);
                                }
                                else
                                {
                                    MessageBox.Show("SORRY THE PLATE DOES NOT MATCH!");
                                    break;
                                }
                            }
                        }
                    }
                }
            }

        }
        
        private void SetLP(string licenseplate) //to display extracted text from license plate
        {
            setLicPlate(String.Format("LP: {0}", licenseplate));
        }

        private void UpdateParkSpaceS(string parkSspace) //to display extracted text from license plate
        {
            parkingSspace(String.Format("Available Student Parking Space: {0}", parkSspace));
        }

        private void UpdateParkSpaceE(string parkEspace) //to display extracted text from license plate
        {
            parkingEspace(String.Format("Available Employee Parking Space: {0}", parkEspace));
        }

        private void UpdateStatus(int FAR) //to display False accept Rate of fingerprint scanning
        {
            SetStatus(String.Format("False Accept Rate(FAR) = {0}", FAR));

        }

        private DPFP.Template Template;
        private DPFP.Verification.Verification Verificator;
    }
}
