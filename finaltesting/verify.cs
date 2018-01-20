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

        public void Verify(DPFP.Template template)
        {
            Template = template;
            ShowDialog();
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
						string clas = (string)dr["plateclass"];

						#region LICENSE PLATE RECOGNITION CODE

						string str = "D:\\THESIS II NEEDS\\this\\Auto_parking_LPR_share\\Auto_parking\\Auto_parking\\bin\\Debug\\ImageTest\\3c.jpg";
						//string str = "C:\\Users\\Mayers Matthew\\Desktop\\FINALPROJECT\\3.jpg";
						Mat cropped = new Mat();
                        Mat ORIG = new Mat(str);

                        List<RotatedRect> boxList = new List<RotatedRect>();
                        List<int> coord = new List<int>();

                        Mat gray1 = new Mat(str, ImreadModes.Grayscale);

                        Mat otsus = new Mat();
                        CvInvoke.Threshold(gray1, otsus, 0, 255, ThresholdType.Otsu);

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

							cerode.Save("nn.png");
							FileStream fs = new FileStream(Application.StartupPath + @"\nn.png", FileMode.Open, FileAccess.Read);
							Image tempe = Image.FromStream(fs);
							fs.Close();
							Bitmap imgnew = new Bitmap(tempe);

							var img = new Bitmap(tempe);
							var ocr = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
							var page = ocr.Process(img);

							int count = 0;

							string newplt = page.GetText();
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
							if (count == 0)
							{

								SetLP(licplt);

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
							licenseplate.Image = otsu;
							otsu.Save("orayt.jpg");
						}

						
						MessageBox.Show(trylang.ToString() + ", Hello " + name + " Welcome");
                        MessageBox.Show("Verified");
                    }
                }
            }
        }



        public static string GetText(Bitmap imagesource) //TESSERACT OCR FROM NuGet PACKAGE OF VISUAL STUDIO 2017
        {
			FileStream fs = new FileStream("C:\\Users\\Mayers Matthew\\Documents\\Visual Studio 2017\\Projects\\Testing\\Testing\\bin\\Debug\\bb.png", FileMode.Open, FileAccess.Read);
			Image tempe = Image.FromStream(fs);
			fs.Close();
			Bitmap imeg = new Bitmap(tempe);

			var ocrtext = string.Empty;
			using (var engine = new TesseractEngine("./tessdata", "eng", EngineMode.Default))
			{
				using (var img = PixConverter.ToPix(imagesource))
				{
					using (var page = engine.Process(img))
					{
						ocrtext = page.GetText();
					}
				}
			}
			return ocrtext;
		}
        
        private void SetLP(string licenseplate) //to display extracted text from license plate
        {
            setLicPlate(String.Format("LP: {0}", licenseplate));
        }

        private void UpdateStatus(int FAR) //to display False accept Rate of fingerprint scanning
        {
            SetStatus(String.Format("False Accept Rate(FAR) = {0}", FAR));

        }

        private DPFP.Template Template;
        private DPFP.Verification.Verification Verificator;
    }
}
