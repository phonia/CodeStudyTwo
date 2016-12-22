using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleTCPServer
{
    /**
     * TCP服务端
     * 1、使用本地ip和port监听
     * 2、往复使用
     * 3、同步监听
     * 4、只接收、不发送数据
     * 5、不做异常错误处理
     * **/
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = null;
            NetworkStream stream = null;
            byte[] buffer = null;

            IPAddress[] ips = Dns.GetHostAddresses("");
            Int32 port = 10086;

            TcpListener listener = new TcpListener(ips[ips.Length - 1], port);

            listener.Start();

            while (true)
            {
                client = listener.AcceptTcpClient();
                buffer = new byte[client.ReceiveBufferSize];
                stream = client.GetStream();
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                client.Close();

                Console.WriteLine(Encoding.UTF8.GetString(buffer).Trim('\0'));
            }
        }
    }
}
