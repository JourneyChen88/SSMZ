using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Collections.Generic;

namespace SenderRoutingWin
{
    [ServiceContract]
    public interface ISendMessageService
    {
        [OperationContract]
        int SendMessage(string message, string MQName, bool needResponse = true);

        [OperationContract]
        string GetProccess(string queueId);

        [OperationContract]
        int SendMessageForMsgID(string message, string MQName, out string msgId);

        [OperationContract]
        IList<string[]> GetQueuesCount();

        [OperationContract]
        int SendMessageBackACK(string message);
    }
}