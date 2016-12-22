using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleTCPClient
{
    /**
     * TCP客户端
     * 1、不显示使用本机的地址与端口号
     * 2、只发送、不接收数据
     * 3、只处理连接异常
     * 4、往复使用
     * 
     * **/
    class Program
    {
        static void Main(string[] args)
        {
            String sendString = null;
            byte[] sendData = null;
            TcpClient client = null;
            NetworkStream stream = null;

            IPAddress[] ips = Dns.GetHostAddresses("");
            Int32 remotePort = 10086;

            while (true)
            {
                sendString = "HelloWrold!";
                sendData = Encoding.UTF8.GetBytes(sendString);
                client = new TcpClient();

                try
                {
                    client.Connect(ips[ips.Length - 1], remotePort);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("连接超时！");
                    Console.WriteLine(ex.Message);
                }

                stream = client.GetStream();
                stream.Write(sendData, 0, sendData.Length);
                stream.Close();
                client.Close();
            }
        }
    }
}
