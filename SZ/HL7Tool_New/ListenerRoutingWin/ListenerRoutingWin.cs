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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sendingApp = AppSettingString.SendingApp;
            string recvApp = AppSettingString.RecvApp; //System.Configuration.ConfigurationManager.AppSettings["recvApp"];
            DBConn dbcon = new DBConn();
            String message = txtOutput.Text;
          

            //手术字典
            if (message.Contains(ConfigurationManager.AppSettings["AcceptTitleOperDic"]))
            {
                OperDicModel dic = HL7ToXmlConverter.ToOperDic(message);
               int res= dbcon.InsertOperDic(dic);
                if (res>0)
                {
                    MessageBox.Show("插入字典成功");
                }
            }

            if (message.Contains(ConfigurationManager.AppSettings["AcceptTitleConfig"]))
            {
                paibanModel paiban = HL7ToXmlConverter.toDataBae(message);
                string tableName = System.Configuration.ConfigurationManager.AppSettings["PaibanTableName"];
                if (dbcon.GetPaiban(paiban, tableName).Rows.Count == 1)
                {
                    int i = dbcon.UpdatePaibanAll(paiban, tableName);
                    if (i > 0)
                    {
                        MessageBox.Show("修改手术成功");
                    }
                }
            }
            if (message.Contains(ConfigurationManager.AppSettings["AcceptTitle"]))
            {
                paibanModel paiban = HL7ToXmlConverter.toDataBae(message);
                string tableName = ConfigurationManager.AppSettings["PaibanTableName"];
                if (dbcon.GetPaiban(paiban, tableName).Rows.Count == 0)
                {
                    int i = dbcon.InsertPaiban(paiban, tableName);
                    if (i > 0)
                    {
                        MessageBox.Show("新加手术成功");
                    }
                }
                else
                    MessageBox.Show("病人已经存在!");
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
                int i = dbcon.UpdatePaibanOstate(tableName, PatID);
                if (i > 0)
                {
                    MessageBox.Show("取消手术成功");
                } 
            }
           
        }
    }
}