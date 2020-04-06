using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace main
{
    public partial class Jhzyysshljld : Form
    {
        adims_BLL.YXL_BLL cll = new adims_BLL.YXL_BLL();
        private string patid = "";
        public Jhzyysshljld(string pid)
        {
            patid = pid;
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Jhzyysshljld_Load(object sender, EventArgs e)
        {
            jichuxianshi();
            xianshi();
            
        }
        private void jibenxinxi()
        {
 
        }
        private void jichuxianshi()
        {
            DataTable dp = cll.jichujiazai(patid);
            if (dp.Rows.Count > 0)
            {
               // Patname,Patsex,Patage,Patdbm,Patdbm
                DataRow da = dp.Rows[0];
                textxm.Text = da["Patname"].ToString();
                textxb.Text = da["Patsex"].ToString();
                textnl.Text = da["Patage"].ToString();
                textbq.Text = da["Patdpm"].ToString();
                textch.Text = da["Patbedno"].ToString();
                textzyblh.Text = da["PatID"].ToString();
                textsqzd.Text = da["Pattmd"].ToString();
                dateTimessrq.Text = da["Odate"].ToString();
                textssmc.Text = da["Oname"].ToString();
                //textssj.Text = da[""].ToString();
            }
        }
        private void xianshi()
        {
            
            //textzyblh.Text = patid;
             DataTable dt = cll.jiazai(patid);
             if (dt.Rows.Count > 0)
             {
                 DataRow da = dt.Rows[0];
                 //textxm.Text = da["xm"].ToString();
                 //textxb.Text = da["xb"].ToString();
                 //textnl.Text = da["ln"].ToString();
                 //textbq.Text = da["bq"].ToString();
                 //textch.Text = da["ch"].ToString();
                 ////textzyblh.Text = da["zyblh"].ToString();
                 
                 //dateTimessrq.Text = da["ssrq"].ToString();
                 //textsqzd.Text = da["sqzd"].ToString();
                 //textssmc.Text = da["ssmc"].ToString();
                 //textssj.Text = da["ssj"].ToString();
                 dateTimerssj.Text = da["rssj"].ToString();
                 if (Convert.ToString(da["fxk"]).Contains("1")) brhdy.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("2")) brhdw.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("3")) ssbwhdy.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("4")) ssbwhdw.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("5")) szqx.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("6")) szmh.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("7")) szysbq.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("8")) sjmccy.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("9")) sjmccw.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("a")) jmsyy.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("b")) jmsyw.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("c")) wgy.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("d")) wgw.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("e")) dngy.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("f")) dngw.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("j")) zxdy.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("h")) zxdw.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("i")) csy.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("g")) csw.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("k")) csbsy.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("r")) jsmclhjql.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("m")) yfycyj.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("n")) qt.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("o")) zrwbstybm.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("p")) zrwbsyscl.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("q")) shshbf.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("l")) shshicu.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("s")) shshmzhfs.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("t")) shshqt.Checked = true;
                 if (Convert.ToString(da["fxk"]).Contains("u")) wjbjchg.Checked = true;
                 sqycfxpg.Text = da["sqycfxpg"].ToString();
                 pfqk.Text = da["pfqk"].ToString();
                 ywgms.Text = da["ywgms"].ToString();
                 tw.Text = da["tw"].ToString();
                 yl.Text = da["yl"].ToString();
                 sj.Text = da["sj"].ToString();
                 zrwmcjsl.Text = da["zrwpmc"].ToString();
                 bbmcjsl.Text = da["bbmc"].ToString();
                 ysqk.Text = da["ysqk"].ToString();
                 pfzk.Text = da["sbpfqk"].ToString();
                 lssj.Text = da["lssj"].ToString();
                 qt1.Text = da["sbqt"].ToString();
                 ylg.Text = da["ylg"].ToString();
                 bw.Text = da["bw"].ToString();
                 zywjb.Text = da["zywjb"].ToString();
                 qm.Text = da["qm"].ToString();
                 
             }
             infochangqixie();
             infochangfuliao();
             infochangqdhd();
            
        }//查询
        private void infochangqixie()
        {
            try
            {
                DataTable dt = cll.jhqixiefuliao1(patid);
                if (dt.Rows.Count == 0)
                {
                    DataTable dtr = cll.jhqixiefuliao1("1");
                    for (int i = 0; i < dtr.Rows.Count; i++)
                    {
                        if (i >= qxqd.Rows.Count)
                        {
                            qxqd.Rows.Add();
                        }
                        qxqd.Rows[i].Cells[0].Value = dtr.Rows[i]["qxpm"];
                        qxqd.Rows[i].Cells[1].Value = dtr.Rows[i]["sqqd"];
                        qxqd.Rows[i].Cells[2].Value = dtr.Rows[i]["gqhd"];
                        qxqd.Rows[i].Cells[3].Value = dtr.Rows[i]["ghhd"];
                        qxqd.Rows[i].Cells[4].Value = dtr.Rows[i]["sbhd"];
                        qxqd.Rows[i].Cells[5].Value = dtr.Rows[i]["qxpm1"];
                        qxqd.Rows[i].Cells[6].Value = dtr.Rows[i]["sqqd1"];
                        qxqd.Rows[i].Cells[7].Value = dtr.Rows[i]["gqhd1"];
                        qxqd.Rows[i].Cells[8].Value = dtr.Rows[i]["ghhd1"];
                        qxqd.Rows[i].Cells[9].Value = dtr.Rows[i]["sbhd1"];
                        qxqd.Rows[i].Cells[10].Value = dtr.Rows[i]["qxpm2"];
                        qxqd.Rows[i].Cells[11].Value = dtr.Rows[i]["sqqd2"];
                        qxqd.Rows[i].Cells[12].Value = dtr.Rows[i]["gqhd2"];
                        qxqd.Rows[i].Cells[13].Value = dtr.Rows[i]["ghhd2"];
                        qxqd.Rows[i].Cells[14].Value = dtr.Rows[i]["sbhd2"];
                    }
                    qxqd.AllowUserToAddRows = false;
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i >= qxqd.Rows.Count)
                        {
                            qxqd.Rows.Add();
                        }
                        qxqd.Rows[i].Cells[0].Value = dt.Rows[i]["qxpm"];
                        qxqd.Rows[i].Cells[1].Value = dt.Rows[i]["sqqd"];
                        qxqd.Rows[i].Cells[2].Value = dt.Rows[i]["gqhd"];
                        qxqd.Rows[i].Cells[3].Value = dt.Rows[i]["ghhd"];
                        qxqd.Rows[i].Cells[4].Value = dt.Rows[i]["sbhd"];
                        qxqd.Rows[i].Cells[5].Value = dt.Rows[i]["qxpm1"];
                        qxqd.Rows[i].Cells[6].Value = dt.Rows[i]["sqqd1"];
                        qxqd.Rows[i].Cells[7].Value = dt.Rows[i]["gqhd1"];
                        qxqd.Rows[i].Cells[8].Value = dt.Rows[i]["ghhd1"];
                        qxqd.Rows[i].Cells[9].Value = dt.Rows[i]["sbhd1"];
                        qxqd.Rows[i].Cells[10].Value = dt.Rows[i]["qxpm2"];
                        qxqd.Rows[i].Cells[11].Value = dt.Rows[i]["sqqd2"];
                        qxqd.Rows[i].Cells[12].Value = dt.Rows[i]["gqhd2"];
                        qxqd.Rows[i].Cells[13].Value = dt.Rows[i]["ghhd2"];
                        qxqd.Rows[i].Cells[14].Value = dt.Rows[i]["sbhd2"];
                    }
                    qxqd.AllowUserToAddRows = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "器械清点加载失败");
            }
        }//器械查询
        private void infochangfuliao()
        {
            try
            {
                DataTable dt = cll.jhqixiefuliao(patid);
                if (dt.Rows.Count == 0)
                {
                    DataTable dtr = cll.jhqixiefuliao("1");
                    for (int i = 0; i < dtr.Rows.Count; i++)
                    {
                        if (i >= flqd.Rows.Count)
                        {
                            flqd.Rows.Add();
                        }
                        flqd.Rows[i].Cells[0].Value = dtr.Rows[i]["pm"];
                        flqd.Rows[i].Cells[1].Value = dtr.Rows[i]["sqqd"];
                        flqd.Rows[i].Cells[2].Value = dtr.Rows[i]["gqhd"];
                        flqd.Rows[i].Cells[3].Value = dtr.Rows[i]["ghhd"];
                        flqd.Rows[i].Cells[4].Value = dtr.Rows[i]["sbhd"];
                        flqd.Rows[i].Cells[5].Value = dtr.Rows[i]["pm1"];
                        flqd.Rows[i].Cells[6].Value = dtr.Rows[i]["sqqd1"];
                        flqd.Rows[i].Cells[7].Value = dtr.Rows[i]["gqhd1"];
                        flqd.Rows[i].Cells[8].Value = dtr.Rows[i]["ghhd1"];
                        flqd.Rows[i].Cells[9].Value = dtr.Rows[i]["sbhd1"];
                    }
                    flqd.AllowUserToAddRows = false;
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i >= flqd.Rows.Count)
                        {
                            flqd.Rows.Add();
                        }
                        flqd.Rows[i].Cells[0].Value = dt.Rows[i]["pm"];
                        flqd.Rows[i].Cells[1].Value = dt.Rows[i]["sqqd"];
                        flqd.Rows[i].Cells[2].Value = dt.Rows[i]["gqhd"];
                        flqd.Rows[i].Cells[3].Value = dt.Rows[i]["ghhd"];
                        flqd.Rows[i].Cells[4].Value = dt.Rows[i]["sbhd"];
                        flqd.Rows[i].Cells[5].Value = dt.Rows[i]["pm1"];
                        flqd.Rows[i].Cells[6].Value = dt.Rows[i]["sqqd1"];
                        flqd.Rows[i].Cells[7].Value = dt.Rows[i]["gqhd1"];
                        flqd.Rows[i].Cells[8].Value = dt.Rows[i]["ghhd1"];
                        flqd.Rows[i].Cells[9].Value = dt.Rows[i]["sbhd1"];
                    }
                    flqd.AllowUserToAddRows = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "止血带包清点加载失败");
            }
        }//敷料查询
        private void infochangqdhd()
        {
            try
            {
                DataTable dt = cll.hdqd(patid);
                if (dt.Rows.Count == 0)
                {
                    DataTable dtr = cll.hdqd("1");
                    for (int i = 0; i < dtr.Rows.Count; i++)
                    {
                        if (i >= qmhd.Rows.Count)
                        {
                            qmhd.Rows.Add();
                        }
                        qmhd.Rows[i].Cells[0].Value = dtr.Rows[i]["zrr"];
                        qmhd.Rows[i].Cells[1].Value = dtr.Rows[i]["sqqd"];
                        qmhd.Rows[i].Cells[2].Value = dtr.Rows[i]["gqhd"];
                        qmhd.Rows[i].Cells[3].Value = dtr.Rows[i]["ghhd"];
                        qmhd.Rows[i].Cells[4].Value = dtr.Rows[i]["sbhd"];
                    }
                    qmhd.AllowUserToAddRows = false;
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i >= qmhd.Rows.Count)
                        {
                            qmhd.Rows.Add();
                        }
                        qmhd.Rows[i].Cells[0].Value = dt.Rows[i]["zrr"];
                        qmhd.Rows[i].Cells[1].Value = dt.Rows[i]["sqqd"];
                        qmhd.Rows[i].Cells[2].Value = dt.Rows[i]["gqhd"];
                        qmhd.Rows[i].Cells[3].Value = dt.Rows[i]["ghhd"];
                        qmhd.Rows[i].Cells[4].Value = dt.Rows[i]["sbhd"];
                    }
                    qmhd.AllowUserToAddRows = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "签名核对加载失败");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            baocun();
            
        }
        private void baocun()
        {
            int result = 0;
            Dictionary<string, string> SQFS = new Dictionary<string, string>();
            SQFS.Add("xm", textxm.Text);
            SQFS.Add("xb", textxb.Text);
            SQFS.Add("ln", textnl.Text);
            SQFS.Add("bq", textbq.Text);
            SQFS.Add("ch", textch.Text);
            SQFS.Add("zyblh", textzyblh.Text);
            SQFS.Add("ssrq", dateTimessrq.Text);
            SQFS.Add("sqzd", textsqzd.Text);
            SQFS.Add("ssmc", textssmc.Text);
            SQFS.Add("ssj", textssj.Text);
            SQFS.Add("rssj", dateTimerssj.Text);



            string AddItem = "";
            AddItem = "";
            if (brhdy.Checked) AddItem += "1";
            if (brhdw.Checked) AddItem += "2";
            if (ssbwhdy.Checked) AddItem += "3";
            if (ssbwhdw.Checked) AddItem += "4";
            if (szqx.Checked) AddItem += "5";
            if (szmh.Checked) AddItem += "6";
            if (szysbq.Checked) AddItem += "7";
            if (sjmccy.Checked) AddItem += "8";
            if (sjmccw.Checked) AddItem += "9";
            if (jmsyy.Checked) AddItem += "a";
            if (jmsyw.Checked) AddItem += "b";
            if (wgy.Checked) AddItem += "c";
            if (wgw.Checked) AddItem += "d";
            if (dngy.Checked) AddItem += "e";
            if (dngw.Checked) AddItem += "f";
            SQFS.Add("sqycfxpg", sqycfxpg.Text);
            SQFS.Add("pfqk", pfqk.Text);
            SQFS.Add("ywgms", ywgms.Text);
            SQFS.Add("tw", tw.Text);
            if (zxdy.Checked) AddItem += "j";
            if (zxdw.Checked) AddItem += "h";
            SQFS.Add("yl", yl.Text);
            SQFS.Add("sj", sj.Text);
            if (csy.Checked) AddItem += "i";
            if (csw.Checked) AddItem += "g";
            if (csbsy.Checked) AddItem += "k";
            if (jsmclhjql.Checked) AddItem += "r";
            if (yfycyj.Checked) AddItem += "m";
            if (qt.Checked) AddItem += "n";
            SQFS.Add("zrwpmc", zrwmcjsl.Text);
            if (zrwbstybm.Checked) AddItem += "o";
            if (zrwbsyscl.Checked) AddItem += "p";
            SQFS.Add("bbmc", bbmcjsl.Text);
            SQFS.Add("ysqk", ysqk.Text);
            SQFS.Add("sbpfqk", pfzk.Text);
            if (shshbf.Checked) AddItem += "q";
            if (shshicu.Checked) AddItem += "l";
            if (shshmzhfs.Checked) AddItem += "s";
            if (shshqt.Checked) AddItem += "t";
            SQFS.Add("lssj", lssj.Text);
            SQFS.Add("sbqt", qt1.Text);
            SQFS.Add("ylg", ylg.Text);
            SQFS.Add("ylgbw", bw.Text);
            SQFS.Add("zywjb", zywjb.Text);
            if (wjbjchg.Checked) AddItem += "u";
            SQFS.Add("qm", qm.Text);
            SQFS.Add("fxk", AddItem);


            DataTable dt = cll.jiazai(patid);
            if (dt.Rows.Count > 0)
            {

                result = cll.jhXiuGai(SQFS);

            }
            else
                result = cll.jhXingZeng(SQFS);
            if (result > 0)
            {
               
            }
            Getqixie();
            Getfuliao();
            Getqdhd();
        }//保存
        private void Getqixie()//器械保存
        {
            try
            {
                DataTable b = cll.jhqixiefuliao1(patid);

                if (b.Rows.Count == 0)
                {
                    for (int i = 0; i < qxqd.Rows.Count; i++)
                    {
                        for (int j = 0; j < 15; j++)
                        {
                            if (qxqd.Rows[i].Cells[j].Value == null)
                                qxqd.Rows[i].Cells[j].Value = "";
                        }

                        Dictionary<string, string> zxdList = new Dictionary<string, string>();
                        zxdList.Clear();
                        int result = 0;
                        zxdList.Add("zyblh", patid);
                        zxdList.Add("qxpm", qxqd.Rows[i].Cells[0].Value.ToString());
                        zxdList.Add("sqqd", qxqd.Rows[i].Cells[1].Value.ToString());
                        zxdList.Add("gqhd", qxqd.Rows[i].Cells[2].Value.ToString());
                        zxdList.Add("ghhd", qxqd.Rows[i].Cells[3].Value.ToString());
                        zxdList.Add("sbhd", qxqd.Rows[i].Cells[4].Value.ToString());
                        zxdList.Add("qxpm1", qxqd.Rows[i].Cells[5].Value.ToString());
                        zxdList.Add("sqqd1", qxqd.Rows[i].Cells[6].Value.ToString());
                        zxdList.Add("gqhd1", qxqd.Rows[i].Cells[7].Value.ToString());
                        zxdList.Add("ghhd1", qxqd.Rows[i].Cells[8].Value.ToString());
                        zxdList.Add("sbhd1", qxqd.Rows[i].Cells[9].Value.ToString());
                        zxdList.Add("qxpm2", qxqd.Rows[i].Cells[10].Value.ToString());
                        zxdList.Add("sqqd2", qxqd.Rows[i].Cells[11].Value.ToString());
                        zxdList.Add("gqhd2", qxqd.Rows[i].Cells[12].Value.ToString());
                        zxdList.Add("ghhd2", qxqd.Rows[i].Cells[13].Value.ToString());
                        zxdList.Add("sbhd2", qxqd.Rows[i].Cells[14].Value.ToString());
                        result = cll.jhqxXingZeng(zxdList);
                    }
                }
                else
                {
                    for (int i = 0; i < qxqd.Rows.Count; i++)
                    {
                        for (int j = 0; j < 15; j++)
                        {
                            if (qxqd.Rows[i].Cells[j].Value == null)
                                qxqd.Rows[i].Cells[j].Value = "";
                        }
                        Dictionary<string, string> zxdList = new Dictionary<string, string>();
                        zxdList.Clear();
                        int result = 0;
                        zxdList.Add("zyblh", patid);
                        string IDh = b.Rows[i][0].ToString();
                        zxdList.Add("ID", IDh);
                        zxdList.Add("qxpm", qxqd.Rows[i].Cells[0].Value.ToString());
                        zxdList.Add("sqqd", qxqd.Rows[i].Cells[1].Value.ToString());
                        zxdList.Add("gqhd", qxqd.Rows[i].Cells[2].Value.ToString());
                        zxdList.Add("ghhd", qxqd.Rows[i].Cells[3].Value.ToString());
                        zxdList.Add("sbhd", qxqd.Rows[i].Cells[4].Value.ToString());
                        zxdList.Add("qxpm1", qxqd.Rows[i].Cells[5].Value.ToString());
                        zxdList.Add("sqqd1", qxqd.Rows[i].Cells[6].Value.ToString());
                        zxdList.Add("gqhd1", qxqd.Rows[i].Cells[7].Value.ToString());
                        zxdList.Add("ghhd1", qxqd.Rows[i].Cells[8].Value.ToString());
                        zxdList.Add("sbhd1", qxqd.Rows[i].Cells[9].Value.ToString());
                        zxdList.Add("qxpm2", qxqd.Rows[i].Cells[10].Value.ToString());
                        zxdList.Add("sqqd2", qxqd.Rows[i].Cells[11].Value.ToString());
                        zxdList.Add("gqhd2", qxqd.Rows[i].Cells[12].Value.ToString());
                        zxdList.Add("ghhd2", qxqd.Rows[i].Cells[13].Value.ToString());
                        zxdList.Add("sbhd2", qxqd.Rows[i].Cells[14].Value.ToString());
                        result = cll.jhQXXiuGai(zxdList);
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }
        private void Getfuliao()//敷料保存
        {
            
            DataTable q = cll.jhqixiefuliao(patid);
            
            if (q.Rows.Count == 0)
            {
                for (int i = 0; i < flqd.Rows.Count; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (flqd.Rows[i].Cells[j].Value == null)
                            flqd.Rows[i].Cells[j].Value = "";
                    }
                    Dictionary<string, string> zxdList = new Dictionary<string, string>();
                    zxdList.Clear();
                    int result = 0;
                    zxdList.Add("zyblh", patid);
                    zxdList.Add("pm", flqd.Rows[i].Cells[0].Value.ToString());
                    zxdList.Add("sqqd", flqd.Rows[i].Cells[1].Value.ToString());
                    zxdList.Add("gqhd", flqd.Rows[i].Cells[2].Value.ToString());
                    zxdList.Add("ghhd", flqd.Rows[i].Cells[3].Value.ToString());
                    zxdList.Add("sbhd", flqd.Rows[i].Cells[4].Value.ToString());
                    zxdList.Add("pm1", flqd.Rows[i].Cells[5].Value.ToString());
                    zxdList.Add("sqqd1", flqd.Rows[i].Cells[6].Value.ToString());
                    zxdList.Add("gqhd1", flqd.Rows[i].Cells[7].Value.ToString());
                    zxdList.Add("ghhd1", flqd.Rows[i].Cells[8].Value.ToString());
                    zxdList.Add("sbhd1", flqd.Rows[i].Cells[9].Value.ToString());
                    result = cll.jhflXingZeng(zxdList);
                }
            }
            else
            {
                for (int i = 0; i < flqd.Rows.Count; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (flqd.Rows[i].Cells[j].Value == null)
                            flqd.Rows[i].Cells[j].Value = "";
                    }
                    Dictionary<string, string> zxdList = new Dictionary<string, string>();
                    zxdList.Clear();
                    int result = 0;
                    zxdList.Add("zyblh", patid);
                    string IDh = q.Rows[i][0].ToString();
                    zxdList.Add("ID", IDh);
                    zxdList.Add("pm", flqd.Rows[i].Cells[0].Value.ToString());
                    zxdList.Add("sqqd", flqd.Rows[i].Cells[1].Value.ToString());
                    zxdList.Add("gqhd", flqd.Rows[i].Cells[2].Value.ToString());
                    zxdList.Add("ghhd", flqd.Rows[i].Cells[3].Value.ToString());
                    zxdList.Add("sbhd", flqd.Rows[i].Cells[4].Value.ToString());
                    zxdList.Add("pm1", flqd.Rows[i].Cells[5].Value.ToString());
                    zxdList.Add("sqqd1", flqd.Rows[i].Cells[6].Value.ToString());
                    zxdList.Add("gqhd1", flqd.Rows[i].Cells[7].Value.ToString());
                    zxdList.Add("ghhd1", flqd.Rows[i].Cells[8].Value.ToString());
                    zxdList.Add("sbhd1", flqd.Rows[i].Cells[9].Value.ToString());
                    result = cll.jhFLXiuGai(zxdList);
                }
            }


        }
        private void Getqdhd()//责任人清点核对签名
        {
            int result = 0;
            DataTable q = cll.hdqd(patid);
            if (q.Rows.Count <= 0)
            {
                for (int i = 0; i < qmhd.Rows.Count; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (qmhd.Rows[i].Cells[j].Value == null)
                            qmhd.Rows[i].Cells[j].Value = "";
                    }
                    Dictionary<string, string> zxdList = new Dictionary<string, string>();
                    zxdList.Clear();
                    zxdList.Add("zyblh", patid);
                    zxdList.Add("zrr", qmhd.Rows[i].Cells[0].Value.ToString());
                    zxdList.Add("sqqd", qmhd.Rows[i].Cells[1].Value.ToString());
                    zxdList.Add("gqhd", qmhd.Rows[i].Cells[2].Value.ToString());
                    zxdList.Add("ghhd", qmhd.Rows[i].Cells[3].Value.ToString());
                    zxdList.Add("sbhd", qmhd.Rows[i].Cells[4].Value.ToString());
                    result = cll.jhqmhdXingZeng(zxdList);
                }
            }
            else
            {
                for (int i = 0; i < qmhd.Rows.Count; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (qmhd.Rows[i].Cells[j].Value == null)
                            qmhd.Rows[i].Cells[j].Value = "";
                    }
                    Dictionary<string, string> zxdList = new Dictionary<string, string>();
                    zxdList.Clear();
                    
                    zxdList.Add("zyblh", patid);
                    string IDh = q.Rows[i][0].ToString();
                    zxdList.Add("ID", IDh);
                    zxdList.Add("zrr", qmhd.Rows[i].Cells[0].Value.ToString());
                    zxdList.Add("sqqd", qmhd.Rows[i].Cells[1].Value.ToString());
                    zxdList.Add("gqhd", qmhd.Rows[i].Cells[2].Value.ToString());
                    zxdList.Add("ghhd", qmhd.Rows[i].Cells[3].Value.ToString());
                    zxdList.Add("sbhd", qmhd.Rows[i].Cells[4].Value.ToString());
                    result = cll.jhqmhdXiuGai(zxdList);
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string jinggao = "";
                int flag = 0;//清点成功标志
                int a1 = 0, a2 = 0, a3 = 0, a4 = 0, a5 = 0, a6 = 0, a7 = 0, a8 = 0, a9 = 0, a10 = 0, a11 = 0,a12=0,a13=0,a14=0;
                for (int i = 0; i < qxqd.Rows.Count; i++)
                {
                    for (int j = 0; j < qxqd.Columns.Count; j++)
                    {
                        qxqd.Rows[i].Cells[0].Style.ForeColor = Color.Black;
                        qxqd.Rows[i].Cells[5].Style.ForeColor = Color.Black;
                        qxqd.Rows[i].Cells[10].Style.ForeColor = Color.Black;
                    }
                    if (qxqd.Rows[i].Cells[1].Value == "")
                        a1 = 0;
                    else a1 = Convert.ToInt32(qxqd.Rows[i].Cells[1].Value); ;

                    if (qxqd.Rows[i].Cells[2].Value == "")
                        a2 = 0;
                    else a2 =Convert.ToInt32(qxqd.Rows[i].Cells[2].Value);
                    if (qxqd.Rows[i].Cells[3].Value == "")
                        a3 = 0;
                    else a3 = Convert.ToInt32(qxqd.Rows[i].Cells[3].Value);
                    if (qxqd.Rows[i].Cells[4].Value == "")
                        a4 = 0;
                    else a4 = Convert.ToInt32(qxqd.Rows[i].Cells[4].Value);

                    if (qxqd.Rows[i].Cells[6].Value == "")
                        a6 = 0;
                    else a6 = Convert.ToInt32(qxqd.Rows[i].Cells[6].Value);

                    if (qxqd.Rows[i].Cells[7].Value == "")
                        a7 = 0;
                    else a7 = Convert.ToInt32(qxqd.Rows[i].Cells[7].Value);

                    if (qxqd.Rows[i].Cells[8].Value == "")
                        a8 = 0;
                    else a8 = Convert.ToInt32(qxqd.Rows[i].Cells[8].Value);

                    if (qxqd.Rows[i].Cells[9].Value == "")
                        a9 = 0;
                    else a9 = Convert.ToInt32(qxqd.Rows[i].Cells[9].Value);

                    if (qxqd.Rows[i].Cells[9].Value == "0")
                    {
                        a9 = int.Parse("╲");
                    }

                   
                    if (qxqd.Rows[i].Cells[11].Value == "")
                        a11 = 0;
                    else a11 = Convert.ToInt32(qxqd.Rows[i].Cells[11].Value);

                    if (qxqd.Rows[i].Cells[12].Value == "")
                        a12 = 0;
                    else a12 = Convert.ToInt32(qxqd.Rows[i].Cells[12].Value);

                    if (qxqd.Rows[i].Cells[13].Value == "")
                        a13 = 0;
                    else a13 = Convert.ToInt32(qxqd.Rows[i].Cells[13].Value);

                    if (qxqd.Rows[i].Cells[14].Value == "")
                        a14 = 0;
                    else a14 = Convert.ToInt32(qxqd.Rows[i].Cells[14].Value);
                    if (a1 != a2 || a1 != a3 ||a1 != a4)
                    {
                        flag++;
                        jinggao = jinggao + qxqd.Rows[i].Cells[0].Value.ToString() + "\n";
                        qxqd.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                    }
                    if (a6 != a7 || a6 != a8 || a6 != a9)
                    {
                        flag++;
                        jinggao = jinggao + qxqd.Rows[i].Cells[5].Value.ToString() + "\n";
                        qxqd.Rows[i].Cells[5].Style.ForeColor = Color.Red;
                    }
                    if (a11 != a12 || a11 != a13 || a11 != a14)
                    {
                        flag++;
                        jinggao = jinggao + qxqd.Rows[i].Cells[10].Value.ToString() + "\n";
                        qxqd.Rows[i].Cells[10].Style.ForeColor = Color.Red;
                    }
                }
                if (flag == 0)
                    MessageBox.Show("清点完成！！！！！！");
                else
                    MessageBox.Show(jinggao + "数量不正确");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex + "请填写完整的术前、关前、关后核对信息！");
            }
            jhflqd();
        }
        private void jhflqd()
        {
            try
            {
                string jinggao = "";
                int flag = 0;//清点成功标志
                int a1 = 0, a2 = 0, a3 = 0, a4 = 0, a5 = 0, a6 = 0, a7 = 0, a8 = 0, a9 = 0, a10 = 0, a11 = 0;
                for (int i = 0; i < flqd.Rows.Count; i++)
                {
                    for (int j = 0; j < flqd.Columns.Count; j++)
                    {
                        flqd.Rows[i].Cells[0].Style.ForeColor = Color.Black;
                        flqd.Rows[i].Cells[5].Style.ForeColor = Color.Black;
                       
                    }
                    if (flqd.Rows[i].Cells[1].Value == "")
                        a1 = 0;
                    else a1 = Convert.ToInt32(flqd.Rows[i].Cells[1].Value);

                    if (flqd.Rows[i].Cells[2].Value == "")
                        a2 = 0;
                    else a2 = Convert.ToInt32(flqd.Rows[i].Cells[2].Value);
                    if (flqd.Rows[i].Cells[3].Value == "")
                        a3 = 0;
                    else a3 = Convert.ToInt32(flqd.Rows[i].Cells[3].Value);
                    if (flqd.Rows[i].Cells[4].Value == "")
                        a4 = 0;
                    else a4 = Convert.ToInt32(flqd.Rows[i].Cells[4].Value);


                    if (flqd.Rows[i].Cells[6].Value == "")
                        a6 = 0;
                    else a6 = Convert.ToInt32(flqd.Rows[i].Cells[6].Value);
                    if (flqd.Rows[i].Cells[7].Value == "")
                        a7 = 0;
                    else a7 = Convert.ToInt32(flqd.Rows[i].Cells[7].Value);

                    if (flqd.Rows[i].Cells[8].Value == "")
                        a8 = 0;
                    else a8 = Convert.ToInt32(flqd.Rows[i].Cells[8].Value);

                    if (flqd.Rows[i].Cells[9].Value == "")
                        a9 = 0;
                    else a9 = Convert.ToInt32(flqd.Rows[i].Cells[9].Value);
                    //if (flqd.Rows[i].Cells[9].Value == "")
                    //    a9 = 0;
                    //else a9 = Convert.ToInt32(flqd.Rows[i].Cells[9].Value);
                    //if (flqd.Rows[i].Cells[9].Value == "0")
                    //{
                    //    a9 = int.Parse("╲");
                    //}

                    //if (flqd.Rows[i].Cells[10].Value == "")
                    //    a10 = 0;
                    //else a10 = Convert.ToInt32(flqd.Rows[i].Cells[10].Value);
                    //if (flqd.Rows[i].Cells[11].Value == "")
                    //    a11 = 0;
                    //else a11 = Convert.ToInt32(flqd.Rows[i].Cells[11].Value);
                    if (a1 != a2 || a1 != a3 || a1 != a4)
                    {
                        flag++;
                        jinggao = jinggao + flqd.Rows[i].Cells[0].Value.ToString() + "\n";
                        flqd.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                    }
                    if (a6 != a7 || a6 != a8 || a6 != a9)
                    {
                        flag++;
                        jinggao = jinggao + flqd.Rows[i].Cells[5].Value.ToString() + "\n";
                        flqd.Rows[i].Cells[5].Style.ForeColor = Color.Red;
                    }
                    //if (a9 != a11 || a9 != a10)
                    //{
                    //    flag++;
                    //    jinggao = jinggao + flqd.Rows[i].Cells[8].Value.ToString() + "\n";
                    //    flqd.Rows[i].Cells[8].Style.ForeColor = Color.Red;
                    //}
                }
                if (flag == 0)
                    MessageBox.Show("清点完成！！！！！");
                else
                    MessageBox.Show(jinggao + "数量不正确");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex + "请填写完整的术前、关前、关后核对信息！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.printDocument1.PrintPage += new PrintPageEventHandler(this.MyPrintDocument_PrintPage);
            //将写好的格式给打印预览控件以便预览
            printPreviewDialog1.Document = printDocument1;
            //显示打印预览
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            DialogResult result = printPreviewDialog1.ShowDialog();
            if (result == DialogResult.OK)
                this.printDocument1.Print();
        }
        private void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            #region
            DataTable dt = cll.jiazai(patid);

            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Font ptzt8 = new Font("微软雅黑", 8);//普通字体 
            Font ptzt9 = new Font("微软雅黑", 9);//普通字体  
            Font ptzt10 = new Font("微软雅黑", 10);//普通字体 
            Font ptzt11 = new Font("微软雅黑", 11);//普通字体 
            Font ptzt12 = new Font("宋体", 10);//普通字体 
            Font ptzt08 = new Font("宋体", 8);//普通字体 
            Font ptzt13 = new Font("微软雅黑", 13);//普通字体   
            Font ptzt18 = new Font("微软雅黑", 18);//普通字体   
            int y = 20; int x = 10; int y1 = y + 15;
            e.Graphics.DrawString("金华市中医医院手术护理记录单", ptzt18, Brushes.Black, new Point(x + 220, y + 10));
            y = 30;
            e.Graphics.DrawString("姓名"+ textxm.Text, ptzt11, Brushes.Black, new Point(x + 10, y + 40));
            e.Graphics.DrawLine(Pens.Black, x + 50, y + 60, x + 120, y + 60);
            e.Graphics.DrawString("性别" +textxb.Text, ptzt11, Brushes.Black, new Point(x + 130, y + 40));
            e.Graphics.DrawLine(Pens.Black, x + 160, y + 60, x + 200, y + 60);
            e.Graphics.DrawString("年龄" + textnl.Text, ptzt11, Brushes.Black, new Point(x + 210, y + 40));
            e.Graphics.DrawLine(Pens.Black, x + 240, y + 60, x + 280, y + 60);
            e.Graphics.DrawString("病区" + textbq.Text, ptzt11, Brushes.Black, new Point(x + 290, y + 40));
            e.Graphics.DrawLine(Pens.Black, x + 320, y + 60, x + 390, y + 60);
            e.Graphics.DrawString("床号" + textch.Text, ptzt11, Brushes.Black, new Point(x + 400, y + 40));
            e.Graphics.DrawLine(Pens.Black, x + 430, y + 60, x + 500, y + 60);
            e.Graphics.DrawString("住院病历号" + textzyblh.Text, ptzt11, Brushes.Black, new Point(x + 510, y + 40));
            e.Graphics.DrawLine(Pens.Black, x + 595, y + 60, x + 750, y + 60);
            e.Graphics.DrawString("手术日期" + dateTimessrq.Text, ptzt11, Brushes.Black, new Point(x + 10, y + 70));
            e.Graphics.DrawLine(Pens.Black, x + 75, y + 87, x + 200, y + 87);
            e.Graphics.DrawString("术前诊断"+textsqzd.Text , ptzt11, Brushes.Black, new Point(x + 210, y + 70));
            e.Graphics.DrawLine(Pens.Black, x + 275, y + 87, x + 470, y + 87);
            e.Graphics.DrawString("手术名称" + textssmc.Text, ptzt11, Brushes.Black, new Point(x + 470, y + 70));
            e.Graphics.DrawLine(Pens.Black, x + 535, y + 87, x + 675, y + 87);
            e.Graphics.DrawString("手术间" + textssj.Text, ptzt11, Brushes.Black, new Point(x + 680, y + 70));
            e.Graphics.DrawLine(Pens.Black, x + 730, y + 87, x + 770, y + 87);
            y = 30;



            e.Graphics.DrawLine(Pens.Black, x + 5, y + 107, x + 5, y + 1105);
            e.Graphics.DrawLine(Pens.Black, x + 5, y + 107, x + 780, y + 107);
            e.Graphics.DrawLine(Pens.Black, x + 780, y + 107, x + 780, y + 1105);


            e.Graphics.DrawString("护\n理\n情\n况", ptzt9, Brushes.Black, new Point(x + 10, y + 200));
            e.Graphics.DrawLine(Pens.Black, x + 30, y + 107, x + 30, y + 340);
            e.Graphics.DrawString("术前：入室时间", ptzt9, Brushes.Black, new Point(x + 35, y + 115));
            e.Graphics.DrawLine(Pens.Black, x + 125, y + 130, x + 222, y + 130);
            x = 45;
            e.Graphics.DrawString("病人核对：", ptzt9, Brushes.Black, new Point(x + 200, y + 115));
            e.Graphics.DrawString("有", ptzt9, Brushes.Black, new Point(x + 260, y + 115));
            e.Graphics.DrawRectangle(Pens.Black, x + 275, y + 120, 10, 10);
            e.Graphics.DrawString("无", ptzt9, Brushes.Black, new Point(x + 290, y + 115));
            e.Graphics.DrawRectangle(Pens.Black, x + 305, y + 120, 10, 10);
            x = 65;
            e.Graphics.DrawString("手术部位核对：", ptzt9, Brushes.Black, new Point(x + 325, y + 115));
            e.Graphics.DrawString("有", ptzt9, Brushes.Black, new Point(x + 415, y + 115));
            e.Graphics.DrawRectangle(Pens.Black, x + 430, y + 120, 10, 10);
            e.Graphics.DrawString("无", ptzt9, Brushes.Black, new Point(x + 440, y + 115));
            e.Graphics.DrawRectangle(Pens.Black, x + 455, y + 120, 10, 10);
            x = 95;
            e.Graphics.DrawString("神志：", ptzt9, Brushes.Black, new Point(x + 455, y + 115));
            e.Graphics.DrawString("清醒", ptzt9, Brushes.Black, new Point(x + 500, y + 115));
            e.Graphics.DrawRectangle(Pens.Black, x + 530, y + 120, 10, 10);
            e.Graphics.DrawString("模糊", ptzt9, Brushes.Black, new Point(x + 545, y + 115));
            e.Graphics.DrawRectangle(Pens.Black, x + 575, y + 120, 10, 10);
            e.Graphics.DrawString("意识不清", ptzt9, Brushes.Black, new Point(x + 590, y + 115));
            e.Graphics.DrawRectangle(Pens.Black, x + 642, y + 120, 10, 10);
            x = 10; y = 20;
            e.Graphics.DrawString("深静脉穿刺：", ptzt9, Brushes.Black, new Point(x + 70, y + 145));
            e.Graphics.DrawString("有", ptzt9, Brushes.Black, new Point(x + 145, y + 145));
            e.Graphics.DrawRectangle(Pens.Black, x + 160, y + 150, 10, 10);
            e.Graphics.DrawString("无", ptzt9, Brushes.Black, new Point(x + 175, y + 145));
            e.Graphics.DrawRectangle(Pens.Black, x + 190, y + 150, 10, 10);
            e.Graphics.DrawString("静脉输液：", ptzt9, Brushes.Black, new Point(x + 220, y + 145));
            e.Graphics.DrawString("有", ptzt9, Brushes.Black, new Point(x + 280, y + 145));
            e.Graphics.DrawRectangle(Pens.Black, x + 295, y + 150, 10, 10);
            e.Graphics.DrawString("无", ptzt9, Brushes.Black, new Point(x + 310, y + 145));
            e.Graphics.DrawRectangle(Pens.Black, x + 325, y + 150, 10, 10);
            e.Graphics.DrawString("胃管：", ptzt9, Brushes.Black, new Point(x + 355, y + 145));
            e.Graphics.DrawString("有", ptzt9, Brushes.Black, new Point(x + 390, y + 145));
            e.Graphics.DrawRectangle(Pens.Black, x + 405, y + 150, 10, 10);
            e.Graphics.DrawString("无", ptzt9, Brushes.Black, new Point(x + 420, y + 145));
            e.Graphics.DrawRectangle(Pens.Black, x + 435, y + 150, 10, 10);
            e.Graphics.DrawString("导尿管：", ptzt9, Brushes.Black, new Point(x + 460, y + 145));
            e.Graphics.DrawString("有", ptzt9, Brushes.Black, new Point(x + 510, y + 145));
            e.Graphics.DrawRectangle(Pens.Black, x + 525, y + 150, 10, 10);
            e.Graphics.DrawString("无", ptzt9, Brushes.Black, new Point(x + 540, y + 145));
            e.Graphics.DrawRectangle(Pens.Black, x + 555, y + 150, 10, 10);
            e.Graphics.DrawString("术前压疮风险评估：            分", ptzt9, Brushes.Black, new Point(x + 580, y + 145));
            e.Graphics.DrawLine(Pens.Black, x + 697, y + 162, x + 730, y + 162);
            y = 10;
            e.Graphics.DrawString("皮肤情况：", ptzt9, Brushes.Black, new Point(x + 70, y + 175));
            e.Graphics.DrawLine(Pens.Black, x + 140, y + 192, x + 350, y + 192);
            e.Graphics.DrawString("药物过敏史：", ptzt9, Brushes.Black, new Point(x + 400, y + 175));
            e.Graphics.DrawLine(Pens.Black, x + 485, y + 192, x + 700, y + 192);
            e.Graphics.DrawString("术中：体位", ptzt9, Brushes.Black, new Point(x + 35, y + 195));
            e.Graphics.DrawLine(Pens.Black, x + 105, y + 209, x + 170, y + 209);
            e.Graphics.DrawString("止血带：", ptzt9, Brushes.Black, new Point(x + 180, y + 195));
            e.Graphics.DrawString("有", ptzt9, Brushes.Black, new Point(x + 235, y + 195));
            e.Graphics.DrawRectangle(Pens.Black, x + 250, y + 200, 10, 10);
            e.Graphics.DrawString("无", ptzt9, Brushes.Black, new Point(x + 270, y + 195));
            e.Graphics.DrawRectangle(Pens.Black, x + 285, y + 200, 10, 10);
            e.Graphics.DrawString("（压力", ptzt9, Brushes.Black, new Point(x + 295, y + 195));
            e.Graphics.DrawLine(Pens.Black, x + 335, y + 209, x + 380, y + 209);
            e.Graphics.DrawString("时间                                                       ）", ptzt9, Brushes.Black, new Point(x + 385, y + 195));
            e.Graphics.DrawLine(Pens.Black, x + 415, y + 209, x + 600, y + 209);
            e.Graphics.DrawString("落实压疮风险护理措施：", ptzt9, Brushes.Black, new Point(x + 70, y + 215));
            x = x - 30;
            e.Graphics.DrawString("有", ptzt9, Brushes.Black, new Point(x + 235, y + 215));
            e.Graphics.DrawRectangle(Pens.Black, x + 250, y + 220, 10, 10);
            e.Graphics.DrawString("无", ptzt9, Brushes.Black, new Point(x + 300, y + 215));
            e.Graphics.DrawRectangle(Pens.Black, x + 315, y + 220, 10, 10);
            e.Graphics.DrawString("不适用", ptzt9, Brushes.Black, new Point(x + 365, y + 215));
            e.Graphics.DrawRectangle(Pens.Black, x + 410, y + 220, 10, 10);
            x = 10;

            e.Graphics.DrawString("减少摩擦力和剪切力", ptzt9, Brushes.Black, new Point(x + 70, y + 235));
            e.Graphics.DrawRectangle(Pens.Black, x + 185, y + 240, 10, 10);
            e.Graphics.DrawString("正确使用预防压疮用具", ptzt9, Brushes.Black, new Point(x + 255, y + 235));
            e.Graphics.DrawRectangle(Pens.Black, x + 385, y + 240, 10, 10);
            e.Graphics.DrawString("其他", ptzt9, Brushes.Black, new Point(x + 455, y + 235));
            e.Graphics.DrawRectangle(Pens.Black, x + 485, y + 240, 10, 10);
            e.Graphics.DrawString("植入物名称及数量", ptzt9, Brushes.Black, new Point(x + 70, y + 255));
            e.Graphics.DrawLine(Pens.Black, x + 175, y + 270, x + 450, y + 270);
            e.Graphics.DrawString("植入物标识：粘贴于背面", ptzt9, Brushes.Black, new Point(x + 460, y + 255));
            e.Graphics.DrawRectangle(Pens.Black, x + 598, y + 260, 10, 10);
            e.Graphics.DrawString("医生处理", ptzt9, Brushes.Black, new Point(x + 635, y + 255));
            e.Graphics.DrawRectangle(Pens.Black, x + 695, y + 260, 10, 10);
            e.Graphics.DrawString("标本名称及数量", ptzt9, Brushes.Black, new Point(x + 70, y + 275));
            e.Graphics.DrawLine(Pens.Black, x + 165, y + 290, x + 700, y + 290);
            e.Graphics.DrawString("术毕：意识情况", ptzt9, Brushes.Black, new Point(x + 35, y + 295));
            e.Graphics.DrawLine(Pens.Black, x + 130, y + 310, x + 220, y + 310);
            e.Graphics.DrawString("皮肤情况", ptzt9, Brushes.Black, new Point(x + 230, y + 295));
            e.Graphics.DrawLine(Pens.Black, x + 290, y + 310, x + 340, y + 310);
            e.Graphics.DrawString("术后送回：病房", ptzt9, Brushes.Black, new Point(x + 350, y + 295));
            e.Graphics.DrawRectangle(Pens.Black, x + 440, y + 300, 10, 10);
            e.Graphics.DrawString("ICU", ptzt9, Brushes.Black, new Point(x + 466, y + 295));
            e.Graphics.DrawRectangle(Pens.Black, x + 490, y + 300, 10, 10);
            e.Graphics.DrawString("麻醉恢复室", ptzt9, Brushes.Black, new Point(x + 505, y + 295));
            e.Graphics.DrawRectangle(Pens.Black, x + 568, y + 300, 10, 10);
            e.Graphics.DrawString("其他", ptzt9, Brushes.Black, new Point(x + 595, y + 295));
            e.Graphics.DrawRectangle(Pens.Black, x + 625, y + 300, 10, 10);
            e.Graphics.DrawString("离室时间", ptzt9, Brushes.Black, new Point(x + 640, y + 295));
            e.Graphics.DrawLine(Pens.Black, x + 700, y + 310, x + 770, y + 310);
            e.Graphics.DrawString("其他", ptzt9, Brushes.Black, new Point(x + 70, y + 315));
            e.Graphics.DrawLine(Pens.Black, x + 90, y + 330, x + 500, y + 330);
            e.Graphics.DrawString("引流管", ptzt9, Brushes.Black, new Point(x + 510, y + 315));
            e.Graphics.DrawLine(Pens.Black, x + 550, y + 330, x + 600, y + 330);
            e.Graphics.DrawString("根（部位                 ）", ptzt9, Brushes.Black, new Point(x + 610, y + 315));
            e.Graphics.DrawLine(Pens.Black, x + 665, y + 330, x + 720, y + 330);
            e.Graphics.DrawString("主要无菌包", ptzt9, Brushes.Black, new Point(x + 70, y + 335));
            e.Graphics.DrawLine(Pens.Black, x + 140, y + 350, x + 470, y + 350);
            e.Graphics.DrawString("主要无菌包检测：合格", ptzt9, Brushes.Black, new Point(x + 480, y + 335));
            e.Graphics.DrawRectangle(Pens.Black, x + 605, y + 340, 10, 10);
            e.Graphics.DrawString("签名:", ptzt9, Brushes.Black, new Point(x + 635, y + 335));
            e.Graphics.DrawLine(Pens.Black, x + 5, y + 360, x + 780, y + 360);
            e.Graphics.DrawString("品名", ptzt9, Brushes.Black, new Point(x + 35, y + 365));
            e.Graphics.DrawString("术前清点", ptzt9, Brushes.Black, new Point(x + 80, y + 363));
            e.Graphics.DrawString("关前核对", ptzt9, Brushes.Black, new Point(x + 160, y + 363));
            e.Graphics.DrawString("关后核对", ptzt9, Brushes.Black, new Point(x + 240, y + 363));
            e.Graphics.DrawString("术毕核对", ptzt9, Brushes.Black, new Point(x + 320, y + 363));
            e.Graphics.DrawString("品名", ptzt9, Brushes.Black, new Point(x + 400, y + 363));
            e.Graphics.DrawString("术前清点", ptzt9, Brushes.Black, new Point(x + 480, y + 363));
            e.Graphics.DrawString("关前核对", ptzt9, Brushes.Black, new Point(x + 560, y + 363));
            e.Graphics.DrawString("关后核对", ptzt9, Brushes.Black, new Point(x + 640, y + 363));
            e.Graphics.DrawString("术毕核对", ptzt9, Brushes.Black, new Point(x + 720, y + 363));
            e.Graphics.DrawLine(Pens.Black, x + 70, y + 360, x + 70, y + 480);
            e.Graphics.DrawLine(Pens.Black, x + 152, y + 360, x + 152, y + 480);
            e.Graphics.DrawLine(Pens.Black, x + 232, y + 360, x + 232, y + 480);
            e.Graphics.DrawLine(Pens.Black, x + 312, y + 360, x + 312, y + 480);
            e.Graphics.DrawLine(Pens.Black, x + 392, y + 360, x + 392, y + 480);
            e.Graphics.DrawLine(Pens.Black, x + 472, y + 360, x + 472, y + 480);
            e.Graphics.DrawLine(Pens.Black, x + 552, y + 360, x + 552, y + 480);
            e.Graphics.DrawLine(Pens.Black, x + 632, y + 360, x + 632, y + 480);
            e.Graphics.DrawLine(Pens.Black, x + 712, y + 360, x + 712, y + 480);
            
            e.Graphics.DrawLine(Pens.Black, x + 5, y + 380, x + 780, y + 380);
            e.Graphics.DrawLine(Pens.Black, x + 5, y + 400, x + 780, y + 400);
            e.Graphics.DrawLine(Pens.Black, x + 5, y + 420, x + 780, y + 420);
            e.Graphics.DrawLine(Pens.Black, x + 5, y + 440, x + 780, y + 440);
            e.Graphics.DrawLine(Pens.Black, x + 5, y + 460, x + 780, y + 460);
            e.Graphics.DrawLine(Pens.Black, x + 5, y + 480, x + 780, y + 480);
            x = x - 25;
            e.Graphics.DrawString("器械名称", ptzt9, Brushes.Black, new Point(x + 35, y +490));
            e.Graphics.DrawString("术前\n清点", ptzt9, Brushes.Black, new Point(x + 115, y + 490));
            
            e.Graphics.DrawString("关前\n核对", ptzt9, Brushes.Black, new Point(x + 175, y + 490));
            e.Graphics.DrawString("关后\n核对", ptzt9, Brushes.Black, new Point(x + 215, y + 490));
            e.Graphics.DrawString("术毕\n核对", ptzt9, Brushes.Black, new Point(x + 255, y + 490));
            e.Graphics.DrawString("器械名称", ptzt9, Brushes.Black, new Point(x + 295, y + 490));
            
            e.Graphics.DrawString("术前\n清点", ptzt9, Brushes.Black, new Point(x + 375, y + 490));
            
            e.Graphics.DrawString("关前\n核对", ptzt9, Brushes.Black, new Point(x + 435, y + 490));
            e.Graphics.DrawString("关后\n核对", ptzt9, Brushes.Black, new Point(x + 475, y + 490));
            e.Graphics.DrawString("术毕\n核对", ptzt9, Brushes.Black, new Point(x + 515, y + 490));
            e.Graphics.DrawString("器械名称", ptzt9, Brushes.Black, new Point(x + 555, y + 490));
           
            e.Graphics.DrawString("术前\n清点", ptzt9, Brushes.Black, new Point(x + 645, y + 490));
           
            e.Graphics.DrawString("关前\n核对", ptzt9, Brushes.Black, new Point(x + 705, y + 490));
            e.Graphics.DrawString("关后\n核对", ptzt9, Brushes.Black, new Point(x + 740, y + 490));
            e.Graphics.DrawString("术毕\n核对", ptzt9, Brushes.Black, new Point(x + 777, y + 490));
            x = x + 25;
            e.Graphics.DrawLine(Pens.Black, x + 5, y + 480, x + 780, y + 480);
            e.Graphics.DrawLine(Pens.Black, x + 5, y + 535, x + 780, y + 535);
            for (int i = 0; i < 20; i++)
            {
                e.Graphics.DrawLine(Pens.Black, x + 5, y + 560, x + 780, y + 560);
                y = y + 25;
            }
            y = 10;
            
            e.Graphics.DrawLine(Pens.Black, x + 70, y + 480, x + 70, y + 1035);
            e.Graphics.DrawLine(Pens.Black, x + 145, y + 480, x + 145, y + 1035);
            e.Graphics.DrawLine(Pens.Black, x + 185, y + 480, x + 185, y + 1035);
            e.Graphics.DrawLine(Pens.Black, x + 225, y + 480, x + 225, y + 1035);
            e.Graphics.DrawLine(Pens.Black, x + 265, y + 480, x + 265, y + 1035);
            e.Graphics.DrawLine(Pens.Black, x + 330, y + 480, x + 330, y + 1035);
            e.Graphics.DrawLine(Pens.Black, x + 405, y + 480, x + 405, y + 1035);
            e.Graphics.DrawLine(Pens.Black, x + 445, y + 480, x + 445, y + 1035);
            e.Graphics.DrawLine(Pens.Black, x + 485, y + 480, x + 485, y + 1035);
            e.Graphics.DrawLine(Pens.Black, x + 525, y + 480, x + 525, y + 1035);
            e.Graphics.DrawLine(Pens.Black, x + 590, y + 480, x + 590, y + 1035);
            e.Graphics.DrawLine(Pens.Black, x + 675, y + 480, x + 675, y + 1035);
            e.Graphics.DrawLine(Pens.Black, x + 710, y + 480, x + 710, y + 1035);
            e.Graphics.DrawLine(Pens.Black, x + 745, y + 480, x + 745, y + 1035);

            e.Graphics.DrawLine(Pens.Black, x + 5, y + 1065, x + 780, y + 1065);
            e.Graphics.DrawLine(Pens.Black, x + 5, y + 1095, x + 780, y + 1095);
            e.Graphics.DrawLine(Pens.Black, x + 5, y + 1125, x + 780, y + 1125);
            //e.Graphics.DrawLine(Pens.Black, x + 745, y + 480, x + 745, y + 1035);
            e.Graphics.DrawLine(Pens.Black, x + 145, y + 1035, x + 145, y + 1125);
            e.Graphics.DrawLine(Pens.Black, x + 315, y + 1035, x + 315, y + 1125);
            e.Graphics.DrawLine(Pens.Black, x + 465, y + 1035, x + 465, y + 1125);
            e.Graphics.DrawLine(Pens.Black, x + 630, y + 1035, x + 630, y + 1125);

            e.Graphics.DrawString("责任人", ptzt9, Brushes.Black, new Point(x + 60, y + 1040));
            e.Graphics.DrawString("术前清点", ptzt9, Brushes.Black, new Point(x + 200, y + 1040));
            e.Graphics.DrawString("关前核对", ptzt9, Brushes.Black, new Point(x + 360, y + 1040));
            e.Graphics.DrawString("关后核对", ptzt9, Brushes.Black, new Point(x + 520, y + 1040));
            e.Graphics.DrawString("术毕核对", ptzt9, Brushes.Black, new Point(x + 680, y + 1040));
            #endregion
            //数据
            #region

            y = 20;
            x = 70;
            for (int a = 0; a < dt.Rows.Count; a++)  //  a行 b列
            {
                e.Graphics.DrawString(dt.Rows[0]["rssj"].ToString(), ptzt9, Brushes.Black, x + 70, y + 125);//入室时间
                e.Graphics.DrawString(dt.Rows[0]["sqycfxpg"].ToString(), ptzt9, Brushes.Black, x + 635, y + 145);//术前压疮风险评估
                e.Graphics.DrawString(dt.Rows[0]["pfqk"].ToString(), ptzt9, Brushes.Black, x + 80, y + 165);//皮肤情况
                e.Graphics.DrawString(dt.Rows[0]["ywgms"].ToString(), ptzt9, Brushes.Black, x + 425, y + 165);//药物过敏史
                e.Graphics.DrawString(dt.Rows[0]["tw"].ToString(), ptzt9, Brushes.Black, x + 45, y + 185);//体位
                e.Graphics.DrawString(dt.Rows[0]["yl"].ToString(), ptzt9, Brushes.Black, x + 275, y + 185);//压力
                e.Graphics.DrawString(dt.Rows[0]["sj"].ToString(), ptzt9, Brushes.Black, x + 355, y + 185);//时间
                e.Graphics.DrawString(dt.Rows[0]["zrwpmc"].ToString(), ptzt9, Brushes.Black, x + 115, y + 245);//植入物名称及数量
                e.Graphics.DrawString(dt.Rows[0]["bbmc"].ToString(), ptzt9, Brushes.Black, x + 105, y + 265);//标本名称及数量
                e.Graphics.DrawString(dt.Rows[0]["ysqk"].ToString(), ptzt9, Brushes.Black, x + 69, y + 285);//意识情况
                e.Graphics.DrawString(dt.Rows[0]["sbpfqk"].ToString(), ptzt9, Brushes.Black, x + 225, y + 285);//皮肤情况
                e.Graphics.DrawString(dt.Rows[0]["lssj"].ToString(), ptzt9, Brushes.Black, x + 640, y + 285);//离室时间
                e.Graphics.DrawString(dt.Rows[0]["sbqt"].ToString(), ptzt9, Brushes.Black, x + 40, y + 305);//其他
                e.Graphics.DrawString(dt.Rows[0]["ylg"].ToString(), ptzt9, Brushes.Black, x + 490, y + 305);//引流管
                e.Graphics.DrawString(dt.Rows[0]["bw"].ToString(), ptzt9, Brushes.Black, x + 608, y + 305);//部位
                e.Graphics.DrawString(dt.Rows[0]["zywjb"].ToString(), ptzt9, Brushes.Black, x + 80, y + 325);//主要无菌包
                string cd = dt.Rows[0]["fxk"].ToString();
                for (int i = 0; i < cd.Length; i++)
                {
                    string b = cd.Substring(i, 1);
                    string sj = b;
                    y = 35; x = 45;
                    if (sj == "1")
                    {                      
                        e.Graphics.DrawLine(pblue2, x + 275, y + 120, x + 280, y + 125);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 280, y + 125, x + 285, y + 115);

                    }
                        
                    else if (sj == "2")
                    {
                        e.Graphics.DrawLine(pblue2, x + 305, y + 120, x + 310, y + 125);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 310, y + 125, x + 315, y + 115);
                    }
                    else if (sj == "3")
                    {
                        e.Graphics.DrawLine(pblue2, x + 450, y + 120, x + 455, y + 125);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 455, y + 125, x + 460, y + 115);
                    }
                    else if (sj == "4")
                    {
                        
                        e.Graphics.DrawLine(pblue2, x + 475, y + 120, x + 480, y + 125);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 480, y + 125, x + 485, y + 115);
                    }
                    else if (sj == "5")
                    {

                        e.Graphics.DrawLine(pblue2, x + 580, y + 120, x + 585, y + 125);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 585, y + 125, x + 590, y + 115);
                    }
                    else if (sj == "6")
                    {

                        e.Graphics.DrawLine(pblue2, x + 625, y + 120, x + 630, y + 125);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 630, y + 125, x + 635, y + 115);
                    }
                    else if (sj == "7")
                    {

                        e.Graphics.DrawLine(pblue2, x + 692, y + 120, x + 697, y + 125);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 697, y + 125, x + 702, y + 115);
                    }
                    else if (sj == "8")
                    {
                        
                        e.Graphics.DrawLine(pblue2, x + 125, y + 140, x + 130, y + 145);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 130, y + 145, x + 135, y + 135);
                    }
                    else if (sj == "9")
                    {

                        e.Graphics.DrawLine(pblue2, x + 155, y + 140, x + 160, y + 145);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 160, y + 145, x + 165, y + 135);
                    }
                    else if (sj == "a")
                    {

                        e.Graphics.DrawLine(pblue2, x + 260, y + 140, x + 265, y + 145);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 265, y + 145, x + 270, y + 135);
                    }
                    else if (sj == "b")
                    {

                        e.Graphics.DrawLine(pblue2, x + 290, y + 140, x + 295, y + 145);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 295, y + 145, x + 300, y + 135);
                    }
                    else if (sj == "c")
                    {

                        e.Graphics.DrawLine(pblue2, x + 370, y + 140, x + 375, y + 145);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 375, y + 145, x + 380, y + 135);
                    }
                    else if (sj == "d")
                    {

                        e.Graphics.DrawLine(pblue2, x + 400, y + 140, x + 405, y + 145);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 405, y + 145, x + 410, y + 135);
                    }
                    else if (sj == "e")
                    {

                        e.Graphics.DrawLine(pblue2, x + 490, y + 140, x + 495, y + 145);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 495, y + 145, x + 500, y + 135);
                    }
                    else if (sj == "f")
                    {
                        e.Graphics.DrawLine(pblue2, x + 520, y + 140, x + 525, y + 145);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 525, y + 145, x + 530, y + 135);
                    }
                    else if (sj == "j")
                    {

                        e.Graphics.DrawLine(pblue2, x + 215, y + 180, x + 220, y + 185);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 220, y + 185, x + 225, y + 175);
                    }
                    else if (sj == "h")
                    {

                        e.Graphics.DrawLine(pblue2, x + 250, y + 180, x + 255, y + 185);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 255, y + 185, x + 260, y + 175);
                    }
                    else if (sj == "i")
                    {

                        e.Graphics.DrawLine(pblue2, x + 185, y + 200, x + 190, y + 205);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 190, y + 205, x + 195, y + 195);
                    }
                    else if (sj == "g")
                    {

                        e.Graphics.DrawLine(pblue2, x + 250, y + 200, x + 255, y + 205);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 255, y + 205, x + 260, y + 195);
                    }
                    else if (sj == "k")
                    {

                        e.Graphics.DrawLine(pblue2, x + 345, y + 200, x + 350, y + 205);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 350, y + 205, x + 355, y + 195);
                    }


                    else if (sj == "r")
                    {

                        e.Graphics.DrawLine(pblue2, x + 150, y + 220, x + 155, y + 225);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 155, y + 225, x + 160, y + 215);
                    }
                    else if (sj == "m")
                    {

                        e.Graphics.DrawLine(pblue2, x + 350, y + 220, x + 355, y + 225);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 355, y + 225, x + 360, y + 215);
                    }
                    else if (sj == "n")
                    {

                        e.Graphics.DrawLine(pblue2, x + 450, y + 220, x + 455, y + 225);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 455, y + 225, x + 460, y + 215);
                    }
                    else if (sj == "o")
                    {

                        e.Graphics.DrawLine(pblue2, x + 563, y + 240, x + 568, y + 245);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 568, y + 245, x + 573, y + 235);
                    }
                    else if (sj == "p")
                    {

                        e.Graphics.DrawLine(pblue2, x + 660, y + 240, x + 665, y + 245);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 665, y + 245, x + 670, y + 235);
                    }
                    else if (sj == "q")
                    {

                        e.Graphics.DrawLine(pblue2, x + 405, y + 280, x + 410, y + 285);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 410, y + 285, x + 415, y + 275);
                    }
                    else if (sj == "l")
                    {

                        e.Graphics.DrawLine(pblue2, x + 455, y + 280, x + 460, y + 285);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 460, y + 285, x + 465, y + 275);
                    }
                    else if (sj == "s")
                    {

                        e.Graphics.DrawLine(pblue2, x + 533, y + 280, x + 538, y + 285);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 538, y + 285, x + 543, y + 275);
                    }
                    else if (sj == "t")
                    {

                        e.Graphics.DrawLine(pblue2, x + 590, y + 280, x + 595, y + 285);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 595, y + 285, x + 600, y + 275);
                    }
                    else if (sj == "u")
                    {

                        e.Graphics.DrawLine(pblue2, x + 570, y + 320, x + 575, y + 325);//第一个X调整下点，第二个X调整上点，第一个Y调整下点，第二个Y调整上点
                        e.Graphics.DrawLine(pblue2, x + 575, y + 325, x + 580, y + 315);
                    }
                }

                int flag = 0;
                int row = 0;
                int count = Convert.ToInt32(flqd.Rows.Count);
                y = 10; x = 10;
                for (int A = row; A < count; A++)
                {
                    flag++;
                    for (int j = 0; j < 9; j++)
                    {
                        if (flqd.Rows[A].Cells[j].Value == null)
                            flqd.Rows[A].Cells[j].Value = "";

                    }
                    e.Graphics.DrawString(flqd.Rows[A].Cells[0].Value.ToString(), ptzt9, Brushes.Black, x + 5, y + 380);
                    e.Graphics.DrawString(flqd.Rows[A].Cells[1].Value.ToString(), ptzt9, Brushes.Black, x + 100, y + 380);
                    e.Graphics.DrawString(flqd.Rows[A].Cells[2].Value.ToString(), ptzt9, Brushes.Black, x + 182, y + 380);
                    e.Graphics.DrawString(flqd.Rows[A].Cells[3].Value.ToString(), ptzt9, Brushes.Black, x + 262, y + 380);
                    e.Graphics.DrawString(flqd.Rows[A].Cells[4].Value.ToString(), ptzt9, Brushes.Black, x + 342, y + 380);
                    e.Graphics.DrawString(flqd.Rows[A].Cells[5].Value.ToString(), ptzt9, Brushes.Black, x + 392, y + 380);
                    e.Graphics.DrawString(flqd.Rows[A].Cells[6].Value.ToString(), ptzt9, Brushes.Black, x + 502, y + 380);
                    e.Graphics.DrawString(flqd.Rows[A].Cells[7].Value.ToString(), ptzt9, Brushes.Black, x + 582, y + 380);
                    e.Graphics.DrawString(flqd.Rows[A].Cells[8].Value.ToString(), ptzt9, Brushes.Black, x + 682, y + 380);
                    e.Graphics.DrawString(flqd.Rows[A].Cells[9].Value.ToString(), ptzt9, Brushes.Black, x + 742, y + 380);
                    y = y + 20;
                }
                count = Convert.ToInt32(qxqd.Rows.Count);
                y = 65; x = 10;
                for (int A = row; A < count; A++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (qxqd.Rows[A].Cells[j].Value == null)
                            qxqd.Rows[A].Cells[j].Value = "";

                    }
                    e.Graphics.DrawString(qxqd.Rows[A].Cells[0].Value.ToString(), ptzt9, Brushes.Black, x + 5, y + 480);
                    e.Graphics.DrawString(qxqd.Rows[A].Cells[1].Value.ToString(), ptzt9, Brushes.Black, x + 100, y + 480);
                    e.Graphics.DrawString(qxqd.Rows[A].Cells[2].Value.ToString(), ptzt9, Brushes.Black, x + 145, y + 480);
                    e.Graphics.DrawString(qxqd.Rows[A].Cells[3].Value.ToString(), ptzt9, Brushes.Black, x + 185, y + 480);
                    e.Graphics.DrawString(qxqd.Rows[A].Cells[4].Value.ToString(), ptzt9, Brushes.Black, x + 225, y + 480);
                    e.Graphics.DrawString(qxqd.Rows[A].Cells[5].Value.ToString(), ptzt9, Brushes.Black, x + 265, y + 480);
                    e.Graphics.DrawString(qxqd.Rows[A].Cells[6].Value.ToString(), ptzt9, Brushes.Black, x + 355, y + 480);
                    e.Graphics.DrawString(qxqd.Rows[A].Cells[7].Value.ToString(), ptzt9, Brushes.Black, x + 405, y + 480);
                    e.Graphics.DrawString(qxqd.Rows[A].Cells[8].Value.ToString(), ptzt9, Brushes.Black, x + 445, y + 480);
                    e.Graphics.DrawString(qxqd.Rows[A].Cells[9].Value.ToString(), ptzt9, Brushes.Black, x + 485, y + 480);
                    e.Graphics.DrawString(qxqd.Rows[A].Cells[10].Value.ToString(), ptzt9, Brushes.Black, x + 525, y + 480);
                    e.Graphics.DrawString(qxqd.Rows[A].Cells[11].Value.ToString(), ptzt9, Brushes.Black, x + 625, y + 480);
                    e.Graphics.DrawString(qxqd.Rows[A].Cells[12].Value.ToString(), ptzt9, Brushes.Black, x + 675, y + 480);
                    e.Graphics.DrawString(qxqd.Rows[A].Cells[13].Value.ToString(), ptzt9, Brushes.Black, x + 710, y + 480);
                    e.Graphics.DrawString(qxqd.Rows[A].Cells[14].Value.ToString(), ptzt9, Brushes.Black, x + 745, y + 480);
                    y = y + 25;
                }
                count = Convert.ToInt32(qmhd.Rows.Count);
                y = 10; x = 10;
                for (int A = row; A < count; A++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (qmhd.Rows[A].Cells[j].Value == null)
                            qmhd.Rows[A].Cells[j].Value = "";

                    }
                    //x:145,315,465,630 y:1065
                    e.Graphics.DrawString(qmhd.Rows[A].Cells[0].Value.ToString(), ptzt9, Brushes.Black, x + 5, y + 1065);
                    e.Graphics.DrawString(qmhd.Rows[A].Cells[1].Value.ToString(), ptzt9, Brushes.Black, x + 205, y + 1065);
                    e.Graphics.DrawString(qmhd.Rows[A].Cells[2].Value.ToString(), ptzt9, Brushes.Black, x + 375, y + 1065);
                    e.Graphics.DrawString(qmhd.Rows[A].Cells[3].Value.ToString(), ptzt9, Brushes.Black, x + 525, y + 1065);
                    e.Graphics.DrawString(qmhd.Rows[A].Cells[3].Value.ToString(), ptzt9, Brushes.Black, x + 690, y + 1065);
                    y = y + 30;
                }
            }
            #endregion
        }
    }
}
