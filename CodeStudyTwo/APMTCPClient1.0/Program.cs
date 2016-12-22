using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace APMTCPClient1._0
{
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

            int i = 0;

            while (i<10)
            {
                i++;
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
