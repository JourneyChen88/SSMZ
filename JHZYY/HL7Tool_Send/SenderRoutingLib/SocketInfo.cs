using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SenderRoutingLib
{
    /// <summary>
    /// Socke客户端配置
    /// </summary>
    public class SocketClientInfo
    {
        /// <summary>
        /// 服务端IP地址
        /// </summary>
        public static string Address { get; set; }

        /// <summary>
        /// 服务端端口
        /// </summary>
        public static int Port { get; set; }

        /// <summary>
        /// 接收的缓冲大小
        /// </summary>
        public static int BufferSize { get; set; }

        /// <summary>
        /// 接收的超时时间，单位是毫秒
        /// </summary>
        public static int ReviceTimeOut { get; set; }

        static SocketClientInfo()
        {
            Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
            Address = ConfigurationManager.AppSettings["Address"];
            BufferSize = Convert.ToInt32(ConfigurationManager.AppSettings["BufferSize"]);
            ReviceTimeOut = string.IsNullOrEmpty(ConfigurationManager.AppSettings["ReviceTimeOut"])
                ?60000:Convert.ToInt32(ConfigurationManager.AppSettings["ReviceTimeOut"]);
        }
    }
}
