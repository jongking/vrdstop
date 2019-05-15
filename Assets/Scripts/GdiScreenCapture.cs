using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Video.Model
{
    class GdiScreenCapture
    {
        public IntPtr handle;
        public IntPtr hdcSrc;
        public GdiScreenCapture()
        {
            handle = User32.GetDesktopWindow();
            // get te hDC of the target window
            hdcSrc = User32.GetWindowDC(handle);
        }

        ~GdiScreenCapture()
        {
            User32.ReleaseDC(handle, hdcSrc);
            //mstream.Close();
        }

        /// <summary>
        /// Creates an Image object containing a screen shot of a specific window
        /// </summary>
        /// <param name="handle">The handle to the window. (In windows forms, this is obtained by the Handle property)</param>
        /// <returns></returns>
        public Image CaptureWindowSBS()
        {
            return CaptureWindowSBS(0, 0);
            // get the size
            //User32.RECT windowRect = new User32.RECT();
            //User32.GetWindowRect(handle, ref windowRect);
            //int width = windowRect.right - windowRect.left;
            //int height = windowRect.bottom - windowRect.top;
            //// create a device context we can copy to
            //IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            //// create a bitmap we can copy it to,
            //// using GetDeviceCaps to get the width/height
            //IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            //// select the bitmap object
            //IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            //// bitblt over
            //GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
            //// restore selection
            //GDI32.SelectObject(hdcDest, hOld);
            //// clean up
            //GDI32.DeleteDC(hdcDest);
            ////User32.ReleaseDC(handle, hdcSrc);
            //// get a .NET image object for it
            //Image img = Image.FromHbitmap(hBitmap);
            ////Image newImage = new Bitmap(img.Height, img.Width);
            ////Graphics g = Graphics.FromImage(newImage);
            ////Point[] destinationPoints = {
            ////    new Point(img.Height, 0), // destination for upper-left point of original
            ////    new Point(img.Height, img.Width),// destination for upper-right point of original
            ////    new Point(0, 0)}; // destination for lower-left point of original
            ////g.DrawImage(img, destinationPoints);
            ////img.Dispose();
            ////img = null;

            //// free up the Bitmap object
            //GDI32.DeleteObject(hBitmap);
            //return img;


        }

        public Image CaptureWindowSBS(int resizewidth, int resizeheight)
        {
            // get the size
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;
            // create a device context we can copy to
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            // bitblt over
            GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
            // restore selection
            GDI32.SelectObject(hdcDest, hOld);
            // clean up
            GDI32.DeleteDC(hdcDest);
            //User32.ReleaseDC(handle, hdcSrc);
            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            GDI32.DeleteObject(hBitmap);

            if (resizewidth > img.Width || resizeheight > img.Height)
            {
                Image newImage = new Bitmap(resizewidth, resizeheight);
                Graphics g = Graphics.FromImage(newImage);
                int resizeleft = (resizewidth - img.Width)/2;
                int resizetop = (resizeheight - img.Height)/2;
                //g.DrawImage(img, resizeleft, resizetop, img.Width / 2, img.Height);
                g.DrawImage(img, 0, resizetop, new Rectangle(0, 0, img.Width / 2, img.Height), GraphicsUnit.Pixel);
                g.DrawImage(img, 0 + resizewidth / 2, resizetop, new Rectangle(img.Width / 2, 0, img.Width / 2, img.Height), GraphicsUnit.Pixel);
                img.Dispose();
                img = null;

                return newImage;
            }
            else
            {
                return img;
            }
        }

        public Image CaptureWindowUV(int resizewidth, int resizeheight)
        {
            // get the size
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;
            // create a device context we can copy to
            IntPtr hdcDest = GDI32.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = GDI32.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object
            IntPtr hOld = GDI32.SelectObject(hdcDest, hBitmap);
            // bitblt over
            GDI32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, GDI32.SRCCOPY);
            // restore selection
            GDI32.SelectObject(hdcDest, hOld);
            // clean up
            GDI32.DeleteDC(hdcDest);
            //User32.ReleaseDC(handle, hdcSrc);
            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            GDI32.DeleteObject(hBitmap);

            if (resizewidth > img.Width || resizeheight > img.Height)
            {
                Image newImage = new Bitmap(resizewidth, resizeheight);
                Graphics g = Graphics.FromImage(newImage);
                int resizeleft = (resizewidth - img.Width) / 2;
                int resizetop = (resizeheight - img.Height) / 2;
                //g.DrawImage(img, resizeleft, resizetop, img.Width / 2, img.Height);
                g.DrawImage(img, resizeleft, resizetop, new Rectangle(0, 0, img.Width, img.Height / 2), GraphicsUnit.Pixel);
                g.DrawImage(img, resizeleft, resizetop + img.Height / 2, new Rectangle(0, img.Height/2, img.Width, img.Height/2), GraphicsUnit.Pixel);
                img.Dispose();
                img = null;

                return newImage;
            }
            else
            {
                return img;
            }
        }

        public byte[] PhotoImageInsert(System.Drawing.Image imgPhoto)
        {
            MemoryStream mstream = new MemoryStream();
            imgPhoto.Save(mstream, ImageFormat.MemoryBmp);
            byte[] byData = new Byte[mstream.Length];
            mstream.Position = 0;
            mstream.Read(byData, 0, byData.Length);
            mstream.Close();
            return byData;
        }

        //MemoryStream mstream = new MemoryStream();
        //public byte[] PhotoImageInsert(System.Drawing.Image imgPhoto)
        //{
        //    imgPhoto.Save(mstream, System.Drawing.Imaging.ImageFormat.Bmp);
        //    byte[] byData = new Byte[mstream.Length];
        //    mstream.Position = 0;
        //    mstream.Read(byData, 0, byData.Length);
        //    return byData;
        //}

        /// <summary>
        /// Helper class containing Gdi32 API functions
        /// </summary>
        private class GDI32
        {

            public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter

            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
                int nWidth, int nHeight, IntPtr hObjectSource,
                int nXSrc, int nYSrc, int dwRop);

            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
                int nHeight);

            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);

            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);

            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }

        /// <summary>
        /// Helper class containing User32 API functions
        /// </summary>
        private class User32
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
            public static extern IntPtr GetDesktopWindow();

            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);

            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        }
    }
}