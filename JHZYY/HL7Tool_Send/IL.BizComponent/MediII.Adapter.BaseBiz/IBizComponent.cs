using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHapi.Base.Model;

namespace MediII.Adapter.BaseBiz
{
    public interface IBizComponent
    {
        /// <summary>
        /// 消息到达处理
        /// </summary>
        /// <param name="message">处理消息的抽象接口</param>
        /// <param name="ackMessage">回复消息</param>
        /// <returns>1 成功，0 失败</returns>
        string Process(string message);
    }
}
