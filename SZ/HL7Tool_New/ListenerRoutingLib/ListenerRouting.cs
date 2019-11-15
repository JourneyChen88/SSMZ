using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using MediII.Adapter.BaseBiz;
using NHapi.Base.Parser;
using NHapi.Model.V24.Message;
using NHapi.Base.Model;
using MediII.Adapter.BaseBiz;
using NHapi.Model.V24.Segment;
using System.Diagnostics;
using MediII.Net.Common;
using System.Data;
using ListenerRoutingLib;
using System.Configuration;
using static MediII.Net.Common.Logger;

namespace MediII.Adapter.ListenerRouting
{
    public class ListenerRouting
    {
        private SqlServerLink sqlHelp = new SqlServerLink();
        public string tableName = ConfigurationManager.AppSettings["PaibanTableName"];
        /// <summary>
        /// 开始处理
        /// </summary>
        /// <param name="listenerInfo"></param>
        public void BeginProcess(ListenerInfo listenerInfo)
        {
            SocketHelper.ConfigInfo = listenerInfo;
            SocketHelper.MessageReviced += new EventHandler<MessageRevicedEventArgs>(SocketHelper_MessageReviced);
            try
            {
                SocketHelper.BeginListen();
            }
            catch (Exception ex)
            {
                LogTxt.WriteError("异常:", ex.ToString(), EventLogEntryType.Error);
            }
        }

        static int reviceCount = 0;


        /// <summary>
        /// 消息达到后交个Biz处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SocketHelper_MessageReviced(object sender, MessageRevicedEventArgs e)
        {
            string sendingApp = AppSettingString.SendingApp;
            string recvApp = AppSettingString.RecvApp; //System.Configuration.ConfigurationManager.AppSettings["recvApp"];
            try
            {
                DBConn dbcon = new DBConn();
                PipeParser parser = new PipeParser();
                //解析出消息类型，创建对应的Biz进行处理
                string message = System.Text.Encoding.UTF8.GetString(e.Contents);
                message = MediII.Common.MLLPHelper.TrimMLLP(message, true, false);
                Logger.WriteLog("接收信息:" + message, LogType.Message);

                //手术字典
                if (message.Contains(ConfigurationManager.AppSettings["AcceptTitleOperDic"]))
                {
                    OperDicModel dic = HL7ToXmlConverter.ToOperDic(message);
                    dbcon.InsertOperDic(dic);
                }


                if (message.Contains(ConfigurationManager.AppSettings["AcceptTitle"]))
                {
                    paibanModel paiban = HL7ToXmlConverter.toDataBae(message);
                    string tableName = System.Configuration.ConfigurationManager.AppSettings["PaibanTableName"];
                    if (dbcon.GetPaiban(paiban, tableName).Rows.Count == 0)
                    {
                        dbcon.InsertPaiban(paiban, tableName);
                    }

                }

                if (message.Contains(ConfigurationManager.AppSettings["AcceptTitleConfig"]))
                {
                    paibanModel paiban = HL7ToXmlConverter.toDataBae(message);
                    string tableName = System.Configuration.ConfigurationManager.AppSettings["PaibanTableName"];
                    if (dbcon.GetPaiban(paiban, tableName).Rows.Count == 1)
                    {
                        dbcon.UpdatePaibanAll(paiban, tableName);
                    }
                }

                if (message.Contains(ConfigurationManager.AppSettings["AcceptTitleCancel"]))
                {
                    string PatID = "";
                    message = message.Replace("ARQ", "\nARQ");
                    string[] sList = message.Split('\n');
                    foreach (string str in sList)
                    {
                        if (str.Contains("ARQ|"))
                        {
                            PatID = str.Split('|')[1].Replace("^", "");
                        }
                    }
                    string tableName = System.Configuration.ConfigurationManager.AppSettings["PaibanTableName"];
                    dbcon.UpdatePaibanOstate(tableName, PatID);
                }

                //string mesStruct = parser.GetMessageStructure(message).Substring(0, 3);
                string ackMsg = MediII.Common.MessageHelper.SetACK("ACK", "", "", recvApp, recvApp, sendingApp, sendingApp,
                                                   Guid.NewGuid().ToString("N"));
                Logger.WriteLog(ackMsg);
                SocketHelper.SendAck(MediII.Common.MLLPHelper.AddMLLP(ackMsg), e.SocketHandler);
            }
            catch (Exception ex)
            {
                //出现异常需要返回，避免队列堵塞
                string ackMsg = MediII.Common.MessageHelper.SetACK("ACK", "", "", recvApp, recvApp, sendingApp, sendingApp,
                                                            Guid.NewGuid().ToString("N"), ex.Message);
                Logger.WriteLog(ackMsg);
                SocketHelper.SendAck(MediII.Common.MLLPHelper.AddMLLP(ackMsg), e.SocketHandler);
                MediII.Common.LogHelper.LogError(ex, Common.LogCatagories.AdapterBiz);
            }
        }

        /// <summary>
        /// 结束处理
        /// </summary>
        public void EndProecess()
        {
            SocketHelper.StopListen();
        }

        public bool IsRuning
        {
            get { return SocketHelper.IsRuning; }
        }
    }
}
