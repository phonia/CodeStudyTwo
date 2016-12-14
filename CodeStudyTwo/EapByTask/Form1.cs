using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EapByTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //System.Net.WebClient 类支持基于事件的异步模式EAP
            WebClient wc = new WebClient();

            //创建TaskCompletionSource从底层的Task对象
            var tcs = new TaskCompletionSource<String>();

            //一个String下载好之后，WebClient对象引发
            //DownloadStringCompleted事件，会造成我们的ProcessString方法的调用
            wc.DownloadStringCompleted += (se, ea) => {
                //这些代码总是在GUI线程上执行：设置Task的状态
                if (ea.Cancelled) tcs.SetCanceled();
                else if (ea.Error != null) tcs.SetException(ea.Error);
                else tcs.SetResult(ea.Result);
            };


            //让Task继续以下Task，在一个消息框中显示结果
            //注意：为了让以下代码在GUI县城上执行，ExecuteSynchronously标志总是必须的；
            //如果没有这个标志，代码会在一个线程池程上运行
            tcs.Task.ContinueWith(t =>
            {
                try {
                    MessageBox.Show(t.Result);
                }
                catch (AggregateException ae)
                {
                    MessageBox.Show(ae.GetBaseException().Message);
                }
            }, TaskContinuationOptions.ExecuteSynchronously);

            //
            wc.DownloadStringAsync(new Uri("http://www.baidu.com"));
            base.OnClick(e);
        }
    }
}
