using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipleServer
{
    class PipleServer
    {
        private readonly NamedPipeServerStream m_pipe = new NamedPipeServerStream(
            "Echo", PipeDirection.InOut, -1, PipeTransmissionMode.Message,
            PipeOptions.Asynchronous | PipeOptions.WriteThrough);

        public PipleServer()
        {
            //异步接受一个客户端连接
            m_pipe.BeginWaitForConnection(ClientConnected, null);
        }

        private void ClientConnected(IAsyncResult result)
        {
            //一个客户端建立了连接，让我们接受另一个客户端
            new PipleServer();

            //接受客户端连接
            m_pipe.EndWaitForConnection(result);

            byte[] data = new byte[1000];
            m_pipe.BeginRead(data, 0, data.Length, GotRequest, data);
        }

        void GotRequest(IAsyncResult result)
        {
            //客户端向我们发送了一个请求，处理它
            Int32 byteRead = m_pipe.EndRead(result);
            byte[] data = (byte[])result.AsyncState;

            //我的示例服务器只是将所有字符更改为大写
            //但是，你可以将这些代码更改为任何计算机限制的操作
            Console.WriteLine("Client Request:" + Encoding.UTF8.GetString(data, 0, byteRead));
            data = Encoding.UTF8.GetBytes(
                Encoding.UTF8.GetString(data, 0, byteRead).ToUpper().ToCharArray());

            //将响应一步地发送给客户端
            m_pipe.BeginWrite(data, 0, data.Length, WriteDone, null);
        }

        void WriteDone(IAsyncResult result)
        {
            //响应已发送给了客户端
            m_pipe.EndWrite(result);
            m_pipe.Close();
        }
    }
}
