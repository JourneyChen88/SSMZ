﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MediII.Adapter.ListenerRouting;
using NHapi.Base.Parser;
using MediII.Adapter.BaseBiz;
using ListenerRoutingLib;
using System.Xml;
using System.Configuration;

namespace ListenerRoutingWin
{
    public partial class ListenerRoutingWin : Form
    {
        ListenerRouting sp = new ListenerRouting();
        System.Threading.Timer gcTimer = null;
        ListenerInfo li = new ListenerInfo();

        public ListenerRoutingWin()
        {
            InitializeComponent();
        }

        private void StartServices()
        {

            //监听会阻塞，如果后续还有操作，启动一个线程进行操作
            li.LoadFromConfig();
            sp.BeginProcess(li);
        }
        private void btnListener_Click(object sender, EventArgs e)
        {
            btnListener.Enabled = false;
            btnEnd.Enabled = true;
            try
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(StartServices)).Start();
            }
            catch
            {
                btnListener.Enabled = true;
            }
        }

        private void ListenerRoutingWin_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();

            System.Environment.Exit(0);
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            btnListener.Enabled = true;
            btnEnd.Enabled = false;
            sp.EndProecess();

        }

        private void ListenerRoutingWin_Load(object sender, EventArgs e)
        {
            btnEnd.Enabled = false;
            Program._AcceptTitleOperDic = ConfigurationManager.AppSettings["AcceptTitleOperDic"];
            Program._CancelOperApply = ConfigurationManager.AppSettings["CancelOperApply"];
            Program._NewOperApply = ConfigurationManager.AppSettings["NewOperApply"];
            Program._PaibanTableName = ConfigurationManager.AppSettings["PaibanTableName"];
            Program._UpdateOperApply = ConfigurationManager.AppSettings["UpdateOperApply"];
        }
        DBConn dbcon = new DBConn();
        private void button1_Click(object sender, EventArgs e)
        {
            string sendingApp = AppSettingString.SendingApp;
            string recvApp = AppSettingString.RecvApp; //System.Configuration.ConfigurationManager.AppSettings["recvApp"];

            String message = txtOutput.Text;


            ////手术字典
            //if (message.Contains(Program._AcceptTitleOperDic))
            //{
            //    OperDicModel dic = HL7ToXmlConverter.ToOperDic(message);
            //    int res = dbcon.InsertOperDic(dic);
            //    if (res > 0)
            //    {
            //        MessageBox.Show("插入字典成功");
            //    }
            //}


            if (message.Contains(Program._NewOperApply))
            {
                OTypesetting paiban = null;
                paiban = HL7ToXmlConverter.toDataBae(message, paiban);
                if (dbcon.GetPaiban(paiban.PatZhuYuanID) == null)
                {
                    int i = dbcon.InsertPaiban(paiban);
                    if (i > 0)
                    {
                        MessageBox.Show("新加手术成功");
                    }
                }
                else
                    MessageBox.Show("病人已经存在!");
            }

            else if (message.Contains(Program._UpdateOperApply))
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
            else if (message.Contains(Program._CancelOperApply))
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
                int i = dbcon.UpdatePaibanOstate(zhuyuanid, -1);
                if (i > 0)
                {
                    MessageBox.Show("取消手术成功");
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dbcon.UpdatePaibanOstate("100009715512", 2);
        }
    }
}
