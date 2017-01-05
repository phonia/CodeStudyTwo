using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace APMTCPClient2._0
{
    public class APMTCPClient
    {
        public Int32 BuffSize { private set; get; }
        public Action ClientConnectedEventHandle { get; set; }
        public Action<TCPClientState> ReceviceMsgEventHandle { get; set; }
        private TCPClientState _client = null;
        private bool IsConnected = false;

        public void Connect(IPAddress ip,Int32 port,int bufferSize)
        {
            this.BuffSize = bufferSize;
            TcpClient client = new TcpClient();
            client.BeginConnect(ip, port, ConnectCallBack, client);
        }

        public void BeginWrite(String message)
        {
            if (!IsConnected) return;
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                _client.NetWorkStream.BeginWrite(data, 0, data.Length, WirteCallBack, _client);
            }
            catch (Exception ex)
            { }
            finally
            { }
        }

        public void DisConnect()
        {
            IsConnected = false;
            if (_client != null)
            {
                _client.Close();
            }
        }

        void ConnectCallBack(IAsyncResult result)
        {
            TcpClient client = (TcpClient)result.AsyncState;
            try
            {
                _client = TCPClientState.GetInstanceOrNull(client, BuffSize);
                client.EndConnect(result);
                IsConnected = true;
                if (ClientConnectedEventHandle != null) ClientConnectedEventHandle();
                client.GetStream().BeginRead(_client.Buff, 0, BuffSize, ReadCallBack, _client);
            }
            catch (Exception ex)
            { }
            finally
            { }
        }

        void ReadCallBack(IAsyncResult result)
        {
            TCPClientState client = (TCPClientState)result.AsyncState;
            try
            {
                int count = client.NetWorkStream.EndRead(result);
                if (ReceviceMsgEventHandle != null&&count>0)
                {
                    ReceviceMsgEventHandle(client);
                }
                if (_client.Client.Connected&&IsConnected)
                {
                    client.NetWorkStream.BeginRead(client.Buff, 0, BuffSize, ReadCallBack, _client);
                }
            }
            catch (Exception ex)
            { }
            finally
            { }
        }

        void WirteCallBack(IAsyncResult result)
        {
            TCPClientState client = (TCPClientState)result.AsyncState;
            client.NetWorkStream.EndWrite(result);
        }
    }

    public class TCPClientState
    {
        public TcpClient Client { get; set; }
        public NetworkStream NetWorkStream { get; private set; }
        public byte[] Buff { get; set; }
        public EndPoint RemoteEndPoint { get; private set; }

        private TCPClientState()
        { }

        /// <summary>
        /// return instance or null
        /// </summary>
        /// <param name="client"></param>
        /// <param name="buffSize"></param>
        /// <returns></returns>
        public static TCPClientState GetInstanceOrNull(TcpClient client, int buffSize)
        {
            if (client != null && client.Connected && buffSize > 0)
            {
                TCPClientState tcp = new TCPClientState();
                tcp.Client = client;
                tcp.NetWorkStream = tcp.Client.GetStream();
                tcp.Buff = new byte[buffSize];
                tcp.RemoteEndPoint = client.Client.RemoteEndPoint;
                return tcp;
            }
            return null;
        }

        public void Close()
        {
            if (NetWorkStream != null)
                NetWorkStream.Close();
            if (Client != null)
                Client.Client.Close();
        }
    }
}
