using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace watch.WatchLib
{
    class Capture
    {
        //添字に使用
        private DateTime imageIndex;


        /// <summary>
        /// 現在時刻を画像の添え字として提供
        /// </summary>
        /// <returns></returns>
        private String getImageIndex() {
            imageIndex = DateTime.Now;
            return imageIndex.ToString();
        }

        /**
        public Boolean saveCaptureImage(File path, String file_name)
        {
            try
            {
                Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics graph = Graphics.FromImage(bmp);
                graph.CopyFromScreen(new Point(0, 0), new Point(0, 0), bmp.Size);
                graph.Dispose();
                bmp.Save(@"" + getImageIndex() + ".bmp");
                return true;
            }
            catch
            {
                return false;
            }
        }
         * **/

        public Bitmap getCaptureImage()
        {
            try
            {
                Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics graph = Graphics.FromImage(bmp);
                graph.CopyFromScreen(new Point(0, 0), new Point(0, 0), bmp.Size);
                graph.Dispose();
                getImageIndex();
                return bmp;
            }
            catch
            {
                return null;
            }
        }
    }
}
