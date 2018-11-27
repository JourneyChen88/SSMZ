using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main
{
    public partial class Mzzqtys : Form
    {
        string yiyuanID = "";
        public string patID = "";
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        public Mzzqtys()
        {
            InitializeComponent();
        }
        private void DataBind()
        {
            DataTable dt1 = bll.GetPaibanInfo(dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"), yiyuanID);
            dataGridView1.DataSource = dt1.DefaultView;
        }

        private void Mzzqtys_Load(object sender, EventArgs e)
        {
            yiyuanID = Program.customer.yiyuanType;
            DataBind();
            cmbMZYS.Items.Clear();
            DataTable dt1 = bll.selectUserName(1);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                cmbMZYS.Items.Add(dt1.Rows[i][0].ToString());
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
            DataBind();
            ClearEdit();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                ClearEdit();
                patID = dataGridView1.CurrentRow.Cells["patid"].Value.ToString();
                BindPatInfo();
                BindZqtysInfo();
            }
            else MessageBox.Show("请选择病人！");
        }
        private void ClearEdit()
        {
            tbPatname.Text = "";
            tbAge.Text = "";
            cmbSex.Text = "";
            cmbAgeDW.Text = "";
            tbZhuyuanID.Text = "";
            tbBedNo.Text = "";
            tbKeshi.Text = "";
            tbBingyin.Text = "";
            tbSSname.Text = "";
            cmbASA.Text = "";

            cbQM0.Checked = false;
            cbQM1.Checked = false;
            cbQM2.Checked = false;
            cbQM3.Checked = false;
            cbZZMZ0.Checked = false;
            cbZZMZ1.Checked = false;
            cbZZMZ2.Checked = false;
            cbZZMZ3.Checked = false;
            cbSJZZ.Checked = false;
            cbLHMZ.Checked = false;
            cbQTMZ.Checked = false;
            tbQTMZname.Text = "";
            cbZTyes.Checked = false;
            cbZTno.Checked = false;
            tbPatRecord.Text = "";
            cmbMZYS.Text = "";
        }
        private void BindPatInfo()
        {
            DataTable dt = bll.GetAllPaibanInfo(patID);
            tbPatname.Text = dt.Rows[0]["Patname"].ToString();
            tbAge.Text = dt.Rows[0]["Patage"].ToString();
            cmbSex.Text = dt.Rows[0]["Patsex"].ToString();
            cmbAgeDW.Text = dt.Rows[0]["ageDW"].ToString();
            tbZhuyuanID.Text = dt.Rows[0]["PatZhuYuanID"].ToString();
            tbBedNo.Text = dt.Rows[0]["patBedno"].ToString();
            tbKeshi.Text = dt.Rows[0]["Patdpm"].ToString();
            tbBingyin.Text = dt.Rows[0]["Pattmd"].ToString();
            tbSSname.Text = dt.Rows[0]["Oname"].ToString();
            dtSSdate.Value = Convert.ToDateTime(dt.Rows[0]["Odate"].ToString());
            cmbASA.Text = dt.Rows[0]["ASA"].ToString();
        }
        private void BindZqtysInfo()
        {
            DataTable dt = bll.GetMZZQTYS(patID);
            if (dt.Rows.Count>0)
            {
                string valueStr = "";
                valueStr = dt.Rows[0]["QM"].ToString();
                if (valueStr.Contains("0"))
                    cbQM0.Checked = true;
                if (valueStr.Contains("1"))
                    cbQM1.Checked = true;
                if (valueStr.Contains("2"))
                    cbQM2.Checked = true;
                if (valueStr.Contains("3"))
                    cbQM3.Checked = true;

                valueStr = dt.Rows[0]["ZZMZ"].ToString();
                if (valueStr.Contains("0"))
                    cbZZMZ0.Checked = true;
                if (valueStr.Contains("1"))
                    cbZZMZ1.Checked = true;
                if (valueStr.Contains("2"))
                    cbZZMZ2.Checked = true;
                if (valueStr.Contains("3"))
                    cbZZMZ3.Checked = true;

                valueStr = dt.Rows[0]["SJZZ"].ToString();
                if (valueStr.Contains("1"))
                    cbSJZZ.Checked = true;

                valueStr = dt.Rows[0]["LHMZ"].ToString();
                if (valueStr.Contains("1"))
                    cbLHMZ.Checked = true;

                valueStr = dt.Rows[0]["QTMZ"].ToString();
                if (valueStr.Contains("1"))
                    cbQTMZ.Checked = true;
                tbQTMZname.Text = dt.Rows[0]["QTMZname"].ToString();

                valueStr = dt.Rows[0]["ZT"].ToString();
                if (valueStr.Contains("1"))
                    cbZTyes.Checked = true;
                if (valueStr.Contains("0"))
                    cbZTno.Checked = true;
                tbPatRecord.Text = dt.Rows[0]["PatRecord"].ToString();
                dtPatRecordDate.Value = Convert.ToDateTime(dt.Rows[0]["PatRecordDate"]);
                cmbMZYS.Text = dt.Rows[0]["MZYS"].ToString();
                dtMZYSdate.Value = Convert.ToDateTime(dt.Rows[0]["MZYSdate"]);
            }
            

            
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (patID != "")
            {

                Dictionary<string, string> beforeVisit = new Dictionary<string, string>();
                int result = 0;

                try
                {
                    bll.UpdPaibanASAandBingqu(patID, cmbASA.Text,tbBingqu.Text);//修改排班表病区和ASA信息

                    beforeVisit.Add("patid", patID);
                    string valueStr = "";
                    if (cbQM0.Checked)
                        valueStr += "0";
                    if (cbQM1.Checked)
                        valueStr += "1";
                    if (cbQM2.Checked)
                        valueStr += "2";
                    if (cbQM3.Checked)
                        valueStr += "3";
                    beforeVisit.Add("QM", valueStr);
                    valueStr = "";
                    if (this.cbZZMZ0.Checked)
                        valueStr += "0";
                    if (cbZZMZ1.Checked)
                        valueStr += "1";
                    if (cbZZMZ2.Checked)
                        valueStr += "2";
                    if (cbZZMZ3.Checked)
                        valueStr += "3";
                    beforeVisit.Add("ZZMZ", valueStr);
                    valueStr = "";
                    if (this.cbSJZZ.Checked)
                        valueStr += "1";
                    else
                        valueStr += "0";
                    beforeVisit.Add("SJZZ", valueStr);
                    valueStr = "";
                    if (this.cbLHMZ.Checked)
                        valueStr += "1";
                    else
                        valueStr += "0";
                    beforeVisit.Add("LHMZ", valueStr);
                    valueStr = "";
                    if (this.cbQTMZ.Checked)
                        valueStr += "1";
                    else
                        valueStr += "0";
                    beforeVisit.Add("QTMZ", valueStr);
                    beforeVisit.Add("QTMZname", tbQTMZname.Text);
                    valueStr = "";
                    if (this.cbZTyes.Checked)
                        valueStr = "1";
                    else if (this.cbZTno.Checked)
                        valueStr = "0";
                    beforeVisit.Add("ZT", valueStr);
                    beforeVisit.Add("PatRecord", tbPatRecord.Text);
                    beforeVisit.Add("PatRecordDate", dtPatRecordDate.Value.ToString());
                    beforeVisit.Add("MZYS", cmbMZYS.Text);
                    beforeVisit.Add("MZYSdate", dtMZYSdate.Value.ToString());

                    DataTable dt = bll.GetMZZQTYS(patID);
                    if (dt.Rows.Count == 0)
                        result = bll.InsertMZZQTYS(beforeVisit);
                    else
                        result = bll.UpdateMZZQTYS(beforeVisit);

                    if (result > 0)
                    {
                        MessageBox.Show("保存成功！");
                    }
                    else
                        MessageBox.Show("保存失败！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "保存出现异常，请重试！");
                }
            }

        }

        private void btnPrintView_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("K16", 737, 1020);  
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            #region  16K纸张
            Pen ptp = Pens.Black;//普通画笔
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);

            Font ptzt = new Font("新宋体", 9);//普通字体
            Font ht10 = new Font("黑体", 10);//普通字体
            Font ptzt2 = new Font("新宋体", 12);//普通字体
            Font ptzt3 = new Font("新宋体", 13);//普通字体
            int JG = 25; int y = 50; int x = 40; int y1 = 0;
            string title1 = "江  苏  盛  泽  医  院";
            string title2 = "江苏省人民医院盛泽分院";
            string title3 = " 麻 醉 知 情 同 意 书";
            int row = 0;
            e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 200, y));
            y = y + 25; y1 = y + 17;
            e.Graphics.DrawString(title2, ptzt3, Brushes.Black, new Point(x + 200, y));
            y = y + 25; y1 = y + 17;
            e.Graphics.DrawString(title3, ptzt2, Brushes.Black, new Point(x + 200, y));

            row++;
            row++;
            //第一行

            e.Graphics.DrawString("姓名：" + tbPatname.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 35, y + 15 + JG * row, x + 110, y + 15 + JG * row);

            e.Graphics.DrawString("性别：" + cmbSex.Text.Trim(), ptzt, Brushes.Black, new Point(x + 120, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 155, y + 15 + JG * row, x + 180, y + 15 + JG * row);

            e.Graphics.DrawString("年龄：" + tbAge.Text+" "+cmbAgeDW.Text, ptzt, Brushes.Black, new Point(x + 190, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 220, y + 15 + JG * row, x + 260, y + 15 + JG * row);

            e.Graphics.DrawString("科别：" + tbKeshi.Text.Trim(), ptzt, Brushes.Black, new Point(x + 270, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 300, y + 15 + JG * row, x + 400, y + 15 + JG * row);

            e.Graphics.DrawString("病区：" + tbBingqu.Text.Trim(), ptzt, Brushes.Black, new Point(x + 410, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 440, y + 15 + JG * row, x + 520, y + 15 + JG * row);

            e.Graphics.DrawString("住院号：" + tbZhuyuanID.Text.Trim(), ptzt, Brushes.Black, new Point(x + 530, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 570, y + 15 + JG * row, x + 660, y + 15 + JG * row);

            row++;

            e.Graphics.DrawString("患者因 " + tbBingyin.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 40, y + 15 + JG * row, x + 200, y + 15 + JG * row);
            e.Graphics.DrawString("于 " + dtSSdate.Text, ptzt, Brushes.Black, new Point(x + 210, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 220, y + 15 + JG * row, x + 330, y + 15 + JG * row);
            e.Graphics.DrawString("拟行 " + tbSSname.Text, ptzt, Brushes.Black, new Point(x + 340, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 370, y + 15 + JG * row, x + 540, y + 15 + JG * row);
            e.Graphics.DrawString("患者ASA分级 " + cmbASA.Text, ptzt, Brushes.Black, new Point(x + 550, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 620, y + 15 + JG * row, x + 660, y + 15 + JG * row);

            row++;

            e.Graphics.DrawString("拟行麻醉方案为：全身麻醉（□喉罩；□气管插管；□支气管插管；□其他）；", ptzt, Brushes.Black, new Point(x, y + JG * row));
            if (cbQM0.Checked)
                e.Graphics.DrawString("                          ✔", ptzt, Brushes.Black, new Point(x, y + JG * row));
            if (cbQM1.Checked)
                e.Graphics.DrawString("                                  ✔", ptzt, Brushes.Black, new Point(x, y + JG * row));
            if (cbQM2.Checked)
                e.Graphics.DrawString("                                              ✔", ptzt, Brushes.Black, new Point(x, y + JG * row));
            if (this.cbQM3.Checked)
                e.Graphics.DrawString("                                                            ✔", ptzt, Brushes.Black, new Point(x, y + JG * row));
            row++;
            e.Graphics.DrawString("椎管内阻滞麻醉（□腰麻；□硬膜外；□腰硬联合；□骶麻） □神经阻滞； □联合麻醉；", ptzt, Brushes.Black, new Point(x, y + JG * row));
            if (this.cbZZMZ0.Checked)
                e.Graphics.DrawString("                ✔", ptzt, Brushes.Black, new Point(x, y + JG * row));
            if (cbZZMZ1.Checked)
                e.Graphics.DrawString("                        ✔", ptzt, Brushes.Black, new Point(x, y + JG * row));
            if (cbZZMZ2.Checked)
                e.Graphics.DrawString("                                  ✔", ptzt, Brushes.Black, new Point(x, y + JG * row));
            if (this.cbZZMZ3.Checked)
                e.Graphics.DrawString("                                              ✔", ptzt, Brushes.Black, new Point(x, y + JG * row));
            if (this.cbSJZZ.Checked)
                e.Graphics.DrawString("                                                       ✔", ptzt, Brushes.Black, new Point(x, y + JG * row));
            if (this.cbLHMZ.Checked)
                e.Graphics.DrawString("                                                                    ✔", ptzt, Brushes.Black, new Point(x, y + JG * row));
           

            row++;
            //第四行

            e.Graphics.DrawString("□其他：" +tbQTMZname.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 50, y + 15 + JG * row, x + 200, y + 15 + JG * row);
            if (this.cbQTMZ.Checked)
                e.Graphics.DrawString("✔", ptzt, Brushes.Black, new Point(x, y + JG * row));
            else
                e.Graphics.DrawLine(Pens.Black, x + 80, y + 4 + JG * row, x + 110, y + 14 + JG * row);
            e.Graphics.DrawString("术后病人镇痛（□是  □否）", ptzt, Brushes.Black, new Point(x + 230, y + JG * row));
            if (this.cbZTyes.Checked)
                e.Graphics.DrawString("              ✔", ptzt, Brushes.Black, new Point(x + 230, y + JG * row));
            if (this.cbZTno.Checked)
                e.Graphics.DrawString("                    ✔", ptzt, Brushes.Black, new Point(x + 230, y + JG * row));
            
            
            row++;
            row++;
            
            List<string> strJWS = StringCut(lb1.Text, 48);
            for (int i = 0; i < strJWS.Count; i++)
            {
                e.Graphics.DrawString(strJWS[i], ptzt, Brushes.Black, new Point(x, y + JG * row));
                 row++;
            }
           
            strJWS = StringCut(lb2.Text, 50);
            for (int i = 0; i < strJWS.Count; i++)
            {
                e.Graphics.DrawString(strJWS[i], ht10, Brushes.Black, new Point(x, y + JG * row));
                 row++;
            }
            strJWS = StringCut(lb3.Text, 50);
            for (int i = 0; i < strJWS.Count; i++)
            {
                e.Graphics.DrawString(strJWS[i], ptzt, Brushes.Black, new Point(x, y + JG * row));
                row++;
            }
            strJWS = StringCut(lb4.Text, 50);
            for (int i = 0; i < strJWS.Count; i++)
            {
                e.Graphics.DrawString(strJWS[i], ptzt, Brushes.Black, new Point(x, y + JG * row));
                row++;
            }
            strJWS = StringCut(lb5.Text, 50);
            for (int i = 0; i < strJWS.Count; i++)
            {
                e.Graphics.DrawString(strJWS[i], ptzt, Brushes.Black, new Point(x, y + JG * row));
                row++;
            }
            strJWS = StringCut(lb6.Text, 50);
            for (int i = 0; i < strJWS.Count; i++)
            {
                e.Graphics.DrawString(strJWS[i], ptzt, Brushes.Black, new Point(x, y + JG * row));
                row++;
            }
            strJWS = StringCut(lb7.Text, 50);
            for (int i = 0; i < strJWS.Count; i++)
            {
                e.Graphics.DrawString(strJWS[i], ptzt, Brushes.Black, new Point(x, y + JG * row));
                row++;
            }
            strJWS = StringCut(lb8.Text, 50);
            for (int i = 0; i < strJWS.Count; i++)
            {
                e.Graphics.DrawString(strJWS[i], ptzt, Brushes.Black, new Point(x, y + JG * row));
                row++;
            }
            strJWS = StringCut(lb9.Text, 50);
            for (int i = 0; i < strJWS.Count; i++)
            {
                e.Graphics.DrawString(strJWS[i], ptzt, Brushes.Black, new Point(x, y + JG * row));
                row++;
            }
            strJWS = StringCut(lb10.Text, 50);
            for (int i = 0; i < strJWS.Count; i++)
            {
                e.Graphics.DrawString(strJWS[i], ht10, Brushes.Black, new Point(x, y + JG * row));
                row++;
            }
            strJWS = StringCut(lb11.Text, 50);
            for (int i = 0; i < strJWS.Count; i++)
            {
                e.Graphics.DrawString(strJWS[i], ptzt, Brushes.Black, new Point(x, y + JG * row));
                row++;
            }
            strJWS = StringCut(lb12.Text, 50);
            for (int i = 0; i < strJWS.Count; i++)
            {
                e.Graphics.DrawString(strJWS[i], ptzt, Brushes.Black, new Point(x, y + JG * row));
                row++;
            }
            strJWS = StringCut(lb13.Text, 50);
            for (int i = 0; i < strJWS.Count; i++)
            {
                e.Graphics.DrawString(strJWS[i], ptzt, Brushes.Black, new Point(x, y + JG * row));
                row++;
            }
            row++;
            e.Graphics.DrawString("   患者或委托人或法定代理人签字："+tbPatRecord.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 210, y + 15 + JG * row, x + 320, y + 15 + JG * row);
            e.Graphics.DrawString("麻醉医师签字：" + cmbMZYS.Text, ptzt, Brushes.Black, new Point(x+340, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 420, y + 15 + JG * row, x + 520, y + 15 + JG * row);

            row++;
            e.Graphics.DrawString("                           日期：" + dtPatRecordDate.Text, ptzt, Brushes.Black, new Point(x, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 210, y + 15 + JG * row, x + 320, y + 15 + JG * row);
            e.Graphics.DrawString("       日期：" + dtMZYSdate.Text, ptzt, Brushes.Black, new Point(x + 340, y + JG * row));
            e.Graphics.DrawLine(Pens.Black, x + 420, y + 15 + JG * row, x + 520, y + 15 + JG * row);


           
          

            #endregion
        }

        public List<string> StringCut(string oldStr, int CutLength)
        {
            DataTable dt = new DataTable();
            List<string> LisStr = new List<string>();
            string[] strJWS = new string[10];
            if (oldStr.Length > 0)
            {

                //换行
                int lt = oldStr.Length / CutLength;
                for (int i = 0; i <= lt; i++)
                {
                    if (i < lt)
                        strJWS[i] = oldStr.Substring(i * CutLength, CutLength);//+ Environment.NewLine
                    else
                        strJWS[i] = oldStr.Substring(i * CutLength);

                    LisStr.Add(strJWS[i]);
                }

            }
            return LisStr;
        }
        public static int StringCutElse(string oldStr, int maxLength)
        {
            int i = 0;
            

            while (oldStr.Length>maxLength)
            {
                oldStr = oldStr.Substring(maxLength);
                i++;
            }

            
            return i;
        }
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.5;
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("K16", 737, 1020);
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
        }
    }
}
