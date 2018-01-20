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
	class savetananforfutureuse
	{
		/*
		#region pre-processing methods for detected license plate


		Mat grayplate = new Mat();
		CvInvoke.CvtColor(cropped, grayplate, ColorConversion.Bgr2Gray);

        Mat adap = new Mat();
		CvInvoke.AdaptiveThreshold(grayplate, adap, 255, AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 15, -5);
                        
                        Mat structElem = new Mat();
		structElem = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1));

                        Mat open = new Mat();
		CvInvoke.MorphologyEx(grayplate, open, MorphOp.Open, structElem, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                        Mat openeotsu = new Mat();
		CvInvoke.Threshold(open, openeotsu, 0, 255, ThresholdType.Otsu);

                        Mat close = new Mat();
		CvInvoke.MorphologyEx(grayplate, close, MorphOp.Close, structElem, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                        Mat closeotsu = new Mat();
		CvInvoke.Threshold(close, closeotsu, 0, 255, ThresholdType.Otsu);

                        Mat dilation = new Mat();
		CvInvoke.MorphologyEx(closeotsu, dilation, MorphOp.Dilate, structElem, new Point(-1, 1), 1, BorderType.Default, new MCvScalar());

                        Mat dileotsu = new Mat();
		CvInvoke.Threshold(dilation, dileotsu, 0, 255, ThresholdType.Otsu);

                        Mat canny = new Mat();
		CvInvoke.Canny(dileotsu, canny, 100, 50, 3, false);


						#endregion
licenseplate.Image.Save("bb.png");
			FileStream fs = new FileStream(Application.StartupPath + @"\bb.png", FileMode.Open, FileAccess.Read);
			Image temp = Image.FromStream(fs);
			fs.Close();
			Bitmap tryo = new Bitmap(temp);*/

//wow = GetText(tryo);
//SetLP(wow);
// duh = true;

/*#region CHARACTER SEGMENTATION CODE


float pn_area = cropped.Size.Width * cropped.Size.Height;
using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
{
	CvInvoke.FindContours(canny, contours, null, RetrType.Ccomp, ChainApproxMethod.ChainApproxSimple);
	int count = contours.Size;
	Mat RectangleImage = new Mat(canny.Size, DepthType.Cv8U, 3);
	RectangleImage.SetTo(new MCvScalar(0));
	for (int i = 0; i < count; i++)
	{
		using (VectorOfPoint contour = contours[i])
		using (VectorOfPoint approxContour = new VectorOfPoint())
		{
			CvInvoke.ApproxPolyDP(contour, approxContour, 3, true);
			Rectangle rectangle = CvInvoke.BoundingRectangle(approxContour);
			float h = rectangle.Size.Height;
			float w = rectangle.Size.Width;
			float hh = h / w;
			float ww = w / h;
			float thearea = ((h * w) / pn_area) * 100;
			if ((hh > 1.1 && hh < 7.0) && (ww > 0.10 && ww < 1.0) && (thearea > 1.0))
			{
				boxlist.Add(CvInvoke.MinAreaRect(approxContour));
				Mat cropped2 = new Mat(cropped, rectangle);
				listchar.Add(cropped2);
				coord.Add(rectangle.Location.X);
			}

		}
	}
}
List<string> extracted = new List<string>();
for (int i = 0; i < listchar.Count; i++)
{
	//MessageBox.Show(i.ToString());
	//CvInvoke.Imshow(i.ToString() + " Row: " + listchar[i].Rows.ToString() + " Col: " + listchar[i].Cols.ToString(), listchar[i]);
	Bitmap kuha = changeimg(listchar[i]);
	string tryr = GetText(kuha);
	extracted.Add(tryr);
	MessageBox.Show(extracted[i]);
}
string testingan = string.Join("",extracted);
SetLP(testingan);

#endregion
									

                        #region FOR REARRANGING SEGMENTED IMAGES


                        for (int i = 0; i<coord.Count; i++)
                        {
                            for (int j = 1; j<coord.Count; j++)
                            {
                                if (coord[i] == coord[j])
                                {
                                    coord.RemoveAt(j);
                                    listchar.RemoveAt(j);
                                }
}
                        }

                        for (int i = 0; i<coord.Count; i++)
                        {
                            for (int j = i; j<coord.Count; j++)
                            {
                                if (coord[i] > coord[j])
                                {
                                    Mat w = listchar[i];
listchar[i] = listchar[j];
                                    listchar[j] = w;
                                }
                            }
                        }

                        for (int i = 0; i<coord.Count; i++)
                        {
                            for (int j = i; j<coord.Count; j++)
                            {
                                if (coord[i] > coord[j])
                                {
                                    int w = coord[i];
coord[i] = coord[j];
                                    coord[j] = w;
                                }
                            }
                        }

						#endregion

						//SetLP(licplt);
						List<string> extracted = new List<string>();
						for (int i = 0; i<coord.Count; i++)
                        {
                            Bitmap kuha = changeimg(listchar[i]);
MessageBox.Show(coord[i].ToString());
							string tryr = GetText(kuha);
extracted.Add(tryr);
						}
						string testingan = string.Join("", extracted);
SetLP(testingan);



public static Bitmap changeimg(Mat image) //To save image in a folder then calling it back again to be processed
{
	Bitmap img;



	cerode.Save("aa.png");
	FileStream fs = new FileStream(Application.StartupPath + @"\aa.png", FileMode.Open, FileAccess.Read);
	Image tempe = Image.FromStream(fs);
	fs.Close();
	img = new Bitmap(tempe);

	return img;
}*/

	}
}
