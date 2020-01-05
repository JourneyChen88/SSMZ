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


namespace main.CGG
{
    public partial class CHAShouShuFXPG : Form
    {

        adims_BLL.YXL_BLL bll = new adims_BLL.YXL_BLL();
        adims_DAL.YXL_DAL dal = new adims_DAL.YXL_DAL();
        AdimsProvider dall = new AdimsProvider();
        string patid = "";

        public CHAShouShuFXPG(string patID)
        {
            patid = patID;
            InitializeComponent();
        }

        private void CHAShouShuFXPG_Load(object sender, EventArgs e)
        {
            this.dateTimeP.Format = DateTimePickerFormat.Custom;
            this.dateTimeP.CustomFormat = "yyyy-MM-dd HH:mm";
            DataTable dtt = dall.GetMZname();
            txtbMZFS.Items.Clear();
            foreach (DataRow dr in dtt.Rows)
            {
                txtbMZFS.Items.Add(dr["name"].ToString());
            }

            
            
            this.Text = "CHA手术风险评估表";
            #region 病人信息
            try
            {
                //病人新
                DataTable dt = bll.SelectPatInfo111(patid);
                if (dt.Rows.Count > 0)
                {

                    txtbKeShi.Text = dt.Rows[0]["Patdpm"].ToString();
                    txtbChuangHao.Text = dt.Rows[0]["patbedno"].ToString();
                    txtbZYH.Text = dt.Rows[0]["PatID"].ToString();
                    txtbAge.Text = dt.Rows[0]["Patage"].ToString();
                    txtbSex.Text = dt.Rows[0]["Patsex"].ToString();
                    txtbHZName.Text = dt.Rows[0]["Patname"].ToString();
                    txtbMZFS.Text = dt.Rows[0]["Amethod"].ToString();
                    txtbMZSSMC.Text = dt.Rows[0]["Oname"].ToString();
                }
                //初始化数据
                dt =dal.GetPenpleInfofengxianpg(patid);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    dateTimeP.Value = Convert.ToDateTime(dr["shoushuriqi"]);
                    if (Convert.ToString(dr["baochunckes"]).Contains("1")) checkI.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("2")) checkII.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("3")) checkIII.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("4")) chkIV.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("5")) checkp1.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("6")) checkp2.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("7")) checkp3.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("8")) checkp4.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("9")) checkp5.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("a")) checkp6.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("b")) checkt1.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("c")) checkt2.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("d")) checkjj.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("e")) ceckyj.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("f")) checkbj.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("g")) chkckqt.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("h")) chkqcgr.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("i")) chkscgr.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("j")) chkqj.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("k")) chkjm.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("l")) chkgm.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("m")) nnisfj0.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("n")) nnisfj1.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("o")) nnis2.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("p")) nnis3.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("q")) qczzss.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("r")) sczzss.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("s")) qgss.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("t")) qxss.Checked = true;
                    if (Convert.ToString(dr["baochunckes"]).Contains("u")) checkjzss.Checked = true;
                  
                    combokssysqm.Text = dr["shoushuyisheng"].ToString();
                    commzysqm.Text = dr["mazuiyisheng"].ToString();
                    combokhsqm.Text = dr["xvhuihushi"].ToString();
                    txtbssqjcd.Text = dr["shoushuqingjiedu"].ToString();
                    txetasa.Text = dr["mazuiasa"].ToString();
                    textsscxsj.Text = dr["sscxsjf"].ToString();
                    defen.Text = dr["defen"].ToString();
                    comboxssdj.Text = dr["ssdj"].ToString();
                    txtbMZFS.Text = dr["mazfangshi"].ToString();
                    txtbMZSSMC.Text = dr["shishissmc"].ToString();
                }
              }
            catch { }

            #endregion
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboxssdj.Text == "")
                {
                    MessageBox.Show("手术等级不能为空");
                    return;
                }
                baocun();
            }
            catch { }

        }
        //打印

        private void baocun()
        {
             try
            {
            Dictionary<string, string> SQF = new Dictionary<string, string>();
            int result = 0;


            SQF.Add("shoushuriqi", Convert.ToDateTime(dateTimeP.Value.ToString()).ToString("yyyy-MM-dd HH:mm"));
            SQF.Add("ZhuYuanhao", patid);

            string AddItem = "";
            AddItem = "";
            if (checkI.Checked) AddItem += "1";
            if (checkII.Checked) AddItem += "2";
            if (checkIII.Checked) AddItem += "3";
            if (chkIV.Checked) AddItem += "4";
            if (checkp1.Checked) AddItem += "5";
            if (checkp2.Checked) AddItem += "6";
            if (checkp3.Checked) AddItem += "7";
            if (checkp4.Checked) AddItem += "8";
            if (checkp5.Checked) AddItem += "9";
            if (checkp6.Checked) AddItem += "a";
            if (checkt1.Checked) AddItem += "b";
            if (checkt2.Checked) AddItem += "c";
            if (checkjj.Checked) AddItem += "d";
            if (ceckyj.Checked) AddItem += "e";
            if (checkbj.Checked) AddItem += "f";
            if (chkckqt.Checked) AddItem += "g";
            if (chkqcgr.Checked) AddItem += "h";
            if (chkscgr.Checked) AddItem += "i";
            if (chkqj.Checked) AddItem += "j";
            if (chkjm.Checked) AddItem += "k";
            if (chkgm.Checked) AddItem += "l";
            if (nnisfj0.Checked) AddItem += "m";
            if (nnisfj1.Checked) AddItem += "n";
            if (nnis2.Checked) AddItem += "o";
            if (nnis3.Checked) AddItem += "p";
            if (qczzss.Checked) AddItem += "q";
            if (sczzss.Checked) AddItem += "r";
            if (qgss.Checked) AddItem += "s";
            if (qxss.Checked) AddItem += "t";
            if (checkjzss.Checked) AddItem += "u";
            SQF.Add("baochunckes", AddItem);

            SQF.Add("shoushuyisheng", combokssysqm.Text);
            SQF.Add("mazuiyisheng", commzysqm.Text);
            SQF.Add("xvhuihushi", combokhsqm.Text);
            SQF.Add("shoushuqingjiedu", txtbssqjcd.Text);

            SQF.Add("mazuiasa", txetasa.Text);
            SQF.Add("sscxsjf", textsscxsj.Text);
            SQF.Add("defen", defen.Text);
            SQF.Add("ssdj", comboxssdj.Text);
            SQF.Add("mazfangshi", txtbMZFS.Text);
            SQF.Add("shishissmc", txtbMZSSMC.Text);
          //  comboxssdj
            DataTable dt = dal.GetPenpleInfofengxianpg(patid);

            if (dt.Rows.Count > 0)
            {
                result = dal.Updatefengxianpg(SQF);
                MessageBox.Show("修改成功");
            }
            else
            {
                result = dal.TljInfengxianpinggu(SQF);
                MessageBox.Show("保存成功！");
            }
            }
             catch { }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            #region

            #endregion
            this.printDocument1.PrintPage += new PrintPageEventHandler(this.MyPrintDocument_PrintPage);
            //将写好的格式给打印预览控件以便预览
            printPreviewDialog1.Document = printDocument1;
            //显示打印预览
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printDocument1.DefaultPageSettings.PaperSize =
                      new System.Drawing.Printing.PaperSize("A4", 820, 1160);
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            printDocument1.DefaultPageSettings.Landscape = true;
            DialogResult result = printPreviewDialog1.ShowDialog();
            if (result == DialogResult.OK)
                this.printDocument1.Print();
        }

        private void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font ptzt9 = new Font("微软雅黑", 9);//普通字体  
            Font ptzt10 = new Font("微软雅黑", 10);//普通字体 
            Font ptzt11 = new Font("宋体", 11);//普通字体 
            Font ptzt13 = new Font("微软雅黑", 13);//普通字体 
            Font ptzt18 = new Font("微软雅黑", 18);//普通字体   


            int y = 40; int x = 30;
            string title1 = "CHA手术风险评估";
            e.Graphics.DrawString(title1, ptzt18, Brushes.Black, new Point(x + 400, y - 10));

            x = x + 10; y = y + 40;
            e.Graphics.DrawString("日期:" + dateTimeP.Value.ToString("yyyy-MM-dd HH:mm"), ptzt11, Brushes.Black, new Point(x + 25, y));
            e.Graphics.DrawString("科室:" + txtbKeShi.Text, ptzt11, Brushes.Black, new Point(x + 220, y));
            e.Graphics.DrawString("床号:" + txtbChuangHao.Text, ptzt11, Brushes.Black, new Point(x + 450, y));
            e.Graphics.DrawString("住院号:" + txtbZYH.Text, ptzt11, Brushes.Black, new Point(x + 600, y));
            e.Graphics.DrawString("年龄:" + txtbAge.Text, ptzt11, Brushes.Black, new Point(x + 750, y));
            e.Graphics.DrawString("性别:" + txtbSex.Text, ptzt11, Brushes.Black, new Point(x + 900, y));
            y = y + 30;
            e.Graphics.DrawString("姓名:" + txtbHZName.Text, ptzt11, Brushes.Black, new Point(x + 25, y));
            e.Graphics.DrawString("麻醉方式:" + txtbMZFS.Text, ptzt11, Brushes.Black, new Point(x + 220, y));
            e.Graphics.DrawString("手术实施名称:" + txtbMZSSMC.Text, ptzt11, Brushes.Black, new Point(x + 500, y));

            y = y + 30;
            //横线
            e.Graphics.DrawLine(Pens.Black, x, y + 0, x + 1070, y + 0);
            e.Graphics.DrawLine(Pens.Black, x, y + 40, x + 1070, y + 40);
            e.Graphics.DrawLine(Pens.Black, x, y + 80, x + 1070, y + 80);
            e.Graphics.DrawLine(Pens.Black, x + 350, y + 120, x + 1070, y + 120);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 160, x + 700, y + 160);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 200, x + 700, y + 200);
            e.Graphics.DrawLine(Pens.Black, x + 350, y + 240, x + 700, y + 240);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 280, x + 700, y + 280);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 320, x + 700, y + 320);
            e.Graphics.DrawLine(Pens.Black, x + 350, y + 360, x + 700, y + 360);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 400, x + 700, y + 400);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 440, x + 1070, y + 440);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 480, x + 1070, y + 480);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 520, x + 1070, y + 520);
            e.Graphics.DrawLine(Pens.Black, x + 0, y + 560, x + 1070, y + 560);


            ////竖线

            e.Graphics.DrawLine(Pens.Black, x, y + 0, x, y + 558);
            e.Graphics.DrawLine(Pens.Black, x + 350, y + 0, x + 350, y + 519);
            e.Graphics.DrawLine(Pens.Black, x + 700, y - 0, x + 700, y + 519);
            e.Graphics.DrawLine(Pens.Black, x + 1070, y - 0, x + 1070, y + 559);

            e.Graphics.DrawString("1、手术切口清洁度 ", ptzt13, Brushes.Black, new Point(x + 70, y + 5));
            e.Graphics.DrawString("2、麻醉分级（ASA分级）", ptzt13, Brushes.Black, new Point(x + 420, y + 5));
            e.Graphics.DrawString("3、手术持续时间", ptzt13, Brushes.Black, new Point(x + 800, y + 5));
            y = y + 40;
            e.Graphics.DrawString("I类手术切口（清洁手术） ", ptzt11, Brushes.Black, new Point(x + 0, y + 15));
            e.Graphics.DrawLine(Pens.Black, x + 300, y + 0, x + 300, y + 40);
            if (checkI.Checked)
            {
                e.Graphics.DrawString("0 ✔", ptzt11, Brushes.Black, new Point(x + 320, y + 15));
            }
            else
            {
                e.Graphics.DrawString("0", ptzt11, Brushes.Black, new Point(x + 320, y + 15));
            }
            e.Graphics.DrawString("手术野无污染：手术切口周边无炎症；\n患者没有进行气道、食道和/或尿道插管；\n患者没有意识障碍。", ptzt11, Brushes.Black, new Point(x + 0, y + 55));
            e.Graphics.DrawString("Ⅱ类手术切口（相对清洁手术）", ptzt11, Brushes.Black, new Point(x + 0, y + 135));
            e.Graphics.DrawLine(Pens.Black, x + 300, y + 120, x + 300, y + 160);
            if (checkII.Checked)
            {
                e.Graphics.DrawString("0 ✔", ptzt11, Brushes.Black, new Point(x + 320, y + 135));
                
            }
            else
            {
                e.Graphics.DrawString("0", ptzt11, Brushes.Black, new Point(x + 320, y + 135));
              
            }
           
            e.Graphics.DrawString("上、下呼吸道，上、下消化道，泌尿生殖道或经\n以上器官的手术；患者进行气道、食道和/或尿道\n插管；患者病情稳定；行胆囊、阴道、阑尾、耳\n鼻手术的患者", ptzt11, Brushes.Black, new Point(x + 0, y + 170));
            e.Graphics.DrawString("Ⅲ类手术切口（清洁-污染手术）", ptzt11, Brushes.Black, new Point(x + 0, y + 250));
            e.Graphics.DrawLine(Pens.Black, x + 300, y + 240, x + 300, y + 280);
            if (checkIII.Checked)
            {
                e.Graphics.DrawString("1 ✔", ptzt11, Brushes.Black, new Point(x + 320, y + 250));

            }
            else
            {
                e.Graphics.DrawString("1", ptzt11, Brushes.Black, new Point(x + 320, y + 250));

            }
            
            e.Graphics.DrawString("开放、新鲜且不干净的伤口；\n前次手术后感染的切口；\n手术中需采取消毒措施的切口。", ptzt11, Brushes.Black, new Point(x + 0, y + 295));
            e.Graphics.DrawString("Ⅳ类手术切口（污染手术）", ptzt11, Brushes.Black, new Point(x + 0, y + 375));
            e.Graphics.DrawLine(Pens.Black, x + 300, y + 360, x + 300, y + 400);
            if (chkIV.Checked)
            {
                e.Graphics.DrawString("1 ✔", ptzt11, Brushes.Black, new Point(x + 320, y + 375));

            }
            else
            {
                e.Graphics.DrawString("1", ptzt11, Brushes.Black, new Point(x + 320, y + 375));

            }
            
            e.Graphics.DrawString("严重的外伤，手术切口有炎症、组织坏死，\n或有内脏引流管。", ptzt11, Brushes.Black, new Point(x + 0, y + 405));
            e.Graphics.DrawString("手术医生签名：" + combokssysqm.Text, ptzt11, Brushes.Black, new Point(x + 0, y + 450));


            e.Graphics.DrawString("P1：正常的患者；除局部病变外，\n无系统性疾病。", ptzt11, Brushes.Black, new Point(x + 350, y + 5));
            e.Graphics.DrawString("P2：患者有轻微的临床症状；\n有轻度或中度系统性疾病。", ptzt11, Brushes.Black, new Point(x + 350, y + 45));
            e.Graphics.DrawString("P3：有严重系统性疾病，日常活动受限，\n但未丧失工作能力。", ptzt11, Brushes.Black, new Point(x + 350, y + 85));
            e.Graphics.DrawString("P4：有严重系统性疾病，已丧失工作能力，\n威胁生命安全。", ptzt11, Brushes.Black, new Point(x + 350, y + 125));
            e.Graphics.DrawString("P5：病情危重，生命难以维持的濒死病人。", ptzt11, Brushes.Black, new Point(x + 350, y + 175));
            e.Graphics.DrawString("P6：脑死亡的患者", ptzt11, Brushes.Black, new Point(x + 350, y + 215));
            e.Graphics.DrawLine(Pens.Black, x + 650, y + 0, x + 650, y + 240);
           // e.Graphics.DrawString(txtb21.Text, ptzt11, Brushes.Black, new Point(x + 660, y + 5));
            if (checkp1.Checked)
            {
                e.Graphics.DrawString("0 ✔", ptzt11, Brushes.Black, new Point(x + 660, y + 5));

            }
            else
            {
                e.Graphics.DrawString("0", ptzt11, Brushes.Black, new Point(x + 660, y + 5));

            }
            if (checkp2.Checked)
            {
                e.Graphics.DrawString("0 ✔", ptzt11, Brushes.Black, new Point(x + 660, y + 45));

            }
            else
            {
                e.Graphics.DrawString("0", ptzt11, Brushes.Black, new Point(x + 660, y + 45));

            }
            if (checkp3.Checked)
            {
                e.Graphics.DrawString("1 ✔", ptzt11, Brushes.Black, new Point(x + 660, y + 85));

            }
            else
            {
                e.Graphics.DrawString("1", ptzt11, Brushes.Black, new Point(x + 660, y + 85));

            }
            if (checkp4.Checked)
            {
                e.Graphics.DrawString("1 ✔", ptzt11, Brushes.Black, new Point(x + 660, y + 125));

            }
            else
            {
                e.Graphics.DrawString("1", ptzt11, Brushes.Black, new Point(x + 660, y + 125));

            }
            if (checkp5.Checked)
            {
                e.Graphics.DrawString("1 ✔", ptzt11, Brushes.Black, new Point(x + 660, y + 175));

            }
            else
            {
                e.Graphics.DrawString("1", ptzt11, Brushes.Black, new Point(x + 660, y + 175));

            }
            if (checkp6.Checked)
            {
                e.Graphics.DrawString("1 ✔", ptzt11, Brushes.Black, new Point(x + 660, y + 215));

            }
            else
            {
                e.Graphics.DrawString("1", ptzt11, Brushes.Black, new Point(x + 660, y + 215));

            }
            //e.Graphics.DrawString(txtb22.Text, ptzt11, Brushes.Black, new Point(x + 660, y + 45));
            //e.Graphics.DrawString(txtb23.Text, ptzt11, Brushes.Black, new Point(x + 660, y + 85));
            //e.Graphics.DrawString(txtb24.Text, ptzt11, Brushes.Black, new Point(x + 660, y + 125));
            //e.Graphics.DrawString(txtb25.Text, ptzt11, Brushes.Black, new Point(x + 660, y + 175));
            //e.Graphics.DrawString(txtb26.Text, ptzt11, Brushes.Black, new Point(x + 660, y + 215));

            e.Graphics.DrawString("4、手术类别", ptzt13, Brushes.Black, new Point(x + 420, y + 250));
            e.Graphics.DrawString("1、浅层组织手术", ptzt11, Brushes.Black, new Point(x + 350, y + 290));
            e.Graphics.DrawString("2、深部组织手术", ptzt11, Brushes.Black, new Point(x + 350, y + 330));
            e.Graphics.DrawString("3、器官手术", ptzt11, Brushes.Black, new Point(x + 350, y + 370));
            e.Graphics.DrawString("4、腔隙手术", ptzt11, Brushes.Black, new Point(x + 350, y + 410));
            e.Graphics.DrawLine(Pens.Black, x + 650, y + 280, x + 650, y + 440);
            if (qczzss.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 660, y + 290));
            if (sczzss.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 660, y + 330));
            if (qgss.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 660, y + 370));
            if (qxss.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 660, y + 410));

            e.Graphics.DrawString(" 麻醉医师签名：" + commzysqm.Text, ptzt11, Brushes.Black, new Point(x + 350, y + 450));

            e.Graphics.DrawString("T1：手术在3小时内完成", ptzt11, Brushes.Black, new Point(x + 710, y + 15));
            e.Graphics.DrawString("T2：完成手术，超过3小时", ptzt11, Brushes.Black, new Point(x + 710, y + 55));
            e.Graphics.DrawLine(Pens.Black, x + 1020, y + 0, x + 1020, y + 80);
            if (checkt1.Checked)
            {
                e.Graphics.DrawString("0 ✔", ptzt11, Brushes.Black, new Point(x + 1030, y + 15));

            }
            else
            {
                e.Graphics.DrawString("0", ptzt11, Brushes.Black, new Point(x + 1030, y + 15));

            }
            if (checkt2.Checked)
            {
                e.Graphics.DrawString("1 ✔", ptzt11, Brushes.Black, new Point(x + 1030, y + 55));

            }
            else
            {
                e.Graphics.DrawString("1", ptzt11, Brushes.Black, new Point(x + 1030, y + 55));

            }
             e.Graphics.DrawString("随访：切口愈合与感染情况", ptzt11, Brushes.Black, new Point(x + 710, y + 95));
            e.Graphics.DrawString("切口愈合:   □甲级   □乙级   □丙级   □其他", ptzt11, Brushes.Black, new Point(x + 710, y + 135));
            if (checkjj.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 805, y + 135));
            if (ceckyj.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 874, y + 135));
            if (checkbj.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 950, y + 135));
            if (chkckqt.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 1015, y + 135));



            e.Graphics.DrawString("切口感染: □浅层感染        □深层感染", ptzt11, Brushes.Black, new Point(x + 710, y + 175));
            if (chkqcgr.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 788, y + 175));
            if (chkscgr.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 932, y + 175));

            e.Graphics.DrawString("在与评价项目相应的框内“□”打勾“√”后，\n分值相加即可完成！", ptzt11, Brushes.Black, new Point(x + 710, y + 215));

            if (chkqj.Checked)
            {
                e.Graphics.DrawString("备皮方式:    1:清洁\t      ☑", ptzt11, Brushes.Black, new Point(x + 710, y + 255));
            }
            else e.Graphics.DrawString("备皮方式:    1:清洁\t      □", ptzt11, Brushes.Black, new Point(x + 710, y + 255));

            if (chkjm.Checked)
            {
                e.Graphics.DrawString(" 2:剪毛\t\t  ☑", ptzt11, Brushes.Black, new Point(x + 805, y + 295));
            }
            else e.Graphics.DrawString(" 2:剪毛\t\t  □", ptzt11, Brushes.Black, new Point(x + 805, y + 295));

            if (chkgm.Checked)
            {
                e.Graphics.DrawString(" 3:术前2小时刮毛  ☑ ", ptzt11, Brushes.Black, new Point(x + 805, y + 335));
            }
            else
            {
                e.Graphics.DrawString(" 3:术前2小时刮毛  □ ", ptzt11, Brushes.Black, new Point(x + 805, y + 335));
            }
            string cvb="";
            if (checkjzss.Checked)
            {
                cvb = "✔";
            }
            
            e.Graphics.DrawString("急诊手术：" + cvb, ptzt11, Brushes.Black, new Point(x + 710, y + 415));

            e.Graphics.DrawString("巡回护士签名：" + combokhsqm.Text, ptzt11, Brushes.Black, new Point(x + 710, y + 455));

            e.Graphics.DrawString("手术风险评估：手术切口清洁程度（    分）+麻醉ASA分级（     分）+手术持续时间（     分）=   分，  NNIS分级： 0-□  1-□  2-□  3-□", ptzt11, Brushes.Black, new Point(x + 0, y + 495));
            if (true) e.Graphics.DrawString(txtbssqjcd.Text, ptzt11, Brushes.Black, new Point(x + 250, y + 495));
            if (true) e.Graphics.DrawString(txetasa.Text, ptzt11, Brushes.Black, new Point(x + 425, y + 495));
            if (true) e.Graphics.DrawString(textsscxsj.Text, ptzt11, Brushes.Black, new Point(x + 615, y + 495));

            int num_temp = 0;
            try
            {
                num_temp = Int32.Parse(txtbssqjcd.Text) + Int32.Parse(txetasa.Text) + Int32.Parse(textsscxsj.Text);
            }
            catch { }

            e.Graphics.DrawString(num_temp.ToString(), ptzt11, Brushes.Black, new Point(x + 700, y + 495));

            if (nnisfj0.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 870, y + 495));
            if (nnisfj1.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 910, y + 495));
            if (nnis2.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 960, y + 495));
            if (nnis3.Checked) e.Graphics.DrawString("✔", ptzt11, Brushes.Black, new Point(x + 1010, y + 495));




        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkI_CheckedChanged(object sender, EventArgs e)
        {
            if (checkI.Checked == true)
            {
                txtbssqjcd.Text = "0";
                checkII.Checked = false;
                checkIII.Checked = false;
                chkIV.Checked = false;
            }
        }

        private void checkII_CheckedChanged(object sender, EventArgs e)
        {
            if (checkII.Checked == true)
            {
                txtbssqjcd.Text = "0";
                checkI.Checked = false;
                checkIII.Checked = false;
                chkIV.Checked = false;
            }
        }

        private void checkIII_CheckedChanged(object sender, EventArgs e)
        {
            if (checkIII.Checked == true)
            {
                txtbssqjcd.Text = "1";
                checkI.Checked = false;
                checkII.Checked = false;
                chkIV.Checked = false;
            }
        }

        private void chkIV_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIV.Checked == true)
            {
                txtbssqjcd.Text = "1";
                checkI.Checked = false;
                checkII.Checked = false;
                checkIII.Checked = false;
            }
        }

        private void checkp1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkp1.Checked == true)
            {
                txetasa.Text = "0";
                checkp2.Checked = false;
                checkp3.Checked = false;
                checkp4.Checked = false;
                checkp5.Checked = false;
                checkp6.Checked = false;
                
            }
        }

        private void checkp2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkp2.Checked == true)
            {
                txetasa.Text = "0";
                checkp1.Checked = false;
                checkp3.Checked = false;
                checkp4.Checked = false;
                checkp5.Checked = false;
                checkp6.Checked = false;
            }
        }

        private void checkp3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkp3.Checked == true)
            {
                txetasa.Text = "1";
                checkp1.Checked = false;
                checkp2.Checked = false;
                checkp4.Checked = false;
                checkp5.Checked = false;
                checkp6.Checked = false;
            }
        }

        private void checkp4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkp4.Checked == true)
            {
                txetasa.Text = "1";
                checkp1.Checked = false;
                checkp2.Checked = false;
                checkp3.Checked = false;
                checkp5.Checked = false;
                checkp6.Checked = false;
            }
        }

        private void checkp5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkp5.Checked == true)
            {
                txetasa.Text = "1";
                checkp1.Checked = false;
                checkp2.Checked = false;
                checkp3.Checked = false;
                checkp4.Checked = false;
                checkp6.Checked = false;
            }
        }

        private void checkp6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkp6.Checked == true)
            {
                txetasa.Text = "1";
                checkp1.Checked = false;
                checkp2.Checked = false;
                checkp3.Checked = false;
                checkp4.Checked = false;
                checkp5.Checked = false;
            }
        }

        private void checkt1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkt1.Checked == true)
            {
                textsscxsj.Text = "0";
                checkt2.Checked = false;
            }
        }

        private void checkt2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkt2.Checked == true)
            {
                textsscxsj.Text = "1";
                checkt1.Checked = false;
            }
        }

        
        private void txtbssqjcd_TextChanged(object sender, EventArgs e)
        {
            if (txtbssqjcd.Text == "")
            {
                txtbssqjcd.Text = "0";
            }
            if (txetasa.Text == "")
            {
                txetasa.Text = "0";
            }
            if (textsscxsj.Text == "")
            {
                textsscxsj.Text = "0";
            }
            defen.Text = (Convert.ToInt32(txetasa.Text) + Convert.ToInt32(txtbssqjcd.Text) + Convert.ToInt32(textsscxsj.Text)).ToString();
        }

        private void txetasa_TextChanged(object sender, EventArgs e)
        {
            if (txtbssqjcd.Text == "")
            {
                txtbssqjcd.Text = "0";
            }
            if (txetasa.Text == "")
            {
                txetasa.Text = "0";
            }
            if (textsscxsj.Text == "")
            {
                textsscxsj.Text = "0";
            }
            defen.Text = (Convert.ToInt32(txetasa.Text) + Convert.ToInt32(txtbssqjcd.Text) + Convert.ToInt32(textsscxsj.Text)).ToString();
        }

        private void textsscxsj_TextChanged(object sender, EventArgs e)
        {
            if (txtbssqjcd.Text == "")
            {
                txtbssqjcd.Text = "0";
            }
            if (txetasa.Text == "")
            {
                txetasa.Text = "0";
            }
            if (textsscxsj.Text == "")
            {
                textsscxsj.Text = "0";
            }
            defen.Text = (Convert.ToInt32(txetasa.Text) + Convert.ToInt32(txtbssqjcd.Text) + Convert.ToInt32(textsscxsj.Text)).ToString();
        }

        private void defen_TextChanged(object sender, EventArgs e)
        {
            if (defen.Text == "3")
            {
                nnisfj0.Checked = false;
                nnisfj1.Checked = false;
                nnis2.Checked = false;
                nnis3.Checked = true;
            }
            if (defen.Text == "2")
            {
                nnisfj0.Checked = false;
                nnisfj1.Checked = false;
                nnis2.Checked = true;
                nnis3.Checked = false;
            }
            if (defen.Text == "1")
            {
                nnisfj0.Checked = false;
                nnisfj1.Checked = true;
                nnis2.Checked = false;
                nnis3.Checked = false;
            }
            if (defen.Text == "0")
            {
                nnisfj0.Checked = true;
                nnisfj1.Checked = false;
                nnis2.Checked = false;
                nnis3.Checked = false;
            }
        }

      






    }
}
