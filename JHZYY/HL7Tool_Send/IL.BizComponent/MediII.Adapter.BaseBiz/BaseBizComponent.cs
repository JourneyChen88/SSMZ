using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapi.Model.V24.Segment;
using System.Data.Entity;
using NHapi.Model.V24.Message;
using MediII.Common;
using NHapi.Base.Util;
using System.ComponentModel.Composition;

namespace MediII.Adapter.BaseBiz
{
    [Export]
    public abstract class BaseBizComponent : IBizComponent
    {
        protected PipeParser parser = new PipeParser();

        static IBizBehavior BizBehavior { get; set; }

        static BaseBizComponent()
        {
            string bizBehavior = System.Configuration.ConfigurationManager.AppSettings["BizBehavior"];
            if (string.IsNullOrEmpty(bizBehavior))
            {
                BizBehavior = new DefaultBizBehavior();
            }
            else
            {
                string[] ar = bizBehavior.Split(',');
                string assemblyName = ar[0];
                string typeName = ar[1];
                try
                {
                    BizBehavior = Activator.CreateInstance(assemblyName, typeName).Unwrap() as IBizBehavior;
                }
                catch
                {
                    BizBehavior = new DefaultBizBehavior();
                }
            }
        }

        /// <summary>
        /// 发送应用程序名
        /// </summary>
        static readonly string SEDING_APPLICATION = System.Configuration.ConfigurationManager.AppSettings["SedingApplication"];               

        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="message">解析后消息的抽象类型</param>
        public abstract string DoProcess(IMessage message);

        /// <summary>
        ///实现处理的接口
        /// </summary>
        /// <param name="message">传入的消息字符</param>
        /// <returns></returns>
        public string Process(string message)
        {
            IMessage m = null;
            try
            {
                m = parser.Parse(message);        
            }
            catch(Exception ex)
            {
                //MediII.Common.LogHelper.LogError(string.Format("消息消息失败,传入消息为:\n\r{0}",message),LogCatagories.AdapterBiz);
                string sendingApp = System.Configuration.ConfigurationManager.AppSettings["SedingApplication"];
                string recvApp = "MediII";
                string ackMsg = MediII.Common.MessageHelper.SetACK("ACK", "", "", sendingApp, sendingApp,
                                                   recvApp, recvApp, "123456789", ex.Message);
                return ackMsg;
            }
            ISegment msh = (ISegment)m.GetStructure("MSH");
            string messageID = Terser.Get(msh, 10, 0, 1, 1);
            string guid = MediII.Common.GUIDHelper.NewGUID();
            OnProcessing(guid,messageID, message);            
            string ack = DoProcess(m);
            OnProcessed(guid,parser.GetAckID(ack), ack, true); 
            return ack;
        }
        
        /// <summary>
        /// 保存回复
        /// </summary>
        /// <param name="ack"></param>
        private void OnProcessed(string id,string messageID,string ack,bool successed)
        {
            BizBehavior.AfterProcess(id,messageID, ack, successed);   
        }

        /// <summary>
        /// 保存接受信息
        /// </summary>
        /// <param name="message"></param>
        private void OnProcessing(string id,string messageID,string message)
        {
            BizBehavior.BeforeProcess(id,messageID, message);
        }    

        /// <summary>
        ///设置一般回复(ACK)
        /// </summary>
        /// <param name="oEntity">原消息体</param>
        /// <param name="messageType">消息类型</param>
        /// <param name="trriggerEvent">触发事件</param>
        /// <param name="messageStruct">消息结构</param>
        /// <param name="ErrMSG">错误信息</param>
        /// <returns>ACK字符</returns>
        public string SetACK(dynamic oEntity, string messageType, string trriggerEvent, string messageStruct = "", string errMSG = "",dynamic ack = null)
        {
            //ACK ack = new ACK();
            if (ack == null)
                ack = new ACK();

            return MediII.Common.MessageHelper.SetACK(messageType,trriggerEvent,messageStruct,SEDING_APPLICATION,SEDING_APPLICATION,
                                                   oEntity.MSH.SendingApplication.NamespaceID.Value, oEntity.MSH.SendingFacility.NamespaceID.Value, oEntity.MSH.MessageControlID.Value,
                                                   errMSG,ack);            
        }
    }
}
