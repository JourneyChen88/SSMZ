using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using adims_BLL;
using Adims_Utility;

namespace main
{
    public partial class MAzmenzhenPGdan : Form
    {
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        adims_BLL.mz bll = new adims_BLL.mz();//业务逻辑层
        adims_DAL.mz dal = new adims_DAL.mz();//数据访问层
        //adims_MODEL.mz model = new adims_MODEL.mz();//实体层
        string patID, menzhenhaov;
        public MAzmenzhenPGdan(string patID1)
        {
            patID = patID1;
            InitializeComponent();
        }

        private void MAzmenzhenPGdan_Load(object sender, EventArgs e)
        {
            BindCombox3();
            info();
        }

        private void BindCombox3()
        {
            // mazyscom.Items.Clear();
            mazuiyisheng.Items.Clear();
            DataTable dt = new DataTable();
            dt = DAL.GetAllMZYS();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                mazuiyisheng.Items.Add(dt.Rows[i][0]);
                // mazyscom.Items.Add(dt.Rows[i][0]);
            }
        }
        #region<<显示>>
        private void info()
        {
            DataTable dt = dal.GetMazuimzpgdan(patID);
            DataRow dr1 = dt.Rows[0];

            textBoxname.Text = dr1["xingming"].ToString();
            textBoxage.Text = dr1["age"].ToString();
            textBoxsex.Text = dr1["sex"].ToString();
            textBoxtizhong.Text = dr1["tizhong"].ToString();
            textBoxzhenduan.Text = dr1["zhenduan"].ToString();
            menzhenhaov = dr1["menzhenhao"].ToString();
            dt = dal.baocunMazuimzpgdan(patID);
            if (dt.Rows.Count > 0)
            {

                DataRow dr = dt.Rows[0];

                txtbp.Text = dr["BP"].ToString();
                if (Convert.ToString(dr["R"]).Contains("1")) COBQM.Checked = true;
                if (Convert.ToString(dr["R"]).Contains("2")) cmbzgnmz.Checked = true;
                if (Convert.ToString(dr["R"]).Contains("3")) cmbsjzc.Checked = true;
                if (Convert.ToString(dr["R"]).Contains("4")) cmbwtzl.Checked = true;
                txtp.Text = dr["P"].ToString();
                txtt.Text = dr["T"].ToString();
               // cmdxx.Text = dr["xuexing"].ToString();


                if (Convert.ToString(dr["xinxueguan"]).Contains("1")) ckboxxxgzc.Checked = true;
                if (Convert.ToString(dr["xinxueguan"]).Contains("2")) xxgyc.Checked = true;
                if (Convert.ToString(dr["xinxueguan"]).Contains("3")) xxgxt.Checked = true;
                if (Convert.ToString(dr["xinxueguan"]).Contains("4")) checkBoxxj.Checked = true;
                if (Convert.ToString(dr["xinxueguan"]).Contains("5")) checkBoxbmbb.Checked = true;
                if (Convert.ToString(dr["xinxueguan"]).Contains("6")) checkBoxzy.Checked = true;
                if (Convert.ToString(dr["xinxueguan"]).Contains("7")) checkBoxgaoxy.Checked = true;
                if (Convert.ToString(dr["xinxueguan"]).Contains("8")) checkBoxxg.Checked = true;
                if (Convert.ToString(dr["xinxueguan"]).Contains("9")) checkBoxypl.Checked = true;
                textBoxxxgqt.Text = dr["xinxeguanqita"].ToString();

                if (Convert.ToString(dr["feihehuxi"]).Contains("1")) fhhxzc.Checked = true;
                if (Convert.ToString(dr["feihehuxi"]).Contains("2")) fhhxyc.Checked = true;
                if (Convert.ToString(dr["feihehuxi"]).Contains("3")) checkBoxxy.Checked = true;
                if (Convert.ToString(dr["feihehuxi"]).Contains("4")) checkBoxjy.Checked = true;
                if (Convert.ToString(dr["feihehuxi"]).Contains("5")) xhhxcopd.Checked = true;
                if (Convert.ToString(dr["feihehuxi"]).Contains("6")) fhhxfy.Checked = true;
                if (Convert.ToString(dr["feihehuxi"]).Contains("7")) fhhxqigy.Checked = true;
                if (Convert.ToString(dr["feihehuxi"]).Contains("8")) fhhxxc.Checked = true;
                if (Convert.ToString(dr["feihehuxi"]).Contains("9")) fhhxpzjs.Checked = true;
                if (Convert.ToString(dr["feihehuxi"]).Contains("a")) fhhxtb.Checked = true;
                fhhxqt.Text = dr["feihehuxiqita"].ToString();

                if (Convert.ToString(dr["bnsz"]).Contains("1")) bnszzc.Checked = true;
                if (Convert.ToString(dr["bnsz"]).Contains("2")) bnszyc.Checked = true;
                if (Convert.ToString(dr["bnsz"]).Contains("3")) bnszndz.Checked = true;
                if (Convert.ToString(dr["bnsz"]).Contains("4")) checkBoxxueniao.Checked = true;
                if (Convert.ToString(dr["bnsz"]).Contains("5")) checkBoxshengnbq.Checked = true;
                if (Convert.ToString(dr["bnsz"]).Contains("6")) checkBoxyjq.Checked = true;
                textBoxbnsz2.Text = dr["bnszqt"].ToString();
                if (Convert.ToString(dr["gdcw"]).Contains("1")) dgwczc.Checked = true;
                if (Convert.ToString(dr["gdcw"]).Contains("2")) gdwcyc.Checked = true;
                if (Convert.ToString(dr["gdcw"]).Contains("3")) checkBoxganbing.Checked = true;
                if (Convert.ToString(dr["gdcw"]).Contains("4")) checkBoxfanliu.Checked = true;
                if (Convert.ToString(dr["gdcw"]).Contains("5")) checkBoxweizhuliu.Checked = true;
                if (Convert.ToString(dr["gdcw"]).Contains("6")) checkBoxkuiyang.Checked = true;
                gdwcqt.Text = dr["gdcwqt"].ToString();

                if (Convert.ToString(dr["shenjing"]).Contains("1")) shenjingzc.Checked = true;
                if (Convert.ToString(dr["shenjing"]).Contains("2")) shenjingyc.Checked = true;
                if (Convert.ToString(dr["shenjing"]).Contains("3")) checkBoxzhongfeng.Checked = true;
                if (Convert.ToString(dr["shenjing"]).Contains("4")) checkBoxchouchu.Checked = true;
                if (Convert.ToString(dr["shenjing"]).Contains("5")) checkBoxzzjwl.Checked = true;
                textBoxsjqt.Text = dr["shenjingqita"].ToString();

                if (Convert.ToString(dr["xueye"]).Contains("1")) xueyezc.Checked = true;
                if (Convert.ToString(dr["xueye"]).Contains("2")) xueyeyc.Checked = true;
                xueyexzqk.Text = dr["xueye1"].ToString();
                textBoxxyqt.Text = dr["xueyeqita"].ToString();


                if (Convert.ToString(dr["nfbdx"]).Contains("1")) nfbdxzc.Checked = true;
                if (Convert.ToString(dr["nfbdx"]).Contains("2")) nfbdxyc.Checked = true;
                if (Convert.ToString(dr["nfbdx"]).Contains("3")) checkBoxtnb.Checked = true;
                if (Convert.ToString(dr["nfbdx"]).Contains("4")) checkBoxjk.Checked = true;
                if (Convert.ToString(dr["nfbdx"]).Contains("5")) checkBoxyds.Checked = true;
                if (Convert.ToString(dr["nfbdx"]).Contains("6")) checkBoxpiz.Checked = true;
                textBoxnfb2.Text = dr["nfbdxqita"].ToString();

                if (Convert.ToString(dr["jirou"]).Contains("1")) jirouzc.Checked = true;
                if (Convert.ToString(dr["jirou"]).Contains("2")) jirouyc.Checked = true;
                if (Convert.ToString(dr["jirou"]).Contains("3")) jirouzzjwl.Checked = true;
                if (Convert.ToString(dr["jirou"]).Contains("4")) jiroutanhuan.Checked = true;
                textBoxjrqt.Text = dr["jirouqita"].ToString();

                if (Convert.ToString(dr["jingshen"]).Contains("1")) jszc.Checked = true;
                if (Convert.ToString(dr["jingshen"]).Contains("2")) jsyc.Checked = true;
                if (Convert.ToString(dr["jingshen"]).Contains("3")) jsflz.Checked = true;
                if (Convert.ToString(dr["jingshen"]).Contains("4")) jsyyz.Checked = true;
                if (Convert.ToString(dr["jingshen"]).Contains("5")) jsjz.Checked = true;
                textBoxjsqt.Text = dr["jingshenqita"].ToString();

                if (Convert.ToString(dr["ck"]).Contains("1")) ckyes.Checked = true;
                if (Convert.ToString(dr["ck"]).Contains("2")) ckno.Checked = true;
                if (Convert.ToString(dr["ck"]).Contains("3")) ckprq.Checked = true;
                if (Convert.ToString(dr["ck"]).Contains("4")) ckcrq.Checked = true;
                if (Convert.ToString(dr["ck"]).Contains("5")) ckyq.Checked = true;
                textBoxckqt.Text = dr["chankeqita"].ToString();

                if (Convert.ToString(dr["xiyan"]).Contains("1")) xysjyes.Checked = true;
                if (Convert.ToString(dr["xiyan"]).Contains("2")) xysjno.Checked = true;
                if (Convert.ToString(dr["xiyan"]).Contains("3")) xiyan.Checked = true;
                if (Convert.ToString(dr["xiyan"]).Contains("4")) shijiu.Checked = true;
                if (Convert.ToString(dr["xiyan"]).Contains("5")) yaowucy.Checked = true;
                textxy.Text = dr["xiyanqita"].ToString();

                if (Convert.ToString(dr["gms"]).Contains("1")) gmsyes.Checked = true;
                if (Convert.ToString(dr["gms"]).Contains("2")) gmsno.Checked = true;
                if (Convert.ToString(dr["gms"]).Contains("3")) shiwugm.Checked = true;
                if (Convert.ToString(dr["gms"]).Contains("4")) yaowugm.Checked = true;
                if (Convert.ToString(dr["gms"]).Contains("5")) yaoming.Checked = true;
                gmsqita.Text = dr["gmsqita"].ToString();

                if (Convert.ToString(dr["jwmzs"]).Contains("1")) gwmzsyes.Checked = true;
                if (Convert.ToString(dr["jwmzs"]).Contains("2")) gwmzsno.Checked = true;
                if (Convert.ToString(dr["jwmzs"]).Contains("3")) cgkn.Checked = true;
                if (Convert.ToString(dr["jwmzs"]).Contains("4")) mzygm.Checked = true;
                gwmzsqita.Text = dr["jwmzsqt"].ToString();

                if (Convert.ToString(dr["jzs"]).Contains("1")) jzsyes.Checked = true;
                if (Convert.ToString(dr["jzs"]).Contains("2")) jzsno.Checked = true;
                if (Convert.ToString(dr["jzs"]).Contains("3")) jzsmzygm.Checked = true;
                if (Convert.ToString(dr["jzs"]).Contains("4")) exgr.Checked = true;
                jzsqita.Text = dr["jzsqita"].ToString();

                if (Convert.ToString(dr["xzytsyw"]).Contains("1")) xztsywyes.Checked = true;
                if (Convert.ToString(dr["xzytsyw"]).Contains("2")) xztsywno.Checked = true;
                tesyaw.Text = dr["xzytsyw1"].ToString();
                xztsywqt.Text = dr["xzytsywqt"].ToString();

                if (Convert.ToString(dr["qsqk"]).Contains("1")) qsqkyes.Checked = true;
                if (Convert.ToString(dr["qsqk"]).Contains("2")) qsqkno.Checked = true;
                xzqsqk.Text = dr["qsqk1"].ToString();
                gqqsqk.Text = dr["qsqkqt"].ToString();

                if (Convert.ToString(dr["qdtcd"]).Contains("1")) qdtcdyes.Checked = true;
                if (Convert.ToString(dr["qdtcd"]).Contains("2")) qdtcdno.Checked = true;
                if (Convert.ToString(dr["qdtcd"]).Contains("3")) checkBoxzhangk.Checked = true;
                if (Convert.ToString(dr["qdtcd"]).Contains("4")) checkBoxhansheng.Checked = true;
                if (Convert.ToString(dr["qdtcd"]).Contains("5")) checkBoxjinduan.Checked = true;
                if (Convert.ToString(dr["qdtcd"]).Contains("6")) checkBoxthysx.Checked = true;
                if (Convert.ToString(dr["qdtcd"]).Contains("7")) checkBoxhjg.Checked = true;
                if (Convert.ToString(dr["qdtcd"]).Contains("8")) checkBoxxxg.Checked = true;
                if (Convert.ToString(dr["qdtcd"]).Contains("9")) qgyw.Checked = true;
                if (Convert.ToString(dr["qdtcd"]).Contains("a")) qgyp.Checked = true;
                if (Convert.ToString(dr["qdtcd"]).Contains("b")) qgzl.Checked = true;
                txtzk.Text = dr["zhangkou"].ToString();

                if (Convert.ToString(dr["yaci"]).Contains("1")) yaczc.Checked = true;
                if (Convert.ToString(dr["yaci"]).Contains("2")) yacyc.Checked = true;
                if (Convert.ToString(dr["yaci"]).Contains("3")) checkBoxsongdong.Checked = true;
                if (Convert.ToString(dr["yaci"]).Contains("4")) checkBoxqueshi.Checked = true;
                if (Convert.ToString(dr["yaci"]).Contains("5")) checkBoxdaig.Checked = true;
                if (Convert.ToString(dr["yaci"]).Contains("6")) checkBoxshangya.Checked = true;
                if (Convert.ToString(dr["yaci"]).Contains("7")) checkBoxxiaya.Checked = true;
                if (Convert.ToString(dr["yaci"]).Contains("8")) checkBoxbufen.Checked = true;
                if (Convert.ToString(dr["yaci"]).Contains("9")) checkBoxquanbu.Checked = true;

                if (Convert.ToString(dr["mazuiczbw"]).Contains("1")) czbwzc.Checked = true;
                if (Convert.ToString(dr["mazuiczbw"]).Contains("2")) czbwyc.Checked = true;
                if (Convert.ToString(dr["mazuiczbw"]).Contains("3")) ganran.Checked = true;
                if (Convert.ToString(dr["mazuiczbw"]).Contains("4")) jixing.Checked = true;
                if (Convert.ToString(dr["mazuiczbw"]).Contains("5")) waishang.Checked = true;

                if (Convert.ToString(dr["xiongpian"]).Contains("1")) xiongpianzc.Checked = true;
                if (Convert.ToString(dr["xiongpian"]).Contains("2")) xiongpianyc.Checked = true;
                xiongpianqita.Text = dr["xiongpianqita"].ToString();


                if (Convert.ToString(dr["xindiantu"]).Contains("1")) xdtzc.Checked = true;
                if (Convert.ToString(dr["xindiantu"]).Contains("2")) xdtyc.Checked = true;
                xdtqt.Text = dr["xdtqt"].ToString();

                if (Convert.ToString(dr["xcg"]).Contains("1")) xcgzc.Checked = true;
                if (Convert.ToString(dr["xcg"]).Contains("2")) xcgyc.Checked = true;
                xcgqt.Text = dr["xcgqt"].ToString();

                if (Convert.ToString(dr["mianyi"]).Contains("1")) miaoyizc.Checked = true;
                if (Convert.ToString(dr["mianyi"]).Contains("2")) miaoyiyc.Checked = true;
                miaoyiqita.Text = dr["miaoyiqt"].ToString();

                if (Convert.ToString(dr["ningxue"]).Contains("1")) ningxuezc.Checked = true;
                if (Convert.ToString(dr["ningxue"]).Contains("2")) ningxueyc.Checked = true;
                ningxueqt.Text = dr["ningxueqt"].ToString();

                if (Convert.ToString(dr["shenghua"]).Contains("1")) shenghuazc.Checked = true;
                if (Convert.ToString(dr["shenghua"]).Contains("2")) shenghuayc.Checked = true;
                shenghuaqt.Text = dr["shenghuaqt"].ToString();

                if (Convert.ToString(dr["qita"]).Contains("1")) qtzc.Checked = true;
                if (Convert.ToString(dr["qita"]).Contains("2")) qtyc.Checked = true;
                qtqt.Text = dr["qita1"].ToString();

                zongtipinggu.Text = dr["zongtpg"].ToString();

                if (Convert.ToString(dr["asafj"]).Contains("1")) asa1.Checked = true;
                if (Convert.ToString(dr["asafj"]).Contains("2")) asa2.Checked = true;
                if (Convert.ToString(dr["asafj"]).Contains("3")) asa3.Checked = true;
                if (Convert.ToString(dr["asafj"]).Contains("4")) asa4.Checked = true;
                if (Convert.ToString(dr["asafj"]).Contains("5")) asa5.Checked = true;
                if (Convert.ToString(dr["asafj"]).Contains("6")) asa6.Checked = true;

                if (Convert.ToString(dr["sfbw"]).Contains("1")) sbw.Checked = true;
                if (Convert.ToString(dr["sfbw"]).Contains("2")) bbw.Checked = true;
                mqczwtjy.Text = dr["mqczwtjy"].ToString();

                mazuiyisheng.Text = dr["sqmzysqz"].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dr["riqi"]);
            }

        }
        #endregion
        #region<<保存>>
        public void baocun()
        {
            Dictionary<string, string> SQFS = new Dictionary<string, string>();
            int result = 0;
            string AddItem = "";
            SQFS.Add("zhuyuanhao", patID);

            //  ([zhuyuanhao],[BP],[R],[P],[T],[xuexing],[xinxueguan],[xinxeguanqita]
            //,[feihehuxi],[feihehuxiqita],[bnsz],[bnszqt],[gdcw],[gdcwqt],[shenjing]
            //,[shenjingqita],[xueye],[xueye1],[xueyeqita],[nfbdx],[nfbdxqita]
            //,[jirou],[jirouqita],[jingshen],[jingshenqita],[ck],[chankeqita],[xiyan]
            //,[xiyanqita],[gms],[gmsqita],[jwmzs],[jwmzsqt],[jzs],[jzsqita]
            //,[xzytsyw],[xzytsyw1],[xzytsywqt],[qsqk],[qsqk1],[qsqkqt],[qdtcd]
            //,[yaci],[mazuiczbw],[xiongpian],[xiongpianqita],[xindiantu],[xdtqt]
            //,[xcg],[xcgqt],[mianyi],[miaoyiqt],[ningxue],[ningxueqt],[shenghua]
            //,[shenghuaqt],[qita],[qita1],[zongtpg],[asafj],[sfbw],[mqczwtjy]
            //,[sqmzysqz],[riqi],[mzfs],[ydyy],[ydyyqt],[wcyy],[wcyyqt]
            //,[jmyy],[jmyyqt],[tsyy],[jhjh],[jhjhqt] ,[mzglyd],[ztjh]
            //,[ztfs],[ztyy],[ysbh],[pgyisheng],[hunzqm],[pgsj])
            SQFS.Add("BP", txtbp.Text);
            AddItem = "";
            if (COBQM.Checked) AddItem += "1";
            if (cmbzgnmz.Checked) AddItem += "2";
            if (cmbsjzc.Checked) AddItem += "3";
            if (cmbwtzl.Checked) AddItem += "4";
            SQFS.Add("R", AddItem);
            SQFS.Add("P", txtp.Text);
            SQFS.Add("T", txtt.Text);
            SQFS.Add("xuexing", "");
            AddItem = "";
            if (ckboxxxgzc.Checked) AddItem += "1";
            if (xxgyc.Checked) AddItem += "2";
            if (xxgxt.Checked) AddItem += "3";
            if (checkBoxxj.Checked) AddItem += "4";
            if (checkBoxbmbb.Checked) AddItem += "5";
            if (checkBoxzy.Checked) AddItem += "6";
            if (checkBoxgaoxy.Checked) AddItem += "7";
            if (checkBoxxg.Checked) AddItem += "8";
            if (checkBoxypl.Checked) AddItem += "9";
            SQFS.Add("xinxueguan", AddItem);
            SQFS.Add("xinxeguanqita", textBoxxxgqt.Text);
            AddItem = "";
            if (fhhxzc.Checked) AddItem += "1";
            if (fhhxyc.Checked) AddItem += "2";
            if (checkBoxxy.Checked) AddItem += "3";
            if (checkBoxjy.Checked) AddItem += "4";
            if (xhhxcopd.Checked) AddItem += "5";
            if (fhhxfy.Checked) AddItem += "6";
            if (fhhxqigy.Checked) AddItem += "7";
            if (fhhxxc.Checked) AddItem += "8";
            if (fhhxpzjs.Checked) AddItem += "9";
            if (fhhxtb.Checked) AddItem += "a";
            SQFS.Add("feihehuxi", AddItem);
            SQFS.Add("feihehuxiqita", fhhxqt.Text);
            AddItem = "";
            if (bnszzc.Checked) AddItem += "1";
            if (bnszyc.Checked) AddItem += "2";
            if (bnszndz.Checked) AddItem += "3";
            if (checkBoxxueniao.Checked) AddItem += "4";
            if (checkBoxshengnbq.Checked) AddItem += "5";
            if (checkBoxyjq.Checked) AddItem += "6";
            SQFS.Add("bnsz", AddItem);
            SQFS.Add("bnszqt", textBoxbnsz2.Text);
            //,[feihehuxi],[feihehuxiqita],[bnsz],[bnszqt],[gdcw],[gdcwqt],[shenjing]
            //,[shenjingqita],[xueye],[xueye1],[xueyeqita],[nfbdx],[nfbdxqita]
            //,[jirou],[jirouqita],[jingshen],[jingshenqita],[ck],[chankeqita],[xiyan]
            //,[xiyanqita],[gms],[gmsqita],[jwmzs],[jwmzsqt],[jzs],[jzsqita]
            //,[xzytsyw],[xzytsyw1],[xzytsywqt],[qsqk],[qsqk1],[qsqkqt],[qdtcd]
            //,[yaci],[mazuiczbw],[xiongpian],[xiongpianqita],[xindiantu],[xdtqt]
            //,[xcg],[xcgqt],[mianyi],[miaoyiqt],[ningxue],[ningxueqt],[shenghua]
            //,[shenghuaqt],[qita],[qita1],[zongtpg],[asafj],[sfbw],[mqczwtjy]
            //,[sqmzysqz],[riqi],[mzfs],[ydyy],[ydyyqt],[wcyy],[wcyyqt]
            //,[jmyy],[jmyyqt],[tsyy],[jhjh],[jhjhqt] ,[mzglyd],[ztjh]
            //,[ztfs],[ztyy],[ysbh],[pgyisheng],[hunzqm],[pgsj])
            AddItem = "";
            if (dgwczc.Checked) AddItem += "1";
            if (gdwcyc.Checked) AddItem += "2";
            if (checkBoxganbing.Checked) AddItem += "3";
            if (checkBoxfanliu.Checked) AddItem += "4";
            if (checkBoxweizhuliu.Checked) AddItem += "5";
            if (checkBoxkuiyang.Checked) AddItem += "6";
            SQFS.Add("gdcw", AddItem);
            SQFS.Add("gdcwqt", gdwcqt.Text);

            AddItem = "";
            if (shenjingzc.Checked) AddItem += "1";
            if (shenjingyc.Checked) AddItem += "2";
            if (checkBoxzhongfeng.Checked) AddItem += "3";
            if (checkBoxchouchu.Checked) AddItem += "4";
            if (checkBoxzzjwl.Checked) AddItem += "5";
            SQFS.Add("shenjing", AddItem);
            SQFS.Add("shenjingqita", textBoxsjqt.Text);
            AddItem = "";
            if (xueyezc.Checked) AddItem += "1";
            if (xueyeyc.Checked) AddItem += "2";
            SQFS.Add("xueye", AddItem);
            SQFS.Add("xueye1", xueyexzqk.Text);
            SQFS.Add("xueyeqita", textBoxxyqt.Text);
            //,[feihehuxi],[feihehuxiqita],[bnsz],[bnszqt],[gdcw],[gdcwqt],[shenjing]
            //,[shenjingqita],[xueye],[xueye1],[xueyeqita],[nfbdx],[nfbdxqita]
            AddItem = "";
            if (nfbdxzc.Checked) AddItem += "1";
            if (nfbdxyc.Checked) AddItem += "2";
            if (checkBoxtnb.Checked) AddItem += "3";
            if (checkBoxjk.Checked) AddItem += "4";
            if (checkBoxyds.Checked) AddItem += "5";
            if (checkBoxpiz.Checked) AddItem += "6";
            SQFS.Add("nfbdx", AddItem);
            SQFS.Add("nfbdxqita", textBoxnfb2.Text);

            AddItem = "";
            if (jirouzc.Checked) AddItem += "1";
            if (jirouyc.Checked) AddItem += "2";
            if (jirouzzjwl.Checked) AddItem += "3";
            if (jiroutanhuan.Checked) AddItem += "4";
            SQFS.Add("jirou", AddItem);
            SQFS.Add("jirouqita", textBoxjrqt.Text);

            AddItem = "";
            if (jszc.Checked) AddItem += "1";
            if (jsyc.Checked) AddItem += "2";
            if (jsflz.Checked) AddItem += "3";
            if (jsyyz.Checked) AddItem += "4";
            if (jsjz.Checked) AddItem += "5";
            SQFS.Add("jingshen", AddItem);
            SQFS.Add("jingshenqita", textBoxjsqt.Text);

            AddItem = "";
            if (ckyes.Checked) AddItem += "1";
            if (ckno.Checked) AddItem += "2";
            if (ckprq.Checked) AddItem += "3";
            if (ckcrq.Checked) AddItem += "4";
            if (ckyq.Checked) AddItem += "5";
            SQFS.Add("ck", AddItem);
            SQFS.Add("chankeqita", textBoxckqt.Text);
            AddItem = "";
            if (xysjyes.Checked) AddItem += "1";
            if (xysjno.Checked) AddItem += "2";
            if (xiyan.Checked) AddItem += "3";
            if (shijiu.Checked) AddItem += "4";
            if (yaowucy.Checked) AddItem += "5";
            SQFS.Add("xiyan", AddItem);
            SQFS.Add("xiyanqita", textxy.Text);

            AddItem = "";
            if (gmsyes.Checked) AddItem += "1";
            if (gmsno.Checked) AddItem += "2";
            if (shiwugm.Checked) AddItem += "3";
            if (yaowugm.Checked) AddItem += "4";
            if (yaoming.Checked) AddItem += "5";
            SQFS.Add("gms", AddItem);
            SQFS.Add("gmsqita", gmsqita.Text);

            AddItem = "";
            if (gwmzsyes.Checked) AddItem += "1";
            if (gwmzsno.Checked) AddItem += "2";
            if (cgkn.Checked) AddItem += "3";
            if (mzygm.Checked) AddItem += "4";
            SQFS.Add("jwmzs", AddItem);
            SQFS.Add("jwmzsqt", gwmzsqita.Text);

            AddItem = "";
            if (jzsyes.Checked) AddItem += "1";
            if (jzsno.Checked) AddItem += "2";
            if (jzsmzygm.Checked) AddItem += "3";
            if (exgr.Checked) AddItem += "4";
            SQFS.Add("jzs", AddItem);
            SQFS.Add("jzsqita", jzsqita.Text);

            AddItem = "";
            if (xztsywyes.Checked) AddItem += "1";
            if (xztsywno.Checked) AddItem += "2";
            SQFS.Add("xzytsyw", AddItem);
            SQFS.Add("xzytsyw1", tesyaw.Text);
            SQFS.Add("xzytsywqt", xztsywqt.Text);

            AddItem = "";
            if (qsqkyes.Checked) AddItem += "1";
            if (qsqkno.Checked) AddItem += "2";
            SQFS.Add("qsqk", AddItem);
            SQFS.Add("qsqk1", xzqsqk.Text);
            SQFS.Add("qsqkqt", gqqsqk.Text);

            AddItem = "";
            if (qdtcdyes.Checked) AddItem += "1";
            if (qdtcdno.Checked) AddItem += "2";
            if (checkBoxzhangk.Checked) AddItem += "3";
            if (checkBoxhansheng.Checked) AddItem += "4";
            if (checkBoxjinduan.Checked) AddItem += "5";
            if (checkBoxthysx.Checked) AddItem += "6";
            if (checkBoxhjg.Checked) AddItem += "7";
            if (checkBoxxxg.Checked) AddItem += "8";
            if (qgyw.Checked) AddItem += "9";
            if (qgyp.Checked) AddItem += "a";
            if (qgzl.Checked) AddItem += "b";
            SQFS.Add("qdtcd", AddItem);
            SQFS.Add("zhangkou", txtzk.Text);
            //,[jirou],[jirouqita],[jingshen],[jingshenqita],[ck],[chankeqita],[xiyan]
            //,[xiyanqita],[gms],[gmsqita],[jwmzs],[jwmzsqt],[jzs],[jzsqita]
            //,[xzytsyw],[xzytsyw1],[xzytsywqt],[qsqk],[qsqk1],[qsqkqt],[qdtcd],[zhangkou]

            AddItem = "";
            if (yaczc.Checked) AddItem += "1";
            if (yacyc.Checked) AddItem += "2";
            if (checkBoxsongdong.Checked) AddItem += "3";
            if (checkBoxqueshi.Checked) AddItem += "4";
            if (checkBoxdaig.Checked) AddItem += "5";
            if (checkBoxshangya.Checked) AddItem += "6";
            if (checkBoxxiaya.Checked) AddItem += "7";
            if (checkBoxbufen.Checked) AddItem += "8";
            if (checkBoxquanbu.Checked) AddItem += "9";
            SQFS.Add("yaci", AddItem);

            AddItem = "";
            if (czbwzc.Checked) AddItem += "1";
            if (czbwyc.Checked) AddItem += "2";
            if (ganran.Checked) AddItem += "3";
            if (jixing.Checked) AddItem += "4";
            if (waishang.Checked) AddItem += "5";
            SQFS.Add("mazuiczbw", AddItem);

            AddItem = "";
            if (xiongpianzc.Checked) AddItem += "1";
            if (xiongpianyc.Checked) AddItem += "2";
            SQFS.Add("xiongpian", AddItem);
            SQFS.Add("xiongpianqita", xiongpianqita.Text);

            AddItem = "";
            if (xdtzc.Checked) AddItem += "1";
            if (xdtyc.Checked) AddItem += "2";
            SQFS.Add("xindiantu", AddItem);
            SQFS.Add("xdtqt", xdtqt.Text);

            AddItem = "";
            if (xcgzc.Checked) AddItem += "1";
            if (xcgyc.Checked) AddItem += "2";
            SQFS.Add("xcg", AddItem);
            SQFS.Add("xcgqt", xcgqt.Text);

            AddItem = "";
            if (miaoyizc.Checked) AddItem += "1";
            if (miaoyiyc.Checked) AddItem += "2";
            SQFS.Add("mianyi", AddItem);
            SQFS.Add("miaoyiqt", miaoyiqita.Text);

            AddItem = "";
            if (ningxuezc.Checked) AddItem += "1";
            if (ningxueyc.Checked) AddItem += "2";
            SQFS.Add("ningxue", AddItem);
            SQFS.Add("ningxueqt", ningxueqt.Text);

            AddItem = "";
            if (shenghuazc.Checked) AddItem += "1";
            if (shenghuayc.Checked) AddItem += "2";
            SQFS.Add("shenghua", AddItem);
            SQFS.Add("shenghuaqt", shenghuaqt.Text);
            AddItem = "";
            if (qtzc.Checked) AddItem += "1";
            if (qtyc.Checked) AddItem += "2";
            SQFS.Add("qita", AddItem);
            SQFS.Add("qita1", qtqt.Text);

            SQFS.Add("zongtpg", zongtipinggu.Text);
            AddItem = "";
            if (asa1.Checked) AddItem += "1";
            if (asa2.Checked) AddItem += "2";
            if (asa3.Checked) AddItem += "3";
            if (asa4.Checked) AddItem += "4";
            if (asa5.Checked) AddItem += "5";
            if (asa6.Checked) AddItem += "6";
            SQFS.Add("asafj", AddItem);

            AddItem = "";
            if (sbw.Checked) AddItem += "1";
            if (bbw.Checked) AddItem += "2";
            SQFS.Add("sfbw", AddItem);
            SQFS.Add("mqczwtjy", mqczwtjy.Text);

            SQFS.Add("sqmzysqz", mazuiyisheng.Text);
            SQFS.Add("riqi", Convert.ToDateTime(dateTimePicker1.Value.ToString()).ToString("yyyy-MM-dd HH:mm"));
            DataTable dt = dal.baocunMazuimzpgdan(patID);

            if (dt.Rows.Count > 0)
            {
                result = dal.UpdateMazuimzpgdan(SQFS);
                MessageBox.Show("修改成功");
            }
            else
            {
                result = dal.InsertMazuimzpgdan(SQFS);
                MessageBox.Show("保存成功！");
            }


        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (mazuiyisheng.Text.IsNullOrEmpty())
            {
                MessageBox.Show("麻醉医生不能为空!");
                mazuiyisheng.Focus();//获得焦点
                return;
            }
            baocun();
        }
        #region<<打印>>
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;//打印
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            //           //printDocument1.DefaultPageSettings.PaperSize =
            //    new System.Drawing.Printing.PaperSize("K16", 740, 1020);
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("A4", 820, 1160);
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font ptzt9 = new Font("新宋体", 9);
            Font ptzt10 = new Font("新宋体", 10);
            Pen ptp = Pens.Black;//普通画笔
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);
            Pen pblack2 = new Pen(Brushes.Black, 2);
            Font ptzt = new Font("宋体", 10);//普通字体
            Font ptzt1 = new Font("新宋体", 10);//普通字体
            Font ptzt2 = new Font("黑体", 10, FontStyle.Bold);//加粗十号
            Font ptzt3 = new Font("新宋体", 20, FontStyle.Bold);//加粗14号
            Font ptzt4 = new Font("宋体", 14, FontStyle.Bold);//加粗11号
            Font ptzt5 = new Font("宋体", 16, FontStyle.Bold);//加粗16号
            Font ptzt11 = new Font("宋体", 11);//普通字体 
            Font ptzt6 = new Font("宋体", 12);//普通字体
            int y = 80; int x = 50; int y1 = 0;
            string title1 = "新疆医科大学第五附属医院麻醉门诊评估单";
            e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 80, y));
            y = y + 50; y1 = y + 15;
            e.Graphics.DrawString("姓名:" + textBoxname.Text.Trim(), ptzt11, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 15, x + 200, y + 15);
            e.Graphics.DrawString("年龄:" + textBoxage.Text.Trim(), ptzt11, Brushes.Black, new Point(x + 200, y));
            e.Graphics.DrawLine(Pens.Black, x + 240, y + 15, x + 280, y + 15);
            e.Graphics.DrawString("岁", ptzt11, Brushes.Black, new Point(x + 280, y));
            e.Graphics.DrawString("性别:" + textBoxsex.Text.Trim(), ptzt11, Brushes.Black, new Point(x + 340, y));
            e.Graphics.DrawString("体重:" + textBoxtizhong.Text.Trim(), ptzt11, Brushes.Black, new Point(x + 500, y));
            e.Graphics.DrawLine(Pens.Black, x + 540, y + 15, x + 580, y + 15);
            e.Graphics.DrawString("kg", ptzt11, Brushes.Black, new Point(x + 580, y));
            y = y + 25;
            e.Graphics.DrawString("术前诊断:" + textBoxzhenduan.Text.Trim(), ptzt11, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 72, y + 15, x + 245, y + 15);
            e.Graphics.DrawString("拟行麻醉:□全麻 □椎管内麻醉 □神经阻滞 □无痛诊疗", ptzt11, Brushes.Black, new Point(x + 250, y));
            if (COBQM.Checked) e.Graphics.DrawString("✔", ptzt1, Brushes.Black, new Point(x + 321, y));
            if (cmbzgnmz.Checked) e.Graphics.DrawString("✔", ptzt1, Brushes.Black, new Point(x + 376, y));
            if (cmbsjzc.Checked) e.Graphics.DrawString("✔", ptzt1, Brushes.Black, new Point(x + 478, y));
            if (cmbwtzl.Checked) e.Graphics.DrawString("✔", ptzt1, Brushes.Black, new Point(x + 565, y));
            y = y + 25;
            e.Graphics.DrawString("术前一般情况: BP " + txtbp.Text.Trim() , ptzt11, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("mmHg", ptzt11, Brushes.Black, new Point(x + 190, y));
            e.Graphics.DrawString("P " + txtp.Text.Trim() , ptzt6, Brushes.Black, new Point(x + 250, y));
            e.Graphics.DrawString( "次/分", ptzt11, Brushes.Black, new Point(x + 340, y));
            e.Graphics.DrawString("SPO₂ " + txtt.Text.Trim() , ptzt11, Brushes.Black, new Point(x + 400, y));

            y = y + 30; y1 = y + 15;
            e.Graphics.DrawString("系统情况", ptzt, Brushes.Black, new Point(x + 65, y));
            e.Graphics.DrawString("现在状态", ptzt, Brushes.Black, new Point(x + 320, y));
            e.Graphics.DrawString("过去或其它情况", ptzt, Brushes.Black, new Point(x + 520, y));
            e.Graphics.DrawLine(pblack, x, y1 + 5, x + 710, y1 + 5);
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawLine(ptp, x + 235, y - 5, x + 235, y + 795);
            e.Graphics.DrawLine(ptp, x + 490, y - 5, x + 490, y + 510);
            e.Graphics.DrawString("心血管", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (ckboxxxgzc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("正常", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (xxgyc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("异常", ptzt10, Brushes.Black, new Point(x + 205, y));


            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            if (xxgxt.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawString("胸痛", ptzt9, Brushes.Black, new Point(x + 250, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 300, y, 10, 10);
            if (checkBoxxj.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 300, y, x + 305, y + 10);
                e.Graphics.DrawLine(pblue2, x + 305, y + 10, x + 310, y);
            }
            e.Graphics.DrawString("心悸", ptzt9, Brushes.Black, new Point(x + 310, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 10, 10);
            if (checkBoxbmbb.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 360, y, x + 365, y + 10);
                e.Graphics.DrawLine(pblue2, x + 365, y + 10, x + 370, y);
            }
            e.Graphics.DrawString("瓣膜病变", ptzt9, Brushes.Black, new Point(x + 370, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 435, y, 10, 10);
            if (checkBoxzy.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 435, y, x + 440, y + 10);
                e.Graphics.DrawLine(pblue2, x + 440, y + 10, x + 445, y);
            }
            e.Graphics.DrawString("杂音", ptzt9, Brushes.Black, new Point(x + 445, y));
            e.Graphics.DrawString(textBoxxxgqt.Text, ptzt9, Brushes.Black, new Point(x + 490, y));
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            if (checkBoxgaoxy.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawString("高血压", ptzt9, Brushes.Black, new Point(x + 250, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 300, y, 10, 10);
            if (checkBoxxg.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 300, y, x + 305, y + 10);
                e.Graphics.DrawLine(pblue2, x + 305, y + 10, x + 310, y);
            }
            e.Graphics.DrawString("心梗", ptzt9, Brushes.Black, new Point(x + 310, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 10, 10);
            if (checkBoxypl.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 360, y, x + 365, y + 10);
                e.Graphics.DrawLine(pblue2, x + 365, y + 10, x + 370, y);
            }
            e.Graphics.DrawString("易疲劳", ptzt9, Brushes.Black, new Point(x + 370, y));


            y = y + 25; y1 = y + 15;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("肺和呼吸", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (fhhxzc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("正常", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (fhhxyc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("异常", ptzt10, Brushes.Black, new Point(x + 205, y));


            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            if (checkBoxxy.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawString("吸烟", ptzt9, Brushes.Black, new Point(x + 250, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 300, y, 10, 10);
            if (checkBoxjy.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 300, y, x + 305, y + 10);
                e.Graphics.DrawLine(pblue2, x + 305, y + 10, x + 310, y);
            }
            e.Graphics.DrawString("戒烟", ptzt9, Brushes.Black, new Point(x + 310, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 10, 10);
            if (xhhxcopd.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 360, y, x + 365, y + 10);
                e.Graphics.DrawLine(pblue2, x + 365, y + 10, x + 370, y);
            }
            e.Graphics.DrawString("COPD", ptzt9, Brushes.Black, new Point(x + 370, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 435, y, 10, 10);
            if (fhhxfy.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 435, y, x + 440, y + 10);
                e.Graphics.DrawLine(pblue2, x + 440, y + 10, x + 445, y);
            }
            e.Graphics.DrawString("肺炎", ptzt9, Brushes.Black, new Point(x + 445, y));
            e.Graphics.DrawString(fhhxqt.Text, ptzt9, Brushes.Black, new Point(x + 490, y));
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            if (fhhxqigy.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawString("气管炎", ptzt9, Brushes.Black, new Point(x + 250, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 300, y, 10, 10);
            if (fhhxxc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 300, y, x + 305, y + 10);
                e.Graphics.DrawLine(pblue2, x + 305, y + 10, x + 310, y);
            }
            e.Graphics.DrawString("哮喘", ptzt9, Brushes.Black, new Point(x + 310, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 10, 10);
            if (fhhxpzjs.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 360, y, x + 365, y + 10);
                e.Graphics.DrawLine(pblue2, x + 365, y + 10, x + 370, y);
            }
            e.Graphics.DrawString("皮质激素", ptzt9, Brushes.Black, new Point(x + 370, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 435, y, 10, 10);
            if (fhhxtb.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 435, y, x + 440, y + 10);
                e.Graphics.DrawLine(pblue2, x + 440, y + 10, x + 445, y);
            }
            e.Graphics.DrawString("TB", ptzt9, Brushes.Black, new Point(x + 445, y));

            y = y + 25; y1 = y + 15;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("泌尿生殖", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (bnszzc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("正常", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (bnszyc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("异常", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            if (bnszndz.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawString("尿毒症", ptzt9, Brushes.Black, new Point(x + 250, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 300, y, 10, 10);
            if (checkBoxxueniao.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 300, y, x + 305, y + 10);
                e.Graphics.DrawLine(pblue2, x + 305, y + 10, x + 310, y);
            }
            e.Graphics.DrawString("血尿", ptzt9, Brushes.Black, new Point(x + 310, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 10, 10);
            if (checkBoxshengnbq.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 360, y, x + 365, y + 10);
                e.Graphics.DrawLine(pblue2, x + 365, y + 10, x + 370, y);
            }
            e.Graphics.DrawString("肾功不全", ptzt9, Brushes.Black, new Point(x + 370, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 435, y, 10, 10);
            if (checkBoxyjq.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 435, y, x + 440, y + 10);
                e.Graphics.DrawLine(pblue2, x + 440, y + 10, x + 445, y);
            }
            e.Graphics.DrawString("月经期", ptzt9, Brushes.Black, new Point(x + 445, y));
            e.Graphics.DrawString(textBoxbnsz2.Text, ptzt9, Brushes.Black, new Point(x + 490, y));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("肝胆胃肠", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (dgwczc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("正常", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (gdwcyc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("异常", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            if (checkBoxganbing.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawString("肝病", ptzt9, Brushes.Black, new Point(x + 250, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 300, y, 10, 10);
            if (checkBoxfanliu.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 300, y, x + 305, y + 10);
                e.Graphics.DrawLine(pblue2, x + 305, y + 10, x + 310, y);
            }
            e.Graphics.DrawString("反流", ptzt9, Brushes.Black, new Point(x + 310, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 10, 10);
            if (checkBoxweizhuliu.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 360, y, x + 365, y + 10);
                e.Graphics.DrawLine(pblue2, x + 365, y + 10, x + 370, y);
            }
            e.Graphics.DrawString("胃潴留", ptzt9, Brushes.Black, new Point(x + 370, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 435, y, 10, 10);
            if (checkBoxkuiyang.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 435, y, x + 440, y + 10);
                e.Graphics.DrawLine(pblue2, x + 440, y + 10, x + 445, y);
            }
            e.Graphics.DrawString("溃疡", ptzt9, Brushes.Black, new Point(x + 445, y));
            e.Graphics.DrawString(gdwcqt.Text, ptzt9, Brushes.Black, new Point(x + 490, y));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("神经", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (shenjingzc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("正常", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (shenjingyc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("异常", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            if (checkBoxzhongfeng.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawString("中风", ptzt9, Brushes.Black, new Point(x + 250, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 300, y, 10, 10);
            if (checkBoxchouchu.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 300, y, x + 305, y + 10);
                e.Graphics.DrawLine(pblue2, x + 305, y + 10, x + 310, y);
            }
            e.Graphics.DrawString("抽搐", ptzt9, Brushes.Black, new Point(x + 310, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 10, 10);
            if (checkBoxzzjwl.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 360, y, x + 365, y + 10);
                e.Graphics.DrawLine(pblue2, x + 365, y + 10, x + 370, y);
            }
            e.Graphics.DrawString("神经肌肉病变", ptzt9, Brushes.Black, new Point(x + 370, y));
            e.Graphics.DrawString(textBoxsjqt.Text, ptzt9, Brushes.Black, new Point(x + 490, y));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("血液", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (shenjingzc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("正常", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (shenjingyc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }

            e.Graphics.DrawString("异常", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawString(xueyexzqk.Text, ptzt9, Brushes.Black, new Point(x + 240, y));
            e.Graphics.DrawString(textBoxxyqt.Text, ptzt9, Brushes.Black, new Point(x + 490, y));

            y = y + 30; y1 = y + 15;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("内分泌代谢", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (nfbdxzc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("正常", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (nfbdxyc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("异常", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            if (checkBoxtnb.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawString("糖尿病", ptzt9, Brushes.Black, new Point(x + 250, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 300, y, 10, 10);
            if (checkBoxjk.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 300, y, x + 305, y + 10);
                e.Graphics.DrawLine(pblue2, x + 305, y + 10, x + 310, y);
            }
            e.Graphics.DrawString("甲亢/低", ptzt9, Brushes.Black, new Point(x + 310, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 10, 10);
            if (checkBoxyds.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 360, y, x + 365, y + 10);
                e.Graphics.DrawLine(pblue2, x + 365, y + 10, x + 370, y);
            }
            e.Graphics.DrawString("胰岛素", ptzt9, Brushes.Black, new Point(x + 370, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 415, y, 10, 10);
            if (checkBoxpiz.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 415, y, x + 420, y + 10);
                e.Graphics.DrawLine(pblue2, x + 420, y + 10, x + 425, y);
            }
            e.Graphics.DrawString("皮质激素", ptzt9, Brushes.Black, new Point(x + 425, y));
            e.Graphics.DrawString(textBoxnfb2.Text, ptzt9, Brushes.Black, new Point(x + 490, y));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("肌肉", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (jirouzc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("正常", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (jirouyc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("异常", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            if (jirouzzjwl.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawString("重症肌无力", ptzt9, Brushes.Black, new Point(x + 250, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 10, 10);
            if (jiroutanhuan.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 360, y, x + 365, y + 10);
                e.Graphics.DrawLine(pblue2, x + 365, y + 10, x + 370, y);
            }
            e.Graphics.DrawString("瘫痪", ptzt9, Brushes.Black, new Point(x + 370, y));
            e.Graphics.DrawString(textBoxjrqt.Text, ptzt9, Brushes.Black, new Point(x + 490, y));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("精神", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (jszc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("正常", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (jsyc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("异常", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            if (jsflz.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawString("精神分裂症", ptzt9, Brushes.Black, new Point(x + 250, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 10, 10);
            if (jsyyz.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 360, y, x + 365, y + 10);
                e.Graphics.DrawLine(pblue2, x + 365, y + 10, x + 370, y);
            }
            e.Graphics.DrawString("抑郁症", ptzt9, Brushes.Black, new Point(x + 370, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 435, y, 10, 10);
            if (jsjz.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 435, y, x + 440, y + 10);
                e.Graphics.DrawLine(pblue2, x + 440, y + 10, x + 445, y);
            }
            e.Graphics.DrawString("紧张", ptzt9, Brushes.Black, new Point(x + 445, y));
            e.Graphics.DrawString(textBoxjsqt.Text, ptzt9, Brushes.Black, new Point(x + 490, y));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("产科", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (ckyes.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("是", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (ckno.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("否", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            if (ckprq.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawString("哺乳期", ptzt9, Brushes.Black, new Point(x + 250, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 10, 10);
            if (ckcrq.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 360, y, x + 365, y + 10);
                e.Graphics.DrawLine(pblue2, x + 365, y + 10, x + 370, y);
            }
            e.Graphics.DrawString("产褥期", ptzt9, Brushes.Black, new Point(x + 370, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 435, y, 10, 10);
            if (ckyq.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 435, y, x + 440, y + 10);
                e.Graphics.DrawLine(pblue2, x + 440, y + 10, x + 445, y);
            }
            e.Graphics.DrawString("孕期", ptzt9, Brushes.Black, new Point(x + 445, y));
            e.Graphics.DrawString(textBoxckqt.Text, ptzt9, Brushes.Black, new Point(x + 490, y));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("吸烟、嗜酒、药物依赖", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (xysjyes.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("是", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (xysjno.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("否", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            if (xiyan.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawString("吸烟", ptzt9, Brushes.Black, new Point(x + 250, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 10, 10);
            if (shijiu.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 360, y, x + 365, y + 10);
                e.Graphics.DrawLine(pblue2, x + 365, y + 10, x + 370, y);
            }
            e.Graphics.DrawString("嗜酒", ptzt9, Brushes.Black, new Point(x + 370, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 415, y, 10, 10);
            if (yaowucy.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 415, y, x + 420, y + 10);
                e.Graphics.DrawLine(pblue2, x + 420, y + 10, x + 425, y);
            }
            e.Graphics.DrawString("药物成瘾", ptzt9, Brushes.Black, new Point(x + 425, y));
            e.Graphics.DrawString(textxy.Text, ptzt9, Brushes.Black, new Point(x + 490, y));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("过敏史/病历记录", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (gmsyes.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("是", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (gmsno.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("否", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            if (shiwugm.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawString("食物过敏", ptzt9, Brushes.Black, new Point(x + 250, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 10, 10);
            if (yaowugm.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 360, y, x + 365, y + 10);
                e.Graphics.DrawLine(pblue2, x + 365, y + 10, x + 370, y);
            }
            e.Graphics.DrawString("药物过敏", ptzt9, Brushes.Black, new Point(x + 370, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 435, y, 10, 10);
            if (yaoming.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 435, y, x + 440, y + 10);
                e.Graphics.DrawLine(pblue2, x + 440, y + 10, x + 445, y);
            }
            e.Graphics.DrawString("药名", ptzt9, Brushes.Black, new Point(x + 445, y));
            e.Graphics.DrawString(gmsqita.Text, ptzt9, Brushes.Black, new Point(x + 490, y));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("既往麻醉史", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (gwmzsyes.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("是", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (gwmzsno.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("否", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            if (cgkn.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawString("插管困难", ptzt9, Brushes.Black, new Point(x + 250, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 10, 10);
            if (mzygm.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 360, y, x + 365, y + 10);
                e.Graphics.DrawLine(pblue2, x + 365, y + 10, x + 370, y);
            }
            e.Graphics.DrawString("麻醉药过敏", ptzt9, Brushes.Black, new Point(x + 370, y));
            e.Graphics.DrawString(gwmzsqita.Text, ptzt9, Brushes.Black, new Point(x + 490, y));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("家族史/外科情况", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (jzsyes.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("是", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (jzsno.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("否", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            if (jzsmzygm.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawString("麻醉药过敏", ptzt9, Brushes.Black, new Point(x + 250, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 10, 10);
            if (exgr.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 360, y, x + 365, y + 10);
                e.Graphics.DrawLine(pblue2, x + 365, y + 10, x + 370, y);
            }
            e.Graphics.DrawString("恶性高热", ptzt9, Brushes.Black, new Point(x + 370, y));
            e.Graphics.DrawString(jzsqita.Text, ptzt9, Brushes.Black, new Point(x + 490, y));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("现在用特殊药物", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (xztsywyes.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("是", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (xztsywno.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("否", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawString(tesyaw.Text, ptzt9, Brushes.Black, new Point(x + 240, y));
            e.Graphics.DrawString(xztsywqt.Text, ptzt9, Brushes.Black, new Point(x + 490, y));
            y = y + 30; y1 = y + 15;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("全身情况好", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (qsqkyes.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("是", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (qsqkno.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("否", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawString(xzqsqk.Text, ptzt9, Brushes.Black, new Point(x + 240, y));
            e.Graphics.DrawString(gqqsqk.Text, ptzt9, Brushes.Black, new Point(x + 490, y));
            y = y + 25;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("气道通畅度", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (qdtcdyes.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("是", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (qdtcdno.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("否", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            if (checkBoxzhangk.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawString("张口<" + txtzk.Text + " cm", ptzt9, Brushes.Black, new Point(x + 250, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 345, y, 10, 10);
            if (checkBoxhansheng.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 345, y, x + 350, y + 10);
                e.Graphics.DrawLine(pblue2, x + 350, y + 10, x + 355, y);
            }
            e.Graphics.DrawString("鼾声", ptzt9, Brushes.Black, new Point(x + 355, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 390, y, 10, 10);
            if (checkBoxjinduan.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 390, y, x + 395, y + 10);
                e.Graphics.DrawLine(pblue2, x + 395, y + 10, x + 400, y);
            }
            e.Graphics.DrawString("颈短", ptzt9, Brushes.Black, new Point(x + 400, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 450, y, 10, 10);
            if (checkBoxthysx.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 450, y, x + 455, y + 10);
                e.Graphics.DrawLine(pblue2, x + 455, y + 10, x + 460, y);
            }
            e.Graphics.DrawString("头后仰受限", ptzt9, Brushes.Black, new Point(x + 460, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 550, y, 10, 10);
            if (checkBoxhjg.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 550, y, x + 555, y + 10);
                e.Graphics.DrawLine(pblue2, x + 555, y + 10, x + 560, y);
            }
            e.Graphics.DrawString("喉结高", ptzt9, Brushes.Black, new Point(x + 560, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 620, y, 10, 10);
            if (checkBoxxxg.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 620, y, x + 625, y + 10);
                e.Graphics.DrawLine(pblue2, x + 625, y + 10, x + 630, y);
            }
            e.Graphics.DrawString("小下颌", ptzt9, Brushes.Black, new Point(x + 630, y));
            y = y + 25;

            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            if (qgyw.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawString("气管移位", ptzt9, Brushes.Black, new Point(x + 250, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 345, y, 10, 10);
            if (qgyp.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 345, y, x + 350, y + 10);
                e.Graphics.DrawLine(pblue2, x + 350, y + 10, x + 355, y);
            }
            e.Graphics.DrawString("气管压迫", ptzt9, Brushes.Black, new Point(x + 355, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 430, y, 10, 10);
            if (qgzl.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 430, y, x + 435, y + 10);
                e.Graphics.DrawLine(pblue2, x + 435, y + 10, x + 440, y);
            }
            e.Graphics.DrawString("气管肿瘤", ptzt9, Brushes.Black, new Point(x + 440, y));

            y = y + 30;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("牙齿", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (qdtcdyes.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("正常", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (qdtcdno.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("异常", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            if (checkBoxzhangk.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawString("松动", ptzt9, Brushes.Black, new Point(x + 250, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 325, y, 10, 10);
            if (checkBoxhansheng.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 325, y, x + 330, y + 10);
                e.Graphics.DrawLine(pblue2, x + 330, y + 10, x + 335, y);
            }
            e.Graphics.DrawString("缺失", ptzt9, Brushes.Black, new Point(x + 335, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 390, y, 10, 10);
            if (checkBoxjinduan.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 390, y, x + 395, y + 10);
                e.Graphics.DrawLine(pblue2, x + 395, y + 10, x + 400, y);
            }
            e.Graphics.DrawString("戴冠", ptzt9, Brushes.Black, new Point(x + 400, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 450, y, 10, 10);
            if (checkBoxthysx.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 450, y, x + 455, y + 10);
                e.Graphics.DrawLine(pblue2, x + 455, y + 10, x + 460, y);
            }
            e.Graphics.DrawString("上牙", ptzt9, Brushes.Black, new Point(x + 460, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 550, y, 10, 10);
            if (checkBoxhjg.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 550, y, x + 555, y + 10);
                e.Graphics.DrawLine(pblue2, x + 555, y + 10, x + 560, y);
            }
            e.Graphics.DrawString("下牙", ptzt9, Brushes.Black, new Point(x + 560, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 600, y, 10, 10);
            if (checkBoxxxg.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 600, y, x + 605, y + 10);
                e.Graphics.DrawLine(pblue2, x + 605, y + 10, x + 610, y);
            }
            e.Graphics.DrawString("部分", ptzt9, Brushes.Black, new Point(x + 610, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 650, y, 10, 10);
            if (checkBoxxxg.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 650, y, x + 655, y + 10);
                e.Graphics.DrawLine(pblue2, x + 655, y + 10, x + 660, y);
            }
            e.Graphics.DrawString("全部", ptzt9, Brushes.Black, new Point(x + 660, y));

            y = y + 30;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("麻醉操作部位", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (czbwzc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("正常", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (czbwyc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("异常", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            if (ganran.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
                e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            }
            e.Graphics.DrawString("感染", ptzt9, Brushes.Black, new Point(x + 250, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 325, y, 10, 10);
            if (jixing.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 325, y, x + 330, y + 10);
                e.Graphics.DrawLine(pblue2, x + 330, y + 10, x + 335, y);
            }
            e.Graphics.DrawString("畸形", ptzt9, Brushes.Black, new Point(x + 335, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 390, y, 10, 10);
            if (waishang.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 390, y, x + 395, y + 10);
                e.Graphics.DrawLine(pblue2, x + 395, y + 10, x + 400, y);
            }
            e.Graphics.DrawString("外伤", ptzt9, Brushes.Black, new Point(x + 400, y));

            y = y + 30;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("胸片X片", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (xiongpianzc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("正常", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (xiongpianyc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("异常", ptzt10, Brushes.Black, new Point(x + 205, y));
            // e.Graphics.DrawString(xiongpianqita.Text, ptzt9, Brushes.Black, new Point(x + 240, y));
            string myString = xiongpianqita.Text.Replace("\n", string.Empty).Replace("\r", string.Empty);
            string xiongpian = "";
            int xiongpianzs = myString.Trim().Length;
            int rowxp = xiongpianzs / 30;

            for (int i = 0; i <= rowxp; i++)//50个字符就换行
            {
                if (i < rowxp)
                    xiongpian = myString.Substring(i * 30, 30); //从第i*50个开始，截取50个字符串
                else
                    xiongpian = myString.Substring(i * 30);
                e.Graphics.DrawString(xiongpian, ptzt9, Brushes.Black, new Point(x + 240, y + i * 15));

            }





            y = y + 30;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("心电图", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (xdtzc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("正常", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (xdtyc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("异常", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawString(xdtqt.Text, ptzt9, Brushes.Black, new Point(x + 240, y));
            y = y + 30;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("血常规", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (xcgzc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("正常", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (xcgyc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("异常", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawString(xcgqt.Text, ptzt9, Brushes.Black, new Point(x + 240, y));

            y = y + 30;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("免疫", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (miaoyizc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("正常", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (miaoyiyc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("异常", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawString(miaoyiqita.Text, ptzt9, Brushes.Black, new Point(x + 240, y));

            y = y + 30;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("凝血", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (ningxuezc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("正常", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (ningxueyc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("异常", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawString(ningxueqt.Text, ptzt9, Brushes.Black, new Point(x + 240, y));

            //y = y + 30;
            //e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            //e.Graphics.DrawString("生化", ptzt10, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            //if (shenghuazc.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
            //    e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            //}
            //e.Graphics.DrawString("正常", ptzt10, Brushes.Black, new Point(x + 160, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            //if (shenghuayc.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
            //    e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            //}
            //e.Graphics.DrawString("异常", ptzt10, Brushes.Black, new Point(x + 205, y));
            //e.Graphics.DrawString(shenghuaqt.Text, ptzt9, Brushes.Black, new Point(x + 240, y));

            y = y + 30;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            e.Graphics.DrawString("其他", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (qtzc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("正常", ptzt10, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            if (qtyc.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
                e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            }
            e.Graphics.DrawString("异常", ptzt10, Brushes.Black, new Point(x + 205, y));
            e.Graphics.DrawString(qtqt.Text, ptzt9, Brushes.Black, new Point(x + 240, y));
            y = y + 20;
            e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            y = y + 10;
            e.Graphics.DrawString("总体评估" + zongtipinggu.Text, ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("ASA分级", ptzt10, Brushes.Black, new Point(x + 230, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 280, y, 10, 10);
            if (asa1.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 280, y, x + 285, y + 10);
                e.Graphics.DrawLine(pblue2, x + 285, y + 10, x + 290, y);
            }
            e.Graphics.DrawString("Ⅰ", ptzt10, Brushes.Black, new Point(x + 290, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 310, y, 10, 10);
            if (asa2.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 310, y, x + 315, y + 10);
                e.Graphics.DrawLine(pblue2, x + 315, y + 10, x + 320, y);
            }
            e.Graphics.DrawString("Ⅱ", ptzt10, Brushes.Black, new Point(x + 320, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 340, y, 10, 10);
            if (asa3.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 340, y, x + 345, y + 10);
                e.Graphics.DrawLine(pblue2, x + 345, y + 10, x + 350, y);
            }
            e.Graphics.DrawString("Ⅲ", ptzt10, Brushes.Black, new Point(x + 350, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 370, y, 10, 10);
            if (asa4.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 370, y, x + 375, y + 10);
                e.Graphics.DrawLine(pblue2, x + 375, y + 10, x + 380, y);
            }
            e.Graphics.DrawString("Ⅳ", ptzt10, Brushes.Black, new Point(x + 380, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 400, y, 10, 10);
            if (asa5.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 400, y, x + 405, y + 10);
                e.Graphics.DrawLine(pblue2, x + 405, y + 10, x + 410, y);
            }
            e.Graphics.DrawString("Ⅴ", ptzt10, Brushes.Black, new Point(x + 410, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 430, y, 10, 10);
            if (asa6.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 430, y, x + 435, y + 10);
                e.Graphics.DrawLine(pblue2, x + 435, y + 10, x + 440, y);
            }
            e.Graphics.DrawString("E", ptzt10, Brushes.Black, new Point(x + 440, y));
            e.Graphics.DrawString("是否饱胃" + zongtipinggu.Text, ptzt10, Brushes.Black, new Point(x + 460, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 540, y, 10, 10);
            if (sbw.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 540, y, x + 545, y + 10);
                e.Graphics.DrawLine(pblue2, x + 545, y + 10, x + 550, y);
            }
            e.Graphics.DrawString("是", ptzt10, Brushes.Black, new Point(x + 550, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 580, y, 10, 10);
            if (bbw.Checked == true)
            {
                e.Graphics.DrawLine(pblue2, x + 580, y, x + 585, y + 10);
                e.Graphics.DrawLine(pblue2, x + 585, y + 10, x + 590, y);
            }
            e.Graphics.DrawString("否", ptzt10, Brushes.Black, new Point(x + 590, y));


            y = y + 20;
            e.Graphics.DrawString("目前存在的问题和建议：", ptzt10, Brushes.Black, new Point(x, y));
            string str1_zd = "";
            int StrLength_zd = mqczwtjy.Text.Trim().Length;
            int row_zd = StrLength_zd / 37;

            for (int i = 0; i <= row_zd; )//50个字符就换行
            {
                if (i < row_zd)
                    str1_zd = mqczwtjy.Text.ToString().Substring(i * 37, 37); //从第i*50个开始，截取50个字符串
                else
                    str1_zd = mqczwtjy.Text.ToString().Substring(i * 37);
                e.Graphics.DrawString(str1_zd, ptzt, Brushes.Black, x + 150, y);
                i++;
                if (i > row_zd)
                {

                }
                else
                {
                    y = y + 15;
                }

            }
            y = y + 30;
            e.Graphics.DrawString("术前麻醉医师签字：", ptzt10, Brushes.Black, new Point(x, y));
            e.Graphics.DrawString("日期：" + dateTimePicker1.Value.ToString("yyyy年MM月dd日"), ptzt10, Brushes.Black, new Point(x + 580, y));


          
          

        }
        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
           
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            
                Close();
          
        }

        private void textBoxxxg2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string url = "http://192.168.18.44/medexecgwebsetup/Pages/MedExViewNew.aspx?PID=" + menzhenhaov + "";
            System.Diagnostics.Process.Start(url);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string url = "http://192.168.18.55/modules/main/rmsysMain.aspx?rmkj_userno=webreport&HIS_PARAM_MODE=3&HIS_PARAM_PATNO=" + menzhenhaov + "";
            System.Diagnostics.Process.Start(url);
           
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string IPaddress = "192.168.18.14";
            bool flag = DataValid.PingHost(IPaddress, 1000);
            if (flag == true)
            {
                Process proc = new Process();
                proc.StartInfo.FileName = "D:\\Emr\\EmrCli.exe";
                proc.StartInfo.Arguments = "用户代码=10086|住院号=" + menzhenhaov + "|功能模块=个人病历浏览|只读=1|是否病案号=1";
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
            }
            else MessageBox.Show("电子病历 数据库未连接，请检查网络");
        }

    }
}
