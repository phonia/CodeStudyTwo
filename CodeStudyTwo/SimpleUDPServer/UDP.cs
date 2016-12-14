using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace SimpleUDPServer
{
    public class UDP
    {
        private UdpClient _receviceUdpClient = null;
        private bool _isRecevicing = true;

        public void BeginRecevice(String port)
        {
            _receviceUdpClient = null;
            if (_receviceUdpClient == null)
            {
                IPAddress[] ips = Dns.GetHostAddresses("");
                IPEndPoint ipEndPoint = new IPEndPoint(ips[1], Convert.ToInt32(port));
                _receviceUdpClient = new UdpClient(ipEndPoint);
                _receviceUdpClient.BeginReceive(ReceviceCallBack, _receviceUdpClient);
            }
        }

        public void ReceviceCallBack(IAsyncResult result)
        {
            if (result.IsCompleted&&_isRecevicing)
            {
                UdpClient udpClient = (UdpClient)result.AsyncState;
                IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] bytes = udpClient.EndReceive(result, ref remoteIpEndPoint);
                udpClient.BeginReceive(ReceviceCallBack, null);
                MessageBox.Show(Encoding.UTF8.GetString(bytes));
            }
        }

        public void EndRecevice()
        {
            try
            {
                _isRecevicing = false;
                if (_receviceUdpClient != null)
                {
                    _receviceUdpClient.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Send()
        { }
    }
}
