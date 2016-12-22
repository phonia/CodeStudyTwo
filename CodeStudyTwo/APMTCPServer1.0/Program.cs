using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace APMTCPServer1._0
{
    /***
     * 初步的APMTCPServer
     * 1、采用apm异步方式，包括监听、获取、读写数据
     * 2、只简单实现功能，不做任何的代码处理
     * **/
    class Program
    {
        static void Main(string[] args)
        {
            TCPServer tcpServer = new TCPServer();
            ThreadStart ts = new ThreadStart(tcpServer.Start);
            Thread myThread = new Thread(ts);
            myThread.Start();
            Console.ReadLine();
        }
    }

    class TCPServer
    {
        List<TCPClientState> _clients = null;
        object _lockObject = new object();

        public void Start()
        {
            IPAddress[] ips = Dns.GetHostAddresses("");
            Int32 port = 10086;

            TcpListener listener = new TcpListener(ips[ips.Length - 1], port);
            listener.Start();
            listener.BeginAcceptTcpClient(new AsyncCallback(HandleTCPClientAccpet), listener);
        }

        public void HandleTCPClientAccpet(IAsyncResult result)
        {
            Console.WriteLine("Time");
            TcpListener listener = (TcpListener)result.AsyncState;
            TcpClient client = listener.EndAcceptTcpClient(result);
            try
            {
                TCPClientState instance = new TCPClientState() { Client = client, RemoteIP = client.Client.RemoteEndPoint, Buff = new byte[4096] };
                lock (_lockObject)
                {
                    if (_clients == null) _clients = new List<TCPClientState>();
                    _clients.Add(instance);
                }
                NetworkStream networkStream = client.GetStream();
                instance.NetWorkStream = client.GetStream();
                networkStream.BeginRead(instance.Buff, 0, 4096, new AsyncCallback(HandleReceviedData), instance);
            }
            catch (Exception ex)
            { }
            finally
            { }
            listener.BeginAcceptTcpClient(new AsyncCallback(HandleTCPClientAccpet), listener);
        }

        public void HandleReceviedData(IAsyncResult result)
        {
            try
            {
                TCPClientState instance = (TCPClientState)result.AsyncState;
                int count = instance.NetWorkStream.EndRead(result);
                if(count>0)
                    Console.WriteLine(Encoding.UTF8.GetString(instance.Buff).Trim('\0'));

                instance.NetWorkStream.BeginRead(instance.Buff, 0, 4096, HandleReceviedData, instance);
            }
            catch (Exception ex)
            { }
            finally
            { }
        }
    }

    class TCPClientState
    {
        public TcpClient Client { get; set; }
        public EndPoint RemoteIP { get; set; }
        public byte[] Buff { get; set; }
        public NetworkStream NetWorkStream { get; set; }
    }
}
