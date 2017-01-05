using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace APMTCPClient2._0
{
    public partial class Form1 : Form
    {
        APMTCPClient _client = null;

        public Form1()
        {
            InitializeComponent();
            ResetBtn(true);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            _client = new APMTCPClient();
            _client.ClientConnectedEventHandle += ConnectedEventHandle;
            _client.ReceviceMsgEventHandle += DataEventHandle;
            _client.Connect(IPAddress.Parse(tbIP.Text), Convert.ToInt32(tbPort.Text), 4096);
            this.Text = "连接中";
            ResetBtn();
        }

        private void btnDisConnect_Click(object sender, EventArgs e)
        {
            _client.DisConnect();
            ResetBtn();
            this.btnConnect.Enabled = true;
            this.Text = "未连接";
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            this._client.BeginWrite(this.tbMsg.Text);
        }

        void ConnectedEventHandle()
        {
            this.Text = "已连接";
            this.btnDisConnect.Enabled = true;
            this.btnSend.Enabled = true;
        }

        void DataEventHandle(TCPClientState client)
        {
            MessageBox.Show(Encoding.UTF8.GetString(client.Buff));
        }

        void ResetBtn(bool state=false)
        {
            this.btnConnect.Enabled = state;
            this.btnDisConnect.Enabled = state;
            this.btnSend.Enabled = state;
        }
    }
}
