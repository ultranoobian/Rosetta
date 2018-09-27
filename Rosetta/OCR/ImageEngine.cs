using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using OCS = OpenCvSharp;
using Cv2 = OpenCvSharp.Cv2;

namespace OCR
{
    /// <summary>
    /// ImageEngine examines an image using OpenCV and highlights regions of interest
    /// that may contain tables and table-like formats.
    /// </summary>
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
            Cv2.CvtColor(rsz, grey, ColorConversionCodes.BGR2GRAY);

#if IMG_DEBUG
            Cv2.ImShow("grey", grey);
            Cv2.WaitKey(0);
#endif

            //Apply adaptive thresholding to get negative
            Mat bw = new Mat();
            Cv2.AdaptiveThreshold(~grey, bw, 255, AdaptiveThresholdTypes.MeanC, ThresholdTypes.Binary, 15, -2);
            bit = OCS.Extensions.BitmapConverter.ToBitmap(bw);
            bit.Save("bw.tiff", System.Drawing.Imaging.ImageFormat.Tiff);

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
#if IMG_DEBUG
            Cv2.ImShow("horizontal", horizontal);
            Cv2.WaitKey(0);
#endif
            bit = OCS.Extensions.BitmapConverter.ToBitmap(horizontal);
            bit.Save("horizontal.tiff", System.Drawing.Imaging.ImageFormat.Tiff);

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
#if IMG_DEBUG
            Cv2.ImShow("vertical", vertical);
            Cv2.WaitKey(0);
#endif
            bit = OCS.Extensions.BitmapConverter.ToBitmap(vertical);
            bit.Save("vertical.tiff", System.Drawing.Imaging.ImageFormat.Tiff);


            // create a mask which includes the tables
            Mat mask = horizontal + vertical;
#if IMG_DEBUG
            Cv2.ImShow("mask", mask);
            Cv2.WaitKey(0);
#endif

            // find the joints between the lines of the tables, we will use this information in order to descriminate tables from pictures (tables will contain more than 4 joints while a picture only 4 (i.e. at the corners))
            Mat joints = new Mat();
            //bitwise_and(horizontal, vertical, joints);
            Cv2.BitwiseAnd(horizontal, vertical, joints);

            //Cv2.ImShow("joints", joints);
            bit = OCS.Extensions.BitmapConverter.ToBitmap(joints);
            bit.Save("joints.tiff", System.Drawing.Imaging.ImageFormat.Tiff);
#if IMG_DEBUG
            Cv2.ImShow("a", joints);
            Cv2.WaitKey(0);
#endif

            //Thread.Sleep(2000);


            // Find external contours from the mask, which most probably will belong to tables or to images
            //vector<Vec4i> hierarchy;
            //std::vector<std::vector<cv::Point>> contours;
            OCS.HierarchyIndex[] hierarchy;
            //List<List<OCS.Point>> contours = new List<List<OCS.Point>>;
            OCS.Point[][] contours;
            //cv::findContours(mask, contours, hierarchy, CV_RETR_EXTERNAL, CV_CHAIN_APPROX_SIMPLE, Point(0, 0));
            Cv2.FindContours(mask, out contours, out hierarchy, OCS.RetrievalModes.External, OCS.ContourApproximationModes.ApproxSimple, new OCS.Point(0, 0));

            //////vector<vector<Point>> contours_poly(contours.size() );
            //////vector<Rect> boundRect(contours.size() );
            //////vector<Mat> rois;
            List<List<OCS.Point>> contours_poly = new List<List<OCS.Point>>(contours.Length);
            List<OCS.Rect> boundRect = new List<OCS.Rect>(contours.Length);
            List<Mat> rois = new List<Mat>();
             

            ////for (size_t i = 0; i < contours.size(); i++)
            ////{
            ////    // find the area of each contour
            ////    double area = contourArea(contours[i]);
            ///
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
            ////    //drawContours( rsz, contours, i, Scalar(0, 0, 255), CV_FILLED, 8, vector<Vec4i>(), 0, Point() );
            ////    rectangle(rsz, boundRect[i].tl(), boundRect[i].br(), Scalar(0, 255, 0), 1, 8, 0);
            ////}

            for(int i = 0; i < contours.Length; i++)
            {
                double area = Cv2.ContourArea(contours[i]);
                if (area < 100.0)
                {
                    // Skip because its not likely such a small area is a cell
                    continue;
                }
                // contours_poly is null at runtime. so we create a new entry and exit array
                contours_poly.Add(new List<OCS.Point>());
                OutputArray contour_poly_output = OutputArray.Create(contours_poly[i]);

                InputArray contour_poly_input = InputArray.Create(contours[i]);
                Cv2.ApproxPolyDP(InputArray.Create(contours[i]), contour_poly_output, 0.0, true);
                Rect boundingRect = Cv2.BoundingRect(InputArray.Create(contours_poly[i]));
                boundRect.Add(boundingRect);
                //boundRect[i] = Cv2.BoundingRect(InputArray.Create(contours_poly[i]));
                //OCS.Mat roi = Cv2.joints()
            }
#if IMG_DEBUG
            Cv2.NamedWindow("Output", WindowMode.KeepRatio);
            Cv2.Rectangle(rsz, boundRect.ElementAt(0), Scalar.Red, 10);
            Cv2.ImShow("Output", rsz);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
#endif
            ////for (size_t i = 0; i < rois.size(); ++i)
            ////{
            ////    /* Now you can do whatever post process you want
            ////     * with the data within the rectangles/tables. */
            ////    imshow("roi", rois[i]);
            ////    waitKey();
            ////}

            return boundRect.Count;
        }
    }
}
