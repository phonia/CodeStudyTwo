using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace APMTCPServer2._0
{
    public partial class Form1 : Form
    {
        APMTCPServer _server = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_server == null)
            {
                _server = new APMTCPServer(IPAddress.Parse(tbIp.Text), Convert.ToInt32(tbPort.Text));
                _server.HandleReceviedMessageEvent += HandelReceviedMessage;
                this.tbIp.ReadOnly = true;
                this.tbPort.ReadOnly = true;
            }
            _server.StartServer();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_server != null)
                _server.EndServer();
        }

        void HandelReceviedMessage(TCPClientState tcpClientState)
        {
            String message = Encoding.UTF8.GetString(tcpClientState.Buff);
            //lbClient.Items.Contains(tcpClientState.Client.Client.RemoteEndPoint.ToString());
            //lbData.Items.Add(message);
            Action actionc = () => {
                if (!lbClient.Items.Contains(tcpClientState.Client.Client.RemoteEndPoint.ToString()))
                {
                    lbClient.Items.Add(tcpClientState.Client.Client.RemoteEndPoint.ToString());
                }
            };

            Action actionm = () => {
                lbData.Items.Add(message);
            };
            this.lbClient.BeginInvoke(actionc);
            this.lbData.BeginInvoke(actionm);
            //this.lbClient.BeginInvoke(
        }
    }
}
