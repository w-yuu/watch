using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using watch.WatchLib;

namespace watch
{
    public partial class Form1 : Form
    {
        private ConnectServer imageServer = new ConnectServer();
        private ConnectMessage messageServer = new ConnectMessage();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            imageServer.startServer();
            messageServer.startServer();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            imageServer.kill();
            messageServer.kill();
        }
    }
}
