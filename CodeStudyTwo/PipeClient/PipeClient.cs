using System;
using System.IO.Pipes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeClient
{
    public class PipeClient 
    {
        private readonly NamedPipeClientStream m_pipe;

        public PipeClient(String serverName, String message)
        {
            m_pipe = new NamedPipeClientStream(serverName, "Echo",
                PipeDirection.InOut, PipeOptions.Asynchronous | PipeOptions.WriteThrough);
            m_pipe.Connect();//必须先连接才能设置
            m_pipe.ReadMode = PipeTransmissionMode.Message;

            //异步的将数据发送给服务器
            byte[] outPut = Encoding.UTF8.GetBytes(message);
            m_pipe.BeginWrite(outPut, 0, outPut.Length, WriteDone, null);
        }

        void WriteDone(IAsyncResult result)
        {
            //数据已经发送给了服务器
            m_pipe.EndWrite(result);

            //异步的读取服务器的响应
            byte[] data = new Byte[1000];
            m_pipe.BeginRead(data, 0, data.Length, GotResponse, data);
        }

        void GotResponse(IAsyncResult result)
        {
            //服务器已经响应，显示响应，并关闭出站连接
            Int32 byteRead = m_pipe.EndRead(result);

            byte[] data = (byte[])result.AsyncState;
            Console.WriteLine("Server response:" + Encoding.UTF8.GetString(data, 0, byteRead));
            m_pipe.Close();
        }
    }
}
