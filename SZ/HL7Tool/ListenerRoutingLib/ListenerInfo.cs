using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace MediII.Adapter.ListenerRouting
{
    public class ListenerInfo
    {
        /// <summary>
        /// 缓存大小
        /// </summary>
        public int BufferSize { get; set; }

        /// <summary>
        /// 监听端口
        /// </summary>
        public int LocalPort
        {
            get;
            set;
        }

        /// <summary>
        /// 最大连接
        /// </summary>
        public int MaxConnections
        {
            get;
            set;
        }

        /// <summary>
        /// 超时设置
        /// </summary>
        public int TimeOut
        {
            get;
            set;
        }

        public void LoadFromConfig()
        {
            this.LocalPort = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
            this.MaxConnections = Convert.ToInt32(ConfigurationManager.AppSettings["MaxConn"]);
            this.BufferSize = Convert.ToInt32(ConfigurationManager.AppSettings["BufferSize"]);
            this.TimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["ReviceTimeOut"]);
        }
    }
}
