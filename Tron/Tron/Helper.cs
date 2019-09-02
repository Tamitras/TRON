using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Tron
{
    class Helper
    {

        /// <summary>
        /// -------------------------------------------------------------------------------
        /// ------------------------
        /// -------------------------------------------------------------------------------
        /// </summary>

        [Flags]
        public enum MouseEventFlags : uint
        {
            MOUSEEVENT_MOVE = 0x0001,
            MOUSEEVENT_LEFTDOWN = 0x0002,
            MOUSEEVENT_LEFTUP = 0x0004,
            MOUSEEVENT_RIGHTDOWN = 0x0008,
            MOUSEEVENT_RIGHTUP = 0x0010,
            MOUSEEVENT_MIDDLEDOWN = 0x0020,
            MOUSEEVENT_MIDDLEUP = 0x0040,
            MOUSEEVENT_XDOWN = 0x0080,
            MOUSEEVENT_XUP = 0x0100,
            MOUSEEVENT_WHEEL = 0x0800,
            MOUSEEVENT_VIRTUALDESK = 0x4000,
            MOUSEEVENT_ABSOLUTE = 0x8000
        }

        [Flags]
        public enum SendInputEventType : uint
        {
            InputMouse,
            InputKeyboard,
            InputHardware
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            public SendInputEventType type;
            public MOUSEANDKEYBOARDINPUT mkhi;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct MOUSEANDKEYBOARDINPUT
        {
            [FieldOffset(0)]
            public MOUSEINPUT mi;

            [FieldOffset(0)]
            public KEYBOARDINPUT ki;

            [FieldOffset(0)]
            public HARDWAREINPUT hi;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBOARDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public MouseEventFlags dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        public static void MouseMove(int dx, int dy)
        {
            INPUT mouseMove = new INPUT();
            mouseMove.type = SendInputEventType.InputMouse;
            mouseMove.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENT_MOVE;
            mouseMove.mkhi.mi.dx = dx;
            mouseMove.mkhi.mi.dy = dy;
            SendInput(1, ref mouseMove, Marshal.SizeOf(new INPUT()));
        }

        public static void ClickLeftMouseButtonDown()
        {
            INPUT mouseDownInput = new INPUT();
            mouseDownInput.type = SendInputEventType.InputMouse;
            mouseDownInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENT_LEFTDOWN;
            SendInput(1, ref mouseDownInput, Marshal.SizeOf(new INPUT()));
        }

        public static void ClickLeftMouseButtonUp()
        {
            INPUT mouseUpInput = new INPUT();
            mouseUpInput.type = SendInputEventType.InputMouse;
            mouseUpInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENT_LEFTUP;
            SendInput(1, ref mouseUpInput, Marshal.SizeOf(new INPUT()));
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImportAttribute("User32.dll")]
        public static extern IntPtr SetForegroundWindow(int hWnd);


        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        // The WM_COMMAND message is sent when the user selects a command item from 
        // a menu, when a control sends a notification message to its parent window, 
        // or when an accelerator keystroke is translated.
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_COMMAND = 0x111;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_LBUTTONDBLCLK = 0x203;
        public const int WM_RBUTTONDOWN = 0x204;
        public const int WM_RBUTTONUP = 0x205;
        public const int WM_RBUTTONDBLCLK = 0x206;

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        public const int MOUSEEVENTF_RIGHTUP = 0x10;
        public const int MOUSEEVENTF_MOVE = 0x01;
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll")]
        static extern int GetWindowRgn(IntPtr hWnd, IntPtr hRgn);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref Rect Rect);

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
                BitBlt(hdcTo, 0, 0, Width, Height, hdcFrom, 0, 0, SRCCOPY);
                SelectObject(hdcTo, hLocalBitmap);
                //We delete the memory device context.
                DeleteDC(hdcTo);
                //We release the screen device context.
                ReleaseDC(hWnd, hdcFrom);
                //Image is created by Image bitmap handle and assigned to Bitmap variable.
                bmp = Image.FromHbitmap(hBitmap);
                //Delete the compatible bitmap object. 
                DeleteObject(hBitmap);
                //bmp.Save(@"C:\Users\ekaufmann\Desktop\screenys", ImageFormat.Bmp);
            }
            return bmp;
        }

        public static IntPtr GetParentFromChild(IntPtr childPtr)
        {
            IntPtr oldptr = childPtr;
            while (true)
            {
                IntPtr newPtr = Helper.GetParent(oldptr);                
               
                if(IntPtr.Zero == newPtr)
                {
                    return oldptr; // ParentProzess
                }
                else
                {
                    oldptr = newPtr; // Next round
                }
            }

        }

    }
}
