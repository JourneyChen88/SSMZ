using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediII.Adapter.BaseBiz;
using NHapi.Model.V24.Message;
using NHapi.Model.V24.Segment;
using NHapi.Base.Model;
using System.Data.Common;
using System.Transactions;

namespace MediII.Adapter.BizComponent.ORM
{
    public class BizComponent_ORM : BaseBizComponent, IBizComponent
    {
        public override string DoProcess(IMessage m)
        {
            ORM_O01 ormO01 = m as ORM_O01;
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    ORC orc = ormO01.GetORDER(0).ORC;
                    int yeWuLx = 0;//-1:退单,0:单据恢复到初时状态，1:预约,2:登记，3：报告完成,4:审核报告
                    string shenQingDId = orc.PlacerOrderNumber.EntityIdentifier.Value;
                    string caoZuoYuan = orc.GetEnteredBy(0).IDNumber.Value;
                    string caoZuoKs = orc.EntererSLocation.PointOfCare.Value;

                    string errMsg = string.Empty;
                    //检查登记
                    if (orc.OrderControl.Value == "SC"
                        && orc.OrderStatus.Value == "SC")
                    {
                        yeWuLx = 2;
                        //解析消息，得到消息内容
                    }
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        return SetACK(ormO01, "ACK", "", "", errMsg);
                    }
                    StringBuilder sb=new StringBuilder();
                    sb.Append("申请单ID:[").Append(shenQingDId).Append("]").Append(Environment.NewLine);
                    sb.Append("操作员:[").Append(caoZuoYuan).Append("]").Append(Environment.NewLine);
                    sb.Append("附：接收到消息后，可参考组装消息进行解析，在此不再罗列，只以[申请单ID]作为测试");
                    MediII.Net.Common.LogTxt.WriteError("接收消息内容：" + Environment.NewLine, sb.ToString(), System.Diagnostics.EventLogEntryType.Information);
                    scope.Complete();
                    return SetACK(ormO01, "ACK", "", "");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    return SetACK(ormO01, "ACK", "", "", ex.Message);
                }
                else
                {
                    return SetACK(ormO01, "ACK", "", "", ex.Message + Environment.CommandLine + ex.InnerException.Message);
                }
            }
        }      
    }
}
