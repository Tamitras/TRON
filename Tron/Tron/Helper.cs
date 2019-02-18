using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Tron
{
    class Helper
    {

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        static extern int GetWindowRgn(IntPtr hWnd, IntPtr hRgn);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT Rect);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);



        public const int SRCCOPY = 13369376;
        public const int WM_CLICK = 0x00F5;
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "GetDC")]
        internal static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleDC")]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport("gdi32.dll", EntryPoint = "CreateCompatibleBitmap")]
        internal static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
        [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
        internal static extern IntPtr DeleteDC(IntPtr hDc);
        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        internal static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);
        [DllImport("gdi32.dll", EntryPoint = "BitBlt")]
        internal static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, int RasterOp);
        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        internal static extern IntPtr DeleteObject(IntPtr hDc);
        [DllImport("user32.dll")]
        public static extern int SendMessage(
              int hWnd,      // handle to destination window
              uint Msg,       // message
              long wParam,  // first message parameter
              long lParam   // second message parameter
              );
        //static IntPtr hWnd = FindWindow(null, "My programs title");

        public static Bitmap createBitmap(IntPtr hWnd, int heigth, int width)
        {
            Bitmap bmp = null;
            IntPtr hdcFrom = GetDC(hWnd);
            IntPtr hdcTo = CreateCompatibleDC(hdcFrom);
            //X and Y coordinates of window
            //int Width = 529;
            //int Height = 436;


            int Width = heigth;
            int Height = width;


            IntPtr hBitmap = CreateCompatibleBitmap(hdcFrom, Width, Height);
            if (hBitmap != IntPtr.Zero)
            {
                // adjust and copy
                IntPtr hLocalBitmap = SelectObject(hdcTo, hBitmap);
                BitBlt(hdcTo, 0, 0, Width, Height,
                    hdcFrom, 0, 0, SRCCOPY);
                SelectObject(hdcTo, hLocalBitmap);
                //We delete the memory device context.
                DeleteDC(hdcTo);
                //We release the screen device context.
                ReleaseDC(hWnd, hdcFrom);
                //Image is created by Image bitmap handle and assigned to Bitmap variable.
                bmp = System.Drawing.Image.FromHbitmap(hBitmap);
                //Delete the compatible bitmap object. 
                DeleteObject(hBitmap);
                //bmp.Save(@"C:\Users\ekaufmann\Desktop\screenys", ImageFormat.Bmp);
            }
            return bmp;
        }



        //private static MCvScalar drawingColor = new Bgr(Color.Red).MCvScalar;

        //// Determines boundary of brightness while turning grayscale image to binary (black-white) image
        //private const int Threshold = 5;

        //// Erosion to remove noise (reduce white pixel zones)
        //private const int ErodeIterations = 3;

        //// Dilation to enhance erosion survivors (enlarge white pixel zones)
        //private const int DilateIterations = 3;

        //// Containers for images demonstrating different phases of frame processing 
        ////private static Mat rawFrame = new Mat(@"C:\Users\ekaufmann\Desktop\screenys\theme.jpg");           // Frame as obtained from video
        //private static Mat rawFrame = new Mat();           // Frame as obtained from video
        //private static Mat backgroundFrame = new Mat();    // Frame used as base for change detection
        //private static Mat diffFrame = new Mat();          // Image showing differences between background 
        //                                                   // and raw frame
        //private static Mat grayscaleDiffFrame = new Mat(); // Image showing differences in 8-bit color depth
        //private static Mat binaryDiffFrame = new Mat();    // Image showing changed areas in white and 
        //                                                   // unchanged in black
        //private static Mat denoisedDiffFrame = new Mat();  // Image with irrelevant changes removed 
        //                                                   // with opening operation
        //private static Mat finalFrame = new Mat();         // Video frame with detected object marked

        //public static void ProcessFrame(Mat backgroundFrame, int threshold, int erodeIterations, int dilateIterations)
        //{

        //    Mat rawFrame = CvInvoke.Imread("C:/Users/ekaufmann/Desktop/screenys/theme.bmp");          // Frame as obtained from video
        //    backgroundFrame = CvInvoke.Imread("C:/Users/ekaufmann/Desktop/screenys/test.bmp");   // Frame used as base for change detection
        //    Mat diffFrame = new Mat();

        //    // Find difference between background (first) frame and current frame
        //    CvInvoke.AbsDiff(backgroundFrame, rawFrame, diffFrame);

        //    // Apply binary threshold to grayscale image (white pixel will mark difference)
        //    CvInvoke.CvtColor(diffFrame, grayscaleDiffFrame, ColorConversion.Bgr2Gray);
        //    CvInvoke.Threshold(grayscaleDiffFrame, binaryDiffFrame, threshold, 255, ThresholdType.Binary);

        //    // Remove noise with opening operation (erosion followed by dilation)
        //    CvInvoke.Erode(binaryDiffFrame, denoisedDiffFrame, null,
        //             new Point(-1, -1), erodeIterations, BorderType.Default, new MCvScalar(1));
        //    CvInvoke.Dilate(denoisedDiffFrame, denoisedDiffFrame, null,
        //             new Point(-1, -1), dilateIterations, BorderType.Default, new MCvScalar(1));

        //    rawFrame.CopyTo(finalFrame);
        //    DetectObject(denoisedDiffFrame, finalFrame);
        //}

        //private static void DetectObject(Mat detectionFrame, Mat displayFrame)
        //{
        //    using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
        //    {
        //        // Build list of contours
        //        CvInvoke.FindContours(detectionFrame, contours, null,
        //                              RetrType.List, ChainApproxMethod.ChainApproxSimple);

        //        // Selecting largest contour
        //        if (contours.Size > 0)
        //        {
        //            double maxArea = 0;
        //            int chosen = 0;
        //            for (int i = 0; i < contours.Size; i++)
        //            {
        //                VectorOfPoint contour = contours[i];

        //                double area = CvInvoke.ContourArea(contour);
        //                if (area > maxArea)
        //                {
        //                    maxArea = area;
        //                    chosen = i;
        //                }
        //            }

        //            // Draw on a frame
        //            MarkDetectedObject(displayFrame, contours[chosen], maxArea);
        //        }
        //    }
        //}

        //private static void MarkDetectedObject(Mat frame, VectorOfPoint contour, double area)
        //{
        //    // Getting minimal rectangle which contains the contour
        //    Rectangle box = CvInvoke.BoundingRectangle(contour);

        //    // Drawing contour and box around it
        //    CvInvoke.Polylines(frame, contour, true, drawingColor);
        //    CvInvoke.Rectangle(frame, box, drawingColor);

        //    // Write information next to marked object
        //    Point center = new Point(box.X + box.Width / 2, box.Y + box.Height / 2);

        //    var info = new string[] {
        //        $"Area: {area}",
        //        $"Position: {center.X}, {center.Y}"
        //    };

        //    WriteMultilineText(frame, info, new Point(box.Right + 5, center.Y));
        //}

        //private static void WriteMultilineText(Mat frame, string[] lines, Point origin)
        //{
        //    for (int i = 0; i < lines.Length; i++)
        //    {
        //        int y = i * 10 + origin.Y; // Moving down on each line
        //        CvInvoke.PutText(frame, lines[i], new Point(origin.X, y),
        //                         FontFace.HersheyPlain, 0.8, drawingColor);
        //    }
        //}

        //public static void ShowWindowsWithImageProcessingStages()
        //{
        //    CvInvoke.Imshow("RawFrameWindowName", rawFrame);
        //    //CvInvoke.Imshow(GrayscaleDiffFrameWindowName, grayscaleDiffFrame);
        //    //CvInvoke.Imshow(BinaryDiffFrameWindowName, binaryDiffFrame);
        //    //CvInvoke.Imshow(DenoisedDiffFrameWindowName, denoisedDiffFrame);
        //    //CvInvoke.Imshow(FinalFrameWindowName, finalFrame);
        //}
    }
}
