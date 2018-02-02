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

        public int parkstud = 200;
        public int parkemp = 100;

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

        protected override void Process(DPFP.Sample Sample) //Processing and comparing of scanned fingerprint and stored fingerprint
        {
            con.Open();
            MySqlCommand comm = new MySqlCommand("Select * from parkingdetail", con);
            MySqlDataAdapter adp = new MySqlDataAdapter(comm);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();
            DataTable result_ = dt;// database handler class for CRUD operations

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

							foreach (char i in checknew) {
								foreach (char c in dbnewplt) {
									if (i == c) {
										count++;
										break;
									}
								}
							}
							if (count > 3)
							{

								SetLP(licplt);
                                if(wat == "Not Parked") {
                                    MySqlCommand cmd = new MySqlCommand("UPDATE parkingdetail SET status = 'Parked' WHERE regid = " + trylang + ";", con);

                                    con.Open(); cmd.ExecuteNonQuery();
                                    con.Close();

                                    parkstud = parkstud - 1;
                                    UpdateParkSpaceS(parkstud.ToString());
                                }
                                else {
                                    MySqlCommand cmd = new MySqlCommand("UPDATE parkingdetail SET status = 'Not Parked' WHERE regid = " + trylang + ";", con);

                                    con.Open(); cmd.ExecuteNonQuery();
                                    con.Close();

                                    parkstud = parkstud + 1;
                                    UpdateParkSpaceS(parkstud.ToString());
                                }
                                MessageBox.Show(trylang.ToString() + ", Hello " + name + " Welcome" + parkstud.ToString() + "");
								MessageBox.Show("Verified");

							}
							else
							{
								MessageBox.Show("SORRY THE PLATE DOES NOT MATCH!");
							}
						}

						if(clas == "OLD")
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

								SetLP(licplt);

								MessageBox.Show(trylang.ToString() + ", Hello " + name + " Welcome");
								MessageBox.Show("Verified");

							}
							else
							{
								MessageBox.Show("SORRY THE PLATE DOES NOT MATCH!");
							}
						}
                    }
                }
            }
        }



  //      public static string GetText(Bitmap imagesource) //TESSERACT OCR FROM NuGet PACKAGE OF VISUAL STUDIO 2017
  //      {
		//	FileStream fs = new FileStream("C:\\Users\\Mayers Matthew\\Documents\\Visual Studio 2017\\Projects\\Testing\\Testing\\bin\\Debug\\bb.png", FileMode.Open, FileAccess.Read);
		//	Image tempe = Image.FromStream(fs);
		//	fs.Close();
		//	Bitmap imeg = new Bitmap(tempe);

		//	var ocrtext = string.Empty;
		//	using (var engine = new TesseractEngine("./tessdata", "eng", EngineMode.Default))
		//	{
		//		using (var img = PixConverter.ToPix(imagesource))
		//		{
		//			using (var page = engine.Process(img))
		//			{
		//				ocrtext = page.GetText();
		//			}
		//		}
		//	}
		//	return ocrtext;
		//}
        
        private void SetLP(string licenseplate) //to display extracted text from license plate
        {
            setLicPlate(String.Format("LP: {0}", licenseplate));
        }

        private void UpdateParkSpaceS(string parkSspace) //to display extracted text from license plate
        {
            parkingSspace(String.Format("Student Parking Space: {0}", parkSspace));
        }

        private void UpdateParkSpaceE(string parkEspace) //to display extracted text from license plate
        {
            parkingEspace(String.Format("Employee Parking Space: {0}", parkEspace));
        }

        private void UpdateStatus(int FAR) //to display False accept Rate of fingerprint scanning
        {
            SetStatus(String.Format("False Accept Rate(FAR) = {0}", FAR));

        }

        private DPFP.Template Template;
        private DPFP.Verification.Verification Verificator;
    }
}
