using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace MediII.Adapter.BaseBiz
{
    /// <summary>
    /// 默认的实现,写日志
    /// </summary>
    public class DefaultBizBehavior:IBizBehavior
    {
        public void AfterProcess(string id,string messageID, string ack, bool successed)
        {
            string content = string.Format("消息:{0}已处理完毕，处理结果为:{1}\n\r回复消息为:{1}",messageID,successed,ack);
            //MediII.Common.LogHelper.LogDebug(content, Common.LogCatagories.BizBehavior);
        }

        public void BeforeProcess(string id, string messageID, string message)
        {
            string content = string.Format("消息:{0}已接收，内容为:{1}", messageID, message);
            //MediII.Common.LogHelper.LogDebug(content, Common.LogCatagories.BizBehavior);
        }
    }
}
