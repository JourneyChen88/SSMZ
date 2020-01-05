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
using adims_MODEL;
using System.Drawing.Printing;

namespace main
{
    public partial class SSsunshangpgb : Form
    {
        adims_BLL.YXL_BLL bll = new adims_BLL.YXL_BLL();
        adims_DAL.YXL_DAL dal = new adims_DAL.YXL_DAL();
        AdimsProvider dall = new AdimsProvider();
        string patid;
        int a = 0;
        int b = 0;
        int c = 0;
        int d = 0;
        int c1 = 0;
        int f = 0;
        int g = 0;
        int h = 0;
        int i = 0;
        int j = 0;
        int k = 0;

       
        //int h = 0;
        public SSsunshangpgb(string id)
        {
            patid = id;
            InitializeComponent();
        }

        private void SSsunshangpgb_Load(object sender, EventArgs e)
        {
            this.dateTimeP.Format = DateTimePickerFormat.Custom;
            this.dateTimeP.CustomFormat = "yyyy-MM-dd HH:mm";
            DataTable dtt = dall.GetAll_hushi();
            comboBpghs.Items.Clear();
            foreach (DataRow dr in dtt.Rows)
            {
                comboBpghs.Items.Add(dr["user_name"].ToString());
            }
            DataTable dt = bll.SelectPatInfo111(patid);
            if (dt.Rows.Count > 0)
            {

                txtbKeShi.Text = dt.Rows[0]["Patdpm"].ToString();
                txtbChuangHao.Text = dt.Rows[0]["patbedno"].ToString();
                txtbZYH.Text = dt.Rows[0]["PatID"].ToString();
                txtbAge.Text = dt.Rows[0]["Patage"].ToString();
                int age=  int.Parse(dt.Rows[0]["Patage"].ToString());
                if (age >= 14 && age <= 49)
                {
                    check1416.Checked = true;
                    check5064.Checked = false;
                    check6574.Checked = false;
                    check7480.Checked = false;
                    check80jia.Checked = false;
                 }
                if (age >= 50 && age <= 64)
                {
                    check1416.Checked = false;
                    check5064.Checked = true;
                    check6574.Checked = false;
                    check7480.Checked = false;
                    check80jia.Checked = false;
                }
                if (age >= 65 && age <= 74)
                {
                    check1416.Checked = false;
                    check5064.Checked = false;
                    check6574.Checked = true;
                    check7480.Checked = false;
                    check80jia.Checked = false;
                }
                if (age >= 75 && age <= 80)
                {
                    check1416.Checked = false;
                    check5064.Checked = false;
                    check6574.Checked = false;
                    check7480.Checked = true;
                    check80jia.Checked = false;
                }
                if (age > 80 )
                {
                    check1416.Checked = false;
                    check5064.Checked = false;
                    check6574.Checked = false;
                    check7480.Checked = false;
                    check80jia.Checked = true;
                }
                txtbSex.Text = dt.Rows[0]["Patsex"].ToString();
                if (dt.Rows[0]["Patsex"].ToString() == "男")
                {
                    nan.Checked = true;
                    checnv.Checked = false;
                }
                else
                {
                    checnv.Checked = true;
                    nan.Checked = false;
                }
                txtbHZName.Text = dt.Rows[0]["Patname"].ToString();
               // txtbMZFS.Text = dt.Rows[0]["Amethod"].ToString();
                txtbMZSSMC.Text = dt.Rows[0]["Oname"].ToString();
            }
            dt = dal.GetPenpleInfoylsspgb(patid);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                //SQF.Add("shoushuriqi", Convert.ToDateTime(dateTimeP.Value.ToString()).ToString("yyyy-MM-dd mm:ss"));
                
                //SQF.Add("ssmingcheng", txtbMZSSMC.Text);
                txtbMZSSMC.Text = dr["ssmingcheng"].ToString();
                dateTimeP.Value = Convert.ToDateTime(dr["shoushuriqi"]);
               
                
                if (Convert.ToString(dr["txtzsg"]).Contains("1")) cheboxzd.Checked = true;
                if (Convert.ToString(dr["txtzsg"]).Contains("2")) chaoyuezd.Checked = true;
                if (Convert.ToString(dr["txtzsg"]).Contains("3")) feipang.Checked = true;
                if (Convert.ToString(dr["txtzsg"]).Contains("4")) diyuzd.Checked = true;

                if (Convert.ToString(dr["pflx"]).Contains("1")) jiankang.Checked = true;
                if (Convert.ToString(dr["pflx"]).Contains("2")) feibo.Checked = true;
                if (Convert.ToString(dr["pflx"]).Contains("3")) ganzao.Checked = true;
                if (Convert.ToString(dr["pflx"]).Contains("4")) shuizhong.Checked = true;
                if (Convert.ToString(dr["pflx"]).Contains("5")) chaoshi.Checked = true;
                if (Convert.ToString(dr["pflx"]).Contains("6")) yansecha.Checked = true;
                if (Convert.ToString(dr["pflx"]).Contains("7")) liekaihongban.Checked = true;

                if (Convert.ToString(dr["xbnl"]).Contains("1")) nan.Checked = true;
                if (Convert.ToString(dr["xbnl"]).Contains("2")) checnv.Checked = true;
                if (Convert.ToString(dr["xbnl"]).Contains("3")) check1416.Checked = true;
                if (Convert.ToString(dr["xbnl"]).Contains("4")) check5064.Checked = true;
                if (Convert.ToString(dr["xbnl"]).Contains("5")) check6574.Checked = true;
                if (Convert.ToString(dr["xbnl"]).Contains("6")) check7480.Checked = true;
                if (Convert.ToString(dr["xbnl"]).Contains("7")) check80jia.Checked = true;

                if (Convert.ToString(dr["yybl"]).Contains("1")) ebingzhi.Checked = true;
                if (Convert.ToString(dr["yybl"]).Contains("2")) xinshuai.Checked = true;
                if (Convert.ToString(dr["yybl"]).Contains("3")) xueguanbing.Checked = true;
                if (Convert.ToString(dr["yybl"]).Contains("4")) pinxue.Checked = true;
                if (Convert.ToString(dr["yybl"]).Contains("5")) chouyan.Checked = true;

                if (Convert.ToString(dr["kbnl"]).Contains("1")) wanquanzikong.Checked = true;
                if (Convert.ToString(dr["kbnl"]).Contains("2")) oushijin.Checked = true;
                if (Convert.ToString(dr["kbnl"]).Contains("3")) dabianshijin.Checked = true;
                if (Convert.ToString(dr["kbnl"]).Contains("4")) daxiaobiansj.Checked = true;

                if (Convert.ToString(dr["ydnl"]).Contains("1")) wanquan.Checked = true;
                if (Convert.ToString(dr["ydnl"]).Contains("2")) fanzaobuan.Checked = true;
                if (Convert.ToString(dr["ydnl"]).Contains("3")) lingmode.Checked = true;
                if (Convert.ToString(dr["ydnl"]).Contains("4")) xianzhide.Checked = true;
                if (Convert.ToString(dr["ydnl"]).Contains("5")) chidun.Checked = true;
                if (Convert.ToString(dr["ydnl"]).Contains("6")) guding.Checked = true;

                if (Convert.ToString(dr["ys"]).Contains("1")) zhongdeng.Checked = true;
                if (Convert.ToString(dr["ys"]).Contains("2")) cha.Checked = true;
                if (Convert.ToString(dr["ys"]).Contains("3")) bishi.Checked = true;
                if (Convert.ToString(dr["ys"]).Contains("4")) liuzhi.Checked = true;
                if (Convert.ToString(dr["ys"]).Contains("5")) jinshi.Checked = true;
                if (Convert.ToString(dr["ys"]).Contains("6")) yanshi.Checked = true;

                if (Convert.ToString(dr["sjxza"]).Contains("1")) tangnb.Checked = true;
                if (Convert.ToString(dr["sjxza"]).Contains("2")) shenjing.Checked = true;
                if (Convert.ToString(dr["sjxza"]).Contains("3")) ganjue.Checked = true;
                if (Convert.ToString(dr["sjxza"]).Contains("4")) shoushusj.Checked = true;
                if (Convert.ToString(dr["sjxza"]).Contains("5")) yaowu.Checked = true;
                zongpinfen.Text = dr["zpf"].ToString();
                comboBpghs.Text = dr["pghs"].ToString();

                if (Convert.ToString(dr["pfbzfl"]).Contains("1")) weixian1.Checked = true;
                if (Convert.ToString(dr["pfbzfl"]).Contains("2")) weixian2.Checked = true;
                if (Convert.ToString(dr["pfbzfl"]).Contains("3")) weixian3.Checked = true;

                if (Convert.ToString(dr["yfcs"]).Contains("1")) ningjiaodian.Checked = true;
                if (Convert.ToString(dr["yfcs"]).Contains("2")) jajpmfl.Checked = true;
                if (Convert.ToString(dr["yfcs"]).Contains("3")) tiweidian.Checked = true;
                if (Convert.ToString(dr["yfcs"]).Contains("4")) toumianbu.Checked = true;
                if (Convert.ToString(dr["yfcs"]).Contains("5")) erkuo.Checked = true;
                if (Convert.ToString(dr["yfcs"]).Contains("6")) jianjiagu.Checked = true;
                if (Convert.ToString(dr["yfcs"]).Contains("7")) zuobu.Checked = true;
                if (Convert.ToString(dr["yfcs"]).Contains("8")) diweibu.Checked = true;
                if (Convert.ToString(dr["yfcs"]).Contains("9")) zuogu.Checked = true;
                if (Convert.ToString(dr["yfcs"]).Contains("a")) gugucujiang.Checked = true;
                if (Convert.ToString(dr["yfcs"]).Contains("b")) qibu.Checked = true;
                if (Convert.ToString(dr["yfcs"]).Contains("c")) zugen.Checked = true;
                if (Convert.ToString(dr["yfcs"]).Contains("d")) huaibu.Checked = true;
                if (Convert.ToString(dr["yfcs"]).Contains("e")) qiagu.Checked = true;
                if (Convert.ToString(dr["yfcs"]).Contains("f")) wanzheng.Checked = true;
                if (Convert.ToString(dr["yfcs"]).Contains("g")) buwanzheng.Checked = true;
                if (Convert.ToString(dr["yfcs"]).Contains("h")) zhitifuliao.Checked = true;
            
               
            }
        }

        private void baocun()
        {
            try
            {
                Dictionary<string, string> SQF = new Dictionary<string, string>();
                int result = 0;


                SQF.Add("shoushuriqi", Convert.ToDateTime(dateTimeP.Value.ToString()).ToString("yyyy-MM-dd HH:mm"));
                SQF.Add("ZhuYuanhao", patid);
                SQF.Add("ssmingcheng", txtbMZSSMC.Text);
                
                string AddItem = "";
                AddItem = "";
                if (cheboxzd.Checked) AddItem += "1";
                if (chaoyuezd.Checked) AddItem += "2";
                if (feipang.Checked) AddItem += "3";
                if (diyuzd.Checked) AddItem += "4";
                SQF.Add("txtzsg", AddItem);
                AddItem = "";
                if (jiankang.Checked) AddItem += "1";
                if (feibo.Checked) AddItem += "2";
                if (ganzao.Checked) AddItem += "3";
                if (shuizhong.Checked) AddItem += "4";
                if (chaoshi.Checked) AddItem += "5";
                if (yansecha.Checked) AddItem += "6";
                if (liekaihongban.Checked) AddItem += "7";
                SQF.Add("pflx", AddItem);
                AddItem = "";
                if (nan.Checked) AddItem += "1";
                if (checnv.Checked) AddItem += "2";
                if (check1416.Checked) AddItem += "3";
                if (check5064.Checked) AddItem += "4";
                if (check6574.Checked) AddItem += "5";
                if (check7480.Checked) AddItem += "6";
                if (check80jia.Checked) AddItem += "7";
                SQF.Add("xbnl", AddItem);
                AddItem = "";
                if (ebingzhi.Checked) AddItem += "1";
                if (xinshuai.Checked) AddItem += "2";
                if (xueguanbing.Checked) AddItem += "3";
                if (pinxue.Checked) AddItem += "4";
                if (chouyan.Checked) AddItem += "5";
                SQF.Add("yybl", AddItem);
                AddItem = "";
                if (wanquanzikong.Checked) AddItem += "1";
                if (oushijin.Checked) AddItem += "2";
                if (dabianshijin.Checked) AddItem += "3";
                if (daxiaobiansj.Checked) AddItem += "4";
                SQF.Add("kbnl", AddItem);
                AddItem = "";
                if (wanquan.Checked) AddItem += "1";
                if (fanzaobuan.Checked) AddItem += "2";
                if (lingmode.Checked) AddItem += "3";
                if (xianzhide.Checked) AddItem += "4";
                if (chidun.Checked) AddItem += "5";
                if (guding.Checked) AddItem += "6";
                SQF.Add("ydnl", AddItem);
                AddItem = "";
                if (zhongdeng.Checked) AddItem += "1";
                if (cha.Checked) AddItem += "2";
                if (bishi.Checked) AddItem += "3";
                if (liuzhi.Checked) AddItem += "4";
                if (jinshi.Checked) AddItem += "5";
                if (yanshi.Checked) AddItem += "6";
                SQF.Add("ys", AddItem);
                AddItem = "";
                if (tangnb.Checked) AddItem += "1";
                if (shenjing.Checked) AddItem += "2";
                if (ganjue.Checked) AddItem += "3";
                if (shoushusj.Checked) AddItem += "4";
                if (yaowu.Checked) AddItem += "5";
                SQF.Add("sjxza", AddItem);
                SQF.Add("zpf", zongpinfen.Text);
                SQF.Add("pghs", comboBpghs.Text);
                AddItem = "";
                if (weixian1.Checked) AddItem += "1";
                if (weixian2.Checked) AddItem += "2";
                if (weixian3.Checked) AddItem += "3";
                SQF.Add("pfbzfl", AddItem);
                AddItem = "";
                if (ningjiaodian.Checked) AddItem += "1";
                if (jajpmfl.Checked) AddItem += "2";
                if (tiweidian.Checked) AddItem += "3";
                if (toumianbu.Checked) AddItem += "4";
                if (erkuo.Checked) AddItem += "5";
                if (jianjiagu.Checked) AddItem += "6";
                if (zuobu.Checked) AddItem += "7";
                if (diweibu.Checked) AddItem += "8";
                if (zuogu.Checked) AddItem += "9";
                if (gugucujiang.Checked) AddItem += "a";
                if (qibu.Checked) AddItem += "b";
                if (zugen.Checked) AddItem += "c";
                if (huaibu.Checked) AddItem += "d";
                if (qiagu.Checked) AddItem += "e";
                if (wanzheng.Checked) AddItem += "f";
                if (buwanzheng.Checked) AddItem += "g";
                if (zhitifuliao.Checked) AddItem += "h";
                
                SQF.Add("yfcs", AddItem);

               
                //  comboxssdj
                DataTable dt = dal.GetPenpleInfoylsspgb(patid);

                if (dt.Rows.Count > 0)
                {
                    result = dal.Updateylsspgb(SQF);
                    MessageBox.Show("修改成功");
                }
                else
                {
                    result = dal.TljInylsspgb(SQF);
                    MessageBox.Show("保存成功！");
                }
            }
            catch(Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

       

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            baocun();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void cheboxzd_CheckedChanged(object sender, EventArgs e)
        {
            if (cheboxzd.Checked == true)
            {

                chaoyuezd.Checked = false;
                feipang.Checked = false;
                diyuzd.Checked = false;
                a = 0;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }
        private void chaoyuezd_CheckedChanged(object sender, EventArgs e)
        {
            if (chaoyuezd.Checked == true)
            {
                cheboxzd.Checked = false;


                feipang.Checked = false;
                diyuzd.Checked = false;
                a = 1;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void feipang_CheckedChanged(object sender, EventArgs e)
        {
            if (feipang.Checked == true)
            {
                cheboxzd.Checked = false;
                a = 2;
                chaoyuezd.Checked = false;

                diyuzd.Checked = false;

                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }
        private void diyuzd_CheckedChanged(object sender, EventArgs e)
        {
            if (diyuzd.Checked == true)
            {
                cheboxzd.Checked = false;
                a = 3;
                chaoyuezd.Checked = false;
                feipang.Checked = false;


                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void jiankang_CheckedChanged(object sender, EventArgs e)
        {
            if (jiankang.Checked == true)
            {
                feibo.Checked = false;
                ganzao.Checked = false;
                shuizhong.Checked = false;
                chaoshi.Checked = false;
                yansecha.Checked = false;
                liekaihongban.Checked = false;
                b = 0;

                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void feibo_CheckedChanged(object sender, EventArgs e)
        {
            if (feibo.Checked == true)
            {
                jiankang.Checked = false;
                ganzao.Checked = false;
                shuizhong.Checked = false;
                chaoshi.Checked = false;
                yansecha.Checked = false;
                liekaihongban.Checked = false;
                b = 1;

                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void ganzao_CheckedChanged(object sender, EventArgs e)
        {
            if (ganzao.Checked == true)
            {
                jiankang.Checked = false;
                feibo.Checked = false;
                shuizhong.Checked = false;
                chaoshi.Checked = false;
                yansecha.Checked = false;
                liekaihongban.Checked = false;
                b = 1;

                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void shuizhong_CheckedChanged(object sender, EventArgs e)
        {
            if (shuizhong.Checked == true)
            {
                jiankang.Checked = false;
                feibo.Checked = false;
                ganzao.Checked = false;
                chaoshi.Checked = false;
                yansecha.Checked = false;
                liekaihongban.Checked = false;
                b = 1;

                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void chaoshi_CheckedChanged(object sender, EventArgs e)
        {
            if (chaoshi.Checked == true)
            {
                jiankang.Checked = false;
                feibo.Checked = false;
                ganzao.Checked = false;
                shuizhong.Checked = false;
                yansecha.Checked = false;
                liekaihongban.Checked = false;
                b = 1;

                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void yansecha_CheckedChanged(object sender, EventArgs e)
        {
            if ( yansecha.Checked == true)
            {
                jiankang.Checked = false;
                feibo.Checked = false;
                ganzao.Checked = false;
                shuizhong.Checked = false;
                chaoshi.Checked = false;
                liekaihongban.Checked = false;
                b = 2;

                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void liekaihongban_CheckedChanged(object sender, EventArgs e)
        {
            if ( liekaihongban.Checked == true)
            {
                jiankang.Checked = false;
                feibo.Checked = false;
                ganzao.Checked = false;
                shuizhong.Checked = false;
                chaoshi.Checked = false;
                yansecha.Checked = false;
                b = 3;

                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void nan_CheckedChanged(object sender, EventArgs e)
        {
            if (nan.Checked == true)
            {
                checnv.Checked = false;
                
                c = 1;

                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void checnv_CheckedChanged(object sender, EventArgs e)
        {
            if ( checnv.Checked == true)
            {
                nan.Checked = false;
                //check1416.Checked = false;
                //check5064.Checked = false;
                //check6574.Checked = false;
                //check7480.Checked = false;
                //check80jia.Checked = false;
                c = 2;

                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void check1416_CheckedChanged(object sender, EventArgs e)
        {
            if (check1416.Checked == true)
            {
                check5064.Checked = false;
                check6574.Checked = false;
                check7480.Checked = false;
                check80jia.Checked = false;
                c1 = 1;

                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void check5064_CheckedChanged(object sender, EventArgs e)
        {
            if ( check5064.Checked == true)
            {
                check1416.Checked = false;
                check6574.Checked = false;
                check7480.Checked = false;
                check80jia.Checked = false;
                c1 = 2;

                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void check6574_CheckedChanged(object sender, EventArgs e)
        {
            if (check6574.Checked == true)
            {
                check1416.Checked = false;
                check5064.Checked = false;
                check7480.Checked = false;
                check80jia.Checked = false;
                c1 = 3;

                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void check7480_CheckedChanged(object sender, EventArgs e)
        {
            if (check7480.Checked == true)
            {
                check1416.Checked = false;
                check5064.Checked = false;
                check6574.Checked = false;
                check80jia.Checked = false;
                c1 = 4;

                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void check80jia_CheckedChanged(object sender, EventArgs e)
        {
            if (check80jia.Checked == true)
            {
                check1416.Checked = false;
                check5064.Checked = false;
                check6574.Checked = false;
                check7480.Checked = false;
                c1 = 5;

                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void ebingzhi_CheckedChanged(object sender, EventArgs e)
        {
            if (ebingzhi.Checked == true)
            {
                xinshuai.Checked = false;
                xueguanbing.Checked = false;
                pinxue.Checked = false;
                chouyan.Checked = false;
                d =8;

                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void xinshuai_CheckedChanged(object sender, EventArgs e)
        {
            if (xinshuai.Checked == true)
            {
                ebingzhi.Checked = false;
                xueguanbing.Checked = false;
                pinxue.Checked = false;
                chouyan.Checked = false;
                d = 5;

                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void xueguanbing_CheckedChanged(object sender, EventArgs e)
        {
            if (xueguanbing.Checked == true)
            {
                ebingzhi.Checked = false;
                xinshuai.Checked = false;
                pinxue.Checked = false;
                chouyan.Checked = false;
                d = 5;

                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void pinxue_CheckedChanged(object sender, EventArgs e)
        {
            if (pinxue.Checked == true)
            {
                ebingzhi.Checked = false;
                xinshuai.Checked = false;
                xueguanbing.Checked = false;
                chouyan.Checked = false;
                d = 2;

                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void chouyan_CheckedChanged(object sender, EventArgs e)
        {
            if (chouyan.Checked == true)
            {
                ebingzhi.Checked = false;
                xinshuai.Checked = false;
                xueguanbing.Checked = false;
                pinxue.Checked = false;
                d = 1;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void wanquanzikong_CheckedChanged(object sender, EventArgs e)
        {
            if (wanquanzikong.Checked == true)
            {
                oushijin.Checked = false;
                dabianshijin.Checked = false;
                daxiaobiansj.Checked = false;
                f= 0;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void oushijin_CheckedChanged(object sender, EventArgs e)
        {
            if (oushijin.Checked == true)
            {
                wanquanzikong.Checked = false;
                dabianshijin.Checked = false;
                daxiaobiansj.Checked = false;
                f = 1;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void dabianshijin_CheckedChanged(object sender, EventArgs e)
        {
            if (dabianshijin.Checked == true)
            {
                wanquanzikong.Checked = false;
                oushijin.Checked = false;
                daxiaobiansj.Checked = false;
                f = 2;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void daxiaobiansj_CheckedChanged(object sender, EventArgs e)
        {
            if (daxiaobiansj.Checked == true)
            {
                wanquanzikong.Checked = false;
                oushijin.Checked = false;
                dabianshijin.Checked = false;
                f = 3;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void wanquan_CheckedChanged(object sender, EventArgs e)
        {
            if (wanquan.Checked == true)
            {
                fanzaobuan.Checked = false;
                lingmode.Checked = false;
                xianzhide.Checked = false;
                chidun.Checked = false;
                guding.Checked = false;
               
                g = 0;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void fanzaobuan_CheckedChanged(object sender, EventArgs e)
        {
            if (fanzaobuan.Checked == true)
            {
                wanquan.Checked = false;
                lingmode.Checked = false;
                xianzhide.Checked = false;
                chidun.Checked = false;
                guding.Checked = false;

                g = 1;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void lingmode_CheckedChanged(object sender, EventArgs e)
        {
            if (lingmode.Checked == true)
            {
                wanquan.Checked = false;
                fanzaobuan.Checked = false;
                xianzhide.Checked = false;
                chidun.Checked = false;
                guding.Checked = false;

                g = 2;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void xianzhide_CheckedChanged(object sender, EventArgs e)
        {
            if (xianzhide.Checked == true)
            {
                wanquan.Checked = false;
                fanzaobuan.Checked = false;
                lingmode.Checked = false;
                chidun.Checked = false;
                guding.Checked = false;

                g = 3;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void chidun_CheckedChanged(object sender, EventArgs e)
        {
            if (chidun.Checked == true)
            {
                wanquan.Checked = false;
                fanzaobuan.Checked = false;
                lingmode.Checked = false;
                xianzhide.Checked = false;
                guding.Checked = false;

                g = 4;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void guding_CheckedChanged(object sender, EventArgs e)
        {
            if (guding.Checked == true)
            {
                wanquan.Checked = false;
                fanzaobuan.Checked = false;
                lingmode.Checked = false;
                xianzhide.Checked = false;
                chidun.Checked = false;

                g = 5;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void zhongdeng_CheckedChanged(object sender, EventArgs e)
        {
            if (zhongdeng.Checked == true)
            {
                cha.Checked = false;
                bishi.Checked = false;
                liuzhi.Checked = false;
                jinshi.Checked = false;
                yanshi.Checked = false;

                h = 0;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void cha_CheckedChanged(object sender, EventArgs e)
        {
            if (cha.Checked == true)
            {
                zhongdeng.Checked = false;
                bishi.Checked = false;
                liuzhi.Checked = false;
                jinshi.Checked = false;
                yanshi.Checked = false;

                h = 1;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void bishi_CheckedChanged(object sender, EventArgs e)
        {
            if (bishi.Checked == true)
            {
                zhongdeng.Checked = false;
                cha.Checked = false;
                liuzhi.Checked = false;
                jinshi.Checked = false;
                yanshi.Checked = false;

                h = 2;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void liuzhi_CheckedChanged(object sender, EventArgs e)
        {
            if (liuzhi.Checked == true)
            {
                zhongdeng.Checked = false;
                cha.Checked = false;
                bishi.Checked = false;
                jinshi.Checked = false;
                yanshi.Checked = false;

                h = 2;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void jinshi_CheckedChanged(object sender, EventArgs e)
        {
            if (jinshi.Checked == true)
            {
                zhongdeng.Checked = false;
                cha.Checked = false;
                bishi.Checked = false;
                liuzhi.Checked = false;
                yanshi.Checked = false;

                h = 3;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void yanshi_CheckedChanged(object sender, EventArgs e)
        {
            if (yanshi.Checked == true)
            {
                zhongdeng.Checked = false;
                cha.Checked = false;
                bishi.Checked = false;
                liuzhi.Checked = false;
                jinshi.Checked = false;

                h = 3;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void tangnb_CheckedChanged(object sender, EventArgs e)
        {
            if (tangnb.Checked == true)
            {
                shenjing.Checked = false;
                ganjue.Checked = false;
               

                i = 4;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void shenjing_CheckedChanged(object sender, EventArgs e)
        {
            if (shenjing.Checked == true)
            {
                tangnb.Checked = false;
                ganjue.Checked = false;

                i = 5;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void ganjue_CheckedChanged(object sender, EventArgs e)
        {
            if (ganjue.Checked == true)
            {
                tangnb.Checked = false;
                shenjing.Checked = false;

                i = 6;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void shoushusj_CheckedChanged(object sender, EventArgs e)
        {
            if (shoushusj.Checked == true)
            {
                
                j =5;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }

            }
            if (shoushusj.Checked == false)
            {
                j = 0;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void yaowu_CheckedChanged(object sender, EventArgs e)
        {
            if (yaowu.Checked == true)
            {
                k = 4;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
            if (yaowu.Checked == false)
            {
                k = 0;
                zongpinfen.Text = (a + b + c + d + f + g + h + i + j + k + c1).ToString();
                int v = a + b + c + d + f + g + h + i + j + k + c1;

                if (10 <= v && v <= 14)
                {
                    weixian1.Checked = true;
                    weixian2.Checked = false;
                    weixian3.Checked = false;
                }
                if (15 <= v && v <= 19)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = true;
                    weixian3.Checked = false;
                }
                if (v > 20)
                {
                    weixian1.Checked = false;
                    weixian2.Checked = false;
                    weixian3.Checked = true;
                }
            }
        }

        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;//打印
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            printDocument1.DefaultPageSettings.PaperSize =
                        new System.Drawing.Printing.PaperSize("A4", 820, 1160);
        }

       

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font ptzt9 = new Font("微软雅黑", 9);//普通字体  
            Font ptzt10 = new Font("微软雅黑", 10);//普通字体 
            Font ptzt11 = new Font("宋体", 11);//普通字体 
            Font ptzt13 = new Font("微软雅黑", 13);//普通字体 
            Font ptzt18 = new Font("微软雅黑", 18);//普通字体   
            Pen pblack2 = new Pen(Brushes.Black, 2);

            int y = 70 ,y1; int x = 30;
            string title2 = "新疆医科大学第五附属医院";
            e.Graphics.DrawString(title2, ptzt18, Brushes.Black, new Point(x + 210, y - 10));
            y = y + 40;
            string title1 = "手术患者压力性损伤评估表";
            e.Graphics.DrawString(title1, ptzt18, Brushes.Black, new Point(x + 210, y - 10));
            x = x + 10; y = y + 40;
            e.Graphics.DrawString("姓名:" + txtbHZName.Text, ptzt11, Brushes.Black, new Point(x + 25, y));
            e.Graphics.DrawString("性别:" + txtbSex.Text, ptzt11, Brushes.Black, new Point(x + 220, y));
            e.Graphics.DrawString("年龄:" + txtbAge.Text, ptzt11, Brushes.Black, new Point(x + 280, y));
            e.Graphics.DrawString("科室:" + txtbKeShi.Text, ptzt11, Brushes.Black, new Point(x + 350, y));
            e.Graphics.DrawString("床号:" + txtbChuangHao.Text, ptzt11, Brushes.Black, new Point(x + 530, y));
            e.Graphics.DrawString("住院号:" + txtbZYH.Text, ptzt11, Brushes.Black, new Point(x + 590, y));
           // e.Graphics.DrawString("年龄:" + txtbAge.Text, ptzt11, Brushes.Black, new Point(x + 350, y));
            //e.Graphics.DrawString("性别:" + txtbSex.Text, ptzt11, Brushes.Black, new Point(x + 900, y));
            y = y + 30;
            e.Graphics.DrawString("日期:    " + dateTimeP.Value.ToString("yyyy年MM月dd日"), ptzt11, Brushes.Black, new Point(x + 25, y));
            //e.Graphics.DrawString("麻醉方式:" + txtbMZFS.Text, ptzt11, Brushes.Black, new Point(x + 220, y));
            e.Graphics.DrawString("手术实施名称:" + txtbMZSSMC.Text, ptzt11, Brushes.Black, new Point(x + 300, y));
            y = y + 30;
            e.Graphics.DrawLine(Pens.Black, x + 25, y + 0, x + 700, y + 0);
            e.Graphics.DrawLine(Pens.Black, x + 25, y + 0, x +25, y + 770);
            e.Graphics.DrawLine(Pens.Black, x + 700, y + 0, x + 700, y + 770);
            e.Graphics.DrawLine(Pens.Black, x + 195, y + 0, x + 195, y + 540);
            e.Graphics.DrawLine(Pens.Black, x + 365, y + 0, x + 365, y + 540);
            e.Graphics.DrawLine(Pens.Black, x + 535, y + 0, x + 535, y + 540);
            e.Graphics.DrawString("  体形、体重与身高", ptzt11, Brushes.Black, new Point(x + 25, y+10));
            e.Graphics.DrawString("危险区域的皮肤类型", ptzt11, Brushes.Black, new Point(x + 195, y + 10));
            e.Graphics.DrawString("    性别与年龄", ptzt11, Brushes.Black, new Point(x + 365, y + 10));
            e.Graphics.DrawString("   组织营养不良", ptzt11, Brushes.Black, new Point(x + 535, y + 10));
            y = y + 30; y1 = y + 10;
            e.Graphics.DrawLine(Pens.Black, x + 25, y + 0, x + 700, y + 0);
            e.Graphics.DrawString("中等", ptzt11, Brushes.Black, x + 30, y1);
            e.Graphics.DrawString("0", ptzt11, Brushes.Black, x + 160, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 175, y1, 10, 10);
            if (cheboxzd.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 175, y1, x + 180, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 180, y1 + 10, x + 185, y1);
            }

            
            e.Graphics.DrawString("健康", ptzt11, Brushes.Black, x + 200, y1);
            e.Graphics.DrawString("0", ptzt11, Brushes.Black, x + 330, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 345, y1, 10, 10);
            if (jiankang.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 345, y1, x + 350, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 350, y1 + 10, x + 355, y1);
            }

            e.Graphics.DrawString("男", ptzt11, Brushes.Black, x + 365, y1);
            e.Graphics.DrawString("1", ptzt11, Brushes.Black, x + 500, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 515, y1, 10, 10);
            if (nan.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 515, y1, x + 520, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 520, y1 + 10, x + 525, y1);
            }

            e.Graphics.DrawString("恶病质", ptzt11, Brushes.Black, x + 535, y1);
            e.Graphics.DrawString("8", ptzt11, Brushes.Black, x + 665, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 680, y1, 10, 10);
            if (ebingzhi.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 680, y1, x + 685, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 685, y1 + 10, x + 690, y1);
            }
            

            y = y + 30; y1 = y + 10;
          
            e.Graphics.DrawString("超过中等", ptzt11, Brushes.Black, x + 30, y1);
            e.Graphics.DrawString("1", ptzt11, Brushes.Black, x + 160, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 175, y1, 10, 10);
            if (chaoyuezd.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 175, y1, x + 180, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 180, y1 + 10, x + 185, y1);
            }


            e.Graphics.DrawString("菲薄", ptzt11, Brushes.Black, x + 200, y1);
            e.Graphics.DrawString("1", ptzt11, Brushes.Black, x + 330, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 345, y1, 10, 10);
            if (feibo.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 345, y1, x + 350, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 350, y1 + 10, x + 355, y1);
            }

            e.Graphics.DrawString("女", ptzt11, Brushes.Black, x + 365, y1);
            e.Graphics.DrawString("2", ptzt11, Brushes.Black, x + 500, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 515, y1, 10, 10);
            if (checnv.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 515, y1, x + 520, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 520, y1 + 10, x + 525, y1);
            }

            e.Graphics.DrawString("心衰", ptzt11, Brushes.Black, x + 535, y1);
            e.Graphics.DrawString("5", ptzt11, Brushes.Black, x + 665, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 680, y1, 10, 10);
            if (xinshuai.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 680, y1, x + 685, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 685, y1 + 10, x + 690, y1);
            }


            y = y + 30; y1 = y + 10;
            e.Graphics.DrawString("肥胖", ptzt11, Brushes.Black, x + 30, y1);
            e.Graphics.DrawString("2", ptzt11, Brushes.Black, x + 160, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 175, y1, 10, 10);
            if (feipang.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 175, y1, x + 180, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 180, y1 + 10, x + 185, y1);
            }

            e.Graphics.DrawString("干燥", ptzt11, Brushes.Black, x + 200, y1);
            e.Graphics.DrawString("1", ptzt11, Brushes.Black, x + 330, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 345, y1, 10, 10);
            if (ganzao.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 345, y1, x + 350, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 350, y1 + 10, x + 355, y1);
            }

            e.Graphics.DrawString("14~49", ptzt11, Brushes.Black, x + 365, y1);
            e.Graphics.DrawString("1", ptzt11, Brushes.Black, x + 500, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 515, y1, 10, 10);
            if (check1416.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 515, y1, x + 520, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 520, y1 + 10, x + 525, y1);
            }

            e.Graphics.DrawString("外周血管病", ptzt11, Brushes.Black, x + 535, y1);
            e.Graphics.DrawString("5", ptzt11, Brushes.Black, x + 665, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 680, y1, 10, 10);
            if (xueguanbing.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 680, y1, x + 685, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 685, y1 + 10, x + 690, y1);
            }


            y = y + 30; y1 = y + 10;
            e.Graphics.DrawString("低于中等", ptzt11, Brushes.Black, x + 30, y1);
            e.Graphics.DrawString("3", ptzt11, Brushes.Black, x + 160, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 175, y1, 10, 10);
            if (diyuzd.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 175, y1, x + 180, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 180, y1 + 10, x + 185, y1);
            }

            e.Graphics.DrawString("水肿", ptzt11, Brushes.Black, x + 200, y1);
            e.Graphics.DrawString("1", ptzt11, Brushes.Black, x + 330, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 345, y1, 10, 10);
            if (shuizhong.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 345, y1, x + 350, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 350, y1 + 10, x + 355, y1);
            }

            e.Graphics.DrawString("50~64", ptzt11, Brushes.Black, x + 365, y1);
            e.Graphics.DrawString("2", ptzt11, Brushes.Black, x + 500, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 515, y1, 10, 10);
            if (check5064.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 515, y1, x + 520, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 520, y1 + 10, x + 525, y1);
            }

            e.Graphics.DrawString("贫血", ptzt11, Brushes.Black, x + 535, y1);
            e.Graphics.DrawString("2", ptzt11, Brushes.Black, x + 665, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 680, y1, 10, 10);
            if (pinxue.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 680, y1, x + 685, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 685, y1 + 10, x + 690, y1);
            }

            y = y + 30; y1 = y + 10;
            e.Graphics.DrawString("(参考亚洲人标准体重表)", ptzt10, Brushes.Black, x + 25, y1);
            //e.Graphics.DrawString("3", ptzt11, Brushes.Black, x + 160, y1);
            //e.Graphics.DrawRectangle(Pens.Black, x + 175, y1, 10, 10);
            //if (diyuzd.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 175, y1, x + 180, y1 + 10);
            //    e.Graphics.DrawLine(pblack2, x + 180, y1 + 10, x + 185, y1);
            //}

            e.Graphics.DrawString("潮湿", ptzt11, Brushes.Black, x + 200, y1);
            e.Graphics.DrawString("1", ptzt11, Brushes.Black, x + 330, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 345, y1, 10, 10);
            if (chaoshi.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 345, y1, x + 350, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 350, y1 + 10, x + 355, y1);
            }

            e.Graphics.DrawString("65~74", ptzt11, Brushes.Black, x + 365, y1);
            e.Graphics.DrawString("3", ptzt11, Brushes.Black, x + 500, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 515, y1, 10, 10);
            if (check6574.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 515, y1, x + 520, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 520, y1 + 10, x + 525, y1);
            }

            e.Graphics.DrawString("抽烟", ptzt11, Brushes.Black, x + 535, y1);
            e.Graphics.DrawString("1", ptzt11, Brushes.Black, x + 665, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 680, y1, 10, 10);
            if (chouyan.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 680, y1, x + 685, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 685, y1 + 10, x + 690, y1);
            }

            y = y + 30; y1 = y + 10;
            //e.Graphics.DrawString("低于中等", ptzt11, Brushes.Black, x + 30, y1);
            //e.Graphics.DrawString("3", ptzt11, Brushes.Black, x + 160, y1);
            //e.Graphics.DrawRectangle(Pens.Black, x + 175, y1, 10, 10);
            //if (diyuzd.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 175, y1, x + 180, y1 + 10);
            //    e.Graphics.DrawLine(pblack2, x + 180, y1 + 10, x + 185, y1);
            //}

            e.Graphics.DrawString("颜色差", ptzt11, Brushes.Black, x + 200, y1);
            e.Graphics.DrawString("2", ptzt11, Brushes.Black, x + 330, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 345, y1, 10, 10);
            if (yansecha.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 345, y1, x + 350, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 350, y1 + 10, x + 355, y1);
            }

            e.Graphics.DrawString("75~80", ptzt11, Brushes.Black, x + 365, y1);
            e.Graphics.DrawString("4", ptzt11, Brushes.Black, x + 500, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 515, y1, 10, 10);
            if (check7480.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 515, y1, x + 520, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 520, y1 + 10, x + 525, y1);
            }

            //e.Graphics.DrawString("抽烟", ptzt11, Brushes.Black, x + 535, y1);
            //e.Graphics.DrawString("1", ptzt11, Brushes.Black, x + 665, y1);
            //e.Graphics.DrawRectangle(Pens.Black, x + 680, y1, 10, 10);
            //if (chouyan.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 680, y1, x + 685, y1 + 10);
            //    e.Graphics.DrawLine(pblack2, x + 685, y1 + 10, x + 690, y1);
            //}

            y = y + 30; y1 = y + 10;
            //e.Graphics.DrawString("低于中等", ptzt11, Brushes.Black, x + 30, y1);
            //e.Graphics.DrawString("3", ptzt11, Brushes.Black, x + 160, y1);
            //e.Graphics.DrawRectangle(Pens.Black, x + 175, y1, 10, 10);
            //if (diyuzd.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 175, y1, x + 180, y1 + 10);
            //    e.Graphics.DrawLine(pblack2, x + 180, y1 + 10, x + 185, y1);
            //}

            e.Graphics.DrawString("裂开/红斑", ptzt11, Brushes.Black, x + 200, y1);
            e.Graphics.DrawString("3", ptzt11, Brushes.Black, x + 330, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 345, y1, 10, 10);
            if (liekaihongban.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 345, y1, x + 350, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 350, y1 + 10, x + 355, y1);
            }

            e.Graphics.DrawString("81+", ptzt11, Brushes.Black, x + 365, y1);
            e.Graphics.DrawString("5", ptzt11, Brushes.Black, x + 500, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 515, y1, 10, 10);
            if (check80jia.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 515, y1, x + 520, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 520, y1 + 10, x + 525, y1);
            }

            //e.Graphics.DrawString("抽烟", ptzt11, Brushes.Black, x + 535, y1);
            //e.Graphics.DrawString("1", ptzt11, Brushes.Black, x + 665, y1);
            //e.Graphics.DrawRectangle(Pens.Black, x + 680, y1, 10, 10);
            //if (chouyan.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 680, y1, x + 685, y1 + 10);
            //    e.Graphics.DrawLine(pblack2, x + 685, y1 + 10, x + 690, y1);
            //}
            y = y + 30; y1 = y + 10;
            e.Graphics.DrawLine(Pens.Black, x + 25, y + 0, x + 700, y + 0);
            e.Graphics.DrawString("   控便能力", ptzt11, Brushes.Black, new Point(x + 25, y + 10));
            e.Graphics.DrawString("     运动能力", ptzt11, Brushes.Black, new Point(x + 195, y + 10));
            e.Graphics.DrawString("        饮食", ptzt11, Brushes.Black, new Point(x + 365, y + 10));
            e.Graphics.DrawString("    神经性障碍", ptzt11, Brushes.Black, new Point(x + 535, y + 10));
            y = y + 30; y1 = y + 10;
            e.Graphics.DrawLine(Pens.Black, x + 25, y + 0, x + 700, y + 0);

            e.Graphics.DrawString("完全自控", ptzt11, Brushes.Black, x + 30, y1);
            e.Graphics.DrawString("0", ptzt11, Brushes.Black, x + 160, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 175, y1, 10, 10);
            if (wanquanzikong.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 175, y1, x + 180, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 180, y1 + 10, x + 185, y1);
            }

            e.Graphics.DrawString("完全", ptzt11, Brushes.Black, x + 200, y1);
            e.Graphics.DrawString("0", ptzt11, Brushes.Black, x + 330, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 345, y1, 10, 10);
            if (wanquan.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 345, y1, x + 350, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 350, y1 + 10, x + 355, y1);
            }

            e.Graphics.DrawString("中等", ptzt11, Brushes.Black, x + 365, y1);
            e.Graphics.DrawString("0", ptzt11, Brushes.Black, x + 500, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 515, y1, 10, 10);
            if (zhongdeng.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 515, y1, x + 520, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 520, y1 + 10, x + 525, y1);
            }

            e.Graphics.DrawString("糖尿病/多发性硬化", ptzt11, Brushes.Black, x + 535, y1);
            e.Graphics.DrawString("/脑血管意外/运动/", ptzt11, Brushes.Black, x + 535, y1 + 15);
            //e.Graphics.DrawString("1", ptzt11, Brushes.Black, x + 665, y1);
            //e.Graphics.DrawRectangle(Pens.Black, x + 680, y1, 10, 10);
            //if (chouyan.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 680, y1, x + 685, y1 + 10);
            //    e.Graphics.DrawLine(pblack2, x + 685, y1 + 10, x + 690, y1);
            //}

            y = y + 30; y1 = y + 10;
            //e.Graphics.DrawLine(Pens.Black, x + 25, y + 0, x + 700, y + 0);

            e.Graphics.DrawString("偶失禁", ptzt11, Brushes.Black, x + 30, y1);
            e.Graphics.DrawString("1", ptzt11, Brushes.Black, x + 160, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 175, y1, 10, 10);
            if (oushijin.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 175, y1, x + 180, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 180, y1 + 10, x + 185, y1);
            }

            e.Graphics.DrawString("烦躁不安", ptzt11, Brushes.Black, x + 200, y1);
            e.Graphics.DrawString("1", ptzt11, Brushes.Black, x + 330, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 345, y1, 10, 10);
            if (fanzaobuan.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 345, y1, x + 350, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 350, y1 + 10, x + 355, y1);
            }

            e.Graphics.DrawString("差", ptzt11, Brushes.Black, x + 365, y1);
            e.Graphics.DrawString("1", ptzt11, Brushes.Black, x + 500, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 515, y1, 10, 10);
            if (cha.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 515, y1, x + 520, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 520, y1 + 10, x + 525, y1);
            }

            e.Graphics.DrawString("感觉/神经障碍", ptzt11, Brushes.Black, x + 535, y1);
            //e.Graphics.DrawString("1", ptzt11, Brushes.Black, x + 665, y1);
            //e.Graphics.DrawRectangle(Pens.Black, x + 680, y1, 10, 10);
            //if (chouyan.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 680, y1, x + 685, y1 + 10);
            //    e.Graphics.DrawLine(pblack2, x + 685, y1 + 10, x + 690, y1);
            //}

            y = y + 30; y1 = y + 10;
           
            e.Graphics.DrawString("尿/大便失禁", ptzt11, Brushes.Black, x + 30, y1);
            e.Graphics.DrawString("2", ptzt11, Brushes.Black, x + 160, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 175, y1, 10, 10);
            if (dabianshijin.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 175, y1, x + 180, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 180, y1 + 10, x + 185, y1);
            }

            e.Graphics.DrawString("冷漠的", ptzt11, Brushes.Black, x + 200, y1);
            e.Graphics.DrawString("2", ptzt11, Brushes.Black, x + 330, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 345, y1, 10, 10);
            if (lingmode.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 345, y1, x + 350, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 350, y1 + 10, x + 355, y1);
            }

            e.Graphics.DrawString("鼻饲", ptzt11, Brushes.Black, x + 365, y1);
            e.Graphics.DrawString("2", ptzt11, Brushes.Black, x + 500, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 515, y1, 10, 10);
            if (bishi.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 515, y1, x + 520, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 520, y1 + 10, x + 525, y1);
            }

            //e.Graphics.DrawString("运动/感觉/神经障碍", ptzt11, Brushes.Black, x + 535, y1);
            e.Graphics.DrawString("4", ptzt11, Brushes.Black, x + 535, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 550, y1, 10, 10);
            if (tangnb.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 550, y1, x + 555, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 555, y1 + 10, x + 560, y1);
            }

            e.Graphics.DrawString("5", ptzt11, Brushes.Black, x + 605, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 620, y1, 10, 10);
            if (shenjing.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 620, y1, x + 625, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 625, y1 + 10, x + 630, y1);
            }

            e.Graphics.DrawString("6", ptzt11, Brushes.Black, x + 665, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 680, y1, 10, 10);
            if (ganjue.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 680, y1, x + 685, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 685, y1 + 10, x + 690, y1);
            }
            y = y + 30; y1 = y + 10;
            e.Graphics.DrawLine(Pens.Black, x + 535, y + 0, x + 700, y + 0);
            e.Graphics.DrawString("大小便失禁", ptzt11, Brushes.Black, x + 30, y1);
            e.Graphics.DrawString("3", ptzt11, Brushes.Black, x + 160, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 175, y1, 10, 10);
            if (daxiaobiansj.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 175, y1, x + 180, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 180, y1 + 10, x + 185, y1);
            }

            e.Graphics.DrawString("限制", ptzt11, Brushes.Black, x + 200, y1);
            e.Graphics.DrawString("3", ptzt11, Brushes.Black, x + 330, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 345, y1, 10, 10);
            if (xianzhide.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 345, y1, x + 350, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 350, y1 + 10, x + 355, y1);
            }

            e.Graphics.DrawString("流质", ptzt11, Brushes.Black, x + 365, y1);
            e.Graphics.DrawString("2", ptzt11, Brushes.Black, x + 500, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 515, y1, 10, 10);
            if (liuzhi.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 515, y1, x + 520, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 520, y1 + 10, x + 525, y1);
            }

            e.Graphics.DrawString("大手术/创伤", ptzt11, Brushes.Black, x + 535, y1);

            y = y + 30; y1 = y + 10;
            e.Graphics.DrawLine(Pens.Black, x + 535, y + 0, x + 700, y + 0);
            //e.Graphics.DrawString("大小便失禁", ptzt11, Brushes.Black, x + 30, y1);
            //e.Graphics.DrawString("3", ptzt11, Brushes.Black, x + 160, y1);
            //e.Graphics.DrawRectangle(Pens.Black, x + 175, y1, 10, 10);
            //if (daxiaobiansj.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 175, y1, x + 180, y1 + 10);
            //    e.Graphics.DrawLine(pblack2, x + 180, y1 + 10, x + 185, y1);
            //}

            e.Graphics.DrawString("迟钝", ptzt11, Brushes.Black, x + 200, y1);
            e.Graphics.DrawString("4", ptzt11, Brushes.Black, x + 330, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 345, y1, 10, 10);
            if (chidun.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 345, y1, x + 350, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 350, y1 + 10, x + 355, y1);
            }

            e.Graphics.DrawString("禁食", ptzt11, Brushes.Black, x + 365, y1);
            e.Graphics.DrawString("3", ptzt11, Brushes.Black, x + 500, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 515, y1, 10, 10);
            if (jinshi.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 515, y1, x + 520, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 520, y1 + 10, x + 525, y1);
            }

            e.Graphics.DrawString("腰以下/脊椎的大手术", ptzt11, Brushes.Black, x + 535, y1);
            e.Graphics.DrawString("或创伤", ptzt11, Brushes.Black, x + 535, y1+15);
            y = y + 30; y1 = y + 10;
           

            //e.Graphics.DrawString("偶失禁", ptzt11, Brushes.Black, x + 30, y1);
            //e.Graphics.DrawString("1", ptzt11, Brushes.Black, x + 160, y1);
            //e.Graphics.DrawRectangle(Pens.Black, x + 175, y1, 10, 10);
            //if (oushijin.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 175, y1, x + 180, y1 + 10);
            //    e.Graphics.DrawLine(pblack2, x + 180, y1 + 10, x + 185, y1);
            //}

            e.Graphics.DrawString("固定", ptzt11, Brushes.Black, x + 200, y1);
            e.Graphics.DrawString("5", ptzt11, Brushes.Black, x + 330, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 345, y1, 10, 10);
            if (guding.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 345, y1, x + 350, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 350, y1 + 10, x + 355, y1);
            }

            e.Graphics.DrawString("厌食", ptzt11, Brushes.Black, x + 365, y1);
            e.Graphics.DrawString("3", ptzt11, Brushes.Black, x + 500, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 515, y1, 10, 10);
            if (yanshi.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 515, y1, x + 520, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 520, y1 + 10, x + 525, y1);
            }

            e.Graphics.DrawString("手术时间≥2小时", ptzt11, Brushes.Black, x + 535, y1);
            e.Graphics.DrawString("5", ptzt11, Brushes.Black, x + 665, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 680, y1, 10, 10);
            if (shoushusj.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 680, y1, x + 685, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 685, y1 + 10, x + 690, y1);
            }
            y = y + 30; y1 = y + 10;
            e.Graphics.DrawLine(Pens.Black, x + 535, y + 0, x + 700, y + 0);
            e.Graphics.DrawString("药物治疗", ptzt11, Brushes.Black, x + 535, y1);
            y = y + 30; y1 = y + 10;
            e.Graphics.DrawLine(Pens.Black, x + 535, y + 0, x + 700, y + 0);
            e.Graphics.DrawString("使用类固醇、细胞毒", ptzt11, Brushes.Black, x + 535, y1);
            e.Graphics.DrawString("性药物、大计量消炎", ptzt11, Brushes.Black, x + 535, y1 + 15);
            y = y + 30; y1 = y + 10;
            //e.Graphics.DrawLine(Pens.Black, x + 535, y + 0, x + 700, y + 0);
            e.Graphics.DrawString("药", ptzt11, Brushes.Black, x + 535, y1);
            e.Graphics.DrawString("4", ptzt11, Brushes.Black, x + 665, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 680, y1, 10, 10);
            if (yaowu.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 680, y1, x + 685, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 685, y1 + 10, x + 690, y1);
            }

            y = y + 30; y1 = y + 10;
            e.Graphics.DrawLine(Pens.Black, x + 25, y + 0, x + 700, y + 0);
            e.Graphics.DrawString("总评分： " + zongpinfen.Text+" 分", ptzt11, Brushes.Black, x + 30, y1);
            e.Graphics.DrawString("评估护士签名：" + comboBpghs.Text, ptzt11, Brushes.Black, x + 365, y1);
            y = y + 30; y1 = y + 10;
            e.Graphics.DrawLine(Pens.Black, x + 25, y + 0, x + 700, y + 0);
            e.Graphics.DrawLine(Pens.Black, x + 260, y, x + 260, y + 150);
            e.Graphics.DrawString("10-14分轻度危险", ptzt11, Brushes.Black, x + 30, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 200, y1, 10, 10);
            if (weixian1.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 200, y1, x + 205, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 205, y1 + 10, x +210, y1);
            }

            e.Graphics.DrawString("预防措施：", ptzt11, Brushes.Black, x + 260, y1);
            y = y + 30; y1 = y + 10;
            //e.Graphics.DrawLine(Pens.Black, x + 25, y + 0, x + 700, y + 0);
            //e.Graphics.DrawLine(Pens.Black, x + 350, y, x + 350, y + 150);
            e.Graphics.DrawString("15-19分高度危险", ptzt11, Brushes.Black, x + 30, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 200, y1, 10, 10);
            if (weixian2.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 200, y1, x + 205, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 205, y1 + 10, x + 210, y1);
            }

            e.Graphics.DrawString("凝胶垫", ptzt11, Brushes.Black, x + 260, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 310, y1, 10, 10);
            if (ningjiaodian.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x +310, y1, x + 315, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 315, y1 + 10, x + 320, y1);
            }
            e.Graphics.DrawString("聚氨酯泡沫敷料", ptzt11, Brushes.Black, x + 330, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 450, y1, 10, 10);
            if (jajpmfl.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 450, y1, x + 455, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 455, y1 + 10, x + 460, y1);
            }

            e.Graphics.DrawString("胶体敷料", ptzt11, Brushes.Black, x + 470, y1);
            e.Graphics.DrawRectangle(Pens.Black, x +540, y1, 10, 10);
            if (zhitifuliao.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 540, y1, x + 545, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 545, y1 + 10, x + 550, y1);
            }
            e.Graphics.DrawString("体位垫", ptzt11, Brushes.Black, x + 560, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 610, y1, 10, 10);
            if (tiweidian.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 610, y1, x + 615, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 615, y1 + 10, x + 620, y1);
            }

            y = y + 30; y1 = y + 10;
            //e.Graphics.DrawLine(Pens.Black, x + 25, y + 0, x + 700, y + 0);
            //e.Graphics.DrawLine(Pens.Black, x + 350, y, x + 350, y + 150);
            e.Graphics.DrawString("大于等于20分极度危险", ptzt11, Brushes.Black, x + 30, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 200, y1, 10, 10);
            if (weixian3.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 200, y1, x + 205, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 205, y1 + 10, x + 210, y1);
            }

            e.Graphics.DrawString("位置：头面部", ptzt11, Brushes.Black, x + 260, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 360, y1, 10, 10);
            if (toumianbu.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 360, y1, x + 365, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 365, y1 + 10, x + 370, y1);
            }

            e.Graphics.DrawString("耳廓", ptzt11, Brushes.Black, x + 380, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 420, y1, 10, 10);
            if (erkuo.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 420, y1, x + 425, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 425, y1 + 10, x + 430, y1);
            }

            e.Graphics.DrawString("肩胛骨", ptzt11, Brushes.Black, x + 440, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 500, y1, 10, 10);
            if (jianjiagu.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 500, y1, x + 505, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 505, y1 + 10, x + 510, y1);
            }
            e.Graphics.DrawString("肘部", ptzt11, Brushes.Black, x + 520, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 560, y1, 10, 10);
            if (zuobu.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 560, y1, x + 565, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 565, y1 + 10, x + 570, y1);
            }

            e.Graphics.DrawString("骶尾部", ptzt11, Brushes.Black, x + 580, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 640, y1, 10, 10);
            if (diweibu.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 640, y1, x + 645, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 645, y1 + 10, x + 650, y1);
            }

            y = y + 30; y1 = y + 10;
            //e.Graphics.DrawLine(Pens.Black, x + 25, y + 0, x + 700, y + 0);
            //e.Graphics.DrawLine(Pens.Black, x + 350, y, x + 350, y + 150);
            //e.Graphics.DrawString("大于20分极度危险", ptzt11, Brushes.Black, x + 30, y1);
            //e.Graphics.DrawRectangle(Pens.Black, x + 200, y1, 10, 10);
            //if (weixian3.Checked == true)
            //{
            //    e.Graphics.DrawLine(pblack2, x + 200, y1, x + 205, y1 + 10);
            //    e.Graphics.DrawLine(pblack2, x + 205, y1 + 10, x + 210, y1);
            //}

            e.Graphics.DrawString("坐骨", ptzt11, Brushes.Black, x + 260, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 310, y1, 10, 10);
            if (zuogu.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 310, y1, x + 315, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 315, y1 + 10, x + 320, y1);
            }
            e.Graphics.DrawString("股骨粗隆", ptzt11, Brushes.Black, x + 330, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 400, y1, 10, 10);
            if (gugucujiang.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 400, y1, x + 405, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 405, y1 + 10, x + 410, y1);
            }
            e.Graphics.DrawString("膝部", ptzt11, Brushes.Black, x +420, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 460, y1, 10, 10);
            if (qibu.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 460, y1, x + 465, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 465, y1 + 10, x + 470, y1);
            }
            e.Graphics.DrawString("足跟", ptzt11, Brushes.Black, x + 480, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 520, y1, 10, 10);
            if (zugen.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 520, y1, x + 525, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 525, y1 + 10, x + 530, y1);
            }
            e.Graphics.DrawString("踝部", ptzt11, Brushes.Black, x + 560, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 600, y1, 10, 10);
            if (huaibu.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 600, y1, x + 605, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 605, y1 + 10, x + 610, y1);
            }
            e.Graphics.DrawString("髂骨", ptzt11, Brushes.Black, x + 620, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 660, y1, 10, 10);
            if (qiagu.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 660, y1, x + 665, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 665, y1 + 10, x + 670, y1);
            }

            y = y + 60; y1 = y + 20;
            e.Graphics.DrawLine(Pens.Black, x + 25, y + 0, x + 700, y + 0);
            //e.Graphics.DrawLine(Pens.Black, x + 25, y + 0, x + 700, y + 0);
            //e.Graphics.DrawLine(Pens.Black, x + 350, y, x + 350, y + 150);
            e.Graphics.DrawString("评价：术后皮肤      完整", ptzt11, Brushes.Black, x + 30, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 220, y1, 10, 10);
            if (wanzheng.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 220, y1, x + 225, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 225, y1 + 10, x + 230, y1);
            }
            e.Graphics.DrawString("不完整", ptzt11, Brushes.Black, x + 260, y1);
            e.Graphics.DrawRectangle(Pens.Black, x + 310, y1, 10, 10);
            if (buwanzheng.Checked == true)
            {
                e.Graphics.DrawLine(pblack2, x + 310, y1, x + 315, y1 + 10);
                e.Graphics.DrawLine(pblack2, x + 315, y1 + 10, x + 320, y1);
            }
            e.Graphics.DrawString("（详见手术患者交接记录单）", ptzt11, Brushes.Black, x + 330, y1);
            y = y + 50; y1 = y + 10;
            e.Graphics.DrawLine(Pens.Black, x + 25, y + 0, x + 700, y + 0);
        }

        
       
    }
}
