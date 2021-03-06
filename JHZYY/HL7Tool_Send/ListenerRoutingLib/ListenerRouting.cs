﻿using ListenerRoutingLib;
//using MediII.Adapter.BaseBiz;
using NHapi.Base.Parser;
using System;
using System.Configuration;

namespace MediII.Adapter.ListenerRouting
{
    public class ListenerRouting
    {
        private SqlServerLink sqlHelp = new SqlServerLink();
        public string _AcceptTitleOperDic = ConfigurationManager.AppSettings["AcceptTitleOperDic"];
        public string _CancelOperApply = ConfigurationManager.AppSettings["CancelOperApply"];
        public string _NewOperApply = ConfigurationManager.AppSettings["NewOperApply"];
        public string _UpdateOperApply = ConfigurationManager.AppSettings["UpdateOperApply"];
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
                LogHelp.WriteErrorLog("异常:", ex.ToString());
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
                LogHelp.WriteLog(message);

                ////手术字典
                //if (message.Contains(_AcceptTitleOperDic))
                //{
                //    OperDicModel dic = HL7ToXmlConverter.ToOperDic(message);
                //    dbcon.InsertOperDic(dic);
                //}


                if (message.Contains(_NewOperApply))
                {
                    OTypesetting paiban = null;
                    paiban = HL7ToXmlConverter.toDataBae(message, paiban);
                    if (dbcon.GetPaiban(paiban.PatZhuYuanID) == null)
                    {
                        dbcon.InsertPaiban(paiban);
                    }

                }

                //修改
                else if (message.Contains(_UpdateOperApply))
                {
                    string zhuyuanid = HL7ToXmlConverter.GetZhuyuanId(message);
                    OTypesetting paiban = dbcon.GetPaiban(zhuyuanid);
                    paiban = HL7ToXmlConverter.toDataBae(message, paiban);

                    if (paiban != null)
                    {
                        if (paiban.Ostate == 0)
                        {
                            dbcon.UpdatePaiban(paiban);
                        }
                    }
                }

                else if (message.Contains(_CancelOperApply))
                {
                    string zhuyuanid = "";
                    message = message.Replace("ARQ", "\nARQ");
                    string[] sList = message.Split('\n');
                    foreach (string str in sList)
                    {
                        if (str.Contains("ARQ|"))
                        {
                            zhuyuanid = str.Split('|')[1].Replace("^", "");
                        }
                    }
                    dbcon.UpdatePaibanOstate(zhuyuanid, -1);
                }

                //string mesStruct = parser.GetMessageStructure(message).Substring(0, 3);
                string ackMsg = MediII.Common.MessageHelper.SetACK("ACK", "", "", recvApp, recvApp, sendingApp, sendingApp,
                                                   Guid.NewGuid().ToString("N"));
                LogHelp.WriteLog(ackMsg);
                SocketHelper.SendAck(MediII.Common.MLLPHelper.AddMLLP(ackMsg), e.SocketHandler);
            }
            catch (Exception ex)
            {
                //出现异常需要返回，避免队列堵塞
                string ackMsg = MediII.Common.MessageHelper.SetACK("ACK", "", "", recvApp, recvApp, sendingApp, sendingApp,
                                                            Guid.NewGuid().ToString("N"), ex.Message);
                LogHelp.WriteErrorLog(ackMsg);
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
