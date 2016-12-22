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
        private Action<byte[],IPEndPoint> _handleMessage;

        public UDP(String ip, String port)
            : this(ip, port, null)
        { }

        public UDP(String ip, String port, Action<byte[],IPEndPoint> handleMessage)
        {
            if (_udpClient == null)
            {
                _udpClient = new UdpClient(new IPEndPoint(IPAddress.Parse(ip), Convert.ToInt32(port)));
                _handleMessage = handleMessage;
            }
        }

        /// <summary>
        /// 发送信息(单播)
        /// </summary>
        /// <param name="remoteIp"></param>
        /// <param name="remotePort"></param>
        /// <param name="message"></param>
        public void BeginSend(String remoteIp,String remotePort,String message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            _udpClient.BeginSend(bytes, bytes.Length, new IPEndPoint(IPAddress.Parse(remoteIp), Convert.ToInt32(remotePort)), SendCallBack, _udpClient);
        }

        /// <summary>
        /// 子网广播
        /// </summary>
        /// <param name="message"></param>
        public void BeginSend(String remotePort,String message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            _udpClient.BeginSend(bytes, bytes.Length, new IPEndPoint(IPAddress.Broadcast, Convert.ToInt32(remotePort)), SendCallBack, _udpClient);
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

        /// <summary>
        /// 
        /// </summary>
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
                udpClient.BeginReceive(ReceviceCallBack, udpClient);
                if (_handleMessage != null && bytes != null && bytes.Length > 0)
                    _handleMessage(bytes,remoteIpEndPoint);
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

        /// <summary>
        /// 加入广多播组
        /// </summary>
        /// <param name="groupIp"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool JoinMulticastGroup(String groupIp,String port)
        {
            if (_udpClient != null)
            {
                _udpClient.JoinMulticastGroup(IPAddress.Parse(groupIp));
                return true;
            }
            return false;
        }

        /// <summary>
        /// 未完成功能
        /// </summary>
        /// <returns></returns>
        public bool EndMultcastGroup()
        {
            if (_udpClient != null)
            {

            }
            return false;
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
