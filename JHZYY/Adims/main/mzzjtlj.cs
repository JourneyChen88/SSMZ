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
    public partial class mzzjtlj : Form
    {
        adims_DAL.mz dall = new adims_DAL.mz();
        AdimsController bll = new AdimsController();
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        string patid;
        DateTime shijian;
        public mzzjtlj(string zhuyuanid,DateTime rsshijian)
        {
            patid = zhuyuanid;
            shijian = rsshijian;
            InitializeComponent();
        }

        private void mzzjtlj_Load(object sender, EventArgs e)
        {
            try
            {
                LodSQFS_HS1();
                bangdingshujv();
                yishi();
                hushi();
               
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

       // gettljhushi()
       private void hushi()
        {
            DataTable dtMZYS = dall.gettljhushi();
            comboxfshs.Items.Clear();
            //cmboxmzys.Items.Clear();
            foreach (DataRow dr in dtMZYS.Rows)
            {
                comboxfshs.Items.Add(dr["user_name"].ToString());
              //  cmboxmzys.Items.Add(dr["user_name"].ToString());
            }
        }
        private void yishi()
        {
            DataTable dtMZYS = dall.GetAllMZYS();
            comboxfsys.Items.Clear();
            cmboxmzys.Items.Clear();
            foreach (DataRow dr in dtMZYS.Rows)
            {
                comboxfsys.Items.Add(dr["user_name"].ToString());
                cmboxmzys.Items.Add(dr["user_name"].ToString());
            }
        }
        public void bangdingshujv()//绑定基本数据源
        {
            tetzhuyuanhao.Text = patid;
            DataTable dt = bll.SelectPatInfo(patid);
            if (dt.Rows.Count == 0)
            {
                return;
            }
            else
            {
               texname.Text = dt.Rows[0]["patname"].ToString();
                tetxsex.Text = dt.Rows[0]["patsex"].ToString();
                tetxage.Text = dt.Rows[0]["patage"].ToString();
                tetzhuyuanhao.Text = dt.Rows[0]["patid"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            save();
        }

        private void save()//保存及修改
        {
            try
            {         
                Dictionary<string, string> SQFS = new Dictionary<string, string>();
                int result = 0;
                string AddItem = "";
                SQFS.Add("zhuyuanhao", patid);
                AddItem = "";
                if (checkqsmz.Checked) AddItem += "1";
                if (cbkrjssmz.Checked) AddItem += "2";
                if (chekyd.Checked) AddItem += "3";
                if (chemyd.Checked) AddItem += "4";
                if (cbkqxyd.Checked) AddItem += "5";
                if (qgcg.Checked) AddItem += "6";
                if (qgn.Checked) AddItem += "7";
                if (zqgn.Checked) AddItem += "8";
                if (qgcgz.Checked) AddItem += "9";
                if (qgcgy.Checked) AddItem += "a";
                if (jk.Checked) AddItem += "b";
                if (jb.Checked) AddItem += "c";
                if (jqdzk.Checked) AddItem += "d";
                SQFS.Add("qsmz", AddItem);
                SQFS.Add("xinghao", qgcgxh.Text);
                SQFS.Add("shendu", qgcgsd.Text);

                AddItem = "";
                if (cbktnwu.Checked) AddItem += "1";
                if (cbktnyou.Checked) AddItem += "2";
                if (cbksmbl1.Checked) AddItem += "3";
                if (cbksmbl2.Checked) AddItem += "4";
                if (cbksmbl3.Checked) AddItem += "5";
                if (cbksmbl4.Checked) AddItem += "6";
                if (cbkcghj.Checked) AddItem += "7";
                if (cbkcgkqzj.Checked) AddItem += "8";
                if (cbkkshj.Checked) AddItem += "9";
                if (cbkcgmc.Checked) AddItem += "a";
                if (cbkcgbc.Checked) AddItem += "b";
                if (cbkhz.Checked) AddItem += "c";
                SQFS.Add("tnsmcg", AddItem);
//                ([zhuyuanhao],[qsmz],[xinghao],[shendu],[tnsmcg]
                SQFS.Add("chaguanxh", hzxh.Text);
                SQFS.Add("chaszdspo2", cgspo2zd.Text);
                SQFS.Add("chagcs", cmbcs.Text);
                AddItem = "";
                if (cbkwcjm.Checked) AddItem += "1";
                if (cbkwcxr.Checked) AddItem += "2";
                if (cbkwcjxfh.Checked) AddItem += "3";
                if (cbkbgqx.Checked) AddItem += "4";
                if (cbkbghx.Checked) AddItem += "5";
                if (cbkbgss.Checked) AddItem += "6";
                if (cbkbgmzzt.Checked) AddItem += "7";
                if (cbkbgks.Checked) AddItem += "8";
                if (cbkbgty.Checked) AddItem += "9";
                if (cbkbgzy.Checked) AddItem += "a";
                if (cbkbgzd.Checked) AddItem += "b";
                if (cbkbgtt.Checked) AddItem += "c";
                if (cbkbgwsyl.Checked) AddItem += "d";
                SQFS.Add("wcbag", AddItem);
                SQFS.Add("bgqcql", txtcql.Text);
               
//           ,[chaguanxh],[chaszdspo2],[chagcs],[wcbag],[bgqcql]
                SQFS.Add("bgqrr", txtbgqrr.Text);
                SQFS.Add("bgxkqzdspo2", txtbghzdsp02.Text);
                AddItem = "";
                if (cbkjmjc.Checked) AddItem += "1";
                if (cbkjc.Checked) AddItem += "2";
                if (cbkkj.Checked) AddItem += "3";
                if (zhnmz.Checked) AddItem += "4";
                if (ymwmz.Checked) AddItem += "5";
                if (yylhmz.Checked) AddItem += "6";
                if (ym.Checked) AddItem += "7";
                if (cbktwz.Checked) AddItem += "8";
                if (cbktwy.Checked) AddItem += "9";
                SQFS.Add("jvmatiwei", AddItem);

                SQFS.Add("tiweibwei", cmbbw.Text);
//           ,[bgqrr],[bgxkqzdspo2],[jvmatiwei],[tiweibwei]
                AddItem = "";
                if (cbkygwu.Checked) AddItem += "1";
                if (cbkygy.Checked) AddItem += "2";
                if (cbkcc.Checked) AddItem += "3";
                if (cbkzg.Checked) AddItem += "4";
                if (cbkygztz.Checked) AddItem += "5";
                if (cbkygzty.Checked) AddItem += "6";
                if (cbkcxw.Checked) AddItem += "7";
                if (cbkcxy.Checked) AddItem += "8";
                if (cbkcxcc.Checked) AddItem += "9";
                if (cbkcxzg.Checked) AddItem += "a";
                if (cbktpgw.Checked) AddItem += "b";
                if (cbktpgy.Checked) AddItem += "c";
                if (cbkzgzlw.Checked) AddItem += "d";
                if (cbkzgzly.Checked) AddItem += "e";
                SQFS.Add("ygcxtpgzgzl", AddItem);

                SQFS.Add("chuancsd", ccd1.Text);
                SQFS.Add("chuanccs", cmbccdcs.Text);
                AddItem = "";
                if (sjzz.Checked) AddItem += "1";
                if (jcsjzz.Checked) AddItem += "2";
                if (sjczy.Checked) AddItem += "3";
                if (bcyd.Checked) AddItem += "4";
                if (bcsjzz.Checked) AddItem += "5";
                if (jjgf.Checked) AddItem += "6";
                if (ylf.Checked) AddItem += "7";
                if (sgsf.Checked) AddItem += "8";
                if (cbkyc.Checked) AddItem += "9";
                if (cbkgn.Checked) AddItem += "a";
                if (cbkzgn.Checked) AddItem += "b";
                if (cbksjdwy.Checked) AddItem += "c";
                if (cbkccygw.Checked) AddItem += "d";
                if (cbkccygy.Checked) AddItem += "e";
                if (cbkhcxyw.Checked) AddItem += "f";
                if (cbkhcxyy.Checked) AddItem += "g";
                if (cbkmzpm.Checked) AddItem += "h";
                SQFS.Add("sjzz", AddItem);
                SQFS.Add("mzpingmian", cmkmzpm.Text);
                
//           ,[ygcxtpgzgzl],[chuancsd],[chuanccs],[sjzz],[mzpingmian]
                SQFS.Add("lishipingmina", cmklspm.Text);
                SQFS.Add("qitacz", cbmqtcz.Text);
                AddItem = "";
                if (cbkqtcz.Checked) AddItem += "1";
                if (cbkztx.Checked) AddItem += "2";
                if (cmkhs.Checked) AddItem += "3";
                if (cbkcj.Checked) AddItem += "4";
                if (cbkdmzg.Checked) AddItem += "5";
                if (cbkjmzg.Checked) AddItem += "6";
                if (cbkjmzgjn.Checked) AddItem += "7";
                if (cbksgx.Checked) AddItem += "8";
                if (cbkgjm.Checked) AddItem += "9";
                SQFS.Add("qitacaozuo", AddItem);
                SQFS.Add("jmsd", textsd.Text);
                SQFS.Add("zongruliang", textzrl.Text);
                SQFS.Add("jingti", textjt.Text);
                SQFS.Add("jiaoti", textjiaot.Text);
                SQFS.Add("rbc", textrbc.Text);
                SQFS.Add("xuejiang", textxuej.Text);
                SQFS.Add("lengcd", textlcd.Text);
                SQFS.Add("xuexiaoban", textxuexb.Text);
                SQFS.Add("zongchuliang", textxzcl.Text);
                SQFS.Add("niaoliang", textnl.Text);
                SQFS.Add("shixueliang", textsxl.Text);

                AddItem = "";
                if (ckbmzxgpj.Checked) AddItem += "1";
                if (cbkzth.Checked) AddItem += "2";
                if (cbkztzhong.Checked) AddItem += "3";
                if (cbkcha.Checked) AddItem += "4";
                if (cbkzhenjh.Checked) AddItem += "5";
                if (cbkzjzhong.Checked) AddItem += "6";
                if (cbkzjcha.Checked) AddItem += "7";
                if (cbkjshao.Checked) AddItem += "8";
                if (cbkjszhong.Checked) AddItem += "9";
                if (cbkjscha.Checked) AddItem += "a";
                if (cbkhxxhhao.Checked) AddItem += "b";
                if (cbkhxxhzhong.Checked) AddItem += "c";
                if (cbkhxxhcha.Checked) AddItem += "d";
                if (cbkhxglhao.Checked) AddItem += "e";
                if (cbkhxglzhong.Checked) AddItem += "f";
                if (cbkhxglcha.Checked) AddItem += "g";
                if (cbkpacu.Checked) AddItem += "h";
                if (cbklcu.Checked) AddItem += "i";
                if (cbkbingf.Checked) AddItem += "j";
                if (cbkfj.Checked) AddItem += "k";
                SQFS.Add("mzxgpj", AddItem);
                SQFS.Add("mzxgpf", textpingf.Text);

//           ,[ltextzrl33ina],[qitacz],[qitacaozuo],[jmsd],[zongruliang]
//           ,[jingti],[jiaoti],[rbc],[xuejiang],[lengcd],[xuexiaoban]
//           ,[zongchuliang],[niaoliang],[shixueliang],[mzxgpj],[mzxgpf]
                AddItem = "";
                if (cbkshuxuejl.Checked) AddItem += "1";
                if (cbkytx.Checked) AddItem += "2";
                if (cbkytxrbc.Checked) AddItem += "3";
                if (cbkxxlj.Checked) AddItem += "4";
                if (cbklengcd.Checked) AddItem += "5";
                if (cbkytxxxb.Checked) AddItem += "6";
                if (cbkztxliang.Checked) AddItem += "7";
                if (cbkztxrbc.Checked) AddItem += "8";
                if (cbkquanxue.Checked) AddItem += "9";
                if (cbksxblfywu.Checked) AddItem += "a";
                if (cbksxblfyyou.Checked) AddItem += "b";
                SQFS.Add("shuxuejl", AddItem);

                SQFS.Add("shuxueqianhb", textsxqhb.Text);
                SQFS.Add("sxqhct", texsxqthct.Text);
                SQFS.Add("sxqbp", textsxqbp.Text);
                SQFS.Add("sxqpr", textsxqpr.Text);
                SQFS.Add("sxqspo2", textsxqspo2.Text);
                SQFS.Add("ytxrbc", textytxrbc.Text);
                SQFS.Add("ytxxxlj", textxxlj.Text);
                SQFS.Add("ytxlcd", texlengcd.Text);
                SQFS.Add("ytxxxb", textytxxxb.Text);
                SQFS.Add("ztxrbc", textztxrbc.Text);
                SQFS.Add("quanxue", textquanxue.Text);
                SQFS.Add("shuxhhb", textsxhhb.Text);
                SQFS.Add("sxhhct", textsxhhct.Text);
                SQFS.Add("sxhbp", textsxhbp.Text);
                SQFS.Add("sxhpr", textsxhpr.Text);
                SQFS.Add("sxhspo2", textsxhspo2.Text);
                SQFS.Add("shuxuebuliangfanyin", textshuxuebuliangfanyin.Text);
//,[shuxuejl],[shuxueqianhb],[sxqhct],[sxqbp],[sxqpr] ,[sxqspo2],[ytxrbc],[ytxxxlj]
//           ,[ytxlcd],[ytxxxb],[ztxrbc],[quanxue],[shuxhhb],[sxhhct]
//           ,[sxhbp],[sxhpr],[sxhspo2],[shuxuebuliangfanyin],
                SQFS.Add("rstime", textrssj.Text);
                SQFS.Add("BP", textbp.Text);
                SQFS.Add("PR", textpr.Text);
                SQFS.Add("SPO2", textspo2.Text);
                SQFS.Add("RR", textrr.Text);
                AddItem = "";
                if (fszz.Checked) AddItem += "1";
                if (dgfz.Checked) AddItem += "2";
                if (dgjk.Checked) AddItem += "3";
                if (xhzz.Checked) AddItem += "4";
                if (xhywfz.Checked) AddItem += "5";
                if (ysqx.Checked) AddItem += "6";
                if (yshx.Checked) AddItem += "7";
                if (ysss.Checked) AddItem += "8";
                if (ysmzzt.Checked) AddItem += "9";
                SQFS.Add("zdyqhsm", AddItem);
                AddItem = "";
                if (congxwu.Checked) AddItem += "1";
                if (congxiyou.Checked) AddItem += "2";
                SQFS.Add("congx", AddItem);
                SQFS.Add("congxidr", congxidrd.Text);
                AddItem = "";
                if (yinliuwu.Checked) AddItem += "1";
                if (yinliuyou.Checked) AddItem += "2";
                SQFS.Add("yinliu", AddItem);
                SQFS.Add("yinliuru", yinliuru.Text);
                SQFS.Add("yinliuchu", yinliuchu.Text);
                AddItem = "";
                if (daoniaowu.Checked) AddItem += "1";
                if (daoniaoyou.Checked) AddItem += "2";
                SQFS.Add("daoniao", AddItem);
                SQFS.Add("daoniaoru", daoniaoru.Text);
                SQFS.Add("daoniaochu", daoniaochu.Text);
                //,[fsqbfzjcl],[fsqbfzjclbz],[cstime],[quxiang],[xunhuan],[ysddsqsp]
                //,[xkqzd],vaspf,[steward],[qxywksyw],[mazpm],[mzpmmianji] ,[fsys] ,[fshs]
                //,[mzys],[IsRead]
                AddItem = "";
                if (fsbfzwu.Checked) AddItem += "1";
                if (fsbfzy.Checked) AddItem += "2";
                if (kongjiang.Checked) AddItem += "3";
                if (zhenjing.Checked) AddItem += "4";
                if (zhentong.Checked) AddItem += "5";
                if (zhentu.Checked) AddItem += "6";
                if (kuorong.Checked) AddItem += "7";
                if (shengya.Checked) AddItem += "8";
                if (jiusuan.Checked) AddItem += "9";
                if (fsshengwen.Checked) AddItem += "a";
                SQFS.Add("fsqbfzjcl", AddItem);
                SQFS.Add("fsqbfzjclbz", fusbeizhu.Text);
                SQFS.Add("cstime", textcssj.Text);
                SQFS.Add("quxiang", quxiang.Text);
                AddItem = "";
                if (xunhzz.Checked) AddItem += "1";
                if (xunhywfz.Checked) AddItem += "2";
                SQFS.Add("xunhuan", AddItem);
                AddItem = "";
                if (ysddsqspwu.Checked) AddItem += "1";
                if (ysddsqspy.Checked) AddItem += "2";
                SQFS.Add("ysddsqsp", AddItem);
                SQFS.Add("xkqzd", xkqzdspo2.Text);
                SQFS.Add("vaspf", vaspf.Text);
                SQFS.Add("steward", strpf.Text);
                AddItem = "";
                if (qxcjfy.Checked) AddItem += "1";
                if (qxyou.Checked) AddItem += "2";
                if (qxwu.Checked) AddItem += "3";
                if (ksxhzc.Checked) AddItem += "4";
                if (kswu.Checked) AddItem += "5";
                if (kesy.Checked) AddItem += "6";
                if (zthdyys.Checked) AddItem += "7";
                if (zthdwys.Checked) AddItem += "8";
                if (zthdw.Checked) AddItem += "9";
                if (zkztw.Checked) AddItem += "a";
                if (zkzty.Checked) AddItem += "b";
                if (zkztk.Checked) AddItem += "c";
                if (zkztg.Checked) AddItem += "d";
                SQFS.Add("qxywksyw", AddItem);
                AddItem = "";
                if (mzpmw.Checked) AddItem += "1";
                if (mzpmy.Checked) AddItem += "2";
                SQFS.Add("mazpm", AddItem);
                SQFS.Add("mzpmmianji", texboxmazpm.Text);
                SQFS.Add("fsys", comboxfsys.Text);
                SQFS.Add("fshs", comboxfshs.Text);
                SQFS.Add("mzys", cmboxmzys.Text);
                AddItem = "";
                if (wtsqk.Checked) AddItem += "1";
                if (ytsqk.Checked) AddItem += "2";
                SQFS.Add("tesuqingk", AddItem);
                SQFS.Add("tesqingzj", tesuqkzj.Text);
                AddItem = "";
                if (sszt.Checked) AddItem += "1";
                if (cheboxpeca.Checked) AddItem += "2";
                if (cheboxpica.Checked) AddItem += "3";
                if (chboxrftn.Checked) AddItem += "4";
                if (cheboxtswsq.Checked) AddItem += "5";
                SQFS.Add("shzt", AddItem);
                SQFS.Add("ftnjl", textBoxftnjl.Text);
                SQFS.Add("twsqjl", textBoxtwsqjl.Text);
                SQFS.Add("fhl", textBoxfhl.Text);
                SQFS.Add("cxl", textBoxcxl.Text);
                SQFS.Add("qitazty", textboxqtzty.Text);
                if (button1.Enabled)
                {
                    SQFS.Add("IsRead", "0");
                }
                else
                {
                    SQFS.Add("IsRead", "1");
                }

                DataTable dt = bll.Tljselectmazuixj(patid);

                if (dt.Rows.Count > 0)
                {

                    result = bll.Updatemazuixiaojietlj(SQFS);

                }
                else
                    result = bll.TljInsertymazuixj(SQFS);
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
        private void LodSQFS_HS1()
        {
            try{
            DataTable dt = bll.Tljselectmazuixj(patid);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];



                if (Convert.ToString(dr["qsmz"]).Contains("1")) checkqsmz.Checked = true;
                if (Convert.ToString(dr["qsmz"]).Contains("2")) cbkrjssmz.Checked = true;
                if (Convert.ToString(dr["qsmz"]).Contains("3")) chekyd.Checked = true;
                if (Convert.ToString(dr["qsmz"]).Contains("4")) chemyd.Checked = true;
                if (Convert.ToString(dr["qsmz"]).Contains("5")) cbkqxyd.Checked = true;
                if (Convert.ToString(dr["qsmz"]).Contains("6")) qgcg.Checked = true;
                if (Convert.ToString(dr["qsmz"]).Contains("7")) qgn.Checked = true;
                if (Convert.ToString(dr["qsmz"]).Contains("8")) zqgn.Checked = true;
                if (Convert.ToString(dr["qsmz"]).Contains("9")) qgcgz.Checked = true;
                if (Convert.ToString(dr["qsmz"]).Contains("a")) qgcgy.Checked = true;
                if (Convert.ToString(dr["qsmz"]).Contains("b")) jk.Checked = true;
                if (Convert.ToString(dr["qsmz"]).Contains("c")) jb.Checked = true;
                if (Convert.ToString(dr["qsmz"]).Contains("d")) jqdzk.Checked = true;
                qgcgxh.Text = dr["xinghao"].ToString();
                qgcgsd.Text = dr["shendu"].ToString();


                if (Convert.ToString(dr["tnsmcg"]).Contains("1")) cbktnwu.Checked = true;
                if (Convert.ToString(dr["tnsmcg"]).Contains("2")) cbktnyou.Checked = true;
                if (Convert.ToString(dr["tnsmcg"]).Contains("3")) cbksmbl1.Checked = true;
                if (Convert.ToString(dr["tnsmcg"]).Contains("4")) cbksmbl2.Checked = true;
                if (Convert.ToString(dr["tnsmcg"]).Contains("5")) cbksmbl3.Checked = true;
                if (Convert.ToString(dr["tnsmcg"]).Contains("6")) cbksmbl4.Checked = true;
                if (Convert.ToString(dr["tnsmcg"]).Contains("7")) cbkcghj.Checked = true;
                if (Convert.ToString(dr["tnsmcg"]).Contains("8")) cbkcgkqzj.Checked = true;
                if (Convert.ToString(dr["tnsmcg"]).Contains("9")) cbkkshj.Checked = true;
                if (Convert.ToString(dr["tnsmcg"]).Contains("a")) cbkcgmc.Checked = true;
                if (Convert.ToString(dr["tnsmcg"]).Contains("b")) cbkcgbc.Checked = true;
                if (Convert.ToString(dr["tnsmcg"]).Contains("c")) cbkhz.Checked = true;
                hzxh.Text = dr["chaguanxh"].ToString();
                cgspo2zd.Text = dr["chaszdspo2"].ToString();
                cmbcs.Text = dr["chagcs"].ToString();

                if (Convert.ToString(dr["wcbag"]).Contains("1")) cbkwcjm.Checked = true;
                if (Convert.ToString(dr["wcbag"]).Contains("2")) cbkwcxr.Checked = true;
                if (Convert.ToString(dr["wcbag"]).Contains("3")) cbkwcjxfh.Checked = true;
                if (Convert.ToString(dr["wcbag"]).Contains("4")) cbkbgqx.Checked = true;
                if (Convert.ToString(dr["wcbag"]).Contains("5")) cbkbghx.Checked = true;
                if (Convert.ToString(dr["wcbag"]).Contains("6")) cbkbgss.Checked = true;
                if (Convert.ToString(dr["wcbag"]).Contains("7")) cbkbgmzzt.Checked = true;
                if (Convert.ToString(dr["wcbag"]).Contains("8")) cbkbgks.Checked = true;
                if (Convert.ToString(dr["wcbag"]).Contains("9")) cbkbgty.Checked = true;
                if (Convert.ToString(dr["wcbag"]).Contains("a")) cbkbgzy.Checked = true;
                if (Convert.ToString(dr["wcbag"]).Contains("b")) cbkbgzd.Checked = true;
                if (Convert.ToString(dr["wcbag"]).Contains("c")) cbkbgtt.Checked = true;
                if (Convert.ToString(dr["wcbag"]).Contains("d")) cbkbgwsyl.Checked = true;
                txtcql.Text = dr["bgqcql"].ToString();
                txtbgqrr.Text = dr["bgqrr"].ToString();
                txtbghzdsp02.Text = dr["bgxkqzdspo2"].ToString();
                ////           ,[chaguanxh],[chaszdspo2],[chagcs],[wcbag],[bgqcql]
                if (Convert.ToString(dr["jvmatiwei"]).Contains("1")) cbkjmjc.Checked = true;
                if (Convert.ToString(dr["jvmatiwei"]).Contains("2")) cbkjc.Checked = true;
                if (Convert.ToString(dr["jvmatiwei"]).Contains("3")) cbkkj.Checked = true;
                if (Convert.ToString(dr["jvmatiwei"]).Contains("4")) zhnmz.Checked = true;
                if (Convert.ToString(dr["jvmatiwei"]).Contains("5")) ymwmz.Checked = true;
                if (Convert.ToString(dr["jvmatiwei"]).Contains("6")) yylhmz.Checked = true;
                if (Convert.ToString(dr["jvmatiwei"]).Contains("7")) ym.Checked = true;
                if (Convert.ToString(dr["jvmatiwei"]).Contains("8")) cbktwz.Checked = true;
                if (Convert.ToString(dr["jvmatiwei"]).Contains("9")) cbktwy.Checked = true;
                cmbbw.Text = dr["tiweibwei"].ToString();
                //SQFS.Add("tiweibwei", cmbbw.Text);
                ////           ,[bgqrr],[bgxkqzdspo2],[jvmatiwei],[tiweibwei]
                if (Convert.ToString(dr["ygcxtpgzgzl"]).Contains("1")) cbkygwu.Checked = true;
                if (Convert.ToString(dr["ygcxtpgzgzl"]).Contains("2")) cbkygy.Checked = true;
                if (Convert.ToString(dr["ygcxtpgzgzl"]).Contains("3")) cbkcc.Checked = true;
                if (Convert.ToString(dr["ygcxtpgzgzl"]).Contains("4")) cbkzg.Checked = true;
                if (Convert.ToString(dr["ygcxtpgzgzl"]).Contains("5")) cbkygztz.Checked = true;
                if (Convert.ToString(dr["ygcxtpgzgzl"]).Contains("6")) cbkygzty.Checked = true;
                if (Convert.ToString(dr["ygcxtpgzgzl"]).Contains("7")) cbkcxw.Checked = true;
                if (Convert.ToString(dr["ygcxtpgzgzl"]).Contains("8")) cbkcxy.Checked = true;
                if (Convert.ToString(dr["ygcxtpgzgzl"]).Contains("9")) cbkcxcc.Checked = true;
                if (Convert.ToString(dr["ygcxtpgzgzl"]).Contains("a")) cbkcxzg.Checked = true;
                if (Convert.ToString(dr["ygcxtpgzgzl"]).Contains("b")) cbktpgw.Checked = true;
                if (Convert.ToString(dr["ygcxtpgzgzl"]).Contains("c")) cbktpgy.Checked = true;
                if (Convert.ToString(dr["ygcxtpgzgzl"]).Contains("d")) cbkzgzlw.Checked = true;
                if (Convert.ToString(dr["ygcxtpgzgzl"]).Contains("e")) cbkzgzly.Checked = true;
                ccd1.Text = dr["chuancsd"].ToString();
                cmbccdcs.Text = dr["chuanccs"].ToString();
                if (Convert.ToString(dr["sjzz"]).Contains("1")) sjzz.Checked = true;
                if (Convert.ToString(dr["sjzz"]).Contains("2")) jcsjzz.Checked = true;
                if (Convert.ToString(dr["sjzz"]).Contains("3")) sjczy.Checked = true;
                if (Convert.ToString(dr["sjzz"]).Contains("4")) bcyd.Checked = true;
                if (Convert.ToString(dr["sjzz"]).Contains("5")) bcsjzz.Checked = true;
                if (Convert.ToString(dr["sjzz"]).Contains("6")) jjgf.Checked = true;
                if (Convert.ToString(dr["sjzz"]).Contains("7")) ylf.Checked = true;
                if (Convert.ToString(dr["sjzz"]).Contains("8")) sgsf.Checked = true;
                if (Convert.ToString(dr["sjzz"]).Contains("9")) cbkyc.Checked = true;
                if (Convert.ToString(dr["sjzz"]).Contains("a")) cbkgn.Checked = true;
                if (Convert.ToString(dr["sjzz"]).Contains("b")) cbkzgn.Checked = true;
                if (Convert.ToString(dr["sjzz"]).Contains("c")) cbksjdwy.Checked = true;
                if (Convert.ToString(dr["sjzz"]).Contains("d")) cbkccygw.Checked = true;
                if (Convert.ToString(dr["sjzz"]).Contains("e")) cbkccygy.Checked = true;
                if (Convert.ToString(dr["sjzz"]).Contains("f")) cbkhcxyw.Checked = true;
                if (Convert.ToString(dr["sjzz"]).Contains("g")) cbkhcxyy.Checked = true;
                if (Convert.ToString(dr["sjzz"]).Contains("h")) cbkmzpm.Checked = true;
                cmkmzpm.Text = dr["mzpingmian"].ToString();
                cmklspm.Text = dr["lishipingmina"].ToString();
                cbmqtcz.Text = dr["qitacz"].ToString();
                if (Convert.ToString(dr["qitacaozuo"]).Contains("1")) cbkqtcz.Checked = true;
                if (Convert.ToString(dr["qitacaozuo"]).Contains("2")) cbkztx.Checked = true;
                if (Convert.ToString(dr["qitacaozuo"]).Contains("3")) cmkhs.Checked = true;
                if (Convert.ToString(dr["qitacaozuo"]).Contains("4")) cbkcj.Checked = true;
                if (Convert.ToString(dr["qitacaozuo"]).Contains("5")) cbkdmzg.Checked = true;
                if (Convert.ToString(dr["qitacaozuo"]).Contains("6")) cbkjmzg.Checked = true;
                if (Convert.ToString(dr["qitacaozuo"]).Contains("7")) cbkjmzgjn.Checked = true;
                if (Convert.ToString(dr["qitacaozuo"]).Contains("8")) cbksgx.Checked = true;
                if (Convert.ToString(dr["qitacaozuo"]).Contains("9")) cbkgjm.Checked = true;
                textsd.Text = dr["jmsd"].ToString();
                textzrl.Text = dr["zongruliang"].ToString();
                textjt.Text = dr["jingti"].ToString();
                textjiaot.Text = dr["jiaoti"].ToString();
                textrbc.Text = dr["rbc"].ToString();
                textxuej.Text = dr["xuejiang"].ToString();
                textlcd.Text = dr["lengcd"].ToString();
                textxuexb.Text = dr["xuexiaoban"].ToString();
                textxzcl.Text = dr["zongchuliang"].ToString();
                textnl.Text = dr["niaoliang"].ToString();
                textsxl.Text = dr["shixueliang"].ToString();

                if (Convert.ToString(dr["mzxgpj"]).Contains("1")) ckbmzxgpj.Checked = true;
                if (Convert.ToString(dr["mzxgpj"]).Contains("2")) cbkzth.Checked = true;
                if (Convert.ToString(dr["mzxgpj"]).Contains("3")) cbkztzhong.Checked = true;
                if (Convert.ToString(dr["mzxgpj"]).Contains("4")) cbkcha.Checked = true;
                if (Convert.ToString(dr["mzxgpj"]).Contains("5")) cbkzhenjh.Checked = true;
                if (Convert.ToString(dr["mzxgpj"]).Contains("6")) cbkzjzhong.Checked = true;
                if (Convert.ToString(dr["mzxgpj"]).Contains("7")) cbkzjcha.Checked = true;
                if (Convert.ToString(dr["mzxgpj"]).Contains("8")) cbkjshao.Checked = true;
                if (Convert.ToString(dr["mzxgpj"]).Contains("9")) cbkjszhong.Checked = true;
                if (Convert.ToString(dr["mzxgpj"]).Contains("a")) cbkjscha.Checked = true;
                if (Convert.ToString(dr["mzxgpj"]).Contains("b")) cbkhxxhhao.Checked = true;
                if (Convert.ToString(dr["mzxgpj"]).Contains("c")) cbkhxxhzhong.Checked = true;
                if (Convert.ToString(dr["mzxgpj"]).Contains("d")) cbkhxxhcha.Checked = true;
                if (Convert.ToString(dr["mzxgpj"]).Contains("e")) cbkhxglhao.Checked = true;
                if (Convert.ToString(dr["mzxgpj"]).Contains("f")) cbkhxglzhong.Checked = true;
                if (Convert.ToString(dr["mzxgpj"]).Contains("g")) cbkhxglcha.Checked = true;
                if (Convert.ToString(dr["mzxgpj"]).Contains("h")) cbkpacu.Checked = true;
                if (Convert.ToString(dr["mzxgpj"]).Contains("i")) cbklcu.Checked = true;
                if (Convert.ToString(dr["mzxgpj"]).Contains("j")) cbkbingf.Checked = true;
                if (Convert.ToString(dr["mzxgpj"]).Contains("k")) cbkfj.Checked = true;
                textpingf.Text = dr["mzxgpf"].ToString();


                if (Convert.ToString(dr["shuxuejl"]).Contains("1")) cbkshuxuejl.Checked = true;
                if (Convert.ToString(dr["shuxuejl"]).Contains("2")) cbkytx.Checked = true;
                if (Convert.ToString(dr["shuxuejl"]).Contains("3")) cbkytxrbc.Checked = true;
                if (Convert.ToString(dr["shuxuejl"]).Contains("4")) cbkxxlj.Checked = true;
                if (Convert.ToString(dr["shuxuejl"]).Contains("5")) cbklengcd.Checked = true;
                if (Convert.ToString(dr["shuxuejl"]).Contains("6")) cbkytxxxb.Checked = true;
                if (Convert.ToString(dr["shuxuejl"]).Contains("7")) cbkztxliang.Checked = true;
                if (Convert.ToString(dr["shuxuejl"]).Contains("8")) cbkztxrbc.Checked = true;
                if (Convert.ToString(dr["shuxuejl"]).Contains("9")) cbkquanxue.Checked = true;
                if (Convert.ToString(dr["shuxuejl"]).Contains("a")) cbksxblfywu.Checked = true;
                if (Convert.ToString(dr["shuxuejl"]).Contains("b")) cbksxblfyyou.Checked = true;
                textsxqhb.Text = dr["shuxueqianhb"].ToString();
                texsxqthct.Text = dr["sxqhct"].ToString();
                textsxqbp.Text = dr["sxqbp"].ToString();
                textsxqpr.Text = dr["sxqpr"].ToString();
                textsxqspo2.Text = dr["sxqspo2"].ToString();
                textytxrbc.Text = dr["ytxrbc"].ToString();
                textxxlj.Text = dr["ytxxxlj"].ToString();
                texlengcd.Text = dr["ytxlcd"].ToString();
                textytxxxb.Text = dr["ytxxxb"].ToString();
                textztxrbc.Text = dr["ztxrbc"].ToString();
                textquanxue.Text = dr["quanxue"].ToString();
                textsxhhb.Text = dr["shuxhhb"].ToString();
                textsxhhct.Text = dr["sxhhct"].ToString();
                textsxhbp.Text = dr["sxhbp"].ToString();
                textsxhpr.Text = dr["sxhpr"].ToString();
                textsxhspo2.Text = dr["sxhspo2"].ToString();
                textshuxuebuliangfanyin.Text = dr["shuxuebuliangfanyin"].ToString();

               
             
               

                textrssj.Text =dr["rstime"].ToString();
                textbp.Text = dr["BP"].ToString();
                textpr.Text = dr["PR"].ToString();
                textspo2.Text = dr["SPO2"].ToString();
                textrr.Text = dr["RR"].ToString();
                if (Convert.ToString(dr["zdyqhsm"]).Contains("1")) fszz.Checked = true;
                if (Convert.ToString(dr["zdyqhsm"]).Contains("2")) dgfz.Checked = true;
                if (Convert.ToString(dr["zdyqhsm"]).Contains("3")) dgjk.Checked = true;
                if (Convert.ToString(dr["zdyqhsm"]).Contains("4")) xhzz.Checked = true;
                if (Convert.ToString(dr["zdyqhsm"]).Contains("5")) xhywfz.Checked = true;
                if (Convert.ToString(dr["zdyqhsm"]).Contains("6")) ysqx.Checked = true;
                if (Convert.ToString(dr["zdyqhsm"]).Contains("7")) yshx.Checked = true;
                if (Convert.ToString(dr["zdyqhsm"]).Contains("8")) ysss.Checked = true;
                if (Convert.ToString(dr["zdyqhsm"]).Contains("9")) ysmzzt.Checked = true;

                if (Convert.ToString(dr["congx"]).Contains("1")) congxwu.Checked = true;
                if (Convert.ToString(dr["congx"]).Contains("2")) congxiyou.Checked = true;
                congxidrd.Text = dr["congxidr"].ToString();

                if (Convert.ToString(dr["yinliu"]).Contains("1")) yinliuwu.Checked = true;
                if (Convert.ToString(dr["yinliu"]).Contains("2")) yinliuyou.Checked = true;
                yinliuru.Text = dr["yinliuru"].ToString();
                yinliuchu.Text = dr["yinliuchu"].ToString();
                if (Convert.ToString(dr["daoniao"]).Contains("1")) daoniaowu.Checked = true;
                if (Convert.ToString(dr["daoniao"]).Contains("2")) daoniaoyou.Checked = true;
                daoniaoru.Text = dr["daoniaoru"].ToString();
                daoniaochu.Text = dr["daoniaochu"].ToString();
                if (Convert.ToString(dr["fsqbfzjcl"]).Contains("1")) fsbfzwu.Checked = true;
                if (Convert.ToString(dr["fsqbfzjcl"]).Contains("2")) fsbfzy.Checked = true;
                if (Convert.ToString(dr["fsqbfzjcl"]).Contains("3")) kongjiang.Checked = true;
                if (Convert.ToString(dr["fsqbfzjcl"]).Contains("4")) zhenjing.Checked = true;
                if (Convert.ToString(dr["fsqbfzjcl"]).Contains("5")) zhentong.Checked = true;
                if (Convert.ToString(dr["fsqbfzjcl"]).Contains("6")) zhentu.Checked = true;
                if (Convert.ToString(dr["fsqbfzjcl"]).Contains("7")) kuorong.Checked = true;
                if (Convert.ToString(dr["fsqbfzjcl"]).Contains("8")) shengya.Checked = true;
                if (Convert.ToString(dr["fsqbfzjcl"]).Contains("9")) jiusuan.Checked = true;
                if (Convert.ToString(dr["fsqbfzjcl"]).Contains("a")) fsshengwen.Checked = true;
                fusbeizhu.Text = dr["fsqbfzjclbz"].ToString();
                textcssj.Text = dr["cstime"].ToString();
                quxiang.Text = dr["quxiang"].ToString();
                if (Convert.ToString(dr["xunhuan"]).Contains("1")) xunhzz.Checked = true;
                if (Convert.ToString(dr["xunhuan"]).Contains("2")) xunhywfz.Checked = true;
                if (Convert.ToString(dr["ysddsqsp"]).Contains("1")) ysddsqspwu.Checked = true;
                if (Convert.ToString(dr["ysddsqsp"]).Contains("2")) ysddsqspy.Checked = true;
                xkqzdspo2.Text = dr["xkqzd"].ToString();
                vaspf.Text = dr["vaspf"].ToString();
                strpf.Text = dr["steward"].ToString();

                if (Convert.ToString(dr["qxywksyw"]).Contains("1")) qxcjfy.Checked = true;
                if (Convert.ToString(dr["qxywksyw"]).Contains("2")) qxyou.Checked = true;
                if (Convert.ToString(dr["qxywksyw"]).Contains("3")) qxwu.Checked = true;
                if (Convert.ToString(dr["qxywksyw"]).Contains("4")) ksxhzc.Checked = true;
                if (Convert.ToString(dr["qxywksyw"]).Contains("5")) kswu.Checked = true;
                if (Convert.ToString(dr["qxywksyw"]).Contains("6")) kesy.Checked = true;
                if (Convert.ToString(dr["qxywksyw"]).Contains("7")) zthdyys.Checked = true;
                if (Convert.ToString(dr["qxywksyw"]).Contains("8")) zthdwys.Checked = true;
                if (Convert.ToString(dr["qxywksyw"]).Contains("9")) zthdw.Checked = true;
                if (Convert.ToString(dr["qxywksyw"]).Contains("a")) zkztw.Checked = true;
                if (Convert.ToString(dr["qxywksyw"]).Contains("b")) zkzty.Checked = true;
                if (Convert.ToString(dr["qxywksyw"]).Contains("c")) zkztk.Checked = true;
                if (Convert.ToString(dr["qxywksyw"]).Contains("d")) zkztg.Checked = true;

                if (Convert.ToString(dr["mazpm"]).Contains("1")) mzpmw.Checked = true;
                if (Convert.ToString(dr["mazpm"]).Contains("2")) mzpmy.Checked = true;
                texboxmazpm.Text = dr["mzpmmianji"].ToString();
                comboxfsys.Text = dr["fsys"].ToString();
                comboxfshs.Text = dr["fshs"].ToString();
                cmboxmzys.Text = dr["mzys"].ToString();



                if (Convert.ToString(dr["tesuqingk"]).Contains("1")) wtsqk.Checked = true;
                if (Convert.ToString(dr["tesuqingk"]).Contains("2")) ytsqk.Checked = true;
                tesuqkzj.Text = dr["tesqingzj"].ToString();

                if (Convert.ToString(dr["shzt"]).Contains("1")) sszt.Checked = true;
                if (Convert.ToString(dr["shzt"]).Contains("2")) cheboxpeca.Checked = true;
                if (Convert.ToString(dr["shzt"]).Contains("3")) cheboxpica.Checked = true;
                if (Convert.ToString(dr["shzt"]).Contains("4")) chboxrftn.Checked = true;
                if (Convert.ToString(dr["shzt"]).Contains("5")) cheboxtswsq.Checked = true;
                textBoxftnjl.Text = dr["ftnjl"].ToString();
                textBoxtwsqjl.Text = dr["twsqjl"].ToString();
                textBoxfhl.Text = dr["fhl"].ToString();
                textBoxcxl.Text = dr["cxl"].ToString();
                textboxqtzty.Text = dr["qitazty"].ToString();
                }
            }
                catch(Exception ex)
            {
                throw ex;
            }
       
           
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void strpf_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataValid.Text_Value_Limit(sender, e); 
        }

       
      
    }
}
