using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using adims_DAL;
using System.Text.RegularExpressions;
namespace main
{
    public partial class JHmzqfsd : Form
    {
        bool xinjian = true;
        DataTable table1_copy;
        DataTable table1;
        adims_DAL.LIS_DB_Help dav = new adims_DAL.LIS_DB_Help();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        adims_DAL.PACU_DAL pdal = new adims_DAL.PACU_DAL();
        string ZYNumber1;
        bool isRead = false;
        public JHmzqfsd(string ZYNumber)
        {
            ZYNumber1 = ZYNumber;
            InitializeComponent();
        }

        private void buttonbaocun_Click(object sender, EventArgs e)
        {
            if (textBoxkebie.Text == "")
            {
                MessageBox.Show("科别不能为空!");
                textBoxkebie.Focus();
                return;
            }
            if (textBoxchanghao.Text == "")
            {
                MessageBox.Show("床号不能为空！");
                textBoxchanghao.Focus();
                return;
            }
            if (textBoxzhuyuanhao.Text == "")
            {
                MessageBox.Show("住院号不能为空！");
                textBoxzhuyuanhao.Focus();
                return;
            }
            if (textBoxxingming.Text == "")
            {
                MessageBox.Show("姓名不能为空！");
                textBoxxingming.Focus();
                return;

            }
            if (textBoxsex.Text == "")
            {
                MessageBox.Show("性别不能为空！");
                textBoxsex.Focus();
                return;
            }
            if (textBoxage.Text == "")
            {
                MessageBox.Show("年龄不能为空！");
                textBoxage.Focus();
                return;
            }
            if (comboBoxyshengqz.Text == "")
            {
                MessageBox.Show("评估医生不能为空！");
                comboBoxyshengqz.Focus();
                return;
            }
            if (textBoxBP.Text == "")
            {
                MessageBox.Show("BP不能为空！");
                textBoxBP.Focus();
                return;
            }
            if (textBoxR.Text == "")
            {
                MessageBox.Show("R不能为空！");
                textBoxR.Focus();
                return;
            }
            if (textBoxP.Text == "")
            {
                MessageBox.Show("P不能为空！");
                textBoxP.Focus();
                return;
            }
            if (textBoxT.Text == "")
            {
                MessageBox.Show("T不能为空！");
                textBoxP.Focus();
                return;
            }
            if (textBoxT.Text == "")
            {
                MessageBox.Show("T不能为空！");
                textBoxP.Focus();
                return;
            }
            if (comboBoxxxg.Text == "")
            {
                MessageBox.Show("心血管不能为空！");
                comboBoxxxg.Focus();
                return;
            }
            //if (comboBoxfhx1.Text == "")
            //{
            //    MessageBox.Show("肺和呼吸不能为空！");
            //    comboBoxfhx1.Focus();
            //    return;
            //}
            if (textnxssfs.Text == "")
            {
                MessageBox.Show("拟行手术方式不能为空！");
                textnxssfs.Focus();
                return;
            }
            if (textBoxmqtsyw1.Text == "")
            {
                MessageBox.Show("特殊药物不能为空！");
                textBoxmqtsyw1.Focus();
                return;
            }
            if (textxy.Text == "")
            {
                MessageBox.Show("血液不能为空！");
                textxy.Focus();
                return;
            }
            if (textBoxxindiantu.Text == "")
            {
                MessageBox.Show("心电图不能为空！");
                textBoxxindiantu.Focus();
                return;
            }
            if (comboBoxasa.Text == "")
            {
                MessageBox.Show("ASA不能为空！");
                comboBoxasa.Focus();
                return;
            }
            if (comboBoxynbw.Text == "")
            {
                MessageBox.Show("是否饱胃不能为空！");
                comboBoxynbw.Focus();
                return;
            }
           
            save();
            bc();
        }
        private void bc()
        {
            Dictionary<string, string> SQFS = new Dictionary<string, string>();
            string AddItem = "";
            SQFS.Add("zhuyuanhao", ZYNumber1);
            SQFS.Add("mzxz", comboBoxmzxz.Text);
            SQFS.Add("mzxzqt", textBoxmzxzqt.Text);
            SQFS.Add("qtsx", textBoxqtzysx.Text);
            AddItem = "";
            if (checkBoxyqshzts.Checked) AddItem += "1";
            if (checkBoxyqshztf.Checked) AddItem += "2";
            SQFS.Add("yqshzt", AddItem);
            SQFS.Add("mzysqz", textBoxmzysqz.Text);
            SQFS.Add("rq", Convert.ToDateTime(dateTimePickerRQ.Value.ToString()).ToString("yyyy-MM-dd HH:mm"));
            DataTable dt = DAL.zqtzs(ZYNumber1);
            if (dt.Rows.Count > 0)
            {
                DAL.zqtzsxg(SQFS);
            }
            else
            {
                DAL.zqtzsxz(SQFS);
            }
        }
        private void save()
        {
            try
            {
                Dictionary<string, string> SQFS = new Dictionary<string, string>();
                int result = 0;
                string AddItem = "";
                if (xinjian == false)
                {
                    SQFS.Add("zhuyuanhao", ZYNumber1 + "001");
                }
                else
                {
                    SQFS.Add("zhuyuanhao", ZYNumber1);
                }
                SQFS.Add("shengao", textBoxshengao.Text);
                SQFS.Add("tizhong", textBoxtzhong.Text);
                SQFS.Add("nxssfs", textnxssfs.Text);
                SQFS.Add("zhushu", textBoxzhushu.Text);
                SQFS.Add("BP", textBoxBP.Text);
                SQFS.Add("R", textBoxR.Text);
                SQFS.Add("P", textBoxP.Text);
                SQFS.Add("T", textBoxT.Text);
                SQFS.Add("qita", textBoxQITA.Text);
                SQFS.Add("xingxueg", comboBoxxxg.Checked.ToString());
                AddItem = "";
                if (checkBoxxt.Checked) AddItem += "1";
                if (checkBoxxj.Checked) AddItem += "2";
                if (checkBoxbmbb.Checked) AddItem += "3";
                if (checkBoxzy.Checked) AddItem += "4";
                if (checkBoxgaoxy.Checked) AddItem += "5";
                if (checkBoxxg.Checked) AddItem += "6";
                if (checkBoxypl.Checked) AddItem += "7";
                if (checkBoxqj.Checked) AddItem += "8";
                SQFS.Add("xingxueg1", AddItem);
                SQFS.Add("xingxueg2", textBoxxxg2.Text);
                SQFS.Add("fhx", comboBoxfhx.Checked.ToString());
                AddItem = "";
                if (checkBoxcopd.Checked) AddItem += "1";
                if (checkBoxfy.Checked) AddItem += "2";
                if (checkBoxks.Checked) AddItem += "3";
                if (checkBoxkt.Checked) AddItem += "4";
                if (checkBoxqigy.Checked) AddItem += "5";
                if (checkBoxxc.Checked) AddItem += "6";
                if (checkBoxyTB.Checked) AddItem += "7";
                SQFS.Add("fhx1", AddItem);
                SQFS.Add("fhx2", textBoxfhx2.Text);
                SQFS.Add("biniao", textBoxbnsz.Checked.ToString());
                AddItem = "";
                if (checkBoxndz.Checked) AddItem += "1";
                if (checkBoxxueniao.Checked) AddItem += "2";
                if (checkBoxshengnbq.Checked) AddItem += "3";
                if (checkBoxyjq.Checked) AddItem += "4";
                SQFS.Add("biniao1", AddItem);
                SQFS.Add("biniao2", textBoxbnsz2.Text);
                SQFS.Add("xiaohua", textBoxxiaohua.Checked.ToString());
                AddItem = "";
                if (checkBoxganbing.Checked) AddItem += "1";
                if (checkBoxfanliu.Checked) AddItem += "2";
                if (checkBoxweizhuliu.Checked) AddItem += "3";
                if (checkBoxkuiyang.Checked) AddItem += "4";

                SQFS.Add("xiaohua1", AddItem);
                SQFS.Add("xiaohua2", textBoxxiaohua2.Text);
                SQFS.Add("sjjr", textBoxjirou.Checked.ToString());
                //sjjr,sjjr1,sjjr2,xuey,xuey1,
                AddItem = "";
                if (checkBoxzhongfeng.Checked) AddItem += "1";
                if (checkBoxchouchu.Checked) AddItem += "2";
                if (checkBoxzzjwl.Checked) AddItem += "3";
                if (checkBoxtanhuan.Checked) AddItem += "4";

                SQFS.Add("sjjr1", AddItem);
                SQFS.Add("sjjr2", textBoxsjjr2.Text);
                SQFS.Add("xuey", textBoxxy.Checked.ToString());
                SQFS.Add("xuey1", textxy.Text);
                SQFS.Add("xuey2", textBoxxy2.Text);
                SQFS.Add("nfb", textBoxnfb.Checked.ToString());
                AddItem = "";
                if (checkBoxtnb.Checked) AddItem += "1";
                if (checkBoxjk.Checked) AddItem += "2";
                if (checkBoxyds.Checked) AddItem += "3";
                if (checkBoxpiz.Checked) AddItem += "4";

                SQFS.Add("nfb1", AddItem);
                SQFS.Add("nfb2", textBoxnfb2.Text);
                SQFS.Add("js", textBoxjs.Checked.ToString());
                AddItem = "";
                if (checkBoxsfl.Checked) AddItem += "1";
                if (checkBoxyy.Checked) AddItem += "2";

                SQFS.Add("js1", AddItem);
                SQFS.Add("js2", textBoxjs2.Text);
                SQFS.Add("ck", textBoxck.Checked.ToString());

                
                AddItem = "";
                if (checkBoxhy.Checked) AddItem += "1";
                SQFS.Add("ck1", AddItem);
                SQFS.Add("ck2", textBoxck2.Text);
                SQFS.Add("xysj", textBoxxyyj.Checked.ToString());
                AddItem = "";
                if (checkBoxxiyan.Checked) AddItem += "1";
                if (checkBoxjieyan.Checked) AddItem += "2";
                if (checkBoxshijiu.Checked) AddItem += "3";
                if (checkBoxyaowuyl.Checked) AddItem += "4";

                SQFS.Add("xysj1", AddItem);
                SQFS.Add("xysj2", textBoxxysj2.Text);
                SQFS.Add("gns", textBoxguomings.Checked.ToString());
                AddItem = "";
                if (checkBoxywswgm.Checked) AddItem += "1";
                if (checkBoxymswm.Checked) AddItem += "2";
                if (checkBoxshoushum.Checked) AddItem += "3";

                SQFS.Add("gns1", AddItem);
                SQFS.Add("gns2", textBoxgms2.Text);
                SQFS.Add("mzs", textBoxmzs.Checked.ToString());
                AddItem = "";
                if (checkBoxcgkn.Checked) AddItem += "1";
                if (checkBoxmazygm.Checked) AddItem += "2";

                SQFS.Add("mzs1", AddItem);
                SQFS.Add("mzs2", textBoxmazs2.Text);
                SQFS.Add("ycb", textBoxycjb.Checked.ToString());


                AddItem = "";
                if (checkBoxjzsmzygm.Checked) AddItem += "1";
                if (checkBoxexgr.Checked) AddItem += "2";

                SQFS.Add("ycb1", AddItem);
                SQFS.Add("ycb2", textBoxyichuanb2.Text);
                SQFS.Add("mqyw", textBoxmqts.Checked.ToString());

                SQFS.Add("mqyw1", textBoxmqtsyw1.Text);
                SQFS.Add("mqyw2", textBoxmqtsyw2.Text);
                SQFS.Add("qsqk", textBoxqsqk.Checked.ToString());
                AddItem = "";
                if (checkBoxcha.Checked) AddItem += "1";
                if (checkBoxyiban.Checked) AddItem += "2";
                if (checkBoxhao.Checked) AddItem += "3";

                SQFS.Add("qsqk1", AddItem);
                SQFS.Add("qsqk2", textBoxqsqk2.Text);
                SQFS.Add("yszk", textBoxysqk.Checked.ToString());
                AddItem = "";
                if (checkBoxqingxing.Checked) AddItem += "1";
                if (checkBoxshishui.Checked) AddItem += "2";
                if (checkBoxhunmi.Checked) AddItem += "3";

                SQFS.Add("yszk1", AddItem);
                SQFS.Add("yszk2", textBoxyzk2.Text);
                SQFS.Add("qdtcd", textBoxqdtcd.Checked.ToString());
                AddItem = "";
                if (checkBoxzhangk.Checked) AddItem += "1";
                if (checkBoxhansheng.Checked) AddItem += "2";
                if (checkBoxjinduan.Checked) AddItem += "3";
                if (checkBoxthysx.Checked) AddItem += "4";
                if (checkBoxhjg.Checked) AddItem += "5";
                if (checkBoxxxg.Checked) AddItem += "6";
                if (checkBoxqgyw.Checked) AddItem += "7";
                if (checkBoxqgyp.Checked) AddItem += "8";
                if (checkBoxqgzl.Checked) AddItem += "9";
                if (checkBoxknzl.Checked) AddItem += "10";
                SQFS.Add("qdtcd1", AddItem);
                SQFS.Add("yc", textBoxyc.Checked.ToString());
                AddItem = "";
                if (checkBoxsongdong.Checked) AddItem += "1";
                if (checkBoxqueshi.Checked) AddItem += "2";
                if (checkBoxyici.Checked) AddItem += "3";
                if (checkBoxshangya.Checked) AddItem += "4";
                if (checkBoxxiaya.Checked) AddItem += "5";
                if (checkBoxbufen.Checked) AddItem += "6";
                if (checkBoxquanbu.Checked) AddItem += "7";
                SQFS.Add("yc1", AddItem);
                SQFS.Add("yk", textBoxyanke.Checked.ToString());
                AddItem = "";
                if (checkBoxtongkong.Checked) AddItem += "1";
                if (checkBoxqingguangyan.Checked) AddItem += "2";
                SQFS.Add("yk1", AddItem);
                SQFS.Add("yk2", textBoxyanke2.Text);
                SQFS.Add("mzccw", textBoxmzccbw.Checked.ToString());
                AddItem = "";
                if (checkBoxganran.Checked) AddItem += "1";
                if (checkBoxjixing.Checked) AddItem += "2";
                if (checkBoxwaishang.Checked) AddItem += "3";
                SQFS.Add("mzccw1", AddItem);
                SQFS.Add("mzccw2", textBoxmzccbw2.Text);
                SQFS.Add("xp", textBoxxiongpian.Checked.ToString());
                SQFS.Add("xp1", textBoxxiongpian2.Text);
                SQFS.Add("xdt", textBoxxindiantu.Text);
                SQFS.Add("xdt1", textBoxxindiantu1.Checked.ToString());
                SQFS.Add("HB", textBoxHB.Text);
                SQFS.Add("WBC", textBoxwbc.Text);
                SQFS.Add("PLT", textBoxplt.Text);
                SQFS.Add("K", textBoxK.Text);
                SQFS.Add("NA", textBoxNa.Text);
                SQFS.Add("C1", textBoxc1.Text);
                SQFS.Add("GLU", textBoxGLU.Text);
                // mzccw2,xp,xp1,xdt,HB,WBC,PLT,K,NA,C1,GLU,SGPT,BUN,CR,PT,APTT,PAO2,
                SQFS.Add("SGPT", textBoxSGPT.Text);
                SQFS.Add("BUN", textBoxBUN.Text);
                SQFS.Add("CR", textBoxCR.Text);
                SQFS.Add("PT", textBoxPT.Text);
                SQFS.Add("APTT", textBoxAPTT.Text);
                SQFS.Add("PAO2", textBoxPAO2.Text);
                //  ASAFJ,NYBW,MZJH,WTJY,YSQZ,RQ


                SQFS.Add("ASAFJ", comboBoxasa.Text);
                AddItem = "";
                if (checkBoxE.Checked) AddItem += "1";
                SQFS.Add("ASAFJ1", AddItem);
                SQFS.Add("NYBW", comboBoxynbw.Text);
                AddItem = "";
                if (checkBoxqsmz.Checked) AddItem += "1";
                if (checkBoxzgnm.Checked) AddItem += "2";
                if (checkBoxqyzz.Checked) AddItem += "3";
                if (checkBoxjbmz.Checked) AddItem += "4";
                if (checkBoxajhmz.Checked) AddItem += "5";
                if (checkBoxapdrmz.Checked) AddItem += "6";
                if (checkBoxzrmz.Checked) AddItem += "7";
                SQFS.Add("MZJH", AddItem);
                SQFS.Add("WTJY", textBoxwtjy.Text);
                SQFS.Add("YSQZ", comboBoxyshengqz.Text);
                SQFS.Add("RQ", Convert.ToDateTime(dateTimePicker1.Value.ToString()).ToString("yyyy-MM-dd HH:mm"));
                
                if (buttonbaocun.Enabled)
                {
                    SQFS.Add("IsRead", "0");
                }
                else
                {
                    SQFS.Add("IsRead", "1");
                }

                DataTable dt = DAL.Getkcyshengshuqianfs(ZYNumber1, dateTimePicker1.Value.ToString("yyyy-MM-dd"));//日期修改过注意

                if (dt.Rows.Count > 0)
                {
                    int s = DAL.Deletekcyishengshuqianfs(ZYNumber1);
                    result = DAL.KCInsertyshengshuqianfs(SQFS);
                    this.table1 = DAL.Getkcyshengshuqianfs(ZYNumber1, dateTimePicker1.Value.ToString("yyyy-MM-dd"));//日期修改过注意
                    if (this.table1_copy != null)
                    {
                        string v_conent = this.getEditContent(this.table1_copy.Rows[0], this.table1.Rows[0]);
                    }
                }
                else
                    result = DAL.KCInsertyshengshuqianfs(SQFS);
                if (result > 0)
                {
                    MessageBox.Show("保存成功！");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string getEditContent(DataRow row1, DataRow row2)
        {
            string v_content = "";
            foreach (DataColumn col in row1.Table.Columns)
            {
                if (object.Equals(row1[col.ColumnName], row2[col.ColumnName]) == false)
                {
                    string v_line = "[{0}]\n<原>{1}  <新>{2}\n";
                    v_line = String.Format(v_line, col.ColumnName, row1[col.ColumnName], row2[col.ColumnName]);
                    v_content += v_line;
                }
            }
            if (v_content == "")
            {
                return v_content;
            }
            else
            {
                string aa = "";
                DAL.updatexiugaijilu(DateTime.Now, Program.customer.user_name, ZYNumber1, aa, v_content, "医生术前访视");//1代表医生术前访视
            }
            return v_content;
        }
        private void buttoncundang_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = DAL.Getkcyshengshuqianfs(ZYNumber1);
            if (dt.Rows.Count == 0)
                MessageBox.Show("需要先保存，才能存档！");
            else
            {
                result = DAL.Updatayishengshuqianfs(ZYNumber1, 1);
                if (result > 0)
                {
                    MessageBox.Show("存档成功！");
                    isRead = true;
                    buttonbaocun.Enabled = false;
                    buttoncundang.Enabled = false;
                }
            }
        }

        private void JHmzqfsd_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm";

            BindMZYY();
            Load_info();
            LodSQFS_HS1();
            mzffbd();
            zqtzs();
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

        }
        private void mzffbd()
        {
            DataTable dtMZYS = DAL.mzff();
            comboBoxmzxz.Items.Clear();
            for (int i = 0; i < dtMZYS.Rows.Count; i++)
            {
                this.comboBoxmzxz.Items.Add(dtMZYS.Rows[i][0]);
            }
        }
        private void zqtzs()
        {
           
            DataTable dt = DAL.zqtzs(ZYNumber1);
            DataRow dr = dt.Rows[0];
            comboBoxmzxz.Text = dr["mzxz"].ToString();
            textBoxmzxzqt.Text = dr["mzxzqt"].ToString();
            textBoxqtzysx.Text = dr["qtsx"].ToString();
            textBoxmzysqz.Text = dr["mzysqz"].ToString();
            dateTimePickerRQ.Text = dr["rq"].ToString();
            if (Convert.ToString(dr["yqshzt"]).Contains("1")) checkBoxyqshzts.Checked = true;
            if (Convert.ToString(dr["yqshzt"]).Contains("2")) checkBoxyqshztf.Checked = true;
        }
        private void BindMZYY()
        {
            DataTable dtMZYS = DAL.GetAllMZYS();
            comboBoxyshengqz.Items.Clear();
            for (int i = 0; i < dtMZYS.Rows.Count; i++)
            {
                this.comboBoxyshengqz.Items.Add(dtMZYS.Rows[i][0]);
            }
        }
        public void Load_info()
        {
            DataTable dt1 = DAL.Getteykcwts(ZYNumber1);
            DataRow dr1 = dt1.Rows[0];

            textBoxxingming.Text = dr1["Patname"].ToString();
            textBoxsex.Text = dr1["patsex"].ToString();
            textBoxage.Text = dr1["patage"].ToString();
            textBoxzhuyuanhao.Text = dr1["patZhuyuanID"].ToString();
            textBoxkebie.Text = dr1["patdpm"].ToString();
            textBoxchanghao.Text = dr1["Patbedno"].ToString();
            textBoxshengao.Text = dr1["patheight"].ToString();
            textBoxtzhong.Text = dr1["patweight"].ToString();
            textBoxshuqianzd.Text = dr1["Pattmd"].ToString();
            textnxssfs.Text = dr1["Oname"].ToString();

        }
        private void LodSQFS_HS1()
        {
            DataTable dt = DAL.Getkcyshengshuqianfs(ZYNumber1);
            if (dt.Rows.Count > 0)
            {
                // MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                //DialogResult dr2 =  MessageBox.Show("当前病人存在术前访视是否新建?");
                if (MessageBox.Show("当前病人存在术前访视是否新建？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    xinjian = false;
                    return;
                }

                else//如果点击“取消”按钮
                {
                    DataRow dr = dt.Rows[0];
                    ///术前访视
                    /////id,zhuyuanhao,shengao,tizhong,zhushu,BP,R,P,T,qita,
                    //xingxueg,xingxueg1,xingxueg2,fhx,fhx1,fhx2,biniao,biniao1,
                    //biniao2,xiaohua,xiaohua1,xiaohua2,sjjr,sjjr1,sjjr2,xuey,xuey1,
                    //xuey2,nfb,nfb1,nfb2,js,js1,js2,ck,ck1,ck2,xysj,xysj1,xysj2,gns,
                    //gns1,gns2,mzs,mzs1,mzs2,ycb,ycb1,ycb2,mqyw,mqyw1,mqyw2,qsqk,qsqk1,
                    //qsqk2,yszk,yszk1,yszk2,qdtcd,qdtcd1,yc,yc1,yk,yk1,yk2,mzccw,mzccw1,
                    //mzccw2,xp,xp1,xdt,HB,WBC,PLT,K,NA,C1,GLU,SGPT,BUN,CR,PT,APTT,PAO2,
                    //ASAFJ,NYBW,MZJH,WTJY,YSQZ,RQ
                    textBoxshengao.Text = dr["shengao"].ToString();
                    textBoxtzhong.Text = dr["tizhong"].ToString();
                    textBoxzhushu.Text = dr["zhushu"].ToString();
                    textBoxBP.Text = dr["BP"].ToString();
                    textBoxR.Text = dr["R"].ToString();
                    textBoxP.Text = dr["P"].ToString();
                    textBoxT.Text = dr["T"].ToString();
                    textBoxQITA.Text = dr["qita"].ToString();
                    if (Convert.ToString(dr["xingxueg"]).Contains("True")) comboBoxxxg.Checked = true;
                   // comboBoxxxg.Text = dr["xingxueg"].ToString();
                    if (Convert.ToString(dr["xingxueg1"]).Contains("1")) checkBoxxt.Checked = true;
                    if (Convert.ToString(dr["xingxueg1"]).Contains("2")) checkBoxxj.Checked = true;
                    if (Convert.ToString(dr["xingxueg1"]).Contains("3")) checkBoxbmbb.Checked = true;
                    if (Convert.ToString(dr["xingxueg1"]).Contains("4")) checkBoxzy.Checked = true;
                    if (Convert.ToString(dr["xingxueg1"]).Contains("5")) checkBoxgaoxy.Checked = true;
                    if (Convert.ToString(dr["xingxueg1"]).Contains("6")) checkBoxxg.Checked = true;
                    if (Convert.ToString(dr["xingxueg1"]).Contains("7")) checkBoxypl.Checked = true;
                    if (Convert.ToString(dr["xingxueg1"]).Contains("8")) checkBoxqj.Checked = true;
                    textBoxxxg2.Text = dr["xingxueg2"].ToString();
                    if (Convert.ToString(dr["fhx"]).Contains("True")) comboBoxfhx.Checked = true;
                    //.Text = dr["fhx"].ToString();
                    if (Convert.ToString(dr["fhx1"]).Contains("1")) checkBoxcopd.Checked = true;
                    if (Convert.ToString(dr["fhx1"]).Contains("2")) checkBoxfy.Checked = true;
                    if (Convert.ToString(dr["fhx1"]).Contains("3")) checkBoxks.Checked = true;
                    if (Convert.ToString(dr["fhx1"]).Contains("4")) checkBoxkt.Checked = true;
                    if (Convert.ToString(dr["fhx1"]).Contains("5")) checkBoxqigy.Checked = true;
                    if (Convert.ToString(dr["fhx1"]).Contains("6")) checkBoxxc.Checked = true;
                    if (Convert.ToString(dr["fhx1"]).Contains("7")) checkBoxyTB.Checked = true;
                    textBoxfhx2.Text = dr["fhx2"].ToString();
                    if (Convert.ToString(dr["biniao"]).Contains("True")) textBoxbnsz.Checked = true;
                    //textBoxbnsz.Text = dr["biniao"].ToString();
                    if (Convert.ToString(dr["biniao1"]).Contains("1")) checkBoxndz.Checked = true;
                    if (Convert.ToString(dr["biniao1"]).Contains("2")) checkBoxxueniao.Checked = true;
                    if (Convert.ToString(dr["biniao1"]).Contains("3")) checkBoxshengnbq.Checked = true;
                    if (Convert.ToString(dr["biniao1"]).Contains("4")) checkBoxyjq.Checked = true;
                    textBoxbnsz2.Text = dr["biniao2"].ToString();
                    if (Convert.ToString(dr["xiaohua"]).Contains("True")) textBoxxiaohua.Checked = true;
                    //textBoxxiaohua.Text = dr["xiaohua"].ToString();
                    if (Convert.ToString(dr["xiaohua1"]).Contains("1")) checkBoxganbing.Checked = true;
                    if (Convert.ToString(dr["xiaohua1"]).Contains("2")) checkBoxfanliu.Checked = true;
                    if (Convert.ToString(dr["xiaohua1"]).Contains("3")) checkBoxweizhuliu.Checked = true;
                    if (Convert.ToString(dr["xiaohua1"]).Contains("4")) checkBoxkuiyang.Checked = true;
                    textBoxxiaohua2.Text = dr["xiaohua2"].ToString();

                    if (Convert.ToString(dr["sjjr"]).Contains("True")) textBoxjirou.Checked = true;
                    //textBoxjirou.Text = dr["sjjr"].ToString();
                    if (Convert.ToString(dr["sjjr1"]).Contains("1")) checkBoxzhongfeng.Checked = true;
                    if (Convert.ToString(dr["sjjr1"]).Contains("2")) checkBoxchouchu.Checked = true;
                    if (Convert.ToString(dr["sjjr1"]).Contains("3")) checkBoxzzjwl.Checked = true;
                    if (Convert.ToString(dr["sjjr1"]).Contains("4")) checkBoxtanhuan.Checked = true;
                    textBoxsjjr2.Text = dr["sjjr2"].ToString();
                    if (Convert.ToString(dr["xuey"]).Contains("True")) textBoxxy.Checked = true;
                    //textBoxxy.Text = dr["xuey"].ToString();
                    //if (Convert.ToString(dr["xuey1"]).Contains("1")) checkBoxcx.Checked = true;
                    //if (Convert.ToString(dr["xuey1"]).Contains("2")) checkBoxpingx.Checked = true;
                    textxy.Text = dr["xuey1"].ToString();
                    textBoxxy2.Text = dr["xuey2"].ToString();
                    if (Convert.ToString(dr["nfb"]).Contains("True")) textBoxnfb.Checked = true;
                    //textBoxnfb.Text = dr["nfb"].ToString();
                    if (Convert.ToString(dr["nfb1"]).Contains("1")) checkBoxtnb.Checked = true;
                    if (Convert.ToString(dr["nfb1"]).Contains("2")) checkBoxjk.Checked = true;
                    if (Convert.ToString(dr["nfb1"]).Contains("3")) checkBoxyds.Checked = true;
                    if (Convert.ToString(dr["nfb1"]).Contains("4")) checkBoxpiz.Checked = true;
                    textBoxnfb2.Text = dr["nfb2"].ToString();
                    if (Convert.ToString(dr["js"]).Contains("True")) textBoxjs.Checked = true;
                    //textBoxjs.Text = dr["js"].ToString();
                    if (Convert.ToString(dr["js1"]).Contains("1")) checkBoxsfl.Checked = true;
                    if (Convert.ToString(dr["js1"]).Contains("2")) checkBoxyy.Checked = true;
                    textBoxjs2.Text = dr["js2"].ToString();
                    if (Convert.ToString(dr["ck"]).Contains("True")) textBoxck.Checked = true;
                    //textBoxck.Text = dr["ck"].ToString();
                    if (Convert.ToString(dr["ck1"]).Contains("1")) checkBoxhy.Checked = true;
                    textBoxck2.Text = dr["ck2"].ToString();
                    if (Convert.ToString(dr["xysj"]).Contains("True")) textBoxxyyj.Checked = true;
                    //textBoxxyyj.Text = dr["xysj"].ToString();
                    if (Convert.ToString(dr["xysj1"]).Contains("1")) checkBoxxiyan.Checked = true;
                    if (Convert.ToString(dr["xysj1"]).Contains("2")) checkBoxjieyan.Checked = true;
                    if (Convert.ToString(dr["xysj1"]).Contains("3")) checkBoxshijiu.Checked = true;
                    if (Convert.ToString(dr["xysj1"]).Contains("4")) checkBoxyaowuyl.Checked = true;
                    textBoxxysj2.Text = dr["xysj2"].ToString();
                    if (Convert.ToString(dr["gns"]).Contains("True")) textBoxguomings.Checked = true;
                    //textBoxguomings.Text = dr["gns"].ToString();
                    if (Convert.ToString(dr["gns1"]).Contains("1")) checkBoxywswgm.Checked = true;
                    if (Convert.ToString(dr["gns1"]).Contains("2")) checkBoxymswm.Checked = true;
                    if (Convert.ToString(dr["gns1"]).Contains("3")) checkBoxshoushum.Checked = true;
                    textBoxgms2.Text = dr["gns2"].ToString();
                    if (Convert.ToString(dr["mzs"]).Contains("True")) textBoxmzs.Checked = true;
                    //textBoxmzs.Text = dr["mzs"].ToString();
                    if (Convert.ToString(dr["mzs1"]).Contains("1")) checkBoxcgkn.Checked = true;
                    if (Convert.ToString(dr["mzs1"]).Contains("2")) checkBoxmazygm.Checked = true;
                    textBoxmazs2.Text = dr["mzs2"].ToString();
                    if (Convert.ToString(dr["ycb"]).Contains("True")) textBoxycjb.Checked = true;
                    //textBoxycjb.Text = dr["ycb"].ToString();
                    if (Convert.ToString(dr["ycb1"]).Contains("1")) checkBoxjzsmzygm.Checked = true;
                    if (Convert.ToString(dr["ycb1"]).Contains("2")) checkBoxexgr.Checked = true;
                    
                    textBoxyichuanb2.Text = dr["ycb2"].ToString();
                    if (Convert.ToString(dr["mqyw"]).Contains("True")) textBoxmqts.Checked = true;
                    //textBoxmqts.Text = dr["mqyw"].ToString();
                    textBoxmqtsyw1.Text = dr["mqyw1"].ToString();
                    textBoxmqtsyw2.Text = dr["mqyw2"].ToString();
                    if (Convert.ToString(dr["qsqk"]).Contains("True")) textBoxqsqk.Checked = true;
                    //textBoxqsqk.Text = dr["qsqk"].ToString();
                    if (Convert.ToString(dr["qsqk1"]).Contains("1")) checkBoxcha.Checked = true;
                    if (Convert.ToString(dr["qsqk1"]).Contains("2")) checkBoxyiban.Checked = true;
                    if (Convert.ToString(dr["qsqk1"]).Contains("3")) checkBoxhao.Checked = true;
                    textBoxqsqk2.Text = dr["qsqk2"].ToString();
                    if (Convert.ToString(dr["yszk"]).Contains("True")) textBoxysqk.Checked = true;
                    //textBoxysqk.Text = dr["yszk"].ToString();
                    if (Convert.ToString(dr["yszk1"]).Contains("1")) checkBoxqingxing.Checked = true;
                    if (Convert.ToString(dr["yszk1"]).Contains("2")) checkBoxshishui.Checked = true;
                    if (Convert.ToString(dr["yszk1"]).Contains("3")) checkBoxhunmi.Checked = true;
                    textBoxyzk2.Text = dr["yszk2"].ToString();
                    if (Convert.ToString(dr["qdtcd"]).Contains("True")) textBoxqdtcd.Checked = true;
                    //textBoxqdtcd.Text = dr["qdtcd"].ToString();
                    if (Convert.ToString(dr["qdtcd1"]).Contains("1")) checkBoxzhangk.Checked = true;
                    if (Convert.ToString(dr["qdtcd1"]).Contains("2")) checkBoxhansheng.Checked = true;
                    if (Convert.ToString(dr["qdtcd1"]).Contains("3")) checkBoxjinduan.Checked = true;
                    if (Convert.ToString(dr["qdtcd1"]).Contains("4")) checkBoxthysx.Checked = true;
                    if (Convert.ToString(dr["qdtcd1"]).Contains("5")) checkBoxhjg.Checked = true;
                    if (Convert.ToString(dr["qdtcd1"]).Contains("6")) checkBoxxxg.Checked = true;
                    if (Convert.ToString(dr["qdtcd1"]).Contains("7")) checkBoxqgyw.Checked = true;
                    if (Convert.ToString(dr["qdtcd1"]).Contains("8")) checkBoxqgyp.Checked = true;
                    if (Convert.ToString(dr["qdtcd1"]).Contains("9")) checkBoxqgzl.Checked = true;
                    if (Convert.ToString(dr["qdtcd1"]).Contains("10")) checkBoxknzl.Checked = true;
                    if (Convert.ToString(dr["yc"]).Contains("True")) textBoxyc.Checked = true;
                    //textBoxyc.Text = dr["yc"].ToString();
                    if (Convert.ToString(dr["yc1"]).Contains("1")) checkBoxsongdong.Checked = true;
                    if (Convert.ToString(dr["yc1"]).Contains("2")) checkBoxqueshi.Checked = true;
                    if (Convert.ToString(dr["yc1"]).Contains("3")) checkBoxyici.Checked = true;
                    if (Convert.ToString(dr["yc1"]).Contains("4")) checkBoxshangya.Checked = true;
                    if (Convert.ToString(dr["yc1"]).Contains("5")) checkBoxxiaya.Checked = true;
                    if (Convert.ToString(dr["yc1"]).Contains("6")) checkBoxbufen.Checked = true;
                    if (Convert.ToString(dr["yc1"]).Contains("7")) checkBoxquanbu.Checked = true;
                    if (Convert.ToString(dr["yk"]).Contains("True")) textBoxyanke.Checked = true;
                    //textBoxyanke.Text = dr["yk"].ToString();
                    if (Convert.ToString(dr["yk1"]).Contains("1")) checkBoxtongkong.Checked = true;
                    if (Convert.ToString(dr["yk1"]).Contains("2")) checkBoxqingguangyan.Checked = true;
                    textBoxyanke2.Text = dr["yk2"].ToString();
                    if (Convert.ToString(dr["mzccw"]).Contains("True")) textBoxmzccbw.Checked = true;
                    //textBoxmzccbw.Text = dr["mzccw"].ToString();
                    if (Convert.ToString(dr["mzccw1"]).Contains("1")) checkBoxganran.Checked = true;
                    if (Convert.ToString(dr["mzccw1"]).Contains("2")) checkBoxjixing.Checked = true;
                    if (Convert.ToString(dr["mzccw1"]).Contains("3")) checkBoxwaishang.Checked = true;
                    textBoxmzccbw2.Text = dr["mzccw2"].ToString();
                    // mzccw2,xp,xp1,xdt,HB,WBC,PLT,K,NA,C1,GLU,SGPT,BUN,CR,PT,APTT,PAO2,
                    //ASAFJ,NYBW,MZJH,WTJY,YSQZ,RQ
                    if (Convert.ToString(dr["xp"]).Contains("True")) textBoxxiongpian.Checked = true;
                    //textBoxxiongpian.Text = dr["xp"].ToString();
                    textBoxxiongpian2.Text = dr["xp1"].ToString();
                    textBoxxindiantu.Text = dr["xdt"].ToString();
                    if (Convert.ToString(dr["xdt1"]).Contains("True")) textBoxxindiantu1.Checked = true;
                    //textBoxxindiantu1.Text = dr["xdt1"].ToString();
                    textBoxHB.Text = dr["HB"].ToString();
                    textBoxwbc.Text = dr["WBC"].ToString();
                    textBoxplt.Text = dr["PLT"].ToString();
                    textBoxK.Text = dr["K"].ToString();
                    textBoxNa.Text = dr["NA"].ToString();
                    textBoxc1.Text = dr["C1"].ToString();
                    textBoxGLU.Text = dr["GLU"].ToString();
                    textBoxSGPT.Text = dr["SGPT"].ToString();
                    textBoxBUN.Text = dr["BUN"].ToString();
                    textBoxCR.Text = dr["CR"].ToString();
                    textBoxPT.Text = dr["PT"].ToString();
                    textBoxAPTT.Text = dr["APTT"].ToString();
                    textBoxPAO2.Text = dr["PAO2"].ToString();
                    comboBoxasa.Text = dr["ASAFJ"].ToString();
                    if (Convert.ToString(dr["ASAFJ1"]).Contains("1")) checkBoxE.Checked = true;
                    comboBoxynbw.Text = dr["NYBW"].ToString();
                    if (Convert.ToString(dr["MZJH"]).Contains("1")) checkBoxqsmz.Checked = true;
                    if (Convert.ToString(dr["MZJH"]).Contains("2")) checkBoxzgnm.Checked = true;
                    if (Convert.ToString(dr["MZJH"]).Contains("3")) checkBoxqyzz.Checked = true;
                    if (Convert.ToString(dr["MZJH"]).Contains("4")) checkBoxjbmz.Checked = true;
                    if (Convert.ToString(dr["MZJH"]).Contains("5")) checkBoxajhmz.Checked = true;
                    if (Convert.ToString(dr["MZJH"]).Contains("6")) checkBoxapdrmz.Checked = true;
                    if (Convert.ToString(dr["MZJH"]).Contains("7")) checkBoxzrmz.Checked = true;
                    textBoxwtjy.Text = dr["WTJY"].ToString();
                    comboBoxyshengqz.Text = dr["YSQZ"].ToString();
                    dateTimePicker1.Text = dr["RQ"].ToString();


                    if (Convert.ToString(dr["IsRead"]) == "0")
                    {
                        buttonbaocun.Enabled = true;
                        //isRead = false;

                    }
                    if (Convert.ToString(dr["IsRead"]) == "1")
                    {
                        //isRead = true;
                        buttonbaocun.Enabled = false;
                        buttoncundang.Enabled = false;
                    }
                    //string jurisdiction = bll.Get_user_jurisdiction(Program.customer);
                    //if (jurisdiction.Contains("8"))
                    //{
                    //    btnJS.Visible = true;
                    //}
                    //else
                    //{
                    //    btnJS.Visible = false;
                    //}
                    //DataTable table1_copy = DAL.Getkcyshengshuqianfs(ZYNumber1);
                    this.table1_copy = DAL.Getkcyshengshuqianfs(ZYNumber1).Copy();

                    //this.table1.Copy();
                }

            }
            else
            {
                Isukey1();
            }

        }
        public void Isukey1()
        {
            //XjcaFgwATLLib.XjcaFgwATLLibClass v_object = new XjcaFgwATLLib.XjcaFgwATLLibClass();
            //string csp = "CIDC Cryptographic Service Provider v1.0.0";
            //string v_str = "从UKey中获取值\n------"
            //    + "\nSN: " + v_object.XJCA_GetCertSN(csp)
            //    + "\nDN: " + v_object.XJCA_GetCertDN(csp)
            //    + "\nXJCA_KeyInsert: " + v_object.XJCA_KeyInsert(csp);
            //v_str = v_object.XJCA_GetCertDN(csp);
            //string regexStr = " L=(\\d+),";
            //Regex r = new Regex(regexStr, RegexOptions.None);
            //Match mc = r.Match(v_str);
            //if (mc.Success)
            //{
            //    string dataStr = mc.Groups[1].Value;
            //    DataTable dt = DAL.Getkcysname(dataStr);
            //    string vv = dt.Rows[0][0].ToString();
            //    comboBoxyshengqz.Text = vv;
            //}
            //else
            //{
            //    MessageBox.Show("请插入正确的key");
            //    return;
            //}
        }

        private void btnDZBL_Click(object sender, EventArgs e)
        {
            string IPaddress = "192.168.1.31";
            bool flag = TextValueLimit.PingHost(IPaddress, 1000);
            if (flag == true)
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = "d:\\ReadEMR\\emrview.exe";

                //"f:\\ReadEMR\\emrview.exe";
                proc.StartInfo.Arguments = "1|" + ZYNumber1;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
            }
            else MessageBox.Show("电子病历 数据库未连接，请检查网络");
        }

        private void buttonyxbl_Click(object sender, EventArgs e)
        {
            //PACS_DB_Help db = new PACS_DB_Help();
            //string IPaddress = "192.168.1.46";
            //bool flag = TextValueLimit.PingHost(IPaddress, 1000);
            //if (flag == true)
            //{
            //    string inpatientID = ZYNumber1;
            //    string yxh = "";//影像号
            //    DataTable dt = db.GetPACS(ZYNumber1);
            //    if (dt.Rows.Count > 0)
            //    {
            //        yxh = dt.Rows[0]["zyid"].ToString();
            //    }
            //    else
            //    {
            //        MessageBox.Show("没有找到住院号对应的影像号！");
            //        return;
            //    }
            //    string url = "http://192.168.1.46:8080/ncpacs/view?pid=" + yxh + "&User=1&Password=1";
            //    System.Diagnostics.Process.Start(url);
            //}
            //else MessageBox.Show("影像病历 数据库未连接，请检查网络");
        }

        private void buttonjybl_Click(object sender, EventArgs e)
        {
            string zhuyuanhao = ZYNumber1;
            string url = "http://192.168.1.230:1080/modules/main/sysMain.aspx?&rmkj_userno=sm&HIS_PARAM_MODE=3&HIS_PARAM_PATNO=" + zhuyuanhao + "";
            System.Diagnostics.Process.Start(url);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string zhuyuanhao = ZYNumber1;
            string url = "http://192.168.1.13/MedExECGWebSetup/Pages/PatientByOneself.aspx?inpatientno=" + zhuyuanhao + "";
            System.Diagnostics.Process.Start(url);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string biaodanneir = "医生术前访视记录单";
            string a = "1";
            string aa = "4";
            DataTable dt1 = DAL.Getteshenhe(ZYNumber1, biaodanneir, Program.customer.user_name);//判断是否提交过
            if (dt1.Rows.Count > 0)
            {
                if (dt1.Rows[0]["leibie"].ToString() == "0")
                {
                    MessageBox.Show("已提交！");
                }
                else if (dt1.Rows[0]["leibie"].ToString() == "1")
                {
                    if (Program.customer.user_name == dt1.Rows[0]["shenqingren"].ToString() && biaodanneir == dt1.Rows[0]["biandanleixing"].ToString() && a == dt1.Rows[0]["leibie"].ToString() && ZYNumber1 == dt1.Rows[0]["zhuyuanhao"].ToString())//登录人,表单,审核
                    {
                        int result = 0;
                        DataTable dt = DAL.Getkcyshengshuqianfs(ZYNumber1);
                        if (dt.Rows[0]["isRead"].ToString() == "1")
                        {
                            result = DAL.Updatayishengshuqianfs(ZYNumber1, 0);
                            if (result > 0)
                            {
                                DAL.Updatemkcpacuxinxi修改4(ZYNumber1, aa, biaodanneir);
                                MessageBox.Show("解锁成功！");
                                buttonbaocun.Enabled = true;
                                isRead = false;
                                buttoncundang.Enabled = true;
                            }
                        }
                    }

                }
                else if (dt1.Rows[0]["leibie"].ToString() == "2")
                {
                    MessageBox.Show("审核不通过！");
                }
                else
                {
                    MessageBox.Show("已修改过，禁止再次修改！");
                }
            }
            else
            {
                tijiaoshenhe al = new tijiaoshenhe(ZYNumber1, biaodanneir);
                al.Show();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxkebie.Text == "")
            {
                MessageBox.Show("科别不能为空禁止打印!");
                textBoxkebie.Focus();
                return;
            }
            if (textBoxchanghao.Text == "")
            {
                MessageBox.Show("床号不能为空禁止打印！");
                textBoxchanghao.Focus();
                return;
            }
            if (textBoxzhuyuanhao.Text == "")
            {
                MessageBox.Show("住院号不能为空禁止打印！");
                textBoxzhuyuanhao.Focus();
                return;
            }
            if (textBoxxingming.Text == "")
            {
                MessageBox.Show("姓名不能为空禁止打印！");
                textBoxxingming.Focus();
                return;

            }
            if (textBoxsex.Text == "")
            {
                MessageBox.Show("性别不能为空禁止打印！");
                textBoxsex.Focus();
                return;
            }
            if (textBoxage.Text == "")
            {
                MessageBox.Show("年龄不能为空禁止打印！");
                textBoxage.Focus();
                return;
            }
            if (textBoxshuqianzd.Text == "")
            {
                MessageBox.Show("术前诊断不能为空禁止打印！");
                textBoxshuqianzd.Focus();
                return;
            }
            if (textnxssfs.Text == "")
            {
                MessageBox.Show("手术名称不能为空禁止打印！");
                textnxssfs.Focus();
                return;
            }
            if (comboBoxyshengqz.Text == "")
            {
                MessageBox.Show("评估医生不能为空！");
                comboBoxyshengqz.Focus();
                return;
            }
            if (textBoxBP.Text == "")
            {
                MessageBox.Show("BP不能为空！");
                textBoxBP.Focus();
                return;
            }
            if (textBoxR.Text == "")
            {
                MessageBox.Show("R不能为空！");
                textBoxR.Focus();
                return;
            }
            if (textBoxP.Text == "")
            {
                MessageBox.Show("P不能为空！");
                textBoxP.Focus();
                return;
            }
            if (textBoxT.Text == "")
            {
                MessageBox.Show("T不能为空！");
                textBoxP.Focus();
                return;
            }
            if (textBoxT.Text == "")
            {
                MessageBox.Show("T不能为空！");
                textBoxP.Focus();
                return;
            }
            if (comboBoxxxg.Text == "")
            {
                MessageBox.Show("心血管不能为空！");
                comboBoxxxg.Focus();
                return;
            }
            //if (comboBoxfhx1.Text == "")
            //{
            //    MessageBox.Show("肺和呼吸不能为空！");
            //    comboBoxfhx1.Focus();
            //    return;
            //}
            if (textxy.Text == "")
            {
                MessageBox.Show("血液不能为空！");
                textxy.Focus();
                return;
            }
            if (textBoxmqtsyw1.Text == "")
            {
                MessageBox.Show("特殊药物不能为空！");
                textBoxmqtsyw1.Focus();
                return;
            }
            if (textBoxxindiantu.Text == "")
            {
                MessageBox.Show("心电图不能为空！");
                textBoxxindiantu.Focus();
                return;
            }
            if (comboBoxasa.Text == "")
            {
                MessageBox.Show("ASA不能为空！");
                comboBoxasa.Focus();
                return;
            }
            if (comboBoxynbw.Text == "")
            {
                MessageBox.Show("是否饱胃不能为空！");
                comboBoxynbw.Focus();
                return;
            }
            panduan();
            a = 0;
        }
        public void panduan()
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            //printDocument1.DefaultPageSettings.PaperSize =
            //           new System.Drawing.Printing.PaperSize("K16", 740, 1020);
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("A4", 820, 1160);
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }
        public void Isukey()
        {
            //XjcaFgwATLLib.XjcaFgwATLLibClass v_object = new XjcaFgwATLLib.XjcaFgwATLLibClass();
            //string csp = "CIDC Cryptographic Service Provider v1.0.0";
            //string v_str = "从UKey中获取值\n------"
            //    + "\nSN: " + v_object.XJCA_GetCertSN(csp)
            //    + "\nDN: " + v_object.XJCA_GetCertDN(csp)
            //    + "\nXJCA_KeyInsert: " + v_object.XJCA_KeyInsert(csp);
            //v_str = v_object.XJCA_GetCertDN(csp);
            //string regexStr = " L=(\\d+),";
            //Regex r = new Regex(regexStr, RegexOptions.None);
            //Match mc = r.Match(v_str);
            //if (mc.Success)
            //{
            //    string dataStr = mc.Groups[1].Value;
            //    DataTable dt = DAL.Getkcysname(dataStr);
            //    string ysname = dt.Rows[0][0].ToString();
            //    if (comboBoxyshengqz.Text == ysname)
            //    {
                    
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("请插入正确的key");
            //    return;
            //}

        }

        int a = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen ptp = Pens.Black;//普通画笔
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);
            Pen pblack2 = new Pen(Brushes.Black, 2);
            Font ptzt9 = new Font("新宋体", 9);
            Font ptzt = new Font("新宋体", 11);//普通字体
            Font ptzt8 = new Font("新宋体", 8);//普通字体
            Font ptzt2 = new Font("黑体", 10, FontStyle.Bold);//加粗十号
            Font ptzt3 = new Font("新宋体", 18, FontStyle.Bold);//加粗14号

            int y = 100; int x = 70; int y1 = 0;
            if (a < 1)
            {
                string title1 = "浙江省金华市中医医院麻醉前访视单";
                e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 160, y));
                y = y + 40; y1 = y + 15;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawLine(ptp, x + 10, y, x + 10, y + 900);
                e.Graphics.DrawLine(ptp, x + 660, y, x + 660, y + 900);
                e.Graphics.DrawLine(ptp, x + 10, y + 900, x + 660, y + 900);
                e.Graphics.DrawLine(ptp, x + 210, y, x + 210, y + 80);
                e.Graphics.DrawLine(ptp, x + 440, y, x + 440, y + 100);
                e.Graphics.DrawLine(ptp, x + 210, y + 20, x + 440, y + 20);
                e.Graphics.DrawLine(ptp, x + 210, y + 40, x + 440, y + 40);
                e.Graphics.DrawLine(ptp, x + 340, y, x + 340, y + 20);
                e.Graphics.DrawLine(ptp, x + 10, y + 40, x + 660, y + 40);
                e.Graphics.DrawLine(ptp, x + 10, y + 80, x + 440, y + 80);
                e.Graphics.DrawLine(ptp, x + 10, y + 100, x + 640, y + 100);

                e.Graphics.DrawString("姓名：", ptzt, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawString("科别:", ptzt, Brushes.Black, new Point(x + 220, y));
                e.Graphics.DrawString(this.textBoxkebie.Text.Trim(), ptzt9, Brushes.Black, new Point(x + 250, y));
                e.Graphics.DrawString("床号：" + this.textBoxchanghao.Text.Trim(), ptzt, Brushes.Black, new Point(x + 340, y));
                e.Graphics.DrawString("术前诊断：", ptzt, Brushes.Black, new Point(x + 450, y));
                y = y + 20;
                e.Graphics.DrawString(textBoxxingming.Text, ptzt, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawString("住院号：" + this.textBoxzhuyuanhao.Text.Trim(), ptzt, Brushes.Black, new Point(x + 220, y));
                e.Graphics.DrawString(textBoxshuqianzd.Text, ptzt, Brushes.Black, new Point(x + 450, y));
                y = y + 20;
                e.Graphics.DrawString("年龄:" + this.textBoxage.Text.Trim() + "岁", ptzt, Brushes.Black, new Point(x + 15, y));
                e.Graphics.DrawString("性别：" + textBoxsex.Text.Trim() + "性", ptzt, Brushes.Black, new Point(x + 120, y));
                e.Graphics.DrawString("身高：" + textBoxshengao.Text.Trim() + "cm", ptzt, Brushes.Black, new Point(x + 215, y));
                e.Graphics.DrawString("体重：" + this.textBoxtzhong.Text.Trim() + "kg", ptzt, Brushes.Black, new Point(x + 320, y));
                e.Graphics.DrawString("拟行手术名称：", ptzt, Brushes.Black, new Point(x + 450, y));
                e.Graphics.DrawString(textnxssfs.Text, ptzt, Brushes.Black, new Point(x + 450, y + 20));
                y = y + 20;
                //e.Graphics.DrawString(textBoxshousmc.Text, ptzt, Brushes.Black, new Point(x + 450, y));
                //e.Graphics.DrawString("住院号：" + this.textBoxzhuyuanhao.Text.Trim(), ptzt, Brushes.Black, new Point(x + 460, y));

                //e.Graphics.DrawString("床号：" + this.txtch.Text.Trim(), ptzt, Brushes.Black, new Point(x + 300, y));
                y = y + 20;


                e.Graphics.DrawString("BP:" + textBoxBP.Text + "mmHg" + "  R:" + textBoxR.Text + "次/分" + "   P:" + textBoxP.Text + "次/分" + "  T:" + textBoxT.Text + "℃", ptzt, Brushes.Black, new Point(x + 10, y));
                //e.Graphics.DrawString("R:" + textBoxR.Text + "次/分", ptzt, Brushes.Black, new Point(x + 80, y));
                //e.Graphics.DrawString("P:" + textBoxP.Text + "次/分", ptzt, Brushes.Black, new Point(x + 150, y));
                //e.Graphics.DrawString("T:" + textBoxT.Text + "℃", ptzt, Brushes.Black, new Point(x + 230, y));

                y = y + 20;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("其他:" + textBoxQITA.Text, ptzt, Brushes.Black, new Point(x + 10, y));
                if (textBoxQITA.Text == "")
                {
                    e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 50, y));
                }
                y = y + 20;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("系统情况", ptzt, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawString("现在情况", ptzt, Brushes.Black, new Point(x + 210, y));
                e.Graphics.DrawString("过去情况", ptzt, Brushes.Black, new Point(x + 450, y));
                e.Graphics.DrawLine(ptp, x + 210, y, x + 210, y + 620);
                e.Graphics.DrawLine(ptp, x + 440, y, x + 440, y + 480);
                e.Graphics.DrawLine(ptp, x + 440, y + 540, x + 440, y + 580);
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("心血管", ptzt, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 80, y1, 10, 10);
                if (comboBoxxxg.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 80, y1, x + 85, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 85, y1 + 10, x + 90, y1);
                }
                e.Graphics.DrawString("有", ptzt, Brushes.Black, new Point(x + 90, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 130, y1, 10, 10);
                if (comboBoxxxg.Checked == false)
                {
                    e.Graphics.DrawLine(pblue2, x + 130, y1, x + 135, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 135, y1 + 10, x + 140, y1);
                }
                e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 140, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxxt.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("胸痛", ptzt, Brushes.Black, new Point(x + 225, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 265, y1, 10, 10);
                if (checkBoxxj.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 265, y1, x + 270, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 270, y1 + 10, x + 275, y1);
                }
                e.Graphics.DrawString("心悸", ptzt, Brushes.Black, new Point(x + 275, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 315, y1, 10, 10);
                if (checkBoxbmbb.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 315, y1, x + 320, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 320, y1 + 10, x + 325, y1);
                }
                e.Graphics.DrawString("瓣膜病变", ptzt, Brushes.Black, new Point(x + 325, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 395, y1, 10, 10);
                if (checkBoxzy.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 395, y1, x + 400, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 400, y1 + 10, x + 405, y1);
                }
                e.Graphics.DrawString("杂音", ptzt, Brushes.Black, new Point(x + 405, y));
                e.Graphics.DrawString(textBoxxxg2.Text, ptzt, Brushes.Black, new Point(x + 440, y));
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxgaoxy.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("高血压", ptzt, Brushes.Black, new Point(x + 225, y));

                e.Graphics.DrawRectangle(Pens.Black, x + 275, y1, 10, 10);
                if (checkBoxxg.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 275, y1, x + 280, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 280, y1 + 10, x + 285, y1);
                }
                e.Graphics.DrawString("心梗", ptzt, Brushes.Black, new Point(x + 285, y));

                e.Graphics.DrawRectangle(Pens.Black, x + 318, y1, 10, 10);
                if (checkBoxypl.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 318, y1, x + 323, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 323, y1 + 10, x + 328, y1);
                }
                e.Graphics.DrawString("易疲劳", ptzt, Brushes.Black, new Point(x + 328, y));

                e.Graphics.DrawRectangle(Pens.Black, x + 395, y1, 10, 10);
                if (checkBoxqj.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 395, y1, x + 400, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 400, y1 + 10, x + 405, y1);
                }
                e.Graphics.DrawString("气急", ptzt, Brushes.Black, new Point(x + 405, y));
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("肺和呼吸", ptzt, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 80, y1, 10, 10);
                if (comboBoxfhx.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 80, y1, x + 85, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 85, y1 + 10, x + 90, y1);
                }
                e.Graphics.DrawString("有", ptzt, Brushes.Black, new Point(x + 90, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 130, y1, 10, 10);
                if (comboBoxfhx.Checked == false)
                {
                    e.Graphics.DrawLine(pblue2, x + 130, y1, x + 135, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 135, y1 + 10, x + 140, y1);
                }
                e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 140, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxcopd.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("COPD", ptzt, Brushes.Black, new Point(x + 225, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 275, y1, 10, 10);
                if (checkBoxfy.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 275, y1, x + 280, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 280, y1 + 10, x + 285, y1);
                }
                e.Graphics.DrawString("肺炎", ptzt, Brushes.Black, new Point(x + 285, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 335, y1, 10, 10);
                if (checkBoxks.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 335, y1, x + 340, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 340, y1 + 10, x + 345, y1);
                }
                e.Graphics.DrawString("咳嗽", ptzt, Brushes.Black, new Point(x + 345, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 385, y1, 10, 10);
                if (checkBoxkt.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 385, y1, x + 390, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 390, y1 + 10, x + 395, y1);
                }
                e.Graphics.DrawString("咳痰", ptzt, Brushes.Black, new Point(x + 395, y));
                e.Graphics.DrawString(textBoxfhx2.Text, ptzt, Brushes.Black, new Point(x + 440, y));
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxqigy.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("气管炎", ptzt, Brushes.Black, new Point(x + 225, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 285, y1, 10, 10);
                if (checkBoxxc.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 285, y1, x + 290, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 290, y1 + 10, x + 295, y1);
                }
                e.Graphics.DrawString("哮喘", ptzt, Brushes.Black, new Point(x + 295, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 345, y1, 10, 10);
                if (checkBoxyTB.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 345, y1, x + 350, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 350, y1 + 10, x + 355, y1);
                }
                e.Graphics.DrawString("TB", ptzt, Brushes.Black, new Point(x + 355, y));

                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("泌尿生殖", ptzt, Brushes.Black, new Point(x + 10, y));

                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);

                if (checkBoxndz.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("尿毒症", ptzt, Brushes.Black, new Point(x + 225, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 275, y1, 10, 10);
                if (checkBoxxueniao.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 275, y1, x + 280, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 280, y1 + 10, x + 285, y1);
                }
                e.Graphics.DrawString("血尿", ptzt, Brushes.Black, new Point(x + 285, y));

                e.Graphics.DrawString(textBoxbnsz2.Text, ptzt, Brushes.Black, new Point(x + 440, y));
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawRectangle(Pens.Black, x + 80, y1, 10, 10);
                if (textBoxbnsz.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 80, y1, x + 85, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 85, y1 + 10, x + 90, y1);
                }
                e.Graphics.DrawString("有", ptzt, Brushes.Black, new Point(x + 90, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 130, y1, 10, 10);
                if (textBoxbnsz.Checked == false)
                {
                    e.Graphics.DrawLine(pblue2, x + 130, y1, x + 135, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 135, y1 + 10, x + 140, y1);
                }
                e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 140, y));
                //e.Graphics.DrawString(.Text, ptzt, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxshengnbq.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("肾功能不全", ptzt, Brushes.Black, new Point(x + 225, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 315, y1, 10, 10);
                if (checkBoxyjq.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 315, y1, x + 320, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 320, y1 + 10, x + 325, y1);
                }
                e.Graphics.DrawString("月经期", ptzt, Brushes.Black, new Point(x + 325, y));
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("消化", ptzt, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 80, y1, 10, 10);
                if (textBoxxiaohua.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 80, y1, x + 85, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 85, y1 + 10, x + 90, y1);
                }
                e.Graphics.DrawString("有", ptzt, Brushes.Black, new Point(x + 90, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 130, y1, 10, 10);
                if (textBoxxiaohua.Checked == false)
                {
                    e.Graphics.DrawLine(pblue2, x + 130, y1, x + 135, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 135, y1 + 10, x + 140, y1);
                }
                e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 140, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxganbing.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("肝病", ptzt, Brushes.Black, new Point(x + 225, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 265, y1, 10, 10);
                if (checkBoxfanliu.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 265, y1, x + 270, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 270, y1 + 10, x + 275, y1);
                }
                e.Graphics.DrawString("反流", ptzt, Brushes.Black, new Point(x + 275, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 315, y1, 10, 10);
                if (checkBoxweizhuliu.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 315, y1, x + 320, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 320, y1 + 10, x + 325, y1);
                }
                e.Graphics.DrawString("尿潴留", ptzt, Brushes.Black, new Point(x + 325, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 395, y1, 10, 10);
                if (checkBoxkuiyang.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 395, y1, x + 400, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 400, y1 + 10, x + 405, y1);
                }
                e.Graphics.DrawString("溃疡", ptzt, Brushes.Black, new Point(x + 405, y));
                e.Graphics.DrawString(textBoxxiaohua2.Text, ptzt, Brushes.Black, new Point(x + 440, y));


                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("神经肌肉", ptzt, Brushes.Black, new Point(x + 10, y));

                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxzhongfeng.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("中风", ptzt, Brushes.Black, new Point(x + 225, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 265, y1, 10, 10);
                if (checkBoxchouchu.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 265, y1, x + 270, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 270, y1 + 10, x + 275, y1);
                }
                e.Graphics.DrawString("抽搐", ptzt, Brushes.Black, new Point(x + 275, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 315, y1, 10, 10);
                if (checkBoxzzjwl.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 315, y1, x + 320, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 320, y1 + 10, x + 325, y1);
                }
                e.Graphics.DrawString("重症肌无力", ptzt, Brushes.Black, new Point(x + 325, y));
                e.Graphics.DrawString(textBoxsjjr2.Text, ptzt, Brushes.Black, new Point(x + 440, y));
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawRectangle(Pens.Black, x + 80, y1, 10, 10);
                if (textBoxjirou.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 80, y1, x + 85, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 85, y1 + 10, x + 90, y1);
                }
                e.Graphics.DrawString("有", ptzt, Brushes.Black, new Point(x + 90, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 130, y1, 10, 10);
                if (textBoxjirou.Checked == false)
                {
                    e.Graphics.DrawLine(pblue2, x + 130, y1, x + 135, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 135, y1 + 10, x + 140, y1);
                }
                e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 140, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxtanhuan.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("瘫痪", ptzt, Brushes.Black, new Point(x + 225, y));

                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("血液", ptzt, Brushes.Black, new Point(x + 10, y));

                e.Graphics.DrawRectangle(Pens.Black, x + 80, y1, 10, 10);
                if (textBoxxy.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 80, y1, x + 85, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 85, y1 + 10, x + 90, y1);
                }
                e.Graphics.DrawString("有", ptzt, Brushes.Black, new Point(x + 90, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 130, y1, 10, 10);
                if (textBoxxy.Checked == false)
                {
                    e.Graphics.DrawLine(pblue2, x + 130, y1, x + 135, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 135, y1 + 10, x + 140, y1);
                }
                e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 140, y));
                e.Graphics.DrawString(textxy.Text, ptzt, Brushes.Black, new Point(x + 215, y));
                e.Graphics.DrawString(textBoxxy2.Text, ptzt, Brushes.Black, new Point(x + 440, y));

                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("内分泌/代谢", ptzt, Brushes.Black, new Point(x + 10, y));

                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxtnb.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("糖尿病", ptzt, Brushes.Black, new Point(x + 225, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 285, y1, 10, 10);
                if (checkBoxjk.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 285, y1, x + 290, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 290, y1 + 10, x + 295, y1);
                }
                e.Graphics.DrawString("甲亢/底", ptzt, Brushes.Black, new Point(x + 295, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 365, y1, 10, 10);
                if (checkBoxyds.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 365, y1, x + 370, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 370, y1 + 10, x + 375, y1);
                }
                e.Graphics.DrawString("胰岛素", ptzt, Brushes.Black, new Point(x + 375, y));
                e.Graphics.DrawString(textBoxnfb2.Text, ptzt, Brushes.Black, new Point(x + 440, y));
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawRectangle(Pens.Black, x + 80, y1, 10, 10);
                if (textBoxnfb.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 80, y1, x + 85, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 85, y1 + 10, x + 90, y1);
                }
                e.Graphics.DrawString("有", ptzt, Brushes.Black, new Point(x + 90, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 130, y1, 10, 10);
                if (textBoxnfb.Checked == false)
                {
                    e.Graphics.DrawLine(pblue2, x + 130, y1, x + 135, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 135, y1 + 10, x + 140, y1);
                }
                e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 140, y));

                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxpiz.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("皮质", ptzt, Brushes.Black, new Point(x + 225, y));

                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("精神", ptzt, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 80, y1, 10, 10);
                if (textBoxjs.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 80, y1, x + 85, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 85, y1 + 10, x + 90, y1);
                }
                e.Graphics.DrawString("有", ptzt, Brushes.Black, new Point(x + 90, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 130, y1, 10, 10);
                if (textBoxjs.Checked == false)
                {
                    e.Graphics.DrawLine(pblue2, x + 130, y1, x + 135, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 135, y1 + 10, x + 140, y1);
                }
                e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 140, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxsfl.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("精神分裂", ptzt, Brushes.Black, new Point(x + 225, y));

                e.Graphics.DrawRectangle(Pens.Black, x + 315, y1, 10, 10);
                if (checkBoxyy.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 315, y1, x + 320, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 320, y1 + 10, x + 325, y1);
                }
                e.Graphics.DrawString("抑郁", ptzt, Brushes.Black, new Point(x + 325, y));
                e.Graphics.DrawString(textBoxjs2.Text, ptzt, Brushes.Black, new Point(x + 440, y));
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("产科", ptzt, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 80, y1, 10, 10);
                if (textBoxck.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 80, y1, x + 85, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 85, y1 + 10, x + 90, y1);
                }
                e.Graphics.DrawString("有", ptzt, Brushes.Black, new Point(x + 90, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 130, y1, 10, 10);
                if (textBoxck.Checked == false)
                {
                    e.Graphics.DrawLine(pblue2, x + 130, y1, x + 135, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 135, y1 + 10, x + 140, y1);
                }
                e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 140, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxsfl.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("怀孕", ptzt, Brushes.Black, new Point(x + 225, y));
                e.Graphics.DrawString(textBoxck2.Text, ptzt, Brushes.Black, new Point(x + 440, y));


                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);





                e.Graphics.DrawString("吸烟、嗜酒、药物依赖", ptzt9, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 140, y1, 10, 10);
                if (textBoxxyyj.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 140, y1, x + 145, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 145, y1 + 10, x + 150, y1);
                }
                e.Graphics.DrawString("有", ptzt, Brushes.Black, new Point(x + 150, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 180, y1, 10, 10);
                if (textBoxxyyj.Checked == false)
                {
                    e.Graphics.DrawLine(pblue2, x + 180, y1, x + 185, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 185, y1 + 10, x + 190, y1);
                }
                e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 190, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxxiyan.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("吸烟", ptzt, Brushes.Black, new Point(x + 225, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 265, y1, 10, 10);
                if (checkBoxjieyan.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 265, y1, x + 270, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 270, y1 + 10, x + 275, y1);
                }
                e.Graphics.DrawString("戒烟", ptzt, Brushes.Black, new Point(x + 275, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 315, y1, 10, 10);
                if (checkBoxshijiu.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 315, y1, x + 320, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 320, y1 + 10, x + 325, y1);
                }
                e.Graphics.DrawString("嗜酒", ptzt, Brushes.Black, new Point(x + 325, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 365, y1, 10, 10);
                if (checkBoxyaowuyl.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 365, y1, x + 370, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 370, y1 + 10, x + 375, y1);
                }
                e.Graphics.DrawString("药物依赖", ptzt, Brushes.Black, new Point(x + 375, y));
                e.Graphics.DrawString(textBoxxysj2.Text, ptzt, Brushes.Black, new Point(x + 440, y));

                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("过敏史、手术史", ptzt, Brushes.Black, new Point(x + 10, y));

                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxywswgm.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("药物/食物过敏", ptzt, Brushes.Black, new Point(x + 225, y));

                e.Graphics.DrawRectangle(Pens.Black, x + 335, y1, 10, 10);
                if (checkBoxymswm.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 335, y1, x + 340, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 340, y1 + 10, x + 345, y1);
                }
                e.Graphics.DrawString("药名/食物名", ptzt, Brushes.Black, new Point(x + 345, y));

                e.Graphics.DrawString(textBoxgms2.Text, ptzt, Brushes.Black, new Point(x + 440, y));
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawRectangle(Pens.Black, x + 80, y1, 10, 10);
                if (textBoxguomings.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 80, y1, x + 85, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 85, y1 + 10, x + 90, y1);
                }
                e.Graphics.DrawString("有", ptzt, Brushes.Black, new Point(x + 90, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 130, y1, 10, 10);
                if (textBoxguomings.Checked == false)
                {
                    e.Graphics.DrawLine(pblue2, x + 130, y1, x + 135, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 135, y1 + 10, x + 140, y1);
                }
                e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 140, y));

                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxshoushum.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("手术名", ptzt, Brushes.Black, new Point(x + 225, y));

                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("既往麻醉史", ptzt, Brushes.Black, new Point(x + 10, y));

                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxcgkn.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("插管困难", ptzt, Brushes.Black, new Point(x + 225, y));

                e.Graphics.DrawRectangle(Pens.Black, x + 315, y1, 10, 10);
                if (checkBoxmazygm.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 315, y1, x + 320, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 320, y1 + 10, x + 325, y1);
                }
                e.Graphics.DrawString("麻醉药过敏", ptzt, Brushes.Black, new Point(x + 325, y));

                e.Graphics.DrawString(textBoxmazs2.Text, ptzt, Brushes.Black, new Point(x + 440, y));
                y = y + 20; y1 = y + 3;

                e.Graphics.DrawRectangle(Pens.Black, x + 80, y1, 10, 10);
                if (textBoxmzs.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 80, y1, x + 85, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 85, y1 + 10, x + 90, y1);
                }
                e.Graphics.DrawString("有", ptzt, Brushes.Black, new Point(x + 90, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 130, y1, 10, 10);
                if (textBoxmzs.Checked == false)
                {
                    e.Graphics.DrawLine(pblue2, x + 130, y1, x + 135, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 135, y1 + 10, x + 140, y1);
                }
                e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 140, y));

                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("家族史/遗传疾病", ptzt, Brushes.Black, new Point(x + 10, y));

                e.Graphics.DrawRectangle(Pens.Black, x + 130, y1, 10, 10);
                if (textBoxycjb.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 130, y1, x + 135, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 135, y1 + 10, x + 140, y1);
                }
                e.Graphics.DrawString("有", ptzt, Brushes.Black, new Point(x + 140, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 160, y1, 10, 10);
                if (textBoxycjb.Checked == false)
                {
                    e.Graphics.DrawLine(pblue2, x + 160, y1, x + 165, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 165, y1 + 10, x + 170, y1);
                }
                e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 170, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxjzsmzygm.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("麻醉药过敏", ptzt, Brushes.Black, new Point(x + 225, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 315, y1, 10, 10);
                if (checkBoxexgr.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 315, y1, x + 320, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 320, y1 + 10, x + 325, y1);
                }
                e.Graphics.DrawString("恶性高热", ptzt, Brushes.Black, new Point(x + 325, y));
                e.Graphics.DrawString(textBoxyichuanb2.Text, ptzt, Brushes.Black, new Point(x + 440, y));

                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("目前特殊药物", ptzt, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawString(textBoxmqtsyw1.Text, ptzt, Brushes.Black, new Point(x + 230, y));
                e.Graphics.DrawString(textBoxmqtsyw2.Text, ptzt, Brushes.Black, new Point(x + 440, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 120, y1, 10, 10);
                if (textBoxck.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 120, y1, x + 125, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 125, y1 + 10, x + 130, y1);
                }
                e.Graphics.DrawString("有", ptzt, Brushes.Black, new Point(x + 130, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 170, y1, 10, 10);
                if (textBoxck.Checked == false)
                {
                    e.Graphics.DrawLine(pblue2, x + 170, y1, x + 175, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 175, y1 + 10, x + 180, y1);
                }
                e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 180, y));
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("全身情况", ptzt, Brushes.Black, new Point(x + 10, y));

                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxcha.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("差", ptzt, Brushes.Black, new Point(x + 225, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 265, y1, 10, 10);
                if (checkBoxyiban.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 265, y1, x + 270, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 270, y1 + 10, x + 275, y1);
                }
                e.Graphics.DrawString("一般", ptzt, Brushes.Black, new Point(x + 275, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 315, y1, 10, 10);
                if (checkBoxhao.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 315, y1, x + 320, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 320, y1 + 10, x + 325, y1);
                }
                e.Graphics.DrawString("好", ptzt, Brushes.Black, new Point(x + 325, y));

                e.Graphics.DrawString(textBoxqsqk2.Text, ptzt, Brushes.Black, new Point(x + 440, y));

                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("意识状况", ptzt, Brushes.Black, new Point(x + 10, y));

                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxqingxing.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("清醒", ptzt, Brushes.Black, new Point(x + 225, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 265, y1, 10, 10);
                if (checkBoxshishui.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 265, y1, x + 270, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 270, y1 + 10, x + 275, y1);
                }
                e.Graphics.DrawString("嗜睡", ptzt, Brushes.Black, new Point(x + 275, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 315, y1, 10, 10);
                if (checkBoxhunmi.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 315, y1, x + 320, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 320, y1 + 10, x + 325, y1);
                }
                e.Graphics.DrawString("昏迷", ptzt, Brushes.Black, new Point(x + 325, y));

                e.Graphics.DrawString(textBoxyzk2.Text, ptzt, Brushes.Black, new Point(x + 440, y));
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("气道通畅度", ptzt, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 100, y1, 10, 10);
                if (textBoxqdtcd.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 100, y1, x + 105, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 105, y1 + 10, x + 110, y1);
                }
                e.Graphics.DrawString("有", ptzt, Brushes.Black, new Point(x + 110, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 130, y1, 10, 10);
                if (textBoxqdtcd.Checked == false)
                {
                    e.Graphics.DrawLine(pblue2, x + 130, y1, x + 135, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 135, y1 + 10, x + 140, y1);
                }
                e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 140, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxzhangk.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("张口<3cm", ptzt, Brushes.Black, new Point(x + 225, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 295, y1, 10, 10);
                if (checkBoxhansheng.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 295, y1, x + 300, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 300, y1 + 10, x + 305, y1);
                }
                e.Graphics.DrawString("鼾声", ptzt, Brushes.Black, new Point(x + 305, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 345, y1, 10, 10);
                if (checkBoxjinduan.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 345, y1, x + 350, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 350, y1 + 10, x + 355, y1);
                }
                e.Graphics.DrawString("颈短", ptzt, Brushes.Black, new Point(x + 355, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 395, y1, 10, 10);
                if (checkBoxthysx.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 395, y1, x + 400, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 400, y1 + 10, x + 405, y1);
                }
                e.Graphics.DrawString("头后仰受限", ptzt, Brushes.Black, new Point(x + 405, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 495, y1, 10, 10);
                if (checkBoxhjg.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 495, y1, x + 500, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 500, y1 + 10, x + 505, y1);
                }
                e.Graphics.DrawString("喉结高", ptzt, Brushes.Black, new Point(x + 505, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 555, y1, 10, 10);
                if (checkBoxxxg.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 555, y1, x + 560, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 560, y1 + 10, x + 565, y1);
                }
                e.Graphics.DrawString("小下颌", ptzt, Brushes.Black, new Point(x + 565, y));
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxqgyw.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("气管移位", ptzt, Brushes.Black, new Point(x + 225, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 295, y1, 10, 10);
                if (checkBoxqgyp.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 295, y1, x + 300, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 300, y1 + 10, x + 305, y1);
                }
                e.Graphics.DrawString("气管压迫", ptzt, Brushes.Black, new Point(x + 305, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 375, y1, 10, 10);
                if (checkBoxqgzl.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 375, y1, x + 380, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 380, y1 + 10, x + 385, y1);
                }
                e.Graphics.DrawString("气管肿瘤", ptzt, Brushes.Black, new Point(x + 385, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 455, y1, 10, 10);
                if (checkBoxknzl.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 455, y1, x + 460, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 460, y1 + 10, x + 465, y1);
                }
                e.Graphics.DrawString("口内肿瘤", ptzt, Brushes.Black, new Point(x + 465, y));
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("牙齿", ptzt, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 80, y1, 10, 10);
                if (textBoxyc.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 80, y1, x + 85, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 85, y1 + 10, x + 90, y1);
                }
                e.Graphics.DrawString("有", ptzt, Brushes.Black, new Point(x + 90, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 130, y1, 10, 10);
                if (textBoxyc.Checked == false)
                {
                    e.Graphics.DrawLine(pblue2, x + 130, y1, x + 135, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 135, y1 + 10, x + 140, y1);
                }
                e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 140, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxsongdong.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("松动", ptzt, Brushes.Black, new Point(x + 225, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 265, y1, 10, 10);
                if (checkBoxqueshi.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 265, y1, x + 270, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 270, y1 + 10, x + 275, y1);
                }
                e.Graphics.DrawString("缺失", ptzt, Brushes.Black, new Point(x + 275, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 315, y1, 10, 10);
                if (checkBoxyici.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 315, y1, x + 320, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 320, y1 + 10, x + 325, y1);
                }
                e.Graphics.DrawString("义齿 （", ptzt, Brushes.Black, new Point(x + 325, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 365, y1, 10, 10);
                if (checkBoxshangya.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 365, y1, x + 370, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 370, y1 + 10, x + 375, y1);
                }
                e.Graphics.DrawString("上牙", ptzt, Brushes.Black, new Point(x + 375, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 415, y1, 10, 10);
                if (checkBoxxiaya.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 415, y1, x + 420, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 420, y1 + 10, x + 425, y1);
                }
                e.Graphics.DrawString("下牙", ptzt, Brushes.Black, new Point(x + 425, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 465, y1, 10, 10);
                if (checkBoxbufen.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 465, y1, x + 470, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 470, y1 + 10, x + 475, y1);
                }
                e.Graphics.DrawString("部分", ptzt, Brushes.Black, new Point(x + 475, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 515, y1, 10, 10);
                if (checkBoxquanbu.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 515, y1, x + 520, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 520, y1 + 10, x + 525, y1);
                }
                e.Graphics.DrawString("全部）", ptzt, Brushes.Black, new Point(x + 525, y));

                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("眼科", ptzt, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 80, y1, 10, 10);
                if (textBoxyanke.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 80, y1, x + 85, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 85, y1 + 10, x + 90, y1);
                }
                e.Graphics.DrawString("有", ptzt, Brushes.Black, new Point(x + 90, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 130, y1, 10, 10);
                if (textBoxyanke.Checked == false)
                {
                    e.Graphics.DrawLine(pblue2, x + 130, y1, x + 135, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 135, y1 + 10, x + 140, y1);
                }
                e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 140, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxtongkong.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("瞳孔异常", ptzt, Brushes.Black, new Point(x + 225, y));

                e.Graphics.DrawRectangle(Pens.Black, x + 315, y1, 10, 10);
                if (checkBoxqingguangyan.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 315, y1, x + 320, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 320, y1 + 10, x + 325, y1);
                }
                e.Graphics.DrawString("青光眼", ptzt, Brushes.Black, new Point(x + 325, y));
                e.Graphics.DrawString(textBoxyanke2.Text, ptzt, Brushes.Black, new Point(x + 440, y));

                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("麻醉穿刺部位", ptzt, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 125, y1, 10, 10);
                if (textBoxmzccbw.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 125, y1, x + 130, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 130, y1 + 10, x + 135, y1);
                }
                e.Graphics.DrawString("有", ptzt, Brushes.Black, new Point(x + 135, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 165, y1, 10, 10);
                if (textBoxmzccbw.Checked == false)
                {
                    e.Graphics.DrawLine(pblue2, x + 165, y1, x + 170, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 170, y1 + 10, x + 175, y1);
                }
                e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 175, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 215, y1, 10, 10);
                if (checkBoxganran.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 215, y1, x + 220, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 220, y1 + 10, x + 225, y1);
                }
                e.Graphics.DrawString("感染", ptzt, Brushes.Black, new Point(x + 225, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 265, y1, 10, 10);
                if (checkBoxjixing.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 265, y1, x + 270, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 270, y1 + 10, x + 275, y1);
                }
                e.Graphics.DrawString("畸形", ptzt, Brushes.Black, new Point(x + 275, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 315, y1, 10, 10);
                if (checkBoxwaishang.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 315, y1, x + 320, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 320, y1 + 10, x + 325, y1);
                }
                e.Graphics.DrawString("外伤", ptzt, Brushes.Black, new Point(x + 325, y));

                e.Graphics.DrawString(textBoxmzccbw2.Text, ptzt, Brushes.Black, new Point(x + 440, y));
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("胸片X片", ptzt, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 80, y1, 10, 10);
                if (textBoxxiongpian.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 80, y1, x + 85, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 85, y1 + 10, x + 90, y1);
                }
                e.Graphics.DrawString("有", ptzt, Brushes.Black, new Point(x + 90, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 130, y1, 10, 10);
                if (textBoxxiongpian.Checked == false)
                {
                    e.Graphics.DrawLine(pblue2, x + 130, y1, x + 135, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 135, y1 + 10, x + 140, y1);
                }
                e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 140, y));
                e.Graphics.DrawString(textBoxxiongpian2.Text, ptzt, Brushes.Black, new Point(x + 230, y));
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("心电图", ptzt, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 80, y1, 10, 10);
                if (textBoxxindiantu1.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 80, y1, x + 85, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 85, y1 + 10, x + 90, y1);
                }
                e.Graphics.DrawString("有", ptzt, Brushes.Black, new Point(x + 90, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 130, y1, 10, 10);
                if (textBoxxindiantu1.Checked == false)
                {
                    e.Graphics.DrawLine(pblue2, x + 130, y1, x + 135, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 135, y1 + 10, x + 140, y1);
                }
                e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 140, y));
                e.Graphics.DrawString(textBoxxindiantu.Text, ptzt, Brushes.Black, new Point(x + 210, y));
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                int v = 50;
                e.Graphics.DrawString("Hb", ptzt, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawString("WBC", ptzt, Brushes.Black, new Point(x + 10 + v * 1, y));
                e.Graphics.DrawString("PLT", ptzt, Brushes.Black, new Point(x + 10 + v * 2, y));
                e.Graphics.DrawString("K", ptzt, Brushes.Black, new Point(x + 10 + v * 3, y));
                e.Graphics.DrawString("Na", ptzt, Brushes.Black, new Point(x + 10 + v * 4, y));
                e.Graphics.DrawString("C1", ptzt, Brushes.Black, new Point(x + 10 + v * 5, y));
                e.Graphics.DrawString("GLU", ptzt, Brushes.Black, new Point(x + 10 + v * 6, y));
                e.Graphics.DrawString("SGPT", ptzt, Brushes.Black, new Point(x + 10 + v * 7, y));
                e.Graphics.DrawString("BUN", ptzt, Brushes.Black, new Point(x + 10 + v * 8, y));
                e.Graphics.DrawString("Cr", ptzt, Brushes.Black, new Point(x + 10 + v * 9, y));
                e.Graphics.DrawString("PT", ptzt, Brushes.Black, new Point(x + 10 + v * 10, y));
                e.Graphics.DrawString("APTT", ptzt, Brushes.Black, new Point(x + 10 + v * 11, y));
                e.Graphics.DrawString("PaO2", ptzt, Brushes.Black, new Point(x + 10 + v * 12, y));
                int f = 50;
                e.Graphics.DrawLine(ptp, x + f, y, x + f, y + 40);
                e.Graphics.DrawLine(ptp, x + f * 2, y, x + f * 2, y + 40);
                e.Graphics.DrawLine(ptp, x + f * 3, y, x + f * 3, y + 40);
                e.Graphics.DrawLine(ptp, x + f * 4, y, x + f * 4, y + 40);
                e.Graphics.DrawLine(ptp, x + f * 5, y, x + f * 5, y + 40);
                e.Graphics.DrawLine(ptp, x + f * 6, y, x + f * 6, y + 40);
                e.Graphics.DrawLine(ptp, x + f * 7, y, x + f * 7, y + 40);
                e.Graphics.DrawLine(ptp, x + f * 8, y, x + f * 8, y + 40);
                e.Graphics.DrawLine(ptp, x + f * 9, y, x + f * 9, y + 40);
                e.Graphics.DrawLine(ptp, x + f * 10, y, x + f * 10, y + 40);
                e.Graphics.DrawLine(ptp, x + f * 11, y, x + f * 11, y + 40);
                e.Graphics.DrawLine(ptp, x + f * 12, y, x + f * 12, y + 40);


                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                int k = 50;
                if (textBoxHB.Text == "")
                {
                    e.Graphics.DrawLine(ptp, x + 10, y + 20, x + 40, y);
                }
                e.Graphics.DrawString(textBoxHB.Text, ptzt, Brushes.Black, new Point(x + 10, y));
                if (textBoxwbc.Text == "")
                {
                    e.Graphics.DrawLine(ptp, x + 10 + k * 1, y + 20, x + 10 + k * 1 + 40, y);
                }
                e.Graphics.DrawString(textBoxwbc.Text, ptzt, Brushes.Black, new Point(x + 10 + k * 1, y));
                if (textBoxplt.Text == "")
                {
                    e.Graphics.DrawLine(ptp, x + 10 + k * 2, y + 20, x + 10 + k * 2 + 40, y);
                }
                e.Graphics.DrawString(textBoxplt.Text, ptzt, Brushes.Black, new Point(x + 10 + k * 2, y));

                if (textBoxK.Text == "")
                {
                    e.Graphics.DrawLine(ptp, x + 10 + k * 3, y + 20, x + 10 + k * 3 + 40, y);
                }
                e.Graphics.DrawString(textBoxK.Text, ptzt, Brushes.Black, new Point(x + 10 + k * 3, y));
                if (textBoxNa.Text == "")
                {
                    e.Graphics.DrawLine(ptp, x + 10 + k * 4, y + 20, x + 10 + k * 4 + 40, y);
                }
                e.Graphics.DrawString(textBoxNa.Text, ptzt, Brushes.Black, new Point(x + 10 + k * 4, y));
                if (textBoxc1.Text == "")
                {
                    e.Graphics.DrawLine(ptp, x + 10 + k * 5, y + 20, x + 10 + k * 5 + 40, y);
                }
                e.Graphics.DrawString(textBoxc1.Text, ptzt, Brushes.Black, new Point(x + 10 + k * 5, y));
                if (textBoxGLU.Text == "")
                {
                    e.Graphics.DrawLine(ptp, x + 10 + k * 6, y + 20, x + 10 + k * 6 + 40, y);
                }
                e.Graphics.DrawString(textBoxGLU.Text, ptzt, Brushes.Black, new Point(x + 10 + k * 6, y));
                if (textBoxSGPT.Text == "")
                {
                    e.Graphics.DrawLine(ptp, x + 10 + k * 7, y + 20, x + 10 + k * 7 + 40, y);
                }
                e.Graphics.DrawString(textBoxSGPT.Text, ptzt, Brushes.Black, new Point(x + 10 + k * 7, y));
                if (textBoxBUN.Text == "")
                {
                    e.Graphics.DrawLine(ptp, x + 10 + k * 8, y + 20, x + 10 + k * 8 + 40, y);
                }
                e.Graphics.DrawString(textBoxBUN.Text, ptzt, Brushes.Black, new Point(x + 10 + k * 8, y));
                if (textBoxCR.Text == "")
                {
                    e.Graphics.DrawLine(ptp, x + 10 + k * 9, y + 20, x + 10 + k * 9 + 40, y);
                }
                e.Graphics.DrawString(textBoxCR.Text, ptzt, Brushes.Black, new Point(x + 10 + k * 9, y));
                if (textBoxPT.Text == "")
                {
                    e.Graphics.DrawLine(ptp, x + 10 + k * 10, y + 20, x + 10 + k * 10 + 40, y);
                }
                e.Graphics.DrawString(textBoxPT.Text, ptzt, Brushes.Black, new Point(x + 10 + k * 10, y));
                if (textBoxAPTT.Text == "")
                {
                    e.Graphics.DrawLine(ptp, x + 10 + k * 11, y + 20, x + 10 + k * 11 + 40, y);
                }
                e.Graphics.DrawString(textBoxAPTT.Text, ptzt, Brushes.Black, new Point(x + 10 + k * 11, y));
                if (textBoxPAO2.Text == "")
                {
                    e.Graphics.DrawLine(ptp, x + 10 + k * 12, y + 20, x + 10 + k * 12 + 40, y);
                }
                e.Graphics.DrawString(textBoxPAO2.Text, ptzt, Brushes.Black, new Point(x + 10 + k * 12, y));
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);
                e.Graphics.DrawString("总体评估    ASA分级 " + comboBoxasa.Text + "级", ptzt, Brushes.Black, new Point(x + 10, y));
                if (checkBoxE.Checked == true)
                {
                    e.Graphics.DrawString("E级", ptzt, Brushes.Black, new Point(x + 210, y));
                }
                e.Graphics.DrawString("是否饱胃 ", ptzt, Brushes.Black, new Point(x + 340, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 410, y1, 10, 10);
                if (comboBoxynbw.Text == "是")
                {
                    e.Graphics.DrawLine(pblue2, x + 410, y1, x + 415, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 415, y1 + 10, x + 420, y1);
                }
                e.Graphics.DrawString("是", ptzt, Brushes.Black, new Point(x + 420, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 440, y1, 10, 10);
                if (comboBoxynbw.Text == "否")
                {
                    e.Graphics.DrawLine(pblue2, x + 440, y1, x + 445, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 445, y1 + 10, x + 450, y1);
                }
                e.Graphics.DrawString("否", ptzt, Brushes.Black, new Point(x + 450, y));
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawString("目前存在的问题及建议:", ptzt, Brushes.Black, new Point(x + 10, y));
                y = y + 20;
                e.Graphics.DrawString(textBoxwtjy.Text, ptzt, Brushes.Black, new Point(x + 10, y));
                if (textBoxwtjy.Text == "")
                {
                    e.Graphics.DrawString("无", ptzt, Brushes.Black, new Point(x + 10, y));
                }
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawLine(ptp, x + 10, y, x + 660, y);

                e.Graphics.DrawString("麻醉计划：", ptzt, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 90, y1, 10, 10);
                if (checkBoxqsmz.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 90, y1, x + 95, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 95, y1 + 10, x + 100, y1);
                }
                e.Graphics.DrawString("全身麻醉", ptzt, Brushes.Black, new Point(x + 100, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 170, y1, 10, 10);
                if (checkBoxzgnm.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 170, y1, x + 175, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 175, y1 + 10, x + 180, y1);
                }
                e.Graphics.DrawString("椎管内麻醉", ptzt, Brushes.Black, new Point(x + 180, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 270, y1, 10, 10);
                if (checkBoxqyzz.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 270, y1, x + 275, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 275, y1 + 10, x + 280, y1);
                }
                e.Graphics.DrawString("区域阻滞", ptzt, Brushes.Black, new Point(x + 280, y));

                e.Graphics.DrawRectangle(Pens.Black, x + 350, y1, 10, 10);
                if (checkBoxjbmz.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 350, y1, x + 355, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 355, y1 + 10, x + 360, y1);
                }
                e.Graphics.DrawString("局部麻醉", ptzt, Brushes.Black, new Point(x + 360, y));
                e.Graphics.DrawRectangle(Pens.Black, x + 430, y1, 10, 10);
                if (checkBoxajhmz.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 430, y1, x + 435, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 435, y1 + 10, x + 440, y1);
                }
                e.Graphics.DrawString("按计划安排手术", ptzt, Brushes.Black, new Point(x + 440, y));
                y = y + 20; y1 = y + 3;
                e.Graphics.DrawRectangle(Pens.Black, x + 90, y1, 10, 10);
                if (checkBoxapdrmz.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 90, y1, x + 95, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 95, y1 + 10, x + 100, y1);
                }
                e.Graphics.DrawString("安排当日，但需延迟手术", ptzt, Brushes.Black, new Point(x + 100, y));

                e.Graphics.DrawRectangle(Pens.Black, x + 290, y1, 10, 10);
                if (checkBoxzrmz.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 290, y1, x + 295, y1 + 10);
                    e.Graphics.DrawLine(pblue2, x + 295, y1 + 10, x + 300, y1);
                }
                e.Graphics.DrawString("继续术前准备，择日安排手术", ptzt, Brushes.Black, new Point(x + 300, y));
                y = y + 20;
                e.Graphics.DrawString("术前评估医生：" + comboBoxyshengqz.Text, ptzt, Brushes.Black, new Point(x + 10, y));
                //string asd = comboBoxyshengqz.Text.Trim();
                //if (asd != "")
                //{
                //    try
                //    {



                //        Image temp = new Bitmap(Application.StartupPath + "\\手麻科6\\" + asd + ".gif");
                //        //Graphics g = Graphics.FromImage(temp);
                //        int width = temp.Width;
                //        int height = temp.Height;
                //        Rectangle destRect = new Rectangle(x + 80, y, 200, 40);

                //        e.Graphics.DrawImage(temp, destRect, 0, 0, temp.Width, temp.Height, System.Drawing.GraphicsUnit.Pixel);
                //    }
                //    catch (Exception)
                //    {
                //        MessageBox.Show("图片不在本地中");
                //        return;
                //    }
                //}

                e.Graphics.DrawString(Convert.ToDateTime(dateTimePicker1.Text).ToString("yyyy年MM月dd日 HH时mm分"), ptzt, Brushes.Black, new Point(x + 400, y));
                if (a < 1)
                {
                    a++;
                    e.HasMorePages = true;
                    return;
                }
            }
            else if(a > 0)
            {
                string title1 = "浙江省金华市中医医院麻醉知情同意书";
                e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 160, y));
                y = y + 40;
                e.Graphics.DrawString("患者姓名：" + textBoxxingming.Text, ptzt9, Brushes.Black, new Point(x, y));
                e.Graphics.DrawString("性别：" + textBoxsex.Text, ptzt9, Brushes.Black, new Point(x + 130, y));
                e.Graphics.DrawString("年龄：" + textBoxage.Text, ptzt9, Brushes.Black, new Point(x + 230, y));
                e.Graphics.DrawString("床号：" + textBoxchanghao.Text, ptzt9, Brushes.Black, new Point(x + 330, y));
                e.Graphics.DrawString("住院号：" + textBoxzhuyuanhao.Text, ptzt9, Brushes.Black, new Point(x + 430, y));
                e.Graphics.DrawString("麻醉选择：" + comboBoxmzxz.Text + "    其他:" + textBoxmzxzqt.Text, ptzt9, Brushes.Black, new Point(x, y + 30));
                y = y + 80;
                e.Graphics.DrawString("根据手术治疗和诊断检查的需要，患者需进行麻醉，麻醉和麻醉操作一般是安全的，但由于个体差异虽然在麻醉前", ptzt9, Brushes.Black, new Point(x + 10, y));
                e.Graphics.DrawString("已经采取力所能及的预防措施，也有可能发生麻醉意外和并发症。现告知如下，包括但不限于：", ptzt9, Brushes.Black, new Point(x + 10, y + 30));
                e.Graphics.DrawString("1.麻醉过程中可能进行以下某一项或多项操作，包括气管插管、锥管内穿刺、周围神经阻滞、深静脉穿刺置管术、动", ptzt9, Brushes.Black, new Point(x + 10, y + 60));
                e.Graphics.DrawString("脉穿刺置管术、喉罩插入、气管切开术、气管和支气管插管、食管超声波检查、有创血液动力学检测等。这些操作均可能", ptzt9, Brushes.Black, new Point(x + 10, y + 90));
                e.Graphics.DrawString("引起组织出血、神经损伤、创伤、感染、坏死等。", ptzt9, Brushes.Black, new Point(x + 10, y + 120));
                e.Graphics.DrawString("2.根据麻醉操作常规、按照《中华人民共和国药典》要求，使用各种、各类麻醉药后，病人出现中毒、过敏、高敏、神经", ptzt9, Brushes.Black, new Point(x + 10, y + 150));
                e.Graphics.DrawString("毒性等反应，导致休克，严重脏器功能损害、呼吸心跳停止，甚至危及生命。", ptzt9, Brushes.Black, new Point(x + 10, y + 180));
                e.Graphics.DrawString("3.麻醉时，特别是急症饱胃病人发生胃内容物反流、误吸、喉痉挛、呼吸道梗阻、神经反射性休克和心律失常等而导", ptzt9, Brushes.Black, new Point(x + 10, y + 210));
                e.Graphics.DrawString("致重要脏器功能损害，危及生命。", ptzt9, Brushes.Black, new Point(x + 10, y + 240));
                e.Graphics.DrawString("4.气管插管和拔管时可引起牙齿脱落、口唇、舌、咽喉、声带、气管和支气管损伤，喉痉挛、气管痉挛、支气管痉挛及", ptzt9, Brushes.Black, new Point(x + 10, y + 270));
                e.Graphics.DrawString("功能损害。气管插管困难导致气道不能维持通气时，需要进行紧急气管切开术，缺氧时可危及生命。", ptzt9, Brushes.Black, new Point(x + 10, y + 300));
                e.Graphics.DrawString("5.椎管内麻醉及区域麻醉发生神经、血管、脊髓等组织结构损伤，可能出现全脊髓麻醉、截瘫、椎管内感染、血肿、腰", ptzt9, Brushes.Black, new Point(x + 10, y + 330));
                e.Graphics.DrawString("痛、头痛、肢体伤残、甚至呼吸心跳停止等危及生命", ptzt9, Brushes.Black, new Point(x + 10, y + 360));
                e.Graphics.DrawString("6.患者本身合并其他疾病或有重要脏器损害者。相关并发症和麻醉危险性显著增加，如哮喘、心脑血管意外等。", ptzt9, Brushes.Black, new Point(x + 10, y + 390));
                e.Graphics.DrawString("7.授权麻醉医师在病人病情治疗必要时使用自费麻醉和抢救药品及物品。", ptzt9, Brushes.Black, new Point(x + 10, y + 420));
                e.Graphics.DrawString("8.麻醉方法的选择和改变由实施麻醉的医师根据病情和手术的需要决定。", ptzt9, Brushes.Black, new Point(x + 10, y + 450));
                e.Graphics.DrawString("9.可能发生术中知晓、术后回忆和术后认知功能的障碍。", ptzt9, Brushes.Black, new Point(x + 10, y + 480));
                e.Graphics.DrawString("10. 麻醉手术中输血输液可能发生致热源反应、过敏反应、血源性传染病等。", ptzt9, Brushes.Black, new Point(x + 10, y + 510));
                e.Graphics.DrawString("11.急症手术麻醉危险性明显高于择期手术，手术室外麻醉危险性明显高于手术室内麻醉。", ptzt9, Brushes.Black, new Point(x + 10, y + 540));
                e.Graphics.DrawString("12.术后镇痛的并发症:呼吸循环抑制、镇痛不全、瘙痒等。", ptzt9, Brushes.Black, new Point(x + 10, y + 570));
                e.Graphics.DrawString("13. 其它发生率极低或难以预料的意外和并发症，以及其它不可预料的不良后果。", ptzt9, Brushes.Black, new Point(x + 10, y + 600));
                e.Graphics.DrawString("14.本麻醉提请患者及家属注意的其它事项:", ptzt9, Brushes.Black, new Point(x + 10, y + 630));
                e.Graphics.DrawRectangle(Pens.Black, x + 10, y + 660, 10, 10);
                if (checkBoxyqshzts.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 10, y + 660, x + 15, y + 670);
                    e.Graphics.DrawLine(pblue2, x + 15, y + 670, x + 20, y + 660);
                }
                e.Graphics.DrawString("是", ptzt9, Brushes.Black, new Point(x + 20, y + 660));
                e.Graphics.DrawRectangle(Pens.Black, x + 60, y + 660, 10, 10);
                if (checkBoxyqshztf.Checked == true)
                {
                    e.Graphics.DrawLine(pblue2, x + 60, y + 660, x + 65, y + 670);
                    e.Graphics.DrawLine(pblue2, x + 65, y + 670, x + 70, y + 660);
                }
                e.Graphics.DrawString("否", ptzt9, Brushes.Black, new Point(x + 70, y +660));
                e.Graphics.DrawString("要求术后镇痛", ptzt9, Brushes.Black, new Point(x + 110, y + 660));
                e.Graphics.DrawString("我院麻醉科医师将切实做好麻醉前准备，按麻醉操作常规认真做好麻醉及防范措施，以良好的医德医术为患者施", ptzt9, Brushes.Black, new Point(x + 10, y + 690));
                e.Graphics.DrawString("行麻醉，力争将麻醉风险降低到最低限度", ptzt9, Brushes.Black, new Point(x + 10, y + 720));
                e.Graphics.DrawString("我已详细阅读以上内容，麻醉科医师对我提出的问题也作了详细的解答，经慎重考虑，我代表患者及家属对麻醉可", ptzt9, Brushes.Black, new Point(x + 10, y + 750));
                e.Graphics.DrawString("能发生的并发症及各种风险表示充分理解，并全权负责签字同意施行麻醉。我授权麻醉科医师在遇有紧急情况时，为保", ptzt9, Brushes.Black, new Point(x + 10, y + 780));
                e.Graphics.DrawString("障患者生命安全实施必要的救治措施，并承担全部所需费用。我知道在麻醉开始之前，我可以拒绝麻醉，并签字记录，以", ptzt9, Brushes.Black, new Point(x + 10, y + 810));
                e.Graphics.DrawString("患者（法定代理人）签字：", ptzt9, Brushes.Black, new Point(x, y + 840));
                e.Graphics.DrawString("或委托代理人签字：", ptzt9, Brushes.Black, new Point(x + 300, y + 840));
                e.Graphics.DrawString("麻醉科医师签字：" + textBoxmzysqz.Text, ptzt9, Brushes.Black, new Point(x, y + 870));
                e.Graphics.DrawString("日期：" + dateTimePickerRQ.Text, ptzt9, Brushes.Black, new Point(x + 300, y + 870));
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //if (radioButton1.Checked == true)
            //{
            //    string a = "true";//选中
            //    string lieming = "xingxueg";

            //    DAL.dx(lieming, ZYNumber1, a);

            //}
            //else
            //{
            //    string a = "false";//未选中
            //    string lieming = "xingxueg";
            //    DAL.dx(lieming, ZYNumber1, a);
            //}
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void checkBox1_DockChanged(object sender, EventArgs e)
        {
            //if (comboBoxxxg.Checked == true)
            //{
            //    string a = "true";//选中
            //    string lieming = "xingxueg";
            //    DAL.dx(lieming, ZYNumber1, a);
            //}
            //else
            //{
            //    string a = "false";//未选中
            //    string lieming = "xingxueg";
            //    DAL.dx(lieming, ZYNumber1, a);
            //}
           
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }
    }
}
