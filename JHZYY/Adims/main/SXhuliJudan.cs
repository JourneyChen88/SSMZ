using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main.CGG
{
    public partial class SXhuliJudan : Form
    {
        adims_BLL.mz bll = new adims_BLL.mz();
        adims_DAL.mz dal = new adims_DAL.mz();
        string patID;
        public SXhuliJudan(string ZYNumber)
        {
            patID = ZYNumber;
            InitializeComponent();
        }
        private void SXhuliJudan_Load(object sender, EventArgs e)
        {
            yishi();
            Hushi();
            //Xuexing();
            //rhxuexing();
            Info();
            
            
            
        }
        #region<<显示信息>>
        private void Info()//显示基本信息
        {
            DataTable dt = dal.GetSXhljilu(patID);
            DataRow dr = dt.Rows[0];
            textBoxkeshi.Text = dr["Patdpm"].ToString();
            textBoxchuanghao.Text = dr["patbedno"].ToString();
            textBoxname.Text = dr["Patname"].ToString();
            textBoxsex.Text = dr["Patsex"].ToString();
            textBoxage.Text = dr["Patage"].ToString();
            textBoxzubie.Text = dr["PatNation"].ToString();
            textBoxzhuyuanID.Text = dr["PatID"].ToString();

             DataTable dt1 = dal.GetSxhljjd(patID);
            if(dt1.Rows.Count>0)
            {
                DataRow dr11 = dt1.Rows[0];
                dateTimePickerrate.Value = Convert.ToDateTime(dr11["sxrate"]);
                 dateTimePicker2.Value = Convert.ToDateTime(dr11["beginsx"]);
                 dateTimePicker3.Value = Convert.ToDateTime(dr11["endinsx"]);
                 comboBoxABO.Text = dr11["Binrenxuexing"].ToString().Substring(0, dr11["Binrenxuexing"].ToString().IndexOf("|"));
                 int aa = dr11["Binrenxuexing"].ToString().Substring(0, dr11["Binrenxuexing"].ToString().IndexOf("|")).Length + 1;
                 comboBoxrh.Text = dr11["Binrenxuexing"].ToString().Substring(aa);
                 comboBoxABO1.Text = dr11["Gxzxuexing"].ToString().Substring(0, dr11["Gxzxuexing"].ToString().IndexOf("|"));
                 int bb = dr11["Gxzxuexing"].ToString().Substring(0, dr11["Gxzxuexing"].ToString().IndexOf("|")).Length + 1;
                 comboBoxrh1.Text = dr11["Gxzxuexing"].ToString().Substring(bb);
                 if (Convert.ToString(dr11["SXfenlei"]).Contains("全血、")) checkBoxQX.Checked = true;
                 if (Convert.ToString(dr11["SXfenlei"]).Contains("冰冻血浆、")) checkBoxBD.Checked = true;
                 if (Convert.ToString(dr11["SXfenlei"]).Contains("普通、")) checkBoxputong.Checked = true;
                 if (Convert.ToString(dr11["SXfenlei"]).Contains("新鲜、")) checkBoxxinxian.Checked = true;
                 if (Convert.ToString(dr11["SXfenlei"]).Contains("去病毒、")) checkBoxqbd.Checked = true;
                 if (Convert.ToString(dr11["SXfenlei"]).Contains("红细胞、")) checkBoxhxb.Checked = true;
                 if (Convert.ToString(dr11["SXfenlei"]).Contains("悬浮、")) checkBoxxf.Checked = true;
                 if (Convert.ToString(dr11["SXfenlei"]).Contains("洗涤、")) checkBoxxd.Checked = true;
                 if (Convert.ToString(dr11["SXfenlei"]).Contains("滤白、")) checkBoxlb.Checked = true;
                 if (Convert.ToString(dr11["SXfenlei"]).Contains("冰冻、")) checkBoxbdong.Checked = true;
                 if (Convert.ToString(dr11["SXfenlei"]).Contains("解冻去甘油、")) checkBoxjd.Checked = true;
                 if (Convert.ToString(dr11["SXfenlei"]).Contains("辐照、")) checkBoxfz.Checked = true;
                 if (Convert.ToString(dr11["SXfenlei"]).Contains("血小板、")) checkBoxxuexiaoban.Checked = true;
                 if (Convert.ToString(dr11["SXfenlei"]).Contains("单采、")) checkBoxdc.Checked = true;
                 if (Convert.ToString(dr11["SXfenlei"]).Contains("浓缩、")) checkBoxns.Checked = true;
                 if (Convert.ToString(dr11["SXfenlei"]).Contains("少白、")) checkBoxsb.Checked = true;
                 if (Convert.ToString(dr11["SXfenlei"]).Contains("坑沉淀、")) checkBoxlcd.Checked = true;
                 textBoxsrl.Text = dr11["SXliang"].ToString();
                 if (Convert.ToString(dr11["kangguominYW"]).Contains("有")) checkBoxy.Checked = true;
                 if (Convert.ToString(dr11["kangguominYW"]).Contains("无")) checkBoxw.Checked = true;

                 textBoxKname1.Text = dr11["kgmname"].ToString().Substring(0, dr11["kgmname"].ToString().IndexOf("|"));
                 int cc = dr11["kgmname"].ToString().Substring(0, dr11["kgmname"].ToString().IndexOf("|")).Length + 1;
                 textBoxKname2.Text = dr11["kgmname"].ToString().Substring(cc);
                
                 textBoxqtw.Text = dr11["SXqianTW"].ToString();
                 textBoxqmb.Text = dr11["SXqianMB"].ToString();
                 textBoxqhx.Text = dr11["SXqianHX"].ToString();
                 textBoxqxy.Text = dr11["SXqianXY"].ToString();

                 textBoxztw.Text = dr11["SXzhongTW"].ToString();
                 textBoxzmb.Text = dr11["SXzhongMB"].ToString();
                 textBoxzhx.Text = dr11["SXzhongHX"].ToString();
                 textBoxzxy.Text = dr11["SXzhongXY"].ToString();

                 textBoxhtw.Text = dr11["SXhouTW"].ToString();
                 textBoxhmb.Text = dr11["SXhouMB"].ToString();
                 textBoxhhx.Text = dr11["SXhouHX"].ToString();
                 textBoxhxy.Text = dr11["SXhouXY"].ToString();
                 textBoxshuzhuqian.Text = dr11["SZ15fenqian"].ToString();
                 textBoxshuzhuhou.Text = dr11["SZ15fenhou"].ToString();
                 textBoxzunyizhu.Text = dr11["SZzunyi"].ToString();
                 if (Convert.ToString(dr11["SZshunlifou"]).Contains("是")) checkBox24.Checked = true;
                 if (Convert.ToString(dr11["SZshunlifou"]).Contains("否")) checkBox25.Checked = true;
                 textBoxbushunlibeizhu.Text = dr11["BSLbeizhu"].ToString();
                 comboBoxYSname.Text = dr11["MZyisheng"].ToString();
                 comboBoxHSname.Text = dr11["HSqianzi"].ToString();
                 dateTimePicker4.Value = Convert.ToDateTime(dr11["qianzirate"]);
                 textBoxjiaojieid.Text = dr11["SzJJXdID"].ToString();
                 textBoxjiaojieliang.Text = dr11["SzJJXdlliang"].ToString();
                 if (Convert.ToString(dr11["SzJJzhaungkuang"]).Contains("顺利")) checkBoxjshunli.Checked = true;
                 if (Convert.ToString(dr11["SzJJzhaungkuang"]).Contains("不顺利")) checkBoxjbshunl.Checked = true;
                 textBoxssmu.Text = dr11["SzJJZKmiaosu"].ToString().Substring(0, dr11["SzJJZKmiaosu"].ToString().IndexOf("|"));
                 int dd = dr11["SzJJZKmiaosu"].ToString().Substring(0, dr11["SzJJZKmiaosu"].ToString().IndexOf("|")).Length + 1;
                 textBoxbssms.Text = dr11["SzJJZKmiaosu"].ToString().Substring(dd);

                 textBoxjiaoname.Text = dr11["SzJJjiaohuqianzi"].ToString();
                 textBoxjiename.Text = dr11["SzJJjieshouqianzi"].ToString();
                 dateTimePicker6.Value = Convert.ToDateTime(dr11["SxFybeginrate"]);
                 dateTimePicker7.Value = Convert.ToDateTime(dr11["SxFyendrate"]);
                 dateTimePicker8.Value = Convert.ToDateTime(dr11["SxFySXrate"].ToString().Substring(0, dr11["SxFySXrate"].ToString().IndexOf("|")));
                 int ee = dr11["SxFySXrate"].ToString().Substring(0, dr11["SxFySXrate"].ToString().IndexOf("|")).Length + 1;
                 dateTimePicker9.Value = Convert.ToDateTime(dr11["SxFySXrate"].ToString().Substring(ee));
                 textBoxSSxuexing.Text = dr11["SxFySXxuexing"].ToString();
                 textBoxsssdid.Text = dr11["SxFySXID"].ToString();
                 textBoxssssl.Text = dr11["SxFySXliang"].ToString();
                 if (Convert.ToString(dr11["SxFySXZhengzhuang"]).Contains("发热")) checkBox28.Checked = true;
                 if (Convert.ToString(dr11["SxFySXZhengzhuang"]).Contains("寒战")) checkBox29.Checked = true;
                 if (Convert.ToString(dr11["SxFySXZhengzhuang"]).Contains("休克")) checkBox30.Checked = true;
                 if (Convert.ToString(dr11["SxFySXZhengzhuang"]).Contains("皮肤充血")) checkBox31.Checked = true;
                 if (Convert.ToString(dr11["SxFySXZhengzhuang"]).Contains("荨麻疹")) checkBox32.Checked = true;
                 if (Convert.ToString(dr11["SxFySXZhengzhuang"]).Contains("黄疸")) checkBox33.Checked = true;
                 if (Convert.ToString(dr11["SxFySXZhengzhuang"]).Contains("腰背痛")) checkBox34.Checked = true;
                 if (Convert.ToString(dr11["SxFySXZhengzhuang"]).Contains("酱油色尿")) checkBox35.Checked = true;
                 if (Convert.ToString(dr11["SxFySXZhengzhuang"]).Contains("伤口渗血不止")) checkBox36.Checked = true;
                 if (Convert.ToString(dr11["SxFySXZhengzhuang"]).Contains("发绀")) checkBox37.Checked = true;
                 if (Convert.ToString(dr11["SxFySXZhengzhuang"]).Contains("呼吸困难")) checkBox38.Checked = true;
                 if (Convert.ToString(dr11["SxFySXZhengzhuang"]).Contains("咯大量血性泡沫痰")) checkBox39.Checked = true;
                 if (Convert.ToString(dr11["SxFySXZhengzhuang"]).Contains("两肺布满湿啰音")) checkBox40.Checked = true;
                 if (Convert.ToString(dr11["SxFySXZhengzhuang"]).Contains("颈静脉怒张")) checkBox41.Checked = true;
                 if (Convert.ToString(dr11["SxFySXZhengzhuang"]).Contains("少尿/无尿")) checkBox42.Checked = true;
                 if (Convert.ToString(dr11["SxFySXZhengzhuang"]).Contains("其它")) checkBox43.Checked = true;
                 int ff = dr11["SxFySXZhengzhuang"].ToString().Substring(0, dr11["SxFySXZhengzhuang"].ToString().IndexOf("|")).Length + 1;
                 textBqita.Text = dr11["SxFySXZhengzhuang"].ToString().Substring(ff);
                 textBoxclgc.Text = dr11["SxFySXCL"].ToString();
                 textBoxcljg.Text = dr11["SxFySXCLJG"].ToString();

                 if (Convert.ToString(dr11["SxFySXSBao"]).Contains("是")) checkBoxhlbuy.Checked = true;
                 if (Convert.ToString(dr11["SxFySXSBao"]).Contains("否")) checkBoxhlbn.Checked = true;

                 dateTimePicker10.Value = Convert.ToDateTime(dr11["SxFySXHLSBaotime"]);
                 if (Convert.ToString(dr11["SxFySXsxSBao"]).Contains("是")) checkBox45.Checked = true;
                 if (Convert.ToString(dr11["SxFySXsxSBao"]).Contains("否")) checkBox46.Checked = true;
                 dateTimePicker11.Value = Convert.ToDateTime(dr11["SxFySXsxSBaotime"]);
                 textBox40.Text = dr11["SxFySXhushiqianzi"].ToString();

            }
////交接记录
            
           
            
          
            
            
            
          
           
            //SQF.Add("SxFySXhushiqianzi", textBox40.Text);

            //DataTable dt = dal.GetSxhljjd(patID);
            
        }
        private void yishi()//绑定医师
        {
            DataTable yishi = dal.GetAllshoushuyishi();
            comboBoxYSname.Items.Clear();
            for (int i = 0; i < yishi.Rows.Count; i++)
            {
                this.comboBoxYSname.Items.Add(yishi.Rows[i][0]);
            }
        }
        private void Hushi()//绑定护士
        {
            DataTable hushi = dal.GetAllshoushuhushi();
            comboBoxHSname.Items.Clear();
            for (int i = 0; i < hushi.Rows.Count; i++)
            {
                this.comboBoxHSname.Items.Add(hushi.Rows[i][0]);

            }
        }
        private void Xuexing()//abo血型
        {
            DataTable xuexing = dal.GetaboXuexing();
            comboBoxABO.Items.Clear();
            for (int i = 0; i < xuexing.Rows.Count; i++)
            {
                this.comboBoxABO.Items.Add(xuexing.Rows[i][0]);
                this.comboBoxABO1.Items.Add(xuexing.Rows[i][0]);
            }
        }
        private void rhxuexing()//rh血型
        {
            DataTable rhxuexing = dal.Getrhxuexing();
            comboBoxrh.Items.Clear();
            for (int i = 0; i < rhxuexing.Rows.Count; i++)
            {
                this.comboBoxrh.Items.Add(rhxuexing.Rows[i][0]);
                this.comboBoxrh1.Items.Add(rhxuexing.Rows[i][0]);
            }
        }
        #endregion
        #region<<保存信息>>
        private void buttonbaocun_Click(object sender, EventArgs e)
        {
            //if (comboBoxABO.Text=="")
            //{
            //    MessageBox.Show("血型不能为空");
            //    comboBoxABO.Focus();
            //    return;
            //}
            //if (comboBoxrh.Text == "")
            //{
            //    MessageBox.Show("RH血型不能为空");
            //    comboBoxrh.Focus();
            //    return;
            //}
            //if (comboBoxABO1.Text == "")
            //{
            //    MessageBox.Show("供血者血型不能为空");
            //    comboBoxABO1.Focus();
            //    return;
            //}
            //if (textBoxsrl.Text == "")
            //{
            //    MessageBox.Show("输血量不能为空");
            //    textBoxsrl.Focus();
            //    return;
            //}
            //if (textBoxsrl.Text == "")
            //{
            //    MessageBox.Show("输血量不能为空");
            //    textBoxsrl.Focus();
            //    return;
            //}
            //if (textBoxqtw.Text == "")
            //{
            //    MessageBox.Show("输血前体温不能为空");
            //    textBoxqtw.Focus();
            //    return;
            //}
            //if (textBoxztw.Text == "")
            //{
            //    MessageBox.Show("输血中体温不能为空");
            //    textBoxztw.Focus();
            //    return;
            //}
            //if (textBoxhtw.Text == "")
            //{
            //    MessageBox.Show("输血后体温不能为空");
            //    textBoxhtw.Focus();
            //    return;
            //}
            ////
            //if (textBoxqmb.Text == "")
            //{
            //    MessageBox.Show("输血前脉搏不能为空");
            //    textBoxqmb.Focus();
            //    return;
            //}
            //if (textBoxzmb.Text == "")
            //{
            //    MessageBox.Show("输血中脉搏不能为空");
            //    textBoxzmb.Focus();
            //    return;
            //}
            //if (textBoxhmb.Text == "")
            //{
            //    MessageBox.Show("输血后脉搏不能为空");
            //    textBoxhmb.Focus();
            //    return;
            //}
            //if (textBoxqhx.Text == "")
            //{
            //    MessageBox.Show("呼吸不能为空");
            //    textBoxqhx.Focus();
            //    return;
            //}
            //if (textBoxzhx.Text == "")
            //{
            //    MessageBox.Show("呼吸不能为空");
            //    textBoxzhx.Focus();
            //    return;
            //}
            //if (textBoxhhx.Text == "")
            //{
            //    MessageBox.Show("呼吸不能为空");
            //    textBoxhhx.Focus();
            //    return;
            //}
            //if (textBoxqxy.Text == "")
            //{
            //    MessageBox.Show("血压不能为空");
            //    textBoxqxy.Focus();
            //    return;
            //}
            //if (textBoxzxy.Text == "")
            //{
            //    MessageBox.Show("血压不能为空");
            //    textBoxzxy.Focus();
            //    return;
            //}
            //if (textBoxhxy.Text == "")
            //{
            //    MessageBox.Show("血压不能为空");
            //    textBoxhxy.Focus();
            //    return;
            //}

            //if (textBoxshuzhuqian.Text == "")
            //{
            //    MessageBox.Show("输血前输入速度不能为空");
            //    textBoxshuzhuqian.Focus();
            //    return;
            //}
            //if (textBoxshuzhuhou.Text == "")
            //{
            //    MessageBox.Show("注入15分钟后输入速度不能为空");
            //    textBoxshuzhuhou.Focus();
            //    return;
            //}
            //if (textBoxzunyizhu.Text == "")
            //{
            //    MessageBox.Show("遵医输入速度不能为空");
            //    textBoxzunyizhu.Focus();
            //    return;
            //}



            baocun();
          
        }
        private void baocun()//保存
        {
            Dictionary<string, string> SQF = new Dictionary<string, string>();
            int result = 0;
            if (buttonbaocun.Enabled)
            {
                SQF.Add("IsRead", "0");
            }
            else
            {
                SQF.Add("IsRead", "1");
            }
            string AddItem = "";
            SQF.Add("ZhuYuanID", patID);
            SQF.Add("name", textBoxname.Text);
            SQF.Add("keshi", textBoxkeshi.Text);
            SQF.Add("sex", textBoxsex.Text);
            SQF.Add("age", textBoxage.Text);
            SQF.Add("sxrate", Convert.ToDateTime(dateTimePickerrate.Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
            SQF.Add("beginsx", Convert.ToDateTime(dateTimePicker2.Value.ToString()).ToString("HH:mm:ss"));
            SQF.Add("endinsx", Convert.ToDateTime(dateTimePicker3.Value.ToString()).ToString("HH:mm:ss"));
            SQF.Add("Binrenxuexing", comboBoxABO.Text +"|"+ comboBoxrh.Text);
            SQF.Add("Gxzxuexing", comboBoxABO1.Text + "|" + comboBoxrh1.Text);
            AddItem = "";
            if (checkBoxQX.Checked) AddItem += "全血、";
            if (checkBoxBD.Checked) AddItem += "冰冻血浆、";
            if (checkBoxputong.Checked) AddItem += "普通、";
            if (checkBoxxinxian.Checked) AddItem += "新鲜、";
            if (checkBoxqbd.Checked) AddItem += "去病毒、";
            if (checkBoxhxb.Checked) AddItem += "红细胞、";
            if (checkBoxxf.Checked) AddItem += "悬浮、";
            if (checkBoxxd.Checked) AddItem += "洗涤、";
            if (checkBoxlb.Checked) AddItem += "滤白、";
            if (checkBoxbdong.Checked) AddItem += "冰冻、";
            if (checkBoxjd.Checked) AddItem += "解冻去甘油、";
            if (checkBoxfz.Checked) AddItem += "辐照、";
            if (checkBoxxuexiaoban.Checked) AddItem += "血小板、";
            if (checkBoxdc.Checked) AddItem += "单采、";
            if (checkBoxns.Checked) AddItem += "浓缩、";
            if (checkBoxsb.Checked) AddItem += "少白、";
            if (checkBoxlcd.Checked) AddItem += "坑沉淀、";
            SQF.Add("SXfenlei", AddItem);
            SQF.Add("SXliang", textBoxsrl.Text);
            string AddItem1 = "";
            AddItem1 = "";
            if (checkBoxy.Checked) AddItem1 += "有";
            if (checkBoxw.Checked) AddItem1 += "无";
            SQF.Add("kangguominYW", AddItem1);
            SQF.Add("kgmname", textBoxKname1.Text + "|" + textBoxKname2.Text);
            // SQF.Add("textBoxKname2", textBoxKname2.Text);
            SQF.Add("SXqianTW", textBoxqtw.Text);//输血前
            SQF.Add("SXqianMB", textBoxqmb.Text);
            SQF.Add("SXqianHX", textBoxqhx.Text);
            SQF.Add("SXqianXY", textBoxqxy.Text);
            SQF.Add("SXzhongTW", textBoxztw.Text);//输血中
            SQF.Add("SXzhongMB", textBoxzmb.Text);
            SQF.Add("SXzhongHX", textBoxzhx.Text);
            SQF.Add("SXzhongXY", textBoxzxy.Text);
            SQF.Add("SXhouTW", textBoxhtw.Text);//输血后
            SQF.Add("SXhouMB", textBoxhmb.Text);
            SQF.Add("SXhouHX", textBoxhhx.Text);
            SQF.Add("SXhouXY", textBoxhxy.Text);
            //15分钟
            SQF.Add("SZ15fenqian", textBoxshuzhuqian.Text);
            SQF.Add("SZ15fenhou", textBoxshuzhuhou.Text);
            SQF.Add("SZzunyi", textBoxzunyizhu.Text);
            string AddItem2 = "";
            AddItem2 = "";
            if (checkBox24.Checked) AddItem2 += "是";
            if (checkBox25.Checked) AddItem2 += "否";
            SQF.Add("SZshunlifou", AddItem2);
            SQF.Add("BSLbeizhu", textBoxbushunlibeizhu.Text);
            SQF.Add("MZyisheng", comboBoxYSname.Text);
            SQF.Add("HSqianzi", comboBoxHSname.Text);
            SQF.Add("qianzirate", Convert.ToDateTime(dateTimePicker4.Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
            //交接记录
            SQF.Add("SzJJXdID", textBoxjiaojieid.Text);
            SQF.Add("SzJJXdlliang", textBoxjiaojieliang.Text);
            string AddItem3 = "";
            AddItem3 = "";
            if (checkBoxjshunli.Checked) AddItem3 += "顺利";
            if (checkBoxjbshunl.Checked) AddItem3 += "不顺利";
            SQF.Add("SzJJzhaungkuang", AddItem3);
            SQF.Add("SzJJZKmiaosu", textBoxssmu.Text + "|" + textBoxbssms.Text);
            SQF.Add("SzJJjiaohuqianzi", textBoxjiaoname.Text);
            SQF.Add("SzJJjieshouqianzi", textBoxjiename.Text);
            SQF.Add("SxFybeginrate", Convert.ToDateTime(dateTimePicker6.Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
            SQF.Add("SxFyendrate", Convert.ToDateTime(dateTimePicker7.Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
            SQF.Add("SxFySXrate", Convert.ToDateTime(dateTimePicker8.Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss") + '|' + Convert.ToDateTime(dateTimePicker9.Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
            SQF.Add("SxFySXxuexing", textBoxSSxuexing.Text);
            SQF.Add("SxFySXID", textBoxsssdid.Text);
            SQF.Add("SxFySXliang", textBoxssssl.Text);
            string AddItem4 = "";
            AddItem4 = "";
            if (checkBox28.Checked) AddItem4 += "发热";
            if (checkBox29.Checked) AddItem4 += "寒战";
            if (checkBox30.Checked) AddItem4 += "休克";
            if (checkBox31.Checked) AddItem4 += "皮肤充血";
            if (checkBox32.Checked) AddItem4 += "荨麻疹";
            if (checkBox33.Checked) AddItem4 += "黄疸";
            if (checkBox34.Checked) AddItem4 += "腰背痛";
            if (checkBox35.Checked) AddItem4 += "酱油色尿";
            if (checkBox36.Checked) AddItem4 += "伤口渗血不止";
            if (checkBox37.Checked) AddItem4 += "发绀";
            if (checkBox38.Checked) AddItem4 += "呼吸困难";
            if (checkBox39.Checked) AddItem4 += "咯大量血性泡沫痰";
            if (checkBox40.Checked) AddItem4 += "两肺布满湿啰音";
            if (checkBox41.Checked) AddItem4 += "颈静脉怒张";
            if (checkBox42.Checked) AddItem4 += "少尿/无尿";
            if (checkBox43.Checked) AddItem4 += "其它" + "|" + textBqita.Text;
            SQF.Add("SxFySXZhengzhuang", AddItem4);
            SQF.Add("SxFySXCL", textBoxclgc.Text);
            SQF.Add("SxFySXCLJG", textBoxcljg.Text);
            string AddItem5 = "";
            AddItem5 = "";
            if (checkBoxhlbuy.Checked) AddItem5 += "是";
            if (checkBoxhlbn.Checked) AddItem5 += "否";
            SQF.Add("SxFySXSBao", AddItem5);
            SQF.Add("SxFySXHLSBaotime", Convert.ToDateTime(dateTimePicker10.Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
            string AddItem6 = "";
            AddItem6 = "";
            if (checkBox45.Checked) AddItem6 += "是";
            if (checkBox46.Checked) AddItem6 += "否";
            SQF.Add("SxFySXsxSBao", AddItem6);
            SQF.Add("SxFySXsxSBaotime", Convert.ToDateTime(dateTimePicker11.Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
            SQF.Add("SxFySXhushiqianzi", textBox40.Text);

            DataTable dt = dal.GetSxhljjd(patID);

            if (dt.Rows.Count > 0)
            {
                result = dal.UpdateSxhljjd(SQF);
                MessageBox.Show("修改成功");
            }
            else
            {
                result = dal.InsertSxhljjd(SQF);
                MessageBox.Show("保存成功！");
            }
            
        }
        #endregion

        #region<<打印信息>>
        private void button2_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;//打印
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            //           //printDocument1.DefaultPageSettings.PaperSize =
            //    new System.Drawing.Printing.PaperSize("K16", 740, 1020);
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("A4", 820, 1160);
        }
        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen ptp = Pens.Black;//普通画笔
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);
            Pen pblack2 = new Pen(Brushes.Black, 2);
            Font ptzt = new Font("宋体", 12);//普通字体
            Font ptzt7 = new Font("宋体", 9);//普通字体
            Font ptzt1 = new Font("新宋体", 8);//普通字体
            Font ptzt5 = new Font("新宋体", 14);//普通字体
            Font ptzt2 = new Font("黑体", 10, FontStyle.Bold);//加粗十号
            Font ptzt3 = new Font("新宋体", 14, FontStyle.Bold);//加粗14号
            Font ptzt4 = new Font("新宋体", 19, FontStyle.Bold);//加粗16号
            int y = 40; int x = 65; int y1 = 0;
            //短横线
            e.Graphics.DrawLine(Pens.Black, x + 42, y + 121, x + 220, y + 121);//科别
            e.Graphics.DrawLine(Pens.Black, x + 270, y + 121, x + 295, y + 121);//床号
            e.Graphics.DrawLine(Pens.Black, x + 375, y + 121, x + 510, y + 121); //姓名
            e.Graphics.DrawLine(Pens.Black, x + 560, y + 121, x + 590, y + 121);//性别
            e.Graphics.DrawLine(Pens.Black, x + 640, y + 121, x + 660, y + 121);//年龄
            e.Graphics.DrawLine(Pens.Black, x + 586, y + 78, x + 659, y + 78);//住院号
            e.Graphics.DrawLine(Pens.Black, x + 69, y + 272, x + 302, y + 272);//输血量
            //e.Graphics.DrawLine(Pens.Black, x + 81, y + 162, x + 230, y + 162);//手术日期
            //e.Graphics.DrawLine(Pens.Black, x + 312, y + 162, x + 659, y + 162);//手术名称
            y = y + 30;
            string title1 = "新疆医科大学第五附属医院";
            e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 205, y));
            y = y + 30;
            string title2 = "输血护理记录单";
            e.Graphics.DrawString(title2, ptzt3, Brushes.Black, new Point(x + 260, y));
            e.Graphics.DrawString("住院ID：" + textBoxzhuyuanID.Text.Trim(), ptzt, Brushes.Black, new Point(x + 515, y));
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawString("科室：" + textBoxkeshi.Text, ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("床号：" + textBoxchuanghao.Text.Trim() + " 床", ptzt, Brushes.Black, new Point(x + 225, y));
            e.Graphics.DrawString("姓名：" + textBoxname.Text.Trim(), ptzt, Brushes.Black, new Point(x + 325, y));
            e.Graphics.DrawString("性别：" + textBoxsex.Text.Trim(), ptzt, Brushes.Black, new Point(x + 510, y));
            e.Graphics.DrawString("年龄：" + textBoxage.Text.Trim(), ptzt, Brushes.Black, new Point(x + 590, y));
            y = y + 25;
            //竖线
            e.Graphics.DrawLine(Pens.Black, x, y + 0, x, y + 900);
            e.Graphics.DrawLine(Pens.Black, x + 660, y + 0, x + 660, y + 900);
            //横线
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 900, x + 660, y + 900);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 0, x + 660, y + 0);
            //e.Graphics.DrawLine(Pens.Black, x + 110, y + 783, x + 250, y + 783);
            //e.Graphics.DrawLine(Pens.Black, x + 122, y + 814, x + 262, y + 814);
            e.Graphics.DrawString("输血日期：" + dateTimePickerrate.Text.Trim(), ptzt, Brushes.Black, new Point(x + 10, y + 10));
            e.Graphics.DrawString("开始时间：" , ptzt, Brushes.Black, new Point(x + 250, y + 10));
            e.Graphics.DrawString("结束时间：" , ptzt, Brushes.Black, new Point(x + 420, y + 10));
            y = y + 30;
            e.Graphics.DrawString("病人血型:ABO血型（" + comboBoxABO.Text.Trim() + "）RH(D)血型（" + comboBoxrh.Text.Trim() + "）;", ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 20;
            e.Graphics.DrawString("供者血型:ABO血型（" + comboBoxABO1.Text.Trim() + "）RH(D)血型（" + comboBoxrh1.Text.Trim() + "）;", ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 20;
            e.Graphics.DrawString("输注血液成分种类:□全血、□冰冻血浆(□普通、□新鲜、□去病毒)、□红细胞(", ptzt, Brushes.Black, new Point(x + 10, y));
            if (checkBoxQX.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 155, y));
            if (checkBoxBD.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 220, y));
            if (checkBoxputong.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 313, y));
            if (checkBoxxinxian.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 378, y));
            if (checkBoxqbd.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 444, y));
            if (checkBoxhxb.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 536 , y));
            y = y + 20;
            e.Graphics.DrawString("□悬浮、□洗涤、□滤白、□冰冻、□解冻去甘油、□辐照)、□血小板（□单采、", ptzt, Brushes.Black, new Point(x + 10, y));
            if (checkBoxxf.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 10, y));
            if (checkBoxxd.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 79, y));
            if (checkBoxlb.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 148, y));
            if (checkBoxbdong.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 214, y));
            if (checkBoxjd.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 280, y));
            if (checkBoxfz.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 396, y));
            if (checkBoxxuexiaoban.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 471, y));
            if (checkBoxdc.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 554, y));
            y = y + 20;
            e.Graphics.DrawString("□浓缩、□少白）、□冷沉淀。", ptzt, Brushes.Black, new Point(x + 10, y));
            if (checkBoxns.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 10, y));
            if (checkBoxsb.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 77, y));
            if (checkBoxlcd.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 160, y));
            y = y + 20;
            e.Graphics.DrawString("输血量：" + textBoxsrl.Text.Trim(), ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 20;
            e.Graphics.DrawString("抗过敏药物：□有 □无", ptzt, Brushes.Black, new Point(x + 10, y));
            if (checkBoxy.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 111, y));
            if (checkBoxw.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 153, y));
            e.Graphics.DrawString("抗过敏药物名称及剂量：1." + textBoxKname1.Text.Trim(), ptzt, Brushes.Black, new Point(x + 200, y));
            y = y + 20;
            e.Graphics.DrawString("2." + textBoxKname2.Text.Trim(), ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 20;
            e.Graphics.DrawLine(Pens.Black, x + 90, y + 36, x + 150, y + 36);//短1
            e.Graphics.DrawLine(Pens.Black, x + 90, y + 56, x + 150, y + 56);
            e.Graphics.DrawLine(Pens.Black, x + 90, y + 76, x + 150, y + 76);
            e.Graphics.DrawLine(Pens.Black, x + 90, y + 96, x + 150, y + 96);
            e.Graphics.DrawLine(Pens.Black, x + 305, y + 36, x + 365, y + 36);//短2
            e.Graphics.DrawLine(Pens.Black, x + 305, y + 56, x + 365, y + 56);
            e.Graphics.DrawLine(Pens.Black, x + 305, y + 76, x + 365, y + 76);
            e.Graphics.DrawLine(Pens.Black, x + 305, y + 96, x + 365, y + 96);
            e.Graphics.DrawLine(Pens.Black, x + 520, y + 36, x + 580, y + 36);//短3
            e.Graphics.DrawLine(Pens.Black, x + 520, y + 56, x + 580, y + 56);
            e.Graphics.DrawLine(Pens.Black, x + 520, y + 76, x + 580, y + 76);
            e.Graphics.DrawLine(Pens.Black, x + 520, y + 96, x + 580, y + 96);
            e.Graphics.DrawLine(Pens.Black, x + 194, y + 147, x + 242, y + 147);//
            e.Graphics.DrawLine(Pens.Black, x + 194, y + 167, x + 242, y + 167);//
            e.Graphics.DrawLine(Pens.Black, x + 567, y + 147, x + 615, y + 147);//
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 0, x + 660, y + 0);//横线
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 120, x + 660, y + 120);//横线
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 180, x + 660, y + 180);//横线
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 300, x + 660, y + 300);//横线
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 415, x + 660, y + 415);
            e.Graphics.DrawLine(Pens.Black, x + 40, y + 0, x + 40, y + 120);//竖线
            e.Graphics.DrawLine(Pens.Black, x + 215, y + 0, x + 215, y + 120);
            e.Graphics.DrawLine(Pens.Black, x + 257, y + 0, x + 257, y + 120);
            e.Graphics.DrawLine(Pens.Black, x + 430, y + 0, x + 430, y + 120);
            e.Graphics.DrawLine(Pens.Black, x + 472, y + 0, x + 472, y + 120);
            y = y + 20;
            e.Graphics.DrawString("输   " + "体温 " + textBoxqtw.Text.Trim(),ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("℃", ptzt, Brushes.Black, new Point(x + 150, y));
            e.Graphics.DrawString("输   " + "体温 " + textBoxztw.Text.Trim(), ptzt, Brushes.Black, new Point(x + 225, y));
            e.Graphics.DrawString("℃", ptzt, Brushes.Black, new Point(x + 365, y));
            e.Graphics.DrawString("输   " + "体温 " + textBoxhtw.Text.Trim(), ptzt, Brushes.Black, new Point(x + 440, y));
            e.Graphics.DrawString("℃", ptzt, Brushes.Black, new Point(x + 580, y));
            y = y + 20;
            e.Graphics.DrawString("血   " + "脉搏 " + textBoxqmb.Text.Trim(), ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("次/分", ptzt, Brushes.Black, new Point(x + 150, y));
            e.Graphics.DrawString("血   " + "脉搏 " + textBoxzmb.Text.Trim(), ptzt, Brushes.Black, new Point(x + 225, y));
            e.Graphics.DrawString("次/分", ptzt, Brushes.Black, new Point(x + 365, y));
            e.Graphics.DrawString("血   " + "脉搏 " + textBoxhmb.Text.Trim(), ptzt, Brushes.Black, new Point(x + 440, y));
            e.Graphics.DrawString("次/分", ptzt, Brushes.Black, new Point(x + 580, y));
            y = y + 20;
            e.Graphics.DrawString("前   " + "呼吸 " + textBoxqhx.Text.Trim(), ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("次/分", ptzt, Brushes.Black, new Point(x + 150, y));
            e.Graphics.DrawString("中   " + "呼吸 " + textBoxzhx.Text.Trim(), ptzt, Brushes.Black, new Point(x + 225, y));
            e.Graphics.DrawString("次/分", ptzt, Brushes.Black, new Point(x + 365, y));
            e.Graphics.DrawString("后   " + "呼吸 " + textBoxhhx.Text.Trim(), ptzt, Brushes.Black, new Point(x + 440, y));
            e.Graphics.DrawString("次/分", ptzt, Brushes.Black, new Point(x + 580, y));
            y = y + 20;
            e.Graphics.DrawString("     " + "血压 " + textBoxqxy.Text.Trim(), ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("mmHg", ptzt, Brushes.Black, new Point(x + 150, y));
            e.Graphics.DrawString("     " + "血压 " + textBoxzxy.Text.Trim(), ptzt, Brushes.Black, new Point(x + 225, y));
            e.Graphics.DrawString("mmHg", ptzt, Brushes.Black, new Point(x + 365, y));
            e.Graphics.DrawString("     " + "血压 " + textBoxhxy.Text.Trim(), ptzt, Brushes.Black, new Point(x + 440, y));
            e.Graphics.DrawString("mmHg", ptzt, Brushes.Black, new Point(x + 580, y));
            y = y + 50;
            e.Graphics.DrawString("输注前15分钟输入的速度" + textBoxshuzhuqian.Text.Trim(), ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("滴/分", ptzt, Brushes.Black, new Point(x + 240, y));
            e.Graphics.DrawString("输注15分钟后遵医嘱输入的速度" + textBoxshuzhuhou.Text.Trim(), ptzt, Brushes.Black, new Point(x + 330, y));
            e.Graphics.DrawString("滴/分", ptzt, Brushes.Black, new Point(x + 610, y));
            y = y + 20;
            e.Graphics.DrawString("必要时遵医嘱输入的速度" + textBoxzunyizhu.Text.Trim(), ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("滴/分", ptzt, Brushes.Black, new Point(x + 240, y));
            y = y + 40;
            e.Graphics.DrawString("输血过程是否顺利：□有 □否", ptzt, Brushes.Black, new Point(x + 10, y));
            if (checkBox24.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 161, y));
            if (checkBox25.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 203, y));
            y = y + 20;
            e.Graphics.DrawString("如不顺利请详述如下：", ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 20;
            e.Graphics.DrawString(textBoxbushunlibeizhu.Text.Trim(), ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 40;
            e.Graphics.DrawString("医护双签（麻醉医生/护士）：" + comboBoxYSname.Text.Trim() + "/" + comboBoxHSname.Text.Trim(), ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString(dateTimePicker4.Value.ToString("yyyy-MM-dd HH:mm"), ptzt, Brushes.Black, new Point(x + 440, y));
            y = y + 40;
            e.Graphics.DrawString("输注过程中如有交接请记录：", ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 20;
            e.Graphics.DrawString("交接时间：         月          日          时          分" , ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 20;
            e.Graphics.DrawString("交接血袋编号：" + textBoxjiaojieid.Text.Trim(), ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("交接量：" + textBoxjiaojieliang.Text.Trim(), ptzt, Brushes.Black, new Point(x + 300, y));
            y = y + 20;
            e.Graphics.DrawString("交接时患者输注情况:□顺利 □不顺利", ptzt, Brushes.Black, new Point(x + 10, y));
            if (checkBoxjshunli.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 168, y));
            if (checkBoxjbshunl.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 227, y));
            e.Graphics.DrawString("(描述):" + textBoxssmu .Text.Trim()+ textBoxbssms.Text.Trim(), ptzt, Brushes.Black, new Point(x + 300, y));
            y = y + 20;
            e.Graphics.DrawString("交接人双签：" + textBoxjiaoname.Text.Trim(), ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("、" + textBoxjiename.Text.Trim(), ptzt, Brushes.Black, new Point(x + 250, y));
            y = y + 35;
            e.Graphics.DrawString("如发生输血反应请填写如下项目：", ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 20;
            e.Graphics.DrawString("输血反应发生时间" , ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("供血者血型：" + textBoxSSxuexing.Text.Trim(), ptzt, Brushes.Black, new Point(x + 300, y));
            y = y + 20;
            e.Graphics.DrawString("输血反应结束时间" , ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("输入血袋编号：" + textBoxsssdid.Text.Trim(), ptzt, Brushes.Black, new Point(x + 300, y));
            y = y + 20;
            e.Graphics.DrawString("输血反应持续时间", ptzt, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawString("输入量：" + textBoxssssl.Text.Trim(), ptzt, Brushes.Black, new Point(x + 460, y));
            y = y + 20;
            e.Graphics.DrawString("患者症状与体征:□发热、□寒战、□休克、□皮肤充血、□荨麻疹、□黄疸、□腰背痛", ptzt, Brushes.Black, new Point(x + 10, y));
            if (checkBox28.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 139, y));
            if (checkBox29.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 206, y));
            if (checkBox30.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 273, y));
            if (checkBox31.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 340, y));
            if (checkBox32.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 440, y));
            if (checkBox33.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 520, y));
            if (checkBox34.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 588, y));
            y = y + 20;
            e.Graphics.DrawString(" 、□酱油色尿、□伤口渗血不止、□发绀、□呼吸困难、□咯大量血性泡沫痰、", ptzt, Brushes.Black, new Point(x + 10, y));
            if (checkBox35.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 38, y));
            if (checkBox36.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 140, y));
            if (checkBox37.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 271, y));
            if (checkBox38.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 338, y));
            if (checkBox39.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 438, y));
            y = y + 20;
            e.Graphics.DrawString("   □两肺布满湿啰音、□颈静脉怒张、□少尿/无尿、□其它:"+textBqita.Text.Trim()+"。", ptzt, Brushes.Black, new Point(x + 10, y));
            if (checkBox40.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 37, y));
            if (checkBox41.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 185, y));
            if (checkBox42.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 301, y));
            if (checkBox43.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 407, y));
            y = y + 20;
            e.Graphics.DrawString("处理过程：", ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 20;
            string str = "";
            int a = textBoxclgc.TextLength / 37;
            for (int i = 0; i <= textBoxclgc.TextLength / 37; i++)
            {
                if (i < a)
                {
                    str += textBoxclgc.Text.Substring(i * 37, 37) + Environment.NewLine;
                }
                else
                {
                    str += textBoxclgc.Text.Substring(i * 37);
                }
            }
            e.Graphics.DrawString(str, ptzt, Brushes.Black, new Point(x +10, y));
            y = y + 40;
            e.Graphics.DrawString("处理结果：" + textBoxcljg.Text.Trim(), ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 20;
            e.Graphics.DrawString("当班护士双签：" + textBox40.Text.Trim(), ptzt, Brushes.Black, new Point(x + 10, y));
            y = y + 20;
            e.Graphics.DrawString("上报护理部：□是 □否", ptzt, Brushes.Black, new Point(x + 10, y));
            if (checkBoxhlbuy.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 111, y));
            if (checkBoxhlbn.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 153, y));
            e.Graphics.DrawString("上报时间：    月    日    时    分", ptzt, Brushes.Black, new Point(x + 360, y));
            y = y + 20;
            e.Graphics.DrawString("上报输血科：□是 □否", ptzt, Brushes.Black, new Point(x + 10, y));
            if (checkBox45.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 111, y));
            if (checkBox46.Checked) e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x + 153, y));
            e.Graphics.DrawString("上报时间：    月    日    时    分", ptzt, Brushes.Black, new Point(x + 360, y));
          
           
        }
        #endregion

        private void button3_Click(object sender, EventArgs e)//退出
        {
            this.Close();
        }

      
    }
}
