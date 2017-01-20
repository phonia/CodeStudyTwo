using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace APMTCP3._0
{
    public class APMTCPServer:IDisposable
    {
        public bool IsRunning { private set; get; }
        public Int32 BufferSize { private set; get; }
        public List<TCPClientBase> Clients { private set; get; }
        public bool IsSync { private set; get; }

        private object _lockObject = new object();
        private TcpListener _Listener = null;

        public APMTCPServer(IPAddress ip, Int32 port,bool isSync=true, int buffSize = 4096)
        {
            _Listener = new TcpListener(new IPEndPoint(ip, port));
            IsRunning = false;
            this.BufferSize = buffSize;
            this.IsSync = isSync;
            this.Clients = new List<TCPClientBase>();
        }

        public void StartServer()
        {
            try
            {
                IsRunning = true;
                _Listener.Start();
                _Listener.BeginAcceptTcpClient(AcceptedClientCallBack, _Listener);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { }
        }

        protected void AcceptedClientCallBack(IAsyncResult result)
        {
            try
            {
                TcpListener listener = (TcpListener)result.AsyncState;
                TcpClient client = listener.EndAcceptTcpClient(result);
                TCPClientBase tcpClientBase = TCPClientBase.GetInstance(client, this.BufferSize);
                lock (_lockObject)
                {
                    if (this.Clients == null) this.Clients = new List<TCPClientBase>();
                    if (!this.Clients.Contains(tcpClientBase))
                        this.Clients.Add(tcpClientBase);
                }
                tcpClientBase.NetworkStream.Flush();
                tcpClientBase.NetworkStream.BeginRead(tcpClientBase.CacheSync, 0, tcpClientBase.CacheSync.Length, ReadCallBack, tcpClientBase);

                if (IsRunning)
                {
                    listener.BeginAcceptTcpClient(AcceptedClientCallBack, result.AsyncState);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { }
        }

        protected void ReadCallBack(IAsyncResult result)
        {
            try
            {
                TCPClientBase tcpClientBase = (TCPClientBase)result.AsyncState;
                Int32 count = tcpClientBase.NetworkStream.EndRead(result);
                if (count > 0)
                {
                    //
                }
                else
                {
                    //
                }

                if (IsRunning)
                {
                    tcpClientBase.NetworkStream.Flush();
                    tcpClientBase.NetworkStream.BeginRead(tcpClientBase.CacheSync, 0, tcpClientBase.CacheSync.Length, ReadCallBack, tcpClientBase);
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
                    Clients.Clear();
                }
                _Listener.Stop();
            }
        }

        public void Send(byte[] bytes,TCPClientBase client)
        {
            if (client != null && client.Client.Connected && client.NetworkStream != null)
            {

            }
        }

        protected void WriteCallBack(IAsyncResult result)
        { }

        #region IDisposable 成员

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
