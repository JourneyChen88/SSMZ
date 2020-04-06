using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace MediII.Adapter.BaseBiz
{
    /// <summary>
    /// Biz处理前后的行为
    /// </summary>
    public interface IBizBehavior
    {
        /// <summary>
        /// 消息处理之后
        /// </summary>
        /// <param name="messageID">消息ID</param>
        /// <param name="ack">回复信息</param>
        /// <param name="successed">true成功 false失败</param>
        void AfterProcess(string id,string messageID, string ack, bool successed);

        /// <summary>
        /// 消息处理之前
        /// </summary>
        /// <param name="messageID">消息ID</param>
        /// <param name="message">消息</param>
        void BeforeProcess(string id,string messageID,string message);
    }
}
