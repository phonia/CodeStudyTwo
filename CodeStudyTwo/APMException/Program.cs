using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APMException
{
    class Program
    {
        static void Main(string[] args)
        {
            WebRequest webRequest = WebRequest.Create("http://www.baidu.com");
            webRequest.BeginGetResponse(ProcessWebResponse, webRequest);

            WebRequest ErrorRequest = WebRequest.Create("http://0.0.0.0");
            ErrorRequest.BeginGetResponse(ProcessWebResponse, ErrorRequest);
            Console.ReadLine();
        }

        static void ProcessWebResponse(IAsyncResult result)
        {
            WebRequest webRequest = (WebRequest)result.AsyncState;

            WebResponse webResponse = null;
            try
            {
                webResponse = webRequest.EndGetResponse(result);
                Console.WriteLine("Content length:" + webResponse.ContentLength);
            }
            catch (WebException we)
            {
                Console.WriteLine(we.GetType() + ":" + we.Message);
            }
            finally
            {
                if (webResponse != null) webResponse.Close();
            }
        }
    }
}
