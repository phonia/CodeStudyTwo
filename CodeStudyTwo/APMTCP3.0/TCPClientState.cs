using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace APMTCP3._0
{
    public class TCPClientBase:IDisposable
    {
        public TcpClient Client { private set; get; }
        public NetworkStream NetworkStream { private set; get; }
        public EndPoint RemoteEndPoint { private set; get; }
        public byte[] CacheSync { get; set; }
        public byte[] CacheAsync { private set; private get; }

        private object _lockObject = new object();
        private Int32 _finger = 0;

        private TCPClientBase() { }

        public static TCPClientBase GetInstance(TcpClient client, int buffSize)
        {
            if (client != null && client.Connected && buffSize > 0)
            {
                TCPClientBase tcp = new TCPClientBase();
                tcp.Client = client;
                tcp.NetworkStream = tcp.Client.GetStream();
                tcp.CacheSync = new byte[buffSize];
                tcp.CacheAsync = new byte[buffSize * 10];
                tcp.RemoteEndPoint = client.Client.RemoteEndPoint;
                return tcp;
            }
            else
            {
                throw new ArgumentNullException(client.GetType().ToString());
            }
        }

        public byte[] ReadDataAsync(int length = 0)
        {
            lock (_lockObject)
            {
                if (length <= 0)
                {
                    if (_finger > 0)
                    {
                        byte[] ret = new byte[_finger];
                        Array.Copy(CacheAsync, ret, _finger);
                        _finger = 0;
                        Array.Clear(CacheAsync, 0, _finger);
                        return ret;
                    }
                    else
                        throw new CustomTCPException("超出数组范围");
                }
                else
                {
                    if (_finger <= length)
                    {
                        byte[] ret = new byte[_finger];
                        Array.Copy(CacheAsync, ret, _finger);
                        _finger = 0;
                        Array.Clear(CacheAsync, 0, _finger);
                        return ret;
                    }
                    else
                    {
                        byte[] ret = new byte[length];
                        Array.Copy(CacheAsync, ret, length);
                        byte[] temp = new byte[_finger - length];
                        Array.Copy(CacheAsync, _finger, temp, 0, _finger - length);
                        Array.Clear(CacheAsync, 0, CacheAsync.Length);
                        Array.Copy(temp, CacheAsync, _finger - length);

                        _finger -= length;
                        return ret;
                    }
                }
            }
        }

        public void WirteDataAsync(int length=0)
        {
            lock (_lockObject)
            {
                if (length <= 0)
                {
                    Array.Copy(CacheSync, 0, CacheAsync, _finger, CacheSync.Length);
                    _finger += CacheSync.Length;
                    Array.Clear(CacheSync, 0, CacheSync.Length);
                }
                else
                {
                    Array.Copy(CacheSync, 0, CacheAsync, _finger, length);
                    _finger += length;
                    Array.Clear(CacheSync, 0, length);
                }
            }
        }

        #region IDisposable 成员

        public void Close()
        {
            if (NetworkStream != null)
                NetworkStream.Close();
            if (Client != null)
                Client.Client.Close();
        }

        #endregion
    }
}
