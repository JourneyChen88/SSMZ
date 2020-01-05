using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NHapi.Model.V24;
using NHapi.Base.Parser;
using NHapi.Model.V24.Message;
using NHapi.Model.V24.Segment;
using MediII.Common;
using Microsoft.International.Converters.PinYinConverter;
using NHapi.Model.V24.Datatype;


namespace SenderRoutingWin
{
    public partial class SenderWCFWin : Form
    {
        string FilePath = string.Empty;
        string message = string.Empty;
        private TextBox txtNum;
        private TextBox txtPort;
        private TextBox txtIP;
        private TextBox txtOutput;
        MessageHelper msgHelper;

        public SenderWCFWin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.txtIP.Text = "http://10.10.150.6:8010/wcfService";
        }


        private void btnSender_Click(object sender, EventArgs e)
        {
            message = this.txtOutput.Text.Trim();
            if (message.Length > 0)
            {
                if (txtIP.Text.Trim().Length > 0 && txtNum.Text.Trim().Length > 0)
                {
                    msgHelper=new MessageHelper(this.txtIP.Text.Trim());
                    object objResult;
                    int iResult = 0;
                    int count = Convert.ToInt32(txtNum.Text.Trim());
                    if (count < 10)
                    {
                        new System.Threading.Thread(o =>
                        {
                            for (int i = 0; i < count; i++)
                            {
                                objResult = msgHelper.SendMFNMessage(message);
                                string ack = objResult == null ? string.Empty : objResult.ToString();
                                if (ack.Contains("AA"))
                                {
                                    iResult++;
                                    SetText(string.Format("成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                }
                                else
                                {
                                    iResult++;
                                    SetText(string.Format("消息处理失败原因：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                }
                            }
                        }).Start();
                    }
                    else
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            new System.Threading.Thread(o =>
                            {
                                for (int i = 0; i < count / 10; i++)
                                {
                                    objResult = msgHelper.SendMFNMessage(message);
                                    string ack = objResult == null ? string.Empty : objResult.ToString();
                                    if (ack.Contains("AA"))
                                    {
                                        iResult++;
                                        SetText(string.Format("成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                    }
                                    else
                                    {
                                        iResult++;
                                        SetText(string.Format("发送失败，错误信息：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                    }
                                }
                            }).Start();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("WCF地址或端口或发送次数未填写");
                }
            }
            else
            {
                MessageBox.Show("无法获取消息内容，请检查");
            }
        }
        delegate void MyInvoke(string str);
        private void SetText(string s)
        {
            if (txtOutput.InvokeRequired)
            {
                MyInvoke _myInvoke = new MyInvoke(SetText);
                this.Invoke(_myInvoke, new object[] { s });
            }
            else
            {
                this.txtOutput.AppendText(s);
                Application.DoEvents();
            }
        }

        private void InitializeComponent()
        {
            this.txtNum = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtNum
            // 
            this.txtNum.Location = new System.Drawing.Point(294, 76);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(33, 21);
            this.txtNum.TabIndex = 9;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(209, 77);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(53, 21);
            this.txtPort.TabIndex = 8;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(45, 76);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(113, 21);
            this.txtIP.TabIndex = 7;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(45, 136);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(282, 21);
            this.txtOutput.TabIndex = 10;
            // 
            // SenderWCFWin
            // 
            this.ClientSize = new System.Drawing.Size(368, 262);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.txtNum);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIP);
            this.Name = "SenderWCFWin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}