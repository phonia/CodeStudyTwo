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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress[] ips = Dns.GetHostAddresses("");
            if (NetHelper.NetHelper.Ping(ips[1].ToString()) && NetHelper.NetHelper.IsFreePort(Convert.ToInt32(this.textBox3.Text)))
            {
                UDP udp = new UDP(ips[1].ToString(), this.textBox3.Text);
                udp.BegingSend(this.textBox2.Text, this.textBox4.Text, "Hello World!");
            }
            else
            {
                MessageBox.Show("网络不同或端口被占用！");
            }
        }
    }
}
