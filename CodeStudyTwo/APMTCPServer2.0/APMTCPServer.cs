using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace APMTCPServer2._0
{
    public class APMTCPServer:IDisposable
    {
        private object                  _lockObject         = new object();

        private TcpListener             _Listener           = null;

        public bool                    IsRunning   { get; private set; }

        public  int                     BufferSize  { get; private set; }

        public List<TCPClientState>     Clients     { get; private set; }

        public Action<TCPClientState>   HandleReceviedMessageEvent;

        public APMTCPServer(IPAddress ip,Int32 port,int bufferSize=4096)
        {
            if (_Listener == null)
            {
                _Listener           = new TcpListener(new IPEndPoint(ip, port));
                IsRunning           = false;
                this.BufferSize     = bufferSize;
            }
        }

        public void StartServer()
        {
            try
            {
                _Listener.Start();
                IsRunning = true;
                _Listener.BeginAcceptTcpClient(AccepetClientCallBack, _Listener);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { }
        }

        public void AccepetClientCallBack(IAsyncResult result)
        {
            try
            {
                TcpListener listener = (TcpListener)result.AsyncState;
                TcpClient client = listener.EndAcceptTcpClient(result);
                TCPClientState tcpClientState = TCPClientState.GetInstanceOrNull(client, BufferSize);
                if (tcpClientState == null) throw new ArgumentNullException();
                lock (_lockObject)
                {
                    if (!Clients.Contains(tcpClientState))
                        Clients.Add(tcpClientState);
                }
                tcpClientState.NetWorkStream.Flush();
                tcpClientState.NetWorkStream.BeginRead(tcpClientState.Buff, 0, tcpClientState.Buff.Length, ReadDataCallBack, tcpClientState);

                if (IsRunning)
                {
                    listener.BeginAcceptTcpClient(AccepetClientCallBack, result.AsyncState);
                }
            }
            catch (Exception ex)
            { }
            finally
            { }
        }

        public void ReadDataCallBack(IAsyncResult result)
        {
            try
            {
                TCPClientState tcpClientState = (TCPClientState)result.AsyncState;
                Int32 count = tcpClientState.NetWorkStream.EndRead(result);
                if (count > 0)
                {
                    if (HandleReceviedMessageEvent != null)
                    {
                        HandleReceviedMessageEvent(tcpClientState);
                    }
                }

                if (IsRunning)
                {
                    tcpClientState.NetWorkStream.Flush();
                    tcpClientState.NetWorkStream.BeginRead(tcpClientState.Buff, 0, tcpClientState.Buff.Length, ReadDataCallBack, tcpClientState);
                }
            }
            catch (Exception ex)
            { }
            finally
            { }
        }

        public void EndServer()
        {
            if (_Listener != null)
            {
                IsRunning = false;
                if (Clients != null && Clients.Count > 0)
                {
                    Clients.ForEach(it => it.Close());
                }
                _Listener.Stop();
            }
        }

        public void BeginSend(byte[] bytes,TCPClientState client)
        {
            if (client != null && client.Client.Connected && client.NetWorkStream != null)
            {
                client.NetWorkStream.Flush();
                client.NetWorkStream.BeginWrite(bytes, 0, bytes.Length, WriteDataCallBack, client);
            }
        }

        public void WriteDataCallBack(IAsyncResult result)
        {
            try
            {
                TCPClientState tcpClientState = (TCPClientState)result.AsyncState;
                tcpClientState.NetWorkStream.EndWrite(result);
            }
            catch (Exception ex)
            { }
            finally
            { }
        }

        #region IDisposable 成员

        public void Dispose()
        {

        }

        #endregion
    }

    public class TCPClientState
    {
        public TcpClient        Client          { get; set; }
        public NetworkStream    NetWorkStream   { get; private set; }
        public byte[]           Buff            { get; set; }
        public EndPoint         RemoteEndPoint  { get; private set; }

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
