using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleUDPClient
{
    public class UDP:IDisposable
    {
        private UdpClient _udpClient = null;
        private bool _isRecevicing = false;

        public UDP(String ip,String port)
        {
            if (_udpClient == null)
                _udpClient = new UdpClient(new IPEndPoint(IPAddress.Parse(ip), Convert.ToInt32(port)));
        }

        public void BegingSend(String remoteIp,String remotePort,String message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            _udpClient.BeginSend(bytes, bytes.Length, new IPEndPoint(IPAddress.Parse(remoteIp), Convert.ToInt32(remotePort)), SendCallBack, _udpClient);
        }

        void SendCallBack(IAsyncResult result)
        {
            if (result.IsCompleted)
            {
                UdpClient udpClient = (UdpClient)result.AsyncState;
                IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any,0);
                udpClient.EndReceive(result,ref remoteIpEndPoint );
            }
        }

        public void BeginRecevice()
        {
            _isRecevicing = true;
            if (_udpClient != null)
            {
                _udpClient.BeginReceive(ReceviceCallBack, _udpClient);
            }
        }

        public void ReceviceCallBack(IAsyncResult result)
        {
            if (result.IsCompleted && _isRecevicing)
            {
                UdpClient udpClient = (UdpClient)result.AsyncState;
                IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                //if (udpClient.Client.RemoteEndPoint != null)
                //    MessageBox.Show(udpClient.Client.RemoteEndPoint.ToString());
                byte[] bytes = udpClient.EndReceive(result, ref remoteIpEndPoint);
                udpClient.BeginReceive(ReceviceCallBack, null);
            }
        }

        public void EndRecevice()
        {
            try
            {
                _isRecevicing = false;
                if (_udpClient != null)
                {
                    _udpClient.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            try
            {
                this._udpClient.Close();
                GC.Collect();
            }
            catch (Exception ex)
            { }
        }

        #endregion
    }
}
