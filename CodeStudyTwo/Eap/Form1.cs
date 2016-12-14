using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Eap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //System.Net.WebClient 类支持基于事件的异步模式（EAP)
            WebClient wc = new WebClient();

            //一个String下载好之后，WebClient对象引发
            //DownloadStringCompleted事件，会造成我们的ProcessString方法的调用
            wc.DownloadStringCompleted += wc_DownloadStringCompleted;

            //开始异步操作（这类似于调用一个Beggin方法)
            wc.DownloadStringAsync(new Uri("http://www.baidu.com"));
            base.OnClick(e);

            TcpClient tcpClient = new TcpClient();
        }

        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            MessageBox.Show((e.Error != null) ? e.Error.Message : e.Result);
        }
    }
}
