using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Diagnostics;

namespace watch.WatchLib
{
    public class ConnectServer
    {
        // リスナースレッド
        private Thread th;
        
        private TcpClient client;
        public String remote_IP = "127.0.0.1";
        public Int32 remote_Port = 2010;
        private bool thread_status = true;
        private Capture capture = new Capture();

        public void startServer()
        {
            th = new Thread(DataListener);
            th.IsBackground = true;
            th.Start();

        }

        /// <summary>
        /// 強制終了
        /// </summary>
        public void kill()
        {
            if (client != null && client.Connected == true)
            {
                client.Close();

            }

            thread_status = false;
            th.Abort();

        }

        // リスナースレッド
        private void DataListener()
        {
            try
            {
                if (client == null)
                {
                    client = new TcpClient();
                }
                client.Connect(remote_IP, remote_Port);

                //---------------------------------------------------------
                while (thread_status == true)
                {
                    System.IO.MemoryStream mms1 = new System.IO.MemoryStream();
                    capture.getCaptureImage().Save(mms1,
                        System.Drawing.Imaging.ImageFormat.Bmp);
                    Byte[] sendData = mms1.GetBuffer();
                    mms1.Close();
                    NetworkStream ns = client.GetStream();
                    ns.Write(sendData, 0, sendData.Length);
                    Debug.WriteLine("send image");
                    Thread.Sleep(200);
                }
            }
            catch (Exception ex)
            {
                kill();
                //MessageBox.Show(ex.ToString() + "Server error.");
            }
        }

        public System.Drawing.Bitmap ConvertBytesToImage(byte[] Image_Bytes)
        {
            // 入力引数の異常時のエラー処理
            if ((Image_Bytes == null) || (Image_Bytes.Length == 0))
            {
                return null;
            }

            // 返却用Bitmap型オブジェクト
            System.Drawing.Bitmap ResultBmp;

            // バイナリ(Byte配列)をストリームに保存
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Image_Bytes))
            {
                // ストリームのバイナリ(Byte配列)をBitmapに変換
                ResultBmp = new System.Drawing.Bitmap(ms);

                // ストリームのクローズ
                ms.Close();
            }

            return ResultBmp;
        }



    }


}
