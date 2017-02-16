using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;

namespace APMTCP3._0
{
    public class APMTCPClient
    {
        public bool IsConnected { private set; get; }
        public Int32 BuffSize { private set; get; }

        private TCPClientBase _client = null;


        public APMTCPClient() {
            IsConnected = false;
        }

        public void Connet(IPAddress ip, Int32 port, int buffSize = 8192)
        {
            this.BuffSize = buffSize;
            TcpClient client = new TcpClient();
            client.BeginConnect(ip, port, ConnectCallBack, client);
        }

        public void BeginWrite(String message)
        { }

        public void DisConnect()
        { }

        void ConnectCallBack(IAsyncResult result)
        {
            TcpClient client = (TcpClient)result.AsyncState;
            try
            {
                _client = TCPClientBase.GetInstance(client, this.BuffSize);
                client.EndConnect(result);
                IsConnected = true;
                //处理连接事件 未完成
                //client.GetStream().BeginRead(_client.CacheAsync
                
            }
            catch (Exception ex)
            { }
            finally
            { }
        }

        void ReadCallBack(IAsyncResult result)
        { }

        void WriteCallBack(IAsyncResult result)
        { }
    }
}
