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
using System.Diagnostics;

namespace main
{
    public partial class menzhenmazuijld : Form
    {
        //adims_BLL.mz bll = new adims_BLL.mz();//业务逻辑层
        adims_DAL.mz dal = new adims_DAL.mz();//数据访问层
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
     
        AdimsController bll = new AdimsController();
        DB2help emr = new DB2help();
        pacsdbhelp_oracle pacs = new pacsdbhelp_oracle();
        LIS_DB_Help lis = new LIS_DB_Help();
        string menzhenhaov;
        string menzhencishuv;
        public menzhenmazuijld(string menzhenhao,string menzhencishu)
        {
            menzhenhaov = menzhenhao;
            menzhencishuv = menzhencishu;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dal.UpdateMazuimzpgdantizhong(txbtizhong.Text, menzhencishuv);
            
            Dictionary<string, string> SQFS = new Dictionary<string, string>();
            int result = 0;
            string AddItem = "";
            SQFS.Add("zhuyuanhao", menzhencishuv);

    //       [ASAfj] [nvarchar](50) NULL,
    //[nixzlff] [nvarchar](50) NULL,
    //[mzff] [nvarchar](50) NULL,
    //[mzys] [nvarchar](50) NULL,
            SQFS.Add("ASAfj", comasafj.Text);
            SQFS.Add("nixzlff", txbnxss.Text);
            SQFS.Add("mzff", comzfs.Text);
            SQFS.Add("mzys", mazyscom.Text);
           
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
            if (xdtzc.Checked) AddItem += "1";
            if (xdtyc.Checked) AddItem += "2";
            SQFS.Add("xindiantu", AddItem);
            SQFS.Add("xdtqt", xdtqt.Text);

            AddItem = "";
            if (xcgzc.Checked) AddItem += "1";
            if (xcgyc.Checked) AddItem += "2";
            SQFS.Add("xcg", AddItem);
            SQFS.Add("xcgqt", xcgqt.Text);
            SQFS.Add("huanzhebenren", texthzbr.Text);
            SQFS.Add("weituor", textwtr.Text);
            SQFS.Add("riqi", Convert.ToDateTime(dateTimePicker1.Value.ToString()).ToString("yyyy-MM-dd HH:mm"));
            SQFS.Add("bbfbns", bbfbns.Text);
            DataTable dt = bll.Tljselectljmzmenzhensqfs(menzhencishuv);

            if (dt.Rows.Count > 0)
            {

                result = bll.Updatetljmzmenzhensqfs(SQFS);

            }
            else
                result = bll.TljInserttljmzmenzhensqfs(SQFS);
            if (result > 0)
            {
                MessageBox.Show("保存成功！");
            }
        
        }
        private void mzfs()
        {
            comzfs.Items.Clear();
            DataTable dt = new DataTable();
            dt = DAL.GetMZname();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comzfs.Items.Add(dt.Rows[i][0]);

            }
        }
        private void BindCombox3()
        {
           // mazyscom.Items.Clear();
            mazyscom.Items.Clear();
            DataTable dt = new DataTable();
            dt = DAL.GetAllMZYS();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                mazyscom.Items.Add(dt.Rows[i][0]);
               // mazyscom.Items.Add(dt.Rows[i][0]);
            }
        }
        private void menzhenmazuijld_Load(object sender, EventArgs e)
        {
            mzfs();
            BindCombox3();
            DataTable dt = dal.GetMazuimzpgdan(menzhencishuv);
            DataRow dr1 = dt.Rows[0];
            textzhuyuanhao.Text = dr1["menzhenhao"].ToString();
            txbname.Text = dr1["xingming"].ToString();
            txbage.Text = dr1["age"].ToString();
            txbsex.Text = dr1["sex"].ToString();
            txbtizhong.Text = dr1["tizhong"].ToString();
            textminzu.Text = dr1["mingzu"].ToString();
            txbkeshi.Text = dr1["keshi"].ToString();
            txbsqzd.Text = dr1["zhenduan"].ToString();
            DataTable dt1 = bll.Tljselectljmzmenzhensqfs(menzhencishuv);
            if (dt1.Rows.Count > 0)
            {
                DataRow dr = dt1.Rows[0];
                comasafj.Text = dr["ASAfj"].ToString();
                txbnxss.Text = dr["nixzlff"].ToString();
                comzfs.Text = dr["mzff"].ToString();
                mazyscom.Text = dr["mzys"].ToString();

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

               


                if (Convert.ToString(dr["xindiantu"]).Contains("1")) xdtzc.Checked = true;
                if (Convert.ToString(dr["xindiantu"]).Contains("2")) xdtyc.Checked = true;
                xdtqt.Text = dr["xdtqt"].ToString();

                if (Convert.ToString(dr["xcg"]).Contains("1")) xcgzc.Checked = true;
                if (Convert.ToString(dr["xcg"]).Contains("2")) xcgyc.Checked = true;
                xcgqt.Text = dr["xcgqt"].ToString();
                texthzbr.Text = dr["huanzhebenren"].ToString();
                textwtr.Text = dr["weituor"].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dr["riqi"]);
                bbfbns.Text = dr["bbfbns"].ToString();
              
            
            }
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
            Font ptzt13 = new Font("宋体", 12, FontStyle.Bold);//普通字体 
            Font ptzt6 = new Font("宋体", 13,FontStyle.Bold);//普通字体
            int y = 80; int x = 50; int y1 = 0;
           
            string title2 = "新疆医科大学第五附属医院无痛诊疗知情同意书";
            e.Graphics.DrawString(title2, ptzt3, Brushes.Black, new Point(x + 55, y));

            y = y + 50; y1 = y + 15;
            e.Graphics.DrawString(dateTimePicker1.Value.ToString("yyyy年MM月dd日"), ptzt11, Brushes.Black, new Point(x+500, y));
            y = y + 20; y1 = y + 15;
            e.Graphics.DrawString("姓名:" + txbname.Text.Trim(), ptzt11, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 15, x + 200, y + 15);
            e.Graphics.DrawString("性别:" + txbsex.Text.Trim(), ptzt11, Brushes.Black, new Point(x + 200, y));
            e.Graphics.DrawLine(Pens.Black, x + 240, y + 15, x + 280, y + 15);
         
            e.Graphics.DrawString("年龄:" + txbage.Text.Trim() + "岁", ptzt11, Brushes.Black, new Point(x + 290, y));
            e.Graphics.DrawString("族别:" + textminzu.Text.Trim(), ptzt11, Brushes.Black, new Point(x + 380, y));
            e.Graphics.DrawLine(Pens.Black, x + 420, y + 15, x + 480, y + 15);
            e.Graphics.DrawString("体重:" + txbtizhong.Text.Trim(), ptzt11, Brushes.Black, new Point(x + 500, y));
            e.Graphics.DrawLine(Pens.Black, x + 540, y + 15, x + 580, y + 15);
            e.Graphics.DrawString("kg", ptzt11, Brushes.Black, new Point(x + 580, y));
            y = y + 25;
            e.Graphics.DrawString("科室/床号:" + txbkeshi.Text.Trim() + " " + textch.Text, ptzt11, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 72, y + 15, x + 245, y + 15);
            e.Graphics.DrawString("住院号/门诊号:" + textzhuyuanhao.Text, ptzt11, Brushes.Black, new Point(x + 250, y));
            e.Graphics.DrawLine(Pens.Black, x + 340, y + 15, x + 445, y + 15);
            e.Graphics.DrawString("ASA分级:" + comasafj.Text, ptzt11, Brushes.Black, new Point(x + 480, y));
            e.Graphics.DrawLine(Pens.Black, x + 540, y + 15, x + 705, y + 15);
            y = y + 25;
            e.Graphics.DrawString("诊    断:" + txbsqzd.Text.Trim(), ptzt11, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 72, y + 15, x + 445, y + 15);
            e.Graphics.DrawString("拟行治疗方法:" + txbnxss.Text, ptzt11, Brushes.Black, new Point(x + 450, y));
            e.Graphics.DrawLine(Pens.Black, x + 552, y + 15, x + 705, y + 15);
            y = y + 25;
            //1999年
            e.Graphics.DrawString("麻醉方法:" + comzfs.Text.Trim(), ptzt11, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 72, y + 15, x + 445, y + 15);
            e.Graphics.DrawString("麻醉医生:", ptzt11, Brushes.Black, new Point(x + 450, y));
            e.Graphics.DrawLine(Pens.Black, x + 522, y + 15, x + 705, y + 15);
            y = y + 30;


            e.Graphics.DrawString("无痛诊疗中可能发生的医疗意外和并发症：", ptzt13, Brushes.Black, new Point(x, y));
            y = y + 30;
            e.Graphics.DrawString("1、药物及其他原因引起的过敏反应、中毒反应及特异质反应。", ptzt13, Brushes.Black, new Point(x, y));
            y = y + 30;
            e.Graphics.DrawString("2、损伤神经、血管或感染导致暂时或永久的功能障碍。", ptzt13, Brushes.Black, new Point(x, y));
            y = y + 30;
            e.Graphics.DrawString("3、治疗时可能发生呼吸抑制，胃内容物反流误吸、喉、气管及支气管痉挛，呼吸道梗塞等，", ptzt13, Brushes.Black, new Point(x, y));
            y = y +30;
            e.Graphics.DrawString("必要时需行气管内插管进行呼吸支持。", ptzt13, Brushes.Black, new Point(x, y));
            y = y + 30;
            e.Graphics.DrawString("4、循环变化使血压下降、心率减慢、心率失常、高血压、神经反射性血流动力学改变所致", ptzt13, Brushes.Black, new Point(x, y));
            y = y + 30;
            e.Graphics.DrawString("的循环骤停。", ptzt13, Brushes.Black, new Point(x, y));
            y = y + 30;
            e.Graphics.DrawString("5、患者本身合并其它疾病或有重要脏器损害者，其相关并发症和危险性显著增加，产生", ptzt13, Brushes.Black, new Point(x, y));
            y = y + 30;
            e.Graphics.DrawString("难以预料的不良后果、意外及并发症。", ptzt13, Brushes.Black, new Point(x, y));
            y = y + 30;
            e.Graphics.DrawString("6、需全身麻醉者，可能出现治疗中知晓和治疗后回忆，各种原因所致的苏醒延迟。", ptzt13, Brushes.Black, new Point(x, y));
            y = y + 30;
            e.Graphics.DrawString("7、由于各种因素影响而达不到预期的无痛诊疗效果。", ptzt13, Brushes.Black, new Point(x, y));
            y = y + 30;
            e.Graphics.DrawString("8、自费项目：无插管全麻210元，麻醉恢复室监护70元。", ptzt13, Brushes.Black, new Point(x, y));
            y = y + 30;
            e.Graphics.DrawString("9、其它 丙泊酚不耐受:"+bbfbns.Text+"。", ptzt13, Brushes.Black, new Point(x, y));
            y = y + 30;
            e.Graphics.DrawString("患者或授权人知情、同意并签字：患者本人：               委托人：", ptzt6, Brushes.Black, new Point(x, y));
            y = y + 30;
            string title1 = "新疆医科大学第五附属医院麻醉门诊访视单";
            e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 80, y));
            y = y + 50; y1 = y + 15;
            e.Graphics.DrawString("系统情况", ptzt, Brushes.Black, new Point(x + 65, y));
            e.Graphics.DrawString("现在状态", ptzt, Brushes.Black, new Point(x + 320, y));
            e.Graphics.DrawString("过去或其它情况", ptzt, Brushes.Black, new Point(x + 520, y));
            e.Graphics.DrawLine(pblack, x, y1 + 5, x + 710, y1 + 5);
            y = y + 25; y1 = y + 15;
            e.Graphics.DrawLine(ptp, x + 235, y - 5, x + 235, y + 300);
            e.Graphics.DrawLine(ptp, x + 490, y - 5, x + 490, y +155);
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

            
            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            //e.Graphics.DrawString("精神", ptzt10, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            //if (jszc.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
            //    e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            //}
            //e.Graphics.DrawString("正常", ptzt10, Brushes.Black, new Point(x + 160, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            //if (jsyc.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
            //    e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            //}
            //e.Graphics.DrawString("异常", ptzt10, Brushes.Black, new Point(x + 205, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            //if (jsflz.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
            //    e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            //}
            //e.Graphics.DrawString("精神分裂症", ptzt9, Brushes.Black, new Point(x + 250, y));

            //e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 10, 10);
            //if (jsyyz.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblue2, x + 360, y, x + 365, y + 10);
            //    e.Graphics.DrawLine(pblue2, x + 365, y + 10, x + 370, y);
            //}
            //e.Graphics.DrawString("抑郁症", ptzt9, Brushes.Black, new Point(x + 370, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 435, y, 10, 10);
            //if (jsjz.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblue2, x + 435, y, x + 440, y + 10);
            //    e.Graphics.DrawLine(pblue2, x + 440, y + 10, x + 445, y);
            //}
            //e.Graphics.DrawString("紧张", ptzt9, Brushes.Black, new Point(x + 445, y));
            //e.Graphics.DrawString(textBoxjsqt.Text, ptzt9, Brushes.Black, new Point(x + 490, y));
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
            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            //e.Graphics.DrawString("既往麻醉史", ptzt10, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            //if (gwmzsyes.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
            //    e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            //}
            //e.Graphics.DrawString("是", ptzt10, Brushes.Black, new Point(x + 160, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            //if (gwmzsno.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
            //    e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            //}
            //e.Graphics.DrawString("否", ptzt10, Brushes.Black, new Point(x + 205, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            //if (cgkn.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
            //    e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            //}
            //e.Graphics.DrawString("插管困难", ptzt9, Brushes.Black, new Point(x + 250, y));

            //e.Graphics.DrawRectangle(Pens.Black, x + 360, y, 10, 10);
            //if (mzygm.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblue2, x + 360, y, x + 365, y + 10);
            //    e.Graphics.DrawLine(pblue2, x + 365, y + 10, x + 370, y);
            //}
            //e.Graphics.DrawString("麻醉药过敏", ptzt9, Brushes.Black, new Point(x + 370, y));
            //e.Graphics.DrawString(gwmzsqita.Text, ptzt9, Brushes.Black, new Point(x + 490, y));
           
            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            //e.Graphics.DrawString("现在用特殊药物", ptzt10, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            //if (xztsywyes.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
            //    e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            //}
            //e.Graphics.DrawString("是", ptzt10, Brushes.Black, new Point(x + 160, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            //if (xztsywno.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
            //    e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            //}
            //e.Graphics.DrawString("否", ptzt10, Brushes.Black, new Point(x + 205, y));
            //e.Graphics.DrawString(tesyaw.Text, ptzt9, Brushes.Black, new Point(x + 240, y));
            //e.Graphics.DrawString(xztsywqt.Text, ptzt9, Brushes.Black, new Point(x + 490, y));
            //y = y + 30; y1 = y + 15;
            //e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            //e.Graphics.DrawString("全身情况好", ptzt10, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            //if (qsqkyes.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
            //    e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            //}
            //e.Graphics.DrawString("是", ptzt10, Brushes.Black, new Point(x + 160, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            //if (qsqkno.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
            //    e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            //}
            //e.Graphics.DrawString("否", ptzt10, Brushes.Black, new Point(x + 205, y));
            //e.Graphics.DrawString(xzqsqk.Text, ptzt9, Brushes.Black, new Point(x + 240, y));
            //e.Graphics.DrawString(gqqsqk.Text, ptzt9, Brushes.Black, new Point(x + 490, y));
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

            //y = y + 30;
            //e.Graphics.DrawLine(pblack, x, y - 3, x + 710, y - 3);
            //e.Graphics.DrawString("麻醉操作部位", ptzt10, Brushes.Black, new Point(x, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            //if (czbwzc.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblue2, x + 150, y, x + 155, y + 10);
            //    e.Graphics.DrawLine(pblue2, x + 155, y + 10, x + 160, y);
            //}
            //e.Graphics.DrawString("正常", ptzt10, Brushes.Black, new Point(x + 160, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 195, y, 10, 10);
            //if (czbwyc.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblue2, x + 195, y, x + 200, y + 10);
            //    e.Graphics.DrawLine(pblue2, x + 200, y + 10, x + 205, y);
            //}
            //e.Graphics.DrawString("异常", ptzt10, Brushes.Black, new Point(x + 205, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 240, y, 10, 10);
            //if (ganran.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblue2, x + 240, y, x + 245, y + 10);
            //    e.Graphics.DrawLine(pblue2, x + 245, y + 10, x + 250, y);
            //}
            //e.Graphics.DrawString("感染", ptzt9, Brushes.Black, new Point(x + 250, y));

            //e.Graphics.DrawRectangle(Pens.Black, x + 325, y, 10, 10);
            //if (jixing.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblue2, x + 325, y, x + 330, y + 10);
            //    e.Graphics.DrawLine(pblue2, x + 330, y + 10, x + 335, y);
            //}
            //e.Graphics.DrawString("畸形", ptzt9, Brushes.Black, new Point(x + 335, y));
            //e.Graphics.DrawRectangle(Pens.Black, x + 390, y, 10, 10);
            //if (waishang.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblue2, x + 390, y, x + 395, y + 10);
            //    e.Graphics.DrawLine(pblue2, x + 395, y + 10, x + 400, y);
            //}
            //e.Graphics.DrawString("外伤", ptzt9, Brushes.Black, new Point(x + 400, y));

            


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
            y = y + 10;
            e.Graphics.DrawString("特殊情况: "+xzqsqk.Text, ptzt9, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(pblack, x+65, y +15, x + 710, y +15);
          
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string url = "http://192.168.18.44/MedExECGWEbSetup/Default.aspx";
            System.Diagnostics.Process.Start(url);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string IPaddress = "192.168.18.55";
            bool flag = DataValid.PingHost(IPaddress, 1000);
            if (flag == true)
            {
                string inpatientID = menzhenhaov;
                string url = "http://192.168.18.55/modules/main/rmsysMain.aspx?rmkj_userno=webreport&HIS_PARAM_MODE=3&HIS_PARAM_PATNO=" + inpatientID + "";
                System.Diagnostics.Process.Start(url);
            }
            else MessageBox.Show("检验信息 数据库未连接，请检查网络");
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
