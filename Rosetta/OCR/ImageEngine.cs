using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using OCS = OpenCvSharp;
using Cv2 = OpenCvSharp.Cv2;
using Point = System.Drawing.Point;

namespace OCR
{
    public class ImageEngine
    {


        public static int ExtractTables(Image img)
        {
            //Delete this after
            Bitmap bit;


            List<Rectangle> AreasOfInterest = new List<Rectangle>();
            Bitmap bitmap = (Bitmap)img;

            Mat srcImg = OCS.Extensions.BitmapConverter.ToMat(bitmap);
            if (srcImg.Data == null)
            {
                throw new NullReferenceException("Image has nothing?");
            }

            //Resize into smaller size
            Mat rsz = new Mat();
            OpenCvSharp.Size size = new OpenCvSharp.Size(4000, 2828);
            Cv2.Resize(srcImg, rsz, size);

            // Convert to greyscale if it has more than one channel
            // else just leave it alone
            Mat grey = new Mat();
            if (rsz.Channels() == 3)
            {
                Cv2.CvtColor(rsz, grey, ColorConversionCodes.BGR2GRAY);
            }
            else
            {
                grey = rsz;
            }

            //Apply adaptive thresholding to get negative
            Mat bw = new Mat();
            Cv2.AdaptiveThreshold(~grey, bw, 255, AdaptiveThresholdTypes.MeanC, ThresholdTypes.Binary, 15, -2);
            bit = OCS.Extensions.BitmapConverter.ToBitmap(bw);
            bit.Save("bw.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            // Create two new masks cloned from bw.

            Mat horizontal = bw.Clone();
            Mat vertical = bw.Clone();

            // adjust this for number of lines
            int scale = 10;

            /////////////////////////
            /////////////////////////
            /////////////////////////

            // Specify size on horizontal axis
            int horizontalsize = horizontal.Cols / scale;

            // Create structure element for extracting horizontal lines through morphology operations
            //Mat horizontalStructure = getStructuringElement(MORPH_RECT, Size(horizontalsize, 1));
            Mat horizontalStructure = Cv2.GetStructuringElement(MorphShapes.Rect, new OCS.Size(horizontalsize, 1));

            // Apply morphology operations
            //erode(horizontal, horizontal, horizontalStructure, Point(-1, -1));
            Cv2.Erode(horizontal, horizontal, horizontalStructure, new OCS.Point(-1, -1));
            //dilate(horizontal, horizontal, horizontalStructure, Point(-1, -1));
            Cv2.Dilate(horizontal, horizontal, horizontalStructure, new OCS.Point(-1, -1));

            //    dilate(horizontal, horizontal, horizontalStructure, Point(-1, -1)); // expand horizontal lines

            // Show extracted horizontal lines
            Cv2.ImShow("horizontal", horizontal);
            bit = OCS.Extensions.BitmapConverter.ToBitmap(horizontal);
            bit.Save("horizontal.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            // Specify size on vertical axis
            int verticalsize = vertical.Rows / scale;

            // Create structure element for extracting vertical lines through morphology operations
            //Mat verticalStructure = getStructuringElement(MORPH_RECT, Size(1, verticalsize));
            Mat verticalStructure = Cv2.GetStructuringElement(MorphShapes.Rect, new OCS.Size(1, verticalsize));

            // Apply morphology operations
            //erode(vertical, vertical, verticalStructure, Point(-1, -1));
            Cv2.Erode(vertical, vertical, verticalStructure, new OCS.Point(-1, -1));
            //dilate(vertical, vertical, verticalStructure, Point(-1, -1));
            Cv2.Dilate(vertical, vertical, verticalStructure, new OCS.Point(-1, -1));

            // Show extracted vertical lines
            Cv2.ImShow("vertical", vertical);
            bit = OCS.Extensions.BitmapConverter.ToBitmap(vertical);
            bit.Save("vertical.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);


            // create a mask which includes the tables
            Mat mask = horizontal + vertical;
            Cv2.ImShow("mask", mask);

            // find the joints between the lines of the tables, we will use this information in order to descriminate tables from pictures (tables will contain more than 4 joints while a picture only 4 (i.e. at the corners))
            Mat joints = new Mat();
            //bitwise_and(horizontal, vertical, joints);
            Cv2.BitwiseAnd(horizontal, vertical, joints);

            //Cv2.ImShow("joints", joints);
            bit = OCS.Extensions.BitmapConverter.ToBitmap(joints);
            bit.Save("joints.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            //Thread.Sleep(2000);


            // Find external contours from the mask, which most probably will belong to tables or to images
            //vector<Vec4i> hierarchy;
            //std::vector<std::vector<cv::Point>> contours;
            OCS.HierarchyIndex[] hierarchy;
            OCS.Point[][] contours;
            //cv::findContours(mask, contours, hierarchy, CV_RETR_EXTERNAL, CV_CHAIN_APPROX_SIMPLE, Point(0, 0));
            Cv2.FindContours(mask, out contours, out hierarchy, OCS.RetrievalModes.External, OCS.ContourApproximationModes.ApproxSimple, new OCS.Point(0, 0));

            //////vector<vector<Point>> contours_poly(contours.size() );
            //////vector<Rect> boundRect(contours.size() );
            //////vector<Mat> rois;
            ////ArrayList<ArrayList<OCS.Point>> contours_poly = new ArrayList<ArrayList<OCS.Point>>();


            ////for (size_t i = 0; i < contours.size(); i++)
            ////{
            ////    // find the area of each contour
            ////    double area = contourArea(contours[i]);

            ////    //        // filter individual lines of blobs that might exist and they do not represent a table
            ////    if (area < 100) // value is randomly chosen, you will need to find that by yourself with trial and error procedure
            ////        continue;

            ////    approxPolyDP(Mat(contours[i]), contours_poly[i], 3, true);
            ////    boundRect[i] = boundingRect(Mat(contours_poly[i]));

            ////    // find the number of joints that each table has
            ////    Mat roi = joints(boundRect[i]);

            ////    vector<vector<Point>> joints_contours;
            ////    findContours(roi, joints_contours, RETR_CCOMP, CHAIN_APPROX_SIMPLE);

            ////    // if the number is not more than 5 then most likely it not a table
            ////    if (joints_contours.size() <= 4)
            ////        continue;

            ////    rois.push_back(rsz(boundRect[i]).clone());

            ////    //        drawContours( rsz, contours, i, Scalar(0, 0, 255), CV_FILLED, 8, vector<Vec4i>(), 0, Point() );
            ////    rectangle(rsz, boundRect[i].tl(), boundRect[i].br(), Scalar(0, 255, 0), 1, 8, 0);
            ////}

            ////for (size_t i = 0; i < rois.size(); ++i)
            ////{
            ////    /* Now you can do whatever post process you want
            ////     * with the data within the rectangles/tables. */
            ////    imshow("roi", rois[i]);
            ////    waitKey();
            ////}




            return AreasOfInterest.Count;
        }
    }




    struct Rectangle
    {
        Point x1;
        Point y1;
        Point width;
        Point height;
    }
}
