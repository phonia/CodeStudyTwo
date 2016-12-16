using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace NetHelper
{
    public class NetHelper
    {
        /// <summary>
        /// 获取本地连接状态
        /// </summary>
        /// <returns></returns>
        public static bool LocalConnectionStatus()
        {
            Int32 dwFlag = new Int32();
            if (!InternetGetConnectedState(ref dwFlag, 0)) return false;
            return true;
        }

        /// <summary>
        /// 检车远程网络地址是否连通
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool Ping(String url)
        {
            Ping ping = new Ping();
            try
            {
                PingReply pr;
                pr = ping.Send(url);
                if (pr.Status != IPStatus.Success)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsFreePort(Int32 port)
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo("netstat", "-a");
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            String result = p.StandardOutput.ReadToEnd();
            if (result.IndexOf(Environment.MachineName.ToLower() + port.ToString()) >= 0)
                return false;
            else
                return true;
        }

        /**************************WinAPI********************************************/
        #region WinAPI

        [System.Runtime.InteropServices.DllImport("winInet.dll")]
        private static extern bool InternetGetConnectedState(ref Int32 dwFlag, int dwReserved);

        #endregion
    }
}
