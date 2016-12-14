using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APMByTask
{
    class Program
    {
        static void Main(string[] args)
        {
            WebRequest webRequest = WebRequest.Create("http://www.baidu.com");
            Task.Factory.FromAsync<WebResponse>(
                webRequest.BeginGetResponse, webRequest.EndGetResponse,
                null, TaskCreationOptions.None)
                .ContinueWith(task =>
                {
                    WebResponse webResponse = null;
                    try
                    {
                        webResponse = task.Result;
                        Console.WriteLine("Content length:" + webResponse.ContentLength);
                    }
                    catch (AggregateException ae)
                    {
                        if (ae.GetBaseException() is WebException)
                            Console.WriteLine("Failed:" + ae.GetBaseException().Message);
                        else
                            throw;
                    }
                    finally
                    {
                        if (webResponse != null) webResponse.Close();
                    }
                });
        }
    }
}
