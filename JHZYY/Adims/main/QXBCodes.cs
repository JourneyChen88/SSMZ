using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;

namespace main
{
    public partial class QXBCodes : Form
    {
        string paID;//住院号
        string Ddate;//时间
        adims_BLL.AdimsController dal = new adims_BLL.AdimsController();
        adims_DAL.Wzdb_DB_Help Wzdb = new adims_DAL.Wzdb_DB_Help();
        public QXBCodes(string patid,string dtiem)
        {
            paID = patid;
            Ddate =Convert.ToDateTime(dtiem).ToString("yyyy-MM-dd");
            InitializeComponent();
            BarCode.BarCodeEvent += new BardCodeHooK.BardCodeDeletegate(BarCode_BarCodeEvent);
        }
        public QXBCodes()
        {
            InitializeComponent();
            BarCode.BarCodeEvent += new BardCodeHooK.BardCodeDeletegate(BarCode_BarCodeEvent);

        }
        BardCodeHooK BarCode = new BardCodeHooK();
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QXBCodes_Load(object sender, EventArgs e)
        {
            BarCode.Start();
            this.txtZYH.Text = paID;
        }
             
        private delegate void ShowInfoDelegate(BardCodeHooK.BarCodes barCode);
        private void ShowInfo(BardCodeHooK.BarCodes barCode)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ShowInfoDelegate(ShowInfo), new object[] { barCode });
            }
            else
            {
                textBox1.Text = barCode.KeyName;
                textBox2.Text = barCode.VirtKey.ToString();
                textBox3.Text = barCode.ScanCode.ToString();
                textBox4.Text = barCode.Ascll.ToString();
                textBox5.Text = barCode.Chr.ToString();
                textBoxCode.Text = barCode.IsValid ? barCode.BarCode : textBoxCode.Text;//是否为扫描枪输入，如果为true则是 否则为键盘输入
                //textBox7.Text += barCode.KeyName;
                //MessageBox.Show(barCode.IsValid.ToString());
            }
        }

        //C#中判断扫描枪输入与键盘输入 

        //Private DateTime _dt = DateTime.Now;  //定义一个成员函数用于保存每次的时间点
        //private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    DateTime tempDt = DateTime.Now;          //保存按键按下时刻的时间点
        //    TimeSpan ts = tempDt .Subtract(_dt);     //获取时间间隔
        //    if (ts.Milliseconds > 50)                           //判断时间间隔，如果时间间隔大于50毫秒，则将TextBox清空
        //        textBox1.Text = "";
        //    dt = tempDt ;
        //}



        private void BarCode_BarCodeEvent(BardCodeHooK.BarCodes barCode)
        {

            ShowInfo(barCode);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            BarCode.Stop();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBoxCode.Text.Length > 0)
            {
                // MessageBox.Show("条码长度：" + textBox6.Text.Length + "\n条码内容：" + textBox6.Text, "系统提示");
            }
        }
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxCode.Text != "")
                {
                    string IPaddress = "132.147.160.27";
                    bool flag = DataValid.PingHost(IPaddress, 1000);
                    if (flag == false)
                    {
                        MessageBox.Show("器械追溯数据库未连接，请检查网络");
                        return;
                    }
                    int rs = Wzdb.UpdateQXB(paID, textBoxCode.Text.Trim());///插入器械追溯的数据库
                    if (rs > 0)
                    {
                       
                    }
                    else
                    {
                        MessageBox.Show("没找到改条码号对应的住院号！");
                        return;
                    }
                    //插入本地数据库
                    DataTable dt = dal.QXCodes(paID, Ddate);
                     int result=0;
                    if (dt.Rows.Count > 0)
                    {
                        result = dal.UpdateQXCodes(paID, Ddate, textBoxCode.Text.Trim());
                    }
                    else
                    {
                        result = dal.insertQXCodes(paID, Ddate, textBoxCode.Text.Trim());
                        int results = dal.UpdatePaiban(paID, Ddate, 1);//修改器械状态
                    }
                    if (result>0)
                    {
                         MessageBox.Show("保存成功！");
                    }
                    //int rss = Wzdb.UpdateQXB(paID, textBox6.Text.Trim());
                    //if (rss>0)
                    //{
                    //    MessageBox.Show("保存成功！");
                    //}
                    //else
                    //{
                    //    MessageBox.Show("没找到改条码号对应的住院号！");
                    //}                   
                }
                else
                {
                    MessageBox.Show("条码号不能为空！");
                }
            }
            catch (Exception ex)
            {
                DataValid.SaveScanLog(ex.ToString());
                MessageBox.Show("保存失败!"+ex.ToString());
            }
           
        }

     

     


    }
}
