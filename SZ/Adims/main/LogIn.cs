///************************************
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
using System.Configuration;
using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using adims_BLL;

namespace main
{
    public partial class LogIn : Form
    {
        #region <<Members>>
        string FileName = "UpdateFile.xml";
        string LocalPath = Application.StartupPath;//本地文件地址(没有文件名)
        string LocalFilePath = Application.StartupPath + "\\UpdateFile.xml";//xml文件的路径
        string LocalTime = "";//本地的时间
        string LocalVersion = "";//本地程序版本
        string ServerTime = "";//本地的时间
        string ServerVersion = "";//本地程序
        AdimsController bll = new AdimsController();

        #endregion

        #region <<Constructors>>

        public LogIn()
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
            if (string.IsNullOrEmpty(Convert.ToString(cmbYiyuan.Text.Trim())))
            {
                MessageBox.Show("请请选择医院代号！");
                cmbYiyuan.Focus();
                return;
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtLoginName.Text.Trim())))
            {
                MessageBox.Show("请输入 用户名！");
                txtLoginName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(Convert.ToString(txtPassWord.Text.Trim())))
            {
                MessageBox.Show("请输入 密码！");
                txtPassWord.Focus();
                return;
            }
            try
            {
                string uid = txtLoginName.Text.Trim();
                string password = txtPassWord.Text.Trim();
                DataTable dtUser = bll.Get_user_info(uid, password);
                if (dtUser.Rows.Count != 0)
                {
                    SaveConfigure();
                    Program.Customer.user_name = dtUser.Rows[0][2].ToString();
                    Program.Customer.position = dtUser.Rows[0][3].ToString();
                    Program.Customer.userno = dtUser.Rows[0][4].ToString();
                    if (cmbYiyuan.Text.Trim() == "江苏盛泽医院")
                        Program.Customer.yiyuanType = "010601";
                    else if (cmbYiyuan.Text.Trim() == "妇幼保健中心")
                        Program.Customer.yiyuanType = "010604";
                    string jurisdiction = bll.Get_user_jurisdiction(Program.Customer);
                    int jurisdiction_Length = jurisdiction.Length;
                    Program.jurisdiction = new bool[jurisdiction_Length];
                    Char[] temp = new char[jurisdiction_Length];
                    temp = jurisdiction.ToCharArray();
                    for (int j = 0; j < jurisdiction.Length; j++)
                        Program.jurisdiction[j] = (temp[j] == '1') ? true : false;
                    DialogResult = DialogResult.OK;
                    LockSet f1 = new LockSet();
                    f1.timer11.Start(); //定时锁定程序
                    loginCount++;
                }
                else
                {
                    MessageBox.Show("账号或密码错误,请检查。");
                    txtPassWord.Text = string.Empty;
                }

            }
            catch (Exception ex)
            {
                string errorStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "服务器中断，登陆失败!!";
                SaveLog(errorStr);
                MessageBox.Show("登录失败！", ex.ToString());
                this.txtPassWord.Text = string.Empty;
            }
        }


        private void SaveLog(string str)
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\ErrorLinkInternet.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.WriteLine(str);
            sw.Close();
            fs.Close();
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
        public void IsUpate()//检查服务器ftp地址是否需要更新
        {
            try
            {
                string ftpURL = ConfigurationManager.AppSettings["FtpURL"];//ftp的路径
                string ftpUser = ConfigurationManager.AppSettings["FtpUser"];//ftp的用户
                string ftpPassword = ConfigurationManager.AppSettings["FtpPWD"];//ftp的密码
                //打开服务器上的的xml
                XmlDocument xmlDoc1 = new XmlDocument();
                ClassFTP ftp = new ClassFTP(ftpUser, ftpPassword);
                xmlDoc1.Load(ftpURL + "UpdateFile.xml");
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

        public int loginCount = 0;
        private void login_Load(object sender, EventArgs e)
        {

            Program.Globals.SeverIp = ConfigurationManager.AppSettings["SeverIp"];
            GetConfigure();
            if (loginCount == 0)
            {
                if (UserFunction.PingHost(Program.Globals.SeverIp))
                {
                    IsUpate();
                }
                else
                    MessageBox.Show("服务器连接中断，请检查。");
            }



        }
        private void SaveConfigure()
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\LogInNormal.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.WriteLine(cmbYiyuan.Text);
            sw.Close();
            fs.Close();
        }
        private void GetConfigure()//获取常用登录医院
        {
            string filepath = Application.StartupPath + "\\LogInNormal.txt";
            if (!File.Exists(filepath))
            {
                File.Create(filepath);
            }
            FileStream fs = new FileStream(filepath, FileMode.Open);
            StreamReader sw = new StreamReader(fs, Encoding.Default);
            cmbYiyuan.Text = sw.ReadLine();
            sw.Close();
            fs.Close();
        }
    }
}
