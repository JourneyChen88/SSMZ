using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Collections.Generic;

namespace SenderRoutingWin
{
    public class MessageHelper
    {
        EndpointAddress address = null;
        Binding Binding = null;
 
        public MessageHelper(string path)
        {
            string _path = path.Trim();
            if (_path.Length > 0)
            {
                if (_path.StartsWith("http://"))
                {
                    Binding = new WSHttpBinding();
                    ((dynamic)Binding).Security.Mode = SecurityMode.None; 
                }
                else if (_path.StartsWith("net.tcp://"))
                {
                    Binding = new NetTcpBinding();
                    ((dynamic)Binding).Security.Mode = SecurityMode.None; 
                }
                address = new EndpointAddress(_path);
            }
        }

        public string GetProccess(string queueId)
        {
            if ((address != null) && (Binding != null))
            {
                using (ChannelFactory<ISendMessageService> channel = new ChannelFactory<ISendMessageService>(Binding, address))
                {
                    ISendMessageService service = channel.CreateChannel();
                    return service.GetProccess(queueId);
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public int SendMessage(string message, string MQName, bool needResponse = true)
        {
            if ((address != null) && (Binding != null))
            {
                using (ChannelFactory<ISendMessageService> channel = new ChannelFactory<ISendMessageService>(Binding, address))
                {
                    ISendMessageService service = channel.CreateChannel();
                    return service.SendMessage(message, MQName, needResponse);
                }
            }
            else
            {
                return -1;
            }
        }

        public int SendMFNMessage(string message)
        {
            if ((address != null) && (Binding != null))
            {
                using (ChannelFactory<ISendMessageService> channel = new ChannelFactory<ISendMessageService>(Binding, address))
                {
                    ISendMessageService service = channel.CreateChannel();
                    return service.SendMessageBackACK(message);
                }
            }
            else
            {
                return -1;
            }
        }

        public int SendMessageForMsgID(string message, string MQName, out string msgId)
        {
            msgId = string.Empty;
            if ((address != null) && (Binding != null))
            {
                using (ChannelFactory<ISendMessageService> channel = new ChannelFactory<ISendMessageService>(Binding, address))
                {
                    ISendMessageService service = channel.CreateChannel();
                    return service.SendMessageForMsgID(message, MQName, out msgId);
                }
            }
            else
            {
                return -1;
            }
        }

        public IList<string[]> GetQueuesCount()
        {
            
            if ((address != null) && (Binding != null))
            {
                try
                {
                    using (ChannelFactory<ISendMessageService> channel = new ChannelFactory<ISendMessageService>(Binding, address))
                    {
                        ISendMessageService service = channel.CreateChannel();
                        return service.GetQueuesCount();
                    }
                }
                catch (Exception ex)
                {
                    string str = ex.Message;
                    return new List<string[]>();
                }
            }
            else
            {
                return new List<string[]>();
            }
        }
    }
}
