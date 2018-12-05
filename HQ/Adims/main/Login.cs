﻿///************************************
///Updated By        : Senvi
///************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Configuration;
using adims_Utility;

namespace main
{
    public partial class login : Form
    {
        #region <<Members>>
        string FileName = "UpdateFile.xml";
        string LocalPath = Application.StartupPath;//本地文件地址(没有文件名)
        string LocalFilePath = Application.StartupPath + "\\UpdateFile.xml";//xml文件的路径
        string LocalTime = "";//本地的时间
        string LocalVersion = "";//本地程序版本
        static string DownPath = Application.StartupPath + "\\UpdateCompare\\";//下载服务器到本地的路劲名字
        string DownFilePath = Application.StartupPath + "\\UpdateCompare\\UpdateFile.xml";//xml文件的路径
        string ServerTime = "";//本地的时间
        string ServerVersion = "";//本地程序
        adims_DAL.Dics.UserDal _UserDal = new adims_DAL.Dics.UserDal();

        #endregion

        #region <<Constructors>>

        public login()
        {
            InitializeComponent();
        }

        #endregion

        #region <<Events>>

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(txtLoginName.Text.Trim())))
                {
                    MessageBox.Show("请输入用户名！");
                    txtLoginName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(Convert.ToString(txtPassWord.Text.Trim())))
                {
                    MessageBox.Show("请输入密码！");
                    txtPassWord.Focus();
                    return;
                }


                Program.customer.uid = txtLoginName.Text.Trim();
                Program.customer.password = txtPassWord.Text.Trim();
                DataTable dt = _UserDal.GetUserInfo(Program.customer);
                if (dt.Rows.Count > 0)
                {
                    Program.customer.user_name = dt.Rows[0][2].ToString();
                    Program.customer.position = dt.Rows[0][3].ToString();
                    Program.customer.type = Convert.ToInt32(dt.Rows[0][4]);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("账号或密码错误,请检查。");
                    txtPassWord.Text = string.Empty;
                    txtPassWord.Focus();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void login_Load(object sender, EventArgs e)
        {
            if (TextValueLimit.PingHost(ConfigurationManager.AppSettings["SeverIp"]))
            {
                IsUpate();
            }
        }
        /// <summary>
        /// 检查服务器ftp地址是否需要更新
        /// </summary>
        public void IsUpate()
        {
            try
            {
                string ftpURL = ConfigurationManager.AppSettings["FtpURL"];//ftp的路径
                string ftpUser = ConfigurationManager.AppSettings["FtpUser"];//ftp的用户
                string ftpPassword = ConfigurationManager.AppSettings["FtpPWD"];//ftp的密码
                //打开服务器上的的xml
                XmlDocument xmlDoc1 = new XmlDocument();
                ClassFTP ftp = new ClassFTP(ftpUser, ftpPassword);
                ftp.DownFtpDir("ftp://192.168.1.80:2121/UpdateFile/", Application.StartupPath + "\\UpdateFile");
                xmlDoc1.Load(Application.StartupPath + "\\UpdateFile\\UpdateFile.xml");
                foreach (XmlNode node in xmlDoc1.ChildNodes)
                {
                    if (node.Name == "Autoupdater")
                    {
                        foreach (XmlNode node1 in node.ChildNodes)
                        {
                            if (node1.Name == "Version")
                            {
                                ServerVersion = node1.InnerText;
                                break;
                            }
                        }
                        break;
                    }
                }
                //打开本地xml
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(LocalFilePath);
                foreach (XmlNode node in xmlDoc.ChildNodes)
                {
                    if (node.Name == "Autoupdater")
                    {
                        foreach (XmlNode node1 in node.ChildNodes)
                        {
                            if (node1.Name == "Version")
                            {
                                LocalVersion = node1.InnerText;
                                break;
                            }
                        }
                        break;
                    }
                }
                //判断版本号是否有更新
                if (float.Parse(ServerVersion) > float.Parse(LocalVersion))
                {
                    Application.Exit();
                    string FileSave = Application.StartupPath;
                    Process proc = new Process();
                    proc.StartInfo.FileName = FileSave + "\\FrmUpdate.exe";
                    proc.StartInfo.UseShellExecute = false;
                    proc.Start();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
