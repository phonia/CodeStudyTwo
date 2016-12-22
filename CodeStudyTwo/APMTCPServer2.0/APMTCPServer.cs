using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace APMTCPServer2._0
{
    public class APMTCPServer
    {

    }

    public class TCPClientState
    {
        public TcpClient Client { get; set; }
        public NetworkStream NetWorkStream { private set; get; }
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
    }
}
