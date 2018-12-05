﻿///************************************
///Updated By        : Senvi
///************************************

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
    public partial class AfterVisit : Form
    {
        #region <<Members>>

        AdimsController bll = new AdimsController();

        #endregion

        #region <<Constructors>>

        /// <summary>
        /// 构造函数
        /// </summary>
        public AfterVisit()
        {
            InitializeComponent();
        }

        #endregion

        #region <<Events>>

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shsf_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            //ds = bll.Sqfs();
            //dgvshsf.DataSource = ds.Tables[0].DefaultView;
            //dgvshsf_CellClick(null, null);
        }

        private void dgvshsf_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClearEdit();
            try
            {
                string patID = Convert.ToString(dgvshsf.SelectedCells[0].Value);
                if (dgvshsf.RowCount == 0 || string.IsNullOrEmpty(patID)) return;
                txtPatid.Controls[0].Text = patID;
                txtPatname.Controls[0].Text = Convert.ToString(dgvshsf.SelectedCells[1].Value);
                txtPatage.Controls[0].Text = Convert.ToString(dgvshsf.SelectedCells[2].Value);
                txtPatsex.Controls[0].Text = Convert.ToString(dgvshsf.SelectedCells[3].Value);
                txtDepartment.Controls[0].Text = Convert.ToString(dgvshsf.SelectedCells[4].Value);
                txtBednumber.Controls[0].Text = Convert.ToString(dgvshsf.SelectedCells[5].Value);
                txtNssss.Controls[0].Text = Convert.ToString(dgvshsf.SelectedCells[6].Value);
                txtSqzd.Controls[0].Text = Convert.ToString(dgvshsf.SelectedCells[7].Value);
                DataTable dt = bll.GetAfterVisit("PatID = '" + txtPatid.Controls[0].Text + "'");
                if (dt.Rows.Count == 0)
                    return;
                SetEditValue(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "出现异常，请检查！");
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveAfterVisit();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintView_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        /// <summary>
        /// 拼写打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font ptzt = new Font("新宋体", 11);//普通字体

            string ap1 = dgvshsf.SelectedCells[11].Value.ToString();
            string oneDays = string.Empty;
            string twoDays = string.Empty;
            string threeDays = string.Empty;

            // 第一天
            if (chkNausea1.Checked == true) oneDays += "恶心 ";
            if (chkVomit1.Checked == true) oneDays += "呕吐 ";
            if (chkHeadache1.Checked == true) oneDays += "头痛 ";
            if (chkWaistlegsPain1.Checked == true) oneDays += "腰腿疼 ";
            if (chkURetention1.Checked == true) oneDays += "尿潴留 ";
            if (chkBPReduce1.Checked == true) oneDays += "血压降低 ";
            if (chkBPElevated1.Checked == true) oneDays += "血压升高 ";
            if (chkHHyperlipidemia1.Checked == true) oneDays += "低氧血症 ";
            if (chkBSuppression1.Checked == true) oneDays += "呼吸抑制 ";
            if (chkThroatPain1.Checked == true) oneDays += "咽喉疼痛 ";
            if (chkLNumbness1.Checked == true) oneDays += "下肢麻木 ";
            oneDays += txtOther1Flag1.Controls[0].Text + " ";
            oneDays += txtOther2Flag1.Controls[0].Text + " ";
            oneDays += txtOther3Flag1.Controls[0].Text + " ";
            oneDays += txtAnalgesicScore1.Controls[0].Text;

            // 第二天
            if (chkNausea2.Checked == true) twoDays += "恶心 ";
            if (chkVomit2.Checked == true) twoDays += "呕吐 ";
            if (chkHeadache2.Checked == true) twoDays += "头痛 ";
            if (chkWaistlegsPain2.Checked == true) twoDays += "腰腿疼 ";
            if (chkURetention2.Checked == true) twoDays += "尿潴留 ";
            if (chkBPReduce2.Checked == true) twoDays += "血压降低 ";
            if (chkBPElevated2.Checked == true) twoDays += "血压升高 ";
            if (chkHHyperlipidemia2.Checked == true) twoDays += "低氧血症 ";
            if (chkBSuppression2.Checked == true) twoDays += "呼吸抑制 ";
            if (chkThroatPain2.Checked == true) twoDays += "咽喉疼痛 ";
            if (chkLNumbness2.Checked == true) twoDays += "下肢麻木 ";
            twoDays += txtOther1Flag2.Controls[0].Text + " ";
            twoDays += txtOther2Flag2.Controls[0].Text + " ";
            twoDays += txtOther3Flag2.Controls[0].Text + " ";
            twoDays += txtAnalgesicScore2.Controls[0].Text;

            // 第三天
            if (chkNausea3.Checked == true) threeDays += "恶心 ";
            if (chkVomit3.Checked == true) threeDays += "呕吐 ";
            if (chkHeadache3.Checked == true) threeDays += "头痛 ";
            if (chkWaistlegsPain3.Checked == true) threeDays += "腰腿疼 ";
            if (chkURetention3.Checked == true) threeDays += "尿潴留 ";
            if (chkBPReduce3.Checked == true) threeDays += "血压降低 ";
            if (chkBPElevated3.Checked == true) threeDays += "血压升高 ";
            if (chkHHyperlipidemia3.Checked == true) threeDays += "低氧血症 ";
            if (chkBSuppression3.Checked == true) threeDays += "呼吸抑制 ";
            if (chkThroatPain3.Checked == true) threeDays += "咽喉疼痛 ";
            if (chkLNumbness3.Checked == true) threeDays += "下肢麻木 ";
            threeDays += txtOther1Flag3.Controls[0].Text + " ";
            threeDays += txtOther2Flag3.Controls[0].Text + " ";
            threeDays += txtOther3Flag3.Controls[0].Text + " ";
            threeDays += txtAnalgesicScore3.Controls[0].Text;

            e.Graphics.DrawString("术 后 随 访 单", new Font("新宋体", 16, FontStyle.Bold), Brushes.Black, new Point(260, 40));
            e.Graphics.DrawString("\n姓名： " + txtPatname.Controls[0].Text.Trim() + "  性别： " + txtPatsex.Controls[0].Text.Trim() + "  年龄： " + txtPatage.Controls[0].Text.Trim() + "  床号： "
                + txtBednumber.Controls[0].Text.Trim(), ptzt, Brushes.Black, new Point(50, 90));
            e.Graphics.DrawString("\n入室情况：血压 " + txtEnterBP.Controls[0].Text.Trim() + "  mmHg  心率 " + txtEnterHeartRate.Controls[0].Text.Trim()
                + "  次/分 Spo2 " + txtEnterSpo2.Controls[0].Text.Trim() + "", ptzt, Brushes.Black, 50, 120);
            e.Graphics.DrawString("\n全麻诱导：主要给药 " + txtMainDrug.Controls[0].Text.Trim(), ptzt, Brushes.Black, 50, 150);
            e.Graphics.DrawString("\n术中出量： 总出量 " + txtSZOutAmount.Controls[0].Text.Trim() + "  总入量 " + txtSZInAmount.Controls[0].Text.Trim(), ptzt, Brushes.Black, 50, 180);
            e.Graphics.DrawString("\n术毕情况：" + txtSBSituation.Controls[0].Text.Trim() + "               术毕去向：" + txtSBWhereabouts.Controls[0].Text.Trim(), ptzt, Brushes.Black, 50, 210);
            e.Graphics.DrawString("\n术后第一天：" + oneDays, ptzt, Brushes.Black, 50, 240);
            e.Graphics.DrawString("\n术后第二天： " + twoDays, ptzt, Brushes.Black, 50, 270);
            e.Graphics.DrawString("\n术后第三天： " + threeDays, ptzt, Brushes.Black, 50, 300);
            e.Graphics.DrawString("\n医师：" + ap1 + "   日期：" + DateTime.Now.ToString(), ptzt, Brushes.Black, 300, 330);

        }

        #endregion

        #region <<Methods>>

        /// <summary>
        /// 控件赋值
        /// </summary>
        private void SetEditValue(DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            txtEnterBP.Controls[0].Text = Convert.ToString(dr["EnterBP"]);
            txtEnterHeartRate.Controls[0].Text = Convert.ToString(dr["EnterHeartRate"]);
            txtEnterSpo2.Controls[0].Text = Convert.ToString(dr["EnterSpo2"]);
            txtMainDrug.Controls[0].Text = Convert.ToString(dr["MainDrug"]);
            txtSZOutAmount.Controls[0].Text = Convert.ToString(dr["SZOutAmount"]);
            txtSZInAmount.Controls[0].Text = Convert.ToString(dr["SZInAmount"]);
            txtSBSituation.Controls[0].Text = Convert.ToString(dr["SBSituation"]);
            txtSBWhereabouts.Controls[0].Text = Convert.ToString(dr["SBWhereabouts"]);
            txtSZBPCauses.Controls[0].Text = Convert.ToString(dr["SZBPCauses"]);
            txtSZHeartRateCauses.Controls[0].Text = Convert.ToString(dr["SZHeartRateCauses"]);
            txtSZSpo2Causes.Controls[0].Text = Convert.ToString(dr["SZSpo2Causes"]);
            txtNauseaValue1.Controls[0].Text = Convert.ToString(dr["NauseaValue1"]);
            txtVomitValue1.Controls[0].Text = Convert.ToString(dr["VomitValue1"]);
            txtHeadacheValue1.Controls[0].Text = Convert.ToString(dr["HeadacheValue1"]);
            txtWaistlegsPainValue1.Controls[0].Text = Convert.ToString(dr["WaistlegsPainValue1"]);
            txtURetentionValue1.Controls[0].Text = Convert.ToString(dr["URetentionValue1"]);
            txtBPReduceValue1.Controls[0].Text = Convert.ToString(dr["BPReduceValue1"]);
            txtBPElevatedValue1.Controls[0].Text = Convert.ToString(dr["BPElevatedValue1"]);
            txtHHyperlipidemiaValue1.Controls[0].Text = Convert.ToString(dr["HHyperlipidemiaValue1"]);
            txtBSuppressionValue1.Controls[0].Text = Convert.ToString(dr["BSuppressionValue1"]);
            txtThroatPainValue1.Controls[0].Text = Convert.ToString(dr["ThroatPainValue1"]);
            txtLNumbnessValue1.Controls[0].Text = Convert.ToString(dr["LNumbnessValue1"]);
            txtOther1Flag1.Controls[0].Text = Convert.ToString(dr["Other1Flag1"]);
            txtOther1Value1.Controls[0].Text = Convert.ToString(dr["Other1Value1"]);
            txtOther2Flag1.Controls[0].Text = Convert.ToString(dr["Other2Flag1"]);
            txtOther2Value1.Controls[0].Text = Convert.ToString(dr["Other2Value1"]);
            txtOther3Flag1.Controls[0].Text = Convert.ToString(dr["Other3Flag1"]);
            txtOther3Value1.Controls[0].Text = Convert.ToString(dr["Other3Value1"]);
            txtAnalgesicScore1.Controls[0].Text = Convert.ToString(dr["AnalgesicScore1"]);
            txtNauseaValue2.Controls[0].Text = Convert.ToString(dr["NauseaValue2"]);
            txtVomitValue2.Controls[0].Text = Convert.ToString(dr["VomitValue2"]);
            txtHeadacheValue2.Controls[0].Text = Convert.ToString(dr["HeadacheValue2"]);
            txtWaistlegsPainValue2.Controls[0].Text = Convert.ToString(dr["WaistlegsPainValue2"]);
            txtURetentionValue2.Controls[0].Text = Convert.ToString(dr["URetentionValue2"]);
            txtBPReduceValue2.Controls[0].Text = Convert.ToString(dr["BPReduceValue2"]);
            txtBPElevatedValue2.Controls[0].Text = Convert.ToString(dr["BPElevatedValue2"]);
            txtHHyperlipidemiaValue2.Controls[0].Text = Convert.ToString(dr["HHyperlipidemiaValue2"]);
            txtBSuppressionValue2.Controls[0].Text = Convert.ToString(dr["BSuppressionValue2"]);
            txtThroatPainValue2.Controls[0].Text = Convert.ToString(dr["ThroatPainValue2"]);
            txtLNumbnessValue2.Controls[0].Text = Convert.ToString(dr["LNumbnessValue2"]);
            txtOther1Flag2.Controls[0].Text = Convert.ToString(dr["Other1Flag2"]);
            txtOther1Value2.Controls[0].Text = Convert.ToString(dr["Other1Value2"]);
            txtOther2Flag2.Controls[0].Text = Convert.ToString(dr["Other2Flag2"]);
            txtOther2Value2.Controls[0].Text = Convert.ToString(dr["Other2Value2"]);
            txtOther3Flag2.Controls[0].Text = Convert.ToString(dr["Other3Flag2"]);
            txtOther3Value2.Controls[0].Text = Convert.ToString(dr["Other3Value2"]);
            txtAnalgesicScore2.Controls[0].Text = Convert.ToString(dr["AnalgesicScore2"]);
            txtNauseaValue3.Controls[0].Text = Convert.ToString(dr["NauseaValue3"]);
            txtVomitValue3.Controls[0].Text = Convert.ToString(dr["VomitValue3"]);
            txtHeadacheValue3.Controls[0].Text = Convert.ToString(dr["HeadacheValue3"]);
            txtWaistlegsPainValue3.Controls[0].Text = Convert.ToString(dr["WaistlegsPainValue3"]);
            txtURetentionValue3.Controls[0].Text = Convert.ToString(dr["URetentionValue3"]);
            txtBPReduceValue3.Controls[0].Text = Convert.ToString(dr["BPReduceValue3"]);
            txtBPElevatedValue3.Controls[0].Text = Convert.ToString(dr["BPElevatedValue3"]);
            txtHHyperlipidemiaValue3.Controls[0].Text = Convert.ToString(dr["HHyperlipidemiaValue3"]);
            txtBSuppressionValue3.Controls[0].Text = Convert.ToString(dr["BSuppressionValue3"]);
            txtThroatPainValue3.Controls[0].Text = Convert.ToString(dr["ThroatPainValue3"]);
            txtLNumbnessValue3.Controls[0].Text = Convert.ToString(dr["LNumbnessValue3"]);
            txtOther1Flag3.Controls[0].Text = Convert.ToString(dr["Other1Flag3"]);
            txtOther1Value3.Controls[0].Text = Convert.ToString(dr["Other1Value3"]);
            txtOther2Flag3.Controls[0].Text = Convert.ToString(dr["Other2Flag3"]);
            txtOther2Value3.Controls[0].Text = Convert.ToString(dr["Other2Value3"]);
            txtOther3Flag3.Controls[0].Text = Convert.ToString(dr["Other3Flag3"]);
            txtOther3Value3.Controls[0].Text = Convert.ToString(dr["Other3Value3"]);
            txtAnalgesicScore3.Controls[0].Text = Convert.ToString(dr["AnalgesicScore3"]);
            if (Convert.ToInt32(dr["AnesthesiaInduction"]) == 0) rdoW.Checked = true;
            else if (Convert.ToInt32(dr["AnesthesiaInduction"]) == 1) rdoSL.Checked = true;
            else if (Convert.ToInt32(dr["AnesthesiaInduction"]) == 2) rdoKN.Checked = true;
            else if (Convert.ToInt32(dr["AnesthesiaInduction"]) == 3) rdoFCKN.Checked = true;
            if (Convert.ToInt32(dr["SZBP"]) == 1) rdoBPPW.Checked = true;
            else rdoBPBD.Checked = true;
            if (Convert.ToInt32(dr["SZHeartRate"]) == 1) rdoXLPW.Checked = true;
            else rdoXLBD.Checked = true;
            if (Convert.ToInt32(dr["SZSpo2"]) == 1) rdoSpo2PW.Checked = true;
            else rdoSpo2BD.Checked = true;

            // 开放静脉
            string openVein = Convert.ToString(dr["OpenVein"]);
            if (openVein.Contains("SZ_")) chkSZ.Checked = true;
            if (openVein.Contains("XZ_")) chkXZ.Checked = true;
            if (openVein.Contains("ZX_")) chkZX.Checked = true;

            // 局麻诱导
            string intubate = Convert.ToString(dr["Intubate"]);
            if (intubate.Contains("SL_")) chkSL.Checked = true;
            if (intubate.Contains("KN_")) chkKN.Checked = true;
            if (intubate.Contains("YG_")) chkYG.Checked = true;
            if (intubate.Contains("CX_")) chkCX.Checked = true;

            // 第一天
            if (Convert.ToInt32(dr["Nausea1"]) == 1) chkNausea1.Checked = true;
            else chkNausea1.Checked = false;
            if (Convert.ToInt32(dr["Vomit1"]) == 1) chkVomit1.Checked = true;
            else chkVomit1.Checked = false;
            if (Convert.ToInt32(dr["Headache1"]) == 1) chkHeadache1.Checked = true;
            else chkHeadache1.Checked = false;
            if (Convert.ToInt32(dr["WaistlegsPain1"]) == 1) chkWaistlegsPain1.Checked = true;
            else chkWaistlegsPain1.Checked = false;
            if (Convert.ToInt32(dr["URetention1"]) == 1) chkURetention1.Checked = true;
            else chkURetention1.Checked = false;
            if (Convert.ToInt32(dr["BPReduce1"]) == 1) chkBPReduce1.Checked = true;
            else chkBPReduce1.Checked = false;
            if (Convert.ToInt32(dr["BPElevated1"]) == 1) chkBPElevated1.Checked = true;
            else chkBPElevated1.Checked = false;
            if (Convert.ToInt32(dr["HHyperlipidemia1"]) == 1) chkHHyperlipidemia1.Checked = true;
            else chkHHyperlipidemia1.Checked = false;
            if (Convert.ToInt32(dr["BSuppression1"]) == 1) chkBSuppression1.Checked = true;
            else chkBSuppression1.Checked = false;
            if (Convert.ToInt32(dr["URetention1"]) == 1) chkThroatPain1.Checked = true;
            else chkThroatPain1.Checked = false;
            if (Convert.ToInt32(dr["LNumbness1"]) == 1) chkURetention1.Checked = true;
            else chkLNumbness1.Checked = false;
            if (Convert.ToInt32(dr["Other11"]) == 1) chkOther11.Checked = true;
            else chkOther11.Checked = false;
            if (Convert.ToInt32(dr["Other21"]) == 1) chkOther21.Checked = true;
            else chkOther21.Checked = false;
            if (Convert.ToInt32(dr["Other31"]) == 1) chkOther31.Checked = true;
            else chkOther31.Checked = false;

            // 第二天
            if (Convert.ToInt32(dr["Nausea2"]) == 1) chkNausea2.Checked = true;
            else chkNausea2.Checked = false;
            if (Convert.ToInt32(dr["Vomit2"]) == 1) chkVomit2.Checked = true;
            else chkVomit2.Checked = false;
            if (Convert.ToInt32(dr["Headache2"]) == 1) chkHeadache2.Checked = true;
            else chkHeadache2.Checked = false;
            if (Convert.ToInt32(dr["WaistlegsPain2"]) == 1) chkWaistlegsPain2.Checked = true;
            else chkWaistlegsPain2.Checked = false;
            if (Convert.ToInt32(dr["URetention2"]) == 1) chkURetention2.Checked = true;
            else chkURetention2.Checked = false;
            if (Convert.ToInt32(dr["BPReduce2"]) == 1) chkBPReduce2.Checked = true;
            else chkBPReduce2.Checked = false;
            if (Convert.ToInt32(dr["BPElevated2"]) == 1) chkBPElevated2.Checked = true;
            else chkBPElevated2.Checked = false;
            if (Convert.ToInt32(dr["HHyperlipidemia2"]) == 1) chkHHyperlipidemia2.Checked = true;
            else chkHHyperlipidemia2.Checked = false;
            if (Convert.ToInt32(dr["BSuppression2"]) == 1) chkBSuppression2.Checked = true;
            else chkBSuppression2.Checked = false;
            if (Convert.ToInt32(dr["URetention2"]) == 1) chkThroatPain2.Checked = true;
            else chkThroatPain2.Checked = false;
            if (Convert.ToInt32(dr["LNumbness2"]) == 1) chkURetention2.Checked = true;
            else chkLNumbness2.Checked = false;
            if (Convert.ToInt32(dr["Other12"]) == 1) chkOther12.Checked = true;
            else chkOther12.Checked = false;
            if (Convert.ToInt32(dr["Other21"]) == 1) chkOther22.Checked = true;
            else chkOther22.Checked = false;
            if (Convert.ToInt32(dr["Other32"]) == 1) chkOther32.Checked = true;
            else chkOther32.Checked = false;

            // 第三天
            if (Convert.ToInt32(dr["Nausea3"]) == 1) chkNausea3.Checked = true;
            else chkNausea3.Checked = false;
            if (Convert.ToInt32(dr["Vomit3"]) == 1) chkVomit3.Checked = true;
            else chkVomit3.Checked = false;
            if (Convert.ToInt32(dr["Headache3"]) == 1) chkHeadache3.Checked = true;
            else chkHeadache3.Checked = false;
            if (Convert.ToInt32(dr["WaistlegsPain3"]) == 1) chkWaistlegsPain3.Checked = true;
            else chkWaistlegsPain3.Checked = false;
            if (Convert.ToInt32(dr["URetention3"]) == 1) chkURetention3.Checked = true;
            else chkURetention3.Checked = false;
            if (Convert.ToInt32(dr["BPReduce3"]) == 1) chkBPReduce3.Checked = true;
            else chkBPReduce3.Checked = false;
            if (Convert.ToInt32(dr["BPElevated3"]) == 1) chkBPElevated3.Checked = true;
            else chkBPElevated3.Checked = false;
            if (Convert.ToInt32(dr["HHyperlipidemia3"]) == 1) chkHHyperlipidemia3.Checked = true;
            else chkHHyperlipidemia3.Checked = false;
            if (Convert.ToInt32(dr["BSuppression3"]) == 1) chkBSuppression3.Checked = true;
            else chkBSuppression3.Checked = false;
            if (Convert.ToInt32(dr["URetention3"]) == 1) chkThroatPain3.Checked = true;
            else chkThroatPain3.Checked = false;
            if (Convert.ToInt32(dr["LNumbness3"]) == 1) chkURetention3.Checked = true;
            else chkLNumbness3.Checked = false;
            if (Convert.ToInt32(dr["Other13"]) == 1) chkOther13.Checked = true;
            else chkOther11.Checked = false;
            if (Convert.ToInt32(dr["Other23"]) == 1) chkOther23.Checked = true;
            else chkOther23.Checked = false;
            if (Convert.ToInt32(dr["Other33"]) == 1) chkOther33.Checked = true;
            else chkOther33.Checked = false;
        }

        /// <summary>
        /// 清空控件
        /// </summary>
        private void ClearEdit()
        {
            txtPatid.Controls[0].Text = string.Empty;
            txtPatname.Controls[0].Text = string.Empty;
            txtPatage.Controls[0].Text = string.Empty;
            txtPatsex.Controls[0].Text = string.Empty;
            txtDepartment.Controls[0].Text = string.Empty;
            txtBednumber.Controls[0].Text = string.Empty;
            userControl11.Controls[0].Text = string.Empty;
            txtNssss.Controls[0].Text = string.Empty;
            txtSqzd.Controls[0].Text = string.Empty;
            txtSS.Controls[0].Text = string.Empty;
            txtmzff.Controls[0].Text = string.Empty;

            txtMainDrug.Controls[0].Text = string.Empty;
            txtEnterBP.Controls[0].Text = string.Empty;
            txtEnterHeartRate.Controls[0].Text = string.Empty;
            txtEnterSpo2.Controls[0].Text = string.Empty;
            txtSZOutAmount.Controls[0].Text = string.Empty;
            txtSZInAmount.Controls[0].Text = string.Empty;
            txtSBSituation.Controls[0].Text = string.Empty;
            txtSBWhereabouts.Controls[0].Text = string.Empty;
            txtSZBPCauses.Controls[0].Text = string.Empty;
            txtSZHeartRateCauses.Controls[0].Text = string.Empty;
            txtSZSpo2Causes.Controls[0].Text = string.Empty;
            txtNauseaValue1.Controls[0].Text = string.Empty;
            txtVomitValue1.Controls[0].Text = string.Empty;
            txtHeadacheValue1.Controls[0].Text = string.Empty;
            txtWaistlegsPainValue1.Controls[0].Text = string.Empty;
            txtURetentionValue1.Controls[0].Text = string.Empty;
            txtBPReduceValue1.Controls[0].Text = string.Empty;
            txtBPElevatedValue1.Controls[0].Text = string.Empty;
            txtHHyperlipidemiaValue1.Controls[0].Text = string.Empty;
            txtBSuppressionValue1.Controls[0].Text = string.Empty;
            txtThroatPainValue1.Controls[0].Text = string.Empty;
            txtLNumbnessValue1.Controls[0].Text = string.Empty;
            txtOther1Flag1.Controls[0].Text = string.Empty;
            txtOther1Value1.Controls[0].Text = string.Empty;
            txtOther2Flag1.Controls[0].Text = string.Empty;
            txtOther2Value1.Controls[0].Text = string.Empty;
            txtOther3Flag1.Controls[0].Text = string.Empty;
            txtOther3Value1.Controls[0].Text = string.Empty;
            txtAnalgesicScore1.Controls[0].Text = string.Empty;
            txtNauseaValue2.Controls[0].Text = string.Empty;
            txtVomitValue2.Controls[0].Text = string.Empty;
            txtHeadacheValue2.Controls[0].Text = string.Empty;
            txtWaistlegsPainValue2.Controls[0].Text = string.Empty;
            txtURetentionValue2.Controls[0].Text = string.Empty;
            txtBPReduceValue2.Controls[0].Text = string.Empty;
            txtBPElevatedValue2.Controls[0].Text = string.Empty;
            txtHHyperlipidemiaValue2.Controls[0].Text = string.Empty;
            txtBSuppressionValue2.Controls[0].Text = string.Empty;
            txtThroatPainValue2.Controls[0].Text = string.Empty;
            txtLNumbnessValue2.Controls[0].Text = string.Empty;
            txtOther1Flag2.Controls[0].Text = string.Empty;
            txtOther1Value2.Controls[0].Text = string.Empty;
            txtOther2Flag2.Controls[0].Text = string.Empty;
            txtOther2Value2.Controls[0].Text = string.Empty;
            txtOther3Flag2.Controls[0].Text = string.Empty;
            txtOther3Value2.Controls[0].Text = string.Empty;
            txtAnalgesicScore2.Controls[0].Text = string.Empty;
            txtNauseaValue3.Controls[0].Text = string.Empty;
            txtVomitValue3.Controls[0].Text = string.Empty;
            txtHeadacheValue3.Controls[0].Text = string.Empty;
            txtWaistlegsPainValue3.Controls[0].Text = string.Empty;
            txtURetentionValue3.Controls[0].Text = string.Empty;
            txtBPReduceValue3.Controls[0].Text = string.Empty;
            txtBPElevatedValue3.Controls[0].Text = string.Empty;
            txtHHyperlipidemiaValue3.Controls[0].Text = string.Empty;
            txtBSuppressionValue3.Controls[0].Text = string.Empty;
            txtThroatPainValue3.Controls[0].Text = string.Empty;
            txtLNumbnessValue3.Controls[0].Text = string.Empty;
            txtOther1Flag3.Controls[0].Text = string.Empty;
            txtOther1Value3.Controls[0].Text = string.Empty;
            txtOther2Flag3.Controls[0].Text = string.Empty;
            txtOther2Value3.Controls[0].Text = string.Empty;
            txtOther3Flag3.Controls[0].Text = string.Empty;
            txtOther3Value3.Controls[0].Text = string.Empty;
            txtAnalgesicScore3.Controls[0].Text = string.Empty;

            rdoW.Checked = false;
            rdoSL.Checked = false;
            rdoKN.Checked = false;
            rdoFCKN.Checked = false;
            rdoBPPW.Checked = false;
            rdoXLPW.Checked = false;
            rdoSpo2PW.Checked = false;
            chkNausea1.Checked = false;
            chkVomit1.Checked = false;
            chkHeadache1.Checked = false;
            chkWaistlegsPain1.Checked = false;
            chkURetention1.Checked = false;
            chkBPReduce1.Checked = false;
            chkBPElevated1.Checked = false;
            chkHHyperlipidemia1.Checked = false;
            chkBSuppression1.Checked = false;
            chkThroatPain1.Checked = false;
            chkLNumbness1.Checked = false;
            chkOther11.Checked = false;
            chkOther21.Checked = false;
            chkOther31.Checked = false;
            chkNausea2.Checked = false;
            chkVomit2.Checked = false;
            chkHeadache2.Checked = false;
            chkWaistlegsPain2.Checked = false;
            chkURetention2.Checked = false;
            chkBPReduce2.Checked = false;
            chkBPElevated2.Checked = false;
            chkHHyperlipidemia2.Checked = false;
            chkBSuppression2.Checked = false;
            chkThroatPain2.Checked = false;
            chkLNumbness2.Checked = false;
            chkOther12.Checked = false;
            chkOther22.Checked = false;
            chkOther32.Checked = false;
            chkNausea3.Checked = false;
            chkVomit3.Checked = false;
            chkHeadache3.Checked = false;
            chkWaistlegsPain3.Checked = false;
            chkURetention3.Checked = false;
            chkBPReduce3.Checked = false;
            chkBPElevated3.Checked = false;
            chkHHyperlipidemia3.Checked = false;
            chkBSuppression3.Checked = false;
            chkThroatPain3.Checked = false;
            chkLNumbness3.Checked = false;
            chkOther13.Checked = false;
            chkOther23.Checked = false;
            chkOther33.Checked = false;
            chkSL.Checked = false;
            chkKN.Checked = false;
            chkYG.Checked = false;
            chkCX.Checked = false;
            chkSZ.Checked = false;
            chkXZ.Checked = false;
            chkZX.Checked = false;
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <returns></returns>
        private void SaveAfterVisit()
        {
            if (!ValiDateAfterVisit()) return;
            Dictionary<string, string> AfterVisitList = new Dictionary<string, string>();
            int result = 0;
            try
            {
                AfterVisitList.Add("EnterBP", txtEnterBP.Controls[0].Text);
                AfterVisitList.Add("EnterHeartRate", txtEnterHeartRate.Controls[0].Text);
                AfterVisitList.Add("EnterSpo2", txtEnterSpo2.Controls[0].Text);

                // 开放静脉
                string openVein = string.Empty;
                if (chkSZ.Checked == true)
                    openVein += "SZ_";
                if (chkXZ.Checked == true)
                    openVein += "XZ_";
                if (chkZX.Checked == true)
                    openVein += "ZX_";
                AfterVisitList.Add("OpenVein", openVein);
                AfterVisitList.Add("MainDrug", txtMainDrug.Controls[0].Text);

                // 局麻诱导
                string intubate = string.Empty;
                if (chkSL.Checked == true)
                    intubate += "SL_";
                if (chkKN.Checked == true)
                    intubate += "KN_";
                if (chkYG.Checked == true)
                    intubate += "YG_";
                if (chkCX.Checked == true)
                    intubate += "CX_";
                AfterVisitList.Add("Intubate", intubate);

                // 插管
                string anesthesiaInduction = string.Empty;
                if (rdoW.Checked == true) AfterVisitList.Add("AnesthesiaInduction", "0");
                else if (rdoSL.Checked == true) AfterVisitList.Add("AnesthesiaInduction", "1");
                else if (rdoKN.Checked == true) AfterVisitList.Add("AnesthesiaInduction", "2");
                else if (rdoFCKN.Checked == true) AfterVisitList.Add("AnesthesiaInduction", "3");
                else AfterVisitList.Add("AnesthesiaInduction", "4");
                AfterVisitList.Add("SZOutAmount", txtSZOutAmount.Controls[0].Text);
                AfterVisitList.Add("SZInAmount", txtSZInAmount.Controls[0].Text);
                AfterVisitList.Add("SBSituation", txtSBSituation.Controls[0].Text);
                AfterVisitList.Add("SBWhereabouts", txtSBWhereabouts.Controls[0].Text);
                if (rdoBPPW.Checked == false) AfterVisitList.Add("SZBP", "0");
                else AfterVisitList.Add("SZBP", "1");
                AfterVisitList.Add("SZBPCauses", txtSZBPCauses.Controls[0].Text);
                if (rdoXLPW.Checked == false) AfterVisitList.Add("SZHeartRate", "0");
                else AfterVisitList.Add("SZHeartRate", "1");
                AfterVisitList.Add("SZHeartRateCauses", txtSZHeartRateCauses.Controls[0].Text);
                if (rdoSpo2PW.Checked == false) AfterVisitList.Add("SZSpo2", "0");
                else AfterVisitList.Add("SZSpo2", "1");
                AfterVisitList.Add("SZSpo2Causes", txtSZSpo2Causes.Controls[0].Text);

                #region <<术后随访情况>>

                // 第一天
                if (chkNausea1.Checked == false) AfterVisitList.Add("Nausea1", "0");
                else AfterVisitList.Add("Nausea1", "1");
                AfterVisitList.Add("NauseaValue1", txtNauseaValue1.Controls[0].Text);

                if (chkVomit1.Checked == false) AfterVisitList.Add("Vomit1", "0");
                else AfterVisitList.Add("Vomit1", "1");
                AfterVisitList.Add("VomitValue1", txtVomitValue1.Controls[0].Text);

                if (chkHeadache1.Checked == false) AfterVisitList.Add("Headache1", "0");
                else AfterVisitList.Add("Headache1", "1");
                AfterVisitList.Add("HeadacheValue1", txtHeadacheValue1.Controls[0].Text);

                if (chkWaistlegsPain1.Checked == false) AfterVisitList.Add("WaistlegsPain1", "0");
                else AfterVisitList.Add("WaistlegsPain1", "1");
                AfterVisitList.Add("WaistlegsPainValue1", txtWaistlegsPainValue1.Controls[0].Text);

                if (chkURetention1.Checked == false) AfterVisitList.Add("URetention1", "0");
                else AfterVisitList.Add("URetention1", "1");
                AfterVisitList.Add("URetentionValue1", txtURetentionValue1.Controls[0].Text);

                if (chkBPReduce1.Checked == false) AfterVisitList.Add("BPReduce1", "0");
                else AfterVisitList.Add("BPReduce1", "1");
                AfterVisitList.Add("BPReduceValue1", txtBPReduceValue1.Controls[0].Text);

                if (chkBPElevated1.Checked == false) AfterVisitList.Add("BPElevated1", "0");
                else AfterVisitList.Add("BPElevated1", "1");
                AfterVisitList.Add("BPElevatedValue1", txtBPElevatedValue1.Controls[0].Text);

                if (chkHHyperlipidemia1.Checked == false) AfterVisitList.Add("HHyperlipidemia1", "0");
                else AfterVisitList.Add("HHyperlipidemia1", "1");
                AfterVisitList.Add("HHyperlipidemiaValue1", txtHHyperlipidemiaValue1.Controls[0].Text);

                if (chkBSuppression1.Checked == false) AfterVisitList.Add("BSuppression1", "0");
                else AfterVisitList.Add("BSuppression1", "1");
                AfterVisitList.Add("BSuppressionValue1", txtBSuppressionValue1.Controls[0].Text);

                if (chkThroatPain1.Checked == false) AfterVisitList.Add("ThroatPain1", "0");
                else AfterVisitList.Add("ThroatPain1", "1");
                AfterVisitList.Add("ThroatPainValue1", txtThroatPainValue1.Controls[0].Text);

                if (chkLNumbness1.Checked == false) AfterVisitList.Add("LNumbness1", "0");
                else AfterVisitList.Add("LNumbness1", "1");
                AfterVisitList.Add("LNumbnessValue1", txtLNumbnessValue1.Controls[0].Text);

                if (chkOther11.Checked == false) AfterVisitList.Add("Other11", "0");
                else AfterVisitList.Add("Other11", "1");
                AfterVisitList.Add("Other1Flag1", txtOther1Flag1.Controls[0].Text);
                AfterVisitList.Add("Other1Value1", txtOther1Value1.Controls[0].Text);

                if (chkOther21.Checked == false) AfterVisitList.Add("Other21", "0");
                else AfterVisitList.Add("Other21", "1");
                AfterVisitList.Add("Other2Flag1", txtOther2Flag1.Controls[0].Text);
                AfterVisitList.Add("Other2Value1", txtOther2Value1.Controls[0].Text);

                if (chkOther31.Checked == false) AfterVisitList.Add("Other31", "0");
                else AfterVisitList.Add("Other31", "1");
                AfterVisitList.Add("Other3Flag1", txtOther3Flag1.Controls[0].Text);
                AfterVisitList.Add("Other3Value1", txtOther3Value1.Controls[0].Text);

                AfterVisitList.Add("AnalgesicScore1", txtAnalgesicScore1.Controls[0].Text);

                // 第二天
                if (chkNausea2.Checked == false) AfterVisitList.Add("Nausea2", "0");
                else AfterVisitList.Add("Nausea2", "1");
                AfterVisitList.Add("NauseaValue2", txtNauseaValue2.Controls[0].Text);

                if (chkVomit2.Checked == false) AfterVisitList.Add("Vomit2", "0");
                else AfterVisitList.Add("Vomit2", "1");
                AfterVisitList.Add("VomitValue2", txtVomitValue2.Controls[0].Text);

                if (chkHeadache2.Checked == false) AfterVisitList.Add("Headache2", "0");
                else AfterVisitList.Add("Headache2", "1");
                AfterVisitList.Add("HeadacheValue2", txtHeadacheValue2.Controls[0].Text);

                if (chkWaistlegsPain2.Checked == false) AfterVisitList.Add("WaistlegsPain2", "0");
                else AfterVisitList.Add("WaistlegsPain2", "1");
                AfterVisitList.Add("WaistlegsPainValue2", txtWaistlegsPainValue2.Controls[0].Text);

                if (chkURetention2.Checked == false) AfterVisitList.Add("URetention2", "0");
                else AfterVisitList.Add("URetention2", "1");
                AfterVisitList.Add("URetentionValue2", txtURetentionValue2.Controls[0].Text);

                if (chkBPReduce2.Checked == false) AfterVisitList.Add("BPReduce2", "0");
                else AfterVisitList.Add("BPReduce2", "1");
                AfterVisitList.Add("BPReduceValue2", txtBPReduceValue2.Controls[0].Text);

                if (chkBPElevated2.Checked == false) AfterVisitList.Add("BPElevated2", "0");
                else AfterVisitList.Add("BPElevated2", "1");
                AfterVisitList.Add("BPElevatedValue2", txtBPElevatedValue2.Controls[0].Text);

                if (chkHHyperlipidemia2.Checked == false) AfterVisitList.Add("HHyperlipidemia2", "0");
                else AfterVisitList.Add("HHyperlipidemia2", "1");
                AfterVisitList.Add("HHyperlipidemiaValue2", txtHHyperlipidemiaValue2.Controls[0].Text);

                if (chkBSuppression2.Checked == false) AfterVisitList.Add("BSuppression2", "0");
                else AfterVisitList.Add("BSuppression2", "1");
                AfterVisitList.Add("BSuppressionValue2", txtBSuppressionValue2.Controls[0].Text);

                if (chkThroatPain2.Checked == false) AfterVisitList.Add("ThroatPain2", "0");
                else AfterVisitList.Add("ThroatPain2", "1");
                AfterVisitList.Add("ThroatPainValue2", txtThroatPainValue2.Controls[0].Text);

                if (chkLNumbness2.Checked == false) AfterVisitList.Add("LNumbness2", "0");
                else AfterVisitList.Add("LNumbness2", "1");
                AfterVisitList.Add("LNumbnessValue2", txtLNumbnessValue2.Controls[0].Text);

                if (chkOther12.Checked == false) AfterVisitList.Add("Other12", "0");
                else AfterVisitList.Add("Other12", "1");
                AfterVisitList.Add("Other1Flag2", txtOther1Flag2.Controls[0].Text);
                AfterVisitList.Add("Other1Value2", txtOther1Value2.Controls[0].Text);

                if (chkOther22.Checked == false) AfterVisitList.Add("Other22", "0");
                else AfterVisitList.Add("Other22", "1");
                AfterVisitList.Add("Other2Flag2", txtOther2Flag2.Controls[0].Text);
                AfterVisitList.Add("Other2Value2", txtOther2Value2.Controls[0].Text);

                if (chkOther32.Checked == false) AfterVisitList.Add("Other32", "0");
                else AfterVisitList.Add("Other32", "1");
                AfterVisitList.Add("Other3Flag2", txtOther3Flag2.Controls[0].Text);
                AfterVisitList.Add("Other3Value2", txtOther3Value2.Controls[0].Text);

                AfterVisitList.Add("AnalgesicScore2", txtAnalgesicScore2.Controls[0].Text);

                // 第三天
                if (chkNausea3.Checked == false) AfterVisitList.Add("Nausea3", "0");
                else AfterVisitList.Add("Nausea3", "1");
                AfterVisitList.Add("NauseaValue3", txtNauseaValue3.Controls[0].Text);

                if (chkVomit3.Checked == false) AfterVisitList.Add("Vomit3", "0");
                else AfterVisitList.Add("Vomit3", "1");
                AfterVisitList.Add("VomitValue3", txtVomitValue3.Controls[0].Text);

                if (chkHeadache3.Checked == false) AfterVisitList.Add("Headache3", "0");
                else AfterVisitList.Add("Headache3", "1");
                AfterVisitList.Add("HeadacheValue3", txtHeadacheValue3.Controls[0].Text);

                if (chkWaistlegsPain3.Checked == false) AfterVisitList.Add("WaistlegsPain3", "0");
                else AfterVisitList.Add("WaistlegsPain3", "1");
                AfterVisitList.Add("WaistlegsPainValue3", txtWaistlegsPainValue3.Controls[0].Text);

                if (chkURetention3.Checked == false) AfterVisitList.Add("URetention3", "0");
                else AfterVisitList.Add("URetention3", "1");
                AfterVisitList.Add("URetentionValue3", txtURetentionValue3.Controls[0].Text);

                if (chkBPReduce3.Checked == false) AfterVisitList.Add("BPReduce3", "0");
                else AfterVisitList.Add("BPReduce3", "1");
                AfterVisitList.Add("BPReduceValue3", txtBPReduceValue3.Controls[0].Text);

                if (chkBPElevated3.Checked == false) AfterVisitList.Add("BPElevated3", "0");
                else AfterVisitList.Add("BPElevated3", "1");
                AfterVisitList.Add("BPElevatedValue3", txtBPElevatedValue3.Controls[0].Text);

                if (chkHHyperlipidemia3.Checked == false) AfterVisitList.Add("HHyperlipidemia3", "0");
                else AfterVisitList.Add("HHyperlipidemia3", "1");
                AfterVisitList.Add("HHyperlipidemiaValue3", txtHHyperlipidemiaValue3.Controls[0].Text);

                if (chkBSuppression3.Checked == false) AfterVisitList.Add("BSuppression3", "0");
                else AfterVisitList.Add("BSuppression3", "1");
                AfterVisitList.Add("BSuppressionValue3", txtBSuppressionValue3.Controls[0].Text);

                if (chkThroatPain3.Checked == false) AfterVisitList.Add("ThroatPain3", "0");
                else AfterVisitList.Add("ThroatPain3", "1");
                AfterVisitList.Add("ThroatPainValue3", txtThroatPainValue3.Controls[0].Text);

                if (chkLNumbness3.Checked == false) AfterVisitList.Add("LNumbness3", "0");
                else AfterVisitList.Add("LNumbness3", "1");
                AfterVisitList.Add("LNumbnessValue3", txtLNumbnessValue3.Controls[0].Text);

                if (chkOther13.Checked == false) AfterVisitList.Add("Other13", "0");
                else AfterVisitList.Add("Other13", "1");
                AfterVisitList.Add("Other1Flag3", txtOther1Flag3.Controls[0].Text);
                AfterVisitList.Add("Other1Value3", txtOther1Value3.Controls[0].Text);

                if (chkOther23.Checked == false) AfterVisitList.Add("Other23", "0");
                else AfterVisitList.Add("Other23", "1");
                AfterVisitList.Add("Other2Flag3", txtOther2Flag3.Controls[0].Text);
                AfterVisitList.Add("Other2Value3", txtOther2Value3.Controls[0].Text);

                if (chkOther33.Checked == false) AfterVisitList.Add("Other33", "0");
                else AfterVisitList.Add("Other33", "1");
                AfterVisitList.Add("Other3Flag3", txtOther3Flag3.Controls[0].Text);
                AfterVisitList.Add("Other3Value3", txtOther3Value3.Controls[0].Text);
                AfterVisitList.Add("AnalgesicScore3", txtAnalgesicScore3.Controls[0].Text);
                AfterVisitList.Add("PatID", txtPatid.Controls[0].Text);

                #endregion

                if (!(bll.GetAfterVisitCount(txtPatid.Controls[0].Text)))// 新增
                    result = bll.InsertAfterVisit(AfterVisitList);
                else
                    result = bll.UpdateAfterVisit(AfterVisitList);
                if (result > 0)
                    MessageBox.Show("保存成功！");
                else
                    MessageBox.Show("保存失败！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "保存出现异常，请检查！");
            }
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool ValiDateAfterVisit()
        {
            if (!ValidationRegex.ValidteData(txtEnterBP.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtEnterBP.Controls[0].Text))
            {
                MessageBox.Show("血压值填写有误，请检查！");
                txtEnterBP.Focus();
                return false;
            }
            if (!ValidationRegex.ValidteData(txtEnterHeartRate.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtEnterHeartRate.Controls[0].Text))
            {
                MessageBox.Show("心率值填写有误，请检查！");
                txtEnterHeartRate.Focus();
                return false;
            }
            if (!ValidationRegex.ValidteData(txtEnterSpo2.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtEnterSpo2.Controls[0].Text))
            {
                MessageBox.Show("Spo2值填写有误，请检查！");
                txtEnterSpo2.Focus();
                return false;
            }
            if (!ValidationRegex.ValidteData(txtSZOutAmount.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtSZOutAmount.Controls[0].Text))
            {
                MessageBox.Show("总出量值填写有误，请检查！");
                txtSZOutAmount.Focus();
                return false;
            }
            if (!ValidationRegex.ValidteData(txtSZInAmount.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtSZInAmount.Controls[0].Text))
            {
                MessageBox.Show("总入量值填写有误，请检查！");
                txtSZInAmount.Focus();
                return false;
            }
            if (!ValidationRegex.ValidteData(txtAnalgesicScore1.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtAnalgesicScore1.Controls[0].Text))
            {
                MessageBox.Show("第一天镇痛评分值填写有误，请检查！");
                txtAnalgesicScore1.Focus();
                return false;
            }
            if (!ValidationRegex.ValidteData(txtAnalgesicScore2.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtAnalgesicScore2.Controls[0].Text))
            {
                MessageBox.Show("第二天镇痛评分值填写有误，请检查！");
                txtAnalgesicScore2.Focus();
                return false;
            }
            if (!ValidationRegex.ValidteData(txtAnalgesicScore3.Controls[0].Text)
                && !ValidationRegex.ValidteDouble(txtAnalgesicScore3.Controls[0].Text))
            {
                MessageBox.Show("第三天镇痛评分值填写有误，请检查！");
                txtAnalgesicScore3.Focus();
                return false;
            }
            return true;
        }

        #endregion
    }
}