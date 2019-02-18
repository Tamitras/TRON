using System;
using System.Drawing;
using System.Drawing.Imaging;
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
    }
}
