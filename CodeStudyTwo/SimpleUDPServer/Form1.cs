using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace SimpleUDPServer
{
    public partial class Form1 : Form
    {
        UDP _udp;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress[] ips = Dns.GetHostAddresses("");
            if (_udp == null)
                _udp = new UDP(ips[ips.Length-1].ToString(), this.textBox1.Text,Show);
            _udp.BeginRecevice();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_udp != null) _udp.EndRecevice();
        }

        void Show(byte[] data,IPEndPoint remoteIp)
        {
            Action action = () =>
            {
                String message = Encoding.UTF8.GetString(data);
                this.listBox1.Items.Add(message);
            };
            this.listBox1.BeginInvoke(action);
            if (_udp != null)
                _udp.BegingSend(remoteIp.Address.ToString(), remoteIp.Port.ToString(), "Holly Shit!");
        }
    }
}
