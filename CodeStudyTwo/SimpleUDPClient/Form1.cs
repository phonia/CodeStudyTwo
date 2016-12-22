using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using NetHelper;

namespace SimpleUDPClient
{
    public partial class Form1 : Form
    {
        private UDP _udp = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress[] ips = Dns.GetHostAddresses("");
            //if (NetHelper.NetHelper.Ping(ips[1].ToString()) && NetHelper.NetHelper.IsFreePort(Convert.ToInt32(this.textBox3.Text)))
            //{

            //}
            //else
            //{
            //    MessageBox.Show("网络不同或端口被占用！");
            //}
            if (_udp == null)
                _udp = new UDP(ips[ips.Length-1].ToString(), this.textBox3.Text, Show);
            //_udp.BegingSend(this.textBox2.Text, this.textBox4.Text, "Hello World!");
            _udp.BeginRecevice();
            _udp.BeginSend(this.textBox4.Text, "Hello World!");
        }

        void Show(byte[] data, IPEndPoint remoteIp)
        {
            MessageBox.Show(Encoding.UTF8.GetString(data));
        }
    }
}
